<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CapOrdenServicio_Admin.aspx.cs" Inherits="SIANWEB.CapOrdenServicio_Admin" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Administración Orden de Servicio</title>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("CapOrdenServicio.css?ver=14")%>">

    <style>
       </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript"> var ApplicationUrl = "<%=ApplicationUrl%>";</script>
    <div class="container-fluid" style="padding: 10px;">

        <div class="divContenedorFiltros">
            <div class="row">
                <div class="col-sm-2 lblFiltro">
                    <label for="txtNombreCliente">Nombre cliente</label>
                </div>
                <div class="col-sm-4">
                    <input type="text" id="txtNombreCliente" class="form-control">
                </div>
                <div class="col-sm-2 lblFiltro">
                    <label for="ddlEstatus">Estatus</label>
                </div>
                <div class="col-sm-2">
                    <select id="ddlEstatus" class="form-control">
                        <option value="">-- Seleccione --</option>
                    </select>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-sm-2 lblFiltro">
                    <label for="txtClienteInicial">Cliente inicial</label>
                </div>
                <div class="col-sm-2">
                    <input type="number" id="txtClienteInicial" class="form-control">
                </div>
                <div class="col-sm-2 lblFiltro">
                    <label for="txtClienteFinal">Cliente final</label>
                </div>
                <div class="col-sm-2">
                    <input type="number" id="txtClienteFinal" class="form-control">
                </div>
                <div class="col-sm-2 "></div>
                <div class="col-sm-2 ">
                    <button type="button" id="btnNuevo" class="btn btn-success btn-accion"><i class="fa fa-plus"></i>Nuevo</button>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-sm-2 lblFiltro">
                    <label for="dtFechaInicial">Fecha inicial</label>
                </div>
                <div class="col-sm-2">
                    <input type="date" id="dtFechaInicial" class="form-control">
                </div>
                <div class="col-sm-2 lblFiltro">
                    <label for="dtFechaFinal">Fecha final</label>
                </div>
                <div class="col-sm-2">
                    <input type="date" id="dtFechaFinal" class="form-control">
                </div>
                <div class="col-sm-2 "></div>
                <div class="col-sm-2 ">
                    <button type="button" id="btnExportar" class="btn btn-secondary btn-accion"><i class="fa fa-file-excel-o"></i>Exportar</button>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-sm-2 lblFiltro">
                    <label for="txtOrdenServicioInicial">Orden servicio inicial</label>
                </div>
                <div class="col-sm-2">
                    <input type="number" id="txtOrdenServicioInicial" class="form-control">
                </div>
                <div class="col-sm-2 lblFiltro">
                    <label for="txtOrdenServicioFinal">Orden servicio final</label>
                </div>
                <div class="col-sm-2">
                    <input type="number" id="txtOrdenServicioFinal" class="form-control">
                </div>
                <div class="col-sm-2 "></div>
                <div class="col-sm-2">
                    <button type="button" id="btnBuscar" class="btn btn-primary btn-accion"><i class="fa fa-search"></i>Buscar</button>
                </div>
            </div>
        </div>
        <div class="table-responsive">
            <table id="tblOrdenesServicio" class="table table-striped table-bordered tblResultados">
                <thead>
                    <tr>
                        <th>Cve Servicio</th>
                        <th>Número</th>
                        <th>Estatus</th>
                        <th>Fecha</th>
                        <th>Núm. cte.</th>
                        <th>Cliente</th>
                        <th>Costo Total</th>
                        <th style="width: 80px;">Confirmación</th>
                        <th style="width: 80px;">Editar</th>
                        <th style="width: 80px;">Eliminar</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
    <!-- Modal Orden de Servicio -->
    <div id="modalOrdenServicio" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="z-index: 1010!important;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span> </button>
                    <h4 class="modal-title" id="lblTituloModal">Alta Orden de Servicio</h4>
                </div>
                <div class="modal-body">
                    <!-- Fila Encabezado -->
                    <div id="dvInformativo" style="display: none;">
                        <div id="dvFechaEstimada" style="display: none;">
                             <span id="lblFechaEstimada" ></span>
                        </div>
                        <div id="dvMatriz"  style="display: none;">
                            <span id="lblMatriz" ></span>
                        </div>
                         <div id="dvIndicaciones"  style="display: none;">                            
                                 <span id="lblIndicaciones" ></span>                           
                         </div>
                   </div>
                        <div class="form-inline" style="margin-bottom: 10px;">
                        <div class="form-group" style="margin-right: 15px;">
                            <label for="txtNumeroFolio" style="margin-right: 5px;" class="w80">Folio:</label>
                            <input type="text" id="txtNumeroFolio" class="form-control" style="width: 120px;" readonly disabled="disabled">
                            <input type="hidden" id="hdnIdOrdenServicio" value="0">
                            <input type="hidden" id="hdnFechaCompromisoOriginal" value="">
                        </div>
                        <div class="form-group" style="margin-right: 15px;">
                            <label for="dtFechaCompromiso" style="margin-right: 5px;">Fecha Compromiso:</label>
                            <input type="date" id="dtFechaCompromiso" class="form-control" style="width: 160px;">
                        </div>

                        <div class="form-group" style="margin-right: 15px;">
                            <div class="checkbox" style="margin-top: 0;">
                                <label>
                                    <input type="checkbox" id="chkExtemporaneo">
                                    Extemporáneo
                                </label>
                            </div>
                        </div>
                        
                        <div id="divEstatus" class="form-group">
                            <label style="margin-right: 5px;">Estatus:</label>
                            <span id="lblEstatus" style="display: inline-block; padding: 4px 10px; background-color: #f5f5f5; border: 1px solid #ddd; border-radius: 3px; font-weight: 600; color: #555; min-width: 120px;"></span>
                        </div>
                    </div>

                    <div id="divCambioCompromiso" class="form-inline" style="margin-bottom: 10px; display: none;">
                        <div class="form-group">
                            <label for="ddlCambioCompromiso" style="margin-right: 5px;" class="w80">Modtivo cambio fecha compromiso:</label>
                            <select id="ddlCambioCompromiso" class="form-control" style="width: 400px;"></select>
                        </div>
                    </div>
                    <!-- Tabs Navigation -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#tabFormulario" aria-controls="tabFormulario" role="tab" data-toggle="tab">
                                <i class="fa fa-file-text-o" style="margin-right:5px;"></i>Datos Generales
                            </a>
                        </li>
                        <li role="presentation">
                            <a href="#tabServicios" aria-controls="tabServicios" role="tab" data-toggle="tab">
                                <i class="fa fa-list" style="margin-right:5px;"></i>Detalle de Servicios
                            </a>
                        </li>
                    </ul>

                    <!-- Tab Content -->
                    <div class="tab-content" >

                        <!-- TAB 1: FORMULARIO -->
                        <div role="tabpanel" class="tab-pane fade in active" id="tabFormulario">

                            <!-- Fila 1: Folio, Fecha Compromiso, Extemporáneo -->
                            <div class="form-inline" style="margin-bottom: 10px;">

                                <div class="form-group" style="margin-right: 10px;">
                                    <label for="txtIdCliente" style="margin-right: 5px;" class="w80">Cliente:</label>
                                    <input type="number" id="txtIdCliente" class="form-control" style="width: 100px;" placeholder="ID">
                                </div>

                                <div class="form-group" style="margin-right: 10px;">
                                    <input type="text" id="txtNombreClienteModal" class="form-control" style="width: 400px;" placeholder="Nombre cliente">
                                </div>

                                <div class="form-group">
                                    <button type="button" id="btnBuscarCliente" class="btn btn-info btn-accion">
                                        <i class="fa fa-search"></i>Buscar
                                    </button>
                                </div>

                            </div>
                            <!-- Fila 5: Territorio -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group" style="margin-right: 15px;">
                                    <label for="txtTerritorio" style="margin-right: 5px;" class="w80">Territorio:</label>
                                    <input type="text" id="txtTerritorio" class="form-control" style="width: 100px;" readonly disabled="disabled">
                                </div>

                                <div class="form-group">
                                    <select id="ddlTerritorio" class="form-control" style="width: 400px;"></select>
                                </div>
                                <div class="form-group">
                                    <input type="hidden" id="hdnIdRepServicio" value="0">
                                    <input type="hidden" id="hdnIdRepServicioTecnico" value="0">
                                    <span id="lblRepresentantes">
                                    </span>
                                </div>
                            </div>

                            <!-- Fila 2: Servicio -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group">
                                    <label for="ddlServicio" style="margin-right: 5px;" class="w80">Servicio:</label>
                                    <select id="ddlServicio" class="form-control" style="width: 400px;"></select>
                                </div>

                            </div>

                            <hr>

                            <!-- Fila 4: Rol y Usuario -->
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="form-inline" style="margin-bottom: 10px;">
                                        <div class="form-group" style="margin-right: 15px;">
                                            <label for="ddlRol" style="margin-right: 5px;" class="w80">Rol:</label>
                                            <select id="ddlRol" class="form-control" style="width: 300px;"></select>
                                        </div>
                                    </div>
                                    <div class="form-inline" style="margin-bottom: 10px;">
                                        <div class="form-group">
                                            <label for="ddlUsuario" style="margin-right: 5px;" class="w80">Usuario:</label>
                                            <select id="ddlUsuario" class="form-control" style="width: 300px;"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <!-- Tabla de Usuarios -->
                                    <div style="margin-bottom: 15px;">
                                       
                                        <div class="table-responsive" style="max-height: 150px; overflow-y: auto;">
                                            <table class="table table-bordered table-condensed table-striped" id="tblUsuario">
                                                <thead>
                                                    <tr style="background-color: #f5f5f5;">
                                                        <th style="width: 80px;">Id Rol</th>
                                                        <th>Rol</th>
                                                        <th style="width: 80px;">Id Rep</th>
                                                        <th>Representante</th>
                                                        <th style="width: 60px; text-align: center;">Quitar</th>
                                                    </tr>
                                                </thead>
                                                <tbody></tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <hr>

                            <!-- Fila 6: Segmento -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group" style="width: 100%;">
                                    <label style="margin-right: 5px; width: 100px;" class="w80">Segmento:</label>
                                    <span id="lblSegmento" class="lblValorDireccion" style="width: calc(100% - 110px); display: inline-block;"></span>
                                </div>
                            </div>

                            <!-- Fila 7: Calle, Número, Colonia -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group" style="margin-right: 10px;">
                                    <label style="margin-right: 5px;">Calle:</label>
                                    <span id="lblCalle" class="lblValorDireccion" style="width: 180px; display: inline-block;"></span>
                                </div>

                                <div class="form-group" style="margin-right: 10px;">
                                    <label style="margin-right: 5px;">Número:</label>
                                    <span id="lblNumero" class="lblValorDireccion" style="width: 80px; display: inline-block;"></span>
                                </div>

                                <div class="form-group">
                                    <label style="margin-right: 5px;">Colonia:</label>
                                    <span id="lblColonia" class="lblValorDireccion" style="width: 180px; display: inline-block;"></span>
                                </div>
                            </div>

                            <!-- Fila 8: Municipio, Estado -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group" style="margin-right: 10px;">
                                    <label style="margin-right: 5px;">Municipio:</label>
                                    <span id="lblMunicipio" class="lblValorDireccion" style="width: 200px; display: inline-block;"></span>
                                </div>

                                <div class="form-group">
                                    <label style="margin-right: 5px;">Estado:</label>
                                    <span id="lblEstado" class="lblValorDireccion" style="width: 200px; display: inline-block;"></span>
                                </div>
                            </div>

                            <!-- Fila 9: RFC, Teléfono -->
                            <div class="form-inline" style="margin-bottom: 10px;">
                                <div class="form-group" style="margin-right: 10px;">
                                    <label style="margin-right: 5px;">RFC:</label>
                                    <span id="lblRfc" class="lblValorDireccion" style="width: 200px; display: inline-block;"></span>
                                </div>

                                <div class="form-group">
                                    <label style="margin-right: 5px;">Teléfono:</label>
                                    <span id="lblTelefono" class="lblValorDireccion" style="width: 200px; display: inline-block;"></span>
                                </div>
                            </div>

                        </div>

                        <!-- TAB 2: DETALLE DE SERVICIOS -->
                        <div role="tabpanel" class="tab-pane fade" id="tabServicios">

                            <div style="margin-bottom: 10px; text-align: right;">
                                <button type="button" id="btnAgregarFilaDet" class="btn btn-info">
                                    <i class="fa fa-plus"></i>Agregar Servicio
                                </button>
                            </div>

                            <div class="table-responsive">
                                <table id="tblDetalleServicios" class="table table-bordered table-hover">
                                    <thead style="background-color: #f5f5f5;">
                                        <tr>
                                            <th style="width: 100px;">Núm</th>
                                            <th style="width: 300px;">Servicio</th>
                                            <th style="width: 100px;">Cantidad</th>
                                            <th style="width: 120px;">Costo</th>
                                            <th style="width: 120px;">Total</th>
                                            <th style="width: 80px; text-align: center;">Quitar</th>
                                            <th style="display:none;"></th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                        </div>

                    </div>
                    <!-- /Tab Content -->

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnGuardarOrdenServicio" class="btn btn-success btn-accion"><i class="fa fa-save"></i>Guardar</button>
                    <button type="button" class="btn btn-default btn-accion" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /Modal -->

    <div id="modalClientes" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="z-index: 1010!important;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span> </button>
                    <h4 class="modal-title">Seleccion de cliente</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                        <table id="tblClientes" class="table table-striped table-bordered tblResultados">
                            <thead>
                                <tr>
                                    <th>Cve Cliente</th>
                                    <th>Nombre comercial</th>
                                    <th>Agregar</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-light btn-accion" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Confirmar -->
    <div id="mdConfirmar" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="z-index: 1015!important;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span> </button>
                    <h4 class="modal-title">Confirmación de Orden de Servicio</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnIdOrdenServicioConfirmar" value="0">
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label>Estado de Confirmación:</label>
                                <div class="radio">
                                    <label>
                                        <input type="radio" name="rdConfirmacion" id="rdCompleto" value="1" checked>
                                        Completo
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" name="rdConfirmacion" id="rdIncompleto" value="0">
                                        Incompleto
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <div class="form-group" id="divMotivoIncompleto" style="display: none;">
                                <label for="ddlMotivoIncompleto">Motivo de Incompleto: <span style="color: red;">*</span></label>
                                <select id="ddlMotivoIncompleto" class="form-control">
                                    <option value="-1">-- Seleccione un motivo --</option>
                                </select>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" id="btnGuardaConfirmacion" class="btn btn-success"><i class="fa fa-save"></i>Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /Modal Confirmar -->

    <!-- Modal Confirmar Eliminación -->
    <div id="mdConfirmarEliminacion" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="z-index: 1015!important;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span> </button>
                    <h4 class="modal-title">Eliminación de Orden de Servicio</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnIdOrdenServicioEliminar" value="0">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="alert alert-warning" role="alert">
                                <i class="fa fa-exclamation-triangle"></i>
                                <strong>Advertencia:</strong> Esta acción marcará la orden de servicio como eliminada. Esta operación no se puede deshacer.
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="ddlMotivoEliminacion">Motivo de Eliminación: <span style="color: red;">*</span></label>
                                <select id="ddlMotivoEliminacion" class="form-control">
                                    <option value="-1">-- Seleccione un motivo --</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarEliminacion" class="btn btn-danger"><i class="fa fa-trash"></i>Confirmar Eliminación</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-times"></i>Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /Modal Confirmar Eliminación -->
    <!-- Loading -->
<div id="loading" class="loading-overlay" >
	<div class="loading-spinner"></div>
</div>

    <!-- Scripts específicos de la página -->
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.js")%>"> </script>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"> </script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select/bootstrap-select.min.js") %>"> </script>
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-select/bootstrap-select.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/OrdenServicio/CapOrdenServicio_ApiHelper.js?ver=250115.11")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/OrdenServicio/CapOrdenServicio_Admin.js?ver=260421.01")%>"> </script>
</asp:Content>
