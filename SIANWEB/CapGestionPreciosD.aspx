<%@ Page Title="Gestión precios" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosD.aspx.cs" Inherits="SIANWEB.CapGestionPreciosD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

         

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }


            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana RadWindow actual
            //--------------------------------------------------------------------------------------------------
             function GetRadWindow() {
                var oWindow = null;
                if (window.RadWindow) {
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog     
                }
                else if (window.frameElement.RadWindow) {
                    oWindow = window.frameElement.RadWindow;  //IE (and Moz as well)  
                }


                if (oWindow == null) {
                    if (window.radWindow) {
                        oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog     
                    }
                    else if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;  //IE (and Moz as well)  
                    }
                }
                return oWindow;
            }


            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid();
            }

            function CloseWindowA(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
                        }

            function TabSelected(sender, args) {

            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
               
                    // debugger;
                    var oWnd = radopen("Ventana_ReportViewer.aspx", "RWReporte");
                    oWnd.center();
                    return true; 
            }

            function AbrirArchivoPDF(WebURL) {
                
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirArchivoPDF(WebURL) {
                debugger;
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFacturaVI");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirFacturaPDF(oWnd, eventArgs) {
                
                var oWnd1 = radopen(oWnd.argument, "AbrirVentana_ImpresionPDFFactura");
                oWnd1.set_showOnTopWhenMaximized(false);
                oWnd1.center();
                oWnd.remove_close(AbrirFacturaPDF);
            }
            function AbrirFacturaPDFCN(WebURL, WebURLCN) {
               
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                if (WebURLCN != '') {
                    oWnd.argument = WebURLCN
                    oWnd.add_close(AbrirFacturaPDF);
                }
                oWnd.center();
            }
            function refreshGrid() {

            }

            function onCommand(sender, eventargs) {
                if (eventargs.get_commandName() == "PerformInsert" || eventargs.get_commandName() == "Update" || eventargs.get_commandName() == "Delete") {
                    var radGrid = $find('<%= rgDetalles.ClientID %>');
                    var table = radGrid.get_masterTableView();
                    var column = table.getColumnByUniqueName("EditCommandColumn");
                    table.hideColumn(column.get_element().cellIndex);

                    column = table.getColumnByUniqueName("DeleteColumn");
                    table.hideColumn(column.get_element().cellIndex);
                }
            }
            function showcolum() {
                var radGrid = $find('<%= rgDetalles.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("EditCommandColumn");
                table.showColumn(column.get_element().cellIndex)

                column = table.getColumnByUniqueName("DeleteColumn");
                table.showColumn(column.get_element().cellIndex);
            }

            function callConfirm(mensaje) {
                radconfirm(mensaje, confirmCallBackFn);
            }


            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%=RAM1.ClientID%>");
                if (arg) {
                    ajaxManager.ajaxRequest('GuardarConvenio');
                }
                else {
                    ajaxManager.ajaxRequest('cancel');
                }
            }

            function callConfirmMod(mensaje) {
                radconfirm(mensaje, confirmModCallBackFn);
            }


            function confirmModCallBackFn(arg) {
                var ajaxManager = $find("<%=RAM1.ClientID%>");
                if (arg) {
                    ajaxManager.ajaxRequest('ModificarConvenio');
                }
                else {
                    ajaxManager.ajaxRequest('cancel');
                }
            }

            function OpenWindowVincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {

                AbrirVentana_VincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr);
            }
            function AbrirVentana_VincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {
                //debugger;
                var oWnd = radopen("Ventana_VinculaSucursal.aspx?Id_PC=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr, "Ventana_VincularSucursal");
                oWnd.center();

            }

            function confirmCallBackFnPrecio(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('continuar');
                } else
                { ajaxManager.ajaxRequest('rebind'); }
            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" ScrollBars="Vertical">
    </telerik:RadAjaxLoadingPanel>
          <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" 
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="True">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="RadUploadpdf">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
               <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                   <%-- <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="divGenerales" UpdatePanelHeight="" />--%>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalles" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <%--<telerik:AjaxUpdatedControl ControlID="btnFacturaEspecial" 
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtSub" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtIva" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal"  style="overflow:scroll; ">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="120%" dir="rtl" OnButtonClick="rtb1_ButtonClick" style="margin-right: 0">
            <Items>
                <telerik:RadToolBarButton CommandName="imprimir" Value="imprimir" ToolTip="Imprimir" CssClass="print"  ValidationGroup="print"
                ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" 
            style="font-family: verdana; font-size: 8pt; width: 99%;" runat="server">
            <tr>
                <td class="style2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
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
                    <table>
                     <tr>
                            <td class="style4">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            <td>
                               <asp:Label ID="LblConvAnt" Text="Convenio ant." runat="server" Visible="false">
                                </asp:Label>
                               </td>
                               <td>
                                <asp:Label ID="TxtConvAnt" Text="LblConvAnterior" runat="server" Visible="false">
                                </asp:Label>
                               </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="font-family: Verdana; font-size: 8pt">
                      <tr>
                            <td class="style4">
                                <asp:Label ID="Label4" Text="Categoría:" runat="server">
                                </asp:Label>
                            </td>
                            <td class="style1">
                                <telerik:RadComboBox ID="CmbId_Cat" MaxHeight="300px" runat="server" Width="150px"
                                    AutoPostBack="true" OnSelectedIndexChanged="CmbId_Cat_SelectedIndexChanged">
                    </telerik:RadComboBox>
                            </td>
                            <td class="style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="CmbId_Cat"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                 </td>
                            <td class="style1B">
                                <asp:Label ID="Label5" Text="Fecha de creación: " runat="server">
                                </asp:Label>
                              </td>
                            <td>
                                <asp:Label ID="TxtPC_FechaCreo" runat="server"  Enabled="false"> </asp:Label>
                              </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <%--Renglón 2 convenio key , ultima mod --%>
                                <tr>
                            <td class="style4">
                                <asp:Label ID="Label11" Text="Convenio Key:" runat="server"></asp:Label>
                               </td>
                            <td class="style1">
                                 <telerik:RadTextBox runat="server" ID="TxtKeyConvenio" MaxHeight="400px" Width="150px" ReadOnly="True">
                            </telerik:RadTextbox>
                         
                              </td>
                            <td class="style2">
                            </td>
                            <td class="style1B">
                                <asp:Label ID="Label3" Text="Última modificación:" runat="server"  > </asp:Label>
                            </td>
                              <td  >
                                <asp:Label ID="TxtPC_FechaUltMod" runat="server"  Enabled="false"> </asp:Label>
                            </td>
                            <td>
                                &nbsp;
                               </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <%--Renglón 3 convenio prov , Notas--%>
                          <tr>
                            <td class="style4">
                                <asp:Label ID="Label9" Text="Convenio proveedor:" runat="server"></asp:Label>
                                 </td>
                            <td class="style1">
                                <telerik:RadTextBox runat="server" ID="TxtPC_NoConvenio" MaxHeight="400px" Width="150px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td class="style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtPC_NoConvenio"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td class="style1B" valign="top" rowspan="3">
                                <asp:Label ID="Label10" Text="Notas:" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="Label13" Text="PDF Proveedor:" runat="server"> </asp:Label>&nbsp;
                            </td>
                            <td colspan="3" rowspan="3" valign="top">
                                <telerik:RadTextBox runat="server" ID="TxtPC_Notas" Height="80px" Width="400px" MaxLength="500"
                                    TextMode="MultiLine" Visible="false">
                            </telerik:RadTextbox>
                                   <asp:Button ID="buttonDescargarpdf" runat="server" CssClass="RadUploadSubmit" Text="Descargar Archivo"
                                    OnClick="btnDescargarPDF_Click" />
                              </td>
                            <td>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPC_Notas"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <%--Renglon 4 Fecha Inicio   --%>
                          <tr>
                            <td class="style4">
                                <asp:Label ID="Label12" Text="Fecha Inicio No. Conv.:" runat="server" ></asp:Label>
                              </td>
                            <td class="style1">
                                <telerik:RadDatePicker ID="TxtPC_FechaInicioConv" runat="server" Culture="es-MX" OnSelectedDateChanged="ValidarFechaInicioConv_SelectedDateChanged"
                                    Width="150px" AutoPostBack="true" Style="margin-top: 6px"   MaxDate="2039/1/1" ReadOnly="True"> 
                                </telerik:RadDatePicker>
                             </td>
                            <td class="style2">
                                <asp:Label ID="TxtPC_ConvenioOtro" runat="server" Style="color: Blue;  text-align:left;" > </asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--Renglon 5 Nombre Convenio   --%>
                               <tr>
                            <td class="style4">
                                <asp:Label ID="Label1" Text="Nombre de convenio:" runat="server" Style="margin-top: 6px"></asp:Label>
                            </td>
                            <td class="style1">
                                <telerik:RadTextBox runat="server" ID="TxtPC_Nombre" MaxHeight="400px" Width="150px"
                                    Style="margin-top: 6px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td class="style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtPC_Nombre"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                              <td>
                            </td>
                        </tr>
                        <%--Renglon 6 Ejecutivo de cuenta  --%>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="LblEjecutivo" Text="Ejecutivo de cuenta:" runat="server"  Visible="false">
                                </asp:Label>
                            </td>
                            <td class="style1">
                                <telerik:RadComboBox ID="CmbId_UEjecutivo" MaxHeight="400px" runat="server" Width="150px"  Visible="false">
                                </telerik:RadComboBox>
                        </td>
                            <td class="style2">
                                
                            </td>
                            <%--  Lado derecho --%>
                            <td class="style1B">
                                <asp:Label ID="Label2" Text="Carga de datos:" runat="server" Visible="false"> </asp:Label>&nbsp;
                            </td>
                            <td class="style1">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="xls,xlsx"
                                    Height="25px" Width="200px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30" Visible="false">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="ValidFiles" runat="server">
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportar_Click" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="buttonDescargarexcel" runat="server" CssClass="RadUploadSubmit" Text="Descargar Archivo"
                                    OnClick="btnExportar_Click"  Visible="false"/>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                        <%--Renglon 7 cargar PDF , Matriz CN  --%>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label7" Text="Desplegar Matriz de CN:" runat="server" Visible="false">
                                </asp:Label>
                            </td>
                            <td class="style1">
                                <asp:CheckBox ID="ChkMatrizCN" runat="server" Text="" Visible="false" />
                        </td>
                            <td class="style2">
                            </td>
                            <%--  Lado derecho --%>
                            <td class="style1B">
                    
                        </td>
                            <td class="style1">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf" AllowedFileExtensions="pdf"
                                    Height="25px" Width="180px" OnFileUploaded="RadAsyncUpload1_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="30" Visible="false">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="Panel2" runat="server">
                                 <asp:Label ID="lblNombreArchivoPDF" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                
                                <asp:Label ID="lblContenidoPDF" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Button ID="buttonSubmitpdf" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF_Click" Visible="false" />
                            </td>
                            <td>
                             
                            </td>
                        </tr>
                    </table>
                    <table >
  
                        <tr>
                             <td colspan="7" class="style1" >
                                <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1"
                                    Width="1300px" Height="380px" HorizontalAlign="NotSet">
                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSubmit" style="overflow:scroll; height:380px;   ">
                       <table>
                         <tr>
                            <td>
                         
                                                    <telerik:RadGrid ID="rgDetalles" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                      EnableLinqExpressions="False"  autopostback="True" GridLines="None" OnDeleteCommand="rgDetalles_DeleteCommand" OnInsertCommand="rgDetalles_InsertCommand"
                                                        OnItemCommand="rgDetalles_ItemCommand" OnItemDataBound="rgDetalles_ItemDataBound"
                                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="rgDetalles_UpdateCommand"
                                                        PageSize="8" Width="100%"  OnPageIndexChanged="rgDetalles_PageIndexChanged" AllowPaging="True"
                                                        Height="370px">

                                                        <%--  Width="1210px">--%>
                                                        <ClientSettings>
                                                            <ClientEvents OnCommand="onCommand" />
                                                           
                                                        </ClientSettings>

                                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="Id_Prd, Id_ConvDet " EditMode="InPlace"
                                                            NoMasterRecordsText="No se encontraron registros.">
                                                            <%--<CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                                                ShowRefreshButton="true" />--%>
                                    <Columns>
                        
                                                                <telerik:GridBoundColumn DataField="Id_ConvDet" UniqueName="Id_ConvDet" Visible="False">
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Clave Key" UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ProdLabel" runat="server" Text='<%# Eval("Id_Prd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="PCD_ClaveProv" HeaderText="Clave Proveedor" UniqueName="PCD_ClaveProv">
                                                                  <ItemTemplate>
                                                                        <asp:Label ID="ClaveProvLabel" runat="server" Text='<%# Eval("PCD_ClaveProv") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>


                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción de producto" UniqueName="Prd_Descripción">
                                                                <ItemTemplate>
                                                                        <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="250px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>
                    

                                                                 <telerik:GridTemplateColumn DataField="PCD_Referencia" HeaderText="Ref. Convenio"
                                                                    UniqueName="PCD_Referencia" Display="False">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="RadTextBoxPCD_Referencia" runat="server"  Text='<%# Bind("PCD_Referencia") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ReferenciaLabel" runat="server" Text='<%# Eval("PCD_Referencia") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>



                                                                <telerik:GridTemplateColumn DataField="PCD_PrecioVtaMin" HeaderText="P. Venta Min."
                                                                    UniqueName="PCD_PrecioVtaMin">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecioVtaMin" runat="server" MaxLength="9"
                                                                            MinValue="0" Text='<%# Bind("PCD_PrecioVtaMin") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PreciovtaminLabel" runat="server" Text='<%# Eval("PCD_PrecioVtaMin") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="PCD_PrecioVtaMax" HeaderText="P. Venta Max."
                                                                    UniqueName="PCD_PrecioVtaMax">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecioVtaMax" runat="server" MaxLength="9"
                                                                            MinValue="0" Text='<%# Bind("PCD_PrecioVtaMax") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PreciovtamaxLabel" runat="server" Text='<%# Eval("PCD_PrecioVtaMax") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEsp" HeaderText="<b>Anterior</b> <br> Precio AAA. Esp."
                                                                    UniqueName="PCD_PrecioAAEsp" Display="false" DataFormatString="{0:N2}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                                                <%--<telerik:GridBoundColumn DataField="PCD_FechaInicio" HeaderText="<b>Anterior</b> <br> Fecha inicio"
                                                                    UniqueName="PCD_FechaInicio" Display="false" DataFormatString="{0:dd/MM/yy}">
                                            <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                </telerik:GridBoundColumn>--%>

                                                                <telerik:GridTemplateColumn HeaderText="<b>Anterior</b> <br> Fecha inicio" UniqueName="PCD_FechaInicio" > 
                                                                        <EditItemTemplate> 
                                                                            <telerik:RadDateTimePicker ID="rptPCD_FechaInicio" runat="server" > 
                                                                            </telerik:RadDateTimePicker> 
                                                                             </EditItemTemplate> 
                                                                        <ItemTemplate> 
                                                                        <asp:Label ID="lbltime" runat="server" Text='<%# Eval( "PCD_FechaInicio","{0: M/d/yyyy}") %>' ></asp:Label> 
                                                                        </ItemTemplate> 
                                                                    </telerik:GridTemplateColumn> 


                                                         

                                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspA" HeaderText="<b>Anterior</b> <br> PAAA Esp."
                                                                    UniqueName="PCD_PrecioAAEspA" DataFormatString="{0:N2}">
                                                                   <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_FechaInicioA" HeaderText="<b>Anterior</b> <br> Fecha inicio"
                                                                    UniqueName="PCD_FechaInicioA" DataFormatString="{0:dd/MM/yy}">
                                            <ItemStyle HorizontalAlign="Center" />
                                                                   <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_FechaFinA" UniqueName="PCD_FechaFinA" Display="false"
                                                                    DataFormatString="{0:dd/MM/yy}">
                                            <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn DataField="PCD_PrecioAAAEsp" HeaderText="<u><b>Actual</b></u> <br> PAAA Esp."
                                                                    UniqueName="PCD_PrecioAAAEspB">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecioAAAEsp" runat="server" MaxLength="9"
                                                                            MinValue="0" Text='<%# Bind("PCD_PrecioAAAEsp") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PrecioAAAEspLabel" runat="server" Text='<%# Eval("PCD_PrecioAAAEsp") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="PCD_FechaInicio" HeaderText="<u><b>Actual</b></u> <br> Fecha inicio" UniqueName="PCD_FechaInicioB"  >
                                                                    <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("PCD_FechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpFechaActualInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("PCD_FechaInicio") %>'>
                                                                            <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput ID="DateInput1" runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>





                                                                <telerik:GridBoundColumn DataField="PCD_FechaFin" UniqueName="PCD_FechaFin" Display="false"
                                                                    DataFormatString="{0:dd/MM/yy}">
                                             <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspC" HeaderText="<b>Futuro</b> <br> PAAA Esp."
                                                                    UniqueName="PCD_PrecioAAEspC" DataFormatString="{0:N2}">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_FechaInicioC" HeaderText="<b>Futuro</b> <br> Fecha inicio"
                                                                    UniqueName="PCD_FechaInicioC" DataFormatString="{0:dd/MM/yy}">
                                            <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PCD_FechaFinC" UniqueName="PCD_FechaFinC" Display="false"
                                                                    DataFormatString="{0:dd/MM/yy}">
                                            <ItemStyle HorizontalAlign="Center" />
                                                                     <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        </telerik:GridBoundColumn>
                                                                  <telerik:GridTemplateColumn DataField="PCD_FechaFinVer" HeaderText="<u><b>Actual</b></u> <br> Fecha fin" UniqueName="upd_PCD_FechaFinVer"  >
                                                                    <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfechafinver" runat="server" Text='<%# Bind("PCD_FechaFinVer",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="ActualFechafin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaFin_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("PCD_FechaFinVer") %>'>
                                                                            <Calendar ID="calfechafinver" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput ID="DateInput1fin" runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>



                                                                <telerik:GridTemplateColumn DataField="PCD_PrecioPAAAEspProv" HeaderText="Costo <br> especial"
                                                                    UniqueName="PCD_PrecioPAAAEspProv" Display="False">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecioPAAAEspProv" runat="server"
                                                                            MaxLength="9" MinValue="0" Text='<%# Bind("PCD_PrecioPAAAEspProv") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PrecioPAAAEspProvLabel" runat="server" Text='<%# Eval("PCD_PrecioPAAAEspProv") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="PCD_PrecioVentaConvenio" HeaderText="P. Venta <br>Convenio"
                                                                    UniqueName="PCD_PrecioVentaConvenio">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecioVentaConvenio" runat="server"
                                                                            MaxLength="9" MinValue="0" Text='<%# Bind("PCD_PrecioVentaConvenio") %>' Width="100%">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PrecioVentaConvenioLabel" runat="server" Text='<%# Eval("PCD_PrecioVentaConvenio") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                 
                                                                <telerik:GridBoundColumn DataField="PCD_Margen" HeaderText="Margen" UniqueName="PCD_Margen"
                                                                    DataFormatString="{0:N1}%">
                                                                     <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                                            <EditFormSettings>
                                                                <EditColumn UniqueName="EditCommandColumn1">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="360px" />
                                </ClientSettings>
                                                    </telerik:RadGrid>
                               
                               
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadAjaxPanel>
                        </td>
                            
                           
                        
                        </tr>
                    </table>
                 
                </td>
            </tr>
            <tr>
               
                <td class="style3">
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                    <asp:HiddenField ID="HFCat_Consecutivo" runat="server" />
                    <asp:HiddenField ID="HFId_PC" runat="server" />
                    <asp:HiddenField ID="HFTipoOp" runat="server" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
    
        .style1
        {
            width: 1057px;
             font-weight: bold;
            
        }
        .style1B
        {
            width: 1057px;
            font-weight: bold;
        }
        .style2
        {
            width: 839px;
            

        }
        .style3
        {
            height: 8px;

        }
         .style4
        {
            width: 1457px;
            font-weight: bold;
           
        }
         .style450
        {
            width: 450px;

        }
      
    </style>
</asp:Content>

