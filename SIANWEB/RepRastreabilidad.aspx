<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" 
    CodeBehind="RepRastreabilidad.aspx.cs" Inherits="SIANWEB.RepRastreabilidad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

<style>
    
.table {
  border-collapse: collapse;
}
.table td, table th {
  border: 1px solid black;
}
.table tr:first-child th {
  border-top: 0;
}
.table tr:last-child td {
  border-bottom: 0;
}
.table tr td:first-child,
.table tr th:first-child {
  border-left: 0;
}
.table tr td:last-child,
.table tr th:last-child {
  border-right: 0;
}

.tdCenter {
    align-content:center;
}


div.FixHeader {
 width:auto;
 height:280px;
 overflow:auto;
}
.LN { 
  background-color:#FFFFFF;
  color:#000000;
  border-color: #C0C0C0;
  border-left-style: solid;
  border-left-width: 2px;
  border-right-style: solid;
  border-right-width: 2px;
  border-top-width: 2px;
  border-bottom-width: 2px;
}

</style>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RAM1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadToolBar1">
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
    </AjaxSettings>
</telerik:RadAjaxManager>
<div runat="server" id="divPrincipal">
<table style="font-family: Verdana; font-size: 8pt">
    <tr>
        <td>
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td colspan="5">
                    </td>
                    <td style="text-align: right" width="500px">
                        <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" style="height:150px" cellspacing="1">
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td valign="top">&nbsp;
                                    <asp:Label ID="lbl01" runat="server" Text="Fecha Inicial:" />
                                </td>
                                <td valign="top">
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rdFechaInicial" Width="100px" runat="server" >
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr><td colspan="2" style="height:10px"> &nbsp;</td></tr>
                            <tr>
                                <td>&nbsp;
                                    <asp:Label ID="lbl02" runat="server" Text="Fecha Final:" />
                                </td>
                                <td>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rdFechaFinal" Width="100px" runat="server" >
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </td>

                    
                    
                    <td rowspan="2" style="width:10px">&nbsp;</td>
                    <td rowspan="2" valign="top">&nbsp;
                        <asp:Label ID="lblPedidos" runat="server" Text="Pedido(s):" />
                    </td>
                    <td rowspan="2" valign="top">
                        <asp:TextBox ID="txtLPdidos" runat="server" Width="200px" Height="50px" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td style="width:10px">&nbsp;</td>
                    <td valign="top">&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="Tipo Documento:" />
                    </td>
                    <td valign="top">
                        <!-- <asp:ListItem Value="2" Text="Notas de Credito"></asp:ListItem> -->
                        <asp:DropDownList ID="cmbTipoDocto" runat="server">
                            <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Factura"></asp:ListItem>
                            
                            <asp:ListItem Value="3" Text="Remisiones"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Pago"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:30px">&nbsp;</td>
                    <td rowspan="2" valign="top">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                    <td colspan="3" align="center" valign="top">
                            <!-- button id="btnXcel" onclick="AbreFlat();">Exportar Excel (Columnas)</button --> &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel" OnClick="btnExportar_Click"/>&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnActualiza" Text="Actualiza" OnClick="btnActualiza_Click" />
                        <div style="visibility:hidden;">
                            <asp:Button runat="server" ID="btnExportarExcelDirecto" Text="Exportar a Excel (Solo Datos)" />
                        </div>
                        
                   </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td >&nbsp;
            <asp:Label ID="lblMensaje" runat="server" />
        </td>
    </tr>
    <tr>
        <td >
            <div style="height: 600px; overflow-y: scroll;"  class="FixHeader container" >
                <table style='width: 1000px; border:2px solid black;  ' id='tbldatos' class="table"  >
                    <thead>
                        <tr>
                            <th colspan="2" class="LN">Cliente</th>
                            <th colspan="2" class="LN">Pedido</th>
                            <th colspan="3" class="LN">Remisión</th>
                            <th colspan="3" class="LN">Factura</th>
                            <th colspan="3" class="LN">Cobranza</th>
                        </tr>
                        <tr  align="center">
                            <td class="LN">Num Cliente</td>
                            <td class="LN">Nombre Cliente</td>
                            <td class="LN">Num Pedido</td>
                            <td class="LN">Fecha Pedido</td>
                            <td class="LN">Fecha</td>
                            <td class="LN">Cantidad</td>
                            <td class="LN">Monto</td>
                            <td class="LN">Fecha</td>
                            <td class="LN">Cantidad</td>
                            <td class="LN">Monto</td>
                            <td class="LN">Fecha</td>
                            <td class="LN">Cantidad</td>
                            <td class="LN">Monto</td>
                            
                        </tr>
                    </thead>
                    <tbody>
                        <%=strTabla%>
                    </tbody>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td >
            <asp:HiddenField ID="HF_ClvPag" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </td>
    </tr>
</table>

</div>
    <script>
        $(document).ready(function () {
            FixHeader(".FixHeader");
        });

        function FixHeader(e) {
            if ($(e).length) {
                $(e).bind("scroll", function () {
                    $e = $(this);
                    $table = $e.find("table:first");
                    $clone = $e.find("#" + $table.attr("clone"));
                    if (!$clone.length) {
                        id = new Date().getTime();
                        $table.attr("clone", id);
                        $clone = $table.clone();
                        $clone.attr("id", id);
                        $clone.css('position', 'relative');
                        $clone.css("visibility", "hidden");
                        $e.append($clone);
                        $clone = $e.find("#" + $table.attr("clone"));
                        $clone.css("width", $clone.innerWidth() + "px");
                        $clone.find("thead tr").find("th, td").each(function () {
                            if ($(this).is(":visible"))
                                $(this).css("width",  $(this).innerWidth() + "px");
                        });
                        $clone.find("tbody, tfoot").each(function () { $(this).html(''); });
                        $clone.css("visibility", "");
                    }
                    $clone.offset({ top: $table.offset().top + $e.scrollTop(), left: $table.offset().left +2 } );
                });
            }
        }
    </script>
</asp:Content>
