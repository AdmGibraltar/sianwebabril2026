<%@ Page Language="C#" AutoEventWireup="true" 
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    CodeBehind="Report_UtilidadPrima.aspx.cs" 
    Inherits="SIANWEB.Report_UtilidadPrima" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<%--
    
    10Nar2022 RFH    
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

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">

      <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/xlsxl.min.js")%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#chbTipo').click(function () {
                var chbPorFechas = $('#chbTipo').is(':checked');
                var foo = document.getElementById('CPH_rowTipo');
                document.getElementById("cmbTipoCuenta").value = "-1";
                if (chbPorFechas) {
                    foo.style.display = 'block';
                } else {
                    foo.style.display = 'none';
                }
            });
            $('#chbCliente').click(function () {
                var chbPorFechas = $('#chbCliente').is(':checked');
                var foo = document.getElementById('CPH_rowCliente');
                document.getElementById("tbNumeroCliente").value = '';
                document.getElementById("tbNombreCliente").value = '';
                if (chbPorFechas) {
                    foo.style.display = 'block';
                } else {
                    foo.style.display = 'none';
                }
            });
        });
    </script>
<div class="container-fluid">    

<div class="row mt5">
    <div class="col-md-12" style="vertical-align:middle;" >            
        <table style="width:100%;" > 
            <tr>  
                <td style="width:50px;">Año</td>
                <td style="width:100px;">
                    <input type="text" class="form-control" id="tbAnio" style="width:100px;" /> 
                </td>
                <td style="width:50px;">Mes</td>
                <td style="width:100px;">
                    <select id="cmbMes" class="form-control" style="width:150px;" >
                        <option value="1" data-mes="1">Enero</option>
                        <option value="2" data-mes="2">Febrero</option>
                        <option value="3" data-mes="3">Marzo</option>
                        <option value="4" data-mes="4">Abril</option>
                        <option value="5" data-mes="5">Mayo</option>
                        <option value="6" data-mes="6">Junio</option>
                        <option value="7" data-mes="7">Julio</option>
                        <option value="8" data-mes="8">Agosto</option>
                        <option value="9" data-mes="9">Septiembre</option>
                        <option value="10" data-mes="10">Octubre</option>
                        <option value="11" data-mes="11">Noviembre</option>
                        <option value="12" data-mes="12">Diciembre</option>
                    </select>
                </td>
                 
               
                <td style="width:50px; display:none;" >Buscar</td>
                <td style="width:150px; display:none;">
                    <input type="text" class="form-control" id="tbTextoBuscar" style="width:150px;"/>
                </td>
                <td style="width:50px;">
                    <button id="btnCargarListado" type="button" class="btn btn-primary btn-sm" title="" 
                        onclick="Reporte.btnAplicar_Click();">
                                <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                    </button>
                </td>
                <td>
                    <img id="spinner_GCIndice" style="display:none; margin-top:5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />                    
                </td>
                <td>
                 &nbsp;
                </td>
                <td style="width:100px;" >
                    <button type="button" class="btn btn-default btn-sm " 
                        onclick="Reporte.btnExportaExcel_Click();">
                                <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                    </button>
                </td>

            </tr>
        </table>
      

<div class="row mt5" style="margin-bottom:20px;">
    <div class="col-md-12 text-center">
        
    <table class="table table-hover table-bordered RadGrid_Outlook" id="tblReporteDinamico">
        <thead>
            <tr>                
                <th class="text-center" style="width:150px; text-align:center; display:none;">Priorización</th>

                <th class="text-center" style="width: 50px;">
                    <label class="LinkCte"
                        title="Tipo de documento"
                        id="ColumnaOrden_122"
                        data-col_id="12"
                        data-orden="12"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Tipo de documento</label>
                </th>

              <th class="text-center" style="width: 50px;">
                   <label class="LinkCte"
                       title="# Producto"
                       id="ColumnaOrden_123"
                       data-col_id="12"
                       data-orden="12"
                       data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                       # Producto</label>
               </th>
               <th class="text-center" style="width: 50px;">
                   <label class="LinkCte"
                       title="Producto"
                       id="ColumnaOrden_125"
                       data-col_id="12"
                       data-orden="12"
                       data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                       Producto</label>
               </th>

                <th class="text-center" style="width: 100px;">
                    <label class="LinkCte"
                        title="Factura"
                        id="ColumnaOrden_10"
                        data-col_id="10"
                        data-orden="10"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Factura</label>
                </th>


<%--                <th class="text-center">
                    <label class="LinkCte"
                        title="Mes"
                        id="ColumnaOrden_11"
                        data-col_id="11"
                        data-orden="11" data-dir="0" onclick="Reporte.PreCargaIndice(this);">Mes</label>                
                </th>   
                
                
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    title="Año"
                    id="ColumnaOrden_9"
                    data-col_id="9" 
                    data-orden="9" 
                    data-dir="0" 
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Año</label>
                </th>  --%>

                <%--Tipo Calculado--%>
<%--                <th class="text-center rd_col_100">
                    <label class="LinkCte" 
                    id="ColumnaOrden_2"
                    data-col_id="2" 
                    data-orden="2" 
                    data-dir="0" 
                    title="Tipo Calculado"                    
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Tipo Calculado</label>
                </th>   --%>

                <%--venta--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_3"                    
                    data-col_id="3" 
                    data-orden="3" 
                    data-dir="0" 
                    title="Venta"
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Venta</label>
                </th>   
                
                <%--costo--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_4"
                    title="Costo"
                    data-col_id="4" 
                    data-orden="4" 
                    data-dir="0" 
                    onclick="Reporte.PreCargaIndice(this);">
                       Costo
                    </label>
                </th>      
                <%--Utilidad Bruta--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_5"                    
                        data-col_id="5" 
                        data-orden="5" 
                        data-dir="0" 
                        title="Utilidad Bruta"
                        onclick="Reporte.PreCargaIndice(this);">
                        Utilidad Bruta
                    </label>
                </th> 
                <%--porcUBReal--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_6"
                        data-col_id="6" 
                        data-orden="6" 
                        data-dir="0"                     
                    title="Porcentaje UB Real"
                        onclick="Reporte.PreCargaIndice(this);">
                        % UB Real
                    </label>
                </th>     
                <%--porcUBPlaneada--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_7"
                    title="Porcentaje UB Planeada"
                        data-col_id="7" data-orden="7" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                    % UB Planeada
                    </label>                    
                </th>  
                <%--varianzaUBrutaPuntos--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_8"
                    title="Varianza U Bruta Puntos"
                        data-col_id="8" data-orden="8" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Varianza U Bruta Puntos
                    </label>                                        
                </th>    
                     <%--Impacto Pesos--%>
                <th class="text-center rd_col_100">
                 <label class="LinkCte"
                 id="ColumnaOrden_9"
                 title="Impacto Pesos"
                     data-col_id="9" data-orden="9" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                     Impacto Pesos
                 </label>                                        
                 </th>  
                <%--Fecha Creación--%>
<%--                 <th class="text-center rd_col_100">
                 <label class="LinkCte"
                 id="ColumnaOrden_10"
                 title="Fecha Creación"
                     data-col_id="10" data-orden="9" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                     Fecha Creación
                 </label>                                        
                 </th> --%>                                                   
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

    <table style="width:98%; display:none;">
        <tr>
            <td style="width: 260px; vertical-align:top; display:none;">
                <div id="container" style="width: 110px; position:relative; ">
                    <input type="text" id="tbFecha" class="form-control datepicker wfecha" value="" style="display:block; margin:5px; " >                                                            
                </div>        
            </td>
            <td style="vertical-align:top"></td>                
        </tr>
        <tr>
            <td class="text-center" align="center"></td>
        </tr>
    </table>

    </div>    
                          


</div>

<script type="text/javascript">
    document.title = 'Cumplimiento de Venta Instalada';
    var _ApplicationUrl = '<%=ApplicationUrl %>';
    var _Usuario_Tipo = "<%= Usuario_Tipo %>";
    var hfId_Rik = '<%=Id_Rik %>';
    var Id_TU = '<%=Id_TU %>';
         var Id_CD = '<%=Id_CD %>';
         var Id_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
    var Id_U = '<%=Id_U %>';
    var FechaCalenda = '<%=Fecha %>';
    // 1OCT-2021 RFH
    var GLOBAL_Activo_AcysCuentasNacionales = 0;
</script>

<%--  <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_config.js?v=20220126")%>"></script>--%>
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20220126")%>"></script>
<%--    <script src="<%=Page.ResolveUrl("~/js/Login.js?v=20220126")%>"></script>   --%> 
    <script src="<%=Page.ResolveUrl("~/js/Paginacion.js?v=20220126")%>"></script>    
<%--    <script src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ClienteDet_ajax.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Cliente.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_ajax.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_main.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Func.js?v=20220126")%>"></script>   --%>
    <script src="<%=Page.ResolveUrl("~/js/reports/Report_UtilidadPrima.js?v=1")%>"></script>

    </div>
</asp:Content>