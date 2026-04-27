<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="GestionIncrementoClientesDetalle.aspx.cs" Inherits="SIANWEB.GestionPrecios.GestionIncrementoClientesDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">

    
<!-- jQuery -->
<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>

<!-- Bootstrap CSS -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">

<!-- Bootstrap JS -->
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <%-- ZEBRA DATEPICKER --%>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    
     <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_acys.css?v=1")%>" rel="stylesheet">
<!-- FontAwesome -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

<!-- Alertify -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>

<!-- DataTables -->
    <link href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet">
<link href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet">
<script type="text/javascript" src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>

<!-- Otros estilos y scripts -->
<%--<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>">--%>

<%--<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>--%>



 <%-- <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
  --%>  

    <style>
.badge-incremento {
    display: inline-block;
    padding: 8px 12px;
    background: linear-gradient(45deg, #ff9800, #ff5722);
    color: white;
    border-radius: 3px;
    box-shadow: none;
    font-weight: bold;
    font-size: 12px;
    vertical-align: middle;
    line-height: 1.5;
    height: 30px;
    white-space: nowrap;
    animation: none;
}
.badge-incremento i {
    margin-right: 5px;
}

/* Estandarizar todos los badges a la misma altura */
.badge {
    display: inline-block;
    padding: 8px 12px;
    font-size: 12px;
    font-weight: bold;
    line-height: 1.5;
    border-radius: 3px;
    height: 30px;
    white-space: nowrap;
    vertical-align: middle;
}

td.editable {
    cursor: pointer;
    background-color: #f9f9f9;
}

</style>

    <style type="text/javascript" >
        
                    .modal {
        z-index: 1050; /* Asegura que el modal esté al frente */
        width: 1600px;
    }
    .modal-backdrop {
        z-index: 1050; /* Fondo detrás del modal */
    }
 
    .modal-dialog {
            width: 1050px; 
     }

    .row-highlight {
        background-color: #d1ecf1 !important; /* Color personalizado */
    }
    .scrollable-tab {
    height: 610px;
    overflow-x: scroll;
    overflow-y: auto;
}
      
table.dataTable th:nth-child(5) {
    width: 400px !important; /* Quinta columna */
}
 
#spinner {
    display: none;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 9999;
}


    </style> 
 <script>
     $(document).ready(function () {
         console.log('jQuery está funcionando:', $().jquery); // Debe imprimir la versión de jQuery
         console.log('Bootstrap cargado correctamente');
         $('#testModal').modal(); // Intenta abrir un modal de prueba (si existe en tu código)
         $('#ModalAlerta').on('hidden.bs.modal', function () {
             ocultarIframe(); // Limpieza automática al cerrar con [X]
     });
     });

    

     function ocultarIframe() {
         const contenedor = document.getElementById("contenedorFrame");
         contenedor.innerHTML = ''; // Elimina el iframe para evitar errores posteriores
         contenedor.style.display = "none";

         $('#ModalAlerta').modal('hide'); // Cierra el modal
     }
 

 </script>
 

    <div class="container-fluid">
    
         <input type="hidden" id="hurl" value="0" />
         <input type="hidden" id="hrenglon" value="0" />
         <asp:HiddenField id="estatus" runat="server" value="1"/> 
        <asp:HiddenField ID="hcorreo" runat="server" ClientIDMode="Static" />
        <asp:HiddenField id="hIdPropuestaGP" runat="server" value="1"/>
          

         <div class="row mt5">

          <div class="col-md-4 col-sm-12 mt5">                                                                  
             <table class="w100">
                    <tr>
                        <td style="width:100px;" class="text-right">Cliente : </td>
                        <td style="width:80px;">
                            <input id="txtCliente" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtCatNomComercial" type="text" class="form-control"/>
                        </td>
                    </tr>
             </table>
                                 
        </div>

        <div class="col-md-2 col-sm-12 mt5">                                                                  
             <table class="w100">
                   
                    <tr>
                        <td style="vertical-align:middle; width:100px;">Días Vigencia :   </td>                
                        <td>    
                            <input id="txtDiasVigencia" type="text" class="form-control"/>                                                        
                        </td>
                         
                    </tr>
        
             </table>
        </div>
        <div class="col-md-1 col-sm-12 mt5">                                                                  
          <table class="w300">
             <tr>
                     <td > 
                         <label>No. de Propuesta: </label></td>
                       <td>   <input id="txtIdPropuestaGP" type="text" class="form-control"/>                                                        
                 </td>
                  
             </tr>
 
      </table>
 </div>


        <div class="col-md-2 col-sm-12 mt5">                                                                  
            <table class="w100">           
                <tr>
                    <%--todo implementar esta opcion <td style="vertical-align:middle; width:100px;">Estatus : </td>                --%>
                        <td>                                    
                            <span id="btnAut_Activo" class="badge badge-info" style="margin-top:4px; background-color:#2dde98; display:none; width:100px; height:20px;">Análisis</span>            
                            <span id="btnAut_Inactivo" class="badge badge-secondary" style="margin-top:4px; display:none; width:100px; height:20px;">Negociación</span>
                            <span id="btnAut_Cerrado" class="badge badge-success" style="margin-top:4px; display:none; width:100px; height:20px;">Cerrado</span>
                        </td>   
                         
                </tr>
            </table>
        </div>
        <div class="col-md-1 col-sm-12 mt5">           
                      <button type="button" id="ConsultarClientes" class="btn btn-primary btn-sm" style="width:100%;"   onclick="verclientes()" title="Consultar información."><i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar</button>
          
        </div>
        <div class="col-md-1 col-sm-12 mt5">    
                    <%--<button type="button" class="btn btn-default" id="btnIncremento_Cancelar">Cancelar</button>--%>  
                     <button id="btnRegresar" type="button" class="btn btn-default btn-sm" style="width:100%;" 
                         title="Regresar a pantala anterior" onclick="cerrarpantalla()">
                         <i class="fa fa-arrow-left"></i><span>&nbsp;Regresar</span>
                     </button>
                   
        </div>    
              <div class="col-md-1 col-sm-12 mt5">  
                   <button type="button"  class="btn btn-primary btn-sm" style="width:100%;"   id="btnIncremento_Guardar" title="Registrar los cambios realizados."><i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar</button>  
                  </div>
 </div>
        <%----segundo renglon--%>
         <div class="row mt5">

          <div class="col-md-4 col-sm-12 mt5">                                                                  
             <table class="w100">
                    <tr>
                        <td style="width:100px;" class="text-right">Matriz :</td>
                        <td style="width:80px;">
                            <input id="txtIdMatriz" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtNombreMatriz" type="text" class="form-control"/>
                        </td>
                    </tr>
             </table>
                                 
        </div>

        <div class="col-md-3 col-sm-12 mt5">                                                                  
             
             <table class="w100">
                <tr>
                        <td style="vertical-align:middle; width:100px;">F. Inicio Incremento :  </td>                
                         
                        <td>
                            <input type="text" id="tbFechaInicio" class="form-control datepicker " value=""/>                                            
                        </td>
                        <td id="ContenedorFechas"></td>
                        <td style="">
                            <img id="spinner_fecha" style="display:none; margin-top:5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />                    
                        </td>
                    </tr>
        
             </table>

        </div>

        <div class="col-md-2 col-sm-12 mt5">                                                                  
                    
                    <form class="form-inline">
                        <div class="form-group">
                            <div class="input-group">
                              <span class="input-group-addon" id="basic-addon1">
                                    <i class="fa fa-filter"></i>
                              </span>
                              <input id="txtProducto" type="text" class="form-control" placeholder="Producto buscar..."/>                                  
                            </div>
                        </div>                            
                    </form>   
        </div>
        <div class="col-md-1 col-sm-12 mt5">
            <button type="button" id="btnEnviarCorreo" class="btn btn-default  btn-sm" style="width:100%;"  title="Enviar correo con la propuesta al cliente .">
                 <i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;Enviar Correo</button>
         </div>
        <div class="col-md-1 col-sm-12 mt5">
            <button type="button" id="btnPropuesta" class="btn btn-default  btn-sm" style="width:100%;"  title="Generar la propuesta en Excel.">
                <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Propuesta
            </button>
        </div>
        <div class="col-md-1 col-sm-12 mt5">
            <button type="button" id="btnValidarPropuesta" class="btn btn-primary  btn-sm" style="width:100%;"  title="Validar Propuesta y cerrar negociación.">
                <i class="fa fa-check-square-o" aria-hidden="true"></i>Validar Prop.
            </button>
        </div>
    </div>

          <%--   Tercer Renglon--%>
        <div class="row mt5">
            <div class="col-md-4 col-sm-12 mt5">
                <table class="w100">
                    <tr>
                        <td style="width: 100px;" class="text-right">Tamaño :</td>
                        <td style="width: 80px;">
                            <input id="txtIdTamaño" type="text" class="form-control" />
                        </td>
                        <td style="width: 100px;" class="text-right">Tipo Cuenta :</td>
                        <td>
                            <input id="txtTipoCuenta" type="text" class="form-control" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-md-3 col-sm-12 mt5">
                <table class="w100">
                    <tr>
                        <td style="vertical-align: middle; width: 100px;">Representante :   </td>
                        <td>
                            <select id="ddlRepresentante" class="form-control rfdDecorated">
                                <option value="">Seleccione...</option>
                                <!-- Opciones dinámicas se agregarán aquí -->
                            </select>
                        </td>

                    </tr>
                </table>
            </div>

            <div class="col-md-2 col-sm-12 mt5">
                <table class="w100">
                    <tr>
                        <td style="vertical-align: middle; width: 100px;">Teléfono :   </td>
                        <td>
                            <input id="txtTelefonoRik" type="text" class="form-control" />
                        </td>

                    </tr>
                </table>
            </div>
            <div class="col-md-1 col-sm-12 mt5">

          <%--      <button id="btnAgregarRenglon" type="button" class="btn btn-primary">Agregar Producto</button>--%>

                <button id="btnAgregarRenglon" type="button" class="btn btn-primary btn-sm" style="width:100%;" Title="Agregar un producto nuevo al cliente.">
                            <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Agregar
                        </button>
            </div>
        
        <div class="col-md-1 col-sm-12 mt5">    
                    <%--todo implementar esta opción <button class="btn btn-sucess" id="btnIncremento_AutAlerta">Enviar Autorización</button>  --%>
            <%-- <button type="button" id="btnExportarExcel"  class="btn btn-primary" data-bs-dismiss="modal" aria-label="Close">Listado Excel</button>--%>

                <button id="btnExportarExcel"    type="button" class="btn btn-default btn-sm" style="width:100%;" title="Genear listado con los productos en excel." >
                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Listado
                        </button>
              <img id="spinner_Exportar" style="display:none; margin-top:5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />                    
           
              
        </div>    
            <div class="col-md-1 col-sm-12 mt5">
            <%--<button type="button" id="btnEnviarAcys" class="btn btn-primary  btn-sm" style="width:100%;"  title="Validar Propuesta y cerrar negociación.">
                <i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;Enviar ACYS
            </button>--%>
        </div>

            </div>
 </div>

     <div class="row mt5">

         <div class="col-md-4">
             <div class="form-group">
                  </div>
         </div>
         <div class="col-md-4">
            
         </div>
           <div class="col-md-4">
          <div class="col-md-2">
                        </div>
 </div>

     </div>

 <div id="spinner" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999;">
    <div class="spinner-border text-primary" role="status">
        
        <img src="<%=Page.ResolveUrl("~/Img/patternfly/spinner.gif") %>"  alt="Cargando...">
 
    </div>
</div>

                         <%-- <th class="text-center">Id Rik</th>
                    <th class="text-center">Representante</th>
                    <th class="text-center" style="width:20px;">Cliente</th>
                    <th class="text-center" style="width:100px;">Nombre del cliente</th>
                    <th class="text-center" style="width:20px;">Matriz</th>
                    <th class="text-center" style="width:50px;">Nombre Matriz</th>--%>
        <%-- <th>Convenio</th>
                    <th>Nombre Conv</th>
                    <th>Convenio Categoría</th>--%>
        <%--<button type="button" class="btn btn-warning" onclick="mostrarIframe()">Mostrar Autorización</button>
    <button type="button" class="btn btn-default" onclick="ocultarIframe()">Cerrar</button>--%>


 <div class="tab-pane active" id="DivtabClientes" style="overflow-x: auto; width: 100%;" > 
    <div class="col-md-12 text-center">
        <table id="clientesTable"   class="table table-hover table-bordered RadGrid_Outlook">
            <thead>
                <tr>
  
                    <th class="text-center" style="width:20px;">Prod</th>
                    <th class="text-center" style="width:50px;">Descripción</th>
                    <th class="text-center" style="width:30px;">Categoría</th>
                   
                    <th class="text-center" style="width:30px;">Unidades</th>
                    <th class="text-center" style="width:30px;">Precio Venta</th>
                     <th class="text-center" style="width:30px;">Venta Mensual</th>
                    <th class="text-center" style="width:30px;">Mg Red Mensual $</th>
                    <th class="text-center" style="width:30px;">Mg Red Mensual %</th>
                    <th class="text-center" style="width:30px;">PO Proyectado</th>
                    <th class="text-center" style="width:30px; ">Costo AAA Proy</th> 
                    <th class="text-center" style="width:30px;">Plista Proyectado</th>
                    <th class="text-center" style="width:30px;">P. Minimo Rik</th>
                    <th class="text-center" style="width:30px;">P. Gerente</th>
                    <th class="text-center" style="width:30px; background-color:lightgray">Precio Negociado</th>
                    <th class="text-center" style="width:30px; background-color:lightgray">% Incremento</th>
                    <th class="text-center" style="width:30px;">Descto. / PLista</th>
                    <th class="text-center" style="width:30px;">Unidades Proy.</th>
                    <th class="text-center" style="width:30px;">Venta Proyectada</th>
                    <th class="text-center" style="width:30px;">Mg Red Proy. $</th>
                    <th class="text-center" style="width:30px;">Mg Red Proy %</th>
                    <th class="text-center" style="width:30px;">Var PPb Red %</th>
                    <th class="text-center" style="width:100px; background-color:lightgray">Comentarios</th>
                    <th class="text-center" style="width:30px;">Estatus</th> 
                    <th class="text-center" style="width:100px;">Convenio</th>
                    <th class="text-center" style="width:30px;">Accion</th> 
                    <th class="text-center" style="width:80px;">Incremento</th>
                    
                   <%--  <% if (2 == 1) { %>
                        <th class="text-center" style="width:40px;" title="Tipo de Cliente" >Tipo de Cliente</th>
                    <% } %>--%>
 
                </tr>
            </thead>
             <tfoot>
                 <tr>
                     <th colspan="5" class="text-end">Totales:</th>
                     <th id="totalVentas"></th> <!-- Venta Mensual -->
                     <th id="totalMgRedMensual"></th> <!-- Mg Red Mensual $ -->
                     <th></th> <!-- Mg Red Mensual % -->
                     <th></th> <!-- PO Proyectado -->
                     <th></th> <!-- Costo AAA Proy -->
                     <th></th> <!-- Plista Proyectado -->
                     <th></th> <!-- P. Minimo Rik -->
                     <th></th> <!-- P. Gerente -->
                     <th></th> <!-- Costo AAA Proy -->
                    <th></th> <!-- p negociado -->
                    <th></th> <!-- incremento -->
                    <th></th> <!--dctopñista -->
                      <th></th>
                      <th></th>
                      <th></th>
                     <th id="totalVentaProy"></th> <!-- Venta Proyectada -->
                     <th id="totalMgRedProy"></th> <!-- Mg Red Proy. $ -->
                     <!-- ... el resto de columnas ... -->
                 </tr>
             </tfoot>

        </table>
       <%-- <button id="procesarClientes" class="btn btn-success" onclick="verclientes()">Procesar Seleccionados</button>--%>

</div>
     
           <div class="row mt5">
               <table class="w100">
                    <tr>
                        <td style="width:100px;" class="text-right">  </td>
                        <td style="width:80px;">
                             
                        </td>
                        <td>
                            
                        </td>
                    </tr>
             </table>
           </div>
    </div>


        
    <div id="modalAgregar" class="modal" width="1190px" tabindex="-1"   >
    <div class="modal-dialog"  style="width:1090px;" >
        <div class="modal-content" style="width:1090px;" >
            <div class="modal-header">
                <h5 class="modal-title">Agregar Producto</h5>
                
            </div>
            <div class="modal-body">
                
            
              <div class="tab-pane active" id="DivtabProductos" style="overflow-x: scroll;overflow-y: scroll;" >  
                                            <table class="table table-hover RadGrid_Gis scroll" id="tblAcuerdoProds">
                                                <thead>
                                                    <tr>     
                                                        <%--
                                                            
                                                            CLAQUIER CAMBIO AQUI MODIFICAR EN GC_RepCumplimientoVtaInstalada_Index.aspx
                                                            
                                                        --%>

                                                        <th style="width:5px!important;"></th>
                                                        <th class="h_cel_center" style="width:30px; display:none;" >#</th>
                                                        <th class="h_cel_center" style="width:100px;" >Código</th>
                                                        <th class="h_cel_center">Descripción</th>
                                                        <th class="h_cel_center">Categoria</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Obj Proy.</th>
                                                       <th class="h_cel_center" style="width:80px!important;">Precio Lista Proy.</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Min Rik</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Gerente</th>
                                                     
                                                         <th class="h_cel_center" style="width:80px!important;">Precio Negociado</th>
                                                     
                                                        <th class="h_cel_center" style="width:80px!important;">Dcto./PLista </th>
                                                        <th class="h_cel_center" style="width:80px!important;">Venta Proy.</th>
                                                       
                                                        <th class="h_cel_center" style="width:50px!important;">Unidades Proyectadas</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Mg Red Proyectada $</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Mg Red Proyectada %</th>
                                                    
                                                        <th class="h_cel_center" style="width:80px!important; display:none;">Mg Red PPb %</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Comentarios</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Lista</th>
                                                        <th style="width:5px!important;"></th>
                                                        <th class="h_cel_center" style="width:70px!important;"></th>
                                                        <th style="width:50px!important;"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>                                       
                                               
                                            </table>  

                                    </div>

                
            </div>
            <div class="modal-footer">
                <button type="button" id="btnCerrarProducto"  class="btn btn-secondary" data-bs-dismiss="modal"><i class="fa fa-window-close-o"></i><span>&nbsp;Cerrar</span></button>
                <button type="button" id="btnGuardarProducto" class="btn btn-primary"> <i class="fa fa-floppy-o"></i><span>&nbsp;Guardar&nbsp; </span></button>
            </div>
        </div>
    </div>
</div>

    <div class="modal" id="modalAcuerdo" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Detalles del Convenio</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Detalles del acuerdo -->
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

   <!-- Modal alerta de precios JFCV -->
<!--\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
     <div class="modal fade" id="ModalAlerta"  role="dialog"  style="height:100%;width:100%; display:none;overflow:auto;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h2">
                        <asp:Label runat="server" ID="Label4"></asp:Label>
                        Alerta de Precio GPMa
                    </h4>
                </div>
            </div>
            <div>
                <div id="spinnerIframe" style="text-align:center; display:none;">
                  <i class="fa fa-spinner fa-spin fa-2x text-warning"></i> Cargando...
                </div>


                <%--<div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 40% !Important;">
                    <iframe class="embed-responsive-item" id="frameAlerta" height="80%" width="100%"   src="../Ventana_AutorizacionPrecios.aspx"></iframe>
                </div>--%>
               <div id="contenedorFrame" class="embed-responsive embed-responsive-16by9" style="padding-bottom: 40%; display: none;">
                    <!-- Aquí se insertará el iframe -->
                </div>
            

            </div>
        </div>
    </div>
<!--\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->       
                      
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


    <script type="text/javascript">
       
        var idCte = "-1";
        var idRikSeleccionado = "-1";
        let representante = "-1"
        let sucursal = "-1";
        var _Usuario_Tipo = "<%= Usuario_Tipo %>";
        var hfId_Rik = '<%=Id_Rik %>';
        var Id_TU = '<%=Id_TU %>';
        var hfId_CD = '<%=Id_CD %>';
        var Id_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
        var Id_U = '<%=Id_U %>';
        var paginaActual = 0;
        var idreportegp = -1;

        function SoloNumeros(evt) {



            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };

        $('#modalAcuerdo').on('show.bs.modal', function () {
            $(this).find('.modal-content').css('opacity', 1); // Asegura visibilidad
        });


        $(document).ready(async function () {
            
            function getQueryParam(param) {
                const urlParams = new URLSearchParams(window.location.search);
                return urlParams.get(param);
            }

           

            // Obtén los parámetros
            idCte = getQueryParam('idcte');
            sucursal = getQueryParam('sucursal');
            representante = getQueryParam('representante');
            idRikSeleccionado = representante;
            nombrecliente = getQueryParam('nombrecliente');
            idmatriz = getQueryParam('idmatriz');
            nombrematriz = getQueryParam('nombrematriz');
            idtamano = getQueryParam('idtamano');
            idtipocuenta = getQueryParam('idtipocuenta');
            nomestatus = getQueryParam('nomestatus');
            idreportegp = getQueryParam('idreportegp');
            console.log('aqui ya cargue el valor de idRikSeleccionado')
            console.log(idRikSeleccionado);
            console.log(idreportegp);

            $("#hIdPropuestaGP").val(idreportegp); // Campo oculto

            CatRepresentante.CargarCombo(function () {
                $('#spinner').css('display', 'block');
                
                console.log('representante cargacombo');
                console.log(representante);
                $('#ddlRepresentante').val(representante);
                var Id_Rik = $('#ddlRepresentante').val();
                /* $('#spinner').hide();*/
                $('#spinner').css('display', 'none');
               
            });
             
            tablaClientes.ajax.reload(function () {
                tablaClientes.page(paginaActual).draw(false); // Mantiene la página actual
                console.log('recarga en ready');
            });
            // Muestra los valores (opcional para depuración)
            console.log('ID Cliente:', idCte);
            console.log('Sucursal:', sucursal);
            console.log('Representante:', representante);
            console.log('nombrecliente:', nombrecliente);
            console.log('nombrematriz:', nombrematriz);
            console.log('idmatriz:', idmatriz);
            console.log('idreportegp:', idreportegp);
             
           

            let txtcliente = document.getElementById("txtCliente");
            
            // Asigna un valor al input
             
            txtcliente.value = idCte;
            
            document.getElementById("txtCliente").value = idCte;
            document.getElementById("txtCatNomComercial").value = nombrecliente;
            document.getElementById("txtIdMatriz").value = idmatriz;
            document.getElementById("txtNombreMatriz").value = nombrematriz;
            document.getElementById("txtDiasVigencia").value = 15;
            document.getElementById("txtTipoCuenta").value = idtipocuenta;
            document.getElementById("txtIdTamaño").value = idtamano;
            


            let txtclientenombre = document.getElementById("txtCatNomComercial");
            txtclientenombre.value = nombrecliente;


            $('#txtCliente').prop('disabled', true);
            $('#txtCatNomComercial').prop('disabled', true);
            $('#txtIdMatriz').prop('disabled', true);
            $('#txtNombreMatriz').prop('disabled', true);
            $('#txtTipoCuenta').prop('disabled', true);
            $('#txtIdTamaño').prop('disabled', true);
            $('#txtIdPropuestaGP').prop('disabled', true);
     
       
            let valorAsignar = representante; // El valor que deseas asignar



            var tablec = $('#clientesTable').DataTable();

            //6mzo
            //leer los datos del cliente y de la Propuesta 
           

            
           
            
            /*Disable_Btn('#btnExportarExcel');*/
            //Disable_Btn('#btnEnviarAcys');

            if (nomestatus == 'Analizar') {
                Disable_Btn('#btnEnviarCorreo');
                Disable_Btn('#btnPropuesta');
                Disable_Btn('#btnValidarPropuesta');
            }

            if (nomestatus == 'Val. Precios') {
                Disable_Btn('#btnEnviarCorreo');
                Disable_Btn('#btnPropuesta');
                Disable_Btn('#btnValidarPropuesta');
            }
            if (nomestatus == 'Analizado') {
                // Disable_Btn('#btnEnviarAcys');
                Disable_Btn('#btnValidarPropuesta');
            }

            if (nomestatus == 'Analizando')
            {
               // Disable_Btn('#btnEnviarAcys');
                Disable_Btn('#btnValidarPropuesta');
            }
            if (nomestatus == 'Propuesta Enviada') {
              //  Disable_Btn('#btnEnviarAcys');

            }

            if (nomestatus == 'Precios Aceptados') {
               // Disable_Btn('#btnEnviarAcys');
                Disable_Btn('#btnIncremento_Guardar');
                Disable_Btn('#btnValidarPropuesta');
                Disable_Btn('#btnAgregarRenglon')
            }


            if (nomestatus == 'ACYS Enviado') {
                Disable_Btn('#btnIncremento_Guardar');
               // Disable_Btn('#btnEnviarAcys');
                Disable_Btn('#btnAgregarRenglon')
                Disable_Btn('#btnEnviarCorreo');
                Disable_Btn('#btnValidarPropuesta');
            }
            
            if (idCte && representante) {
                await consultarDatosClienteAsync(idCte, representante, idreportegp);
            } else {
                console.error("No se recibieron los parámetros necesarios.");
            }


            // Aplicar cambios al campo de búsqueda en cada renderizado
            tablec.on('draw', function () {
                $('input[type="search"]').attr('autocomplete', 'off');
            });
            console.log('cerrando spinner readydocument');
            $('#spinner').css('display', 'none');

            $("#ddlRepresentante").attr("disabled", "disabled");

            //tablaClientes.columns().every(function (index) {
            //    console.log(index, this.header().textContent.trim());
            //});

            //$('#clientesTable').on('draw.dt', function () {
            //    console.log(tablaClientes.data().length);
            //    console.log(tablaClientes.page.info().recordsTotal);
            //    console.log('Filtro global:', tablaClientes.search());
            //    console.log('Registros visibles:', tablaClientes.data().length);
            //    console.log('Total sin filtrar:', tablaClientes.page.info().recordsTotal);
            //    console.log('Total filtrado:', tablaClientes.page.info().recordsDisplay);
            //    if (tablaClientes.data().length === 0 && tablaClientes.page.info().recordsTotal > 0) {
            //        tablaClientes.ajax.reload(null, false);
            //        console.log('recargar');
            //    }
            //});
           
        });

        

        async function consultarDatosClienteAsync(idCte, representante, idreportegp) {
            try {
                const response = await fetch("GestionIncrementoClientesDetalle.aspx/GetPropuesta", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({ id_cte: idCte, representante: representante, idreportegp: idreportegp })
                });
                console.log('getpropuesta');
 
                if (!response.ok) {
                    throw new Error("Error en la consulta AJAX: " + response.statusText);
                }
                console.log('getpropuesta2');
                
                const result = await response.json();
                console.log('resultados inicio');
                console.log(result);
                if (result && result.d && Object.keys(result.d).length > 0) {
                    let data = result.d;
                    console.log('Obtuvo datos la consulta');
                    $("#hcorreo").val(data.Cte_Email);
                    $("#tbFechaInicio").val(data.FechaIncremento);
                    $("#txtDiasVigencia").val(data.Dias); 
                    $("#hIdPropuestaGP").val(data.Id_reporteGP);
                    $("#txtTelefonoRik").val(data.Telefono);

                    $('#txtIdPropuestaGP').prop('disabled', false);
                    $("#txtIdPropuestaGP").val(data.Id_reporteGP);
                    $("#txtIdPropuestaGP").attr("value", data.Id_reporteGP);
                    console.log('actualizando campo oculto de  IdPropuestaGP consultarDatosClienteAsync');
                    console.log(data.Id_reporteGP);
                    console.log('valor de $("#txtIdPropuestaGPss").val(');
                    console.log($("#txtIdPropuestaGP").val());
                    $('#txtIdPropuestaGP').prop('disabled', false);
                } else {
                    // No hay datos, muestra mensaje o realiza acción alternativa
                    alertify.error("La propuesta es nueva, cargando productos...");
                    $("#hIdPropuestaGP").val(0);
                    $("#txtIdPropuestaGP").val("");
                    // Opcional: limpia los campos o toma otra acción
                }
            } catch (error) {
                console.error("Error en la consulta AJAX:", error);
            }
        }

        $(document).ajaxStart(function () {
            // Muestra el spinner cuando comienza una solicitud AJAX
            $('#spinner').show();
            //$('#spinnerlogo').show();
            console.log('abriendo spinner ajaxstart');
            
        }).ajaxStop(function () {
            // Oculta el spinner cuando la solicitud AJAX finaliza
            
            setTimeout(function () {
                $('#spinner').hide();
            }, 1000); // Mantén el spinner visible por 2 segundos
            $('#spinner').hide();
            $('#spinnerlogo').hide();
            $('#spinnerlogo').css('display', 'none');
            $('#spinner').css('display', 'none');
            console.log('cerrando spinner');
        });
        
         
        let tablaClientes =
            $('#clientesTable').DataTable({
                "ajax": {
                    "url": "Clientesget.aspx", // Debes implementar este controlador
                    "type": "GET",
                    "dataSrc": "",
                    //data: function (d) {
                    //    d.representante = representante;
                    //    d.cliente = idCte;
                    //    d.producto = $('#txtProducto').val();
                    //    d.tipoReporte = 0;
                    //    d.idreportegp = idreportegp;
                    //    d.action = 'getClientesdetalle';
                    //}

                    data: function (d) {
                        // Aquí defines los parámetros que deseas enviar al servidor

                        if (!$('#ddlRepresentante').val()) {
                            d.representante = representante; // Valor del combo Representante
                            d.cliente = idCte;             // Valor del textbox Cliente
                            d.producto = $('#txtProducto').val();           // Valor del textbox Producto
                            d.tipoReporte = 0;
                            d.idreportegp = idreportegp;
                        } else {
                            d.representante = representante; // Valor del combo Representante
                            d.cliente = idCte;             // Valor del textbox Cliente
                            d.producto = $('#txtProducto').val();           // Valor del textbox Producto
                            d.tipoReporte = 0;
                            d.idreportegp = idreportegp;
                        }

                        d.action = 'getClientesdetalle';
 

                    }
                },
                "stateSave": true, // Activa el guardado de estado
                "autoWidth": false, 
                "columnDefs": [
                    { "targets": 0, width: "30px" },  
                    { "targets": 1, width: "350px" },  
                    { "targets": 2, width: "20px" },
                    { "targets": 3, width: "200px" },
                    { "targets": 4, width: "400px" },
                    { "targets": 5, width: "20px" },
                    { "targets": 6, width: "20px" },
                    { "targets": 7, width: "20px" },
                    { "targets": 8, width: "20px" },
                    { "targets": 9, width: "20px" },
                    { "targets": 10, width: "20px" },
                    { "targets": 11, width: "200px" }, 
                    { "targets": 12, width: "200px" }, 
                    /*{ "targets": 13, width: "20px" },*/
                    {
                        "targets": 13, "createdCell": function (td) {
                            $(td).css({ "font-weight": "bold", "width": "100px" });
                        }
                    },
                    {
                        "targets": 14, "createdCell": function (td) {
                            $(td).css({ "font-weight": "bold", "width": "100px" });
                        }
                    },
                    /*{ "targets": 14, width: "400px" },*/
                    { "targets": 15, width: "20px" },
                    {
                        "targets": 16, "createdCell": function (td) {
                            $(td).css({ "font-weight": "bold", "width": "80px" });
                        }
                    },
                    { "targets": 17, width: "20px" },
                    { "targets": 18, width: "400px" },
                    { "targets": 19, width: "20px" },
                    { "targets": 20, width: "20px" },
                   /* { "targets": 21, width: "20px" },*/
                    {
                        "targets": 21, "createdCell": function (td) {
                            $(td).css({ "font-weight": "bold", "width": "300px" });
                        }
                    },
                    { "targets": 22, width: "20px" },
                    { "targets": 23, width: "20px" },
                    { "targets": 24, width: "20px" },
                  
                    {
                        targets: 22, // Índice de la columna NomEstatus
                        render: function (data, type, row) {
                            let colorClase = '';
                            switch (data) {
                                case 'Analizar':
                                    colorClase = 'btn btn-primary'; // Azul
                                    break;
                                case 'Analizado':
                                    colorClase = 'btn btn-success'; // Verde
                                    break;
                                case 'Cerrado':
                                    colorClase = 'btn btn-secondary'; // Gris
                                    break;
                                case 'Eliminado':
                                    colorClase = 'btn btn-danger'; // Gris
                                    break;
                                case 'Nuevo':
                                    colorClase = 'btn btn-warning'; // Gris
                                    break;
                                default:
                                    colorClase = 'btn btn-light'; // Color por defecto
                            }
                            // Devuelve un botón con el texto del estatus
                            return `<span style="width:100%;" class="${colorClase}">${data}</span>`;
                        }
                    }

                ],
                "columns": [
                    { "data": "Id_Prd" },
                    { "data": "NomProducto" },
                    { "data": "NomCategoria" },
                    {
                        data: 'Unidades',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'PrecioVenta',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'Ventas',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'MgRed_MensualPesos',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end mgred_mensualpesos' // Alinea el contenido a la derecha
                    },
                    {
                        data: "MgRed_MensualPorc", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'PrecioObjetivoProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    { "data": "CostoAAAAFuturo", className: "costoAAAAFuturo", "visible": false },
                    {
                        data: 'PrecioListaProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end precioListaProyectado' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'PrecioMinRikProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },

                    {
                        data: 'PrecioGteProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },

                    {
                        data: 'PrecioNegociadoProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });

                        },
                        className: 'editable editable-precio editable-numeros' // Alinea el contenido a la derecha
                    },
                    {
                        data: "PorcIncrementoProy", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'editable editable-porcentaje editable-numeros text-end' // agrego par ael calculo Alinea el contenido a la derecha
                    },
                    {
                        data: 'DescuentoSobrePlistaProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end descuentoSobrePlistaProy' // Alinea el contenido a la derecha
                    },
                    //{
                    //    data: 'UnidadesProyectadas',
                    //    render: function (data, type, row) {
                    //        // Formatear el número con comas y dos decimales
                    //        return parseFloat(data).toLocaleString('es-MX', {
                    //            minimumFractionDigits: 2,
                    //            maximumFractionDigits: 2
                    //        });
                    //    },
                    //    className: 'editable editable-unidades editable-numeros' // Alinea el contenido a la derecha
                    //},
                    { "data": "UnidadesProyectadas", className: "UnidadesProyectadas", "visible": false },
                    {
                        data: 'VentaProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end ventaProy' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'MgRed_PesosProy',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end mgRed_PesosProy' // Alinea el contenido a la derecha
                    },

                    {
                        data: "MgRed_PorcProy", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end mgRed_PorcProy' // Alinea el contenido a la derecha
                    },
                    {
                        data: "VarPpbRed_Porc", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end varPpbRed_Porc' // Alinea el contenido a la derecha
                    },
                    { "data": "Comentarios", className: "editable editable-comentarios" },
                    { "data": "NomEstatus", className: "nomEstatus" },
                    {         //Convenio
                        data: 'Id_Pc', // Columna basada en este campo
                        title: 'Convenio',
                        render: function (data, type, row) {
                            if (data > 0) {
                                return `<button type="button" class="btn btn-info btn-convenio"
                                data-id="${row.Id_Pc}"
                                data-numero="${row.Id_Prd}"
                                data-nombre="${row.NomProducto}"
                                data-numero="${row.Pc_NoConvenio}"
                                data-nombre="${row.PC_Nombre}"
                                data-costoaaafuturo="${row.CostoAAAAFuturo}"
                                data-costoaaaactual="${row.CostoAAAActual}"
                                >
                                <i class="fa fa-info-circle"></i>
                            </button>`;
                            } else {
                                return ''; // Sin contenido si no hay acuerdo
                            }
                        }
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `<button type="button" class="btn btn-danger btn-sm btn-color-row">Eliminar</button>`;
                        } 
                    },
                    {
                        data: 'TieneIncremento',
                        name: 'TieneIncremento',
                        title: 'Incremento',
                        className: 'text-center',
                        width: "80px", // <-- ancho personalizado
                        render: function (data, type, row) {
                            if (data === 1 || data === "1" || data === true || data === "true") {
                                return `<span class="badge-incremento" title="Producto con incremento">
                        <i class="fa fa-arrow-up"></i> Con Inc
                    </span>`;
                            }
                            else if (data === 2 || data === "2") {
                                return `<span class="badge badge-warning" title="Necesario subir precio" style="background-color: #eded3fad; color: #333;">
                                    <i class="fa fa-exclamation-triangle"></i> Subir Precio
                                 </span>`;
                            }
     
                            else if (data === 3 || data === "3") {
                                return `<span class="badge badge-warning" title="Precio menor a precio Objetivo" style="background-color: #FF9800; color: #fff;">
                                    <i class="fa fa-arrow-up"></i> Precio < Objetivo
                                </span>`;
                            }
                            else if (data === 4 || data === "4") {
                                return `<span class="badge badge-success" title="Precio superior al precio Objetivo" style="background-color: #28A745; color: #fff;">
                                        <i class="fa fa-arrow-up"></i> Ok
                                </span>`;
                            }
                            return '';
                        }
                    }

                    //{
                    //    data: 'TieneIncremento',
                    //    name: 'TieneIncremento',
                    //    title: 'Incremento',
                    //    className: 'text-center',
                    //    width: "80px", // <-- ancho personalizado
                //        render: function (data, type, row) {
                    //        if (data === 1 || data === "1" || data === true || data === "true") {
                    //            return `<span class="badge-incremento" title="Producto con incremento">
                    //    <i class="fa fa-arrow-up"></i> Con Inc
                    //</span>`;
                //    }
                    //        return '';
                    //    }
                    //}

                ],
                error: function (xhr, status, error) {
                    console.error("Error: ", error);
                    alert("Hubo un problema con la solicitud.");
                }
                ,
                "footerCallback": function (row, data, start, end, display) {
                    // Índices de las columnas a totalizar (ajusta según tu tabla)
                    var idxVentaMensual = 5;      // Venta Mensual
                    var idxMgRedMensual = 6;      // Mg Red Mensual $
                    var idxVentaProy = 17;        // Venta Proyectada
                    var idxMgRedProy = 18;        // Mg Red Proy. $

                    // Función para sumar columna
                    var intVal = function (i) {
                        if (typeof i === 'string') {
                            i = i.replace(/[\$,]/g, '');
                        }
                        return isNaN(i) ? 0 : parseFloat(i);
                    };

                    // Suma de cada columna
                    var totalVentaMensual = data.reduce(function (a, b) {
                        return a + intVal(b['Ventas']);
                    }, 0);

                    var totalMgRedMensual = data.reduce(function (a, b) {
                        return a + intVal(b['MgRed_MensualPesos']);
                    }, 0);

                    var totalVentaProy = data.reduce(function (a, b) {
                        return a + intVal(b['VentaProy']);
                    }, 0);

                    var totalMgRedProy = data.reduce(function (a, b) {
                        return a + intVal(b['MgRed_PesosProy']);
                    }, 0);

                    // Mostrar los totales en el pie de la tabla
                    $(this.api().column(idxVentaMensual).footer()).html(totalVentaMensual.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $(this.api().column(idxMgRedMensual).footer()).html(totalMgRedMensual.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $(this.api().column(idxVentaProy).footer()).html(totalVentaProy.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $(this.api().column(idxMgRedProy).footer()).html(totalMgRedProy.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                }
                ,
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    } 
                }

            });

        $('#spinner').css('display', 'none');
        console.log('cerrando  spinner clietestable');
        $('#clientesTable tbody').on('click', '.btn-color-row', function () {
            const row = $(this).closest('tr');
            const currentColor = row.css('background-color');
            const button = $(this); // Encuentra el botón
            //modificar el nomestatus a eliminado si presiona eliminar 
            const rowData = $('#clientesTable').DataTable().row(row); // Obtiene la fila del DataTable
            const data = rowData.data(); // Obtiene los datos actuales de la fila

            const newColor = currentColor === 'rgb(209, 236, 241)' ? '' : '#f8d7da'; // Alterna entre colores
            row.css('background-color', newColor);

            if (currentColor === 'rgb(248, 215, 218)') { // Rojo claro (en formato RGB)
                   
                row.css('background-color', ''); // Restablecemos el color original
                // Cambiar el texto y el estilo del botón
                button.text('Eliminar') // Cambia el texto
                    .removeClass('btn-success') // Remueve el estilo azul
                    .addClass('btn-danger'); // Añade el estilo verde
                    //.prop('disabled', true); // Deshabilita el botón para evitar múltiples clics
                    // Cambiar NomEstatus a su estado inicial
                    data.NomEstatus = 'Analizar';
            } else {
                 row.css('background-color', '#f8d7da'); // Rojo claro
                button.text('Activar') // Cambia el texto
                    .removeClass('btn-danger') // Remueve el estilo azul
                    .addClass('btn-success'); // Añade el estilo verde
                    // Cambiar NomEstatus a "Eliminado"
                    data.NomEstatus = 'Eliminado';
            };
            // Actualizar los datos en el DataTable
            rowData.data(data).draw(false);
            
        });

        // Evento para redirigir a la pantalla de detalle
        $('#clientesTable').on('click', '.btn-detalle', function () {
             
            const idCliente = $(this).data('id');
            const idSucursal = $(this).data('idsucursal');
            const idRik = $(this).data('idrik');
           
            

            console.log('ID Cliente:', idCliente);
            const data = $('#clientesTable').DataTable().row($(this).parents('tr')).data();

            //const id_Cd = $(this).data('Id_Cte');
            //const id_Rik = $(this).data('Id_Rik');
            
            // Redirige a la nueva página con los parámetros en la URL
            //window.location.href = `ReporteGAPGerente.aspx?idcte=${id_Cte}&sucursal=${id_Cd}&representante=${id_Rik}`;
        });


        $('#clientesTable').on('click', 'td.editable', function () {
            let cell = $(this); // Celda actual
            let currentValue = cell.text(); // Valor actual de la celda
            let maxLength = 40;
            //10dic agregado
            let table = $('#clientesTable').DataTable(); // Referencia al DataTable
          
            // Crear un input para editar el contenido
            let input = $('<input>', {
                type: 'text',
                class: 'form-control',
                value: currentValue,
                maxlength: cell.hasClass('editable-comentarios') ? maxLength : null // Máximo de caracteres solo en comentarios
            });

             paginaActual = tablaClientes.page(); // Guarda la página actual
            console.log('pagina actual: ', paginaActual);


            // Reemplazar el contenido de la celda con el input
            cell.html(input);

            // Enfocar automáticamente el input
            input.focus();


            // Validaciones por tipo de campo<
            input.on('keypress', function (e) {

                if (event.keyCode === 13) { // Detectar Enter (código 13)
                    e.preventDefault(); // Evitar acción por defecto
                }

                if (cell.hasClass('editable-numeros')) {
                    // Solo números (permitir teclas de control como backspace y delete)
                    if (!/[0-9]/.test(String.fromCharCode(e.which)) && ![8, 46].includes(e.keyCode)) {
                        e.preventDefault();
                    }
                } else if (cell.hasClass('editable-comentarios')) {
                    // Letras, números y espacio
                    if (!/[a-zA-Z0-9\s]/.test(String.fromCharCode(e.which)) && ![8, 46].includes(e.keyCode)) {
                        e.preventDefault();
                    }
                }
            });

            // Guardar los cambios al perder el foco
            input.on('blur', function () {
                let newValue = $(this).val(); // Nuevo valor ingresado
                cell.text(newValue); // Reemplazar el input con el nuevo valor

                // Aquí puedes enviar el nuevo valor al servidor si es necesario
                console.log(`Nuevo valor para ${cell.index()}: ${newValue}`);

                // Validar la entrada
                if (cell.hasClass('editable-comentarios') && newValue.length > maxLength) {
                    alert('El comentario no puede exceder los 40 caracteres.');
                    return;
                }
                // Actualizar el modelo de DataTables
                let dtCell = table.cell(cell); // Obteniendo la celda del modelo DataTable
                dtCell.data(newValue).draw(false); // Actualizando el modelo y redibujando la tabla

                // Recalcular ventas proyectadas si se edita unidades o precio
                if (cell.hasClass('editable-unidades') || cell.hasClass('editable-precio')) {
                    calcularVentasProyectadas(table, dtCell.index().row);
                };
                if ( cell.hasClass('editable-porcentaje')) {
                    calcularVentasProyectadasPorcentaje(table, dtCell.index().row);
                };
                 
                // Cambiar el color de fondo de la fila y actualizar botón y NomEstatus
                let row = table.row(dtCell.index().row).node(); // Nodo de la fila
                let rowData = $(row); // Convertir a objeto jQuery

                // Cambiar color de fondo
                rowData.css('background-color', 'rgb(209, 236, 241)'); // Azul claro

                // Actualizar el botón
                let button = rowData.find('.btn-color-row'); // Seleccionar el botón en la fila
                if (button.length > 0) {
                    button.text('Eliminar') // Cambiar texto del botón
                        .removeClass('btn-success') // Remover estilo verde
                        .addClass('btn-danger'); // Agregar estilo rojo
                }

                // Cambiar el texto de NomEstatus a "Analizado"
                let nomEstatus = rowData.find('td.nomEstatus');
                if (nomEstatus.length > 0) {
                    nomEstatus.text('Analizado');
                }


                
            });
            paginaActual = tablaClientes.page(); // Guarda la página actual
            console.log('pagina actual2  ', paginaActual);
            // Guardar al presionar Enter
            input.on('keypress', function (e) {
                if (e.which === 13) $(this).blur();
            });
 
        });
        

        //mostrar tooltip en convenios 
        $(document).on('hidden.bs.modal', function () {
            $('.tooltip').tooltip('dispose'); // Elimina tooltips activos
        });

        $('#clientesTable tbody').on('mouseenter', '.btn-convenio', function () {
            const numero = $(this).data('numero');
            const id = $(this).data('id');
            const nombre = $(this).data('nombre');
            const costoaaafuturo = $(this).data('costoaaafuturo');
            const costoaaaactual = $(this).data('costoaaaactual'); // <-- Agrega esta línea

            
            // Tooltip personalizado
            const tooltipContent = `
        <strong>No. Convenio:</strong> ${numero}<br>
        <strong>Convenio Key:</strong> ${id}<br>
        <strong>Nombre Convenio:</strong> ${nombre}<br>
        <strong>Costo AAA futuro :</strong> ${costoaaafuturo}<br>
        <strong>Costo AAA Actual :</strong> ${costoaaactual}<br>
    `;

            
            $(this).tooltip({
                html: true,
                title: tooltipContent,
                placement: 'top', // Puedes ajustar la posición según sea necesario
                container: 'body'
            }).tooltip('show');

        });

      
        $('#modalAcuerdo').on('show.bs.modal', function () {
            $(this).css('opacity', 1); // Asegura que sea visible
            $('body').addClass('modal-open'); // Añade la clase que permite el scroll bloqueado
        }).on('hidden.bs.modal', function () {
            $('body').removeClass('modal-open'); // Limpia la clase tras cerrar
        });


      

        $('#clientesTable tbody').on('click', '.btn-convenio', function () {
            const numero = $(this).data('numero');
            const id = $(this).data('id');
            const nombre = $(this).data('nombre');
            const costoaaafuturo = $(this).data('costoaaafuturo');
            const costoaaactual = $(this).data('costoaaaactual');
            // Llena el contenido del modal
            $('#modalAcuerdo .modal-body').html(`
            <p><strong>No. Convenio:</strong> ${numero}</p>
            <p><strong>Convenio Key:</strong> ${id}</p>
            <p><strong>Nombre del Convenio:</strong> ${nombre}</p>
            <p><strong>Costo AAA futuro :</strong> ${costoaaafuturo}</p>
            <p><strong>Costo AAA Actual :</strong> ${costoaaactual}</p>
        `);
            console.log('abriendo pantalla modal');
            if (!$('#modalAcuerdo').hasClass('in')) {
                $('#modalAcuerdo').modal('show');
            }
            $('.modal-backdrop').remove();
        });

        function calcularVentasProyectadas(table, rowIndex) {

            let rowData = table.row(rowIndex).data(); // Datos de la fila actual

            let unidades = parseFloat(rowData['UnidadesProyectadas'] || 1);
            let precioNegociadoProy = parseFloat(rowData['PrecioNegociadoProy'] || 1);
            let ventasProyectadas = parseFloat(rowData['VentaProy'] || 0);
            let porcIncrementoProy = parseFloat(rowData['PorcIncrementoProy'] || 0);
            let precioListaProyectado = parseFloat(rowData['PrecioListaProy'] || 0);
            let mgRed_PesosProy = parseFloat(rowData['MgRed_PesosProy'] || 0);
            let mgRed_PorcProy = parseFloat(rowData['MgRed_PorcProy'] || 0);
            let varPpbRed_Porc = parseFloat(rowData['VarPpbRed_Porc'] || 0);
            let costoAAAAFuturo = parseFloat(rowData['CostoAAAAFuturo'] || 0);
            let descuentoSobrePlistaProy = parseFloat(rowData['DescuentoSobrePlistaProy'] || 0);
            let PrecioVenta = parseFloat(rowData['PrecioVenta'] || 0);
            let mgRed_MensualPorc = parseFloat(rowData['MgRed_MensualPorc'] || 0);

 
            // Calcular ventas proyectadas
            ventasProyectadas = unidades * precioNegociadoProy;

            console.log(`unidades : ${unidades}%`);
            console.log(`PrecioNegociadoProy : ${precioNegociadoProy}%`);
             
            console.log(`Porcentaje de Incremento 1 : ${porcIncrementoProy}`);


            if (PrecioVenta == 0) {
                porcIncrementoProy = 0;
            }
            else {
                porcIncrementoProy = (precioNegociadoProy - PrecioVenta) / PrecioVenta;
            }
            
            descuentoSobrePlistaProy = (precioListaProyectado - precioNegociadoProy)  ;
        
            mgRed_PesosProy = ventasProyectadas - (unidades * costoAAAAFuturo);
            mgRed_PorcProy = (ventasProyectadas - (unidades * costoAAAAFuturo)) / ventasProyectadas;
            console.log('mgRed_PorcProy');
            

            rowData['NomEstatus'] = 'Analizado';
            rowData['Id_Zona'] = 1;  //es que esta en estatus de analizado


            rowData['UnidadesProyectadas'] = unidades.toFixed(2);
            rowData['PrecioNegociadoProy'] = precioNegociadoProy.toFixed(2);
            rowData['VentaProy'] = ventasProyectadas.toFixed(2);
            rowData['PorcIncrementoProy'] = porcIncrementoProy.toFixed(2);
            rowData['MgRed_PesosProy'] = mgRed_PesosProy.toFixed(2);
            rowData['MgRed_PorcProy'] = mgRed_PorcProy.toFixed(2);
            rowData['DescuentoSobrePlistaProy'] = descuentoSobrePlistaProy.toFixed(2);
            rowData['MgRed_PesosProy'] = mgRed_PesosProy ;
            rowData['MgRed_PorcProy'] = mgRed_PorcProy;
            rowData['VarPpbRed_Porc'] = mgRed_PorcProy - mgRed_MensualPorc;

            // Actualizar la fila en el modelo de DataTables
            table.row(rowIndex).data(rowData).draw(false);

 
 
        }


        function calcularVentasProyectadasPorcentaje(table, rowIndex) {

            let rowData = table.row(rowIndex).data(); // Datos de la fila actual

            const precio = parseFloat(rowData['PorcIncrementoProy']);
            console.log(precio);

            if (isNaN(precio) || precio < 0) {
                e.preventDefault();
                alert('Por favor ingresa un precio válido mayor o igual a 0.');
            }
            else {
                let porcIncrementoProy = parseFloat(rowData['PorcIncrementoProy'] || 0);


                let unidades = parseFloat(rowData['UnidadesProyectadas'] || 1);
                let precioNegociadoProy = parseFloat(rowData['PrecioNegociadoProy'] || 1);
                let ventasProyectadas = parseFloat(rowData['VentaProy'] || 0);

                let precioListaProyectado = parseFloat(rowData['PrecioListaProy'] || 0);
                let mgRed_PesosProy = parseFloat(rowData['MgRed_PesosProy'] || 0);
                let mgRed_PorcProy = parseFloat(rowData['MgRed_PorcProy'] || 0);
                let varPpbRed_Porc = parseFloat(rowData['VarPpbRed_Porc'] || 0);
                let costoAAAAFuturo = parseFloat(rowData['CostoAAAAFuturo'] || 0);
                let descuentoSobrePlistaProy = parseFloat(rowData['DescuentoSobrePlistaProy'] || 0);
                let PrecioVenta = parseFloat(rowData['PrecioVenta'] || 0);
                let mgRed_MensualPorc = parseFloat(rowData['MgRed_MensualPorc'] || 0);
                let precioObjetivoProy = parseFloat(rowData['PrecioObjetivoProy'] || 1);

                porcIncrementoProy = porcIncrementoProy / 100;
               
                if (PrecioVenta == 0) {
                    porcIncrementoProy = 0;
                    precioNegociadoProy = precioObjetivoProy;
                }
                else {
                    precioNegociadoProy = PrecioVenta * ( 1 +  porcIncrementoProy );
                }

                // Calcular ventas proyectadas
                ventasProyectadas = unidades * precioNegociadoProy;


                descuentoSobrePlistaProy = (precioListaProyectado - precioNegociadoProy);

                mgRed_PesosProy = ventasProyectadas - (unidades * costoAAAAFuturo);
                mgRed_PorcProy = (ventasProyectadas - (unidades * costoAAAAFuturo)) / ventasProyectadas;
                console.log('mgRed_PorcProy');


                rowData['NomEstatus'] = 'Analizado';
                rowData['Id_Zona'] = 1;  //es que esta en estatus de analizado


                rowData['UnidadesProyectadas'] = unidades.toFixed(2);
                rowData['PrecioNegociadoProy'] = precioNegociadoProy.toFixed(2);
                rowData['VentaProy'] = ventasProyectadas.toFixed(2);
                rowData['PorcIncrementoProy'] = porcIncrementoProy.toFixed(2);
                rowData['MgRed_PesosProy'] = mgRed_PesosProy.toFixed(2);
                rowData['MgRed_PorcProy'] = mgRed_PorcProy.toFixed(2);
                rowData['DescuentoSobrePlistaProy'] = descuentoSobrePlistaProy.toFixed(2);
                rowData['MgRed_PesosProy'] = mgRed_PesosProy;
                rowData['MgRed_PorcProy'] = mgRed_PorcProy;
                rowData['VarPpbRed_Porc'] = mgRed_PorcProy - mgRed_MensualPorc;

                // Actualizar la fila en el modelo de DataTables
                table.row(rowIndex).data(rowData).draw(false);

            }

        }
        function verProductos(idCliente) {
            window.open("GestionIncremento.aspx?idCliente=" + idCliente, "_self");
        }


        function Alerta_IdRik() {
            alertify
                .okBtn("Ok")
                .confirm("<b>Error Identificador Rik </b><br/><p>El usuario actual no tiene un Rik asignado, " +
                    "esto causara un mal funcionamiento en el CRM, debe comunicarse a soporte.<p>", function (ev) {
                        ev.preventDefault();
                    }, function (ev) {
                        ev.preventDefault();
                    });
        }

        function verclientes() {
            tablaClientes.ajax.reload();
            console.log('recarga en verclientes');
            } ;
 
      
        function cerrarpantalla() {
            window.open("GestionIncrementoClientes.aspx" , "_self");
        }


        var CatRepresentante = {

            CargarCombo: function (CALLBACK_Exito) {


                $.ajax({
                    url: _ApplicationUrl + 'api/CrmRepresentante/Get_List?Id_Cd=0',
                    cache: false,
                    type: 'GET'
                }).done(function (response, textStatus, jqXHR) {
                    Estado = response.Estado;
                    listado = response.Datos;
                    //console.log(response);        
                    var ddl = $('#ddlRepresentante').empty();
                    ddl.append(
                        $('<option data-Id_U=0>').val(0).text('-- Todos --')
                    );
                    if (Estado == 1) {
                        var ID = 0;
                        for (var i = 0; i < listado.length; i++) {
                            ddl.append(
                                $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_Rik).text(listado[i].U_Nombre)
                            );
                            ID = listado[i].Id_U;
                        }
                        ddl.val(idRikSeleccionado);
                        if (idRikSeleccionado > 0) {
                            ddl.val(idRikSeleccionado);
                            ddl.removeAttr('disabled');
                        }
                        else {

                            ddl.prop('disabled', 'disabled');
                        }
                        /*console.log(Id_TU);*/
                        //if (Id_TU == 2 || Id_TU == 3 || Id_TU == 4 || Id_TU == 5 || Id_TU == 1) {
                        //    ddl.removeAttr('disabled');
                        //} else {
                        //    ddl.prop('disabled', 'disabled');
                        //}




                        console.log("representante " + hfId_Rik);
                       
                        if (hfId_Rik > 0) {
                            ddl.val(hfId_Rik);
                        }
                        if (Id_TU == 2 || Id_TU == 3 || Id_TU == 4 || Id_TU == 5 || Id_TU == 1) {
                            $("#ddlRepresentante").attr("disabled", "disabled");
                        } else {
                            ddl.removeAttr('disabled');
                        }

                        //$('#spinner').css('display', 'none');
                        /*  $('#btnCargarListado').attr('disabled', false);*/
                    }
                    if (CALLBACK_Exito) {
                        CALLBACK_Exito();
                    }
                }).fail(function (jqXHR, textStatus, error) {
                    //$('#spinner_GCIndice').css('display', 'none');
                    //$('#btnCargarListado').attr('disabled', false);
                    alertify.error('Error: CatRepresentante.Cargar_Representante');
                    console.log(jqXHR);
                });
            },

            CargarListadoCheck: function (CALLBACK_Exito) {
                alert(_ApplicationUrl + '/Login.aspx?Id=1');

                $.ajax({
                    url: _ApplicationUrl + '/api/CrmRepresentante/Get_List?Id_Cd=0',
                    cache: false,
                    type: 'GET'
                }).done(function (response, textStatus, jqXHR) {
                    Estado = response.Estado;
                    listado = response.Datos;
                    //console.log(response);        

                    $('#tblRepresentantesCheck > tbody').empty();

                    if (Estado == 1) {
                        var ID = 0;
                        for (var i = 0; i < listado.length; i++) {
                            var row = $('<tr>');
                            row.append($('<td class="text-center">').append(
                                '<input id="chbRespresentante_' + i + '" data-id_rik=' + listado[i].Id_Rik + ' type="checkbox" />'
                            ));
                            row.append($('<td class="text-left" >').append(
                                '<label>' + listado[i].U_Nombre + '</label>'
                            ));
                            $('#tblRepresentantesCheck > tbody').append(row);
                        }
                    }
                    if (CALLBACK_Exito) {
                        CALLBACK_Exito();
                    }
                }).fail(function (jqXHR, textStatus, error) {
                    alertify.error('Error: CatRepresentante.CargarListadoCheck');
                    console.log(jqXHR);
                });
            }
        }

        // Evento del botón para agregar un nuevo renglón
        //$('#btnAgregarRenglon').on('click', function () {
        //    agregarRenglon(clientesTable);
        //});

        $('#btnAgregarRenglon').on('click', function () {
             $('#modalAgregar').modal('show');
           $('.modal-backdrop').remove(); 
        });


        //cerrar la pantalla de proucto 
        $('#btnCerrarProducto').on('click', function () {
            $('#modalAgregar').modal('hide');
        });

        $('#btnPropuesta').on('click', function () {


            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let representante = $('#ddlRepresentante').val();
            let nombrerepresentante = $('#ddlRepresentante').find(':selected').text();
            let telefonorik = $('#txtTelefonoRik').val();
            let fechainicio = $('#tbFechaInicio').val();

            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let tabla = $('#clientesTable').DataTable();
            let datosActuales = [];

            if (nombrerepresentante== '') 
            {
                nombrerepresentante= 'Ventas Directas';
            };

            // Itera por cada fila visible para extraer datos
            tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
                let data = this.data(); // Obtén la fila completa como un objeto

                // Verifica si el campo "NomEstatus" es diferente de "Eliminado"
                if (data.NomEstatus !== "Eliminado") {
                    let fila = {};
                    // Itera sobre las columnas para obtener datos visibles
                    tabla.columns().every(function (colIdx, tableLoop, colLoop) {
                        let celda = tabla.cell(rowIdx, colIdx).data();
                        fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
                    });
                    datosActuales.push(fila); // Agrega la fila al array
                }
            });

            
            console.log(datosActuales);
            $.ajax({
                type: "POST",
                url: "GestionIncrementoClientesDetalle.aspx/GenerarHoja2"  ,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ clientes: datosActuales, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: representante, nombrerepresentante: nombrerepresentante, telefonorik: telefonorik, fechainicio: fechainicio}),
                success: function (response) {
                    console.log('REGRESO CORRECTO');
                    let rutaArchivo = response.d;
                    if (!rutaArchivo.startsWith("Error")) {
                        // Descargar el archivo generado
                        window.location.href = rutaArchivo;
                    } else {
                        alert("Error al generar el archivo: " + rutaArchivo);
                    }
                },
                error: function (error) {
                    console.error("Error:", error.responseText);
                }
            });


            
        });

        function esCorreoValido(emails) {
            // Si está vacío, retornar false
            if (!emails) return false;

            // Expresión regular para un solo correo
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            // Dividir la cadena en correos individuales
            var arrayEmails = emails.split(';');

            // Validar cada correo
            return arrayEmails.every(function (email) {
                // Eliminar espacios en blanco al inicio y final
                email = email.trim();
                // Retornar true si el correo es válido
                return emailRegex.test(email);
            });
        }


            $('#btnEnviarCorreo').on('click', function () {


            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let representante = $('#ddlRepresentante').val();
            let id_rik = $('#ddlRepresentante').val();
            let nombrerepresentante = $('#ddlRepresentante').find(':selected').text();
            let telefonorik = $('#txtTelefonoRik').val();
            let fechainicio = $('#tbFechaInicio').val();
            var idPropuestaGP = $('#hIdPropuestaGP').val();

            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let tabla = $('#clientesTable').DataTable();
            let datosActuales = [];

            if (nombrerepresentante == '') {
                nombrerepresentante = 'Ventas Directas';
            };

            let correocliente = $('#hcorreo').val();




                let idPGP = $('#txtIdPropuestaGP').val();

             //if (!correocliente || !esCorreoValido(correocliente)) {
             //    correocliente = ""; // si no hay correo válido
             //}
                         

                     // Cancelar → abrir prompt para nuevo correo
             alertify.prompt(
                 "<b>Enviar Correo</b><br/><p>El correo se enviara a esta dirección: " + correocliente + "</p>" +
                 "<p> , que se encuentra registrada en el catálogo de clientes, si desea enviar a un correo diferente capture en el cuadro de texto y presione Ok.</p>" ,
                 function (e, nuevoCorreo) {
                     if (e) {
                         if (nuevoCorreo != "") {
                             correocliente = nuevoCorreo;
                         }
                         console.log('correo nuevo');
                         console.log(nuevoCorreo);
                         console.log(correocliente);

                         if (esCorreoValido(correocliente)) {
                             console.log("Nuevo correo:", correocliente);
                            tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
                                let data = this.data(); // Obtén la fila completa como un objeto

                                // Verifica si el campo "NomEstatus" es diferente de "Eliminado"
                                if (data.NomEstatus !== "Eliminado") {
                                    let fila = {};
                                    // Itera sobre las columnas para obtener datos visibles
                                    tabla.columns().every(function (colIdx, tableLoop, colLoop) {
                                        let celda = tabla.cell(rowIdx, colIdx).data();
                                        fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
                                    });
                                    datosActuales.push(fila); // Agrega la fila al array
                                }
                            });


                            console.log(datosActuales);
                            $.ajax({
                                type: "POST",
                                url: "GestionIncrementoClientesDetalle.aspx/GenerarPDF",
                                contentType: "application/json; charset=utf-8",
                                 data: JSON.stringify({ clientes: datosActuales, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: representante, nombrerepresentante: nombrerepresentante, telefonorik: telefonorik, fechainicio: fechainicio, correo: correocliente }),
                                success: function (response) {
                                    console.log('REGRESO CORRETO');
                                    let rutaArchivo = response.d;
                                    if (!rutaArchivo.startsWith("Error")) {
                                        // Descargar el archivo generado
                                        //window.location.href = rutaArchivo;
                                        console.log('se genero archivo correcto ' + rutaArchivo);

                                    } else {
                                        alert("Error al generar el archivo: " + rutaArchivo);
                                    }
                                },
                                error: function (error) {
                                    console.error("Error:", error.responseText);
                                }
                            });
                             
                            //var idPGP = $('#txtIdPropuestaGP').val();
                            if (!idPGP) {
                                alertify.error("No se encontró el idPropuestaGP. No se puede continuar.");
                                return;
                            }

                            $.ajax({
                                type: "POST",
                                url: "GestionIncrementoClientesDetalle.aspx/EnviarPropuesta",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify({
                                    id_cte: id_cte,
                                    nom_comercial: nom_comercial,
                                    representante: representante,
                                    nombrerepresentante: nombrerepresentante,
                                    telefonorik: telefonorik,
                                    fechainicio: fechainicio,
                                    idPropuestaGP: idPGP // <-- Este parámetro debe estar presente y con valor
                                }),
                                success: function (response) {
                                    console.log('REGRESO CORRETO');
                                    let rutaArchivo = response.d;
                                     alertify.success("La propuesta se envío al cliente al correo: " + correocliente);
                                },
                                error: function (error) {
                                    console.error("Error:", error.responseText);
                                }
                            });
                            Enable_Btn('#btnValidarPropuesta');
                            Enable_Btn('#btnAgregarRenglon');
                         } else {
                             alertify.error("Correo inválido, no se pudo enviar el correo.");
                        }
                     } else {
                         alertify.error("Operación cancelada");
                    }
                 });
            





               
                //alertify.confirm(
                //    "<b>Enviar Correo</b><br/><p>El correo se enviara a este correo: " + correocliente + "</p>" +
                //    "<p> , si desea modificarlo puede ir al catálogo de clientes y hacerlo, desea enviar el correo? De la Propuesta: </p>" + idPGP,
                //    function (e) {
                //        if (e) {
                //            // Itera por cada fila visible para extraer datos
                //            tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
                //                let data = this.data(); // Obtén la fila completa como un objeto

                //                // Verifica si el campo "NomEstatus" es diferente de "Eliminado"
                //                if (data.NomEstatus !== "Eliminado") {
                //                    let fila = {};
                //                    // Itera sobre las columnas para obtener datos visibles
                //                    tabla.columns().every(function (colIdx, tableLoop, colLoop) {
                //                        let celda = tabla.cell(rowIdx, colIdx).data();
                //                        fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
                //                    });
                //                    datosActuales.push(fila); // Agrega la fila al array
                //                }
                //            });
                //            console.log(datosActuales);
                //            $.ajax({
                //                type: "POST",
                //                url: "GestionIncrementoClientesDetalle.aspx/GenerarPDF",
                //                contentType: "application/json; charset=utf-8",
                //                data: JSON.stringify({ clientes: datosActuales, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: representante, nombrerepresentante: nombrerepresentante, telefonorik: telefonorik, fechainicio: fechainicio }),
                //                success: function (response) {
                //                    console.log('REGRESO CORRETO');
                //                    let rutaArchivo = response.d;
                //                    if (!rutaArchivo.startsWith("Error")) {
                //                        // Descargar el archivo generado
                //                        //window.location.href = rutaArchivo;
                //                        console.log('se genero archivo correcto ' + rutaArchivo);

                //                    } else {
                //                        alert("Error al generar el archivo: " + rutaArchivo);
                //                    }
                //                },
                //                error: function (error) {
                //                    console.error("Error:", error.responseText);
                //                }
                //            });
                             
                //            //var idPGP = $('#txtIdPropuestaGP').val();
                //            if (!idPGP) {
                //                alertify.error("No se encontró el idPropuestaGP. No se puede continuar.");
                //                return;
                //            }

                //            $.ajax({
                //                type: "POST",
                //                url: "GestionIncrementoClientesDetalle.aspx/EnviarPropuesta",
                //                contentType: "application/json; charset=utf-8",
                //                data: JSON.stringify({
                //                    id_cte: id_cte,
                //                    nom_comercial: nom_comercial,
                //                    representante: representante,
                //                    nombrerepresentante: nombrerepresentante,
                //                    telefonorik: telefonorik,
                //                    fechainicio: fechainicio,
                //                    idPropuestaGP: idPGP // <-- Este parámetro debe estar presente y con valor
                //                }),
                //                success: function (response) {
                //                    console.log('REGRESO CORRETO');
                //                    let rutaArchivo = response.d;
                //                    alertify.success("La propuesta se envío al cliente al correo registrado: " + correocliente);
                //                },
                //                error: function (error) {
                //                    console.error("Error:", error.responseText);
                //                }
                //            });
                //            Enable_Btn('#btnValidarPropuesta');
                //            Enable_Btn('#btnAgregarRenglon');
                //           // Disable_Btn('#btnEnviarAcys');
                //        }
                //    }
                //);


            
        });

        $('#btnExportarExcel').click(function () {
            // Obtén los datos visibles del DataTable
            $('#spinner_Exportar').css('display', 'block');

            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let representante = $('#ddlRepresentante').val();
            let nombrerepresentante = $('#ddlRepresentante').find(':selected').text();
            let telefonorik = $('#txtTelefonoRik').val();
            let fechainicio = $('#tbFechaInicio').val();

            console.log(telefonorik);
            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let tabla = $('#clientesTable').DataTable();
            let datosActuales = [];




            // Itera por cada fila visible para extraer datos
            //tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
            //    let data = this.data(); // Obtén la fila completa como un objeto

            //    // Verifica si el campo "NomEstatus" es diferente de "Eliminado"
            //    if (data.NomEstatus !== "Eliminado") {
            //        let fila = {};
            //        // Itera sobre las columnas para obtener datos visibles
            //        tabla.columns().every(function (colIdx, tableLoop, colLoop) {
            //            let celda = tabla.cell(rowIdx, colIdx).data();
            //            fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
            //        });
            //        datosActuales.push(fila); // Agrega la fila al array
            //    }
            //});

            tabla.rows({ filter: 'applied' }).every(function (rowIdx, tableLoop, rowLoop) {
                let data = this.data(); // Obtén los datos de la fila

                if (data.NomEstatus !== "Eliminado") {
                    let fila = {
                        // Incluye solo las propiedades que necesitas
                        Id_Prd: data.Id_Prd,
                        NomProducto: data.NomProducto,
                        NomCategoria: data.NomCategoria,
                        Unidades: data.Unidades,
                        PrecioVenta: data.PrecioVenta,
                        Ventas: data.Ventas,
                        MgRed_MensualPesos: data.MgRed_MensualPesos,
                        MgRed_MensualPorc: data.MgRed_MensualPorc,
                        PrecioObjetivoProy: data.PrecioObjetivoProy,
                        CostoAAAAFuturo: data.CostoAAAAFuturo,
                        PrecioListaProy: data.PrecioListaProy,
                        PrecioMinRikProy: data.PrecioMinRikProy,
                        PrecioGteProy: data.PrecioGteProy,

                        PrecioNegociadoProy: data.PrecioNegociadoProy,
                        PorcIncrementoProy: data.PorcIncrementoProy,
                        DescuentoSobrePlistaProy: data.DescuentoSobrePlistaProy,
                        UnidadesProyectadas: data.UnidadesProyectadas,
                        VentaProy: data.VentaProy,
                        MgRed_PesosProy: data.MgRed_PesosProy,
                        MgRed_PorcProy: data.MgRed_PorcProy,
                        Comentarios : data.Comentarios,
                       
                        NomEstatus: data.NomEstatus,
                        Id_Pc: data.Id_Pc,
                        Pc_NoConvenio: data.Pc_NoConvenio,
                        PC_Nombre: data.PC_Nombre

                    };
                    datosActuales.push(fila);
                }
            }); 
              
     


            console.log(datosActuales);
            $.ajax({
                type: "POST",
                url: "GestionIncrementoClientesDetalle.aspx/GenerarListado",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ clientes: datosActuales, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: representante, nombrerepresentante: nombrerepresentante, telefonorik: telefonorik, fechainicio: fechainicio }),
                success: function (response) {
                    console.log('REGRESO CORRETO');
                    let rutaArchivo = response.d;
                    if (!rutaArchivo.startsWith("Error")) {
                        // Descargar el archivo generado
                        window.location.href = rutaArchivo;
                    } else {
                        alert("Error al generar el archivo: " + rutaArchivo);
                    }
                },
                error: function (error) {
                    console.error("Error:", error.responseText);
                }
            });

         
            $('#spinner_Exportar').css('display', 'none');
           

            //console.log(datosActuales);
            //alert("generar listado");

            //$.ajax({
            //    type: "POST",
            //    url: "GestionIncrementoClientesDetalle.aspx/GenerarListado",
            //    contentType: "application/json; charset=utf-8",
            //    data: JSON.stringify({ clientes: datosActuales }),
            //    success: function (response) {
            //        console.log('REGRESO CORRETO y trato de abrir el archivo');
            //        console.log("ruta archivo");
            //        console.log(response.d);
            //        let rutaArchivo = response.d;
            //        if (!rutaArchivo.startsWith("Error")) {
            //            // Descargar el archivo generado
            //            window.location.href = rutaArchivo;
            //        } else {
            //            alert("Error al generar el archivo: " + rutaArchivo);
            //        }
            //    },
            //    error: function (error) {
            //        console.error("Error:", error.responseText);
            //    }
            //});

            //// Enviar datos al servidor
            //$.ajax({
            //     url: 'GestionIncrementoClientesDetalle.aspx/ExportarAExcel',
            //    type: 'POST',
            //    contentType: 'application/json; charset=utf-8',
            //    data: JSON.stringify({ clientes: datosActuales }), // Enviar como JSON
            //    success: function (response) {
            //        // Descargar el archivo Excel
            //        console.log("datosok");
            //        let blob = new Blob([response.d], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            //        let link = document.createElement('a');
            //        link.href = window.URL.createObjectURL(blob);
            //        link.download = "Clientesdetalles.xlsx";
            //        link.click();
            //    },
            //    error: function (xhr, status, error) {
            //        console.error("Error al generar el archivo Excel:", error);
            //        alert("Ocurrió un error al generar el archivo Excel.");
            //    }
            //});
        });


        $('#btnIncremento_Guardar').click(function () {
            // Obtén los datos visibles del DataTable
            //jfcv alerta de precio mayo 2025
            var validaralertas = 0;

            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let id_rik = $('#ddlRepresentante').val();
            let nombrerepresentante = $('#ddlRepresentante').find(':selected').text();
            let telefonorik = $('#txtTelefonoRik').val();
            let fechainicio = $('#tbFechaInicio').val();
            let id_tamaño = $('#txtIdTamaño').val();
            let tipocuenta = $('#txtTipoCuenta').val();
            let id_matriz = $('#txtIdMatriz').val();
            let nombre_matriz = $('#txtNombreMatriz').val();
             
            console.log('valor de rik');
            console.log(id_rik);
            if (id_rik == null)
            {
                id_rik = representante;
                nombrerepresentante = 'Ventas Directas';
                console.log('valor de rik cuando entro a id_rik = 0 ');
                console.log(id_rik);
            };

            if (!fechainicio) { 
                alertify.error("Debes seleccionar una fecha antes de guardar."); 
                return false;  
            }
             
            var hoy = new Date();
            hoy.setHours(0, 0, 0, 0);  // Lo iniciamos a 00:00 horas
            if (hoy <= fechainicio) {

                alertify.error("Debes seleccionar una fecha mayor a hoy.");
                console.log('entro por aqui debio mandar aviso');
            }
            console.log(hoy);

              


            if (!telefonorik) {
                alertify.error("Debes seleccionar un telefono antes de validar propuesta.");
                return false;
            }

 
            console.log(telefonorik);
            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let tabla = $('#clientesTable').DataTable();
            let datosActuales = [];      
            let i = 0;

            
            Spinner = $('#spinner');

            tabla.rows({ filter: 'applied' }).every(function (rowIdx, tableLoop, rowLoop) {
                let data = this.data(); // Obtén los datos de la fila
                        
                    let fila = {
                        // Incluye solo las propiedades que necesitas
                        Id_Prd: data.Id_Prd,
                        NomProducto: data.NomProducto,
                        NomCategoria: data.NomCategoria,
                        Unidades: data.Unidades,
                        PrecioVenta: data.PrecioVenta,
                        Ventas: data.Ventas,
                        MgRed_MensualPesos: data.MgRed_MensualPesos,
                        MgRed_MensualPorc: data.MgRed_MensualPorc,
                        PrecioObjetivoProy: data.PrecioObjetivoProy,
                        CostoAAAAFuturo: data.CostoAAAAFuturo,
                        PrecioListaProy: data.PrecioListaProy,
                        PrecioMinRikProy: data.PrecioMinRikProy,
                        PrecioGteProy: data.PrecioGteProy,

                        PrecioNegociadoProy: data.PrecioNegociadoProy,
                        PorcIncrementoProy: data.PorcIncrementoProy,
                        DescuentoSobrePlistaProy: data.DescuentoSobrePlistaProy,
                        UnidadesProyectadas: data.UnidadesProyectadas,
                        VentaProy: data.VentaProy,
                        MgRed_PesosProy: data.MgRed_PesosProy,
                        MgRed_PorcProy: data.MgRed_PorcProy,
                        Comentarios: data.Comentarios,

                        NomEstatus: data.NomEstatus,
                        Id_Pc: data.Id_Pc,
                        Pc_NoConvenio: data.Pc_NoConvenio,
                        PC_Nombre: data.PC_Nombre

                    };
                datosActuales.push(fila);
                console.log(data.PrecioObjetivoProy);
                console.log(data.PrecioNegociadoProy);
                console.log("producto");
                console.log(data.Id_Prd);

                //may 21 2025 solo entre a validar si precio negociado es menor a PrecioObjetivoProy
     
            });



            console.log("despues de ejecutar AlertaPrecioValidaPrecioGPMA, guardo");


          /*  console.log(datosActuales);*/
            $.ajax({
                type: "POST",
                url: "GestionIncrementoClientesDetalle.aspx/Guardar",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ clientes: datosActuales, id_tamaño: id_tamaño, tipocuenta: tipocuenta, id_matriz: id_matriz, nombre_matriz:nombre_matriz, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: id_rik, nombrerepresentante: nombrerepresentante, telefonorik: telefonorik, fechainicio: fechainicio }),
                success: function (response) {
                    console.log('REGRESO CORRETO');
                    let id_ReporteGp = response.d;
                    console.log(id_ReporteGp);
                    alertify.success('Información actualizada. No. de Propuesta asignado: ' + id_ReporteGp);
                    $("#txtIdPropuestaGP").val(id_ReporteGp);

                    // Después de guardar, arma el array de productos a validar
                    let productosAValidar = [];
                    tabla.rows({ filter: 'applied' }).every(function (rowIdx, tableLoop, rowLoop) {
                        let data = this.data();
                        if (parseFloat(data.PrecioObjetivoProy) > parseFloat(data.PrecioNegociadoProy)) {
                            if (data.Id_Ter == null) {
                                data.Id_Ter = 1;
                            }
                            console.log(data.NomEstatus);
                            console.log(id_rik);
                            console.log(data.PrecioObjetivoProy.toString().replace('.', ','));
                            console.log(data.PrecioObjetivoProy);

                            if (data.NomEstatus != 'Eliminado') {
                                productosAValidar.push({
                                    Id_Emp: data.Id_Emp,
                                    Id_Cd: data.Id_Cd,
                                    Id_Cte: id_cte,
                                    Id_Prd: data.Id_Prd,
                                    Id_Ter: parseInt(data.Id_Ter, 10),
                                    PrecioNegociadoProy: parseFloat(data.PrecioNegociadoProy) || 0,
                                    UnidadesProyectadas: parseFloat(data.UnidadesProyectadas) || 0,
                                    NomProducto: data.NomProducto,
                                    NomComercial: nom_comercial,
                                    PrecioObjetivoProy: parseFloat(data.PrecioObjetivoProy) || 0,
                                    Comentarios: data.Comentarios || "",
                                    Id_Rik: id_rik,
                                    Nom_Estatus: data.NomEstatus || "",
                                    Id_reporteGP: id_ReporteGp
                                });
                            }
                        }
                    });

                    console.log('productosAValidar');
                    console.log(productosAValidar);

                    // Llama la función masiva
                    AlertaPrecioValidaPrecioGPMA_Masivo(productosAValidar, function (resultados, error) {
                        if (error) {
                            alertify.error("Error al validar precios.");
                            return;
                        }
      
                        if (resultados && resultados.length > 0) {
                            validaralertas = 1;
                            console.log('valida alertas vale 1');
                            console.log('validaralertas');
                            console.log(validaralertas);
                           /* if (validaralertas == 1) {*/
                               
                                $('#ModalAlerta').appendTo("body").modal('hide');

                            //$("#ModalAlerta").modal({ "backdrop": "static" });

                            ////aqui puedo poner los datos de venta proy, id_rik etc en las variables d ela pantalla porque ya esta abierta?
                            //$('#ModalAlerta').modal('show');

                            //$('#modalAlertaContent').html('<div class="text-center"><img src="Img/patternfly/spinner.gif" alt="Cargando..."></div>');
                            //$.get('../Ventana_AutorizacionPrecios.aspx', function (data) {
                    //            $('#modalAlertaContent').html(data);
                            //}).fail(function () {
                    //            $('#modalAlertaContent').html('<div class="alert alert-danger">Error al cargar la autorización.</div>');
                            //});


                            //$('#ModalAlerta').modal({ "backdrop": "static" });
                            //$('#ModalAlerta').modal('show');
                            const contenedor = document.getElementById("contenedorFrame");
                            contenedor.innerHTML = ''; // Limpia cualquier iframe anterior

                            document.getElementById("spinnerIframe").style.display = "block";

                            const iframe = document.createElement("iframe");
                            iframe.className = "embed-responsive-item";
                            iframe.style.width = "100%";
                            iframe.style.height = "600px";
                            iframe.style.border = "none";
                            iframe.src = "../Ventana_AutorizacionPrecios.aspx";

                            contenedor.appendChild(iframe);
                            contenedor.style.display = "block";

                            $('#ModalAlerta').modal('show'); // Abre modal Bootstrap

                            iframe.onload = function () {
                                document.getElementById("spinnerIframe").style.display = "none";
                            };

                                return;

                         /*   }*/


                            }

                    });
                  
                },
                error: function (error) {
                    console.error("Error:", error.responseText);
                }
            });


            /*$('#spinner').hide();*/
            console.log('cerrando spinner guardar ok');
            $('#spinner').css('display', 'none');

            /*estatus*/
            Enable_Btn('#btnEnviarCorreo');
            Enable_Btn('#btnPropuesta');
            Disable_Btn('#btnValidarPropuesta');
            Enable_Btn('#btnAgregarRenglon');
            //Disable_Btn('#btnEnviarAcys');
           
            //if (nomestatus == 'Analizar') {
            //    Enable_Btn('#btnEnviarCorreo');
            //    Enable_Btn('#btnPropuesta');
            //    Enable_Btn('#btnValidarPropuesta');
            //    Enable_Btn('#btnAgregarRenglon');

            //}
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            // JFCV alertadeprecios 
            // Si tengo registros con precio menor al de rik habro ventana de autorización 
            //
 

        });

        $('#btnValidarPropuesta').click(function () {
            // Obtén los datos visibles del DataTable


            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let id_rik = $('#ddlRepresentante').val();
            let nombrerepresentante = $('#ddlRepresentante').find(':selected').text();
            let telefonorik = $('#txtTelefonoRik').val();
            let fechainicio = $('#tbFechaInicio').val();
            let id_tamaño = $('#txtIdTamaño').val();
            let tipocuenta = $('#txtTipoCuenta').val();
            let id_matriz = $('#txtIdMatriz').val();
            let nombre_matriz = $('#txtNombreMatriz').val();
            
            console.log('valor de rik');
            console.log(id_rik);
            if (id_rik == null) {
                id_rik = representante;
                nombrerepresentante = 'Ventas Directas';
                console.log('valor de rik cuando entro a id_rik = 0 ');
                console.log(id_rik);
            };

            if (!fechainicio) {
                alertify.error("Debes seleccionar una fecha antes de validar propuesta.");
                return false;
            }

            var hoy = new Date();
            hoy.setHours(0, 0, 0, 0);  // Lo iniciamos a 00:00 horas
            if (hoy <= fechainicio) {

                alertify.error("Debes seleccionar una fecha mayor a hoy.");
                console.log('entro por aqui debio mandar aviso');
            }
            console.log(hoy);
            


            if (!telefonorik) {
                alertify.error("Debes seleccionar un telefono antes de validar propuesta.");
                return false;
            }
            console.log(telefonorik);
            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let datosActuales = [];



            //if (confirm(
            //    "Al validar la propuesta de este cliente, ya no podrá ser modificada en el GPMA por incremento de costos." +
            //    "Cualquier cambio deberá hacerse en el módulo de proyectos del CRM.")) {
            alertify.confirm(
                "<b>Validar propuesta</b><br/><p>Al validar la propuesta de este cliente, se programara para que se actualicen estos precios en el ACYS en la fecha </p>" +
                "<b>" + fechainicio + "</b>   pactada y ya no se podra modificar el GPMA por incremento de costos."+
                "<p>Cualquier cambio deberá hacerse en el módulo de proyectos del CRM.</p>",
                function (e) {
                if (e) {
                    
                        // Mostrar spinner
                        $('#spinner').css('display', 'block');

                        console.log('✅ Validación aceptada, iniciando proceso AJAX...');
                        // Validar que las variables no sean undefined antes de enviarlas
                        if (typeof datosActuales === "undefined" || typeof id_tamaño === "undefined" ||
                            typeof tipocuenta === "undefined" || typeof id_matriz === "undefined") {
                            alertify.error("Faltan datos para procesar la solicitud.");
                            $('#spinner').css('display', 'none');
                            return;
                        }

                        let idPGP = $('#txtIdPropuestaGP').val();

                        // Enviar datos mediante AJAX
                        $.ajax({
                            type: "POST",
                            url: "GestionIncrementoClientesDetalle.aspx/Cerrar",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({
                                clientes: datosActuales,
                                id_tamaño: id_tamaño,
                                tipocuenta: tipocuenta,
                                id_matriz: id_matriz,
                                nombre_matriz: nombre_matriz,
                                dias: dias,
                                id_cte: id_cte,
                                nom_comercial: nom_comercial,
                                representante: id_rik,
                                nombrerepresentante: nombrerepresentante,
                                telefonorik: telefonorik,
                                fechainicio: fechainicio,
                                IdPropuestaGP : idPGP
                            }),
                            success: function (response) {
                                console.log('✅ Respuesta recibida:', response);

                                //if (response.d) { // Asegurar que el servidor devuelve el dato esperado
                                    alertify.success('✅ Información actualizada, la propuesta ha sido validada.');
                                //} else {
                                //    alertify.success("⚠ La respuesta no contiene datos esperados.");
                                //}

                                // Habilitar botones
                                Enable_Btn('#btnEnviarCorreo');
                                Enable_Btn('#btnPropuesta');
                                Enable_Btn('#btnValidarPropuesta');
                                Enable_Btn('#btnAgregarRenglon');
                         //       Enable_Btn('#btnEnviarAcys');
                            },
                            error: function (error) {
                                console.error("❌ Error en AJAX:", error.responseText);
                                alertify.error("❌ Hubo un problema al validar la propuesta.");
                            },
                            complete: function () {
                                // Ocultar spinner en todos los casos
                                $('#spinner').css('display', 'none');
                            }
                        }) 
                }
                  else {
                    // user clicked "cancel"
                    //alert("y");
                }
            });

           /* }*/

        });

        //quitar este metodo 
        $('#btnExcel').on('click', function () {


            let dataTable = $('#clientesTable').DataTable();
            let dias = $('#txtDiasVigencia').val();
            console.log(dias);
            let data = [];
            dataTable.rows().every(function () {
                let rowData = this.data();
                data.push(rowData);
            });
            let id_cte = $('#txtCliente').val();
            let nom_comercial = $('#txtCatNomComercial').val();
            let representante = $('#ddlRepresentante').val();
            console.log(data);
            /* let datosActuales = $('#clientesTable').DataTable().rows().data().toArray();*/
            let tabla = $('#clientesTable').DataTable();
            let datosActuales = [];

            //// Itera por cada fila visible para extraer datos
            //tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
            //    let data = this.data();

            //    // Itera sobre las celdas para obtener datos visibles
            //    let fila = {};
            //    tabla.columns().every(function (colIdx, tableLoop, colLoop) {
            //        let celda = tabla.cell(rowIdx, colIdx).data();
            //        fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
            //    });

            //    datosActuales.push(fila);
            //});


            // Itera por cada fila visible para extraer datos
            tabla.rows().every(function (rowIdx, tableLoop, rowLoop) {
                let data = this.data(); // Obtén la fila completa como un objeto

                // Verifica si el campo "NomEstatus" es diferente de "Eliminado"
                if (data.NomEstatus !== "Eliminado") {
                    let fila = {};
                    // Itera sobre las columnas para obtener datos visibles
                    tabla.columns().every(function (colIdx, tableLoop, colLoop) {
                        let celda = tabla.cell(rowIdx, colIdx).data();
                        fila[tabla.column(colIdx).dataSrc()] = celda; // Usa el nombre del campo como clave
                    });
                    datosActuales.push(fila); // Agrega la fila al array
                }
            });

            console.log(datosActuales);
            $.ajax({
                type: "POST",
                url: "GestionIncrementoClientesDetalle.aspx/GenerarHoja2",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ clientes: datosActuales, dias: dias, id_cte: id_cte, nom_comercial: nom_comercial, representante: representante }),
                success: function (response) {
                    console.log('REGRESO CORRETO');
                    let rutaArchivo = response.d;
                    if (!rutaArchivo.startsWith("Error")) {
                        // Descargar el archivo generado
                        window.location.href = rutaArchivo;
                    } else {
                        alert("Error al generar el archivo: " + rutaArchivo);
                    }
                },
                error: function (error) {
                    console.error("Error:", error.responseText);
                }
            });


            // Enviar datos para generar el Excel
            //$.ajax({
            //    type: "POST",
            //    url: "GestionIncrementoClientesDetalle.aspx/ExportarAExcel",
            //    contentType: "application/json; charset=utf-8",
            //    data: JSON.stringify({ data: data }),
            //    success: function (response) {
            //        console.log(response.d); // Debug
            //        // Redirigir al archivo exportado
            //        const fileUrl = response.d; // El WebMethod devuelve la URL del archivo
            //        window.location.href = fileUrl;
            //    },
            //    error: function (error) {
            //        console.error("Error al exportar:", error);
            //        alert("Hubo un error al exportar el archivo.");
            //    }
            //});
        });

        $('#btnGuardarProducto').on('click', function () {
            let precioNegociadotext = $('#tbPrecioNegociadoProy_3').val();
            let precioNegociado = parseFloat(precioNegociadotext.replace(/,/g, '')).toFixed(2);
            console.log('precioNegociado');
            console.log(precioNegociado);
          /*  let precioNegociado = parseFloat($('#tbPrecioNegociadoProy_3').val()) || 0.0;*/
            let precioLista = parseFloat($('#lbPrecioListaProy_3').text()) || 0.0;
            let porcIncrementoProy = ((precioNegociado - precioLista) / (precioLista || 1)) * 100;       
            porcIncrementoProy = porcIncrementoProy < 0 ? 0 : porcIncrementoProy;
            if (precioNegociado == null || precioNegociado <= 0) {
                alertify.alert('Debe capturar el precio negociado y debe ser mayor a cero.');
                return false;
            }
            if ($('#lbNomCategoria_3').text() == null || $('#lbNomCategoria_3').text() == "") {
                alertify.alert('Debe alimentar un producto valido para poder agregarlo');
                return false;
            }
            const nuevoRenglon = {
                //Id_Rik: $('#ddlRepresentante').val(),
                //Nombre_Rik: $('#ddlRepresentante').text(),
                //Id_Cte: $('#txtCliente').val(), Cte_NomComercial: $('#txtCatNomComercial').val(),
                //Id_Matriz: $('#txtIdmatriz').val(),
               
                Id_Prd: $('#tbCodigo_3').val(),
                NomProducto: $('#lbPrdDescripcion_3').text(),
                NomCategoria: $('#lbNomCategoria_3').text(),
                Unidades: parseFloat($('#tbCant_3').val()) || 1,
                /* PrecioVenta: parseFloat($('#tbPrecio_3').val()),
                 Ventas: (parseFloat($('#lbPrecioLista_3').text()) || 0.0) * (parseInt($('#tbCant_3').val()) || 1),
                 en ceros a petición de juanjo*/
                PrecioVenta: 0,
                Ventas: 0 ,
                MgRed_MensualPesos: 0.0,
                MgRed_MensualPorc: 0.0,
                //TipoCuenta: $('#txtTipoCuenta').val(),
                PrecioObjetivoProy: parseFloat($('#lbPrecioObjetivoProy_3').text().replace(/,/g, '')).toFixed(2) ,
                CostoAAAAFuturo: parseFloat($('#lbCostoAAAAFuturo_3').text().replace(/,/g, '')).toFixed(2),

                PrecioListaProy: parseFloat($('#lbPrecioListaProy_3').text().replace(/,/g, '')).toFixed(2),
                PrecioMinRikProy: parseFloat($('#lbPrecioMinRikProy_3').text().replace(/,/g, '')).toFixed(2),
                PrecioGteProy: parseFloat($('#lbPrecioGteProy_3').text().replace(/,/g, '')).toFixed(2),
               /* Id_Tamano: $('#txtIdTamaño').val(),*/
        
                
                //PrecioNegociadoProy: parseFloat($('#tbPrecioNegociadoProy_3').val()) ,
                PrecioNegociadoProy: precioNegociado,
                PorcIncrementoProy: parseFloat($('#lbPorcIncrementoProy_3').text().replace(/,/g, '')).toFixed(2),
                DescuentoSobrePlistaProy: parseFloat($('#lbDescuentoSobrePlistaProy_3').text().replace(/,/g, '')).toFixed(2),
                DescuentoSobrePlistaProy: precioLista - precioNegociado,
                UnidadesProyectadas: parseFloat($('#tbCant_3').val()) || 1,
                VentaProy: parseInt($('#lbVentaProy_3').text().replace(/,/g, '')).toFixed(2),
                MgRed_PesosProy: parseFloat($('#lbMgRed_PesosProy_3').text().replace(/,/g, '')).toFixed(2),
                MgRed_PorcProy: parseFloat($('#lbMgRed_PorcProy_3').text()),
                VarPpbRed_Porc: parseFloat($('#lbVarPpbRed_Porc_3').text()),
                Comentarios: $('#tbComentarios_3').val(),
               
                NomEstatus: $('#lbNomEstatus_3').text(),
                Id_Pc: 0
                 
            };
        
            ///Calcular valores de columnas 
            let unidades = nuevoRenglon.UnidadesProyectadas;
            console.log('unidades');
            console.log(unidades);
            if (unidades == null || unidades <= 0 ) {
                alertify.alert('Las unidades proyectadas deben ser mayor a cero.');
                return false;
            }

            let precioNegociadoProy = nuevoRenglon.PrecioNegociadoProy;
            let ventasProyectadas = nuevoRenglon.VentaProy;
            porcIncrementoProy = nuevoRenglon.PorcIncrementoProy;
            let precioListaProyectado = nuevoRenglon.PrecioListaProy ;
            let mgRed_PesosProy = nuevoRenglon.MgRed_PesosProy ;
            let mgRed_PorcProy = nuevoRenglon.MgRed_PorcProy;
            let varPpbRed_Porc = nuevoRenglon.VarPpbRed_Porc;
            let mgRed_MensualPorc = nuevoRenglon.MgRed_MensualPorc;
            let costoAAAAFuturo = nuevoRenglon.CostoAAAAFuturo ;
            let descuentoSobrePlistaProy = nuevoRenglon.DescuentoSobrePlistaProy ;
            /*let PrecioVenta = nuevoRenglon.PrecioVenta ;*/
  
            // Calcular ventas proyectadas
            ventasProyectadas = unidades * precioNegociadoProy;
            //if (PrecioVenta == 0) {
                porcIncrementoProy = 0;
            //}
            //else {
            //    porcIncrementoProy = (precioNegociadoProy - PrecioVenta) / PrecioVenta;
            //}

             

            descuentoSobrePlistaProy = (precioListaProyectado - precioNegociadoProy);
            mgRed_PesosProy = ventasProyectadas - (unidades * costoAAAAFuturo);
            mgRed_PorcProy = (ventasProyectadas - (unidades * costoAAAAFuturo)) / ventasProyectadas;
            varPpbRed_Porc = mgRed_MensualPorc - mgRed_PorcProy;
            nuevoRenglon.UnidadesProyectadas  = unidades.toFixed(2);
            nuevoRenglon.precioNegociadoProy  = precioNegociadoProy;
            nuevoRenglon.VentaProy  = ventasProyectadas.toFixed(2);
            nuevoRenglon.PorcIncrementoProy  = porcIncrementoProy.toFixed(2);
            nuevoRenglon.MgRed_PesosProy  = mgRed_PesosProy.toFixed(2);
            nuevoRenglon.MgRed_PorcProy = mgRed_PorcProy.toFixed(2);
            nuevoRenglon.DescuentoSobrePlistaProy = descuentoSobrePlistaProy.toFixed(2);
            nuevoRenglon.MgRed_PesosProy = mgRed_PesosProy;
            nuevoRenglon.MgRed_PorcProy = mgRed_PorcProy;
            nuevoRenglon.VarPpbRed_Porc = varPpbRed_Porc;

            console.log('agregar nuevo renglon');
            // Agregar el nuevo renglón al DataTable
            tablaClientes.row.add(nuevoRenglon).draw(false);

            console.log('tablaClientes row add finalizado');
         
           /* $('#RonNo_' + RowNo).remove();*/

           /* document.getElementById("tblAcuerdoProds").deleteRow(0);
            me borro el encabezado también*/

           /* var tabla = $('#tblAcuerdoProds').DataTable();
            tabla.row(0).remove().draw(false);
            me marcaba error a veces 
             //tabla.row(0).remove().draw();
             */
            //console.log(tabla);
            const tbody = document.querySelector("#tblAcuerdoProds tbody");
 
            while (tbody.firstChild) {
                tbody.removeChild(tbody.firstChild);
            }
            
            console.log('tblAcuerdoProds removi renglon');
            // Cerrar el modal
            $('#modalAgregar').modal('hide');
             

            alertify.success('Nuevo producto agregado');
        });
   

        function windowsClose() {
            $('.modal').modal('hide');
            window.parent.cerrarpantalla();
        }

    </script>
 
         <script type="text/javascript">
             var   _ApplicationUrl = '<%= ResolveUrl("~/") %>';
             var _ApplicationUrl2 = '<%=ApplicationUrl %>';
              
             $('#hurl').val(_ApplicationUrl);
             $('#txtproducto').val(_ApplicationUrl);
             console.log(_ApplicationUrl);
         </script>

    <script src="<%=Page.ResolveUrl("~/js/Login.js?v=20220126")%>"></script>         
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Func.js?v=20220126")%>"></script>    

    <script src="<%=Page.ResolveUrl("~/js/GP/GP_Index.js?v=20220126")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20210822")%>"></script>

   </asp:Content>
   
