<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    AutoEventWireup="true" CodeBehind="GC_Index.aspx.cs" Inherits="SIANWEB.GS.GS_Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<%--    
    21DIC-2020 RFH    
--%>

<!-- ALERTIFY 0.3.11 NUEVO -->
    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    

    <!-- JQUERY -->
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        

    
<%--BOOTSTRAP bootstrap--%>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">    
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script> 

    
    <%-- ZEBRA DATEPICKER --%>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    

<%-- FONT AWESOME --%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">    
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    


<div class="container-fluid">    

<div class="row mt5">
    <div class="col-md-1" style="vertical-align:middle;" >
            <label>Rik</label>
    </div>
    <div class="col-md-3">
        <select id="ddlRepresentante" class="form-control" style="width:100%;">
        </select>
    </div>

    <div class="col-md-1 text-left;">
        <button id="btnCargarListado" type="button" class="btn btn-primary btn-sm" onclick="btnCargarAgenda();">
                    <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                </button>
        </div>
    </div>   
</div>


<div class="row mt5" style="margin-bottom:20px;">
    <div class="col-md-12">
        <table style="width:98%" >
            <tr>
                <td style="width: 260px; vertical-align:top">
                    <div id="container" style="width: 110px; position:relative; ">
                        <input type="text" id="tbFecha" class="form-control datepicker wfecha" value="" style="display:block; margin:5px; " >                                                            
                    </div>        
                </td>
                <td style="vertical-align:top">
                    <table class="table table-hover table-bordered RadGrid_Outlook" id="tblCrmVisita" >
                        <thead>
                            <tr>
                                <th style="width:40px; text-align:center;">Id</th>                    
                                <th style="text-align:center;">Fecha</th>                    
                                <th style="width:70px; text-align:center;">Motivo</th>
                                <th style="text-align:center;">Cliente</th>
                                <th style="text-align:center;">VP</th>                    
                                <th style="text-align:center;">Comentarios</th>                    
                                <th style="text-align:center;">Estatus</th>                                        
                                <th style="width:40px; text-align:center;"></th>                                        
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                   </table>         

                     <ul class="pagination" id="PaginacionPie" style="margin:0px!important; display:none;" >
                       <li><a href = "#">&laquo;</a></li>
                       <li class = "active"><a href = "#">1</a></li>
                       <li><a href = "#">2</a></li>           
                       <li><a href = "#">&raquo;</a></li>
                    </ul>

                </td>
            </tr>
        </table>


    </div>
               
</div>

    <script type="text/javascript">
        document.title = 'Gestión Comercial';
        var _ApplicationUrl = '<%=ApplicationUrl %>';
         var _Usuario_Tipo = "<%= Usuario_Tipo %>";
         var hfId_Rik = '<%=Id_Rik %>';
         var Id_TU = '<%=Id_TU1 %>';
         var hfId_CD = '<%=Id_CD %>';
         var Id_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
        var Id_U = '<%=Id_U %>';
    </script>

    <script src="<%=Page.ResolveUrl("~/js/Acys/Paginacion.js?v=20210408")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20210408")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/GC/GC.js?v=20210408")%>"></script>

</asp:Content>