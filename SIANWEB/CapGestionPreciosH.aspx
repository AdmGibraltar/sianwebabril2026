    <%@ Page Title="Historial de Convenio" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosH.aspx.cs" Inherits="SIANWEB.CapGestionPreciosH"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server" >
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
                debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow();
                            });
            }

            //Hace un refresh completo de la  padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
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
            
             
               <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                  
                    <telerik:AjaxUpdatedControl ControlID="rgDetalles" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal"  >
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" style="margin-right: 0">
            <Items>
                 <telerik:RadToolBarButton CommandName="Exportahistorial" Value="expconvenios" ToolTip="Exportar Historial" CssClass="Excel"
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
        <td class="style4">
                                <asp:Label ID="Label11" Text="Convenio Key:" Font-Bold="True" runat="server"></asp:Label>
                                 <telerik:RadTextBox runat="server" ID="TxtKeyConvenio" MaxHeight="400px" Width="150px"
                                    ReadnOnly="true"  BorderStyle="None">
                                </telerik:RadTextBox>
                            </td>
                            <td class="style1">
                               
                            </td>
        
        </tr>
            <tr>
               
                <td>
                  
                   
                   
                 
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

         <table >
  
                        <tr>
                             <td   class="style1" >
                                <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1"
                                    Width="1200px" Height="380px" HorizontalAlign="NotSet">
                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                    <%--<asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSubmit" style="overflow:scroll; height:380px;   ">--%>
                                   
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="rgDetalles" runat="server" AutoGenerateColumns="False" BorderStyle="double"
                                                    EnableLinqExpressions="False" autopostback="True" GridLines="None" OnItemCommand="rgDetalles_ItemCommand"
                                                    OnItemDataBound="rgDetalles_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                    PageSize="8" OnPageIndexChanged="rgDetalles_PageIndexChanged" AllowPaging="True"
                                                    Height="370px" Width="800px">
                                                    
                                                    <ClientSettings>
                                                        <ClientEvents OnCommand="onCommand" />
                                                    </ClientSettings>
                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="Id_PCH, Id_PC " EditMode="InPlace"
                                                        NoMasterRecordsText="No se encontraron registros.">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn UniqueName="Seleccionar">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSeleccionar_All" runat="server" Text="Seleccionar" ToolTip="Seleccionar/deseleccionar todos"
                                                                        AutoPostBack="true" OnCheckedChanged="SelTodo_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="Sel_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="center" Width="50px" />
                                                                 <ItemStyle HorizontalAlign="center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Id_PCH" UniqueName="Id_PCH" Display="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Id_PC" HeaderText="Convenio <br>Proveedor"
                                                                UniqueName="Id_PC" Visible="True">
                                                                <HeaderStyle HorizontalAlign="center" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <%-- <telerik:GridBoundColumn DataField="PC_Nombre" HeaderText="Nombre de convenio" UniqueName="PC_Nombre" Visible="true">
                                                                </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn DataField="PC_Nombre" HeaderText="Nombre de convenio" UniqueName="PC_Nombre">
                                                                <HeaderStyle HorizontalAlign="center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Cat_DescCorta" HeaderText="Categoría" UniqueName="Cat_DescCorta">
                                                                <HeaderStyle HorizontalAlign="center" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PC_Nombreusuario" HeaderText="Usuario <br> responsable"
                                                                UniqueName="PC_Nombreusuario">
                                                                <HeaderStyle HorizontalAlign="center" Width="80px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PC_FechaMod" HeaderText="Fecha de<br>modificación"
                                                                UniqueName="PC_FechaMod" DataFormatString="{0:dd/MM/yy}">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" Text="Imprimir"
                                                                UniqueName="Imprimir" Display="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                                ButtonCssClass="imprimir">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center"  Width="50px" />
                                                            </telerik:GridButtonColumn>
                                                            <%--                       <telerik:GridTemplateColumn DataField="Id_PC" HeaderText="Convenio Key" UniqueName="Id_PC" Visible="False">
                                                                <telerik:GridTemplateColumn DataField="PC_NoConvenio" HeaderText="Convenio <br> Proveedor" UniqueName="PC_NoConvenio">
                                                                    
                                                              
                                                            
                                                                <telerik:GridBoundColumn DataField="Cat_DescCorta" HeaderText="Categoría" UniqueName="Cat_DescCorta">
                                                                    <HeaderStyle Width="150px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                              

                                                               
                                                                
                                                               
                                                                  <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Imprimir"
                                                                    HeaderText="Editar" InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                </telerik:GridEditCommandColumn>--%>
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
     
     
                                </telerik:RadAjaxPanel>
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
            
        }
        .style1B
        {
            width: 1057px;
            
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
           
        }
         .style450
        {
            width: 450px;

        }
      
    </style>
</asp:Content>

