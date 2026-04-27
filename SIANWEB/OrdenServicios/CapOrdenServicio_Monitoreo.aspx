<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CapOrdenServicio_Monitoreo.aspx.cs" Inherits="SIANWEB.CapOrdenServicio_Monitoreo" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Monitoreo Orden de Servicio</title>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/datatables/datatables.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("CapOrdenServicio.css")%>">
    <style>
        </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript"> var ApplicationUrl = "<%=ApplicationUrl%>"; </script>

    <div class="container-fluid" style="padding:10px;">
        <div class="row">
            <div class="col-md-8">
                <div class="div-filtros">
                    <div class="row">
                        <div class="col-sm-2 lblFiltro"><label for="dtFechaInicial">Fecha inicial</label></div>
                        <div class="col-sm-4"><input type="date" id="dtFechaInicial" class="form-control" /></div>
                        <div class="col-sm-2 lblFiltro"><label for="dtFechaFinal">Fecha final</label></div>
                        <div class="col-sm-4"><input type="date" id="dtFechaFinal" class="form-control" /></div>
                    </div>
                    <div class="row" style="margin-top:8px;">                        
                        <div class="col-sm-2 lblFiltro"><label for="ddlEstatus">Estatus</label></div>
                        <div class="col-sm-4"><select id="ddlEstatus" class="form-control"><option value="">-- Seleccione --</option></select></div>
                        <div class="col-sm-2 lblFiltro"><label for="txtOrdenServicio">Orden de servicio</label></div>
                        <div class="col-sm-4"><input type="number" id="txtOrdenServicio" class="form-control" /></div>
                    </div>
                    <div class="row" style="margin-top:8px;">
                        <div class="col-sm-12 text-right">
                            <button type="button" id="btnBuscar" class="btn btn-primary btn-min-160"><i class="fa fa-search"></i> Buscar</button>
                            <button type="button" id="btnExportar" class="btn btn-success btn-min-160"><i class="fa fa-file-excel-o"></i> Exportar</button>
                        </div>
                    </div>
                </div>
               
            </div>
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading"><strong>Resumen indicadores</strong></div>
                    <div class="panel-body" style="padding:5px;">
                        <table id="tblResumen" class="table table-bordered table-condensed tblResumen">
                            <thead>
                                <tr>
                                    <th>Confirmados</th>
                                    <th>Órdenes Servicio</th>
                                    <th>Montos</th>
                                    <th>%</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-left">Ticket completos</td>
                                    <td id="resCompPedidos">0</td>
                                    <td id="resCompMontos">0.00</td>
                                    <td id="resCompPorc">0%</td>
                                </tr>
                                <tr>
                                    <td class="text-left">Ticket incompletos</td>
                                    <td id="resIncompPedidos">0</td>
                                    <td id="resIncompMontos">0.00</td>
                                    <td id="resIncompPorc">0%</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
             <div class="col-md-12">
                  <div class="panel-col-buttons">
     <div class="btn-group" role="group" aria-label="Agregar Columnas">
         <button type="button" id="btnAddColEstatusTicket" class="btn btn-info btn-col btn-min-160" title="Estatus"><i class="fa fa-plus"></i> Estatus</button>
         <button type="button" id="btnAddColTipoConfirmacion" class="btn btn-info btn-col btn-min-160" title="Tipo Confirmación"><i class="fa fa-plus"></i>Tipo confirmación</button>
         <button type="button" id="btnAddColMatrizCN" class="btn btn-info btn-col btn-min-160" title="Matriz cuenta nacional"><i class="fa fa-plus"></i> Matriz CN</button>
          <button type="button" id="btnAddColUsuarioCreador" class="btn btn-info btn-col btn-min-160" title="Usuario Creador"><i class="fa fa-plus"></i> Creador</button>
         <button type="button" id="btnAddColUsuarioAsignado" class="btn btn-info btn-col btn-min-160" title="Usuario Asignado"><i class="fa fa-plus"></i> Asignado</button>        
         <button type="button" id="btnAddColMotivoIncompleto" class="btn btn-info btn-col btn-min-160" title="Motivo Incompleto"><i class="fa fa-plus"></i> Motivo incompleto</button>
         <button type="button" id="btnAddColMotivoCambioFecha" class="btn btn-info btn-col btn-min-160" title="Motivo Cambio Fecha Compromiso"><i class="fa fa-plus"></i> Motivo cambio fecha</button>
         <button type="button" id="btnAddColCodigoProducto" class="btn btn-info btn-col btn-min-160" title="Código producto"><i class="fa fa-plus"></i> Código producto</button>
         <button type="button" id="btnAddColProducto" class="btn btn-info btn-col btn-min-160" title="Producto"><i class="fa fa-plus"></i> Producto</button>
         </div>
 </div>
 <div class="table-responsive" style="margin-top:10px;">
     <table id="tblResultados" class="table table-striped table-bordered tblResultados" style="width:100%;">
         <thead>
             <tr>
                 <th>Id cliente</th>
                 <th>Cliente</th>
                 <th>Orden de servicio</th>
                 <th>Estatus</th>
                 <th>Tipo confirmación</th>
                 <th>Matriz cuenta nacional</th>                 
                 <th>Usuario creador</th>
                 <th>Usuario asignado</th>
                 <th>Motivo incompleto</th>
                 <th>Motivo cambio fecha compromiso</th>
                 <th>Fecha captura</th>
                 <th>Fecha compromiso</th>
                 <th>Fecha confirmacion</th>
                 <th>Tiempo de respuesta</th>
                 <th>Código producto</th>
                 <th>Producto</th>
                 <th>Unidades </th>
                 <th>Monto</th>
                 
             </tr>
         </thead>
         <tbody></tbody>
     </table>
 </div>
            </div>
        </div>
    </div>

    <!-- Loading overlay (estándar) -->
    <div id="loading" class="loading-overlay">
        <div class="loading-spinner"></div>
    </div>

    <!-- Librerías -->
    <script src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveUrl("~/Librerias/datatables/datatables.min.js")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/OrdenServicio/CapOrdenServicio_ApiHelper.js?ver=251021.01")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/OrdenServicio/CapOrdenServicio_Monitoreo.js?ver=251024.06")%>"></script>
</asp:Content>
