<%@ Page Language="C#"     
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.master"
    AutoEventWireup="true" 
    CodeBehind="CL_Autorizaciones.aspx.cs" 
    Inherits="SIANWEB.CL.CL_Autorizaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
   <%-- <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">   --%> 
       <!-- ALERTIFY 0.3.11 NUEVO -->
        <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
        
        <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
        <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        
    
        <%--BOOTSTRAP--%>
        <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>    
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">  
        <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script> 
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/bootstrap.css")%>">    

        <%-- ZEBRA DATEPICKER --%>
        <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
        <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    
        <%-- FONT AWESOME --%>
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">    

        <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
        <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>    
        <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">
        <link href="<%=Page.ResolveUrl("~/css/cl.css")%>" rel="stylesheet">

        <%--TIME PICKER--%>
        <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
        <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
        <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>

        <%--exportar excel--%>
        <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
        <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
        <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
        

        <%--<script type="text/javascript">
            function AjustarPanel() {
                let height = window.innerHeight - 150;
                var element = document.getElementById("CPH_divPrincipal");
                element.style.height = height + 'px';
                console.log('height:' + height);
            }

            document.addEventListener("DOMContentLoaded", function (event) {
                setInterval(AjustarPanel, 1000);
            });

        </script>--%>

 <%--    </telerik:RadCodeBlock>--%>

<%--    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" ScrollBars="Vertical">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>

                 <telerik:AjaxSetting AjaxControlID="btnBotonGraficas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ajaxFormPanel" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>   

        </AjaxSettings>
   </telerik:RadAjaxManager>
        
<div class="container-fluid">

<input type="hidden" id="txtCentrosDist" name="txtCentrosDist" runat="server" />
      
<div runat="server" id="divPrincipal"  style="overflow:auto; ">
    
<div class="row mt5">

 <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1"
                                    Width="1200px" Height="50px" HorizontalAlign="NotSet">--%>

<div class="container-fluid">    
   <div class="row mt5">
    <div class="col-md-12">

        <table id="tbl1" border ="0">
            <tbody>
                <tr>
                    <td>&nbsp; <b>No. Solicitud</b></td>
                    <%--<td></td>--%>
                    <td>&nbsp; &nbsp; &nbsp; &nbsp;<b>Vencido</b></td>
                    <%--<td></td>--%>
                    <td>&nbsp;<b>Código de Producto</b></td>
                    <%--<td></td>--%>
                    <td> &nbsp; &nbsp; &nbsp;&nbsp; <b>Motivo Compra Local</b></td>
                    <%--<td></td>--%>
                    <td> &nbsp; &nbsp; &nbsp;&nbsp; <b>Proveedor Local</b></td>
                    <%--<td></td>--%>
                    <td> &nbsp; &nbsp; &nbsp;&nbsp; <b>Estatus</b></td>
                </tr>
                <tr>
        <%--             <td></td>
                     <td style="width:160px; height:80px;"> 
                         <input id="txtSucursal" class="form-control" disabled="disabled"/>
                     </td>
                    <td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                     <td>
                         <input id="txtSolicitud" class="form-control" maxlength ="9" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                     </td>
                    <%--<td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                    
                    <td style="width:160px;">                   
                        <select id="ddlVencido" class="form-control" style="width:130px;margin-left:20px;">
                        <option value="0">Todos</option>
                        <option value="1">Si</option>
                        <option value="2">No</option> 
                        </select>
                     </td>  
                    <%--<td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                    <td style="width:160px;">                     
                         <input id="txtCodProducto" class="form-control" maxlength ="15" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                     </td>
                    <%--<td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                     <td style="width:290px;">                     
                        <select id="ddlMotivo" class="form-control" style="width:290px;margin-left:20px;">
                        <option value="0">Todos</option>
                        <option value="1">Activación de código por falta de producto</option>
                        <option value="2">Código Central con Abasto Local</option> 
                        <option value="3">Solicitud del Cliente</option>  
                        <option value="4">Compra por Estrategia</option>
                        </select>
                     </td>  
                   <%-- <td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                    <td  style="width:280px;">
                       <select id="ddlProveedorLocal" class="form-control"  style="width:280px;margin-left:20px;">
                           <option value="0">Todos</option>
                        </select>
                     </td>
                    <%--<td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                     <td>
                       <select id="ddlEstatus" class="form-control"  style="width:130px;margin-left:20px;">
                            <option value="-1">Todos</option>
                            <option value="0">Solicitado</option>
                            <option value="1">Autorizado</option>
                            <option value="2">Rechazado</option>
                        </select>
                     </td>                    
                    <td>&nbsp; &nbsp;  &nbsp;                  
                    </td>
                    <td>
                         <button id="btnComprasLocales_Aplicar" type="button" class="btn btn-primary btn-sm" onclick="Indice.btn_ConsultaListado();">
                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                        </button>
                    </td>
                    <%--<td>&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; </td>--%>
                    <td>
                        <button id="btn_CLDescargarExcel" onclick="Indice.btn_DescargarResultados();"  type="button" class="btn btn-default btn-sm" style="width:100%;" >
                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>   
</div>

 <%--</telerik:RadAjaxPanel>--%>

 
<div class="row mt5">
    <div class="col-md-10">
        <table id="tbl_Listado" class="table table-hover table-bordered RadGrid_Outlook" >
        <thead>
            <tr>                                               
                <th class="text-center"  id="ColumnaOrden_1" 
                    rowspan="2" data-col_id="1" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">No. Solicitud</th>                                

                <th class="text-center"  id="ColumnaOrden_2" 
                    rowspan="2" data-col_id="2" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Sucursal</th>                                
                
                <th class="text-center"  id="ColumnaOrden_3" 
                    colspan="2" data-col_id="3" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Motivo Compra Local</th>                                

                <th class="text-center" id="ColumnaOrden_4" 
                    rowspan="2" data-col_id="4" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Fecha</th>                

                <th class="text-center"  id="ColumnaOrden_5" 
                    rowspan="2" data-col_id="5" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Código de Producto</th>                

                <th class="text-center" id="ColumnaOrden_6" 
                    rowspan="2" data-col_id="6" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Descripción de producto</th>                

                <th class="text-center"  id="ColumnaOrden_7" 
                    rowspan="2" data-col_id="7" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Vigencia</th>

                <th class="text-center" id="ColumnaOrden_8" 
                    rowspan="2" data-col_id="8" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Solicitante</th>                

                <th class="text-center" id="ColumnaOrden_9" 
                    rowspan="2" data-col_id="9" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Estatus</th>                

                <th class="text-center" id="ColumnaOrden_10"  
                    rowspan="2" data-col_id="10" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Aut.</th>

                <th class="text-center" id="ColumnaOrden_11" 
                    rowspan="2" data-col_id="11" data-orden="1" data-dir="0" onclick="Indice.btnVerDetalle(this);">Ver detalle</th>                            

                <th class="text-center" id="ColumnaOrden_12"  
                    rowspan="2" data-col_id="12" data-orden="1" data-dir="0" onclick="Indice.btnColumna_Click(this);">Editar</th>

            </tr>
                      
        </thead>
        <tbody>
        </tbody>
        </table>  
   
        <ul class="pagination" id="PaginacionPie" style="margin:0px!important;" >
           <li><a href = "#">&laquo;</a></li>
           <li class = "active"><a href = "#">1</a></li>           
           <li><a href = "#">&raquo;</a></li>
        </ul>

    </div>
</div>


<!-- Modal Detalle -->
<div id="modalDetalle"data-backdrop="static" data-keyboard="false" class="modal" role="dialog" tabindex="-1" style="z-index:1010!important;" >
    <div class="modal-dialog" role="document" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <table style="width:100%;">
                <tr>
                    <td>                        
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <h4 id="h1">Detalle Solicitud Compra Local</h4>
                                </td>
                                <td style="width:50px;">
                                    <img id="spinnerTitle" alt="" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
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
                        <td><div class="col-md-2"><b>Folio</b></div></td>
                        <td><div class="col-md-3"><b>Fecha Solicitud</b></div></td>
                        <td><div class="col-md-3"><b>Fecha Vigencia</b></div></td>
                        <td><div class="col-md-3"><b>Estatus</b></div></td>
                    </tr>
                    <tr>
                        <td style="width:100px;" class="text-left"><h3 id="lbIdComp"></h3></td>                                                                                 
                        <td style="width:150px;" class="text-center">
                            <div class="col-md-2"><input id="tbCLFechaSol" class="form-control datepicker wfecha" /></div>
                        </td>                                                                                    
                        <td style="width:150px;" class="text-center">
                            <input  id="tbCLFechaVig" class="form-control datepicker wfecha" /></td>
                        <td style="width:150px;" class="text-left">
                            <span style="font-size:1em;" id="lbCLEstatus" class="label label-warning"></span> 
                        </td>    
                    </tr>
                  </table>

                   <div class="col-md-12">
                                                  
                            <div class="row mt5">
                                   <div class="panel panel-default">                                      
                                   <div class="panel-heading titulo_blod">Datos Solicitud</div>
                                      <div class="panel-body">

                                       <div class="row mt10">
                                            <div class="col-md-2"  style="font-size:small">Sucursal</div>
                                            <div class="col-md-4">
                                                <input id="tbCLSucursal" class="form-control" />
                                            </div>
                                            <div class="col-md-2"  style="font-size:small">Solicitante</div>
                                            <div class="col-md-4">                        
                                                <input id="tbCLSolicitante" class="form-control" value="" />
                                            </div>
                                       </div>
                                       <div class="row mt10">
                                            <div class="col-md-2"  style="font-size:small">Motivo de Compra Local</div>
                                            <div class="col-md-4">
                                                <input id="tbCLMotivoCompra" class="form-control" />
                                            </div>
                                      </div>
                                      <div class="row mt10">
                                            <div class="col-md-2"  style="font-size:small">Código de Producto</div>
                                            <div class="col-md-4">
                                                <input id="tbCLId_Prd" class="form-control" />
                                            </div>
                                      </div>
                                      <div class="row mt10">
                                            <div class="col-md-2"  style="font-size:small">Descripción de Producto</div>
                                            <div class="col-md-4">
                                                <input id="tbCLPrd_Descripcion" class="form-control" />
                                            </div>
                                      </div>

                                      </div>
                                    </div>

                                   <div class="panel panel-default">  
                                   <div class="panel-heading titulo_blod">Datos Producto</div>
                                      <div class="panel-body">
                                      <div class="row mt10">
                                            <div class="col-md-2"  style="font-size:small">Código Padre</div>
                                            <div class="col-md-4">
                                                <input id="tbCLCodigoPadre" class="form-control" />
                                            </div>
                                          <div class="col-md-2" style="font-size:small">Tipo de Producto</div>
                                            <div class="col-md-4">                        
                                                <input id="tbCLTipoProd" class="form-control" value="" />
                                            </div>
                                       </div>
                                        <div class="row mt10">
                                            <div class="col-md-2" style="font-size:small">Proveedor Local</div>
                                            <div class="col-md-4">
                                                <input id="tbCLProvLocal" class="form-control" />
                                            </div>
                                            <div class="col-md-2" style="font-size:small">Aplicación</div>
                                            <div class="col-md-4">                        
                                                <input id="tbCLAplicacion" class="form-control" value="" />
                                            </div>
                                            
                                       </div>
                                       <div class="row mt10">
                                            <div class="col-md-2" style="font-size:small">Proveedor Central</div>
                                            <div class="col-md-6">
                                                <input id="tbCLProvCentral" class="form-control" />
                                            </div>
                                            
                                       </div>
                                       <div class="row mt10">
                                            <div class="col-md-2" style="font-size:small">Motivo</div>
                                            <div class="col-md-6">
                                                <input id="tbCLMotivo" class="form-control" />
                                            </div>
                                       </div>
                                       <div class="row mt10">
                                            <div class="col-md-2" style="font-size:small">Costo</div>
                                            <div class="col-md-2">
                                                <input id="tbCLCosto" class="form-control" />
                                            </div>
                                           <div class="col-md-2" style="font-size:small">Precio AAA Código Key</div>
                                            <div class="col-md-2">
                                                <input id="tbCLAAAKey" class="form-control" />
                                            </div>
                                       </div>
                                      <div class="row mt10">
                                            <div class="col-md-2" style="font-size:small">Precio AAA Código Compra Local</div>
                                            <div class="col-md-2">
                                                <input id="tbCLAAACompraLocal" class="form-control" />
                                            </div>
                                          <div class="col-md-2" style="font-size:small">Precio Lista</div>
                                            <div class="col-md-2">
                                                <input id="tbCLPrecioLista" class="form-control" />
                                            </div>
                                       </div>
                                       </div>
                                   </div>
                                  <div class="panel panel-default">                                      
                                    <div class="panel-heading titulo_blod">Clientes Exclusivos</div>
                                      <div class="panel-body">
                                          <table>
                                          <tr>
                                              <td colspan="3">
                                                 <%--CLIENTES EXCLUSIVOS --%> 
                                                <div class="tab-pane" id="ClientesExc" > 
                                                <div class="row mt5">
                                                    <div class="col-md-6">
                                                        <table id="tbl_ClientesExc" style="width: 600px;" class="table table-bordered w100">
                                                            <thead>
                                                            <tr>
                                                                <th>No. Cliente</th>
                                                                <th>Nombre Cliente Exclusivo</th>                                                                     
                                                            </tr>
                                                            </thead>
                                                            <tbody></tbody>                            
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            </td>

                                          </tr>
                                      </table>

                                      </div>
                                    </div>


                <div class="modal-footer">                
                <div class="row" id="CLRow_MensajeError" style="display:none;">
                    <div class="col-md-12" style="text-align:left;" >
                    <div class="alert alert-warning alert-dismissible" role="alert">
                        <button type="button" class="close" onclick="$('#CLRow_MensajeError').css('display','none');"  aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <div id="CLRow_MensajeTexto"></div>                                             
                    </div>                        
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 ">
                        <div class="panel panel-default mt5">                                
                         <div class="panel-heading titulo_blod">Logs</div>
                            <div class="panel-body">
                            <table id="tblCompraLocalELogs">
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
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">Cerrar</button>
                </div> 
        </div>
    </div>
</div>
    
<!--Modal Sesion -->                          
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

<!-- Modal Editar -->
<div id="modalEditar"data-backdrop="static" data-keyboard="false" class="modal" role="dialog" tabindex="-1" style="z-index:1010!important;" >
    <div class="modal-dialog" role="document" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <table style="width:100%;">
                <tr>
                    <td>                        
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <h4 id="h2">Actualizar Solicitud de Compra Local</h4>
                                </td>
                                <td style="width:50px;">
                                    <img id="spinnerTitleEdit" alt="" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                </td>
                            </tr>
                        </table>
                    </td>                    
                    <td style="width:1px;">                        
                        <button id="MensajeCargandoEdit" class="btn btn-success btn-xs blink_text">Cargando...</button>
                    </td>
                    <td style="width:30px;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </td>                
                </tr>
                </table>
            </div>
               
            <div class="modal-body" id="DivEditar">
                <table style="width:100%">
                    <tr> 
                        <%--<td style="width:100px;" class="text-left" colspan ="1">Motivo para Compra Local</td>--%>
                        <td>
                            <div class="col-md-3">Motivo para Compra Local</div>
                            <div class="col-md-5"><input  id="txtMotivoCL" class="form-control" /></div>
                        </td>
					</tr>
                    <tr>
                        <%--<td style="width:100px;" class="text-left"  colspan ="1">Código Key del Producto en Desabasto</td>  --%>                                          
						<td><div class="col-md-3">Código Key del Producto en Desabasto</div>
                            <div class="col-md-7"><input  id="txtCodigoKeyCL" class="form-control" /></div></td>
					</tr>
					<tr>
						<%--<td style="width:100px;" class="text-left"  colspan ="1">Código de Abasto del Producto</td>--%>
						<td><div class="col-md-3">Código de Abasto del Producto</div>
                            <div class="col-md-5"><input  id="txtCodigoAbastoCL" class="form-control" /></div>
                            <input  id="txtId_Comp" class="form-control" disabled="disabled" style="visibility:hidden"/>
						</td>
                    </tr>
                  </table>  

                <%--TABS --%>
                <ul class="nav nav-tabs" role="tabPage">
                    <li class="active">
                        <a href="#DatosGenerales" data-toggle="tab">Datos generales</a>                
                    </li>        
                    <li>
                        <a href="#Precios" data-toggle="tab">Precios</a>
                    </li>            
                    <li>
                        <a href="#SAT" data-toggle="tab">SAT</a>
                    </li>                        
                    <li>
                        <a href="#ClientesExclusivos" data-toggle="tab">Clientes Exclusivos</a>
                    </li>                        
                </ul>

                <div class="tab-content">
                      <%--TAB DATOS GENERALES --%>
                      <div class="tab-pane active" id="DatosGenerales">                    

                    <div class="row mt10">
                    <div class="col-md-2"  style="font-size:small">Código del producto</div>
                    <div class="col-md-2">
                        <input id="TextId_Prd" class="form-control" />
                        <input type="hidden" id="Id_Cpr" value=""/>
                    </div>
                    <div class="col-md-2" ></div>
                    <div class="col-md-2" style="font-size:small">
                        Código de abasto local                        
                    </div>
                    <div class="col-md-3">                        
                        <input id="txtCodProd" class="form-control" value="" />
                    </div>
<%--                    <div class="col-md-1 hidden">                        
                        <input id="chkActivo" class="form-contol" type="checkbox"  />Activo
                    </div>--%>
               </div>
                   
         <%--       <div class="row">
                    <div class="col-md-2"><label id="lbl_Val_TextId_Prd" >prueba</label></div>
                    <div class="col-md-2"><label id="lbl_Val_txtCodProd" >pruebas</label></div>
                </div>--%>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Descripción</div>
 <%--                   <div class="col-md-2 hidden">
                        <input id="chkProductoNuevo" class="form-contol" type="checkbox"  /> Producto nuevo
                    </div>--%>
                    <div class="col-md-4">                        
                        <input id="TextPrd_Descrpcion" class="form-control" value="" />
                    </div>
                    
                    <div class="col-md-2" id="col_lbVigencia" >
                        <asp:Label ID="Label889" runat="server" Text="Vigencia Inicio"/>
                    </div>
                    <div class="col-md-2" id="col_tbVigencia" >                        
                        <input id="rdpVigencia" class="form-control datepicker wfecha" value="" />
                    </div>   
                </div>

                <div class="row mt5">
                    <div class="col-md-3"><label id="lbl_Val_TextPrd_Descripcion"></label></div> 
                </div>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Presentación</div>
                    <div class="col-md-3">
                        <%--<select id="cmbPresentacion" class="form-control" style="display:none"></select>--%>
                        <input id="cmbPresentacion" class="form-control" value=""  />                    
                    </div>
                
                    <%-- RBM Vigencias  --%>   
                    <div class="col-md-1"></div>
                    
                    <div class="col-md-2" id="col_lbVigenciaFin" >
                        <asp:Label ID="lblVigenciaFin" runat="server" Text="Vigencia Fin"/>
                    </div>
                    <div class="col-md-2" id="col_tbVigenciaFin" >                        
                        <input  id="rdpVigenciaFin" class="form-control datepicker wfecha" value="" />
                    </div>                  

                </div>

                <div class="row mt5">
                    <div class="col-md-2" style="font-size:small">Tipo de producto</div>
                    <div class="col-md-1">
                        <input id="txtTipoProducto" class="form-control" value="" />
                    </div>
                    <div class="col-md-3">
                        <%--<select id="cmbTipoProducto" class="form-control w100"></select>  --%>                                              
                         <input id="cmbTipoProducto" class="form-control" value="" />                      
                    </div>
                

                        <div class="col-md-1" id="col_TipoSol" >
                             <input  id="txtIdTipoSolicitud" class="form-control" disabled="disabled" style="visibility:hidden"/>
                        </div>
                    </div>

                             
                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Aplicación de producto</div>
                    <div class="col-md-6">                        
                        <%--<select id="cmbFam" class="form-control"></select>  --%> 
                        <input id="cmbFam" class="form-control" value="" />
                    </div>
                    <div class="col-md-1" id="col_idcd">
                        <input  id="txtIdCd" class="form-control" disabled="disabled" style="visibility:hidden"/>
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Sub-familia de producto</div>
                    <div class="col-md-6">
                        <%--<select id="cmbSubFam" class="form-control"></select>--%>
                        <input id="cmbSubFam" class="form-control" value="" />
                    </div>
                    <div class="col-md-1" id="col_Cat">
                        <input  id="cmbCategorias" class="form-control" disabled="disabled" style="visibility:hidden"/>
                    </div>
                </div>

                 <div class="row mt5">
                    <div class="col-md-2" id="divProvCentral"  style="font-size:small">Proveedor Central</div>
                    <div class="col-md-6">
                    <input id="txtProveedorCentral" class="form-control" value=""  />
                    </div>
                </div>

                 <div class="row mt5">
                    <div class="col-md-2" id="divProvLocal"  style="font-size:small">Proveedor Local</div>
                    <div class="col-md-4">
                    <input id="txtProveedor" class="form-control" value=""  />
                    </div>
                </div>

                 
                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Código de producto proveedor</div>
                    <div class="col-md-4">
                    <input id="txtCodProveedor" class="form-control" value="" />
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Descripción de producto proveedor</div>
                    <div class="col-md-4">
                    <input id="txtDesProveedor" class="form-control" value="" />
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">Presentación de producto proveedor</div>
                    <div class="col-md-4">
                       <%-- <select id="cmbPresentacionProv" class="form-control"></select>--%>
                        <input id="cmbPresentacionProv" class="form-control" value="" />
                    </div>
                    <div><label id="lbl_Val_txtpresproveedor"></label> </div>
                </div>          

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">                        
                        Unidad de entrada
                    </div>
                    <div class="col-md-2">                        
                        <%--<select id="cmbUentrada" class="form-control"></select>--%>
                        <input id="cmbUentrada" class="form-control"/>
                    </div>
                       <div class="col-md-2"  style="font-size:small">                        
                        Factor de conversión
                    </div>
                    <div class="col-md-2">                        
                        <input id="txtFactorConversion" class="form-control"/>
                        
                    </div>
           
                    <div class="col-md-1">
                        <label id="lbl_Val_cmbUentrada" forecolor="Red"></label>
                    </div>                 
                </div>

                <div class="row mt5">
                    <div class="col-md-2"  style="font-size:small">
                        Unidad de salida
                    </div>
                    <div class="col-md-2">                        
                       <%-- <select id="cmbUsalida" class="form-control">
                        </select>--%>
                        <input id="cmbUsalida" class="form-control"/>
                    </div>
                    <div class="col-md-2"  style="font-size:small">
                        Unidades de empaque
                    </div>
                    <div class="col-md-2">                        
                        <input id="txtUempaque" class="form-control"/>
                        
                    </div>
                </div>
                                
                <div class="row mt5" id="row_CausaDesabasto">
                    <div class="col-md-2"  style="font-size:small">
                        Causa del desabasto
                    </div>
                    <div class="col-md-6">                                                        
                        <%--<select id="cmbCausaDesabasto" class="form-control"> </select>  --%>                                                  
                        
                        <input id="cmbCausaDesabasto" class="form-control"/>
                    </div>
                    <div class="col-md-1" style="display:none;">
                        <label id="lbl_Val_cmbMotivoDEsabasto" forecolor="Red"></label>
                    </div>
                    <div class="col-md-1" style="display:none;">  
                        <input id="txtMotivoDesabasto" class="form-contol" type="checkbox"/>
                    </div>
                </div>

                <div id="divPedidosRefAbasto" class="row mt5" style="display:none;" >
                    <div class="col-md-3">
                        Pedido Desabastecido
                        <br /><asp:Label ID="lblPedidoSeleccionado" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                        <input id="hddPedidoAbasto" type="hidden"  name="hddPedidoAbasto" runat="server" />
                        <input id="hfCont_PedidoDesabastecido" type="hidden" value="0"  />
                    </div>
                    <div class="col-md-4">

                        <div id="divSegmento_" style="float:left; width: 450px; height: 120px; overflow-y: scroll; border:1px solid #000;">                                                
                            <table class="table" id="tblPedidoDesabastecido">
                                <tbody>
                                </tbody>
                            </table>
                        </div>    
                                                
                        <table>
                            <tr>
                                <td valign="top"></td>
                                <td  colspan="3">
                                    <table border="0" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3">                                             
                                                <div id="divSegmento" style="width: 450px; height: 120px; overflow-y: scroll; display:none;">
                                                <asp:CheckBoxList runat="server" ID="chklstPedidos" AutoPostBack="false" 
                                                    RepeatColumns="3" CellSpacing="3" CellPadding="3" Width="400px"/>
                                                </div>    
                                                <input type="hidden" id="hdn1" value="yes" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style=" visibility:hidden">
                                                <asp:ListBox ID="lstbPedidos" runat="server" Width="20px" Rows="6"  visible="false" OnDblClick="JavaScript: alert('doblecklick');" ></asp:ListBox>
                                                <input name="btnAgregaPedido" value="->>" onclick="JavaScript: lstbPedidos_Click();" type="button" style="visibility: hidden" />
                                                <asp:TextBox id="txtValuesPedidos" runat="server" Width="2px" style="visibility:hidden"/><br />
                                                <input name="btnLimpiaPedidos" value="Limpiar" onclick="JavaScript: QuitabPedidos();" type="button" style="visibility: hidden" />
                                                <asp:ListBox ID="lstPedidosSeleccionados" runat="server" Width="20px" Rows="6"  name="lstPedidosSeleccionados" visible="false" > </asp:ListBox>
                                                <asp:TextBox ID="txtListadSelecionados" runat="server" Rows="6" TextMode="MultiLine" visible="false" ReadOnly="true" BorderStyle="Solid" ></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

                <div id="divMotivoClienteSolicita" class="row mt5" style="display:none;" >
                    <div class="col-md-3">
                        <asp:Label ID="Label789" runat="server" Text="Motivo por el cual el cliente solicita este producto en especial"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lbl_Val_txtMotivo" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        
                        <%--<telerik:RadTextBox Runat=server ID="txtMotivoSolicita" MaxLength="250" RenderMode="Lightweight" TextMode="MultiLine" Width="440px"  Height="70px" w ></telerik:RadTextBox>--%>
                        <textarea id="txtMotivoSolicita" class="form-control" rows="5" cols="70" style="width:100%;"></textarea>

                    </div>
                </div>
  
                <table style="font-family: vernada; font-size:8; display:none;">
                        <tr>
                            
                            <td>                              
                                <table border="0" cellpadding="1" cellspacing="1" style="display:none;">                                                                                                    
                                    <tr>
                                        <td>
                                            <label id="Label8">Sistemas propietarios</label>
                                        </td>
                                        <td colspan="3">
                                            <input id="TextId_Spo" class="form-contol" type="checkbox"/>
                                            <select id="cmbSisProp" class="form-control">
                                            </select>
                                            <label id="lbl_val_TextId_Spo" ></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label id="Label45" runat="server" Text="Agrupado de equipos de sistemas propietarios"  visible="false"></asp:Label>                                           
                                            <asp:Label id="lbl_val_txtAgrupadoSpo" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  colspan="4">
                                            <asp:Label id="Label9" runat="server" Text="Categoría de producto"  visible="false"></asp:Label>                                            
                                            <input id="txtCategoria" class="form-contol" type="text" value="" disabled="disabled"/>                                            
                                            <asp:Label id="lbl_Val_txtCategoria" runat="server" ForeColor="Red"  visible="false"></asp:Label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="2" nowrap="nowrap"></td>
                                        <td>
                                            <div style="visibility:hidden">
                                            <telerik:RadNumericTextBox ID="txtFam" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1" Enabled="false">
                                            </telerik:RadNumericTextBox> </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="2">                                         
                                        </td>
                                        <td>
                                            <div style="visibility:hidden">
                                                <telerik:RadNumericTextBox ID="txtSubFam" runat="server" Width="70px" MaxLength="9"
                                                    MinValue="1" Enabled="false">
                                                </telerik:RadNumericTextBox> 
                                            </div>                                            
                                            <label id="lbl_val_txtSubFam" ></label>
                                        </td>
                                    </tr>
                                  
                                </table>
                              
                            </td>
                        </tr>
                    </table>

            </div>

                      <%--TAB PRECIOS --%>
                    <div class="tab-pane" id="Precios"> 
                    <div class="row mt10">
                    <div class="col-md-6 text-right">

                        <table id="tbl_rgPrecios" class="table table-bordered">
                            <thead>
                                <tr> 
                                    <th class="text-center">Tipo de precio</th>
                                    <th class="text-center">Monto</th>                            
                                    <th class="text-center">Editar</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>   

                    </div>
                    <div class="col-md-4 text-right">
                        
                        <input id="rowIdModificarPrecio" type="hidden" value="" />

                        <table id="tblModificarPrecio" class="border1 w100" style="display:none;" >
                            <tr>
                                <th colspan="2" class="text-center">Editar datos del precio del producto</th>
                            </tr>
                            <tr>
                                <td>Tipo de precio</td>
                                <td class="text-center">
                                    <label id="lbModificaTipoPrecio">..</label>
                                </td>
                            </tr>
                            <tr>
                                <td>Precio</td>
                                <td>                                    
                                    <input id="tbModificaTipoPrecio_Precio" type="text" class="form-control" value="" />
                                </td>
                            </tr>
                            <tr>
                                <td>                                    
                                </td>
                                <td>
                                <table>
                                        <tr>
                                            <td>
                                                <button type="button" class="btn btn-primary" onclick="PrecioProducto.Aplicar(this);">
                                                    <i class="fa fa-check clickable"></i>    
                                                </button>                                    
                                            </td>
                                            <td>
                                                <button type="button" class="btn btn-default" onclick="PrecioProducto.Cancelar(this);">
                                                    <i class="fa fa-remove clickable"></i>
                                                </button>                                                
                                            </td>
                                        </tr>
                                    </table>                                                                                                       
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>

                      <%--SAT --%> 
                      <div class="tab-pane" id="SAT">
                <div class="row mt5"><div class="col-md-6">Unidad de Medida (SAT):</div>
           </div>
               <div class="row mt5">
                    <div class="col-md-6">                        
                        <select id="cmbUnidadMedidaSATDesabasto" class="form-control"></select>                        
                    </div>
                </div>                 
                <div class="row mt5">
                    <div class="col-md-6">
                        Producto/Servicios (SAT):
                    </div>
                </div>

                 <div class="row mt5">
                    <div class="col-md-6">                        
                        <select id="ddlProdServicio_SATDesabasto" class="form-control"></select>                        
                    </div>
                </div>
            </div>

                <%--CLIENTES EXCLUSIVOS --%> 
                <div class="tab-pane" id="ClientesExclusivos" > 
                <div class="row mt5">
                    <div class="col-md-6">
                        <table id="tbl_ClientesExclusivos" class="table table-bordered w100">
                            <thead>
                            <tr>
                                <th style="width:100px;">Tipo de Cte.</th>
                                <th style="width:100px;">No. Cte</th>
                                <th>Nombre</th>
                                <th style="width:50px;"></th>                                                                        
                            </tr>
                            </thead>
                            <tbody></tbody>                            
                        </table>
                    </div>
                </div>
                <div class="row mt5">
                    <div class="col-md-6 text-right">
                        <button id="btnAgregarClienteListado" type="button" class="btn btn-primary" title="Agregar Cliente">
                            Buscar cliente&nbsp;<i class="fa fa-search clickable"></i>     
                        </button>
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
                                    <button type="button" class="btn btn-success" data-dismiss="modal">Cancelar</button>
                                </td>
                                <td>
                                    <button class="btn btn-primary" id="btnEditarCL_Guardar"  onclick="btnEditar_Guardar();">Guardar</button>
                                </td>
                                
                                <td>
                                    <img id="spinnerCLGuardando" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
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
                            <table id="tblCompraLocalLogs">
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

<!-- MODAL BUSCAR CLIENTE -->
<div class="modal fade" id="modalBuscarCliente" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:600px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button10" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H4">
                    Búsqueda de cliente
                </h4>
            </div>
            <div class="modal-body">                                    

                <table>                    
                    <thead>
                        <tr>
                            <td>Texto</td>                        
                            <td>
                                <input id="BuscarCliente_Texto" type="text" value="" style="width:250px;" class="form-control" />
                            </td>
                            <td>
                                <button id="btnBuscarCliente_Ok" type="button" class="btn btn-primary" style="width:100px;">Buscar</button>
                            </td>                        
                            <td>                                        
                                <i id="I1" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:none; margin-top:3px;"></i>
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>                        
                </table>
                           
                <table id="tblBuscarCliente_Lista" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>RFC</th>
                            <th>Nombre</th>
                            <th>Tipo Cuenta</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <span id="BuscarCliente_Lista_RE" class="label label-primary">0 Registros</span>
                
            </div>
            <div class="modal-footer">
                <button id="Button12" type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>                
            </div>
        </div>
    </div>
</div>

</div>

<script type="text/javascript">
 
        // < --- PRODUCTIVO 
        var _ApplicationUrl = '<%= sian %>';

        // < --- PRUEBAS
        //var _ApplicationUrl = 'http://' + self.location.host;

</script>
       
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Ajax.js")%>"> </script>
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/Login.js")%>"> </script>
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Orig.js")%>"> </script>   
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Tools.js")%>"> </script>   
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Entidad.js")%>"> </script>       
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js")%>"> </script>   
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Index.js")%>"> </script>
   <%--<script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Main.js")%>"> </script> --%>

</asp:Content>