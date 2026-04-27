<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComprasLocalesHistorialPrecios.aspx.cs" Inherits="SIANWEB.ComprasLocalesHistorialPrecios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Historial de Precios</title>
</head>
<style>
    @import url(https://fonts.googleapis.com/css?family=Open+Sans:400,600);

*, *:before, *:after {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  background: #105469;
  font-family: 'Open Sans', sans-serif;
}
table {
  background: #012B39;
  border-radius: 0.25em;
  border-collapse: collapse;
  margin: 1em;
}
th {
  border-bottom: 1px solid #364043;
  color: #E2B842;
  font-size: 0.85em;
  font-weight: 600;
  padding: 0.5em 1em;
  text-align: left;
}
td {
  color: #fff;
  font-weight: 400;
  padding: 0.65em 1em;
}
.disabled td {
  color: #4F5F64;
}
tbody tr {
  transition: background 0.25s ease;
}
tbody tr:hover {
  background: #014055;
}


  </style>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="row header" border="1" cellpadding="1">
                    <tr>
                        <th class="cell">FECHA INICIAL</th>
                        <th class="cell">FECHA FINAL</th>
                        <th class="cell">TIPO PRECIO</th>
                        <th class="cell">PRECIO </th>
                    </tr>
                    <%=strTabla%>
                    <!-- <tr>
                        <td class="cell">01/02/2017</td>
                        <td class="cell">31/12/2050</td>
                        <td class="cell">AAA</td>
                        <td class="cell">$ 657.64</td>
                    </tr>
                    <tr>
                        <td class="cell">14/08/2019</td>
                        <td class="cell">24/05/2020</td>
                        <td class="cell">PUBLICO</td>
                        <td class="cell">$ 974.284</td>
                    </tr> -->
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
