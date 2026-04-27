<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatVehiculo.aspx.cs" Inherits="SIANWEB.CatVehiculo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgVehiculos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>


    <div class="formulario" id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
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
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label4" runat="server" Text="Centro de distribuci&oacute;n"></asp:Label>
                </td>
              <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblId_Vehiculo" runat="server" Text="Clave Vehículo"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdVehiculo" runat="server" Width="70px" MaxLength="9" Enabled ="false"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
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
                                &nbsp;</td>
                            <td style="width: 224px">
                                Clave Conf. Vehículo</td>
                            <td style="width: 285px">
                                <telerik:RadTextBox onpaste="return false" ID="txtId_ConfigVehiculo" runat="server"
                                    Width="100px" MaxLength="250" Enabled="False">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                            &nbsp;Vehículo</td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Configuración Vehículo</td>
                            <td style="width: 285px">
                                <telerik:RadComboBox runat="server" ID="cmbConfiguracion" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cmbConfiguracion_SelectedIndexChanged" TabIndex="5"></telerik:RadComboBox></td>
                        </tr>

                       <tr>
                            <td>
                                Placa Vehículo</td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtPlaca" runat="server"
                                    Width="75px" MaxLength="250" TabIndex="1">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Clave Permíso SCT</td>
                            <td style="width: 285px">
                                <telerik:RadTextBox onpaste="return false" ID="txtIdPermisoSCT" runat="server"
                                    Width="100px" MaxLength="250" Enabled="False">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                             </td>
                        </tr>
                        
                       <tr>
                            <td>
                                Modelo Vehículo</td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="txtModelo" runat="server" Width="100px" MaxLength="4"
                                    MinValue="0000" Enabled="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Permíso SCT</td>
                            <td style="width: 285px">
                                <telerik:RadComboBox runat="server" ID="cmbPermisoSCT" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cmbPermisoSCT_SelectedIndexChanged" TabIndex="6"></telerik:RadComboBox></td>
                        </tr>
                        
                        <tr>
                            <td>
                                Tipo Remolque</td>
                            <td colspan="3">
                              <telerik:RadComboBox runat="server" ID="cmbTipoRemolque" Width="200px" TabIndex="3"></telerik:RadComboBox></td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Núm. Permíso</td>
                            <td style="width: 285px">
                                <telerik:RadTextBox onpaste="return false" ID="txtNumPermiso" runat="server"
                                    Width="200px" MaxLength="250" TabIndex="7">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Placa Remolque</td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtPlacaRemolque" runat="server"
                                    Width="75px" MaxLength="250" TabIndex="4">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Políza</td>
                            <td style="width: 285px">
                                <telerik:RadTextBox onpaste="return false" ID="txtPoliza" runat="server"
                                    Width="200px" MaxLength="250" TabIndex="8">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                Aseguradora</td>
                            <td style="width: 285px">
                                <telerik:RadTextBox onpaste="return false" ID="txtAseguradora" runat="server"
                                    Width="200px" MaxLength="250" TabIndex="9">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hiddenActualiza" runat="server" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 224px">
                                &nbsp;</td>
                            <td style="width: 285px">
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="1200px">
                                <telerik:RadGrid ID="rgVehiculos" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    PageSize="8" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnItemCommand="rgVehiculos_ItemCommand" OnNeedDataSource="rgVehiculos_NeedDataSource"
                                    OnPageIndexChanged="rgVehiculos_PageIndexChanged">
                                    <MasterTableView DataKeyNames="Folio" DataMember="listChoferes" PageSize ="8">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText=" Id Vehiculo" UniqueName="Folio" DataField="Folio"  Display="true">
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderText="Vehículo" UniqueName="Nom_Vehiculo" DataField="Nom_Vehiculo" Display="true" >
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Placa Vehículo" UniqueName="PlacaVehiculo" DataField="PlacaVehiculo" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Modelo" UniqueName="ModeloVehiculo" DataField="ModeloVehiculo" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Tipo Remolque" UniqueName="TipoRemolque" DataField="TipoRemolque" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Placa Remolque" UniqueName="PlacaRemolque" DataField="PlacaRemolque" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText=" Id Configuración Vehículo" UniqueName="Id_ConfigVehiculo" DataField="Id_ConfigVehiculo"  Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Nom. Configuración Vehículo" UniqueName="ConfiguracionVehiculo" DataField="ConfiguracionVehiculo"  Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Clave Permiso SCT" UniqueName="IdPermisoSCT" DataField="IdPermisoSCT" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Permiso SCT" UniqueName="PermisoSCT" DataField="PermisoSCT" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Núm. Permiso" UniqueName="NumPermiso" DataField="NumPermiso" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Políza" UniqueName="Poliza" DataField="Poliza" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Aseguradora" UniqueName="Aseguradora" DataField="Aseguradora" Visible="true">
                                            </telerik:GridBoundColumn>
                       

                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        ShowPagerText="True" PageButtonCount="5" />
                                </telerik:RadGrid>
                                    </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>