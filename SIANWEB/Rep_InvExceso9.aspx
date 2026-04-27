<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rep_InvExceso9.aspx.cs" Inherits="SIANWEB.Rep_InvExceso9" %>


<!DOCTYPE html>
<html xmlns=" http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>fusioncharts</title>
</head>

     

<body>
   
     <!-- Step 1 - Include the fusioncharts core library -->

    <script type="text/javascript" src="FusionChartJS/js/fusioncharts.js"></script>
    <!-- Step 2 - Include the fusion theme -->
    <script type="text/javascript" src="FusionChartJS/js/themes/fusioncharts.theme.fusion.js"></script>

     <script type="text/javascript">
         function myJS(var1, var2, var3, var4, var5, var6) {
             var window_dimensions = "toolbars=no,menubar=no,directories=no,location=no,scrollbars=auto,resizable=yes,status=no,width=530,height=500;left=200;top=200"
             window.open("Rep_InvExceso2.aspx?Proveedor=" + var1 + "&Centro=" + var2 + "&DiasVer=" + var4 + "&Tproducto=" + var3 + "&Indicador=" + var5 + "&Dias=" + var6, "_blank", window_dimensions);
         }
        </script>


        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px" style="font-weight: bold">
                    &nbsp;
                </td>
            </tr>
        </table>

     <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td>
                </td>
                <td style="width: 900px">
                    <asp:Label ID="lblLeyenda" runat="server" Text="Centro:<b>[Sucursal]</b> Ultima actualización:<b>[Fecha]</b>"
                        Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="lblerror" runat="server"></asp:label>
                </td>
                <td style="width: 900px">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">

                        <div id="chart-container"></div>
                       
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </asp:Panel>
 

                </td>
            </tr>
        </table>
     
    
     
</body>
</html>