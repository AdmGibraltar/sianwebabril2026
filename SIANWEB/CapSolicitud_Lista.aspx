<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapSolicitud_Lista.aspx.cs" Inherits="SIANWEB.CapSolicitud_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        //--------------------------------------------------------------------------------------------------
        //Abre la ventana de edición de Solicitudes
        //--------------------------------------------------------------------------------------------------
        function AbrirVentana_Solicitud(Id_Solicitud) {
            debugger;
            var oWnd = radopen("CapSolicitud.aspx?Id_Solicitud=" + Id_Solicitud
                        , "AbrirVentana_Solicitud");
            oWnd.set_showOnTopWhenMaximized(false);
            oWnd.maximize();
            oWnd.center();
        }
        function ToolBar_ClientClick(sender, args) {
            debugger;
            var button = args.get_item();
            var continuarAccion = true;

            switch (button.get_value()) {
                case 'new':
                    continuarAccion = false;
                    refreshGrid_Solicitud('VariableSesionDestruir');
                    AbrirVentana_Solicitud(0, 0);
                    break;
                case 'Edit':
                    continuarAccion = false;
                    refreshGrid_Solicitud('VariableSesionDestruir');
                    break;
            }
            args.set_cancel(!continuarAccion);
        }

        //--------------------------------------------------------------------------------------------------
        // Actualiza el Grid cuando se cierra la ventana de detalle
        //--------------------------------------------------------------------------------------------------
        function refreshGrid_Solicitud(accion) {
            debugger;
            var ajaxManager = $find("<%= RAM.ClientID %>");
            ajaxManager.ajaxRequest(accion);
        }

        //--------------------------------------------------------------------------------------------------
        // Se ejecuata cuando el radWindow del detalle de factura se cierra,
        // Esta función es invocada por el evento 'radWindowClose'
        //--------------------------------------------------------------------------------------------------
        function CerrarWindow_ClientEvent(sender, eventArgs) {
            debugger;
            var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
            if (HD_GridRebind.value == '1') {
                refreshGrid_Fac('RebindGrid');
            }
            else {
                refreshGrid_Quejas('QuejasVariableSesionDestruir');
            }
        }

        function LimpiarBanderaRebind(sender, eventArgs) {
            ModificaBanderaRebind('0');
        }

        function ActivarBanderaRebind() {
            ModificaBanderaRebind('1');
        }

        function ModificaBanderaRebind(valor) {
            var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
            HD_GridRebind.value = valor;
        }
    </script>
</telerik:RadCodeBlock>
    
 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    
     <telerik:RadAjaxManager ID="RAM" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>      
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
             <telerik:RadWindow ID="AbrirVentana_Solicitud" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Solicitud"
                Modal="True" Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
               InitialBehaviors="Maximize"> 
            </telerik:RadWindow>  
        </Windows>
    </telerik:RadWindowManager>
    
    <telerik:RadToolBar  ID="RadToolBar1" runat="server" onbuttonclick="RadToolBar1_ButtonClick"  Width="100%" >
        <%--<Items>
            <telerik:RadToolBarButton Text="" CommandName="Nuevo" ToolTip ="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png"/>
        </Items>--%>
     </telerik:RadToolBar> 
     <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
    <div class="formulario" id="divPrincipal" runat="server">    
        <table>
            <tr>
                <td>
                    <div id="filtros" runat="server">
                        <table border="0">
                            <tr>
                                <td class="style2">
                                    Solicitud</td>
                                <td>
                                    <telerik:RadTextBox ID="txtIdSolicitud" runat="server" Width="65px" MaxLength="70" >
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td width="110">
                                    Nombre Cliente</td>
                                <td>
                                   
                                    <telerik:RadTextBox ID="txtNomCliente" runat="server" Height="16px" MaxLength="70" 
                                        Width="250px">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                   
                                </td>
                                <td width="70">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                
                            </tr>
                            <tr>
                                <td width="110">
                                    Num&nbsp; Factura</td>
                                <td>
                                    <telerik:RadTextBox ID="txtFactura" runat="server" Height="16px" MaxLength="70" 
                                        Width="125px">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                
                            </tr>
                             <tr>
                                <td>
                                    Fecha inicial
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="125px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="70">
                                    Fecha final
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="150px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha2" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput2" runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Tipo de Servicio</td>
                                <td>
                                    <telerik:RadComboBox ID="cmbTipoServicio" runat="server" Width="250px" Height="200px"
                                        MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Accion de Cierre</td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAccionCierre" runat="server" Width="250px" Height="200px"
                                        MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                             <tr>
                                <td>Estatus</td>
                                <td>
                                    <telerik:RadComboBox ID="CmbEstado" runat="server" Width="250px" Height="200px" AutoPostBack = "true"
                                        MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                 </td>
                                 <td></td>
                                 <td></td>
                                 <td>
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click" ToolTip="Buscar" />
                                 </td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table> 

        <table border = "0" >
            <tr>
                <td>
                  <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="1200px" BorderStyle="Solid" BorderWidth="1px">
                       <telerik:RadGrid ID="rgFolios" runat="server" GridLines="None"  AutoGenerateColumns="False" DataMember="lstSolicitud" style="margin-bottom: 0px"  PageSize = "10"
        onneeddatasource="rgFolios_NeedDataSource" AllowPaging = "True" onitemcommand="rgFolios_ItemCommand"  CellSpacing="0" onpageindexchanged="rgFolios_PageIndexChanged" 
        Width="1200px" MasterTableView-NoMasterRecordsText="No se encontraron registros." >

            <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" >
                
                <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
               
                <Columns>

               <%-- Columnas --%>
                    <telerik:GridBoundColumn DataField="Id_Solicitud" HeaderText="Solicitud" Display="true" UniqueName="Id_Solicitud" HeaderStyle-Width = "40px">
                            <HeaderStyle Width="40px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Cte_Nom" HeaderText="Nombre Cliente" Display="true" UniqueName="Cte_Nom" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Num_Factura" HeaderText="Núm. Factura" Display="true" UniqueName="Num_Factura" HeaderStyle-Width = "70px">
                            <HeaderStyle Width="70px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" Display="true" UniqueName="FechaCreacion" HeaderStyle-Width = "75px">
                            <HeaderStyle Width="90px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Tipo_Servicio" HeaderText="Tipo Servicio" Display="true" UniqueName="Tipo_Servicio" HeaderStyle-Width = "100px">
                             <HeaderStyle Width="100px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="estado" HeaderText="Estatus" Display="true" UniqueName="Estatus"  HeaderStyle-Width = "30px">
                             <HeaderStyle Width="30px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Accion" HeaderText="Accion Cierre" Display="true" UniqueName="Accion"  HeaderStyle-Width = "30px">
                             <HeaderStyle Width="30px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                <%--Botones --%> 
               
                   <telerik:GridTemplateColumn HeaderText="Ver" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnEditar" runat="server"  CssClass="edit" ToolTip="Editar" CommandName="Modificar" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                   </telerik:GridTemplateColumn>
                   
                   <telerik:GridButtonColumn ButtonCssClass="baja" HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  UniqueName = "Eliminar"
                   ConfirmDialogType="RadWindow" ConfirmText="¿Está seguro de dar de baja la solicitud?" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ButtonType ="ImageButton" 
                   ImageUrl ="~/Imagenes/blank.png" CommandName = "Eliminar" >
                    <HeaderStyle HorizontalAlign="Center" Width="35"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </telerik:GridButtonColumn>                                   

             </Columns>     
                     
             </MasterTableView>
              <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
          
          <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                ShowPagerText="True" PageButtonCount="3" />
        </telerik:RadGrid> 
                  </asp:Panel>
                </td>
            </tr>
       </table>
    </div>

    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />

</asp:Content>
