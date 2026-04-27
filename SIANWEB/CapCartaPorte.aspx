<%@ Page Title="Carta Porte" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="CapCartaPorte.aspx.cs" Inherits="SIANWEB.CapCartaPorte" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
     <telerik:RadWindowManager ID="RAM" RenderMode="Lightweight" runat="server" EnableShadow="true">
     </telerik:RadWindowManager>  
     
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
     </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
                </telerik:AjaxSetting>
                
                <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="rgProductos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
             </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>  



        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>


    <div class="formulario" id="divPrincipal" runat="server">   
     
         <table id="Table1" style="font-family: verdana; font-size: 8pt" runat="server">
            <tr  colspan = "3">               
                <td class="style1">
                    <div>
                        <table>
                               <tr>
                                   <td>Documento</td>
                                   <td>&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtDocumento" runat="server" MaxLength="50" Width="140px" Text ="" Enabled ="false" ></telerik:RadTextBox>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>Tipo Documento</td>
                                   <td> <telerik:RadTextBox ID="txtTipoDoc" runat="server" MaxLength="50" Width="140px" Text ="" Enabled ="false" ></telerik:RadTextBox></td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td colspan="7" class="auto-style6"><strong>Datos de Vehículo</strong></td>
                                   <td class="auto-style6">&nbsp;</td>
                                   <td class="auto-style6">&nbsp;</td>
                               </tr>
                                   <tr>
                                   <td class="auto-style2">Vehículo</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtVehiculo" runat="server" MaxLength="50" Width="300px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       <asp:ImageButton ID="BuscarVehiculo" runat="server" ImageUrl="~/Img/find16.png" OnClick="BuscarVehiculo_Click" ToolTip="Buscar" ValidationGroup="buscar" />
                                       </td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Configuración autotransporte</td>
                                   <td class="auto-style7">
                                       <telerik:RadTextBox ID="txtIdPermisoSCT" runat="server" MaxLength="250" Width="70px" Text ="" Enabled="False" ></telerik:RadTextBox>
                                       <telerik:RadTextBox ID="txtPermisoSCT" runat="server" MaxLength="250" Width="300px" Text ="" Enabled="False" Height="21px" ></telerik:RadTextBox>
                                       </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                                   <tr>
                                   <td class="auto-style2">Placa Vehículo</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtPlaca" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Permiso SCT</td>
                                   <td class="auto-style7">
                                       <telerik:RadTextBox ID="txtIdConfVehiculo" runat="server" MaxLength="50" Width="70px" Text ="" Enabled="False" ></telerik:RadTextBox>
                                       <telerik:RadTextBox ID="txtConfVehiculo" runat="server" MaxLength="50" Width="300px" Text ="" Enabled="False" ></telerik:RadTextBox>
                                       </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                                   <tr>
                                   <td class="auto-style2">Modelo Vehículo</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtModelo" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                   <td class="auto-style8">
                                       Núm. Permiso</td>
                                   <td class="auto-style7">
                                       <telerik:RadTextBox ID="txtPermiso" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="auto-style2">Tipo Remolque</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtTipoRemolque" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Aseguradora</td>
                                   <td class="auto-style7">
                                       <telerik:RadTextBox ID="txtAseguradora" runat="server" MaxLength="50" Width="300px" AutoPostBack="true" ></telerik:RadTextBox>
                                   </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="auto-style2">Placa Remolque</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtPlacaRemolque" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Póliza&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                   <td class="auto-style7">
                                        <telerik:RadTextBox ID="txtPoliza" runat="server" MaxLength="50" Width="140px" AutoPostBack="true" ></telerik:RadTextBox>
                                   </td>
                                   <td class="auto-style7">
                                        &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                            <tr>
                                   <td colspan="7" class="auto-style6"><strong>Datos de Chofer</strong></td>
                                   <td class="auto-style6">&nbsp;</td>
                                   <td class="auto-style6">&nbsp;</td>
                               </tr>
                             <tr>
                                   <td class="auto-style2">Nombre</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                    <telerik:RadTextBox ID="txtNombre" runat="server" MaxLength="50" Width="300px" CssClass = "textbox" ReadOnly="True" LabelWidth="56px" Resize="None"></telerik:RadTextBox>
                                       
                                   </td>
                                   <td>
                                       <asp:ImageButton ID="BuscarChofer" runat="server" ImageUrl="~/Img/find16.png" OnClick="BuscarChofer_Click" ToolTip="Buscar" ValidationGroup="buscar" />
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Distancia Total Km</td>
                                   <td class="auto-style7">
                                       <telerik:RadTextBox ID="txtKm" runat="server" MaxLength="5" Width="70px" Text ="" Rows="1" ></telerik:RadTextBox>
                                       <asp:CheckBox ID="chkDistancia" runat="server" OnCheckedChanged="chkDistancia_CheckedChanged" AutoPostBack="true" Text="Aplicar a todos " />

                                   </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                            <tr>
                                   <td class="auto-style2">RFC</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtRfc" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Código Postal</td>
                                   <td class="auto-style7">
                                        <telerik:RadTextBox ID="txtCodigoPostal" runat="server" MaxLength="50" Width="140px" AutoPostBack="true" Display="true" Enabled="false"></telerik:RadTextBox>

                                       <asp:CheckBox ID="chkCodigoPostal" runat="server"  AutoPostBack="true" Text="Aplicar a todos " OnCheckedChanged="chkCodigoPostal_CheckedChanged" />

                                   </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                             <tr>
                                   <td class="auto-style2">Núm. Licencia</td>
                                   <td class="auto-style2">&nbsp;</td>
                                   <td>
                                       <telerik:RadTextBox ID="txtLicencia" runat="server" MaxLength="50" Width="140px" Text ="" ></telerik:RadTextBox>
                                   </td>
                                   <td>
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                                   <td class="auto-style8">
                                       Colonia </td>
                                   <td class="auto-style7">
                                        <telerik:RadTextBox ID="txtColonia" runat="server" MaxLength="50" Width="350px" AutoPostBack="true" Display="true" Enabled="false"></telerik:RadTextBox>
                                   </td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                                   <td class="auto-style7">
                                       &nbsp;</td>
                               </tr>
                             </table>

                    </div>


                </td>                
            </tr>
             <tr>
             <td class="auto-style4">

                 <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Datos de Productos</strong></td>
            </tr>
             <tr>
             <td class="auto-style4">

                 NOTA: Los campos Peso Kg, Distancia Km, código postal entrega  y colonia entrega son obligatorios, el campo Clave Mat. Peligroso y Embalaje serán necesarios solo con  
                 <br />
                 la opción de peligroso seleccionada de lo contrario se enviaran vacíos, si la información no es correcta o está incompleta la carta porte no se generará.</td>
            </tr>
         </table>
        
          <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
<%--             <tr>
                         <td>
                </td>
            </tr>--%>
            <tr>
                <td>
                </td>
                <td>
<%--                  <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="270px" ResizeMode="AdjacentPane"
                            ResizeWithBrowserWindow="true" BorderSize="0" Width="98%">--%>
                     <%--<telerik:RadPane ID="RadPane3" runat="server" Height="450px" BorderStyle="Double">--%>
                 <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="1200px" Height="250px">
                  <telerik:RadGrid ID="rgProductos" runat="server"  onneeddatasource="rgProductos_NeedDataSource"  onitemdatabound="rgProductos_ItemDataBound"
                    style="margin-bottom: 0px" onitemcommand="rgProductos_ItemCommand" CellSpacing="0" OnEditCommand="rgProductos_EditCommand"
                    OnUpdateCommand="rgProductos_UpdateCommand" AutoGenerateColumns="False" OnLoad="rgProductos_Load" OnItemCreated="rgProductos_ItemCreated" 
                    OnInit="rgProductos_Init" AllowPaging ="true"
                    PageSize="10" OnPageIndexChanged="rgProductos_PageIndexChanged" Culture="es-ES">

                    <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Prd" 
                    EditMode="InPlace" DataMember="lstProductos" HorizontalAlign="NotSet" AutoGenerateColumns="False" 
                        NoMasterRecordsText="No se encontraron registros.">
                                            
                                                       
                                <CommandItemSettings  ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" ShowAddNewRecordButton = "false"/>
                                            
                                     <Columns>
                                     
                                      <telerik:GridBoundColumn DataField="Id_RemDet" UniqueName="Id_RemDet" Visible="False">
                                     </telerik:GridBoundColumn>


                       <%-- Id Producto --%>
                                    <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd" FooterStyle-Width = "80px">

                                        <FooterStyle Width="80px"></FooterStyle>

                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="100px" MaxLength="15"
                                                MinValue="1" Text='<%# Eval("Id_Prd") %>'  AutoPostBack="true" Enabled ="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                                    
                    <%-- Descripción --%>
                                    <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Descripcion" UniqueName="Descripcion">
                                        <HeaderStyle Width="450px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-bottom-style: none">
                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="450px" ReadOnly="true" Text='<%# Eval("Descripcion") %>' Enabled="false">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                            
                    <%-- Cantidad --%>
                                    <telerik:GridTemplateColumn HeaderText="Cant." DataField="Cantidad" UniqueName="Cantidad">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Width="40px" MaxLength="4"
                                                Text='<%# Eval("Cantidad") %>' AutoPostBack="false" Enabled ="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                
                    <%--Peso --%>
                                  <telerik:GridTemplateColumn HeaderText="Peso Kg" DataField="Kg" UniqueName="Kg">
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblKg" runat="server" Text='<%# Eval("Kg") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtKg" runat="server" Width="80px" MaxLength="9"
                                                Text='<%# Eval("Kg") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtKg" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                     <%-- Distancia Recorrida --%>
                                      <telerik:GridTemplateColumn HeaderText="Distancia Km" DataField="Distancia" UniqueName="Distancia">
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistancia" runat="server" Text='<%# Eval("Distancia") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtDistancia" runat="server" Width="80px" MaxLength="9"
                                                Text='<%# Eval("Distancia") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtDistancia" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                     <%-- Origen --%> 
                                     <telerik:GridTemplateColumn HeaderText="Orígen" DataField="Origen" UniqueName="Origen">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrigen" runat="server" Text='<%# Eval("Origen") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtOrigen" runat="server" Width="90px" ReadOnly="true" Enabled ="false"
                                                Text='<%#  Eval("Origen") %>'>
                                            </telerik:RadTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                    <%-- Destino --%>
                                    <telerik:GridTemplateColumn HeaderText="Destino" DataField="Destino" UniqueName="Destino">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestino" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Destino") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbltxtDestino" runat="server" ForeColor="#FF0000" Visible="false" />
                                            
                                            
                                            <telerik:RadComboBox ID="cmbDestino" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" 
                                                LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbDestino_ClientSelectedIndexChanged"
                                                OnClientLoad="cmbDestino_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="150px" Height ="140px" EnableLoadOnDemand="true" 
                                                OnClientBlur="Combo_ClientBlur"  >
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# Eval("Calle") %>' />   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                       
                    
                   <%--  Codigo Postal Entrega --%>
                                     <telerik:GridTemplateColumn HeaderText="Código Postal Entrega" DataField="codigopostal" UniqueName="codigopostal">
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigopostal" runat="server" Text='<%# Eval("Codigopostal") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCodigoPostal" runat="server" Width="90px" MaxLength="5"
                                                MinValue="5" Text='<%# Eval("CodigoPostal") %>'  AutoPostBack="true" Enabled ="true" OnTextChanged="txtCodigoPostal_TextChanged">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                 
                   <%-- Colonia Entrega --%>
                                          <telerik:GridTemplateColumn HeaderText="Colonia Entrega" DataField="Colonia" UniqueName="Colonia">
                                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColonia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Colonia") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbltxtColonia" runat="server" ForeColor="#FF0000" Visible="false" />                                   
                                            
                                            <telerik:RadComboBox ID="cmbColonia" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" AutoPostBack="true"
                                                LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbColonia_ClientSelectedIndexChanged" OnItemsRequest="cmbColonia_ItemsRequested"
                                                OnClientLoad="cmbColonia_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="200px"  EnableLoadOnDemand="true" 
                                                OnClientBlur="Combo_ClientBlur"  >
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Descripcion") %>' />   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>


                    <%-- Fecha dd/MM/yy HH:mm --%>
                                      <telerik:GridDateTimeColumn EditFormColumnIndex="1" UniqueName="Fecha" PickerType="DateTimePicker" HeaderText="Fecha Entrega" 
                                            DataField="Fecha" DataFormatString="{0:MM/dd/yyyy}">
                                      <HeaderStyle Width="100px" HorizontalAlign="Center" />   
                                      </telerik:GridDateTimeColumn>

                  <%-- EsPeligroso --%>  
                                       <telerik:GridCheckBoxColumn HeaderText="Es Peligroso" DataField="EsPeligroso" UniqueName="EsPeligroso" >
                                          <HeaderStyle Width="50px" HorizontalAlign="Center" /> 
                                        </telerik:GridCheckBoxColumn>
                                                            
             
                  <%-- IdClaveMatPeligroso --%>
                                    
                                       <telerik:GridTemplateColumn HeaderText="Clave Mat. Peligroso" DataField="ClaveMatPeligroso" UniqueName="ClaveMatPeligroso">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblClaveMatPeligroso" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaveMatPeligroso") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>

                                            <asp:Label ID="lbltxtClaveMatPeligroso" runat="server" ForeColor="#FF0000" Visible="false" />                      
                                            <telerik:RadComboBox ID="cmbClaveMatPeligroso" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" OnItemsRequest="cmbClaveMatPeligroso_ItemsRequested"
                                                LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbClaveMatPeligroso_ClientSelectedIndexChanged" AutoPostBack="true" 
                                                OnClientLoad="cmbClaveMatPeligroso_OnLoad" HighlightTemplatedItems="true" MaxHeight="100px" Width="100px"  EnableLoadOnDemand="true" 
                                                OnClientBlur="Combo_ClientBlur"  >
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Descripcion") %>' />   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                 <%-- ClaveEmbalaje --%>
                                         <telerik:GridTemplateColumn HeaderText="Embalaje" DataField="Embalaje" UniqueName="Embalaje">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmbalaje" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Embalaje") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbltxtEmbalaje" runat="server" ForeColor="#FF0000" Visible="false" />                                   
                                            
                                            <telerik:RadComboBox ID="cmbEmbalaje" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" 
                                                LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbEmbalaje_ClientSelectedIndexChanged"
                                                OnClientLoad="cmbEmbalaje_OnLoad" HighlightTemplatedItems="true" MaxHeight="100px" Width="100px"  EnableLoadOnDemand="true" 
                                                OnClientBlur="Combo_ClientBlur"  >
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Descripcion") %>' />   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                                                                       
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar" HeaderText="Editar">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </telerik:GridEditCommandColumn> 

                                    </Columns>
                                <EditFormSettings>
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                </EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>

                       <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                        
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                  </asp:Panel>
                     <%--</telerik:RadPane>--%>
             <%--   </telerik:RadSplitter>--%>
                 </td>
            </tr>
<%--             <tr>
                 <td>
                </td>
                <td>
                   
                </td>
            </tr>--%>
        </table>

        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
      </div>

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
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }
            function CloseWindow() {
                // debugger;
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

            function AbrirCFDITrasladoPDF(WebURL) {
                var oWnd = window.radopen(WebURL, 'AbrirVentana_ImpresionPDFCFDITraslado');
                oWnd.center();
            }

            function CloseAndRebind() {
                GetRadWindow().Close();
            }
            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
            function BuscarChofer() {
                var oWnd;
                oWnd = radopen("Ventana_Buscar.aspx?Chofer=true", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function BuscarVehiculo() {
                var oWnd;
                oWnd = radopen("Ventana_Buscar.aspx?Vehiculo=true", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function chofer(param) {
                debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
            function seguro(param) {
                debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
            function Cliente(param) {
                debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
            //Combo MatPeligroso
            var txtClaveMatPeligroso;
            var cmbClaveMatPeligroso;
            function txtClaveMatPeligroso_OnLoad(sender, args) {
                txtClaveMatPeligroso = sender;
            }
            function cmbClaveMatPeligroso_OnLoad(sender, args) {
                cmbClaveMatPeligroso = sender;
            }
            //cuando el campo de texto de edición del Grid  pierde el foco
            function txtClaveMatPeligroso_OnBlur(sender, args) {
                debugger; 
                OnBlur(sender, cmbClaveMatPeligroso);
            }
            //cuando el combo de edición del Grid  cambia de indice
            function cmbClaveMatPeligroso_ClientSelectedIndexChanged(sender, eventArgs) {
                debugger;
                cmbClaveMatPeligroso_ClientSelectedIndexChanged(eventArgs.get_item(), txtClaveMatPeligroso);
            }
            //Combo Destino
            var txtDestino;
            var cmbDestino;
            function txtDestino_OnLoad(sender, args) {
                txtDestino = sender;
            }
            function cmbDestino_OnLoad(sender, args) {
                cmbDestino = sender;
            }
            //cuando el campo de texto de edición del Grid  pierde el foco
            function txtDestino_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbDestino);
            }
            //cuando el combo de edición del Grid  cambia de indice
            function cmbDestino_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtDestino);
            }
            //Combo Colonia
            var txtColonia;
            var cmbColonia;
            function txtColonia_OnLoad(sender, args) {
                txtColonia = sender;
            }
            function cmbColonia_OnLoad(sender, args) {
                cmbColonia = sender;
            }
            //cuando el campo de texto de edición del Grid  pierde el foco
            function txtColonia_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbColonia);
            }
            //cuando el combo de edición del Grid  cambia de indice
            function cmbColonia_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtColonia);
            }
            //Combo Embalaje
            var txtEmbalaje;
            var cmbEmbalaje;
            function txtEmbalaje_OnLoad(sender, args) {
                txtEmbalaje = sender;
            }
            function cmbEmbalaje_OnLoad(sender, args) {
                cmbEmbalaje = sender;
            }
            //cuando el campo de texto de edición del Grid  pierde el foco
            function txtEmbalaje_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbEmbalaje);
            }
            //cuando el combo de edición del Grid  cambia de indice
            function cmbEmbalaje_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtEmbalaje);
            }
            function requestStart(sender, eventArgs) {
                alert('Request start initiated by: ' + eventArgs.get_eventTarget());
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Descargar") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RAM.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest(null);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>