<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SIANWEB.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <button id="btnCerrar" onclick="window.close();">Cerrar Ventana</button>&nbsp;&nbsp;
    </div>
    </form>
</body>
     <script>
         function loaded() {
             alert('Page is loaded');

         }

     </script>
</html>
