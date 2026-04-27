<%@ Page Title="Registro de facturación" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Reporte_LogFactura.aspx.cs" Inherits="SIANWEB.Reporte_LogFactura" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                //validar rango correcto de fechas.
                if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                    var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 330, 150, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                return true;
            }

            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'mostrar':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function txtCliente_OnBlur(sender, args) {

            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          <%--  <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
               <%-- <telerik:RadToolBarButton CommandName="mostrar" Value="mostrar" ToolTip="Imprimir"
                    CssClass="print" ImageUrl="~/Imagenes/blank.png" />--%>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                </td>
                <td style="text-align: right;display:none" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold;display:none">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Serie"></asp:Label>
                                </td>
                                <td>
                                     <telerik:RadComboBox ID="cmbSerie" runat="server" MaxHeight="250px" Width="120px">
                                     </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFecha1"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha2" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFecha2"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9" AutoPostBack="True" OnTextChanged="txtCliente_TextChanged">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtCliente_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_ValCliente" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                 <td width="70">
                                     <asp:Label ID="lblFac" runat="server" Text="Factura"></asp:Label>
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtFactura" runat="server" Width="70px" MinValue="1"
                                         MaxLength="9" AutoPostBack="True" >
                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                     </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button  ID="btnBuscar" CssClass="btn btn-info" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    
                    </td>
                </tr>
            </table>
               <table>
    <tr>
        <td>
            <telerik:RadGrid ID="rgLogDocumento" runat="server" AutoGenerateColumns="False" GridLines="None"
                OnNeedDataSource="rgLogDocumento_NeedDataSource" PageSize="15" AllowPaging="True"
                OnPageIndexChanged="rgLogDocumento_PageIndexChanged">
                <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                    <Columns>  
                         <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente">
                             <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="UsuarioFactura" HeaderText="Usuario Fac." UniqueName="UsuarioFactura">
                             <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn> 
                         <telerik:GridBoundColumn DataField="DocFactura" HeaderText="Factura" UniqueName="DocFactura">
                             <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>
                          
                         <telerik:GridBoundColumn DataField="FechaFactura" HeaderText="Fecha Fac." UniqueName="FechaFactura"  DataFormatString="{0:dd/MM/yyyy}">
                             <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>    
               
                        
                         <telerik:GridBoundColumn DataField="UsuarioCancela" HeaderText="Usuario Cancelación" UniqueName="UsuarioCancela">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn  DataField="FechaCancela" HeaderText="Fecha Cancelación" UniqueName="FechaCancela" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  

                        <telerik:GridBoundColumn  DataField="UsuarioRelacionAlmacen" HeaderText="Relacionó a Almacen" UniqueName="UsuarioRelacionAlmacen">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  

                         <telerik:GridBoundColumn  DataField="DocRelacionAlmacen" HeaderText="Documento" UniqueName="DocRelacionAlmacen">
                                <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>  

                        <telerik:GridBoundColumn  DataField="FechaRelacionAlmacen" HeaderText="Fecha Relación a Almacen" UniqueName="FechaRelacionAlmacen" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  

                   
                         <telerik:GridBoundColumn  DataField="UsuarioConfirmadaAlmacen" HeaderText="Confirmó a Almacen" UniqueName="UsuarioConfirmadaAlmacen">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                         <telerik:GridBoundColumn  DataField="DocConfirmadaAlmacen" HeaderText="Documento" UniqueName="DocConfirmadaAlmacen">
                                <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaConfirmadaAlmacen" HeaderText="Fecha Confirmada a Almacen" UniqueName="FechaConfirmadaAlmacen" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  

                      <%--  <telerik:GridBoundColumn  DataField="UsuarioEmbarque" HeaderText="Embarcó" UniqueName="UsuarioEmbarque">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                         <telerik:GridBoundColumn  DataField="DocEmbarque" HeaderText="Documento" UniqueName="DocEmbarque">
                                <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaEmbarque" HeaderText="Fecha Embarque" UniqueName="FechaEmbarque" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> --%>
                        
                        <telerik:GridBoundColumn  DataField="UsuarioEmbarqueV2" HeaderText="Embarcó (Factura/Remisión)" UniqueName="UsuarioEmbarqueV2">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocEmbarqueV2" HeaderText="Documento" UniqueName="DocEmbarqueV2">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaEmbarqueV2" HeaderText="Fecha Embarque" UniqueName="FechaEmbarqueV2" >
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                        <telerik:GridBoundColumn  DataField="UsuarioEntregada" HeaderText="Entregó" UniqueName="UsuarioEntregada">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocEntregada" HeaderText="Documento" UniqueName="DocEntregada">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaEntregada" HeaderText="Fecha Entrega" UniqueName="FechaEntregada" >
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                        <telerik:GridBoundColumn  DataField="UsuarioRegresoAlmacen" HeaderText="Regreso a Almacen" UniqueName="UsuarioRegresoAlmacen">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                         <telerik:GridBoundColumn  DataField="DocRegresoAlmacen" HeaderText="Documento" UniqueName="DocRegresoAlmacen">
                                <HeaderStyle Width="310px" />
                         </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaRegresoAlmacen" HeaderText="Fecha Regreso Almacen" UniqueName="FechaRegresoAlmacen" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                        <telerik:GridBoundColumn  DataField="UsuarioRelacionCobranza" HeaderText="Relación a Cobranza" UniqueName="UsuarioRelacionCobranza">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocRelacionCobranza" HeaderText="Documento" UniqueName="DocRelacionCobranza">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaRelacionCobranza" HeaderText="Fecha Relación Cobranza" UniqueName="FechaRelacionCobranza" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                        <telerik:GridBoundColumn  DataField="UsuarioConfirmadaCobranza" HeaderText="Confirmada en Cobranza" UniqueName="UsuarioConfirmadaCobranza">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocConfirmadaCobranza" HeaderText="Documento" UniqueName="DocConfirmadaCobranza">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaConfirmadaCobranza" HeaderText="Fecha Confirmada Cobranza" UniqueName="FechaConfirmadaCobranza" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                         <telerik:GridBoundColumn  DataField="UsuarioEnviadaRevision" HeaderText="Enviado a Revisión" UniqueName="UsuarioEnviadaRevision">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocEnviadaRevision" HeaderText="Documento" UniqueName="DocEnviadaRevision">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaEnviadaRevision" HeaderText="Fecha Envio a Revisión" UniqueName="FechaEnviadaRevision" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                         <telerik:GridBoundColumn  DataField="UsuarioConfirmadaRevision" HeaderText="Confirmó Revisión" UniqueName="UsuarioConfirmadaRevision">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocConfirmadaRevision" HeaderText="Documento" UniqueName="DocConfirmadaRevision">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaConfirmadaRevision" HeaderText="Fecha Confirmación Revisión" UniqueName="FechaConfirmadaRevision" >
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn> 

                         <telerik:GridBoundColumn  DataField="UsuarioEnviadaCobro" HeaderText="Envió a Cobro" UniqueName="UsuarioEnviadaCobro">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="DocEnviadaCobro" HeaderText="Documento" UniqueName="DocEnviadaCobro">
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn  DataField="FechaEnviadaCobro" HeaderText="Fecha Enviada a Cobro" UniqueName="FechaEnviadaCobro" >
                            <HeaderStyle Width="310px" />
                        </telerik:GridBoundColumn>
                        
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                    ShowPagerText="True" PageButtonCount="3" />
            </telerik:RadGrid>
        </td>
    </tr>
</table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>
