<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master"     AutoEventWireup="true" CodeBehind="CapAlertaAutorizacionGPMaDet.aspx.cs" Inherits="SIANWEB.CapAlertaAutorizacionGPMaDet" %>

 

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

<!-- jQuery -->
 
  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- DataTables -->
<link href="<%=Page.ResolveUrl("~/Librerias/DataTables/datatables.min.css")%>" rel="stylesheet" />
<script src="<%=Page.ResolveUrl("~/Librerias/DataTables/datatables.min.js")%>"></script>



<!-- Font Awesome -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />



<!-- Alertify -->
<link href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>" rel="stylesheet" />
<link href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>" rel="stylesheet" />
<script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>

<!-- Bootstrap -->
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
     <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
      


    <style>
        .info-header {
            background-color: #f5f5f5;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .info-label {
            font-weight: bold;
            margin-right: 5px;
            color: #333;
        }
        .info-value {
            color: #0056b3;
        }
        .percentage {
            color: #28a745;
        }
        .currency {
            color: #0056b3;
        }
        .btn-circle {
            width: 30px;
            height: 30px;
            padding: 0;
            border-radius: 15px;
            text-align: center;
            font-size: 12px;
            line-height: 30px;
            display: inline-block;
            margin: 0 2px;
            }

        .btn-group-actions {
            white-space: nowrap;
            text-align: center;
        }

        .btn-group-actions .btn {
            float: none;
            display: inline-block;
        }

        .btn-circle i {
            margin: 0;
            line-height: inherit;
        }
        .input-tiny {
                width: 30px !important;
                padding: 6px 3px !important;
                text-align: center !important;
            }

        .input-small {
            width: 50px !important;
            padding: 6px 3px !important;
            text-align: center !important;
        }

        .input-container {
            display: flex;
            gap: 5px;
        }

        .total-venta-destacado {
            font-size: 1.3em;
            font-weight: bold;
            color: #007bff; /* Azul Bootstrap */
            background: #e9f7fe;
            padding: 6px 12px;
            border-radius: 6px;
            margin-right: 10px;
        }

        .porcentaje-destacado {
            font-size: 1.2em;
            font-weight: bold;
            color: #28a745; /* Verde Bootstrap */
            background: #eafbe7;
            padding: 6px 12px;
            border-radius: 6px;
        }
        .botones-acciones {
            margin-top: 15px; /* Espacio entre el total y los botones */
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">
        <!-- Header Info -->
        <div id="spinner" style="display: none; position: fixed; top: 0; left: 0; width: 100vw; height: 100vh; background: rgba(255,255,255,0.7); z-index: 9999; text-align: center;">
            <div style="position: relative; top: 40vh;">
                <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                <div>Cargando datos...</div>
            </div>
        </div>
        <div class="row info-header">
         <div class="col-md-1">
                     <input type="text" id="txtUsuario" width="1" class="form-control" runat="server" onpaste="return false"
                         readonly="readonly" style="display: none" />
                     <input type="text" id="txtIdAutorizacionPrecio" width="1" class="form-control" runat="server" onpaste="return false"
                         readonly="readonly" style="display: none" />
                     <input type="text" id="txtId_Cpr" width="1" class="form-control" runat="server" onpaste="return false"
                         readonly="readonly" style="display: none" />
                     <input type="text" id="txtId_Cd" width="1" class="form-control" runat="server" onpaste="return false"
                         readonly="readonly" style="display: none" />
            </div>
        </div>
         <div class="row">
             <div class="col-md-4">
                 <div class="form-group">
                     <div class="col-md-1">
                         <b></b>
                         </div>
                     <div class="col-md-3">
                 
                            Fecha de la Solicitud
                          </div>
                     <div class="col-md-4">
                              <input type="text" id="txtFecha" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                            readonly="readonly" />

                     </div>
             

                 </div>
             </div>
             <div class="col-md-4">
                 <div class="form-group">
                     <div class="col-md-3">
                         Núm. de Solicitud 
          
                     </div>
                     <div class="col-md-3">
                         <input type="text" id="txtSolicitudAutorizacion" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                             readonly="readonly" />
                     </div>

                     <div class="col-md-3">
                          Tipo de solicitud
                     </div>
                     <div class="col-md-3">
                    
                           <input type="text" id="txtTipoAutorizacion" class="form-control" runat="server" visible="false" onpaste="return false" style="text-align: right"
readonly="readonly" />
                           <input type="text" id="txtNomTipoSolicitud" class="form-control" runat="server" onpaste="return false" style="text-align: right"
readonly="readonly" />
                     </div>


                 </div>
             </div>
              <div class="col-md-4">
            
                    <div class="col-md-3">
                                 Estatus:
                             </div>
                             <div class="col-md-3">
               
                                                            <input type="text" id="txtNomEstatus" class="form-control" runat="server" onpaste="return false" style="text-align: right"
readonly="readonly" />
                             </div>


             </div>
         </div>
         <div class="row">
             <div class="col-md-12">
                 <b></b>
             </div>
         </div>
          


        <div class="row info-header">
            <div class="col-md-1">
                <span class="info-label">Cliente:</span>
                <span id="lblCliente" runat="server" class="info-value"></span>
            </div>
            <div class="col-md-2">
                <span class="info-label">Nombre:</span>
                <span id="lblNombre" runat="server" class="info-value"></span>
            </div>
            <div class="col-md-2">
                      <div class="col-md-4">
                         <span class="info-label">Tipo Cte</span>
                         <span id="Span2" runat="server" class="info-value"></span>
                     </div>
                     <div class="col-md-3">
                        <span class="info-label">Tamaño</span>
                        <span id="Span3" runat="server" class="info-value"></span>
                    </div>
                    <div class="col-md-5">
                            <span class="info-label">Territorio:</span>
                            <span id="lblTerritorio" runat="server" class="info-value"></span>
                    </div>

             </div>
            
            <div class="col-md-1">
                <span class="info-label">Venta Poyectada</span>
                <span id="lblVentas" runat="server" class="currency"></span>
            </div>
            <div class="col-md-1">
                <span class="info-label">Utilidad Prima</span>
                <span id="lblUtilPrima" runat="server" class="currency"></span>
            </div>
            <div class="col-md-1">
                <span class="info-label">% Utilidad Prima</span>
                <span id="lblPorcUtilPrima" runat="server" class="percentage"></span>
            </div>
            <div class="col-md-1">
                <span class="info-label">UAFIR Mensual</span>
                <span id="lblUafirMes" runat="server" class="currency"></span>
            </div>
            <div class="col-md-1">
                <span class="info-label">UAFIR Anual</span>
                <span id="lblUafirAnual" runat="server" class="currency"></span>
            </div>
            <div class="col-md-1">
                <span class="info-label">% UAFIR Cliente</span>
                <span id="lblPorcUafir" runat="server" class="percentage"></span>
            </div>
             <div class="col-md-1">
                 <span class="info-label">Ut. Remanente</span>
                 <span id="Span1" runat="server" class="currency"></span>
             </div>
        </div>

          <div class="row">
      <div class="col-md-12">
          <div class="form-group">
              <div class="col-md-1">
                  <input type="text" id="txtCliente" class="form-control" runat="server" onpaste="return false"
                      readonly="readonly" />
              </div>
              <div class="col-md-2">
                  <input type="text" id="txtClienteNombre" class="form-control" runat="server" onpaste="return false"
                      readonly="readonly" />
              </div>
               
               <div class="col-md-2">
                   <div class="col-md-4">
                    <div class="input-container">
                        <input type="text" id="txtTipoCliente" class="form-control input-small" runat="server" 
                            onpaste="return false" readonly="readonly" maxlength="2" />
                       
                    </div>
                       </div>

                   <div class="col-md-3">
                     <div class="input-container">
                         <input type="text" id="txtTamaño" class="form-control input-tiny" runat="server" 
                             onpaste="return false" readonly="readonly" maxlength="1" />
                     </div>
                        </div>
                   <div class="col-md-5">

                        <input type="text" id="txtTerritorio" class="form-control" runat="server" onpaste="return false"
                            readonly="readonly" />
   
                    </div>


                </div>


              
              <div class="col-md-1">
                  <input type="text" id="txtVentas" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtUtilidadPrima" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtPorcUtilidadPrima" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtUafirmes" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtUafirAnual" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtPorcUafirCte" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
              <div class="col-md-1">
                  <input type="text" id="txtUtilRemanente" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                      readonly="readonly" />
              </div>
          </div>
      </div>
  </div>

        <!-- Botones de Acción -->
        <div class="row mb-3">
            <div class="col-md-6 text-right">
            </div>
            <div class="col-md-6 text-left">
                <div class="row" id="rowTotalesVenta">
                    <div class="col-md-7">
                        <div id="totalesVenta" class="info-header" style="margin-bottom: 5px;">
                            <span class="info-label">Total Monto Venta :    </span>
                            <span id="lblTotalVenta" class="currency total-venta-destacado"></span>
                            <span class="info-label" style="margin-left: 20px;"> </span>
                            <span id="lblPorcentajeVenta" class="percentage porcentaje-destacado"></span>
                        </div>
                    </div>
                     <div class="col-md-5 text-right botones-acciones">
                            <asp:Button ID="btnAutorizarTodo" runat="server" CssClass="btn btn-success" Text="Autorizar" OnClientClick="onAutorizarClick(); return false;"  />
                            <asp:Button ID="btnRechazarTodo" runat="server" CssClass="btn btn-primary" Text="Rechazar" OnClientClick="onRechazarClick(); return false;" />
                            <asp:Button ID="btnSalir" runat="server" CssClass="btn btn-default" Text="  Salir" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Grid de Productos -->
        <div class="row">
            <div class="col-md-12">
                <table id="tblProductos" class="table table-striped table-bordered" style="width:100%">
                </table>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
<div id="dvModalCancelarSolicitud" class="modal fade bg-dark" role="dialog" aria-labelledby="myModalcancela">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header"  style="padding: 10px;">

                <input type="hidden" id="HF_IdAutSolicitudcan" name="HF_IdAutSolicitudcan" />

                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H12">Rechazar Solicitud de Precio
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <label for="lblMotivo">
                            Motivo
                        </label>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                           <select id="cmbAutorizacion" class="form-control">
                                <option value="1">Vinculate a la matriz de CN</option>
                                <option value="2">Utilidad Baja para la categoría del cliente</option>
                                <option value="3">Amplie la justificación de la solicitud</option>
                                <option value="5">Vinculate a Convenio de Precios Esp.</option>
                                <option value="4">Otro</option>
                            </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label for="lblMotivoRechazo">
                            Motivo de Rechazo
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <input type="text" id="txtMotivoRechazo" name="txtMotivoRechazo" class="form-control" placeholder="Motivo de Rechazo" />
                    </div>
                </div>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" onclick="$('#dvModalCancelarSolicitud').modal('hide');">
                    Cerrar</button>

                <button  type="button" class="btn btn-primary"
                    id="btnModCancelarSol" onclick="cancelarsolicitud(jQuery)">
                    Confirmar
                </button>
                 <button  type="button" class="btn btn-primary"
                     id="btnModCancelarTodo" onclick="cancelarTodo(jQuery)">
                     Rechazar Todo
                 </button>


            </div>
        </div>
    </div>
</div>


  
    
   <script type="text/javascript">

       

       // Configurar Alertify
       alertify.set({
           labels: {
               ok: "Aceptar",
               cancel: "Cancelar"
           },
           delay: 5000,
           buttonReverse: false,
           buttonFocus: "ok"
       });

       // Función auxiliar para obtener parámetros de la URL
       function getParameterByName(name) {
           var url = window.location.href;
           name = name.replace(/[\[\]]/g, '\\$&');
           var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
               results = regex.exec(url);
           if (!results) return null;
           if (!results[2]) return '';
           return decodeURIComponent(results[2].replace(/\+/g, ' '));
       }
       function autorizarsolicitud($) {
           var idSolicitud = $('#HF_IdAutSolicitudaut').val();
           var idAutorizacionDet = $('#HF_Id_AutorizacionPrecioDet').val();
           var justificacion = $('#txtJustificacion').val();
           var id_cte = $('#CPH_txtCliente').val();
           var precioVta = $('#tblProductos').DataTable()
               .row(function (idx, data, node) {
                   return data.Id_AutorizacionPrecioDet == idAutorizacionDet;
               })
               .data().Precio_Vta;
           var fechaVigencia = $('#HF_FechaVigencia').val();

           if (!justificacion) {
               alertify.error('Debe ingresar una justificación');
               return;
           }
           console.log(fechaVigencia);
           // Formatear fecha para envío
           var fechaFormateada;
           try {
               if (fechaVigencia && fechaVigencia.indexOf('/Date(') === 0) {
                   // Si la fecha viene en formato /Date(timestamp)/
                   var timestamp = parseInt(fechaVigencia.replace(/\/Date\((-?\d+)\)\//, '$1'));
                   var fecha = new Date(timestamp);
                   fechaFormateada = fecha.toLocaleDateString('es-ES', {
                       year: 'numeric',
                       month: '2-digit',
                       day: '2-digit'
                   });
               } else {
                   // Si la fecha viene en otro formato
                   var fecha = new Date(fechaVigencia);
                   if (isNaN(fecha.getTime())) {
                       // Si la fecha no es válida, usar la fecha actual
                       fecha = new Date();
                   }
                   fechaFormateada = fecha.toLocaleDateString('es-ES', {
                       year: 'numeric',
                       month: '2-digit',
                       day: '2-digit'
                   });
               }
           } catch (e) {
               console.error('Error al formatear fecha:', e);
               // Si hay error, usar la fecha actual
               fechaFormateada = new Date().toLocaleDateString('es-ES', {
                   year: 'numeric',
                   month: '2-digit',
                   day: '2-digit'
               });
           }

           console.log(fechaFormateada);

           $.ajax({
               type: "POST",
               url: "CapAlertaAutorizacionGPMADet.aspx/AceptarProducto",
               data: JSON.stringify({
                   id: parseInt(idSolicitud),
                   id_cte: parseInt(id_cte),
                   id_autorizacion_det: parseInt(idAutorizacionDet),
                   precio_vta: parseFloat(precioVta),
                   fecha_vigencia: fechaFormateada,
                   justificacion: justificacion
               }),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   if (response.d && response.d.success) {
                       alertify.success('Producto autorizado correctamente');
                       $('#dvModalAutorizarSolicitud').modal('hide');
                       $('#tblProductos').DataTable().ajax.reload();
                   } else {
                       alertify.error(response.d.message || 'Error al autorizar el producto');
                   }
               },
               error: function (error) {
                   alertify.error('Error al autorizar el producto');
               }
           });
       }

       

       function initializeDataTable() {
           $('#spinner').show(); 
           // Configuración de idioma español
           var languageConfig = {
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
           };

          

           
            
           // Configuración de DataTable
           var table = $('#tblProductos').DataTable({
               "processing": true,
               "serverSide": false,
               "ajax": {
                   "url": "CapAlertaAutorizacionGPMaDet.aspx/GetProductos",
                   "type": "POST",
                   "contentType": "application/json",
                   "data": function () {
                       var id_cte = getParameterByName('cte') || $('#CPH_txtCliente').val() || 0;
                       var idAutorizacionPrecio = "<%= Request.QueryString["Orden"] ?? "0" %>";
                       return JSON.stringify({
                           id_cte: parseInt(id_cte)   ,
                           idAutorizacionPrecio: parseInt(idAutorizacionPrecio)   
                       });
                   },
                   "dataSrc": function (response) {
                       $('#spinner').hide();
                       if (response.d && response.d.success) {
                           if (response.d.data && response.d.data.length > 0) {
                               alertify.success("Productos cargados correctamente");
                               return response.d.data;
                           } else {
                               alertify.log(response.d.message || "No se encontraron productos");
                               $('.modal').modal('hide');
                               window.parent.closeModalDetalle();
                               return [];
                           }
                       } else {
                           alertify.error(response.d.message || "Error al cargar productos");
                           return [];
                       }
                   }
               },
               "columns": [
                   {
                       "data": "Id_Prd",
                       "title": "Código",
                       "width": "5%"
                   },
                   {
                       "data": "Prd_Descripcion",
                       "title": "Descripción",
                       "width": "15%"
                   },
                   {
                       "data": "UnidadesProyectadas",
                       "title": "Unidades",
                       "width": "5%"
                   },
                   {
                       "data": "PrecioLista",
                       "title": "Precio de Lista",
                       "width": "8%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "Precio_MinimoRik",
                       "title": "Precio Mínimo Rik",
                       "width": "8%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "Precio_MinimoGte",
                       "title": "Precio Mínimo Gerente",
                       "width": "8%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "Precio_AAA",
                       "title": "Precio AAA",
                       "width": "8%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "PrecioObjetivo",
                       "title": "Precio Objetivo Proyectado",
                       "width": "8%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "Precio_Vta",
                       "title": "Precio Venta Negociado",
                       "width": "8%",
                       "render": function (data, type, row) {
                           if (type === 'display') {
                               // Crear un input editable con el valor formateado
                               return '<input type="text" class="form-control precio-venta" ' +
                                   'value="' + new Intl.NumberFormat('es-MX', {
                                       minimumFractionDigits: 2,
                                       maximumFractionDigits: 2
                                   }).format(data || 0) + '" ' +
                                   'data-original-value="' + (data || 0) + '" ' +
                                   'style="width: 100%; text-align: right;">';
                           }
                           return data;
                       }
                   },
                   {
                       "data": "VentaProy",
                       "name": "VentaProy",
                       "title": "Monto de Venta",
                       "width": "7%",
                       "render": function (data) {
                           return new Intl.NumberFormat('es-MX', {
                               style: 'currency',
                               currency: 'MXN'
                           }).format(data || 0);
                       }
                   },
                   {
                       "data": "JustificacionMemo",
                       "title": "Comentarios",
                       "width": "10%"
                   },
                   {
                       "data": "Justificacion",
                       "title": "Justificación",
                       "width": "10%"
                   },
                   {
                       "data": "Nom_Motivo",
                       "title": "Motivo",
                       "width": "8%"
                   },
                   {
                       "data": "FechaVigencia",
                       "title": "Fecha de Vigencia",
                       "width": "8%",
                       "render": function (data) {
                           if (!data) return '';
                           // Extraer el timestamp de /Date(...)/ y convertir a fecha
                           var timestamp = parseInt(data.replace(/\/Date\((-?\d+)\)\//, '$1'));
                           var fecha = new Date(timestamp);
                           return fecha.toLocaleDateString('es-ES', {
                               day: '2-digit',
                               month: '2-digit',
                               year: 'numeric'
                           });
                       }
                   },
                   {
                       "data": "Req_Aut_Director",
                       "title": "Req. Aut. Dir.",
                       "width": "5%",
                       "render": function (data) {
                           return data ? 'Sí' : 'No';
                       }
                   },
                   {
                       "data": "Id_AutorizacionPrecioDet",
                       "title": "Id_AutorizacionPrecioDet",
                       "visible": false
                   },
                   {
                       "data": null,
                       "title": "Acciones",
                       "width": "8%",
                       "className": "text-center",
                       "orderable": false,
                       "render": function (data, type, row) {
                           return '<div class="btn-group-actions">' +
                               '<button type="button" class="btn btn-circle btn-primary btnAceptar" ' +
                               'data-id="' + row.Id_Prd + '" ' +
                               'data-autorizacion-det="' + row.Id_AutorizacionPrecioDet + '" ' +
                               'data-precio-vta="' + row.Precio_Vta + '" ' +
                               'data-fecha-vigencia="' + row.FechaVigencia + '" ' +
                               'title="Aceptar">' +
                               '<i class="fa fa-check"></i>' +
                               '</button>' +
                               '<button type="button" class="btn btn-circle btn-primary btnRechazar" ' +
                               'data-id="' + row.Id_Prd + '" ' +
                               'data-autorizacion-det="' + row.Id_AutorizacionPrecioDet + '" ' +
                               'data-precio-vta="' + row.Precio_Vta + '" ' +
                               'data-fecha-vigencia="' + row.FechaVigencia + '" ' +
                               'title="Rechazar">' +
                               '<i class="fa fa-times"></i>' +
                               '</button>' +
                               '</div>';
                       }
                   }
               ],
               "language": languageConfig,
               "dom": '<"top"lf>rt<"bottom"ip><"clear">',
               "pageLength": 10,
               "ordering": true,
               "searching": true,
               "responsive": true,
               "drawCallback": function (settings) {
                   actualizarTotalesVenta();
               },
               "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Todos"]]
           });

       

           // Eventos para los botones
           $('#tblProductos').on('click', '.btnAceptar', function () {
               var id = $(this).data('id');
               var autorizacionDet = $(this).data('autorizacion-det');
               inicializarModalAutorizarSolicitud(id, autorizacionDet);
           });
 

          
           return table; 
       }

       // Asegurarnos de que jQuery y DataTables estén cargados
       function checkDependencies() {
           if (typeof jQuery === 'undefined') {
               console.error('jQuery no está cargado');
               return false;
           }
           if (typeof $.fn.DataTable === 'undefined') {
               console.error('DataTables no está cargado');
               return false;
           }
           // Verificar que alertify esté disponible
           if (typeof alertify === 'undefined') {
               console.error('Alertify no está cargado');
               return false;
           }
           return true;
       }


       // Modificar la inicialización del documento
       $(document).ready(function () {
           $('#spinner').show();
           // Asegurarse de que todas las dependencias estén cargadas
           var checkDependenciesInterval = setInterval(function () {
               if (checkDependencies()) {
                   clearInterval(checkDependenciesInterval);

                   // Inicializar Alertify primero
                   if (typeof alertify !== 'undefined') {
                       alertify.set({
                           labels: {
                               ok: "Aceptar",
                               cancel: "Cancelar"
                           },
                           delay: 5000,
                           buttonReverse: false,
                           buttonFocus: "ok"
                       });
                   }

                   console.log('Iniciando carga de datos...');
                   var id_cte = "<%= Request.QueryString["cte"] ?? "0" %>";
                   var id_reporteGP = "<%= Request.QueryString["Orden"] ?? "0" %>";
                   var idAutorizacionPrecio = "<%= Request.QueryString["Orden"] ?? "0" %>";
                   console.log('Parámetros:', { id_cte, idAutorizacionPrecio });

              // Limpiar window.radWindowUtils si existe
              if (window.radWindowUtils) {
                  delete window.radWindowUtils;
              }

              // Cargar datos y DataTable de manera secuencial
                   cargarDatosEncabezado(id_cte, idAutorizacionPrecio, function () {
                  console.log('Encabezado cargado, inicializando DataTable...');
                  console.log('cargar initializeDataTable');
                  setTimeout(function () {
                      initializeDataTable();
                  }, 100);
                   
              });
              //console.log('cargar initializeDataTable de nuevo');
              //initializeDataTable(); 
          }
      }, 100);

      // Establecer un tiempo máximo de espera
      setTimeout(function () {
          clearInterval(checkDependenciesInterval);
          if (!checkDependencies()) {
              console.error('No se pudieron cargar todas las dependencias después de 5 segundos');
              alertify.error('Error al cargar las dependencias necesarias');
          }
      }, 5000);
  });



       // Función para cargar el encabezado
       function cargarDatosEncabezado(id_cte, idAutorizacionPrecio, callback) {
           console.log('Iniciando carga de datos de encabezado...');
           console.log('id_cte:', id_cte);
           console.log('idAutorizacionPrecio:', idAutorizacionPrecio);
           $('#spinner').show();
           $.ajax({
               type: "POST",
               url: "CapAlertaAutorizacionGPMaDet.aspx/GetDatosEncabezado",
               data: JSON.stringify({
                   id_cte: id_cte,
                   idAutorizacionPrecio: idAutorizacionPrecio
               }),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               async: false,// Esto hace que la llamada sea síncrona
               success: function (response) {
                   $('#spinner').hide();
                  
                   if (response && response.d && response.d.success) {
                       console.log('Datos recibidos:', response.d);

                       if (response.d.success) {
                           var datos = response.d.data;
                           console.log('Datos a procesar:', datos);

                           try {

                               console.log(datos.Cliente);
                                // Sanitizar valores numéricos
                                datos.Ventas = isFinite(datos.Ventas) ? datos.Ventas : 0;
                                datos.UtilidadPrima = isFinite(datos.UtilidadPrima) ? datos.UtilidadPrima : 0;
                                datos.PorcUtilidadPrima = isFinite(datos.PorcUtilidadPrima) ? datos.PorcUtilidadPrima : 0;
                                datos.UafirMes = isFinite(datos.UafirMes) ? datos.UafirMes : 0;
                                datos.UafirAnual = isFinite(datos.UafirAnual) ? datos.UafirAnual : 0;
                                datos.PorcUafir = isFinite(datos.PorcUafir) ? datos.PorcUafir : 0;
                                datos.UtilRemanente = isFinite(datos.UtilRemanente) ? datos.UtilRemanente : 0;


                               // Primero verificar que los elementos existan
                               var elementos = {
                                   txtCliente: $('#CPH_txtCliente'),
                                   txtClienteNombre: $('#CPH_txtClienteNombre'),
                                   txtTerritorio: $('#CPH_txtTerritorio'),
                                   txtIdAutorizacionPrecio: $('#CPH_txtIdAutorizacionPrecio'),
                                 
                                   txtSolicitudAutorizacion: $('#CPH_txtSolicitudAutorizacion'),  // Correcto
                                   txtId_Cd: $('#CPH_txtId_Cd'),
                                   txtFecha: $('#CPH_txtFecha'),
                                   /*txtTipoAutorizacion: $('#CPH_txtTipoAutorizacion'),*/
                                   txtNomTipoSolicitud: $('#CPH_txtNomTipoSolicitud'),
                                   txtNomEstatus: $('#CPH_txtNomEstatus'),
                                   
                               };
                                
                               // Verificar que todos los elementos existan
                               for (var key in elementos) {
                                   if (elementos[key].length === 0) {
                                       console.error(`Elemento ${key} no encontrado en el DOM`);
                                   }
                               }
                               console.log(datos.Cliente);
                                
                               // Asignar valores usando jQuery con validación
                               elementos.txtCliente.val(datos.Cliente?.toString() || '');
                               elementos.txtClienteNombre.val(datos.ClienteNombre || '');
                               elementos.txtTerritorio.val(datos.Territorio?.toString() || '');

                               
                               elementos.txtIdAutorizacionPrecio.val(datos.IdAutorizacionPrecio?.toString() || '');
                               elementos.txtId_Cd.val(datos.Id_Cd?.toString() || '');
                               elementos.txtFecha.val(formatearFecha(datos.FechaSolicitud) || '');
                               
                             /*  elementos.txtTipoAutorizacion.val(datos.TipoAutorizacion?.toString() || '');*/

                                
                               elementos.txtNomTipoSolicitud.val(datos.NomTipoSolicitud || '');
                               elementos.txtNomEstatus.val(datos.NomEstatus || '');
                               elementos.txtSolicitudAutorizacion.val(datos.IdAutorizacionPrecio?.toString() || '');  
                               console.log(formatNumber(datos.Ventas));

 

                               $('#CPH_txtVentas').val(formatNumber(datos.Ventas) || '');
                               $('#CPH_txtUtilidadPrima').val(formatNumber(datos.UtilidadPrima )|| '');
                               $('#CPH_txtPorcUtilidadPrima').val(formatNumber(datos.PorcUtilidadPrima )|| '');
                               $('#CPH_txtUafirmes').val(formatNumber(datos.UafirMes) || '');
                               $('#CPH_txtUafirAnual').val(formatNumber(datos.UafirAnual) || '');

                               $('#CPH_txtPorcUafirCte').val(formatNumber(datos.PorcUafir)  || '');
                               $('#CPH_txtUtilRemanente').val(formatNumber(datos.UtilRemanente) || '');
 
                               // Actualizar campos de texto
                               $('#CPH_txtCliente').val(datos.Cliente || '');

                               $('#CPH_txtTipoCliente').val(datos.Tipo_Cliente || '');
                               $('#CPH_txtTamaño').val(datos.Id_Tamaño || '');
             
                               // Actualizar campos numéricos adicionales
     
                               alertify.success('Datos del encabezado cargados correctamente');
                               console.log('aquiiba lo de valores');
                               // Llamar al callback después de que todo se haya cargado correctamente
                               //if (typeof callback === 'function') {
                               //    callback();
                               //}

                           } catch (e) {
                               $('#spinner').hide();
                               console.error('Error al procesar datos:', e);
                               alertify.error('Error al procesar los datos: ' + e.message);
                           }
                       } else {
                           $('#spinner').hide();
                           console.warn('La respuesta indica error:', response.d.message);
                           alertify.error(response.d.message || "Error al cargar los datos del encabezado");
                       }
                       if (typeof callback === 'function') {
                           callback();
                       }

                   } else {
                       $('#spinner').hide();
                       console.error('Respuesta inválida del servidor');
                       alertify.error("Respuesta inválida del servidor");
                   }
               },
               error: function (xhr, status, error) {
                   $('#spinner').hide();
                   console.error('Error en la llamada AJAX:', {
                       status: status,
                       error: error,
                       xhr: xhr
                   });
                   alertify.error("Error al cargar datos del encabezado: " + error);

                   // Mostrar más detalles del error si están disponibles
                   if (xhr.responseText) {
                       try {
                           var resp = JSON.parse(xhr.responseText);
                           console.error('Detalles del error:', resp);
                       } catch (e) {
                           console.error('Error response text:', xhr.responseText);
                       }
                   }
                   if (typeof callback === 'function') {
                       callback(); // Opcional: solo si quieres inicializar la tabla aunque falle la carga
                   }

               }
           });
       }
       // Función auxiliar para formatear números
       function formatNumber(value) {
           if (!value || !isFinite(value)) return '0.00';
           return new Intl.NumberFormat('es-MX', {
               minimumFractionDigits: 2,
               maximumFractionDigits: 2
           }).format(value);
       }

       // Funciones auxiliares para formateo
       function formatCurrency(value) {
           if (!isFinite(value) || value == null) return '$0.00';
           return new Intl.NumberFormat('es-MX', {
               style: 'currency',
               currency: 'MXN'
           }).format(value);
       }

       function formatPercentage(value) {
           if (!isFinite(value) || value == null) return '0.00%';
           return new Intl.NumberFormat('es-MX', {
               style: 'percent',
               minimumFractionDigits: 2,
               maximumFractionDigits: 2
           }).format(value / 100);
       }
       // Función auxiliar para formatear fechas
       function formatearFecha(fechaStr) {
           if (!fechaStr) return '';
           // Si la fecha viene en formato /Date(...)/ usar este código:
           if (typeof fechaStr === 'string' && fechaStr.indexOf('/Date(') === 0) {
               var timestamp = parseInt(fechaStr.replace(/\/Date\((-?\d+)\)\//, '$1'));
               var fecha = new Date(timestamp);
               return fecha.toLocaleDateString('es-ES', {
                   day: '2-digit',
                   month: '2-digit',
                   year: 'numeric'
               });
           }
           // Si la fecha viene como string ISO usar este código:
           var fecha = new Date(fechaStr);
           return fecha.toLocaleDateString('es-ES', {
               day: '2-digit',
               month: '2-digit',
               year: 'numeric'
           });
       }

 
       function inicializarModalAutorizarSolicitud(idSolicitud, idAutorizacionDet) {
           var button = $(this); // Botón que disparó el evento
           var precioVta = button.data('precio-vta');
           var fechaVigencia = button.data('fecha-vigencia');

           $('#HF_IdAutSolicitudaut').val(idSolicitud);
           $('#HF_Id_AutorizacionPrecioDet').val(idAutorizacionDet);
           $('#HF_Precio_Vta').val(precioVta);
           $('#HF_FechaVigencia').val(fechaVigencia);
           $('#txtJustificacion').val('');
           $("#dvModalAutorizarSolicitud").appendTo("body");
           $("#dvModalAutorizarSolicitud").modal({ "backdrop": "static" });
           $('#dvModalAutorizarSolicitud').modal('show');
       }

      

       $(document).on('change', '.precio-venta', function () {
           var $input = $(this);
           var table = $('#tblProductos').DataTable();
           var $row = $input.closest('tr');
           var rowIdx = table.row($row).index();

           var precioVta = parseFloat($input.val().replace(/[^0-9.,]/g, '').replace(',', '.')) || 0;
           var rowData = table.row($row).data();
           var unidades = parseFloat(rowData.UnidadesProyectadas) || 0;
           var ventaProy = precioVta * unidades;

           rowData.Precio_Vta = precioVta;
           rowData.VentaProy = ventaProy;

           table.row($row).data(rowData).invalidate();

           var colIdx = table.column('VentaProy:name').index();
           if (colIdx === undefined || colIdx < 0) {
               alertify.error('No se encontró la columna VentaProy');
               return;
           }
           var ventaProyCell = table.cell($row, colIdx);
           ventaProyCell.data(ventaProy).draw(false);
           actualizarTotalesVenta();
          
       });


       //JFCV Autorizar una solicitud masiva Inicio
       // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

       function onAutorizarClick() {

           inicializarModalAutorizarSolicitudTodas(1)
       }

       //JFCV Autorizar una solicitud 
       //Muestro la pantalla de autorizar donde solicito la justificación
       //y al aceptar realiza la autorización MASIVA
       // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
       function inicializarModalAutorizarSolicitudTodas(idSolicitud) {

           idSolicitud = $('#CPH_txtSolicitudAutorizacion').val();
        

         <%-- document.getElementById('<%=txtCliente.ClientID%>').innerHTML = '';--%>
           $("#dvModalAutorizarSolicitudTodas").appendTo("body")
           $("#dvModalAutorizarSolicitudTodas").modal({ "backdrop": "static" });
          
           $('#dvModalAutorizarSolicitudTodas #HF_IdAutSolicitudauttodas').val(idSolicitud);
           $('#HF_IdAutSolicitudauttodas').val(idSolicitud);
          
           $('#txtJustificaciontodas').val('');
           $('#dvModalAutorizarSolicitudTodas').modal('show');
       }

       function autorizarsolicitudtodas($) {
           let table = $('#tblProductos').DataTable();
           let justificacion = $('#txtJustificaciontodas').val(); // Justificación general
           let idSolicitud = $('#HF_IdAutSolicitudauttodas').val();
           let id_cte = $('#CPH_txtCliente').val();

           if (!justificacion) {
               alertify.error('Debe ingresar una justificación');
               return;
           }

           let rows = table.rows({ page: 'current' }).data();
           if (rows.length === 0) {
               alertify.error('No hay productos para autorizar.');
               return;
           }

           let ajaxCalls = [];

           for (let i = 0; i < rows.length; i++) {
               let row = rows[i];
               let idAutorizacionDet = row.Id_AutorizacionPrecioDet;
               let precioVta = row.Precio_Vta;
               let fechaVigencia = row.FechaVigencia;

               let fechaFormateada;
               try {
                   if (fechaVigencia && fechaVigencia.indexOf('/Date(') === 0) {
                       let timestamp = parseInt(fechaVigencia.replace(/\/Date\((-?\d+)\)\//, '$1'));
                       let fecha = new Date(timestamp);
                       fechaFormateada = fecha.toLocaleDateString('es-ES', {
                           year: 'numeric',
                           month: '2-digit',
                           day: '2-digit'
                       });
                   } else {
                       let fecha = new Date(fechaVigencia);
                       if (isNaN(fecha.getTime())) {
                           fecha = new Date();
                       }
                       fechaFormateada = fecha.toLocaleDateString('es-ES', {
                           year: 'numeric',
                           month: '2-digit',
                           day: '2-digit'
                       });
                   }
               } catch (e) {
                   fechaFormateada = new Date().toLocaleDateString('es-ES', {
                       year: 'numeric',
                       month: '2-digit',
                       day: '2-digit'
                   });
               }

               // Guarda la promesa de cada llamada AJAX
               ajaxCalls.push(
                   $.ajax({
                       type: "POST",
                       url: "CapAlertaAutorizacionGPMADet.aspx/AceptarProducto",
                       data: JSON.stringify({
                           id: parseInt(idSolicitud),
                           id_cte: parseInt(id_cte),
                           id_autorizacion_det: parseInt(idAutorizacionDet),
                           precio_vta: parseFloat(precioVta),
                           fecha_vigencia: fechaFormateada,
                           justificacion: justificacion
                       }),
                       contentType: "application/json; charset=utf-8",
                       dataType: "json"
                   })
               );
           }

           // Espera a que todas las peticiones terminen
           Promise.all(ajaxCalls)
               .then(function (results) {
                   let errores = 0;
                   results.forEach(function (response, idx) {
                       if (response.d && response.d.success) {
                           // Puedes mostrar un mensaje por cada producto si lo deseas
                            alertify.success('Producto autorizado: ' + rows[idx].Id_Prd);
                       } else {
                           errores++;
                           alertify.error('Error al autorizar el producto: ' + rows[idx].Id_Prd);
                       }
                   });
                   if (errores === 0) {
                       alertify.success("Se han enviado las autorizaciones.");
                   }
               })
               .catch(function (error) {
                   alertify.error("Ocurrió un error en la autorización masiva.");
               })
               .finally(function () {
                   $('#dvModalAutorizarSolicitudTodas').modal('hide');
                   windowsClose();
                   /*table.ajax.reload();*/
               });
       }

       //cancelar todas las solicitudes del folio 
       function onRechazarClick()
       {
           let idSolicitud = $('#CPH_txtSolicitudAutorizacion').val();
            
           $('#HF_IdAutSolicitudcan').val(idSolicitud);
           $('#txtMotivoRechazo').val('');
           $('#dvModalCancelarSolicitud #btnModCancelarSol').hide();
           $('#dvModalCancelarSolicitud #btnModCancelarTodo').show();
           
           $("#dvModalCancelarSolicitud").appendTo("body");
           $("#dvModalCancelarSolicitud").modal({ "backdrop": "static" });
           $('#dvModalCancelarSolicitud').modal('show');

       }

       $('#tblProductos').on('click', '.btnRechazar', function () {
            var id = $(this).data('id');
            var button = $(this); // Botón que disparó el evento
            var autorizacionDet = $(this).data('autorizacion-det');
            var precioVta = button.data('precio-vta');
           var fechaVigencia = button.data('fecha-vigencia');

           
           $('#dvModalCancelarSolicitud #btnModCancelarSol').show();
           $('#dvModalCancelarSolicitud #btnModCancelarTodo').hide();

            $('#HF_IdAutSolicitudcan').val(autorizacionDet);
            $('#txtMotivoRechazo').val('');
            $("#dvModalCancelarSolicitud").appendTo("body");
            $("#dvModalCancelarSolicitud").modal({ "backdrop": "static" });
            $('#dvModalCancelarSolicitud').modal('show');
       });

       //$('#btnModCancelarSol').click(function () {
       //    var id = $('#modalRechazo').data('id');
       //    var motivo = $('#txtMotivoRechazo').val();
       //    if (!motivo) {
       //        alertify.error('Debe ingresar un motivo de rechazo');
       //        return;
       //    }
       //    rechazarProducto(id, motivo);
       //    $('#modalRechazo').modal('hide');
       //});

       // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
       function cancelarsolicitud($) {

            var actualCancelar = $('#dvModalCancelarSolicitud #HF_IdAutSolicitudcan').val();
            var motivorechazo = $('#dvModalCancelarSolicitud #txtMotivoRechazo').val();
            var usuariocan = $("#" + '<%=txtUsuario.ClientID%>').val();
            var id_cd = $("#" + '<%=txtId_Cd.ClientID%>').val();
 
           var justificarechazo = motivorechazo;
           var idmotivorechazo = $('#cmbAutorizacion').val(); // El value seleccionado
           var motivorechazo = $('#cmbAutorizacion option:selected').text(); // El texto seleccionado


             CancelarSolicitud(actualCancelar, motivorechazo, idmotivorechazo, justificarechazo, usuariocan, id_cd);

             //CancelarSolicitud(actualCancelar, motivorechazo, usuariocan, id_cd);

             $('#dvModalCancelarSolicitud').modal('hide');

       }


       function cancelarTodo($) {
           var actualCancelar = $('#dvModalCancelarSolicitud #HF_IdAutSolicitudcan').val();
           /* var motivorechazo = $('#dvModalCancelarSolicitud #txtMotivoRechazo').val();*/
           var usuariocan = $("#" + '<%=txtUsuario.ClientID%>').val();
           var id_cd = $("#" + '<%=txtId_Cd.ClientID%>').val();

           var idmotivorechazo = $('#cmbAutorizacion').val(); // El value seleccionado
           var motivorechazo = $('#cmbAutorizacion option:selected').text(); // El texto seleccionado
           var justificarechazo = motivorechazo;

           if (!motivorechazo) {
               alertify.error('Debe ingresar una justificación');
               return;
           }

           if (!justificarechazo || justificarechazo.trim() === "") {
               alertify.error('Debe ingresar una justificación');
               return;
           }
           let table = $('#tblProductos').DataTable();
           //solo de los que esta desplegando??? ToDo
           let rows = table.rows({ page: 'current' }).data();


           let fechaFormateada;
           for (var i = 0; i < rows.length; i++) {
               let row = rows[i];

               let idAutorizacionDet = row.Id_AutorizacionPrecioDet;
               let idPrd = row.Id_Prd;
               let precioVta = row.Precio_Vta;
               let fechaVigencia = row.FechaVigencia;

               try {
                   CancelarSolicitud(
                       idAutorizacionDet,
                       motivorechazo,
                       idmotivorechazo,
                       justificarechazo,
                       usuariocan,
                       id_cd
                   );

               } catch (e) {
                   console.error('Error al rechazar : ' + idAutorizacionDet, e);
               }
      
           }
           alertify.success("Rechazo masivo en proceso");
           $('#dvModalCancelarSolicitud').modal('hide');
           windowsClose();
       }

       function CancelarSolicitud(folio, motivorechazo, idmotivorechazo, justificarechazo, usuario, id_cd) {

           var dataValue = "{parametro: '" + folio + "', motivorechazo: '" + motivorechazo + "', idmotivorechazo: '" + idmotivorechazo + "', justificarechazo: '" + justificarechazo + "', usuario: '" + usuario + "', idcd: '" + id_cd + "'}";

           $.ajax({
               type: "POST",
               url: "CapAlertaAutorizacionGPMaDet.aspx/RechazarFolioGPMA",
               data: dataValue,
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               error: function (XMLHttpRequest, textStatus, errorThrown) {
                   alert.error("Error " + folio);
               },
               success: function (response) {
                   alertify.success("Solicitud de precio rechazada " + folio);
               },
               complete: function (response) {

                   /*$('#spinner').css("display", "none");*/
                 }
             });

            location.reload();

       }

       function actualizarTotalesVenta() {
           var table = $('#tblProductos').DataTable();
           var totalVenta = 0;

           // Suma el monto de venta de todos los productos visibles
           table.rows({ filter: 'applied' }).data().each(function (row) {
               totalVenta += parseFloat(row.VentaProy) || 0;
           });

           // Obtén el valor de ventas futuro
           var ventasFuturo = parseFloat($('#CPH_txtVentas').val().replace(/[^\d.-]/g, '')) || 0;

           // Calcula el porcentaje
           var porcentaje = ventasFuturo > 0 ? (totalVenta / ventasFuturo) * 100 : 0;

           // Actualiza los labels
           $('#lblTotalVenta').text(
               new Intl.NumberFormat('es-MX', { style: 'currency', currency: 'MXN' }).format(totalVenta)
           );
           $('#lblPorcentajeVenta').text(
               new Intl.NumberFormat('es-MX', { style: 'percent', minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(porcentaje / 100)
           );
       }

       $('#<%= btnSalir.ClientID %>').on('click', function (e) {
           e.preventDefault();
           console.log('saliendo ');
           // Cierra todos los modales abiertos
           $('.modal').modal('hide');
           // Redirige a la pantalla principal
           window.parent.closeModalDetalle();
       });

       function windowsClose() {
           $('.modal').modal('hide');
           window.parent.closeModalDetalle();
       }

   </script>

    <div id="dvModalAutorizarSolicitud" class="modal fade bg-dark" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header" style="padding: 10px;">
                 <input type="hidden" id="HF_IdAutSolicitudaut" name="HF_IdAutSolicitudaut" />
                 <input type="hidden" id="HF_Id_AutorizacionPrecioDet" name="HF_Id_AutorizacionPrecioDet" />
                 <input type="hidden" id="HF_Precio_Vta" name="HF_Precio_Vta" />
                 <input type="hidden" id="HF_FechaVigencia" name="HF_FechaVigencia" />
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title">Autorizar Solicitud de Precio</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <label for="lblJustificacion">Justificación</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <input type="text" id="txtJustificacion" name="txtMotivo" class="form-control" placeholder="Justificación" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" onclick="$('#dvModalAutorizarSolicitud').modal('hide');">
                    Cerrar
                </button>
                <button type="button" class="btn btn-primary" onclick="autorizarsolicitud(jQuery)">
                    Confirmar
                </button>
            </div>
        </div>
    </div>
</div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ 
        Solicita la justicicación cuando se desea autorizar todas las solicitudes de una sola vez 
        Se teclea una justificacion para todas
        -->
    <div id="dvModalAutorizarSolicitudTodas" class="modal fade bg-dark" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">

                    <input type="hidden" id="HF_IdAutSolicitudauttodas" name="HF_IdAutSolicitudauttodas" />

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H12">Autorizar Todas las Solicitudes  de Precios 
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <label for="lblJustificaciontodas">
                                Justificación para todas las solicitudes
                            </label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="text" id="txtJustificaciontodas" name="txtMotivotodas" class="form-control" placeholder="Justificación" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="$('#dvModalAutorizarSolicitudTodas').modal('hide');">
                        Cerrar</button>

                    <button type="button" class="btn btn-primary"
                        id="btnAutorizartodas" onclick="autorizarsolicitudtodas(jQuery)">
                        Confirmar
                    </button>

                </div>
            </div>
        </div>
    </div>

      

         
</asp:Content>