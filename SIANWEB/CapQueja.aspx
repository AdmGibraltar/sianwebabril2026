<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master" AutoEventWireup="true" CodeBehind="CapQueja.aspx.cs" Inherits="SIANWEB.CapQueja" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">       
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                debugger;
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            function pageLoad() {
                rgProductos = $find("<%= rgProductos.ClientID %>");
            }
            function requestStart(sender, eventArgs) {
                alert('Request start initiated by: ' + eventArgs.get_eventTarget());
            }
            function onRequestStart(sender, args) {
                debugger;
                if (args.get_eventTarget().indexOf("Descargar") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function FrmPreguntas() {
                var oWnd = radopen("FrmPreguntas.aspx"
                    , "Preguntas");
                oWnd.center();
            }
            function Respuesta(Respuesta) {
                debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(Respuesta);
            }
            function OnClientValidationFailed(sender, args) {
                var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                    if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                        alert("Este tipo de archivo no es válido, se agregara al listado, pero no se almacenara con la queja.");
                    }
                    else {
                        alert("Este archivo excede el tamaño maximo permitido por archivo!");
                    }
                }
                else {
                    alert("Este tipo de archivo no es válido, se agregara al listado, pero no se almacenara con la queja.");
                }
            }
            var rgProductos = null;
            function IdPrd_OnBlur(sender, eventArgs) {
                //debugger;
                OnBlur(sender, $find(cmbProductoClientID));
            }
            function txtPrd_Descripcion(sender, args) {//cmbProducto_OnLoad
                cmbProducto = sender;
            }
            function txtProductoPartida_OnLoad(sender, args) {
                _prd = sender;
                ultimo_producto = sender.get_value();
            }
            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var txtId_Prd;
            var cmbProducto;
            var lbl_cmbProductoClientId = '';
            function confirmCallBackFn(arg) {
                debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
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
            var txtConservar;
            var cmbConservar;
            function txtConservar_OnLoad(sender, args) {
                txtConservar = sender;
            }
            function cmbConservar_OnLoad(sender, args) {
                cmbConservar = sender;
            }
            //cuando el campo de texto de edición del Grid de TerritorioPartida pirde el foco
            function txtConservar_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbConservar);
            }
            //cuando el combo de edición del Grid de TerritorioPartida cambia de indice
            function cmbConservar_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtConservar);
            }
            //--------------------------------------------------------------------------------------------------
            //            Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }
            function CloseWindow(mensaje) {
                debugger;
                var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        debugger;
                        CloseAndRebind();
                        RefreshParentPage();
                    });
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                debugger;
                GetRadWindow().Close();
            }
            //Hace un refresh completo de la ventana padre = F5
            function CloseAndRebind() {
                GetRadWindow().Close();
            }
            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                debugger;
                GetRadWindow().BrowserWindow.location.reload();
            }
        </script>
    </telerik:RadScriptBlock>

    <telerik:RadWindowManager ID="RAM" RenderMode="Lightweight" runat="server" EnableShadow="true">  
            <Windows>
             <telerik:RadWindow ID="FrmPregunta" runat="server" Behaviors="Move"
                Opacity="100" VisibleStatusbar="False" Width="350px" Height="180px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Ubicación de Material"
                Modal="True" Localization-Restore="Restaurar" Localization-Maximize="Maximizar">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>  
                                                                                                                                                                                                                            
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
 
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False"> 
     <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
             <%--  <telerik:AjaxSetting AjaxControlID="gvFiles">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="gvFiles" />
                 </UpdatedControls>
               </telerik:AjaxSetting>--%>
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
             <telerik:AjaxSetting AjaxControlID="cmbtquejas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
           
            </telerik:AjaxSetting>
            </AjaxSettings> 
    </telerik:RadAjaxManager>

    <telerik:RadToolBar  ID="RadToolBar1" runat="server" dir="rtl" OnButtonClick = "RadToolBar1_ButtonClick" Width="608%" >
        <Items>    
            <telerik:RadToolBarButton CommandName="save"  Value="save" ToolTip="Guardar" CssClass="save" ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            <%--<telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new" ImageUrl="Imagenes/blank.png" />--%>
        </Items>
     </telerik:RadToolBar>       
      
    <table id="Table1" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                </td>
            </tr>
    </table> 
    
    <div class="formulario" id="divPrincipal" runat="server">
                    
                <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
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

                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" OnTabClick="RadTabStrip1_TabClick">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Datos &lt;u&gt;G&lt;/u&gt;enerales" AccessKey="G" PageViewID="RadPageViewDGenerales" Value="DatosGenerales" Selected="True" Visible="true">
                        </telerik:RadTab>

                        <telerik:RadTab runat="server" Text="&lt;u&gt;P&lt;/u&gt;roductos" AccessKey="P" PageViewID="RadPageViewProductos" Value="Productos">
                        </telerik:RadTab>
                            
                        <telerik:RadTab runat="server" AccessKey="A" Text="&lt;u&gt;A&lt;/u&gt;rchivos Adjuntos" PageViewID="RadPageViewArchivos"  Value="Adjuntos" >
                        </telerik:RadTab>

                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" 
                    BorderStyle="Solid" BorderWidth="1px" ScrollBars="Hidden" 
                    onpageviewcreated="RadMultiPage1_PageViewCreated">

                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                                    <div id="formularioDatosGenerales" runat="server" style = " border:1">
                                        
                   <table  style="border:1px solid black;" >

                
                    <tr>
                        <td></td>
                        <td>Nombre Cliente</td>
                        <td>
                            <asp:TextBox ID="txtCliente" runat="server" Width="270px"></asp:TextBox>

                    

                        </td>
                        <td class="style7">&nbsp;</td>
                         <td><asp:Label ID="lbltqueja" runat="server" Text="Tipo Queja"></asp:Label>&nbsp;&nbsp; </td>
                        <td>
                            <telerik:RadComboBox ID="cmbtquejas" runat="server" 
                                ChangeTextOnKeyBoardNavigation="true" AutoPostBack="True"
                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" 
                                HighlightTemplatedItems="true" MarkFirstMatch="true"                                                                  
                                    Width="250px" onselectedindexchanged="cmbtquejas_SelectedIndexChanged">
                            </telerik:RadComboBox>

                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>Número Sucursal</td>
                        <td colspan ="1">  
                            <asp:TextBox ID="txtIdCd" runat="server" Width="60px"></asp:TextBox>
                            <telerik:RadComboBox ID="cmbNomSucursal" runat="server" 
                                ChangeTextOnKeyBoardNavigation="true" AutoPostBack="True"
                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" 
                                HighlightTemplatedItems="true" MarkFirstMatch="true"                                                                  
                                    Width="200px" onselectedindexchanged="cmbNomSucursal_SelectedIndexChanged">
                            </telerik:RadComboBox>
                         </td>
                         <td class="style7"></td>
                         <td>
                            <asp:Label ID="lblRemision" runat="server" Text="Remisión" visible ="false" Enabled="false"></asp:Label></td>
                         <td> 
                            <asp:TextBox ID="txtRemision" runat="server" Width="200px" Visible ="false" Enabled ="false"></asp:TextBox>
                    </tr>

                    <tr>
                    <td></td>
                    <td>Clave Cliente Directo</td>
                    <td colspan ="1">  
                            <asp:TextBox ID="txtIdcte_directo" runat="server" Width="125px" Enabled = "true" maxlength ="9" onkeypress="return event.charCode >= 48 && event.charCode <= 57"></asp:TextBox>                         
                         </td>
                    <td class="style7"></td>
                    <td>
                    <asp:Label ID="lblprioridad1" runat="server" Text="Prioridad"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbPrioridad" runat="server" ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" 
                        DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains" Height="20px" 
                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                        MarkFirstMatch="true" AutoPostBack = "true"
                        onselectedindexchanged="cmbPrioridad_SelectedIndexChanged" Width="250px">
                    </telerik:RadComboBox>
                </td>
            </tr>

                    <tr>
                <td></td>
                    <td>Nombre Cliente Directo</td>
                    <td colspan ="1">  
                            <asp:TextBox ID="txtNomcte_directo" runat="server" Width="250px"  Enabled = "true"></asp:TextBox>                            
                         </td>
                    <td></td>
                        <td>
                            <asp:Label ID="lblIdQueja" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEsConsulta" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>Fecha del Evento</td>
                        <td>

                            <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFecha" CssClass="toDate" Width="50%" runat="server" >
                            </telerik:RadDatePicker>
                            
                         </td>
                        <td>&nbsp;</td>
          
                         <td><asp:Label ID="lblRes" runat="server" Visible="False" Value ="Si" Text ="Si"></asp:Label> </td>
                         <td rowspan = "6"> 
                          <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height ="150px" Width="250px" BorderStyle = "None">
                                <telerik:RadListBox ID="RadListBox1" runat="server" CheckBoxes="true"  AutoPostBack = "true"
                                    ShowCheckAll="true" style="top: 0px; left: 0px; width: auto; height:auto;  " 
                                    SelectionMode="Multiple" OnSelectedIndexChanged="RadListBox1_SelectedIndexChanged" OnItemCheck="RadListBox1_ItemCheck1">
                                   
                                </telerik:RadListBox>
                            </asp:Panel>
                         
                         
                         
                         </td>
                    </tr>

                    <tr>
                    <td></td>
                    <td>Dondé Ocurrió</td>
                        <td class="style3">                            
                        <telerik:RadComboBox ID="cmbOcurrio" runat="server" ChangeTextOnKeyBoardNavigation="true" AutoPostBack="false"
                                DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true" MarkFirstMatch="true"                                                                  
                                Width="270px">
                        </telerik:RadComboBox></td>
                        <td colspan ="1">  
                            &nbsp;</td>
                         <td>&nbsp;</td>                        
                    </tr>  
                     
                    <tr>
                    <td></td>
                        <td>Otro Motivo</td>
                        <td><asp:TextBox ID="txtOtroMotivo" runat="server" Enabled="true" Width="270px"  MaxLength="50"></asp:TextBox></td>
                        <td></td>
                        <td>Motivos</td>
                        
                    </tr>  

                    <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblembarque" runat="server" Text="Declaración de Embarque"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtembarque" runat="server" CssClass="textbox"  MaxLength="20"></asp:TextBox>
                        </td>
            <td></td>
            <td></td>
            
            </tr>
           
                    <tr>
                        <td></td>
                        <td>Fecha Recepción del Embarque</td>
                        <td>
                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rdpFechaEmbarque" 
                                CssClass="toDate" Width="50%" runat="server" AutoPostBack="True" 
                                onselecteddatechanged="rdpFechaEmbarque_SelectedDateChanged1" >
                                <Calendar RenderMode="Lightweight" UseColumnHeadersAsSelectors="False" 
                                    UseRowHeadersAsSelectors="False">
                                </Calendar>
                                <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" 
                                    DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%">
                                   <%-- <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />--%>
                                </DateInput>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>                           
                         </td>
                        <td>&nbsp;</td>
          
                         <td> </td>
                         <td > 
                          
                         
                         
                         
                         </td>
                    </tr>

            <tr>
            <td></td>
            <td>
                <asp:Label ID="lblflete" runat="server" Text="Guía de Flete"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtflete" runat="server" CssClass="textbox"  MaxLength="20"></asp:TextBox>
                </td>
            <td></td>
            <td></td>
           
            </tr>

                       
                    <tr>
                <td></td>
                <td colspan = "2" rowspan = "5" >
                    <telerik:RadEditor RenderMode="Lightweight" runat="server" SkinID="" ID="RadEditorResume" EmptyMessage="Descripción detallada de la queja!" EditModes="Design" Width="470px" Height="130px" EnableResize="False">
                          <Modules>
                            <telerik:EditorModule Name="RadEditorHtmlInspector" Enabled="true" Visible="false" />
                            <telerik:EditorModule Name="RadEditorNodeInspector" Enabled="true" Visible="false" />
                            <telerik:EditorModule Name="RadEditorDomInspector" Enabled="false" />
                            <telerik:EditorModule Name="RadEditorStatistics" Enabled="false" />
                          </Modules>
                    </telerik:RadEditor>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblCompañia" runat="server" Text="Línea&nbsp; Transporte"></asp:Label>
                <td>
<%--                    <asp:TextBox ID="txtCompañia" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>--%>
                   <telerik:RadComboBox ID="CmbTransporte" runat="server" ChangeTextOnKeyBoardNavigation="true" AutoPostBack="False"
                   DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true" MarkFirstMatch="true"                                                                  
                   Width="270px">
                   </telerik:RadComboBox>
            </tr> 
                    
                    <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="lblPlacas" runat="server" Text="Placas"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtPlacas" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
                        </td>
             </tr>
                   
                    <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblNomChofer" runat="server" Text="Nombre Chofer"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNomChofer" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
                        </td>
             </tr>
                   
                    <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblfechaembarque" runat="server" Text="Fecha Embarque"></asp:Label></td>
                <td>
                     <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaEmbarque" CssClass="toDate" Width="50%" runat="server" >
                            </telerik:RadDatePicker>

                        </td>
            </tr>
                   
                    <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                    <td>
                        &nbsp;</td>
             </tr>
         </table>

                                    </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RadPageViewProductos" runat="server">
                          <div id="divProductos" runat="server" style = "overflow:auto">
                              <telerik:RadGrid ID="rgProductos" runat="server" GridLines="None" onneeddatasource="rgProductos_NeedDataSource" DataMember="lstProductos" oninsertcommand="rgProductos_InsertCommand" 
                                    onitemdatabound="rgProductos_ItemDataBound" onupdatecommand="rgProductos_UpdateCommand" style="margin-bottom: 0px" ondeletecommand="rgProductos_DeleteCommand1" onitemcommand="rgProductos_ItemCommand" 
                                    onitemcreated="rgProductos_ItemCreated"  CellSpacing="0"  OnRowDrop="rgProductos_RowDrop" OnPreRender="rgProductos_PreRender" AllowPaging="True" Culture="es-ES">
                                  <ClientSettings>
                                      <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                  </ClientSettings>
                              <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="ID_Emp, Id_Cd, Id_Prd, Descripcion, Presentacion, Cantidad, Prd_UniEmp, Lote, Fabricacion, Caducidad, Marca, Costo, Num_Fac, Nom_Prov, ConservarProd" EditMode="InPlace" 
                              DataMember="lstProductos" HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros." PageSize="10">

                                <PagerStyle FirstPageToolTip="Pagina iniziale" LastPageToolTip="Ultima pagina"
                                    NextPagesToolTip="Pagine successive" NextPageToolTip="Pagina successiva"
                                    PagerTextFormat="Cambia pagina: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong> - Registros de <strong>{2}</strong> a <strong>{3}</strong> - Número total de registros <strong> {5} </strong>."
                                    PageSizeLabelText="Número de Registros:" PrevPagesToolTip="Págine Previa"
                                    PrevPageToolTip="Página Siguiente" />
                
                                <CommandItemSettings ShowAddNewRecordButton ="True" ExportToPdfText="Export to Pdf"  AddNewRecordText="Agregar"  RefreshText="Actualizar" />
                                <Columns>
                    <%-- Id Emp --%>             
                                     <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp" >
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                     </telerik:GridBoundColumn>

                    <%-- Id Cd --%>
                                     <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd" >
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                     </telerik:GridBoundColumn>

                                     <telerik:GridTemplateColumn HeaderText="Id Producto" DataField="Id_Prd" UniqueName="Id_PrdN" FooterStyle-Width = "70px">

                                        <FooterStyle Width="70px"></FooterStyle>

                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="txtProducto_TextChanged" OnLoad="txtProducto_Load" AutoPostBack="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Id Producto --%>
                                    <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd"  Visible="false">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Left" />
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
                                    <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Producto.Descripcion" UniqueName="Descripcion">
                                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-bottom-style: none">
                                                        <telerik:RadTextBox ID="txtPrd_Descripcion" runat="server" Width="200px" ReadOnly="true"
                                                            Text='<%# Eval("Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Presentación --%>
                                    <telerik:GridTemplateColumn HeaderText="Presentación" DataField="Presentacion" UniqueName="Presentacion">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# Eval("Presentacion") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtPrd_Presentacion" runat="server" Width="50px" ReadOnly="true"
                                                Text='<%#  Eval("Presentacion") %>'>
                                            </telerik:RadTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                    
                    <%-- Cantidad --%>
                                    <telerik:GridTemplateColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Width="60px" MaxLength="9"
                                                Text='<%# Eval("Cantidad") %>'  OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Unidades Empaque --%>
                                    <telerik:GridTemplateColumn HeaderText="Unidades Empaque" DataField="Prd_UniEmp" UniqueName="Prd_UniEmp">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrd_UniEmp" runat="server" Text='<%# Eval("Prd_UniEmp") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtPrd_UniEmp" runat="server" Width="50px" Text='<%# Eval("Prd_UniEmp") %>'  AutoPostBack="false">
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
                                            <telerik:RadTextBox ID="txtLote" runat="server" Width="100px" MaxLength="15" OnTextChanged="txtLote_TextChanged" AutoPostBack="true"
                                                Text='<%# Eval("Lote") %>' >
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtLote" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                   
                    <%-- Fecha Fabricación --%>
                                    <telerik:GridDateTimeColumn DataField="Fabricacion" HeaderText="Fabricacion" FilterControlWidth="165px"
                                         PickerType="DatePicker"  UniqueName = "Fabricacion" >
                                         <HeaderStyle Width="165px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>

                    <%-- Fecha Caducidad --%>
                                    <telerik:GridDateTimeColumn DataField="Caducidad" HeaderText="Caducidad" FilterControlWidth="165px"
                                         PickerType="DatePicker"  UniqueName = "Caducidad" >
                                         <HeaderStyle Width="165px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>

                    <%-- Marca --%>
                                    <telerik:GridTemplateColumn HeaderText="Marca" DataField="Marca" UniqueName="Marca" Visible  ="false" >
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("Marca") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtMarca" runat="server" Width="80px" MaxLength="9" Visible="false"
                                                Text= "NA" AutoPostBack="false">
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtMarca" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>   
                        
                    <%-- Costo AAA --%>
                                    <telerik:GridTemplateColumn HeaderText="Costo AAA Unitario" DataField="Costo" UniqueName="Costo">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCosto" runat="server" Text='<%# Eval("Costo") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCosto" runat="server" Width="80px" MaxLength="9"
                                                Text='<%# Eval("Costo") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtCosto" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Factura o remision --%>
                                    <telerik:GridTemplateColumn HeaderText="Num. Fact o Rem." DataField="Num_Fac" UniqueName="Num_Fac">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNum_Fac" runat="server" Text='<%# Eval("Num_Fac") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtNum_Fac" runat="server" Width="80px" MaxLength="9"
                                                Text='<%# Eval("Num_Fac") %>' AutoPostBack="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblVal_txtNum_Fac" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                    <%-- Proveedor --%>
                                    <telerik:GridTemplateColumn HeaderText="Proveedor" DataField="Nom_Prov" UniqueName="Nom_Prov">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNom_Prov" runat="server" Text='<%# Eval("Nom_Prov") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtNom_Prov" runat="server" Width="150px"
                                                Text='<%# Eval("Nom_Prov") %>' AutoPostBack="false">
                                            </telerik:RadTextBox>
                                            <asp:Label ID="lblVal_txtNom_Prov" runat="server" ForeColor="#FF0000"></asp:Label>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>

                   <%--Conservar--%>
                                     <telerik:GridTemplateColumn HeaderText="Conservar Producto" DataField="ConservarProd" UniqueName="ConservarProd">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblConservar" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConservarProd") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbltxtConservar" runat="server" ForeColor="#FF0000" Visible="false" />
                                            <telerik:RadComboBox ID="cmbConservar" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                MarkFirstMatch="true" LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbConservar_ClientSelectedIndexChanged"
                                                OnClientLoad="cmbConservar_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="100%" EnableLoadOnDemand="true" OnClientBlur="Combo_ClientBlur"  >
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
                                    
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar" HeaderText="Editar">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </telerik:GridEditCommandColumn>  
                                
                                    <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderText="Eliminar" UniqueName="DeleteColumn">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>

                             </Columns>     

                              <EditFormSettings>
                                 <EditColumn UniqueName="EditCommandColumn">
                                 </EditColumn>
                             </EditFormSettings>                       
                          </MasterTableView>
                      </telerik:RadGrid> 
                      <label>Solo se podrán agregar máximo 15 registros por queja, al llegar a 15 el botón de agregar se deshabilitara automáticamente.</label>
                                    </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RadPageViewArchivos" runat="server">
                                    <div id="TablasArchivos" runat="server">
                                     
                                     <table>
                                     <tr>
                                            <td colspan ="2"><b>Tipos de archivos validos son jpg, jpeg, gif, png, bmp, doc, docx, rtf, xml, ppt, pptx, xls, xlsx, pdf, txt.</b><br />
                                                            <img alt="" class="style5" src="Imagenes/archivos.png" />
                                            </td>
                                    </tr>
                                    <tr>
                                            <td>                                            
                                                 <asp:Panel ID="PnlFotos" runat="server" Width="900px" BorderStyle="Solid" BorderWidth="1px" GroupingText ="Fotos del producto" ScrollBars = "Auto">
                                            <table>
                                            <tr>
                                                <td>
                                                      <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="UploadFotos" MultipleFileSelection="Automatic" Width="500px" TemporaryFolder="~/Archivos/RadUploadTemp" 
                                                       onfileuploaded="RadAsyncUpload_FileUploaded" Visible ="true" />
                                                      <%--<telerik:RadProgressArea runat="server" ID="RadProgressArea2" CssClass = "textbox" />--%>
                                                </td>
                                             </tr>
                                             <tr>
                                                <td>
                                                     <telerik:RadGrid ID="gvFotos" runat="server" GridLines="None" AutoGenerateColumns="False" style="margin-bottom: 0px"  PageSize = "10" AllowPaging = "True"  CellSpacing="0"  Width="700px" MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                         onitemcommand="gvFotos_ItemCommand" onneeddatasource="gvFotos_NeedDataSource" onpageindexchanged="gvFotos_PageIndexChanged" ondeletecommand="gvFotos_DeleteCommand" DataMember="lstDocumento"   >

                                                                <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" 
                                                                DataKeyNames = "Id_Doc"  > 
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Folio" Display="true" UniqueName="Id_Doc" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Doc_Nombre" HeaderText="Nombre Documento" Display="true" UniqueName="Doc_Nombre" HeaderStyle-Width = "200px">
                                                                                        <HeaderStyle Width="200px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Formato" HeaderText="Formato" Display="true" UniqueName="Formato" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn DataField="Tamano" HeaderText="Tamano" Display="false" UniqueName="Tamano" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="Tipo" Display="true" UniqueName="TipoDoc" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Archivo" HeaderText="Archivo" Display="false" UniqueName="Archivo" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridTemplateColumn HeaderText="Descargar">
                                                                                     <ItemTemplate>
                                                                                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id_Doc", "CapDescargarArchivo.aspx?Id_Doc={0}") %>' Text="Descargar"></asp:HyperLink>
                                                                                     </ItemTemplate>
                                                                                 </telerik:GridTemplateColumn>

                                                                                 <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                                                        ButtonType="ImageButton" CommandName="Delete" HeaderText="Eliminar" UniqueName="DeleteColumn" >
                                                                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                                </telerik:GridButtonColumn> 
                                                                                  
                                                                         </Columns>     
                                                                 </MasterTableView>
                                                          <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente" SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
          
                                                      <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                            ShowPagerText="True" PageButtonCount="3" />
                                                 </telerik:RadGrid> 
                                                </td>
                                             </tr>
                                            </table>     
                                            </asp:Panel>
                                            </td>
                                      </tr>
                                      <tr>
                                            <td>
                                                 <asp:Panel ID="PnlFactura" runat="server" Width="900px" BorderStyle="Solid" BorderWidth="1px" GroupingText ="Factura del producto reportado" ScrollBars = "Auto">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="UploadFactura" MultipleFileSelection="Automatic" Width="500px" TemporaryFolder="~/Archivos/RadUploadTemp" 
                                                            onfileuploaded="RadAsyncUpload_FileUploaded" Visible ="true" />
                                                         <%--<telerik:RadProgressArea runat="server" ID="RadProgressArea3" CssClass = "textbox" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="gvFactura" runat="server" GridLines="None" 
                                                            AutoGenerateColumns="False"  style="margin-bottom: 0px"  PageSize = "10" AllowPaging = "True" 
                                                        CellSpacing="0"  Width="700px" 
                                                            MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                            onitemcommand="gvFactura_ItemCommand" 
                                                            onneeddatasource="gvFactura_NeedDataSource" 
                                                            onpageindexchanged="gvFactura_PageIndexChanged" 
                                                            ondeletecommand="gvFactura_DeleteCommand">

                                                                <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" 
                                                                DataKeyNames = "Id_Doc" >
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
                                                                            <Columns>
                                                                                 <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Folio" Display="true" UniqueName="Id_Doc" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Doc_Nombre" HeaderText="Nombre Documento" Display="true" UniqueName="Doc_Nombre" HeaderStyle-Width = "200px">
                                                                                        <HeaderStyle Width="200px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Formato" HeaderText="Formato" Display="true" UniqueName="Formato" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn DataField="Tamano" HeaderText="Tamano" Display="false" UniqueName="Tamano" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>
                                                                                
                                                                                <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="Tipo" Display="true" UniqueName="TipoDoc" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Archivo" HeaderText="Archivo" Display="false" UniqueName="Archivo" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridTemplateColumn HeaderText="Descargar">
                                                                                     <ItemTemplate>
                                                                                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id_Doc", "CapDescargarArchivo.aspx?Id_Doc={0}") %>' Text="Descargar"></asp:HyperLink>
                                                                                     </ItemTemplate>
                                                                                 </telerik:GridTemplateColumn>

                                                                                 <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                                                        ButtonType="ImageButton" CommandName="Delete" HeaderText="Eliminar" UniqueName="DeleteColumn">
                                                                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                                </telerik:GridButtonColumn> 

                                                                         </Columns>     
                                                                 </MasterTableView>
                                                          <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente" SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
          
                                                      <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                            ShowPagerText="True" PageButtonCount="3" />
                                                 </telerik:RadGrid> 
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                             </td>
                                       </tr>
                                       <tr>
                                            <td>                                     
                                                <asp:Panel ID="PnlAdicional" runat="server" Width="900px" BorderStyle="Solid" BorderWidth="1px" GroupingText ="Documentos Adicionales" ScrollBars = "Auto">
                                            <table>
                                                <tr>
                                                    <td> 
                                                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="UploadAdicional" MultipleFileSelection="Automatic" Width="500px" TemporaryFolder="~/Archivos/RadUploadTemp" 
                                                            onfileuploaded="RadAsyncUpload_FileUploaded" Visible ="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td>
                                                         <telerik:RadGrid ID="gvAdicional" runat="server" GridLines="None" 
                                                             AutoGenerateColumns="False" style="margin-bottom: 0px"  PageSize = "10" 
                                                             AllowPaging = "True"  CellSpacing="0"  Width="700px"   DataMember="lstDocumentos"
                                                      MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                             onitemcommand="gvAdicional_ItemCommand"  onneeddatasource="gvAdicional_NeedDataSource" 
                                                      onpageindexchanged="gvAdicional_PageIndexChanged" 
                                                             ondeletecommand="gvAdicional_DeleteCommand">

                                                                <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" 
                                                                DataKeyNames = "Id_Doc" >
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
                                                                            <Columns>
                                                                                 <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Folio" Display="true" UniqueName="Id_Doc" HeaderStyle-Width = "40px">
                                                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Doc_Nombre" HeaderText="Nombre Documento" Display="true" UniqueName="Doc_Nombre" HeaderStyle-Width = "200px">
                                                                                        <HeaderStyle Width="200px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Formato" HeaderText="Formato" Display="true" UniqueName="Formato" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn DataField="Tamano" HeaderText="Tamano" Display="false" UniqueName="Tamano" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="Tipo" Display="true" UniqueName="TipoDoc" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn DataField="Archivo" HeaderText="Archivo" Display="false" UniqueName="Archivo" HeaderStyle-Width = "70px">
                                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridTemplateColumn HeaderText="Descargar">
                                                                                     <ItemTemplate>
                                                                                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id_Doc", "CapDescargarArchivo.aspx?Id_Doc={0}") %>' Text="Descargar"></asp:HyperLink>
                                                                                     </ItemTemplate>
                                                                                 </telerik:GridTemplateColumn>

                                                                                 <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                                                        ButtonType="ImageButton" CommandName="Delete" HeaderText="Eliminar" UniqueName="DeleteColumn">
                                                                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                                </telerik:GridButtonColumn> 

                                                                         </Columns>     
                                                                 </MasterTableView>
                                                          <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente" SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
          
                                                      <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                            ShowPagerText="True" PageButtonCount="3" />
                                                 </telerik:RadGrid> 
                                                     </td>
                                                </tr>
                                            </table>     
                                                  
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

    <asp:HiddenField ID="HiddenField1" runat="server" Value="N" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HF_Cve" runat="server" />

  </asp:Content>