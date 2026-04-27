<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorReportesPropuestaTecnoEconomica.aspx.cs" 
Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.VisorReportesPropuestaTecnoEconomica" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>

    <style>
       table tr td
       {
           text-align:justify !important;
       }
    
    </style>

</head>

<body>

    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="124000" runat="server"></asp:ScriptManager>    

   <%-- <asp:Panel ID="pnlVisorDeReporte" runat="server">    
--%>
        <div id="divReporte" style="display:block; width:100%;">                                    
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" style="width:99%;">
            </rsweb:ReportViewer>
        </div>

   <%-- </asp:Panel>
--%>
    </form>

    <script type="text/javascript">
        //-- To Hide ReportViewer Export to Word and Excel (Works)
        /*
        window.onload = function () {
        try {
        var formatDropDown = document.getElementById('ReportViewer1_ctl06_ctl04_ctl00_Menu');
        var formats = formatDropDown.childNodes;
        if (formatDropDown != null) {
        console.log(formatDropDown);
        formatDropDown.removeChild(formats[1]);
        formatDropDown.removeChild(formats[2]);
        }
        } catch (err) {
        console.log('Error:' + err.message);
        }
        }*/
        $(document).ready(function () {
            setTimeout(function () {
                /*
                try {
                var formatDropDown = document.getElementById('ReportViewer1_ctl06_ctl04_ctl00_Menu');
                var formats = formatDropDown.childNodes;
                if (formatDropDown != null) {
                formatDropDown.removeChild(formats[1]);
                formatDropDown.removeChild(formats[2]);
                }
                } catch (err) {
                console.log('Error:' + err.message);
                }
                */
            }, 2000);
        });
    </script>
    
</body>