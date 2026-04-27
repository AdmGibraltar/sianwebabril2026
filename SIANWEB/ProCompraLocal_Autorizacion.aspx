<%@ Page Title="Autorización de compras locales" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProCompraLocal_Autorizacion.aspx.cs" Inherits="SIANWEB.ProAutCompraLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<script language="JavaScript">

    function muestra() {
        document.getElementById('divModif').style.display = 'block';
        document.getElementById('hfModifVigencia').innerHTML = '1';
    }

    function OnDateSelected(sender, eventArgs) {
            var date1 = sender.get_selectedDate();
            if (date1 == null ) 
            {
                return false;
            }
            else {
                
                var hoy = new Date();
                hoy.setHours(0,0,0,0);
                try{
                if (hoy > date1) {
                  alert("Fecha De Vigencia No Valida");
                  sender.clear();
                  //    return false;
                }
                }
                catch (e) {
                    alert(e.toString());
                }
                }
            }
</script>
    <div>
        <telerik:RadToolBar runat="server" ID="rtb1" AutoPostBack="True" dir="rtl" Width="100%"
            OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png" visible="false"
                    Enabled="false" Owner="rtb1" />
                <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir" visible="false"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="delete" CssClass="delete" ToolTip="Eliminar" visible="false"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false"
                    >
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" ToolTip="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png" visible="false" />
            </Items>
        </telerik:RadToolBar>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CmbCentro">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div id="divPrincipal" runat="server">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                   <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <br />
            <table style="font-family: verdana; font-size: 8pt;" >
                <!-- Tabla principal--->
                <tr>
                    <td>
                    </td>
                    <td>
                        <table cellpacing=2 border=0 cellspacing=2 width="600px" >
                            <!--Tab 1  Tabla 1-->
                            <tr>
                                <td>
                                    <b><asp:Label ID="Label3" runat="server" Text="Folio" /></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFolio" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><asp:Label ID="Label6" runat="server" Text="Sucursal" /></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSucursal" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><asp:Label ID="Label5" runat="server" Text="Usuario que solicita" /></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSolicitaId" runat="server"></asp:Label>
                                    <asp:Label ID="lblSolicitaNombre" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><asp:Label ID="Label2" runat="server" Text="Fecha de solicitud"/></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaSol" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><asp:Label ID="Label4" runat="server" Text="Fecha de autorización" /></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblAutorizacion" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr nowrap="nowrap" >
                                <td>
                                    <b><asp:Label ID="Label7" runat="server" Text="Fecha de Vigencia" /></b>
                                </td>
                                <td>&nbsp;</td>
                                <td nowrap="nowrap" >
                                    <asp:Label ID="lblFechaVigencia" runat="server" Text=""></asp:Label>&nbsp;
                                    <a href="#" onclick="muestra();" id="lnkModificar" runat="server" >modificar</a>&nbsp;&nbsp;
                                    <div id="divModif" style="display:none">
                                        <asp:HiddenField ID="hfModifVigencia" runat="server" />
                                        <telerik:RadDatePicker Runat="server" id="rdpVigencia" ClientEvents-OnDateSelected="OnDateSelected" ></telerik:RadDatePicker>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <b><asp:Label ID="Label8" runat="server" Text="Comentarios" /></b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top" align="left">
                                    <asp:Label ID="lblComentarios" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hddTipoSolicitud" runat="server" /> 
                                    <asp:HiddenField ID="hddCorreoSOlicitante" runat="server" /> 
                                    <asp:Label ID="lblTipoSolicitud" runat="server" visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
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
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="rg1" runat="server" OnNeedDataSource="rg1_NeedDataSource" AutoGenerateColumns="False"
                            GridLines="None" OnPageIndexChanged="rg1_PageIndexChanged" PageSize="15" AllowPaging="True"
                            MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Det" DataField="Id_Det" Visible="false" UniqueName="Id_Det">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="Id_Prd" UniqueName="Id_Prd">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Descripción" DataField="Descripcion" UniqueName="Descripcion">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="250px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn HeaderText="Costo Local" DataField="Costo" DataFormatString="{0:N}"
                                        UniqueName="Costo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn HeaderText="AAA Local" DataField="AAA" DataFormatString="{0:N}"
                                        UniqueName="AAA">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Publico Local" DataField="Publico" DataFormatString="{0:N}"
                                        UniqueName="Publico">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn HeaderText="Costo CEDIS" DataField="Costo_Old" DataFormatString="{0:N}"
                                        UniqueName="Costo_Old">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="AAA CEDIS" DataField="AAA_Old" DataFormatString="{0:N}"
                                        UniqueName="AAA_Old">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Publico CEDIS" DataField="Publico_Old" DataFormatString="{0:N}"
                                        UniqueName="Publico_Old">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FechaAut" HeaderText="Fecha de aut." UniqueName="FechaAut">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Autorizar" UniqueName="Autoriza">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkAutoriza" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado")) %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkAutoriza_All" runat="server" Text="Autorizar" GroupName="autorizaAll"
                                                TextAlign="Left" OnCheckedChanged="chkAutoriza_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Rechazar" UniqueName="Rechaza">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkRechaza" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkRechaza_All" runat="server" Text="Rechazar" GroupName="autorizaAll"
                                                TextAlign="Left" OnCheckedChanged="chkRechaza_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Pendiente" UniqueName="Pendiente" Display="false">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkPendiente" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkPendiente_All" runat="server" GroupName="autorizaAll" Text="Pendiente"
                                                TextAlign="Left" OnCheckedChanged="chkPendiente_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Compra local enfocada" UniqueName="Enfocada" Display="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEnfocada" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "CompraEnfocada")) %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "CompraEnfocada")) ? false : true %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkEnfocada_All" runat="server" Text="Compra local enfocada" TextAlign="Left"
                                                OnCheckedChanged="chkEnfocada_CheckedChanged" AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="AutorizaOld" UniqueName="AutorizaOld" Display="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutorizaOld" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="RechazaOld" UniqueName="RechazaOld" Display="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRechazaOld" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
