<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ComprasLocalesConsulta.aspx.cs" Inherits="SIANWEB.ComprasLocalesConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />

<script type="text/javascript">
    $(function () {
        $("[id$=txtBuscaXCodProd]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/ComprasLocalesConsulta.aspx/GetProductName") %>',
                    data: "{ 'prodName': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[1],
                                val: item.split('-')[0]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hdtxtBuscaCodi]").val(i.item.val);
            },
            minLength: 1
        });
    });


    $(function () {
        $("[id$=txtBuscaXProvee]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/ComprasLocalesConsulta.aspx/GetProveedorSoli") %>',
                    data: "{ 'provName': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[1],
                                val: item.split('-')[0]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hdtxtBuscaProv]").val(i.item.val);
            },
            minLength: 1
        });
    });



</script>

<table id="TblEncabezado" style=" font-family: verdana; font-size: 8pt" runat="server" width="99%" >
    <tr>
        <td>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </td>
        <td style="text-align: right" width="150px">
            <asp:Label ID="LabelCDI81" runat="server" Text="Centro de distribucion "></asp:Label>
        </td>
        <td width="150px" style="font-weight: bold">
            <div id="dvCmbCentros" runat="server" >
                <telerik:RadComboBox ID="cmbCentrosDist" MaxHeight="300px" runat="server" 
                    OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
                    
            </div>
            <input type="hidden" id="txtCentrosDist" name="txtCentrosDist" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width: 90%; text-align: center" colspan="4">
            <asp:Label ID="lblTituloProducto" runat="server" CssClass="tituloProducto" Font-Size="28px"
                ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
<telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
    <AjaxSettings>
                
    </AjaxSettings>
</telerik:RadAjaxManager>

 <div runat="server" id="divConsultaSolicitud" style="font-family: Verdana; font-size: 8pt" visible="false">
<table width="950px" border="0" cellpadding="3" cellspacing="1" >
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" nowrap="nowrap">
            <table border=0 cellpadding=2 cellspacing=2>
                <tr>
                    <td nowrap="nowrap">Número de Solicitud:</td>
                    <td><asp:TextBox ID="txtBuscaXSolCom" runat="server" Width="100px" Enabled="true" MaxLength="6" /></td>
                    <td>&nbsp;<asp:Button ID="btnBuscaSoli" Text="Buscar por Solicitud" runat="server" OnClick="BuscaSolixSoli" Width="130px"  Visible="false"/></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td nowrap="nowrap">Codigo del producto:</td>
                    <td><asp:TextBox ID="txtBuscaXCodProd" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                    <td>&nbsp;<asp:Button ID="btnBuscaCod" Text="Buscar por Producto" runat="server" OnClick="BuscaSoliXProdu" Width="130px"  Visible="false"/></td>
                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaCodi" runat="server" /></td>
                </tr>
                <tr>
                    <td nowrap="nowrap">Proveedor:</td>
                    <td><asp:TextBox ID="txtBuscaXProvee" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                    <td>&nbsp;<asp:Button ID="btnBuscaProv" Text="Buscar por Proveedor" runat="server" OnClick="BuscaSoliXProve" Width="130px" Visible="false"/></td>
                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaProv" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="4" >&nbsp;<asp:Button ID="btnBuscaGen" Text="Buscar" runat="server" OnClick="BuscaCombinado" Width="130px"/></td>
                </tr>
            </table>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td  valign="top" nowrap="nowrap">
            <telerik:RadGrid ID="rgCompraLocal" runat="server" AutoGenerateColumns="False" GridLines="Both"
                PageSize="20" AllowPaging="True" 
                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                OnPageIndexChanged="rgCompraLocal_PageIndexChanged" OnNeedDataSource="rgCompraLocal_NeedDataSource"
                OnItemCommand="rgCompraLocal_ItemCommand">
                <MasterTableView>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Id_Comp" HeaderText="Clave" UniqueName="column">
                            <ItemTemplate>
                                <asp:Label ID="lblcve" runat="server" Text='<%# Bind("Id_Comp") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Cd_Nombre" HeaderText="CDI" UniqueName="column1" Visible="false">
                            <HeaderStyle Width="80px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Solicito_Nombre" HeaderText="Solicito" UniqueName="column2">
                            <HeaderStyle Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TipoSolicitud" HeaderText="Tipo de Solicitud" UniqueName="column3">
                            <HeaderStyle Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FechaAut" HeaderText="Fecha Autorización" UniqueName="column4"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FechaSol" HeaderText="Fecha Solicitud" UniqueName="column5">
                            <HeaderStyle Width="180px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Vigencia" HeaderText="Fecha Vigencia" UniqueName="column5">
                            <HeaderStyle Width="180px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EstatusAut" HeaderText="Partidas Autorizadas" UniqueName="column6" >
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton"  HeaderText="Ver detalle" CommandName="Detail" ImageUrl="Imagenes/iconos/book_blue_view.png" 
                            Text="Ver Detalle" UniqueName="DetailColumn">
                            <HeaderStyle Width="29px" />
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton"  HeaderText="Modificar" CommandName="Actualizar" ImageUrl="Imagenes/iconos/book_blue_view.png" 
                            Text="Actualizar Clientes" UniqueName="DetailColumn2">
                            <HeaderStyle Width="29px" />
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                    ShowPagerText="True" PageButtonCount="3" />
            </telerik:RadGrid>
            </td>
        <td >&nbsp;</td>
        <td valign="top" nowrap="nowrap">
            <telerik:RadGrid ID="rgDetalleSolicitud" runat="server" AutoGenerateColumns="false" GridLines="None"
                PageSize="30" AllowPaging="false" AllowSorting="false"  width="100%"
                OnItemCommand="rgDetalleSolicitud_ItemCommand"
                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                    visible="true">
                <MasterTableView>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Solicitud" HeaderText="Solicitud" UniqueName="column">
                            <ItemTemplate>
                                <asp:Label ID="lblSolicitud" runat="server" Text='<%# Bind("Solicitud") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="TipoSol" HeaderText="" UniqueName="column5" visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoSolicitud" runat="server" Text='<%# Bind("TipoSol") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Num" HeaderText="Num" UniqueName="column1">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                            <HeaderStyle Width="300px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Costo" HeaderText="Costo" UniqueName="column2">
                            <HeaderStyle Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="column3"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="column4">
                            <HeaderStyle Width="80px" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                    ShowPagerText="True" PageButtonCount="3" />
            </telerik:RadGrid>
        </td>
        </tr>
</table>
</div>
                                

</asp:Content>
