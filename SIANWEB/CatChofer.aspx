<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatChofer.aspx.cs" Inherits="SIANWEB.CatChofer" %>
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
            <telerik:AjaxSetting AjaxControlID="rgChoferes">
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
                                <asp:Label ID="lblId_Chofer" runat="server" Text="Clave Chofer"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdChofer" runat="server" Width="100px" MaxLength="9"
                                    MinValue="1" Enabled="False">
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
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblApellidoPaterno" runat="server" Text="Apellido Paterno"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtApellidoPaterno" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblApellidoMaterno" runat="server" Text="Apellido Materno"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtApellidoMaterno" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
           
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:Label ID="lblRfc" runat="server" Text="Rfc"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtRfc" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLicencia" runat="server" Text="Licencia"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtLicencia" runat="server"
                                    Width="200px" MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                             
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
                        </tr>
                        <tr>
                            <td style="height: 11px">
                                &nbsp;
                            </td>
                            <td width="70" style="height: 11px">
                                &nbsp;
                            </td>
                            <td width="83" style="height: 11px">
                                &nbsp;
                            </td>
                            <td width="40" style="height: 11px">
                                &nbsp;
                            </td>
                            <td width="100" style="height: 11px">
                                &nbsp;
                            </td>
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
                                <telerik:RadGrid ID="rgChoferes" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnItemCommand="rgChoferes_ItemCommand" OnNeedDataSource="rgChoferes_NeedDataSource"
                                    OnPageIndexChanged="rgChoferes_PageIndexChanged">
                                    <MasterTableView DataKeyNames="Folio" DataMember="listChoferes" PageSize ="10">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Folio" DataField="Folio"  Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Nombre" UniqueName="Nombre" DataField="Nombre" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Apellido Paterno" UniqueName="ApellidoPaterno" DataField="ApellidoPaterno" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ApellidoMaterno" HeaderText="Apellido Materno" UniqueName="ApellidoMaterno" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Rfc" UniqueName="Rfc" DataField="Rfc"  Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumLicencia" HeaderText="Licencia" UniqueName="NumLicencia" Display="true">
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
                                        ShowPagerText="True" PageButtonCount="10" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>