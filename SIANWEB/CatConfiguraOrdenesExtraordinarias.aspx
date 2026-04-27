<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" 
    AutoEventWireup="true" CodeBehind="CatConfiguraOrdenesExtraordinarias.aspx.cs" Inherits="SIANWEB.CatConfiguraOrdenesExtraordinarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
<div runat="server" id="divPrincipal">
    <table style="font-family: Verdana; font-size: 8pt">
        <tr>
            <td colspan="3">&nbsp;
                <asp:Label ID="lblMensaje" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" style="height:150px" cellspacing="1">
                    <tr>
                        <td valign="top">
                            <h2>Parámetros de Verificación - Ordenes de Compra Extraordinarias </h2>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td valign="top">&nbsp;
                                        <asp:Label ID="lbl01" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura01" />
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox runat="server" ID="txtProVta3Meses" TextMode="Number"  Width="50" MaxLength="2"  AutoPostBack="false" />
                                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtProVta3Meses" ID="regExpPromVta3" ValidationExpression = "^[0-9]+\.[0-9]{2}$" runat="server" 
                                                    ForeColor="DarkRed" Font-Bold="true" 
                                                    ErrorMessage="Porcentaje Maximo no puede ser mayor del 99%."></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width:10px"> &nbsp;</td>
                                    <td>&nbsp;
                                        <asp:Label ID="lbl02" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura02" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtProVta12Meses" TextMode="Number"  Width="50" MaxLength="2"  AutoPostBack="false" />
                                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtProVta12Meses" ID="regExpProVmta12" ValidationExpression = "^[0-9]+\.[0-9]{2}$" runat="server" 
                                                    ForeColor="DarkRed" Font-Bold="true" 
                                                    ErrorMessage="Porcentaje Maximo no puede ser mayor del 99%."></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr><td colspan="5" style="height:10px"> &nbsp;</td></tr>
                                <tr>
                                    <td>&nbsp;
                                        <asp:Label ID="lbl03" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura03" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDesvEst3Meses" TextMode="Number" Width="50"  MaxLength="2"  AutoPostBack="false" />
                                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtDesvEst3Meses" ID="regExpDesEst3" ValidationExpression = "^[0-9]+\.[0-9]{2}$" runat="server" 
                                                    ForeColor="DarkRed" Font-Bold="true" 
                                                    ErrorMessage="Porcentaje Maximo no puede ser mayor del 99%."></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width:10px"> &nbsp;</td>
                                    <td>&nbsp;
                                        <asp:Label ID="lbl04" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura04" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDesvEst12Meses" TextMode="Number" Width="50"  MaxLength="2"  AutoPostBack="false" />
                                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtDesvEst12Meses" ID="regExpDesEst12" ValidationExpression = "^[0-9]+\.[0-9]{2}$" runat="server" 
                                                    ForeColor="DarkRed" Font-Bold="true" 
                                                    ErrorMessage="Porcentaje Maximo no puede ser mayor del 99%."></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr><td colspan="5" style="height:10px"> &nbsp;</td></tr>
                                <tr>
                                    <td>&nbsp;
                                        <asp:Label ID="lbl05" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura05" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDiasAcumCantOCEx" TextMode="Number" Width="50" AutoPostBack="false" />
                                    </td>
                                    <td style="width:10px"> &nbsp;</td>
                                    <td>&nbsp;
                                        <asp:Label ID="lbl06" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura06" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtMesePicoMax" TextMode="Number" Width="50" MaxLength="2" AutoPostBack="false" />
                                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtMesePicoMax" ID="regExpPicoMax" ValidationExpression = "^[0-9]+\.[0-9]{2}$" runat="server" 
                                                    ForeColor="DarkRed" Font-Bold="true" 
                                                    ErrorMessage="Porcentaje Maximo no puede ser mayor del 99%."></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td style="height:20px"> &nbsp;</td></tr>
                    <tr>
                        <td valign="top">
                            <table class="blueTable">
                                <tbody>
                                    <tr>
                                        <td valign="top">&nbsp;
                                            <asp:Label ID="lbl07" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura07" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" nowrap>
                                            <asp:DropDownList runat="server" ID="drpCondicion1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAYOR A" Text="MAYOR A"/>
                                                <asp:ListItem Value="MENOR A" Text="MENOR A"/>
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam1Op1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam1Op2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor1P1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor2P1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP1O1" TextMode="Number" Width="50" MaxLength="4" />
                                            &nbsp;,&nbsp;
                                            <asp:DropDownList runat="server" ID="drpFactor3P1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP1O2" TextMode="Number" Width="50" MaxLength="4" />
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" style="height:10px"> &nbsp;</td></tr>
                                    <tr>
                                        <td valign="top">&nbsp;
                                            <asp:Label ID="lbl08" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura08" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" nowrap>
                                            <asp:DropDownList runat="server" ID="drpCondicion2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAYOR A" Text="MAYOR A"/>
                                                <asp:ListItem Value="MENOR A" Text="MENOR A"/>
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam2Op1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam2Op2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor1P2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor2P2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP2O1" TextMode="Number" Width="50" MaxLength="4" />
                                            &nbsp;,&nbsp;
                                            <asp:DropDownList runat="server" ID="drpFactor3P2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP2O2" TextMode="Number" Width="50" MaxLength="4" />
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" style="height:10px"> &nbsp;</td></tr>
                                    <tr>
                                        <td valign="top">&nbsp;
                                            <asp:Label ID="lbl09" runat="server" />
                                            <asp:HiddenField runat="server" ID="hdfConfigura09" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" nowrap>
                                            <asp:DropDownList runat="server" ID="drpCondicion3" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAYOR A" Text="MAYOR A"/>
                                                <asp:ListItem Value="MENOR A" Text="MENOR A"/>
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam3Op1" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList runat="server" ID="drpParam3Op2" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="MAX" Text="MAX" />
                                                <asp:ListItem Value="MIN" Text="MIN" />
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor1P3" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="drpFactor2P3" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP3O1" TextMode="Number" Width="50" MaxLength="4"/>
                                            &nbsp;,&nbsp;
                                            <asp:DropDownList runat="server" ID="drpFactor3P3" >
                                                <asp:ListItem Value="" Text="-- Seleccione -- " Selected="True"  />
                                                <asp:ListItem Value="1" Text="PVM Últimos 3 Meses"/>
                                                <asp:ListItem Value="2" Text="PVM Últimos 12 Meses"/>
                                                <asp:ListItem Value="3" Text="PDE Últimos 3 Meses"/>
                                                <asp:ListItem Value="4" Text="PDE Últimos 12 Meses"/>
                                                <asp:ListItem Value="6" Text="Pico Máximo Últimos 24 Meses"/>
                                            </asp:DropDownList>
                                            *
                                            <asp:TextBox runat="server" ID="txtMultiplicadorP3O2" TextMode="Number" Width="50" MaxLength="4"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td align="center" valign="top">


                                <asp:Button runat="server" ID="btnActualiza" Text="Actualiza" CssClass="primary" OnClick="btnActualiza_Click"  />
                            <div style="visibility:hidden;">
                            
                            </div>
                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
