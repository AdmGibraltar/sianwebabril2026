<%@ Page Language="C#" 
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    AutoEventWireup="true" 
    CodeBehind="GC_RepCumplimientoVI_index.aspx.cs" 
    Inherits="SIANWEB.GC.GC_RepCumplimientoVI_index" %>

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
                 
                <td style="width:50px;">Rol</td>
                   <td style="width:100px;">
                        <select id="CmbRol" class="form-control" style="width:150px;" >
                        <option value="0" data-rol="0">--Todos--</option>
                        <option value="1" data-rol="1">RSC</option>
                        <option value="2" data-rol="2">ASC</option>
                        <option value="3" data-rol="3">RIK</option> 
                    </select> 
                   </td>
                <td style="width:150px;">
                    <select id="ddlRepresentante" class="form-control" style="width:200px;">
                    </select>            
                </td>
                <td style="width:50px;">Territorios</td> 
                <td style="width:150px;">
                    <select id="ddlTerritorio" class="form-control" style="width:200px;">
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
                    <button type="button" class="btn btn-default btn-sm hidden" 
                        onclick="Reporte.Exportar_Excel();">
                                <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                            </button>
                                       <div class="dropdown inline"  >
                                          <button 
                                            style="height:29px;"
                                            class="btn btn-default w100" 
                                            type="button" id="dropdownMenu_PropTecnoE" 
                                            data-toggle="dropdown" 
                                            aria-haspopup="true" 
                                            aria-expanded="true">
                                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte&nbsp;
                                          </button>
                                          <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="cursor:pointer;" >                                
                                            <li><a data-desglosado="0" onclick="Reporte.btnExportaExcel_Click(this);" >No Desglosado</a></li>
                                            <li><a data-desglosado="1" onclick="Reporte.btnExportaExcel_Click(this);" >Desglosado</a></li>
                                          </ul>
                                        </div>

                </td>

            </tr>
        </table>
        <table style="width: 20%;">
            <tr>
                <td style="vertical-align: middle;">Por Cliente</td>
                <td style="vertical-align: middle;">Por Tipo de Cliente</td>
            </tr>
            <tr>
                <td style="vertical-align: middle;">
                    <input type="checkbox" id="chbCliente" />
                </td>
                <td style="vertical-align: middle;">
                    <input type="checkbox" id="chbTipo" />
                </td>
            </tr>
        </table>


        <div class="row" id="rowCliente" runat="server" style="margin-top:5px; display:none;">
        <table>
            <tr>
                <td style="vertical-align: middle; width: 100px;">Cliente: &nbsp;</td>
                <td>
                    <input type="text" id="tbNumeroCliente" class="form-control" style="width: 100px;" /></td>
                <td id="ContenedorCliente"></td>
                <td>
                    <input type="text" id="tbNombreCliente" class="form-control" style="width: 300px;" /></td>

            </tr>
               </table>
    </div>
        
<div class="row" id="rowTipo" runat="server" style="margin-top:5px; display:none;">
        <table>
            <tr> 
                    <td style="width: 100px;">Tipo de Cliente</td>
                    <td style="width: 100px;">
                        <select id="cmbTipoCuenta" class="form-control" style="width: 150px;">
                            <option value="-1" data-tcuenta="-1">--Todos--</option>
                            <option value="0" data-tcuenta="0">Local</option>
                            <option value="1" data-tcuenta="1">Cuenta Nacional</option>
                            <option value="2" data-tcuenta="2">Cuenta Cordinada</option>
                        </select>
                    </td> 
            </tr>
        </table>
    </div>
    </div>
</div>

<div class="row mt5" style="margin-bottom:20px;">
    <div class="col-md-12 text-center">
        
    <table class="table table-hover table-bordered RadGrid_Outlook" id="tblReporteDinamico">
        <thead>
            <tr>                
                <th class="text-center" style="width:150px; text-align:center; display:none;">Priorización</th>

                <th class="text-center" style="width: 50px;">
                    <label class="LinkCte"
                        title="Tipo de Cliente"
                        id="ColumnaOrden_12"
                        data-col_id="12"
                        data-orden="12"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Tipo de Cliente</label>
                </th>

                <th class="text-center" style="width: 100px;">
                    <label class="LinkCte"
                        title="Número de Cliente / Número de Producto"
                        id="ColumnaOrden_10"
                        data-col_id="10"
                        data-orden="10"
                        data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                        Núm Cte<br>
                        No. Prod.</label>
                </th>


                <th class="text-center">
                    <label class="LinkCte"
                        title="Nombre del Cliente / Descripción de Producto"
                        id="ColumnaOrden_11"
                        data-col_id="11"
                        data-orden="11" data-dir="0" onclick="Reporte.PreCargaIndice(this);">Cliente<br>Descripción</label>                
                </th>   
                
                <!--th class="text-center rd_col_100">
                    <label class="LinkCte" 
                    id="ColumnaOrden_1"
                    data-col_id="1" data-orden="1" data-dir="0" onclick="Reporte.PreCargaIndice(this);" 
                    >Venta Instalada Consolidada</label></!th-->  

                <%--VtaMesTot--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    title="Venta facturada del total de productos"
                    id="ColumnaOrden_9"
                    data-col_id="9" 
                    data-orden="9" 
                    data-dir="0" 
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Venta Total del mes</label>
                </th>  

                <%--VtaMes--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte" 
                    id="ColumnaOrden_2"
                    data-col_id="2" 
                    data-orden="2" 
                    data-dir="0" 
                    title="Venta del Mes ACyS"                    
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Venta del Mes (Acys)</label>
                </th>   

                <%--VtaInst--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_3"                    
                    data-col_id="3" 
                    data-orden="3" 
                    data-dir="0" 
                    title="Venta estimada mensual de los productos del ACyS"
                    onclick="Reporte.PreCargaIndice(this);" 
                    >Vta Instalada (Acys)</label>
                </th>   
                
                <%--MESVI--%>
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_4"
                    title="Venta mensual facturada de los productos del ACyS vs. Venta mensual estimada de los productos"
                    data-col_id="4" 
                    data-orden="4" 
                    data-dir="0" 
                    onclick="Reporte.PreCargaIndice(this);">
                        Vta del mes vs VI
                    </label>
                </th>                                    
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_5"                    
                        data-col_id="5" 
                        data-orden="5" 
                        data-dir="0" 
                        title="% Cumplimiento Venta Instalada en el Mes"
                        onclick="Reporte.PreCargaIndice(this);">
                        % Cumplimiento VI en el mes
                    </label>
                </th>                    
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_6"
                        data-col_id="6" 
                        data-orden="6" 
                        data-dir="0"                     
                    title="Venta Promedio Trimestre Anterior"
                        onclick="Reporte.PreCargaIndice(this);">
                        Venta Prom. Trim. Ant.
                    </label>
                </th>                    
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_7"
                    title="Promedio Trimestre Anterior Vs Venta Instalada"
                        data-col_id="7" data-orden="7" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                    Prom. trim .ant vs VI
                    </label>                    
                </th>                    
                <th class="text-center rd_col_100">
                    <label class="LinkCte"
                    id="ColumnaOrden_8"
                    title="% Cumplimiento Acys Vs Promedio Trimestre Anterior"
                        data-col_id="8" data-orden="8" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                    % Cump. ACYS vs Prom. trim ant.
                    </label>                                        
                    </th>    
                
                <%--Venta total del mes--%>

                <!--th class="text-center rd_col_100" >
                    <label class="LinkCte"
                    id="ColumnaOrden_9"
                    data-col_id="9" data-orden="9" data-dir="0" onclick="Reporte.PreCargaIndice(this);">
                    
                    Productos Acys y Vta Esporádica
                    </label>           
                </th-->     

                <th class="text-center" style="width:50px;">                   
                    Acys                   
                    </th>                                                    
            </tr>
            <tr>
                 <td></td>
                 <td></td>
                 <td class="text-right">
                     <label>Totales:</label>
                 </td>
                 <td>
                     <label id="lbTotalCol8" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol2" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol3" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol4" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol5" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol9" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol6" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol7" ></label>                 
                 </td>                 
                 <td></td>                 
             </tr>
        </thead>
        <tbody>
        </tbody>
        <%--<tfoot>
             <tr>
                 <td></td>
                 <td class="text-right">
                     <label>Totales:</label>
                 </td>
                 <td>
                     <label id="lbTotalCol8" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol2" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol3" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol4" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol5" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol9" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol6" ></label>                 
                 </td>
                 <td>
                     <label id="lbTotalCol7" ></label>                 
                 </td>                 
                 <td></td>                 
             </tr>
        </tfoot>--%>
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
                          
<!--Modal -->                          
<div class="modal fade" id="dvDialogoInicioSesion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button id="btndvDialogoInicioSesionCerrar" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H3">
                    Iniciar sesion
                </h4>
            </div>
            <div class="modal-body">                                    
                <form action="" id="frmDvDialogoInicioSesion">
                <div class="form-group">
                    <label for="Cu_User">
                        Usuario
                    </label>
                    <input type="text" id="Username" name="Username" class="form-control" />
                </div>
                <div class="form-group">
                    <label for="Cu_pass">
                        Contraseña
                    </label>
                    <input type="password" id="Password" name="Password" class="form-control" />
                </div>
                </form>
                <div id="wrnDvDialogoInicioSesion" class="alert alert-warning" style="display: none;">
                    <span class="pficon pficon-warning-triangle-o"></span>
                    <div id="msgWrnDvDialogoInicioSesion">
                        Mensaje
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnDvDialogoInicioSesionCerrar" type="button" class="btn btn-default"
                    onclick="redireccionarALogin()" data-dismiss="modal">
                    Cerrar</button>
                <button type="button" class="btn btn-primary" id="btnDvDialogoInicioSesionLogin"
                    onclick="login_ajax(jQuery)">
                    Confirmar
                </button>
            </div>
        </div>
    </div>
</div>
                        
<!--Modal -->                          
<div id="modalListadoAcys" class="modal fade" data-keyboard="false" tabindex="-1" role="dialog" data-backdrop="static"  aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button id="btndvDialogoInicioSesionCerrar" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H3">
                    Seleccione el Acuerdo
                </h4>
            </div>
            <div class="modal-body">                                    

                <table id="tblAcuerdos" class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <td class="text-center">Folio</td>
                            <td class="text-center">Estatus</td>
                            <td class="text-center">Territorio</td>
                            <td class="text-center">Rik</td>
                            <td class="text-center">Fecha Fin</td>
                            <td></td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                
            </div>
            <div class="modal-footer">
                <button id="btnListadoAcys_Cerrar" type="button" class="btn btn-default" data-dismiss="modal">Cerrar
                </button>                
            </div>
        </div>
    </div>
</div>

<!--Modal -->                          
<div id="modalListadoUsuarios" class="modal fade" data-keyboard="false" tabindex="-1" role="dialog" data-backdrop="static"  aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H3">Seleccione el usuario para asignar el Acys</h4>
            </div>
            <div class="modal-body">                                    

                <table id="tblSeleccionUsuarioBuscar">                    
                    <tbody>
                        <td>Texto</td>
                        <td>
                            <input id="tbBuscarRepresentante" type="text" class="form-control" value="" placeholder="Nombre del representante..." />
                        </td>
                        <td>
                            <button type="button" class="btn btn-primary" onclick="Reporte.btnBuscarRepresentante();">Buscar</button>                
                        </td>
                    </tbody>
                </table>

                <table id="tblSeleccionUsuario" class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <td class="text-center">Rik</td>
                            <td class="text-center">Nombre</td>    
                            <td class="text-center">Territorio</td>    
                            <td></td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar
                </button>                
            </div>
        </div>
    </div>
</div>

<!--Modal -->                          
<div id="modalAcys" data-backdrop="static" data-keyboard="false" class="modal" role="dialog" tabindex="-1" style="z-index:1010!important;" >
    <div class="modal-dialog" role="document" style="width:950px;">
        <div class="modal-content">
            <div class="modal-header">                
                <table style="width:100%;">
                <tr>
                    <td>                        
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <h4 id="h1">Acuerdo Comercial y de Servicios (ACyS)</h4>
                                </td>
                                <td style="width:50px;">
                                    <img id="spinnerAcysTitle" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                </td>
                            </tr>
                        </table>
                    </td>                    
                    <td style="width:1px;">                        
                        <button id="MensajeCargando" class="btn btn-success btn-xs blink_text">Cargando...</button>
                    </td>
                    <td style="width:30px;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </td>                
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div2">

                    <table style="width:100%">
                    <tr> 
                        <td style="width:100px;" class="text-center">Folio</td>                                            
                        <td style="width:100px;" class="text-center">Fecha</td>                                            
                        <td style="width:150px;" class="text-center">Fecha Inicio</td>
                        <td style="width:150px;" class="text-center">Fecha Fin</td>
                        <td class="text-center">
                            <input id="hfAcysCNTipo" type="hidden" value="0"></input>
                            <label id="lbAcysCNTipo"></label>
                        </td>
                        <td style="width:100px;" class="text-center">Estatus</td>
                    </tr>
                    <tr>
                        <td>     
                            <input type="hidden" id="hfId_Acs" value="0" />
                            <input type="hidden" id="hfAcs_Version" value="0" />
                            <input type="hidden" id="hfVencido" value="0" />
                            <input type="hidden" id="hfId_Cte" value="0" />
                            <input type="hidden" id="hfId_Ter" value="0" />
                            <input type="hidden" id="hfAcs_Procedencia" value="0" />
                            <input type="hidden" id="hfReporteVI" value="1" />
                            <h4 id="tbId_Acs"></h4>                                            
                        </td>
                        <td>                            
                            <input type="text" id="tbAcs_Fecha" class="form-control datepicker wfecha" />                        
                        </td>                                                                                    
                        <td>                            
                            <input type="text" id="tbAcs_FechaInicio" class="form-control datepicker wfecha" />                        
                        </td>                                                                                    
                        <td>                        
                            <input type="text" id="tbAcs_FechaFin" class="form-control datepicker wfecha" />                                                    
                        </td>
                        <td class="text-center">
                            <div id="divAcysCN"></div>                            
                        </td>
                        <td style="width:150px;" class="text-center">
                            <span style="font-size:1em;" id="lbAcysEstatus" class="label label-warning">
                            </span>                            
                        </td>                                        
                    </tr>
                    </table>

                    <ul class="nav nav-tabs" id="tabPage">
                        <li class="active">
                            <a href="#tabCliente" data-toggle="tab">Cliente</a>
                        </li>
                        <li>
                            <a href="#tabAcuerdoEconomico" data-toggle="tab">Acuerdo Económico de Producto</a>
                        </li>                                    
                    </ul>
                                        
                    <div class="tab-content">
                        <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <%--TAB CLIENTE --%>
                        <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <div class="tab-pane active" id="tabCliente">                    

                        <div class="col-md-12">
                                                  
                            <div class="row mt5">
                            
                                   <div class="panel panel-default">                                      
                                    <div class="panel-heading titulo_blod">Información Fiscal/Comercial</div>
                                      <div class="panel-body">

                                      <input type="hidden" id="hfAcys_CteNumero" value="0" />

                                      <table style="width:100%">
                                        <tr>
                                            <td colspan="1">Nombre</td>
                                            <td colspan="6"><input type="text" id="tbAcys_CteNombre" class="form-control" /></td>
                                            <td colspan="1">Número</td>
                                            <td colspan="2"><input type="text" id="tbAcys_CteNumero" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Dirección</td>
                                            <td colspan="6"><input type="text" id="tbAcys_CteDireccion" class="form-control" /></td>
                                            <td colspan="1">Colonia</td>
                                            <td colspan="2"><input type="text" id="tbAcys_CteCol" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Municipio</td>
                                            <td colspan="4"><input type="text" id="tbAcys_CteMunicipio" class="form-control" /></td>
                                            <td colspan="1">C.P.</td>
                                            <td colspan="1"><input type="text" id="tbAcys_CteCP" class="form-control" /></td>                                            
                                            <td colspan="1">RFC</td>
                                            <td colspan="2"><input type="text" id="tbAcys_CteRFC" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Territorio</td>
                                            <td colspan="2">
                                                <input type="text" id="tbCteTerritorio" class="form-control" style="display:block"/>
                                                <select id="selCteTerritorio" class="form-control" style="display:none">
                                                    <option value="0"></option>                                                    
                                                </select>
                                            </td>
                                            <td colspan="1">RIK Nombre</td>
                                            <td colspan="4"><input type="text" id="tbAcys_RikMombre" class="form-control" /></td>                                            
                                            <td colspan="1">#</td>
                                            <td colspan="1"><input type="text" id="tbAcys_RikNumero" class="form-control" /></td>                                            
                                        </tr>                                        
                                        <tr>
                                            <td colspan="10">
                                                <button class="btn btn-primary btn-xs" id="btnBuscarCliente">Buscar Cliente</button>
                                            </td>
                                        </tr>
                                      </table>

                                      <div class="alert alert-danger alert-dismissible" role="alert" id="Cliente_Alert" style="display:none;" >                                            
                                            <table>
                                                <tr>
                                                    <td><strong>Error: </strong></td>
                                                    <td><div id="Cliente_Alert_Texto"></div> </td>
                                                </tr>
                                            </table>
                                      </div>

                                      </div>
                                    </div>

                                    <div class="panel panel-default">                                      
                                    <div class="panel-heading titulo_blod">Contactos del Cliente</div>
                                      <div class="panel-body">

                                      <table style="width:100%">
                                        <tr>
                                            <td colspan="1"></td>
                                            <td colspan="7" class="h_cel_center">Nombre</td>
                                            <td colspan="1" class="h_cel_center">Teléfono</td>                                            
                                            <td colspan="1" class="h_cel_center">Correo</td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Pagos</td>
                                            <td colspan="7"><input type="text" id="tbContacto1Nom" class="form-control" /></td>                                            
                                            <td colspan="1">
                                                <input type="text" id="tbContacto1Tel" class="form-control" maxlength="80"/>
                                            </td>
                                            <td colspan="1"><input type="text" id="tbContacto1Correo" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Compras</td>
                                            <td colspan="7"><input type="text" id="tbContacto2Nom" class="form-control" /></td>                                            
                                            <td colspan="1"><input type="text" id="tbContacto2Tel" class="form-control" maxlength="80"/></td>                                                                                        
                                            <td colspan="1"><input type="text" id="tbContacto2Correo" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>                            
                                            <td colspan="1">Almacén</td>                
                                            <td colspan="7"><input type="text" id="tbContacto3Nom" class="form-control" /></td>                                            
                                            <td colspan="1"><input type="text" id="tbContacto3Tel" class="form-control" maxlength="80"/></td>                                                                                        
                                            <td colspan="1"><input type="text" id="tbContacto3Correo" class="form-control" /></td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="1">Mantenimiento</td>
                                            <td colspan="7"><input type="text" id="tbContacto4Nom" class="form-control" /></td>                                            
                                            <td colspan="1"><input type="text" id="tbContacto4Tel" class="form-control" maxlength="80"/></td>                                                                                        
                                            <td colspan="1"><input type="text" id="tbContacto4Correo" class="form-control" /></td>                                            
                                        </tr>                                       
                                      </table>

                                      </div>
                                    </div>

                            </div>
                            
                            <div class="row mt5">                                                                
                                   <div class="panel panel-default">
                                      <div class="panel-heading titulo_blod">Especificaciones adicionales</div>
                                      <div class="panel-body">

                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:150px;">CDI</td>
                                                <td>                                                    
                                                    <label id="tbEspAd_CDI">No</label>
                                                </td>                                                
                                            </tr>
                                            <tr>
                                                <td>Cuenta corporativa</td>
                                                <td>                                                    
                                                    <select id="lbEspAd_CuentaCorporativa" class="form-control" style="width:70px;">
                                                        <option value="1">Si</option>
                                                        <option value="0">No</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <!--tr>
                                                <td>Nombre comercial</td>
                                                <td>
                                                    <input type="text" id="tbEspAd_NombreComer" class="form-control" style="width:100%;"/>
                                                </td>
                                            </tr-->

                                        </table>

                                      </div>
                                    </div>
                            </div>

                        </div>                          

                        </div>                       
                        <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <%--TAB ACUERDO ECONOMICO --%>
                        <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <div class="tab-pane" id="tabAcuerdoEconomico">                    

                            <div class="panel panel-default mt5">                                
                                <div class="panel-heading titulo_blod">Vigencia</div>
                                <div class="panel-body">

                                    <table>
                                        <tr>
                                            <td class="lb_z1">Vigencia a partir de</td>
                                            <td>                                                             
                                                <input id="tbAcs_VigenciaAPartir" type="text" class="form-control datepicker_sololunes wfecha">
                                            </td>
                                            <td class="lb_z1">Semana inicial</td>
                                            <td>
                                                <input id="tbAcs_Semana" type="text" class="form-control" style="width:60px;" disabled/>
                                            </td>
                                            <td class="lb_z1">                                                
                                            </td>                                            
                                        </tr>
                                    </table>                                    

                                </div>
                            </div>
                                                        
                            <div class="panel panel-default mt5">                                
                                <div class="panel-heading titulo_blod">Productos</div>
                                <div class="panel-body">
                                    <%--
                                                            
                                            CLAQUIER CAMBIO AQUI MODIFICAR EN CapAcys2_Index.aspx
                                                            
                                   --%>          
                                    
                                            <table class="table table-hover RadGrid_Gis" id="tblAcuerdoProds">
                                                <thead>
                                                    <tr>
                                                        <th style="width:5px!important;"></th>
                                                        <th class="h_cel_center" style="width:30px; display:none;" >#</th>
                                                        <th class="h_cel_center" style="width:100px;" >Código</th>
                                                        <th class="h_cel_center" style="width:120px!important">Descripción</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Pres.</th>
                                                        <th class="h_cel_center" style="width:50px!important;">Uni.</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Cantidad</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Lista</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Precio Venta</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Subtotal</th>
                                                        <th class="h_cel_center" style="width:120px!important">Frecuencia</th>
                                                        <th class="h_cel_center" style="width:100px!important">Doc. de entrega</th>
                                                        <th class="h_cel_center" style="width:70px!important;">Requiere Orden Compra</th>
                                                        <th style="width:50px!important;"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>                                       
                                                <tfoot>
                                                    <td colspan="6" align="right"><label>Suma</label></td>
                                                    <td align="right"><label id="lbAcysProductosSuma">0.00</label></td>
                                                    <td colspan="4"></td>
                                                    </tfoot>
                                            </table>  

                               <ul class="nav nav-tabs" id="tabPage" style="display:none;">
                                    <li class="active">
                                        <a href="#tabProductos_Acys" data-toggle="tab">Productos ACyS</a>
                                    </li>
                                    <li>
                                        <a href="#tabProductos_CN" data-toggle="tab">Productos Cuentas Nacionales</a>
                                    </li>                                    
                                </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tabProductos_Acys">                                                                 

                                        </div>
                                        <div class="tab-pane active" id="tabProductos_CN">   
                                                                                        
                                            <table class="table table-hover RadGrid_Gis" style="display:none;" id="tblAcuerdoProds_CN">
                                                <thead>
                                                    <tr>
                                                        <th class="h_cel_center" style="width:30px; display:none;" >#</th>
                                                        <th class="h_cel_center" style="width:100px;" >Código</th>
                                                        <th class="h_cel_center">Descripción</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Pres.</th>
                                                        <th class="h_cel_center" style="width:50px!important;">Uni.</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Cantidad</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Lista</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Precio Venta</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Subtotal</th>
                                                        <th class="h_cel_center" style="width:120px!important">Frecuencia</th>
                                                        <th class="h_cel_center" style="width:100px!important">Doc. de entrega</th>
                                                        <th class="h_cel_center" style="width:70px!important;">Requiere Orden Compra</th>
                                                        <th style="width:50px!important;"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>                                       
                                                <!--tfoot>
                                                    <td colspan="6" align="right"><label>Suma</label></td>
                                                    <td align="right"><
                                                        label id="lbAcysProductosSuma_CN">0.00</label>
                                                    </td>
                                                    <td colspan="4"></td>
                                                    </!--tfoot-->
                                            </table>  
                 
                                        </div>
                                    </div>


                                        


                                    <table>
                                    <tr>
                                        <td>
                                            <button class="btn btn-primary btn-xs" id="btnAgregarRenglon">
                                            <i class="fa fa-plus fa-lg"></i>&nbsp;Agregar producto
                                            </button>
                                        </td>
                                        <td>
                                            <button class="btn btn-primary btn-xs" id="btnUploadExcel"  >
                                               Cargar producto Excel
                                            </button>
                                        </td> 
                                        <td>
                                            <button class="btn btn-primary btn-xs" id="btnActivarVI" style="display:none;" 
                                            title="Cargar automáticamente los códigos de productos vendidos al cliente en los últimos 3 meses.">Activar V.I.
                                            </button>
                                        </td>
                                        <td style="display:none;">
                                            <p>* Teclee el número del producto seguido la tecla enter para buscar el producto.</p>
                                        </td>
                                    </tr>
                                    </table>

                                    <table style="border-collapse:separate; border-spacing:10px; width:100%;">
                                    <tr>
                                        <td style="width:50px;">
                                            <div style="width:30px; height:30px; background-color:#ffc845;">&nbsp;</div>
                                        </td>
                                        <td>
                                            <label>Productos identificados en color “amarillo” provienen de matrices centrales (solo pueden ser editados por ejecutivos de área de cuenta nacional central).</label>
                                        </td>
                                        </tr><tr>
                                        <td style="width:50px;">
                                            <div style="width:30px; height:30px; background-color:#caccd1;">&nbsp;</div>
                                        </td>
                                        <td>
                                            <label>Si la cuenta es identificada como “coordinada”, son productos identificados en color “gris”, registrados por los RIKs (permite especificar el numero de unidades, precio, documentos de entrega, etc.).</label>
                                        </td>
                                    </tr>
                                    </table>

                                </div>
                            </div>
                            
                        </div>
                        
                    </div>
                    

                </div>
            <div class="modal-footer">                

                <div class="row" id="AcysRow_MensajeError" style="display:none;">
                    <div class="col-md-12" style="text-align:left;" >
                    <div class="alert alert-warning alert-dismissible" role="alert">
                        <button type="button" class="close" onclick="$('#AcysRow_MensajeError').css('display','none');"  aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>   
                        <div id="AcysRow_MensajeTexto"></div>                                             
                    </div>                        
                    </div>
                </div>
                                
                <div class="row">
                    <div class="col-md-12 ">

                        <table>
                            <tr>
                                <td>
                                    <button class="btn btn-default" id="btnReducirVentana" style="display:none;">
                                    <i class="fa fa-expand" aria-hidden="true"></i>&nbsp;Reducir
                                    </button>                                    
                                </td>
                                <td>
                                    <button class="btn btn-default" id="btnExpandirVentana" style="display:block;">
                                    <i class="fa fa-expand" aria-hidden="true"></i>&nbsp;Expandir
                                    </button>
                                </td>
                                <td>
                                    <button class="btn btn-default" id="btnAcys_Cancelar">Cancelar</button>            
                                </td>
                                <td>
                                    <button class="btn btn-primary" id="btnAcys_Guardar">Guardar</button>
                                </td>
                                <td>
                                    <button class="btn btn-primary btn_ControlOrden" 
                                    id="btnEjecutarAutorizacion_Gerente" 
                                    onclick="btnAutorizarAcys_Gerente();" 
                                    style="display:none;">Autorizar ACyS</button>
                                </td>
                                <td>
                                    <button 
                                    id="btnEjecutarRechazar_Gerente" 
                                    class="btn btn-danger" 
                                    onClick = "btnRechazarAcys_Gerente();"
                                    style="display:none;">Rechazar
                                    </button>
                                </td>
                                <td>
                                    <img id="spinnerAcysGuardando" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 ">
                        <div class="panel panel-default mt5">                                
                         <div class="panel-heading titulo_blod">Logs</div>
                            <div class="panel-body">
                            <table id="tblAcysLogs">
                                <tbody>
                                    <tr><td>Vacio...</td></tr>
                                </tbody>
                            </table>            
                            </div>    
                        </div>                                                                       
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

<!--Modal -->
<div id="modalBuscarCliente" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                    <span aria-hidden="true">&times;</span>
                </button>                
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h2">Buscar cliente</h4></td>
                    <td></td>
                    <td>                        
                        <button id="Button3" class="btn btn-success btn-xs blink_text" style="display:none;" >Cargando...</button>                        
                    </td>                    
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div3">

                    <table style="width:100%;" >                        
                        <tr>
                            <td style="width:60px;">Buscar:</td>
                            <td style="width:200px;">
                                <input type="text" id="tbBuscarCliente_Texto" class="form-control" placeholder=""/>
                            </td>                                                        
                            <td style="width:50px;">                                
                                <button id="btnBuscarCliente_Buscar" class="btn btn-primary w100">
                                    <i class="fa fa-check clickable"></i>     
                                </button>
                            </td>
                            <td>
                                <img id="spinnerBuscandoCliente" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                            </td>
                        </tr>
                    </table>

                    <table id="tbBuscarCliente_Listado" class="table-bordered table-hover" style="width:100%;">
                        <thead>
                            <tr>
                                <td style="width:80px;">#</td>
                                <td style="width:100px;">RFC</td>
                                <td>Nombre</td>
                                <td>Territorio</td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>                            
                        </tbody>                        
                    </table>                    

                    <label id="lbBuscarCliente_RegEncontrados" > </label>
                                        
                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">

                        <button class="btn btn-default" id="modalBucarCliente_Cancelar">Cancelar</button>            

                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

    <!--Modal Carga Excel Productos-->
<div class="modal fade" id="modalCargaExcel" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:400px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="iconbtnCloseCargaExcel" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H6">
                    Cargar documento
                </h4>
            </div>
            <div class="modal-body">                                    

                <table class="center" style="width:100%;">
                <tr>
                <td>
                    <label>Descargar Documento: <a href="Files/CargaProductos.xlsx"  download ><i title="Clic para descargar"  class="ml-2 fa fa-download fa-2x" aria-hidden="true"></i></a></label>
                </td>
                </tr>
                <tr>
                <td>
                    <label>Subir Documento:</label>
                </td>
                </tr>               
                <tr>
                <td align="center">
                    <input  type="file" id="CargarExcelAcys" name="CargarExcelAcys" accept=""/>
                </td>
                </tr>               
                </table>    

            </div>
            <div class="modal-footer">
                <span style="display:none" id="loaderCargaExcelModal"><i class="fa fa-spinner fa-pulse fa-1x fa-fw"></i> Cargando...</span>

                <button id="CloseCargaExcelModal" type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar
                </button>                
                <button id="btnSubirExcel_Guarar" type="button" 
                        class="btn btn-primary"                         
                        onclick="">
                        Subir
                 </button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalGuardaProdIG" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:400px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="iconbtnCloseGuardaProdIG" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H6">
                    Agregar Producto a acys <span id="lblNoAcysProdSelect"></span>
                </h4>
            </div>
            <div class="modal-body">                                    
                <input id="IdAcsSelect" name="IdAcsSelect" class="input-sm" type="hidden" readonly />
                <input id="IdVersionSelect" name="IdVersionSelect" class="input-sm" type="hidden" readonly />
                <input id="IdTerSelect" name="IdTerSelect" class="input-sm" type="hidden" readonly />

                <table class="center" style="width:100%;">
                <tr>
                <td colspan="2">
                    <label>Al presionar Guardar estarán integrando el producto en el  ACyS </label>
                </td>
                </tr>
                <tr>
                <td>
                    <label>Código: </label>
                </td>
                    <td><input id="lblCodigoProdSelect" name="lblCodigoProdSelect"   class="input-sm" type="text" readonly /></td>
                </tr>
                <tr>
                <td style="width: 27%;">
                    <label>Precio:</label>
                </td>
                 <td>
                    <input id="lblMontoProdSelect"  name="lblMontoProdSelect"  class="input-sm" type="text" readonly />
                </td>
                </tr>
                <tr>
                <td>
                    <label>Cantidad:</label>
                </td>
                <td>
                    <input id="lblCantidadSelect"  name="lblCantidadSelect"  class="input-sm" type="text" readonly />
                </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label id="msjExistAsysDet"> </label>
                    </td>
                </tr>
                </table>    

            </div>
            <div class="modal-footer">
                <span style="display:none" id="loaderGuardaProdIG"><i class="fa fa-spinner fa-pulse fa-1x fa-fw"></i> Cargando...</span>

                <button id="CloseGuardaProdIG" type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar
                </button>                
                <button id="btnGuardaProdIG" type="button" 
                        class="btn btn-primary"                         
                        onclick="">
                        Guardar
                 </button>
            </div>
        </div>
    </div>
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
    <script src="<%=Page.ResolveUrl("~/js/GC/RepCumpVI.js?v=20220427")%>"></script>

    </div>
</asp:Content>