<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" 
    AutoEventWireup="true" CodeBehind="RepHPedidoDX.aspx.cs" Inherits="SIANWEB.RepHPedidoDX" %>
    
<%@ Register assembly="DevExpress.Web.ASPxScheduler.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script language="javascript">
            function muestra_oculta1() {
            try {  
                if (document.getElementById) { //se obtiene el id
                    var el = document.getElementById('PedBack'); //se define la variable "el" igual a nuestro div
                    el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
                    // el = document.getElementById('PedC');
                    // el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                    // el = document.getElementById('PedI');
                    //  el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                }
            }
            catch(err) {
              alert(err.message);
            }
        }

        function muestra_oculta2() {
            try {
                if (document.getElementById) { //se obtiene el id
                    var el = document.getElementById('ParBack'); //se define la variable "el" igual a nuestro div
                    el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
                    //el = document.getElementById('ParC');
                    //el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                    //el = document.getElementById('ParI');
                    //el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                }
            }
            catch (err) {
                alert(err.message);
            }
        }



        function muestra_oculta3() {
            try {
                if (document.getElementById) { //se obtiene el id
                    var el = document.getElementById('PickBack'); //se define la variable "el" igual a nuestro div
                    el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
                    //el = document.getElementById('ParC');
                    //el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                    //el = document.getElementById('ParI');
                    //el.style.display = (el.style.display == 'none') ? 'block' : 'none';
                }
            }
            catch (err) {
                alert(err.message);
            }
        }

        function AbreFlat() {
            var FechaIstr = document.getElementById("<%=rdFechaInicial.ClientID%>");
            var sFeechaI = FechaIstr.value.replaceAll(/-/g, "");

            var FechaFstr = document.getElementById("<%=rdFechaFinal.ClientID%>");
            var sFeechaF = FechaFstr.value.replaceAll(/-/g, "");

            var Pedidostr = document.getElementById("<%=txtLPdidos.ClientID%>");
            var Filtrostr = document.getElementById("<%=cmbEstatusPedido.ClientID%>");

            url = "WebForm2.aspx?sFechaI=" + sFeechaI + "&sFechaF=" + sFeechaF + "&sPedidos=" + Pedidostr.value + "&sFiltro=" + Filtrostr.value;
            window.open(url, 'popup_window', 'width=900,height=800,left=100,top=50,resizable=yes');
        }

    </script>
</telerik:RadCodeBlock>
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
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
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
                        <asp:Label ID="Label3" runat="server" Text="Estatus Pedido:" />
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="cmbEstatusPedido" runat="server">
                            <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Pedido Completo"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Pedido Incompleto"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Pedido Con Picking"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Pedido Sin Picking"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:30px">&nbsp;</td>
                    <td rowspan="2" valign="top">
                        <table width="450px">
                            <tr class="a_Table-folder js-folder" id="js-january">
                                <td onclick="Javascript:muestra_oculta1();">📁 <b>Pedidos Totales</b></td>
                                <td align="right"><%=strPedTot %></td>
                            </tr>
                            <tr style="display:none" id="PedBack" >
                                <td colspan="3" valign="top"  width="100%" >
                                <table width="100%" cellspacing="2" border="0">
                                    <tr>
                                        <td colspan="5" onclick="Javascript:muestra_oculta1();" align="left">↩ Regresar</td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="right"><b>Pedidos</b></td>
                                        <td align="right"><b>Monto</b></td>
                                        <td align="center"><b> %</b></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Pedidos Completos</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strPedCom %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strPedComMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strPedComPerce %></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Pedidos Incompletos</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strPedInc %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strPedIncMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strPedIncPerce %></td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                            <tr><td colspan="3">&nbsp;</td></tr>
                            <tr class="a_Table-folder js-folder" id="js-february">
                                <td onclick="Javascript:muestra_oculta2();" align="left">📁 <b>Partidas Totales</b></td>
                                <td align="right"><%=strParTot %></td>
                            </tr>
                            <tr style="display:none"  id="ParBack">
                                <td colspan="3" valign="top" width="100%" >
                                <table width="100%" cellspacing="2">
                                    <tr>
                                        <td colspan="5" id="Td1" onclick="Javascript:muestra_oculta2();">↩ Regresar</td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="right"><b>Partidas</b></td>
                                        <td align="right"><b>Monto</b></td>
                                        <td align="center"><b>%</b></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Partidas Completas</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strParCom %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strParComMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strParComPerce %></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Partidas Incompletas</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strParInc %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strParIncMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strParIncPerce %></td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                            
                            <tr><td colspan="3">&nbsp;</td></tr>
                            <tr class="a_Table-folder js-folder" id="js-february2">
                                <td onclick="Javascript:muestra_oculta3();" align="left">📁 <b>Pedidos Con Picking</b></td>
                                <td align="right"><%=strPedConPicking %></td>
                            </tr> 
                            <tr style="display:none"  id="PickBack">
                                <td colspan="3" valign="top" width="100%" >
                                <table width="100%" cellspacing="2">
                                    <tr>
                                        <td colspan="5" id="Td1" onclick="Javascript:muestra_oculta3();">↩ Regresar</td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="right"><b>Pedidos</b></td>
                                        <td align="right"><b>Monto</b></td>
                                        <td align="center"><b>%</b></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd; border-top: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Pedidos Con Picking</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strPedConPicking %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strPedConPickingMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strPedCPickPorcen %></td>
                                    </tr>
                                    <tr style="border-bottom: 1px solid #ddd;">
                                        <td style="width:30px" nowrap>&nbsp;</td>
                                        <td style="width:150px" nowrap><i>Pedidos Sin Picking</i></td>
                                        <td style="width:50px" nowrap align="right">&nbsp;<%=strPedSinPicking %></td>
                                        <td style="width:120px" nowrap align="right">&nbsp;$ <%=strPedSinPickingMonto %></td>
                                        <td style="width:80px" nowrap align="right">&nbsp;<%=strPedSPickPorcen %></td>
                                    </tr>
                                </table>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                    <td colspan="3" align="center" valign="top">
                            <button id="btnXcel" onclick="AbreFlat();">Exportar Excel (Columnas)</button>&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel" OnClick="buttonSaveAs_Click"/>&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnActualiza" Text="Actualiza" OnClick="btnActualiza_Click"/>
                        <div style="visibility:hidden;">
                            <asp:Button runat="server" ID="btnExportarExcelDirecto" Text="Exportar a Excel (Solo Datos)" OnClick="btnExportarExcelDirecto_Click"/>
                        </div>
                        
                   </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;
            <asp:Label ID="lblMensaje" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
             <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" OptionsPager-RowsPerPage="500" 
                Theme="Moderno" ClientIDMode="AutoID" DataSourceID="PivotDataSource" 
                OptionsView-ShowRowTotals="False" ClientInstanceName="PivotGrid"
                OptionsView-ShowTotalsForSingleValues="False" 
                OptionsCustomization-AllowFilter="True">
                <Fields>
                
                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="IdCte" ID="fIdCte"
                        Caption="Id Cliente" />
                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="NomCte" ID="fNomCte"
                        Caption="Cliente" />
                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="Id_Ped" ID="fIdPedido"
                        Caption="Pedido" />
                    <dx:PivotGridField Area="FilterArea" AreaIndex="4" FieldName="IdProducto" ID="fIdProducto"
                        Caption="Codigo Producto" />
                    <dx:PivotGridField Area="FilterArea" AreaIndex="5" FieldName="Producto" ID="ffProducto"
                        Caption="Producto" />

                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TipoFecha" ID="fTipoFecha"
                        Caption="Tipo Fecha"/>
           
                    <dx:PivotGridField Area="FilterArea" AreaIndex="1" FieldName="EstatusF" ID="fEstatus"
                        Caption="Estatus Pedido" SummaryType="Max" SummaryDisplayType="Default" />
                    <dx:PivotGridField Area="FilterArea" AreaIndex="2" FieldName="PorcentajeF" ID="fPorcentaje"
                        Caption="% Cumplimiento Fact" SummaryType="Max" SummaryDisplayType="PercentOfColumn" />
                    
                    <dx:PivotGridField Area="FilterArea" AreaIndex="4" FieldName="Id_Fac" ID="fIdFactura"
                        Caption="Factura" SummaryType="Max" />

                  <dx:PivotGridField Area="DataArea" AreaIndex="1" FieldName="Cantidad" ID="fCantidad"
                        Caption="Unidades" Options-ShowTotals="False" 
                        Options-ShowCustomTotals="False" Options-ShowGrandTotal="False" />
                  <dx:PivotGridField Area="DataArea" AreaIndex="2" FieldName="Monto" ID="fMonto"
                        Caption="Monto" Options-ShowTotals="False" CellFormat-FormatString="c0" CellFormat-FormatType="Numeric"
                        Options-ShowCustomTotals="False" Options-ShowGrandTotal="False" />

                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="Fecha" ID="fFecha" CellFormat-FormatType="DateTime"
                        Caption="Fecha" SummaryType="Max" SummaryDisplayType="Default" 
                        CellFormat-FormatString="dd/MMM/yyyy" Options-ShowTotals="False" 
                        Options-ShowCustomTotals="False" Options-ShowGrandTotal="False" />

                    
                        
                </Fields>
                <OptionsView HorizontalScrollBarMode="Auto" />
                <OptionsPager RowsPerPage="500"></OptionsPager>
                <optionsdata dataprocessingengine="LegacyOptimized" />
            </dx:ASPxPivotGrid>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:SqlDataSource ID="PivotDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:sianwebmtyConnectionString %>" >
            </asp:SqlDataSource>
            <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="ASPxPivotGrid1" Visible="False" />
            <asp:HiddenField ID="HF_ClvPag" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </td>
    </tr>
</table>
</div>
</asp:Content>
