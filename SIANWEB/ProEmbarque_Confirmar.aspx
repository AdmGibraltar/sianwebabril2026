<%@ Page Title="Facturas en embarque" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" 
    AutoEventWireup="true" CodeBehind="ProEmbarque_Confirmar.aspx.cs" Inherits="SIANWEB.ProEmbarque_Confirmar" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function callConfirm(mensaje) {
                radconfirm(mensaje, confirmCallBackFn);
            }
            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%=RAM1.ClientID%>");
                if (arg) {
                    ajaxManager.ajaxRequest('Aceptar');
                }
                else {
                    ajaxManager.ajaxRequest('Cancelar');
                }
            }
          </script>
    </telerik:RadCodeBlock>
     <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="DivPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="DivPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEmbarque">
                <UpdatedControls>
              <telerik:AjaxUpdatedControl ControlID="DivPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

    <div runat ="Server" id ="DivPrincipal" >
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" Height="30" OnButtonClick="RadToolBar1_ButtonClick">
            
        </telerik:RadToolBar>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
        <br />        

        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right; width:150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
               <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table width ="99%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td >
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
                                <asp:Label ID="lblNome" Text="Nombre" runat="server">
                                 </asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="300px">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCte" Text="Cliente" runat="server">
                                 </asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="90px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
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
                                <asp:Label ID="lblFIni" Text="Fecha inicial" runat="server">
                                 </asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="120px">
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
                                <asp:Label ID="lblFFin" Text="Fecha final" runat="server"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="120px">
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
                            <td align=right>
                                &nbsp;</td>
                            <td>
                                &nbsp;<asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                              
                                 <asp:Label ID="Label1" Text="Tipo Doc." runat="server">
                                 </asp:Label>
                            </td>
                            <td colspan ="3">
                               
                                <telerik:RadComboBox runat ="Server"  ID ="CmbTipo" >
                                    <Items>
                                    <telerik:RadComboBoxItem runat ="Server" Value ="-1" Text="--Todos--" Selected="true" />
                                    <telerik:RadComboBoxItem runat="Server" Value ="1" Text= "Factura"  />
                                    <telerik:RadComboBoxItem runat="Server" Value ="2" Text ="Remisión"/> 
                                    </Items> 
                                </telerik:RadComboBox>
                                
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                               
                            </td>
                            <td>
                                 <asp:ImageButton ID="ImgConfirmaTodos" runat="server" 
                                    ToolTip="Confirmar seleccionados"  ImageUrl="~/Imagenes/check2.png" Height="24px" 
                                    Width="24px"  OnClientClick="callConfirm('¿Esta seguro que desea confirmar los documentos seleccionados? <br/><br/> Todos los documentos pasaran a estatus <b> Entregado </b>')" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <telerik:RadGrid ID="rgEmbarque" runat="server" AutoGenerateColumns="False" Widht="500px"
                        GridLines="None" OnNeedDataSource="rgEmbarque_NeedDataSource" OnItemDataBound="rgEmbarque_ItemDataBound" 
                        OnItemCommand="rgEmbarque_ItemCommand" OnPageIndexChanged="rgEmbarque_PageIndexChanged" 
                        PageSize="3" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                        
                        <MasterTableView ClientDataKeyNames="Id_DocStr">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                               <telerik:GridBoundColumn DataField="Id_Emp" UniqueName="Id_Emp" Display ="false">
                                </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="Id_Cd" UniqueName="Id_Cd" Display ="false">
                                </telerik:GridBoundColumn>
                               <telerik:GridBoundColumn DataField="Id_Doc" UniqueName="Id_Doc" Display ="false">
                                </telerik:GridBoundColumn>
                              <telerik:GridTemplateColumn HeaderText="UniqueID" DataField="UniqueID" UniqueName="UniqueID" display="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblUniqueID" runat="server" Text='<%# Eval("UniqueID")%>'></asp:Label>
                                                    </ItemTemplate>
                               </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="UniqueID" UniqueName="UniqueID" Display ="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_Tipo" UniqueName="Emb_Tipo" Display ="false">
                                </telerik:GridBoundColumn>
                                  <telerik:GridTemplateColumn UniqueName="Seleccionar">
                                                <HeaderTemplate>
                                                         <asp:CheckBox ID="CheckBox1" runat="server"  
                                                        autopostback="true" oncheckedchanged="ChkSeleccionarTodo_CheckedChanged"  />
                                                </ItemTemplate>
                                              </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSeleccionado" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Seleccionado") %>'
                                                        Style="cursor: hand" oncheckedchanged="ChkSeleccionado_CheckedChanged" autopostback="true"  />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                            </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Doc_EstatusStr" HeaderText="Estatus" UniqueName="Doc_EstatusStr">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_DocStr" HeaderText="Documento" UniqueName="Id_DocStr">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Doc_Fecha" HeaderText="Fecha" UniqueName="Doc_Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn DataField="Id_Emb" HeaderText="Embarque" UniqueName="Id_Emb">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                    <HeaderStyle HorizontalAlign="Center" Width="250px"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Confirmar" HeaderText="Confirmar" ConfirmDialogType="RadWindow"
                                    ConfirmText="Se cambiará el estatus del documento <b>#[[ID]]</b> a entregado<br/><br/> ¿Desea continuar?</br></br>"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="380px" Text="Confirmar" UniqueName="Confirmar"
                                    ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>