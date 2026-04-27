<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ProPlaneacionReparto_Admin.aspx.cs" Inherits="SIANWEB.ProPlaneacionReparto_Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbDesasingar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbProgramaReparto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbAsignar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
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
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table>

                        <tr>
                            <td>
                                <asp:Label ID="lblFechaEntregaInicial" runat="server" Text="Fecha entrega inicial" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="TxtFechaEntregaInicial" runat="server" width="100px">
                                    <Calendar ID="Calendar1" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LblFechaEntregaFinal" runat="server" Text="Fecha entrega final" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="TxtFechaEntregaFinal" runat="server" width="100px">
                                    <Calendar ID="Calendar2" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
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
                                <asp:Label ID="LblPedidoInicial" runat="server" Text="Pedido inicial" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="TxtPedidoInicial" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblPedidoFinal" runat="server" Text="Pedido final" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="TxtPedidoFinal" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td><asp:Label ID="LblOrdenarPor" runat="server" Text="Ordenar Por:"></asp:Label></td>
                            <td>
                                <telerik:radcombobox ID="CmbOrdenarPor" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Fecha Entrega" Value="Ped_FechaEntrega"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Pedido" Value="Id_Ped"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Fecha" Value="Ped_Fecha"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Numero de Cliente" Value="Id_Cte"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Credito" Value="CreditoStr"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Piezas Ordenadas" Value="Ped_Cantidad"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Piezas Disponibles" Value="Ped_CantidadDisponible"></telerik:RadComboBoxItem>
<%--                                    <telerik:RadComboBoxItem Text="% Piezas Disponibles" Value="Ped_PorcentajeCantidadDisponible"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Importe Ordenado" Value="Ped_ImporteOrdenado"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Importe Disponible" Value="Ped_ImporteDisponible"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="% Importe Disponible" Value="Ped_PorcentajeImporteDisponible"></telerik:RadComboBoxItem>
--%>                                    <telerik:RadComboBoxItem Text="% Piezas Asignado" Value="Ped_PorcentajeAsignado"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="% Importe Asignado" Value="Ped_PorcentajeImporteAsignado"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Ruta" Value="Ruta"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:radcombobox>
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
                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="TxtFechaInicial" runat="server" width="100px">
                                    <Calendar ID="Calendar1" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="TxtFechaFinal" runat="server" width="100px">
                                    <Calendar ID="Calendar2" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
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
                                <asp:Label ID="LblClienteInicial" runat="server" Text="Cliente inicial" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="TxtClienteInicial" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblClienteFinal" runat="server" Text="Cliente final" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="TxtClienteFinal" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Desasignar
                            </td>
                            <td>
                                <asp:ImageButton ID="ImbDesasingar" runat="server" ImageUrl="~/Imagenes/check2.png" Width="16px" Height="16px" OnClick="ImbDesasingar_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Panel ID="PnlRuta" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>Numero de Ruta: </td>
                                            <td>
                                                <telerik:radnumerictextbox onpaste="return false" id="TxtRutaFiltro" runat="server"
                                                    width="70px" maxlength="9" minvalue="1">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:radnumerictextbox>
                                            </td>
                                            <td><asp:ImageButton ID="ImbProgramaRepartoPrint" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="imprimir" OnClick="ImbProgramaRepartoPrint_Click" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblRuta" runat="server" Text="Ruta" />
                            </td>
                            <td>
                                                    <telerik:RadComboBox ID="cmbRuta" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblTipoPedidos" runat="server" Text="Tipo Pedidos" />
                            </td>
                            <td>
                                <telerik:radcombobox ID="CmbTipoPedidos" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Todos" Value="0"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Venta Instalada" Value="1"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Cuentas Corporativas" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Venta Instalada y Cuentas Corporativas" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Esporádicos" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:radcombobox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Asignar
                            </td>
                            <td>
                                <asp:ImageButton ID="ImbAsignar" runat="server" ImageUrl="~/Imagenes/check2.png" Width="16px" Height="16px" OnClick="ImbAsignar_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>Picking List</td>
                            <td>
                                <asp:ImageButton ID="ImbPickingList" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="imprimir" OnClick="ImbPickingList_Click" />
                            </td>
                            <td rowspan="3">
                                <asp:Panel ID="PnlPickingOpciones" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RblOpcionPrint" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Picking List Completo" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Picking List por Cliente" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Picking List Concentrado" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Picking List Ruta" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:ImageButton ID="ImbPickingListPrint" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="imprimir" OnClick="ImbPickingListPrint_Click" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Auxiliar almacén" />
                            </td>
                            <td>
                                                    <telerik:RadComboBox ID="cmbAuxiliar" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                            </td>
                            <td>
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblEstatusPedido" runat="server" Text="Estatus Pedido"></asp:Label>
                            </td>
                            <td>
                                <telerik:radcombobox ID="CmbEstatusPedido" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Pedidos Pendientes" Value="1"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Todos" Value="0"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Pedidos Surtidos" Value="2"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:radcombobox>
                            </td>
                            <td>
                                <asp:Label ID="LblCredito" runat="server" Text="Crédito"></asp:Label>
                            </td>
                            <td>
                                <telerik:radcombobox ID="CmbCredito" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Todos" Value=0></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Si" Value=1></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="No" Value=2></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:radcombobox>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImbBuscar" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar" OnClick="ImbBuscar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="8">


                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Por Pedido" AccessKey="P" PageViewID="RadPageViewPorPedido"
                                Value="Por Pedido">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Por Producto" AccessKey="R" PageViewID="RadPageViewPorProducto"
                                Value="Por Producto">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>


                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" >
                        <telerik:RadPageView ID="RadPageViewPorPedido" runat="server">




                    <telerik:radgrid id="rgAsignacion" runat="server" autogeneratecolumns="False" gridlines="None" onpageindexchanged="rg_PageIndexChanged"
                        onneeddatasource="rg_NeedDataSource" onitemcommand="rgAsignacion_ItemCommand" mastertableview-nomasterrecordstext="No se encontraron registros."
                        pagesize="100" allowpaging="True">
                        <MasterTableView>
                             
                            <Columns>                            
                                <telerik:GridBoundColumn DataField="Ruta" HeaderText="Ruta" UniqueName="Ruta">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <HeaderStyle Width="30px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoPedido" HeaderText="Tipo Pedido" UniqueName="TipoPedido">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    <HeaderStyle Width="60px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="Id_Ped">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <HeaderStyle Width="60px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha Pedido" UniqueName="fecha" DataFormatString="{0:dd/MM/yy}">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_FechaEntrega" HeaderText="Fecha Entrega" UniqueName="fechaEntrega" DataFormatString="{0:dd/MM/yy}">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="cliente">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="column5">
                                    <HeaderStyle Width="300px" />
                                    <ItemStyle Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credito" HeaderText="Crédito" UniqueName="column6" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CreditoStr" HeaderText="Crédito" UniqueName="column7">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_PermiteParcialidades" HeaderText="Parcialidades" UniqueName="column8">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Cantidad" HeaderText="Cantidad Pedido" UniqueName="column9">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Facturado" HeaderText="Cantidad Facturado" UniqueName="column10">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Remisionado" HeaderText="Cantidad Remisionado" UniqueName="column11">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Pendiente" HeaderText="Cantidad Pendiente" UniqueName="column12">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Asignado" HeaderText="Cantidad Asignada" UniqueName="column13">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Picking" HeaderText="Cantidad Picking" UniqueName="column14">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_CantidadDisponible" HeaderText="Cantidad Sugerida" UniqueName="column15">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>  
<%--                                <telerik:GridBoundColumn DataField="Ped_PorcentajeCantidadDisponible" HeaderText="%Pzas Disp" UniqueName="column8"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn> --%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Pzas Disp" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPorcentajeCantidadDisponible" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_CantidadDisponible").ToString())/decimal.Parse(Eval("Ped_Cantidad").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
--%>



<%--                                <telerik:GridBoundColumn DataField="Ped_PorcentajeImporteDisponible" HeaderText="%Imp Disp" UniqueName="column8"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
    --%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Imp Disp" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_ImporteDisponible").ToString())/decimal.Parse(Eval("Ped_ImporteOrdenado").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
--%>                                <telerik:GridTemplateColumn UniqueName="Seleccionar">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="ChkSeleccionarTodos" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSeleccionarTodos_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
<%--                                <telerik:GridBoundColumn DataField="Ped_PorcentajeAsignado" HeaderText="%Pzas Asig" UniqueName="column8">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>--%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Pzas Asig" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPorcentajePiezasAsignado" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_Asignado").ToString())/decimal.Parse(Eval("Ped_Cantidad").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
--%>
                                <%--<telerik:GridBoundColumn DataField="Ped_PorcentajeImporteAsignado" HeaderText="%Imp Asig" UniqueName="column8">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                    --%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Imp Asig" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_ImporteAsignado").ToString())/decimal.Parse(Eval("Ped_ImporteOrdenado").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
--%>
                                
                                
                                <telerik:GridTemplateColumn HeaderText="Cambiar Ruta" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgCambiarRuta" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Cambiar Ruta" CommandName="Cambiar Ruta" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>                                
                                
                                
                                <telerik:GridTemplateColumn HeaderText="Operar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Guardar" ConfirmDialogType="RadWindow"
                                    ConfirmText="" Text="Guardar"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/check2.png" ButtonCssClass="aceptar">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="16px" Height="16px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Desea eliminar los datos?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                    ConfirmDialogWidth="350px">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                           
                        </MasterTableView>
                         <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="6"></Scrolling>
                            </ClientSettings>
                    </telerik:radgrid>

                    </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewPorProducto" runat="server">


                    <telerik:radgrid id="rgAsignacionporproducto" runat="server" autogeneratecolumns="False" gridlines="None" onpageindexchanged="rg_PageIndexChangedPorProducto"
                        onneeddatasource="rg_NeedDataSourceporproducto" onitemcommand="rgAsignacionporproducto_ItemCommand" mastertableview-nomasterrecordstext="No se encontraron registros."
                        pagesize="100" allowpaging="True">
                        <MasterTableView>
                             
                            <Columns>
                                <telerik:GridBoundColumn DataField="Ruta" HeaderText="Ruta" UniqueName="Ruta">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <HeaderStyle Width="20px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Num. Prod." UniqueName="Id_Prd">
                                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                                    <HeaderStyle Width="30px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripcion">
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Presentacion" HeaderText="Presentación" UniqueName="Prd_Presentacion">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle Width="20px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_UniNe" HeaderText="Unidades" UniqueName="Prd_UniNe">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle Width="20px" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="Ped_Cantidad" HeaderText="Cantidad Pedido" UniqueName="column9">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Facturado" HeaderText="Cantidad Facturado" UniqueName="column10">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Remisionado" HeaderText="Cantidad Remisionado" UniqueName="column11">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Pendiente" HeaderText="Cantidad Pendiente" UniqueName="column12">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Asignado" HeaderText="Cantidad Asignada" UniqueName="column13">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn> 
                                <telerik:GridBoundColumn DataField="Prd_Disponible" HeaderText="Cantidad Disponible" UniqueName="column14">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Picking" HeaderText="Cantidad Picking" UniqueName="column15">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_CantidadDisponible" HeaderText="Cantidad Sugerida" UniqueName="column16">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>  

<%--                                <telerik:GridBoundColumn DataField="Ped_PorcentajeCantidadDisponible" HeaderText="%Pzas Disp" UniqueName="column8"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>--%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Pzas Disp" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPorcentajeCantidadDisponible" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_CantidadDisponible").ToString())/decimal.Parse(Eval("Ped_Cantidad").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                               <telerik:GridBoundColumn DataField="Ped_ImporteOrdenado" HeaderText="Imp Ord" UniqueName="column8" DataFormatString="{0:N2}"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_ImporteDisponible" HeaderText="Imp Disp" UniqueName="column9" DataFormatString="{0:N2}"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_PorcentajeImporteDisponible" HeaderText="%Imp Disp" UniqueName="column8"  Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                --%>
<%--                                <telerik:GridTemplateColumn HeaderText="%Imp Disp" UniqueName="column9">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Math.Round((Int32.Parse(Eval("Ped_ImporteDisponible").ToString())/decimal.Parse(Eval("Ped_ImporteOrdenado").ToString())) * 100,0) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
--%>                                <telerik:GridTemplateColumn UniqueName="Seleccionar">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="ChkSeleccionarTodos" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSeleccionarTodos_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
<%--                                <telerik:GridBoundColumn DataField="Ped_PorcentajeAsignado" HeaderText="%Pzas Asig" UniqueName="column8">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
    --%>
                                <%--
                                <telerik:GridBoundColumn DataField="Ped_PorcentajeImporteAsignado" HeaderText="%Imp Asig" UniqueName="column8">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                    --%>
                                <telerik:GridTemplateColumn HeaderText="Operar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Guardar" ConfirmDialogType="RadWindow"
                                    ConfirmText="" Text="Guardar"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/check2.png" ButtonCssClass="aceptar">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="16px" Height="16px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Desea eliminar los datos?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                    ConfirmDialogWidth="350px">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                           
                        </MasterTableView>
                         <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="6"></Scrolling>
                            </ClientSettings>
                    </telerik:radgrid>




                    </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function popup() {
                var oWnd = radopen("WebMap01.aspx", "AbrirVentana_Version");
                oWnd.center();
            }
            function AbrirVentana_PlaneacionReparto(Id, Id_Cte, Nom_Cte, Fecha, Territorio, Credito,Ruta) {


                var oWnd = radopen("ProPlaneacionReparto.aspx?Id=" + Id + "&Id_Cte=" + Id_Cte + "&Nom_Cte=" + Nom_Cte + "&Fecha=" + Fecha + "&Territorio=" + Territorio + "&Credito=" + Credito + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>&Ruta=" + Ruta, "AbrirVentana_PlaneacionReparto");
                oWnd.center();
            }
            function AbrirVentana_PlaneacionReparto_DireccionEntrega(Id, Id_Cte, Nom_Cte, Fecha, Territorio, Credito, Ruta) {
                var oWnd = radopen("ProPlaneacionRepartoDireccionEntregaRuta.aspx?Id=" + Id + "&Id_Cte=" + Id_Cte + "&Nom_Cte=" + Nom_Cte + "&Fecha=" + Fecha + "&Territorio=" + Territorio + "&Credito=" + Credito + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>&Ruta=" + Ruta, "AbrirVentana_PlaneacionReparto");
                oWnd.center();
            }



            function AbrirVentana_ProAsignPedxPrd(Id_Prd, Prd_Nom,Ruta) {

                var oWnd = radopen("ProAsignPedxPrd_Picking.aspx?Id_Prd=" + Id_Prd + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>&Ruta=" + Ruta, "AbrirVentana_PlaneacionReparto");
                oWnd.center();
                return false;
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
            function ClientTabSelecting(sender, args) {

            }
            function onResize(sender, eventArgs) {

            }
        </script>
    </telerik:radcodeblock>
</asp:Content>
