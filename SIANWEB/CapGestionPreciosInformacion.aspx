<%@ Page Title="Información" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosInformacion.aspx.cs" Inherits="SIANWEB.CapGestionPreciosInformacion" %>



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
                                CloseWindow();
                             
                            });
            }

            //Hace un refresh completo de la  padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function AbrirArchivoPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function refreshGrid() {

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


            function AbrirArchivoPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
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
            <telerik:AjaxSetting AjaxControlID="RadUploadpdf">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="RadUploadpdf2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="RadUploadpdf3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="RadUploadpdf4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="RadUploadpdf5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
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
             <telerik:AjaxSetting AjaxControlID="btnSolVin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnSolVin2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnSolVin3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnSolVin4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSolVin5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="abrirarchivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal"  >
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="120%" dir="rtl" OnButtonClick="rtb1_ButtonClick" style="margin-right: 0">
            <Items>
                <telerik:RadToolBarButton CommandName="guardar" Value="guardar" ToolTip="Guardar" CssClass="save"  ValidationGroup="guardar"
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
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución" display="false"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True" display="false">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>


        <table style="font-family: Verdana; font-size: 8pt">
        <%--como realizar solicitud vinculacion--%>
            <tr>
           
                    <td class="style1B" colspan="3">
                                <asp:Label ID="lblInfo" Text="¿Cómo realizar las solicitudes de vinculación?" runat="server"> </asp:Label>
    </td>
                    
                    <td>
                    
                           
                    </td>
                    <td>  
                    </td>
                    <td></td>
               
               </tr>
 
              <tr>
                <td>
                    
                      <asp:Button ID="buttonDescargarpdf" runat="server" CssClass="borderbottom"  ToolTip = "Descargar Archivo" Text=""
                                    OnClick="btnDescargarPDF_Click" Visible= "false" />   
                </td>
                <td>
                   
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                    <td style="width: 25px;">
                        <asp:ImageButton ID="btnSolVin" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 2px -944px; height: 16px;
                            width: 16px; margin-left: 0px;" OnClick="Button1_Click" />
<%--  <asp:ImageButton ID="btnBuscar1" runat="server" ImageUrl="~/Img/find16.png" OnClick="Button1_Click"
                                    ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
--%>

                    </td>

                    <td style="width: 320px;">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf" AllowedFileExtensions="pdf"
                                    Height="25px" Width="150px" OnFileUploaded="RadAsyncUpload1_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="40">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                              
                              
                            </td>
                    <td style="width: 80px;">
                                <asp:Button ID="buttonSubmitpdf" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF_Click" />
                    </td>
                   <td style="width: 180px;">
                    <asp:Label ID="lblNombreOriginalPDF" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="true"></asp:Label>

                    </td>
                    <td>
                      <asp:Label ID="lblNombreArchivoPDF" runat="server"   Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                 
                    </td>
                      <td></td>
                    
            </tr>


            <%--Cómo saber si están aplicando los precios AAA especiales 2 --%>

              <tr>
           
                    <td class="style1B" colspan="3">
                                <asp:Label ID="lblInfo2" Text="¿Cómo saber si están aplicando los precios AAA especiales?" runat="server"> </asp:Label>
                    </td>
                    <td style="width: 180px;">
                     <asp:Label ID="lblNombreOriginalPDF2" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="true"></asp:Label>
                                               
                    </td>
                    <td>
                    </td>
                    <td></td>
                   
             </tr>
             <tr>
                <td>
                     <asp:Button ID="buttonDescargarpdf2" runat="server" CssClass="borderbottom"  ToolTip = "Descargar Archivo"  Text=""
                                    OnClick="btnDescargarPDF2_Click" />   
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
                <td>
                  
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
            </tr>
 
             
              <tr>
                    <td style="width: 25px;">
                        <asp:ImageButton ID="btnSolVin2" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 2px -944px; height: 16px;
                            width: 16px; margin-left: 0px;" OnClick="Button2_Click" />

                    </td>

                    <td style="width: 320px;">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf2" AllowedFileExtensions="pdf"
                                    Height="25px" Width="150px" OnFileUploaded="RadAsyncUpload2_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="40">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                              
                              
                            </td>
                    <td style="width: 80px;">
                                <asp:Button ID="buttonSubmitpdf2" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF2_Click" Visible="false" /> 
                    </td>
                   <td style="width: 180px;">
                          
                    </td>
                    <td>
                      <asp:Label ID="lblNombreArchivoPDF2" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                 
                    </td>
                      <td>
                            
                    </td>
            </tr>


             <%--Cómo consultar la bonificación total del centro de distribución archivo 3 --%>
             <tr>
           
                    <td class="style1B" colspan="3">
                                <asp:Label ID="lblInfo3" Text="¿Cómo consultar la bonificación total del centro de distribución?" runat="server"> </asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                   
             </tr>

              <tr>
                <td>
                   <asp:Button ID="buttonDescargarpdf3" runat="server" CssClass="borderbottom"  ToolTip = "Descargar Archivo"  Text=""
                                    OnClick="btnDescargarPDF3_Click" Visible="false" />   
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
                <td>
                  
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
            </tr>

             
              <tr>
                    <td style="width: 25px;">
                        <asp:ImageButton ID="btnSolVin3" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 2px -944px; height: 16px;
                            width: 16px; margin-left: 0px;" OnClick="Button3_Click" />

                    </td>

                    <td style="width: 320px;">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf3" AllowedFileExtensions="pdf"
                                    Height="25px" Width="150px" OnFileUploaded="RadAsyncUpload3_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="40">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                  
                              
                            </td>
                    <td style="width: 80px;">
                                <asp:Button ID="buttonSubmitpdf3" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF3_Click" />
                    </td>
                   <td style="width: 180px;">
                     <asp:Label ID="lblNombreOriginalPDF3" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="true"></asp:Label>
                                              
                    </td>
                    <td>
                      <asp:Label ID="lblNombreArchivoPDF3" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                 
                    </td>
                      <td>
                               
                    </td>
            </tr>


             <%--Puedo solicitar precios AAAEspeciales archivo 4 --%>
             <tr>
           
                    <td class="style1B" colspan="3">
                                <asp:Label ID="lblInfo4" Text="¿Puedo solicitar precios AAAEspeciales?" runat="server"> </asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                   <td></td>
               
               </tr>
 
              <tr>
                <td>
                   <asp:Button ID="buttonDescargarpdf4" runat="server"  CssClass="borderbottom"  ToolTip = "Descargar Archivo"  Text=""
                                    OnClick="btnDescargarPDF4_Click"  Visible="false" />  
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
                <td>
                  
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
             </tr>

             
              <tr>
                    <td style="width: 25px;">
                        <asp:ImageButton ID="btnSolVin4" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 2px -944px; height: 16px;
                            width: 16px; margin-left: 0px;" OnClick="Button4_Click" />

                    </td>

                    <td style="width: 320px;">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf4" AllowedFileExtensions="pdf"
                                    Height="25px" Width="150px" OnFileUploaded="RadAsyncUpload4_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="40">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                 
                            
                              
                            </td>
                    <td style="width: 80px;">
                                <asp:Button ID="buttonSubmitpdf4" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF4_Click" />
                    </td>
                   <td style="width: 180px;">
                     <asp:Label ID="lblNombreOriginalPDF4" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="true"></asp:Label>
   
                    </td>
                    <td>
                      <asp:Label ID="lblNombreArchivoPDF4" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                 
                    </td>
                      <td>
                                  
                    </td>
            </tr>


              <%--Política de precios de convenios AAAEspeciales archivo 5 --%>
             <tr>
           
                    <td class="style1B" colspan="3">
                                <asp:Label ID="lblInfo5" Text="Política de precios de convenios AAAEspeciales" runat="server"> </asp:Label>&nbsp;
                    </td>
                    
                    <td>
                    
                    </td>
                    <td>
                    </td>
                    <td></td>
                   
             </tr>

              <tr>
                <td>
                  <asp:Button ID="buttonDescargarpdf5" runat="server" CssClass="borderbottom"  ToolTip = "Descargar Archivo"  Text=""
                                    OnClick="btnDescargarPDF5_Click"   Visible="false" /> 
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                    
                </td>
            </tr>

             
              <tr>
                    <td style="width: 25px;">
                        <asp:ImageButton ID="btnSolVin5" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 2px -944px; height: 16px;
                            width: 16px; margin-left: 0px;" OnClick="Button5_Click" />

                    </td>

                    <td style="width: 320px;">
                                <telerik:RadAsyncUpload runat="server" ID="RadUploadpdf5" AllowedFileExtensions="pdf"
                                    Height="25px" Width="150px" OnFileUploaded="RadAsyncUpload5_FileUploadedpdf"
                                    ControlObjectsVisibility="None" ToolTip="Seleccione archivo PDF a subir" MaxFileInputsCount="1"
                                    InputSize="40">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                 
                            </td>
                    <td style="width: 80px;">
                                <asp:Button ID="buttonSubmitpdf5" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    OnClick="btnImportarPDF5_Click" />
                    </td>
                   <td style="width: 180px;">
                     <asp:Label ID="lblNombreOriginalPDF5" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="true"></asp:Label>
                     

                    </td>
                    <td>
                      <asp:Label ID="lblNombreArchivoPDF5" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000099"
                                            Visible="false"></asp:Label>
                                 
                    </td>
                      <td>
                         
                    </td>
            </tr>


        </table>
 <table>
            <tr>
               
                <td class="style3">
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                    <asp:HiddenField ID="HFCat_Consecutivo" runat="server" />
                    <asp:HiddenField ID="HFId_PC" runat="server" />
                    <asp:HiddenField ID="HFTipoOp" runat="server" />

                    <asp:HiddenField ID="HF_SolVin" runat="server" />
                    <asp:HiddenField ID="HF_SolVin2" runat="server" />
                    <asp:HiddenField ID="HF_SolVin3" runat="server" />
                    <asp:HiddenField ID="HF_SolVin4" runat="server" />
                    <asp:HiddenField ID="HF_SolVin5" runat="server" />   

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
        .borderbottom
            {
	            color: #000099;
	            background-color: Transparent;
	            height: 50px;
	            width: 150px;
	            padding: 10px;
	            border: solid 0px #000099;
	            border-bottom: solid 0px #000099;
	            font-size:X-Small;
	            font-weight:bold;
	            -moz-border-radius: 0px 0px 0px 0px;
	            
            }

      
    </style>
</asp:Content>

