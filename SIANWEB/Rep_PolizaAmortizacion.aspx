<%@ Page Title="Reporte Poliza Amortización" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage01.master" CodeBehind="Rep_PolizaAmortizacion.aspx.cs" Inherits="SIANWEB.Rep_PolizaAmortizacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                args.set_cancel(!continuarAccion);
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
   <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" ClientEvents-OnRequestStart="onRequestStart"
        onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick" >
            <Items>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
       
        <div class="center" >
            <div class="center" style="margin-top:5%">
                <asp:Label ID="LblAnio" runat="server" Text="Año"></asp:Label>
                <telerik:RadComboBox ID="cmbAño" runat="server" Width="130px" MaxHeight="250px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
            ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True" ControlToValidate="cmbAño"
            ValidationGroup="Mostrar"></asp:RequiredFieldValidator>
            </div>
            <div class="center">
                
                <asp:Label ID="LblAnio0" runat="server" Text="Mes"></asp:Label>
                <telerik:RadComboBox ID="cmbMes" runat="server" Width="130px" MaxHeight="250px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" Owner="cmbMes" />
                        <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" Owner="cmbMes" />
                    </Items>
                </telerik:RadComboBox>
            </div>

            <div class="center">
                <asp:Label ID="LblNivel" runat="server" Text="Agrupar"></asp:Label>
                <asp:CheckBox ID="cbCliente" runat="server" Text="Cliente" />
            </div>
            
            <div class="center">
                 <asp:Label ID="Label2" runat="server" Text="Reporte"></asp:Label>
                <telerik:RadComboBox ID="cbReport" runat="server" Width="130px" MaxHeight="250px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Saldo Amortizar" Value="1" Owner="cbReport" />
                        <telerik:RadComboBoxItem runat="server" Text="Poliza" Value="2" Owner="cbReport" />
                        <telerik:RadComboBoxItem runat="server" Text="Kardex" Value="3" Owner="cbReport" />
                        <telerik:RadComboBoxItem runat="server" Text="Devolciones" Value="4" Owner="cbReport" />
                        <telerik:RadComboBoxItem runat="server" Text="Anticipada" Value="5" Owner="cbReport" />
                    </Items>
                </telerik:RadComboBox>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </div>
        
         
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .content
        {
            
            display: flex;
            align-items: center;
        }
        .center {
            margin: auto;
            width: 50%;
            padding: 10px;
        }
    </style>
</asp:Content>