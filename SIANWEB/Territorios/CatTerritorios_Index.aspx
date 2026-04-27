<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
AutoEventWireup="true" CodeBehind="CatTerritorios_Index.aspx.cs" Inherits="SIANWEB.Terr.CatTerritorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
 	<!-- JUN10-2020 RFH Actualizado -->
 
    <!-- ALERTIFY 0.3.11 NUEVO -->
    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
        
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        
    
    <!-- BOOSTRAP -->
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">    
    
    <%--<script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-4.3.1-dist/js/bootstrap.min.js")%>"></script>    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-4.3.1-dist/css/bootstrap.min.css")%>">    --%>

    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script> 

    <%-- ZEBRA DATEPICKER --%>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    
    <%-- FONT AWESOME --%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">    
        
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/Territorios.css")%>" rel="stylesheet">

    <%--TIME PICKER--%>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    
    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<div class="container-fluid">

    <div class="row mt5">

        <div class="col-md-3 col-sm-12 mt5">            
            <form class="form-inline">
                <div class="form-group">
                    <div class="input-group">
                      <span class="input-group-addon" id="basic-addon1">
                            <i class="fa fa-filter"></i>
                      </span>
                      <input id="tbParametroBusqueda" type="text" class="form-control" placeholder="Estoy buscando...">                                  
                    </div>
                </div>                            
            </form>        
        </div>     

        <div class="col-md-2 col-sm-12 mt5">                                                                  
            <table class="w100">
                <tr>
                    <td style="text-align:right">
                        <label>Tipo</label>
                    </td>
                    <td>
                        <select id="ddlTipoRepresentante" class="form-control w100">
                            <option value="-1">--Todo--</option>
                            <option value="1">RSC</option>
                            <option value="2">Asesor</option>
                            <option value="3">Rik</option>
                            <option value="4">Gerencial</option>
                            <option value="5">Inter CD</option>
                            <option value="6">Venta Mostrador</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>

        <div class="col-md-2 col-sm-12 mt5">                   
              <table class="w100">
                <tr>
                    <td style="text-align:right">
                        <label>Estatus</label>
                    </td>
                    <td>
                        <select id="ddlTer_Activo" class="form-control w100">
                            <option value="-1">--Todo--</option>
                            <option value="1">Activos</option>
                            <option value="0">Inactivos</option>                
                        </select>            
                    </td>
                </tr>
            </table>
        </div>
                        
        <div class="col-md-1 col-sm-10">                   
            <button id="btnCargarListado" type="button" class="btn btn-primary btn-sm w100" style="margin-top:8px!important;">
                <i class="fa fa-search"></i>&nbsp;Buscar
            </button>  
        </div>

        <div class="col-md-1 col-sm-2 mt5">                   
            <span style="position:absolute;" >                    
                    <i id="spinner_Indice" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:block; margin-top:3px;"></i>
            </span>        
        </div>

        <div class="col-md-1 col-sm-12">                   
        </div>

        <div class="col-md-1 col-sm-12">
            <button id="btnTerr_DescargarReporte" type="button" class="btn btn-default btn-sm w100" style="margin-top:8px!important;">
                <i class="fa fa-cloud-download" aria-hidden="true"></i>&nbsp;Reporte
            </button>          
        </div>

        <div class="col-md-1 col-sm-12">
            <button id="btnTerritorio_Nuevo" type="button" class="btn btn-primary btn-sm w100" style="margin-top:8px!important;">
                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Nuevo
            </button>          
        </div>
         
    </div>

    <div class="row mt5">
        <div class="col-md-12" style=" text-align:center;" >
        <div class="table-responsive">
            <table id="tblTerritorios" class="table table-hover table-bordered RadGrid_Outlook">
                    <thead>
                        <tr>
                            <th class="terr_cell_th ColClave">Clave</th>
                            <th class="terr_cell_th ColDesc ">Descripci&oacute;n</th>
                            <th class="terr_cell_th ColUEN ">UEN</th>
                            <th class="terr_cell_th ColRepr">Representante</th>
                            <th class="terr_cell_th ColSegm">Segmento</th>
                            <th class="terr_cell_th ColTClie">Tipo cliente</th>
                            <th class="terr_cell_th ColTRep">TR</th>
                            <th class="terr_cell_th ColEstatus">Estatus</th>
                            <th class="terr_cell_th"></th>                        
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
               </table>         

             <ul class="pagination" id="PaginacionPie" style="margin:0px!important;" >
               <li><a href = "#">&laquo;</a></li>
               <li class = "active"><a href = "#">1</a></li>
               <li><a href = "#">2</a></li>           
               <li><a href = "#">&raquo;</a></li>
            </ul>
        </div>                         
        </div>
    </div>

</div>


<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
<div id="modalTerritorio" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;">
    <div class="modal-dialog vertical-center" role="document" style="width:600px" >
        <div class="modal-content" style="width:100%;">
            <div class="modal-header">                
                <table style="width:100%;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <h4 id="h4">Territorio</h4>
                                </td>
                                <td>
                                    <span style="" >                
                                        <i id="spinner_modalTerritorio" class="fa fa-spinner fa-pulse fa-2x fa-fw" 
                                        style="display:block; margin-top:3px;"></i>
                                    </span>                            
                                </td>
                            </tr>
                        </table>
                    </td>                    
                    <td style="margin-top:30px;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>                
                    </td>
                </tr>
                </table>
           </div>
            <div class="modal-body" id="Div5">

                <input type="hidden" id="hfIdInsertUpdate" value="0" />
                             
                    <div class="panel panel-default" style="margin-top:5px;">                                                              
                        <div class="panel-body">
                        
                    <table class="w100 table_t">
                        <tr>
                            <td style="width:100px;" class="text-right">Tipo</td>
                            <td>                                 
                                <select id="ddlTipoTerritorio" class="form-control" style="width:200px;">
                                    <option value="1">RSC</option>
                                    <option value="2">ASESOR</option>
                                    <option value="3">RIK</option>
                                    <option value="4">GERENCIAL</option>
                                    <option value="5" disabled="disabled">Inter CD</option>
                                    <option value="6" disabled="disabled">Venta Mostrador</option>
                                </select>
                            </td>                            
                        </tr>
                    </table>

                    <div id="div_Uen" class="tbl-Visible">               
                    <table>
                        <tr>
                            <td style="width:100px;" class="text-right">UEN</td>
                            <td style="width:80px;">
                                <input id="tbUEN" type="text" class="form-control" readonly/>
                            </td>
                            <td style="width:250px;">
                                <select id="ddlUEN" class="form-control w100">                                    
                                </select>
                            </td>
                        </tr>
                    </table>
                    </div>

                    <div id="div_Segmento" class="tbl-Visible">               
                    <table  id="trSegmento">
                        <tr>
                            <td style="width:100px;" class="text-right">Segmento / Giro</td>
                            <td style="width:80px;">
                                <input id="tbSegmento" type="text" class="form-control" readonly/>
                            </td>
                            <td style="width:320px;">
                                <select id="ddlSegmento" class="form-control w100">                                    
                                </select>
                            </td>
                        </tr>
                    </table>
                    </div>

                    <table>                        
                        <tr>
                            <td style="width:100px;" class="text-right">Tipo de cliente</td>
                            <td style="width:80px;">
                                <input id="tbTipoCliente" type="text" class="form-control" />
                            </td>
                            <td style="width:250px;">
                                <select id="ddlTipoCliente" class="form-control">
                                </select>
                            </td>
                        </tr>
                    </table>

                    <table class="w100">
                        <tr>
                            <td style="width:100px;" class="text-right">Clave territorio</td>
                            <td>
                                <input type="text" id="tbClaveTerritorio" class="form-control" style="width:200px;" readonly/>
                            </td>
                        </tr>
                    </table>

                        </div>
                    </div>

                    <!--br-->

                    <table>
                        <tr>
                            <td style="width:100px;" class="text-right">Id Local</td>
                            <td colspan="1">
                                <input id="tbIdLocal" type="text" class="form-control" onchange="ValidarNombre(this.value)" style="width:200px;" placeholder="Escribe el nombre"/>
                                <div id="mensajeError" class="error-box"></div>
                            </td>
                        </tr>
                    </table>

                    <div class="panel panel-default" style="margin-top:5px;">                                      
                        <!--div class="panel-heading titulo_blod">Territorio autorizado</div-->
                        <div class="panel-body">
                        <h5>Territorios autorizados</h5>
                            <table class="w100">
                            <tr>
                                <td style="width:100px;" class="text-right">Representante</td>
                                <td style="width:80px;">
                                    <input id="tbIdRepresentante" type="text" class="form-control"/>
                                </td>
                                <td>
                                    <select id="ddlRepresentante" class="form-control">
                                    </select>
                                </td>
                            </tr>
                            </table>
                            <table class="w100">
                                <tr>
                                    <td style="width:100px;" class="text-right">Territorio</td>
                                    <td>
                                        <input id="tbTerritorio" type="text" class="form-control" style="width:200px;"/>
                                    </td>                            
                                </tr>
                                <tr>
                                    <td class="text-right">Estado</td>
                                    <td>                                    
                                        <span id="btnTerrAut_Activo" class="badge badge-success" style="margin-top:4px; background-color:#2dde98; display:none; width:100px; height:20px;">Activo</span>            
                                        <span id="btnTerrAut_Inactivo" class="badge badge-secondary" style="margin-top:4px; display:none; width:100px; height:20px;">Inactivo</span>
                                    </td>                            
                                </tr>
                            </table>                    

                        </div>
                    </div>
                                        
                                      
                    <div id="div_SolocitudCambio" class="tbl-Oculto">                                       
                        <div class="panel panel-default" style="margin-top:5px;">                                      
                        <!--div class="panel-heading titulo_blod">Solicitud de cambio</div-->
                        <div class="panel-body">
                            <h5>Solicitud de cambio</h5>

                            <input type="hidden" id="hfIdAutorizacion" value="" />

                           <table style="width:100%;" >
                                <tr>
                                    <td style="width:100px;" class="text-right">Representane</td>
                                    <td style="width:80px;">
                                        <input id="tbSolicitudCambio_Representante" type="text" class="form-control" readonly/>
                                    </td>
                                    <td>
                                        <select id="ddlSolicitudCambio_Representante" class="form-control w100">
                                        </select>
                                    </td>                            
                                </tr>
                            </table>

                            <table class="w100">
                                <tr>
                                    <td style="width:100px;" class="text-right">Territorio</td>
                                    <td colspan="2">
                                        <input id="tbSolicitudCambio_Terr" type="text" class="form-control" style="width:200px;" />
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td style="width:100px;" class="text-right">Estado</td>
                                    <td colspan="1">                                        
                                        <!--button id="btnSolcitudCambio_EstadoActivo" class="btn btn-success bt-sm">Territorio Activo</button>                           
                                        <button id="btnSolcitudCambio_EstadoInactivo" class="btn btn-secundary bt-sm">Territorio Inactivo</button-->
                                        <select id="ddlSolicitudCambio_Activo" class="form-control" style="width:100px;">
                                            <option value="1">Activo</option>
                                            <option value="0">Inactivo</option>
                                        </select>
                                    </td>        
                                    <td colspan="1">                                        
                                        <span id="spanSolCamb_Activo" class="badge badge-success" style="margin-top:4px; background-color:#2dde98; display:none;">Activo</span>            
                                        <span id="spanSolCamb_Inactivo" class="badge badge-secondary" style="margin-top:4px; display:none;">Inactivo</span>
                                    </td>                                                                        
                                </tr>
                                <tr>
                                    <td style="width:100px;" align="left">
                                        <button id="btnCancelarSolicitudCambios" class="btn btn-primary bt-sm">Cancelar</button>                           
                                    </td>                                    
                                    <td></td>
                                    <td style="width:80px;" align="right">                                        
                                    </td>                                
                                </tr>
                            </table>   

                        </div>
                        </div>                                          
                    </div>
                        <input type="hidden" id="hfGuardarCambioDeEstado" value="0" />
                        <input type="hidden" id="hfTer_Activo" value="0" />

                      <!--table>
                        <tr>
                            <td style="width:100px;">El territorio esta</td>
                            <td>                                
                                <span id="baTerritorioActvo" class="badge badge-success" style="margin-top:4px; background-color:#2dde98; display:none;">Activo</span>            
                                <span id="baTerritorioInactvo" class="badge badge-secondary" style="margin-top:4px; display:none;">Inactivo</span>
                            </td>
                            <td>                                
                                <button class="btn btn-success btn-sm" id="btnTerritorio_Activar">Activar territorio</button>            
                                <button class="btn btn-danger btn-sm" id="btnTerritorio_Desactivar">Desactivar territorio</button>                       
                            </td>                            
                        </tr>
                    </table-->                   
                                        
                </div>
                <div class="modal-footer">                                    
                    <span id="btnCrearSolicitudDeCambio">
                        <button class="btn btn-default">Editar</button>                           
                    </span>
                    <button class="btn btn-default" id="btnTerritorio_Cancelar">Cancelar</button>            
                    <button class="btn btn-primary" id="btnTerritorio_Guardar">Guadar</button>            
            </div>
        </div>
    </div>
</div>

<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
<div id="modalDetalle" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;">
    <div class="modal-dialog vertical-center" role="document" style="width:700px" >
        <div class="modal-content" style="width:100%;">
            <div class="modal-header">                
                <table style="width:100%;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <h4 id="h1">Detalle de Territorio</h4>
                                </td>
                                <td>
                                    <span style="" >                
                                        <i id="spinner_modalDetalle" class="fa fa-spinner fa-pulse fa-2x fa-fw" 
                                        style="display:block; margin-top:3px;"></i>
                                    </span>                            
                                </td>
                            </tr>
                        </table>
                    </td>                    
                    <td style="margin-top:30px;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>                
                    </td>
                </tr>
                </table>
           </div>
           <div class="modal-body" id="Div2">

                <input id="hfModal_Id_Ter" type="hidden" value="">                                  
                           
                <div class="row">                  
                 
                    <div class="col-md-10 col-sm-12">                   
                        <form class="form-inline">
                            <div class="form-group w100">
                                <div class="input-group w100">
                                  <span class="input-group-addon" id="Span1" style="width:35px;" >
                                        <i class="fa fa-filter"></i>
                                  </span>
                                  <input id="tbDetalle_Buscar" type="text" class="form-control w100" placeholder="Estoy buscando...">                                  
                                </div>
                            </div>                            
                        </form>      
                    </div>
                
                    <div class="col-md-2 col-sm-12">                   
                        <button id="btnModalDetalle_Buscar" type="button" class="btn btn-primary btn-sm w100">
                            <i class="fa fa-search"></i>&nbsp;Buscar
                        </button>  
                    </div>
    
                </div>                

                <div class="row">                                   
                    <div class="col-md-12 col-sm-12">                   

                    
                <table id="tblTerritorioDetalle" class="table table-hover table-bordered RadGrid_Outlook">
                    <thead>
                    <tr>
                        <th class="terr_cell_th ColClave" style="width:100px;" >No.Cte.</th>
                        <th class="terr_cell_th ColClave" >Nombre</th>                        
                        <th class="terr_cell_th ColClave" >Territorio</th>                            
                        <!--th class="terr_cell_th ColClave" >RIK</th-->                                                    
                        <th class="terr_cell_th ColClave" ></th>                                                    
                    </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>  

                <label id="lbTerritorioDetalle_Count"></label>
                    </div>
                </div>          

           </div>
           <div class="modal-footer">                                    
                <button class="btn btn-default" data-dismiss="modal" id="Button2">Cerrar</button>            
                <!--button class="btn btn-primary" id="Button3">Guadar</button-->            
            </div>
        </div>
    </div>
</div>
                           
<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->                          
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

<!--\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->

    <script type="text/javascript">
        document.title = 'Territorios';
        var _ApplicationUrl = '<%=ApplicationUrl %>';
        var _Usuario_Tipo = "<%= Usuario_Tipo %>";

        function ValidarNombre(val) {
            const valor = val.toUpperCase();
            const palabrasProhibidas = ['INTER CD', 'VENTA MOSTRADOR', 'INTER', 'MOSTRADOR'];
            const mensajeError = document.getElementById("mensajeError");
            let contienePalabraProhibida = false;

            for (let i = 0; i < palabrasProhibidas.length; i++) {
                if (valor.includes(palabrasProhibidas[i])) {
                    mensajeError.textContent = '❌ No se puede utilizar la palabra prohibida:' +  palabrasProhibidas[i];
                    mensajeError.style.display = "block";
                    tbIdLocal.value = "";
                    tbIdLocal.focus();
                    return;
                }
            }
 
            if (!contienePalabraProhibida) {
                mensajeError.textContent = ""; // Limpia el mensaje si no hay error
            }
        }
    </script>

    <script src="<%=Page.ResolveUrl("~/js/Login.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Config.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Config.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_PermisosUsuario.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_UEN.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Excel.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Segmento.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_TipoCliente.js?v=20220509")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Paginacion.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Aut.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Reps.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Ajax.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_Main.js?v=20220509")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Territorios/Terr_TipoTerritorio.js?v=20220509")%>"></script>
    
</asp:Content>