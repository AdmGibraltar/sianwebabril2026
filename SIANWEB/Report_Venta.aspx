<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Venta.aspx.cs" 
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    Inherits="SIANWEB.Report_Venta" %>

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
    <style>

     .table-container2 {
    overflow-x: auto; /* Permite el scroll horizontal */
    max-width: 100%; /* Ajusta el ancho del contenedor según sea necesario */
}

.table2 {
    width: 100%; /* O establece un ancho fijo según tus necesidades */
    border-collapse: collapse; /* Quita el espacio entre bordes */
}
    </style>

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
    <div class="col-md-12 " style="vertical-align:middle;" >            
        <table style="width:100%;" class="" > 
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
    <div class="col-md-12 text-center table-container2">
        
    <table class="table table-hover table-bordered table-scroll table2" id="tblReporteDinamico">
        <thead>
            <tr>                
                <th class="text-center" style="width:150px; text-align:center; display:none;">Priorización</th>
                <th class="text-center" style="width: 50px;">
                    <label class="LinkCte"
                        title="#"
                        id="ColumnaOrden_132"
                        data-col_id="12"
                        data-orden="12"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        #</label>
                </th>
                <th class="text-center" style="width: 50px;">
                    <label class="LinkCte"
                        title="Tipo"
                        id="ColumnaOrden_122"
                        data-col_id="12"
                        data-orden="12"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Tipo</label>
                </th>
                <th class="text-center" style="width: 50px;">
                    <label class="LinkCte"
                        title="Folio Fiscal/UUID"
                        id="ColumnaOrden_132"
                        data-col_id="12"
                        data-orden="12"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Folio Fiscal/UUID</label>
                </th>
                 
                 <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Número de Documento"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                         Número de Documento</label>
                 </th>
                 
          <%--       <th class="text-center" style="width: 100px;">
                    <label class="LinkCte"
                        title="Estatus Fiscal"
                        id="ColumnaOrden_10"
                        data-col_id="10"
                        data-orden="10"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Estatus Fiscal </label>
                </th> --%>
                <th class="text-center" style="width: 100px;">
                    <label class="LinkCte"
                        title="Fecha Cancelación"
                        id="ColumnaOrden_10"
                        data-col_id="10"
                        data-orden="10"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Fecha Cancelación </label>
                </th> 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Estatus del Documento"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                           Estatus del Documento  </label>
                 </th> 
                
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="  Código de Cliente/Proveedor     "
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                           Código de Cliente/Proveedor  </label>
                 </th>


                
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title=" Cliente/Proveedor"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                           Cliente/Proveedor </label>
                 </th>     

           
                 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Grupo  "
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                           Grupo  </label>
                 </th>     

               <th class="text-center" style="width: 100px;">
                   <label class="LinkCte"
                       title="# Territorio"
                       id="ColumnaOrden_130"
                       data-col_id="10"
                       data-orden="10"
                       data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                         # Territorio  </label>
               </th>   
                 
               <th class="text-center" style="width: 100px;">
                    <label class="LinkCte"
                        title="Territorio"
                        id="ColumnaOrden_1550"
                        data-col_id="10"
                        data-orden="10"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                         Territorio  </label>
                </th> 
                
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Fecha de Contabilización"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                           Fecha de Contabilización  </label>
                 </th>     
                 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Total del Documento"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Total del Documento </label>
                 </th>    
                 <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Impuesto Total"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Impuesto Total </label>
                 </th>
                 <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="SubTotal"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          SubTotal </label>
                 </th> 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Código de Producto"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Código de Producto </label>
                 </th> 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Descripción"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Descripción </label>
                 </th> 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Cantidad"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Cantidad </label>
                 </th> 
                <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Precio"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Precio </label>
                 </th> 
                 <th class="text-center" style="width: 100px;">
                     <label class="LinkCte"
                         title="Line Total"
                         id="ColumnaOrden_10"
                         data-col_id="10"
                         data-orden="10"
                         data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                          Line Total </label>
                 </th> 
                
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

    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Paginacion.js?v=20220126")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/reports/Report_Venta.js?v=2")%>"></script>

    </div>
</asp:Content>
