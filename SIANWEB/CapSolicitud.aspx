<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master" AutoEventWireup="true" CodeBehind="CapSolicitud.aspx.cs" Inherits="SIANWEB.CapSolicitud" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadWindowManager ID="RAM" RenderMode="Lightweight" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>                                                                                                                                                                                                   
                                                                                                                                                                                                                            
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
      
      <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
      <ClientEvents OnRequestStart="onRequestStart" />
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="gvFiles">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="gvFiles" />
               </UpdatedControls>
           </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
       </AjaxSettings>
       </telerik:RadAjaxManager>

      <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">       
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
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
            function AlertaFocus(mensaje, control) {
                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
            }
       </script>
    </telerik:RadScriptBlock>

      <telerik:RadToolBar  ID="RadToolBar1" runat="server" onbuttonclick="RadToolBar1_ButtonClick" Width="100%" AutoPostBack="true" >
        <Items>
                       <%--<telerik:RadToolBarButton runat="server" Text="" CommandName="Nuevo" CheckOnClick="false"  AllowSelfUnCheck="true" ToolTip ="Nuevo" CssClass="new" ImageUrl="Imagenes/blank.png"/>
                       <telerik:RadToolBarButton runat="server" Text="" CommandName="Guardar" CheckOnClick="false" AllowSelfUnCheck="true" ToolTip ="Guardar" CssClass="save" ImageUrl="Imagenes/blank.png"/>--%>
        </Items>
     </telerik:RadToolBar>    
     
       <div class="formulario" id="div1" runat="server">
      
      <table id="Table1" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            </tr>
        </table>

      <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                    </td>
                    <td width="150px" style="font-weight: bold">
                    </td>
                </tr>
        </table> 

      <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0"  OnTabClick="RadTabStrip1_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;G&lt;/u&gt;enerales" AccessKey="G" PageViewID="RadPageViewDGenerales" Selected="True" Value="DatosGenerales">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" Text="&lt;u&gt;P&lt;/u&gt;roductos" AccessKey="P" PageViewID="RadPageViewProductos" Value="Productos">
                            </telerik:RadTab>
                            
                            <telerik:RadTab runat="server" AccessKey="A" Text="&lt;u&gt;A&lt;/u&gt;rchivos Adjuntos" PageViewID="RadPageViewArchivos" Visible="true" Value="Adjuntos">
                            </telerik:RadTab>

                        </Tabs>
                    </telerik:RadTabStrip>

      <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid" BorderWidth="1px" ScrollBars="Hidden">

                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                                      <div id="formularioDatosGenerales" runat="server">
                                        <table  style="border:1px solid black;">                                                                                                                                 
                                            <%--<thead >
                                            <tr>
                                            <th  style="font-family:  verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black;  border-collapse:collapse;" colspan="3">  Datos del Cliente</th>                                                         
                                            <th  style="font-family:  verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black;  border-collapse:collapse;" colspan="3">  Datos del Técnico</th>
                                            </tr>
                                            </thead> --%> 
                                            <tr>
                                                <td></td>
                                                <td>Sucursal</td>
                                                <td>
                                                    <telerik:RadComboBox ID="Cmbsucursal" runat="server" AutoPostBack="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="True" 
                                                        Filter="Contains" HighlightTemplatedItems="True" IsCaseSensitive="false" 
                                                        LoadingMessage="Cargando..." MarkFirstMatch="True" Width="250px" 
                                                        >
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td></td>
                                                 <td>
                                                     Fecha</td>
                                                <td>
                                                 <telerik:radtextbox ID="txtFecha" runat="server" ReadOnly="True" CssClass = "textbox" width="125px" enabled ="false">
                                                    </telerik:radtextbox>                       
                                                    <asp:Label ID="lblMotivosQueja" runat="server" Text=""></asp:Label>
                                                    </td>
                                                
                                            </tr>                                         
                                            <tr> 
                                                <td>
                                                </td>
                                                <td>        
                                                    <asp:Label ID="lblIdqueja" runat="server" Text="Id queja"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID ="txtIdQueja" runat="server" Width="125px" MaxLength="9" MinValue="1"
                                                        Enabled="true" AutoPostBack = "True">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>


                                                     <%--<asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                                            ToolTip="Buscar" onclick="btnBuscar_Click" Visible = "false" />--%>
                                                </td>                                                                                                          
                                                <td >
                                                    &nbsp;</td>
                                                                                          
                                                    <td>
                                                        <asp:Label ID="lblInvestigador" runat="server" Text="Investigador"></asp:Label>
                                                </td>                                                                                                         
                                                    <td width="50">
                                                        <asp:TextBox ID="txtInvestigador" runat="server" Width="280px"></asp:TextBox>
                                                </td>

                                                    
                                            </tr>
                                            <tr> 
                                                <td></td>
                                                <td class="style11"><asp:Label ID="lblnomcliente" runat="server" Text="Nombre Cliente"></asp:Label></td>                                                  
                                                <td colspan="2">
                                                    <telerik:radtextbox ID="txtcli_Nombre" runat="server" ReadOnly="True" CssClass = "textbox"
                                                        width="250px" >
                                                    </telerik:radtextbox>                       </td>                                                                                                                                                              
                                                
                                                <td colspan="1">
                                                    <asp:Label ID="lblcc0" runat="server" Text="CC"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="CmbCC" runat="server" AutoPostBack="True" 
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="True" 
                                                        Filter="Contains" HighlightTemplatedItems="True" IsCaseSensitive="false" 
                                                        LoadingMessage="Cargando..." MarkFirstMatch="True" Width="280px">
                                                    </telerik:RadComboBox>
                                                </td>
         
                                            </tr>
                                            <tr> 
                                                <td>
                                                </td>
                                                <td class="style11"> 
                                                        <asp:Label ID="lblcorreo" runat="server" Text="Correo Electronico"></asp:Label></td>
                                                <td>
                                                    <telerik:radtextbox ID="txtcorreo" runat="server" Width="250px" MaxLength="50" CssClass = "textbox"
                                                        ReadOnly="True"></telerik:radtextbox>                               
                                                </td>                                                                                                          
                                                <td Width="20px" >
                                                    &nbsp;</td>
                                                                                                    
                                                    <td>
                                                        <asp:Label ID="lblSolicitud" runat="server" Text="Label" Visible="False"></asp:Label>
                                                </td>                                                                                                         
                                                    <td width="50">
                   
                                                        <telerik:RadTextBox ID="txtId_Cliente" runat="server" AutoPostBack="True" 
                                                            CssClass="textbox" Height="16px" ReadOnly="False" Visible="False" width="100px">
                                                        </telerik:RadTextBox>
                                                        
                                                </td>
                                              
                                            </tr>                                              
                                            
                                            <%--<tr>
                                                    <th  style="font-family: verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black; border-collapse:collapse;" colspan="6"> Descripción del problema</th> 
                                            </tr>--%>
                                            <tr>
                                                <td></td>
                                                <td class="style11">Fecha Evento</td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtFechaEvento" runat="server" CssClass="textbox" 
                                                        MaxLength="50" ReadOnly="True" Width="250px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td></td>
                                                
                                                <td>Dondé Ocurrió</td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbOcurrio" runat="server" Width="250px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                 </tr>
                                            <tr>
                                                <td></td>
                                                <td class="style11">Motivos</td>
                                                <td>
                                                    <textarea id="txtMotivos" cols ="50" name="S2" rows="3" runat = "server" readonly = "readonly"></textarea></td>
                                                <td></td>
                                                
                                                <td>Descripción Detallada</td>
                                                <td>
                                                    <textarea  id ="txtDescripcion" cols="50" name="S1" rows="3" runat = "server" readonly = "readonly"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td class="style11">Otro Motivo</td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtOtros" runat="server" CssClass="textbox" 
                                                        ReadOnly="True" width="250px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <telerik:RadListBox ID="RlbMotivos" runat="server" CheckBoxes="true" 
                                                        SelectionMode="Multiple" ShowCheckAll="true" 
                                                        style="top: 0px; left: 0px; width: 10px; height:10px;  " visible="false">
                                                    </telerik:RadListBox>
                                                </td>
                                            </tr>  
                                            <%--<tr>
                                                <th  style="font-family: verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black; border-collapse:collapse;" colspan="3" > General</th>  
                                                <th  style="font-family: verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black; border-collapse:collapse;" colspan="3" > Categorización</th>                                                         
                                            </tr> --%>
              
                                            <tr> 
                                                <td>
                                                </td>
                                                <td class="style11"> 
                                                    <asp:Label ID="lblestado" runat="server" Text="Estado"></asp:Label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="CmbEstado" runat="server" Width = "250px" AutoPostBack = "true" DropDownWidth="110px"></telerik:RadComboBox> 
                                                </td>                                                                                                          
                                                <td Width="20px" >
                                                    &nbsp;</td>
                                                                                                     
                                                    <td>
                                                        <asp:Label ID="lbltqueja" runat="server" Text="Tipo de queja"></asp:Label></td>                                                                                                         
                                                    <td width="50">
                                                    <telerik:RadComboBox ID="CmbTipoQueja" runat="server" Width = "250px" 
                                                            DropDownWidth="175px"></telerik:RadComboBox> 
                                                </td>

                                            </tr>
                                            <tr> 
                                                <td class="style2">
                                                </td>
                                                <td class="style9"> 
                                                    <asp:Label ID="lbltservicio" runat="server" Text="Tipo de servicio"></asp:Label>
                                                </td>
                                                <td class="style2">
                                                    <telerik:RadComboBox ID="Cmbtservicio" runat="server" DropDownWidth="250px" 
                                                        Width="250px">
                                                    </telerik:RadComboBox>
                                                </td>                                                                                                          
                                                <td Width="20px" class="style2" >
                                                    </td>
                                                <td>
                                                        Prioridad</td>                                                     
                                                    <td  width="50" class="style2">
                                                        <telerik:RadComboBox ID="CmbPrioridad" runat="server" Width="250px">
                                                        </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td class="style7">
                                                </td>
                                                <td class="style10"> 
                                                    <asp:Label ID="lblacierre" runat="server" Text="Acción de Cierre"></asp:Label></td>
                                                <td class="style7">
                                                    <telerik:RadComboBox ID="CmbAccion" runat="server" DropDownWidth="250px" 
                                                        Width="250px">
                                                    </telerik:RadComboBox>
                                                    <br />
                                                </td>                                                                                                          
                                                <td Width="20px" class="style7" >
                                                    </td>
                                                     
                                                <td class="style7"> 
                                                    <asp:Label ID="lblFechaTerminacion" runat="server" Text="Fecha Terminación" Visible = "false"></asp:Label></td>
                                                <td>
                                                    
                                                    <telerik:RadDatePicker ID="dpFechaTerminacion" Runat="server" Visible = "false"></telerik:RadDatePicker>
                                                    
                                                    <asp:Label ID="lblEsCambioInvestigador" runat="server"></asp:Label>
                                                    
                                                </td>                                                                                                          

                                            </tr>
                                           
                                        <%--<tr>
                                            <th  style="font-family: verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black; border-collapse:collapse;" colspan="3"> Solicitud de información</th> 
                                            <th  style="font-family: verdana; font-size: 8pt; background-color:#87CEFA; border:1px solid black; border-collapse:collapse;" colspan="3"> Motivo de rechazo</th>
                                        </tr>--%>
                                            <tr>
                                            <td>
                                            </td>
                                            <td colspan = "2" rowspan = "2">
                                              <asp:Panel ID="pnlComentarios" runat="server"  BorderStyle="Solid" BorderWidth="1px" GroupingText ="Comentarios"> 
                                                <telerik:RadEditor RenderMode="Lightweight" runat="server" SkinID="BasicSetOfTools" ID="RadEditorComentarios" EmptyMessage="Esta area esta destinada para solicitar información faltante a usuario de la queja."
                                                    EditModes="Design" Width="470px" Height="120px" Skin="Default">
                                                        <Modules>
                                                                <telerik:EditorModule Name="RadEditorHtmlInspector" Enabled="true" Visible="false" />
                                                                <telerik:EditorModule Name="RadEditorNodeInspector" Enabled="true" Visible="false" />
                                                                <telerik:EditorModule Name="RadEditorDomInspector" Enabled="false" />
                                                                <telerik:EditorModule Name="RadEditorStatistics" Enabled="false" />
                                                            </Modules>
                                                </telerik:RadEditor>
                                             </asp:Panel>
                                            </td>
                                            <td colspan = "3" rowspan ="2">
                                            <asp:Panel ID="pnlRechazo" runat="server" BorderStyle="Solid" BorderWidth="1px" GroupingText ="Motivos Rechazo"> 
                                                <telerik:RadEditor ID="RadEditorRechazo" runat="server" EditModes="Design" 
                                                    EmptyMessage="Esta area esta destinada para explicar por que se rechaza la queja." 
                                                    Height="120px" RenderMode="Lightweight" Skin="Default" SkinID="BasicSetOfTools" 
                                                    Width="530px">
                                                    <Modules>
                                                        <telerik:EditorModule Enabled="true" Name="RadEditorHtmlInspector" 
                                                            Visible="false" />
                                                        <telerik:EditorModule Enabled="true" Name="RadEditorNodeInspector" 
                                                            Visible="false" />
                                                        <telerik:EditorModule Enabled="false" Name="RadEditorDomInspector" />
                                                        <telerik:EditorModule Enabled="false" Name="RadEditorStatistics" />
                                                    </Modules>
                                                </telerik:RadEditor>
                                                </asp:Panel>
                                                </td>
                                            </tr> 
           
                                        </table>
                                        
                                    </div>
                        </telerik:RadPageView>

                       <telerik:RadPageView ID="RadPageViewProductos" runat="server">
                       <div id="divProductos" runat="server"  style = "overflow:auto">
                                         <telerik:RadGrid ID="rgProductos" runat="server" GridLines="None" onneeddatasource="rgProductos_NeedDataSource" DataMember="lstProductos" oninsertcommand="rgProductos_InsertCommand" onitemdatabound="rgProductos_ItemDataBound" 
                                    onupdatecommand="rgProductos_UpdateCommand" style="margin-bottom: 0px" ondeletecommand="rgProductos_DeleteCommand1" onitemcommand="rgProductos_ItemCommand" onitemcreated="rgProductos_ItemCreated" CellSpacing="0">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Prd,Descripcion,Presentacion,Cantidad, Prd_UniEmp, Lote, Fabricacion, Caducidad, Marca, Costo, Mantener" 
                                        EditMode="InPlace" DataMember="lstProductos" HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros." PageSize="5">

                                <PagerStyle FirstPageToolTip="Pagina iniziale" LastPageToolTip="Ultima pagina"
                                    NextPagesToolTip="Pagine successive" NextPageToolTip="Pagina successiva"
                                    PagerTextFormat="Cambia pagina: {4} &nbsp;Pagina <strong>{0}</strong> di <strong>{1}</strong> - Righe da <strong>{2}</strong> a <strong>{3}</strong> - Numero righe totali <strong>{5}</strong>."
                                    PageSizeLabelText="Numero righe:" PrevPagesToolTip="Pagine precedenti"
                                    PrevPageToolTip="Pagina precedente" />
                
                                <CommandItemSettings  ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" ShowAddNewRecordButton = "false"/>
                
                                <Columns>
                    <%-- Id Producto --%>
                                    <telerik:GridTemplateColumn HeaderText="Id Producto" DataField="Id_Prd" UniqueName="Id_PrdN" FooterStyle-Width = "80px">

                                        <FooterStyle Width="80px"></FooterStyle>

                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1" Text='<%# Eval("Id_Prd") %>'  AutoPostBack="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                                    <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd"  Visible="false">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table border="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Descripción --%>
                                    <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Descripcion" UniqueName="Descripcion">
                                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# Eval("Descripcion") %>'/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-bottom-style: none">
                                                        <telerik:RadTextBox ID="txtPrd_Descripcion" runat="server" Width="190px" ReadOnly="true" Text='<%# Eval("Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Presentación --%>
                                    <telerik:GridTemplateColumn HeaderText="Presen." DataField="Presentacion" UniqueName="Presentacion">
                                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# Eval("Presentacion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtPrd_Presentacion" runat="server" Width="50px" ReadOnly="true" Text='<%# Eval("Presentacion") %>'>
                                                                    </telerik:RadTextBox>
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
                                                Text='<%# Eval("Cantidad") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Unidades Empaque --%>
                                     <telerik:GridTemplateColumn HeaderText="Unidades Empaque" DataField="Prd_UniEmp" UniqueName="Prd_UniEmp">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrd_UniEmp" runat="server" Text='<%# Eval("Prd_UniEmp") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtPrd_UniEmp" runat="server" Width="40px" MaxLength="4"
                                                Text='<%# Eval("Prd_UniEmp") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtPrd_UniEmp" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Lote --%>
                                    <telerik:GridTemplateColumn HeaderText="Lote" DataField="Lote" UniqueName="Lote">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLote" runat="server" Text='<%# Eval("Lote") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtLote" runat="server" Width="50px" MaxLength="9"
                                                Text='<%# Eval("Lote") %>'  AutoPostBack="false">
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtLote" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                   
                    <%-- Fecha Fabricación --%>
                                    <telerik:GridDateTimeColumn DataField="Fabricacion" HeaderText="Fabricación" FilterControlWidth="40px"
                                         PickerType="DatePicker" DataFormatString="{0:MM/dd/yyyy}" UniqueName = "Fabricacion" >
                                         <HeaderStyle Width="160px"></HeaderStyle>
                                       
                                    </telerik:GridDateTimeColumn>

                    <%-- Fecha Caducidad --%>
                                    <telerik:GridDateTimeColumn DataField="Caducidad" HeaderText="Caducidad" FilterControlWidth="40px"
                                         PickerType="DatePicker" DataFormatString="{0:MM/dd/yyyy}" UniqueName = "Caducidad" >
                                         <HeaderStyle Width="160px"></HeaderStyle>
                                       
                                    </telerik:GridDateTimeColumn>
                                   
                    <%-- Marca --%>
                                    <telerik:GridTemplateColumn HeaderText="Marca" DataField="Marca" UniqueName="Marca">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("Marca") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtMarca" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Marca") %>' AutoPostBack="false">
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtMarca" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>   
                        
                    <%-- Costo --%>
                                    <telerik:GridTemplateColumn HeaderText="Costo AAA Unitario" DataField="Costo" UniqueName="Costo">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCosto" runat="server" Text='<%# Eval("Costo") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCosto" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Costo") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCosto" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Factura o remision --%>
                                    <telerik:GridTemplateColumn HeaderText="Num. Fact o Rem." DataField="Facorem" UniqueName="Facorem">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacorem" runat="server" Text='<%# Eval("Num_Fac") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtFacorem" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Num_Fac") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtFacorem" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Proveedor --%>
                                    <telerik:GridTemplateColumn HeaderText="Proveedor" DataField="Proveedor" UniqueName="Proveedor">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblProveedor" runat="server" Text='<%# Eval("Nom_Prov") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Nom_Prov") %>' AutoPostBack="false">
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtProveedor" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%--Mantener--%>
                                     <telerik:GridTemplateColumn HeaderText="Mantener Registro" DataField="Mantener" UniqueName="Mantener">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMantener" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mantener") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbltxtMantener" runat="server" ForeColor="#FF0000" Visible="false" />
                                            <telerik:RadComboBox ID="cmbMantener" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                MarkFirstMatch="true" LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbMantener_ClientSelectedIndexChanged"
                                                OnClientLoad="cmbMantener_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="100%" EnableLoadOnDemand="true" OnClientBlur="Combo_ClientBlur"  >
                                                <ExpandAnimation Type="none" />
                                                <CollapseAnimation Type="none" />
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%--Transferencia--%>
                                     <telerik:GridTemplateColumn HeaderText="Transferencia" DataField="Transferencia" UniqueName="Transferencia">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransferencia" runat="server" Text='<%# Eval("Transferencia") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtTransferencia" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Transferencia") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtTranferencia" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                    <%--Id Entrada--%>
                                     <telerik:GridTemplateColumn HeaderText="Id documento" DataField="Rem_Trans" UniqueName="Rem_Trans">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRem_Transa" runat="server" Text='<%# Eval("Rem_Trans") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtRem_Trans" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Rem_Trans") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtRem_Trans" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                    <%--C. Estandar--%>
                                     <telerik:GridTemplateColumn HeaderText="Costo Estandar" DataField="Costoestandar" UniqueName="Costoestandar">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostoestandar" runat="server" Text='<%# Eval("Costoestandar") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtCostoestandar" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("Costoestandar") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCostoestandar" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                    <%--Nota Credito--%>
                                     <telerik:GridTemplateColumn HeaderText="Nota Crédito" DataField="NCredito" UniqueName="NCredito">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNCredito" runat="server" Text='<%# Eval("NCredito") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtNCredito" runat="server" Width="70px" MaxLength="9"
                                                Text='<%# Eval("NCredito") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtNCredito" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%--Boton Editar--%>                
                                    <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar" HeaderText="Editar">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </telerik:GridEditCommandColumn>  --%>
                             </Columns>     

                              <EditFormSettings>
                                 <EditColumn UniqueName="EditCommandColumn">
                                 </EditColumn>
                             </EditFormSettings>                       
                          </MasterTableView>

                          <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                                        SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                      </telerik:RadGrid> 
                      
                       <table width="99%">
                            <tr>
                                <td>
                                </td>
                                <td width="70%">
                                </td>
                                <td class="style12">
                                    <asp:Label ID="lblTotalAAA" runat="server" Text="Total AAA" Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotalAAA" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td class="style12">
                                    <asp:Label ID="lblTotalEstandar" runat="server" Text="Total P. Estandar" Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotalEstandar" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                       
                      </div>
                        </telerik:RadPageView>

                         <telerik:RadPageView ID="RadPageViewArchivos" runat="server">
                                    <div id="TablasArchivos" runat="server">
                                           <table>
                                               <tr>
                                                   <td></td>
                                                   <td> 
                                                        <asp:Panel ID="PnlFiles" runat="server" Width="1000px" BorderStyle="Solid" BorderWidth="1px" GroupingText ="Arvhivos Cargados">
                                                        <telerik:RadGrid ID="gvFiles" runat="server" GridLines="None" style="margin-bottom: 0px" AllowPaging = "True"  CellSpacing="0"  Width="900px" 
                                                        MasterTableView-NoMasterRecordsText="No se encontraron registros." onitemcommand="gvFiles_ItemCommand" onneeddatasource="gvFiles_NeedDataSource" onpageindexchanged="gvFiles_PageIndexChanged" >
                                                              <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace" AllowMultiColumnSorting="False" AutoGenerateColumns = "False" HorizontalAlign ="NotSet" DataKeyNames = "Id_Emp, Id_Cd, Id_Doc" >              
                                                         <Columns>

                                               <%-- Columnas --%>
                                                    <telerik:GridBoundColumn DataField="Id_Emp" Display="false" HeaderStyle-Width="40px" HeaderText="Id_Emp" UniqueName="Id_Emp">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                          <telerik:GridBoundColumn DataField="Id_Cd" Display="false" HeaderStyle-Width="40px" HeaderText="Id_Cd" UniqueName="Id_Cd">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Id_Doc" Display="true" HeaderStyle-Width="40px" HeaderText="Folio" UniqueName="Id_Doc">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Doc_Nombre" Display="true" HeaderStyle-Width="200px" HeaderText="Nombre Documento" UniqueName="Doc_Nombre">
                                                                             <HeaderStyle Width="200px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Formato" Display="true" HeaderStyle-Width="70px" HeaderText="Formato" UniqueName="Formato">
                                                                             <HeaderStyle Width="70px" />
                                                                         </telerik:GridBoundColumn>
                                                                         
                                                                         <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="Tipo Archivo" Display="true" UniqueName="TipoDoc" HeaderStyle-Width = "70px">
                                                                          <HeaderStyle Width="70px"></HeaderStyle>
                                                                         </telerik:GridBoundColumn>
                                                                         
                                                                         <telerik:GridBoundColumn DataField="Tamaño" Display="true" HeaderStyle-Width="70px" HeaderText="Tamaño" UniqueName="Tamaño">
                                                                             <HeaderStyle Width="70px" />
                                                                         </telerik:GridBoundColumn>

                                                    <telerik:GridTemplateColumn HeaderText = "Movimientos">
                                                       <ItemTemplate>
                                                          <%-- <asp:ImageButton ID="Descargar"  ImageUrl ="~/Imagenes/descarga-boton-300x80.png" Width ="100px" ToolTip = "Descargar" runat="server" CommandName="DescargarArchivo"  CommandArgument='<%# Eval("Id_Doc", "GetFile.aspx?Id_Doc={0}") %>'></asp:ImageButton>--%>
                                                           <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id_Doc", "CapDescargarArchivo.aspx?Id_Doc={0}") %>' Text="Descargar"></asp:HyperLink>
                                                       </ItemTemplate>
                                                    </telerik:GridTemplateColumn>  
                             

                                             </Columns>     
                     
                                                   </MasterTableView>
                                                              <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente" SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
                                                              <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                                ShowPagerText="True" PageButtonCount="3" />
                                                     </telerik:RadGrid> 
                                                        </asp:Panel>
                                                   </td>
                                               </tr>
                                                 <tr>
                                                     <td>
                                                     </td>
                                                     <td>
                                                         <asp:Panel ID="PnlSolucion" runat="server" BorderStyle="Solid" BorderWidth="1px" GroupingText="Archivo de análisis y de soporte" Width="1000px">
                                                             <telerik:RadGrid ID="gvSolucion" runat="server" AllowPaging="True" CellSpacing="0"  GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                                 onitemcommand="gvSolucion_ItemCommand" onneeddatasource="gvSolucion_NeedDataSource" onpageindexchanged="gvSolucion_PageIndexChanged" style="margin-bottom: 0px" Width="900px" >
                                                                 <MasterTableView AllowFilteringByColumn="False" AllowMultiColumnSorting="False" AutoGenerateColumns="False" DataKeyNames="Id_Emp, Id_Cd, Id_Doc" EditMode="InPlace" HorizontalAlign="NotSet">
                                                                     <Columns>
                                                                         <%-- Columnas --%>
                                                                          <telerik:GridBoundColumn DataField="Id_Emp" Display="false" HeaderStyle-Width="40px" HeaderText="Id_Emp" UniqueName="Id_Emp">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                          <telerik:GridBoundColumn DataField="Id_Cd" Display="true" HeaderStyle-Width="40px" HeaderText="Id_Cd" UniqueName="Id_Cd">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Id_Doc" Display="true" HeaderStyle-Width="40px" HeaderText="Folio" UniqueName="Id_Doc">
                                                                             <HeaderStyle Width="40px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Doc_Nombre" Display="true" HeaderStyle-Width="200px" HeaderText="Nombre Documento" UniqueName="Doc_Nombre">
                                                                             <HeaderStyle Width="200px" />
                                                                         </telerik:GridBoundColumn>

                                                                         <telerik:GridBoundColumn DataField="Formato" Display="true" HeaderStyle-Width="70px" HeaderText="Formato" UniqueName="Formato">
                                                                             <HeaderStyle Width="70px" />
                                                                         </telerik:GridBoundColumn>
                                                                         
                                                                         <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="Tipo Archivo" Display="true" UniqueName="TipoDoc" HeaderStyle-Width = "70px">
                                                                          <HeaderStyle Width="70px"></HeaderStyle>
                                                                         </telerik:GridBoundColumn>
                                                                         
                                                                         <telerik:GridBoundColumn DataField="Tamaño" Display="true" HeaderStyle-Width="70px" HeaderText="Tamaño" UniqueName="Tamaño">
                                                                             <HeaderStyle Width="70px" />
                                                                         </telerik:GridBoundColumn>
                                                                         
                                                                         <telerik:GridTemplateColumn HeaderText="Movimientos">
                                                                             <ItemTemplate>
                                                                         <%--        <asp:ImageButton ID="Descargar" runat="server" CommandArgument='<%# Eval("Id_Doc", "GetFile.aspx?Id_Doc={0}") %>' CommandName="DescargarArchivo" 
                                                                                     ImageUrl="~/Imagenes/descarga-boton-300x80.png" ToolTip="Descargar" 
                                                                                     Width="100px" />--%>
                                                                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id_Doc", "CapDescargarArchivo.aspx?Id_Doc={0}") %>' Text="Descargar"></asp:HyperLink>
                                                                             </ItemTemplate>
                                                                         </telerik:GridTemplateColumn>
                                                                     </Columns>
                                                                 </MasterTableView>
                                                                 <SortingSettings EnableSkinSortStyles="False" SortedAscToolTip="Ascendente" 
                                                                     SortedDescToolTip="Descendente" SortToolTip="Ordenar ascendente/descendente" />
                                                                 <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" 
                                                                     NextPagesToolTip="Páginas siguientes" NextPageToolTip="Página siguiente" 
                                                                     PageButtonCount="3" 
                                                                     PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;." 
                                                                     PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores" 
                                                                     PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                             </telerik:RadGrid>
                                                         </asp:Panel>
                                                     </td>
                                                     
                                                  </tr>
                                                  <tr>
                                                         <td>
                                                         </td>
                                                         <td>
                                                             <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                                             <asp:HiddenField ID="HiddenHeight" runat="server" />
                                                         </td>
                                                     </tr>
                                           </table>
                                    </div>
                        </telerik:RadPageView>
                  </telerik:RadMultiPage>
      
      </div>   

</asp:Content>