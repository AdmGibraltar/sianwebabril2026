<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="ProPlaneacionRepartoDireccionEntregaRuta.aspx.cs" Inherits="SIANWEB.ProPlaneacionRepartoDireccionEntregaRuta" Title="Planeación de Reparto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"  />
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

<%--    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
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
    </telerik:radtoolbar>
--%>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Pedido"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="LableFecha" runat="server" Text="Fecha"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtFecha" runat="server" Width="60px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Credito"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCredito" runat="server" Width="70px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <telerik:RadTextBox onpaste="return false" ID="txtClienteNombre" runat="server" Width="250px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelECalle" runat="server" Text="Calle"></asp:Label>
                                    </td>
                                    <td valign="middle">
                                        <telerik:RadTextBox ID="txtEcalle" runat="server" onpaste="return false" Width="200px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelENumero" runat="server" Text="Número"></asp:Label>
                                    </td>
                                    <td class="style1" valign="middle">
                                        <telerik:RadTextBox ID="txtEnumero" runat="server" onpaste="return false">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle" width="10">
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelECP" runat="server" Text="CP"></asp:Label>
                                    </td>
                                    <td valign="middle">
                                        <telerik:RadNumericTextBox ID="txtEcp" runat="server" MaxLength="6" MinValue="0"
                                            Width="120px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelEColonia" runat="server" Text="Colonia"></asp:Label>
                                    </td>
                                    <td valign="middle">
                                        <telerik:RadTextBox ID="txtEcolonia" runat="server" onpaste="return false" Width="200px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelEMunicipio" runat="server" Text="Municipio"></asp:Label>
                                    </td>
                                    <td colspan="4" valign="middle">
                                        <telerik:RadTextBox ID="txtEmunicipio" runat="server" onpaste="return false" Width="200px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelEEstado" runat="server" Text="Estado"></asp:Label>
                                    </td>
                                    <td valign="middle">
                                        <telerik:RadTextBox ID="txtEestado" runat="server" onpaste="return false" Width="200px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelESector" runat="server" Text="Sector"></asp:Label>
                                    </td>
                                    <td class="style1" valign="middle">
                                        <telerik:RadTextBox ID="txtESector" runat="server" onpaste="return false">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle" width="10">
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelETelefonos" runat="server" Text="Teléfonos"></asp:Label>
                                    </td>
                                    <td valign="middle">
                                        <telerik:RadTextBox ID="txtEtelefono" runat="server" MaxLength="20" onpaste="return false"
                                            Width="125px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelEFax" runat="server" Text="Fax"></asp:Label>
                                    </td>
                                    <td class="style1" valign="middle">
                                        <telerik:RadTextBox ID="txtEfax" runat="server" MaxLength="20" Width="125px">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                    </td>
                                </tr>
                                <tr>

                                                <td colspan="9">Ruta:

                                                    <telerik:RadComboBox ID="cmbRutaG" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                               </td>
                                            </tr>
                                            <tr>
                                                <td colspan="9" align="right"><asp:Button ID="BtnAgregarDirEntrega" runat="server" Text="Guarda Ruta" OnClick="Click_BtnAgregarDirEntrega" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                    </table>
                    </td>
                    </tr>

        </table>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
    <asp:HiddenField ID="HF_Ped" runat="server" />
    <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function AbrirVentana_AsigPrdxPed_Asignar(Id, Id_Cte, Nom_Cte) {
                AbrirVentana_AsigPrdxPed(Id, Id_Cte, Nom_Cte);
                return false;
            }
            function AbrirVentana_PlaneacionReparto(Id, Id_Cte, Nom_Cte) {


                var oWnd = radopen("ProAsignPrdxPed.aspx", "AbrirVentana_PlaneacionReparto");
                oWnd.center();
            }
            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
        </script>
    </telerik:radcodeblock>
</asp:Content>
