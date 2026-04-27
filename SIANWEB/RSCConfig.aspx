<%@ Page Language="C#" 
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    AutoEventWireup="true" 
    CodeBehind="RSCConfig.aspx.cs" 
    Inherits="SIANWEB.GestionRSC.RSCConfig" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
    <div class="container-fluid">    

    </div>

    
<script type="text/javascript">
    document.title = 'Gestion RSC';
    var _ApplicationUrl = '<%=ApplicationUrl %>';
    var _Usuario_Tipo = "<%= Usuario_Tipo %>";
    var hfId_Rik = '<%=Id_Rik %>';
    var Id_TU = '<%=Id_TU %>';
         var Id_CD = '<%=Id_CD %>';
         var Id_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
        var Id_U = '<%=Id_U %>';
    // 1OCT-2021 RFH
    var GLOBAL_Activo_AcysCuentasNacionales = 0;
</script>

    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_config.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Login.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Paginacion.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ClienteDet_ajax.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Cliente.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_ajax.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_main.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Func.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/GC/GC_Asc.js?v=20220126")%>"></script>

    </div>

</asp:Content>