<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" validateRequest="false"  AutoEventWireup="true" CodeBehind="CapQueja_Lista.aspx.cs" Inherits="SIANWEB.CapQueja_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    //--------------------------------------------------------------------------------------------------
    //Abre la ventana de edición de Quejas
    //--------------------------------------------------------------------------------------------------
    function AbrirVentana_Quejas(Id_Queja, Id_Cd, Id_Emp, Tipo) {
        var oWnd = $find("<%=RAM1.ClientID%>");
        oWnd = radopen("CapQueja.aspx?Id_Queja=" + Id_Queja
            + "&Id_Cd=" + Id_Cd
            + "&Id_Emp=" + Id_Emp
            + "&Tipo=" + Tipo
            , "AbrirVentana_Quejas");
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
                refreshGrid_Quejas('VariableSesionDestruir');
                AbrirVentana_Quejas(0, 310, 1);
                break;
            case 'Edit':
                continuarAccion = false;
                refreshGrid_Quejas('VariableSesionDestruir');
                break;
        }
        args.set_cancel(!continuarAccion);
    }
    //********************************
    //refrescar grid
    //********************************
    function refreshGrid() {
        var ajaxManager = $find("<%= RAM1.ClientID %>");
        ajaxManager.ajaxRequest('RebindGrid');
    }
    //--------------------------------------------------------------------------------------------------
    // Actualiza el Grid cuando se cierra la ventana de detalle
    //--------------------------------------------------------------------------------------------------
    function refreshGrid_Fac(accion) {
        var ajaxManager = $find("<%= RAM1.ClientID %>");
        ajaxManager.ajaxRequest(accion);
    }
</script>
</telerik:RadCodeBlock>

    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    
     <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
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
            <telerik:RadWindow ID="AbrirVentana_Quejas" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="1100px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Queja"
                Modal="True" Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
                InitialBehaviors="Maximize" OnClientClose="refreshGrid">
            </telerik:RadWindow>

           <%-- <telerik:RadWindow ID="AbrirVentana_Quejas" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="true" KeepInScreenBounds="True" Overlay="True" Title="Queja"
                Modal="True" Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
               InitialBehaviors="Maximize"> 
            </telerik:RadWindow>  --%>

        </Windows>
    </telerik:RadWindowManager>


       <telerik:RadToolBar  id="RadToolBar1" runat="server" dir = "rtl" onbuttonclick="RadToolBar1_ButtonClick" Width="100%" >
        <Items>
                <telerik:RadToolBarButton CommandName="Nuevo" Value="Nuevo" ToolTip="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
        </Items>
     </telerik:RadToolBar>
      
       <div class="formulario" id="divPrincipal" runat="server">
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
       <table>
            <tr>
                <td>
                    <div id="filtros" runat="server">
                        <table border="0">
                            <tr>
                                <td class="style2">
                                    Queja</td>
                                <td>
                                    <telerik:RadTextBox ID="txtqueja" runat="server" Width="65px" MaxLength="70" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="style2">
                                    Tipo Queja
                                </td>
                                <td colspan ="2">
                                    <telerik:RadComboBox ID="cmbtquejas" runat="server" Width="250px" Height="200px"
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
                            </tr>
                           
                            <tr>
                                <td width="110">
                                    Embarque</td>
                                <td>
                                   
                                    <telerik:RadTextBox ID="txtEmbarque" runat="server" MaxLength="70" 
                                        onpaste="return false" Width="125px">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                   
                                </td>
                                <td width="110">
                                    Guia Flete</td>
                                <td>
                                    <telerik:RadTextBox ID="txtFlete" runat="server" MaxLength="70" 
                                        onpaste="return false" Width="125px">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td >
                                    &nbsp;
                                </td>
                            </tr>
                           
                            <tr>
                                <td width="110">
                                    Fecha inicial
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="150px">
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
                                <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click" ToolTip="Buscar"/>
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
            <telerik:RadGrid ID="rgQuejas" runat="server" AutoGenerateColumns="false" AllowPaging = "true" PageSize ="10" onitemcommand="rgQuejas_ItemCommand" 
                onpageindexchanged="rgQuejas_PageIndexChanged" onneeddatasource="rgQuejas_NeedDataSource">
         
             <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Emp, Id_Cd, Id_Queja" EditMode="InPlace" DataMember="dtQuejas" HorizontalAlign="NotSet" 
             AutoGenerateColumns="false" NoMasterRecordsText="No se encontraron registros.">
            
                <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />

                <Columns>
                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp" >
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd" >
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Id_Queja" HeaderText="Queja" Display="true" UniqueName="Id_Queja" >
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Nom_Cliente" HeaderText="Cliente" Display="true" UniqueName="Nom_Cliente" >
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="TipoQueja" HeaderText="Tipo Queja" Display="true" UniqueName="TipoQueja" >
                    <HeaderStyle Width="230px" HorizontalAlign="Center" />
                    <ItemStyle Width="230px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" Display="true" UniqueName="FechaCreacion" HeaderStyle-Width = "75px">
                            <HeaderStyle Width="90px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Embarque" HeaderText="Embarque" Display="true" UniqueName="Embarque">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Guia_Flete" HeaderText="Guia de Flete" Display="true" UniqueName="Guia_Flete">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>


                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server"  CssClass="edit" ToolTip="Editar" CommandName="Editar" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>

                  <%--  <telerik:GridTemplateColumn HeaderText="Crear Copia" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server"  CssClass="edit" ToolTip="Copiar" CommandName="Copiar" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>--%>
           
                </Columns>

            </MasterTableView>
               
                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" 
                        PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                        ShowPagerText="True" PageButtonCount="3" />
                <ClientSettings>
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
         </telerik:RadGrid>
        </td>
    </tr>
    </table>
       </div>
    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />

</asp:Content>