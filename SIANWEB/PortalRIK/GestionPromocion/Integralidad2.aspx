<%@ Page Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master"  AutoEventWireup="true" 
    CodeBehind="Integralidad2.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Integralidad2" %>

<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <!--script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script-->
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/font-awesome.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    
    <style>
        .searchbutton {
            margin-top: -24px;
            margin-left: 160px;
            border:none!important;
            background-color: #ffffff!important;
        }

        .toast-pf-top-right-rel {
          left: 20px;
          position: relative;
          right: 20px;
          top: 12px;
          z-index: 1035;
          /* Medium devices (desktops, 992px and up) */
        }

        .ui-autocomplete-input {
            width:205px;
        }

    </style>

    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
    
    <div id="modalIntegralidadInicial" class="row" style="display: block;">

        <div class="col-sm-9 col-md-12">

          <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">
                    <h3>
                        <strong>Integralidad</strong>                        
                    </h3>
                </div>
            </div>

            <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" id="tabSeguimiento">
                        <li class="active">
                            <a href="#dvIntegralidad" data-toggle="tab">Integralidad</a>
                        </li>
                        <li>                            
                            <a href="#dvGenerarRes" data-toggle="tab">Generar Resultados</a>                            
                        </li>                        
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="dvIntegralidad">

                            <table style="border-spacing: 5px; border-collapse: separate; width:100%;">
                                <tbody>
                                    <tr>
                                        <td style="width: 150px;">Representante</td>
                                        <td style="width: 150px;">
                                            <select id="ddlRepresentante" class="form-control" style="width: 250px;">
                                              </select>
                                        </td>
                                        <td style="width: 150px;">Cliente
                                            <button id="btnCargarClientes" 
                                                onclick="Integralidad.CargarClientes();" 
                                                type="button" 
                                                class="btn btn-default btn-sm"                                                 
                                                style="width:100%; display:none;">
                                                CargarClientes
                                            </button>  
                                        </td>
                                        <td>                                                           
                                            <div class="ui-widget">                                
                                            <select id="ddlRazonSocialCtes" class="form-control" style="width: 250px;">
                                              </select>
                                           </div>                                 
                                        </td>
                                        <td style="width: 100px;">
                                            <button id="btnBajarReporteExcel" 
                                                onclick="Integralidad.btnBajarReporteExcel_Click();" 
                                                type="button" 
                                                class="btn btn-default btn-sm"                                                 
                                                style="width:100%;">
                                                <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                                            </button>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Segmento</td>
                                        <td>
                                            <select id="ddlFiltroSegmentos" class="form-control"  style="width: 250px;">
                                            </select>
                                        </td>
                                        <td>
                                            <button id="btnAplicarConsulta" onclick="Integralidad.btnAplicar_Click();" type="button" class="btn btn-primary btn-sm">
                                                <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                                            </button>
                                        </td>
                                        <td> 
                                            <img id="spinner_Cargando" style="display: block; margin-top: 5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            
                            <table class="table table-hover table-bordered RadGrid_Outlook" id="tblIntegralidad" >
                                <thead>
                                    <tr>                                
                                        <!--th class="text-center" style="width:70px;" title="Tipo de Cliente">Rik</!--th-->
                                        <th class="text-center" style="width:70px;">
                                            <label id="ColumnaOrden_1" style="cursor:pointer;" data-col_id="1" data-orden="1" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Num. Cte.</label>
                                        </th>
                                        <th class="text-center">                                            
                                            <label id="ColumnaOrden_2" style="cursor:pointer;" data-col_id="2" data-orden="2" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Cliente</label>
                                        </th>
                                        <th class="text-center" style="width:100px;">
                                            <label id="ColumnaOrden_3" style="cursor:pointer;" data-col_id="3" data-orden="3" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Territorio</label>
                                        </th>
                                        <th class="text-center" style="width:250px;">
                                            <label id="ColumnaOrden_4" style="cursor:pointer;" data-col_id="4" data-orden="4" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Segmento</label>
                                        </th>
                                        <th class="text-center" style="width:85px;">
                                            <label id="ColumnaOrden_5" style="cursor:pointer;" data-col_id="5" data-orden="5" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Cantidad</label>
                                        </th>
                                        <th class="text-center" style="width:150px;">
                                            <label id="ColumnaOrden_6" style="cursor:pointer;" data-col_id="6" data-orden="6" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Unidad</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#f5c542" >
                                            <label id="ColumnaOrden_61" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">VPO</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#f5c542" >
                                            <label id="ColumnaOrden_62" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">VPO Meta</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#f5c542" >
                                            <label id="ColumnaOrden_7" style="cursor:pointer;" data-col_id="7" data-orden="7" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Promedio Trimestral</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#f5c542"">
                                            <label id="ColumnaOrden_8" style="cursor:pointer;" data-col_id="8" data-orden="8" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Integralidad vs Teo.</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#f5c542"">
                                            <label id="ColumnaOrden_9" style="cursor:pointer;" data-col_id="9" data-orden="9" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Integralidad vs Obs.</label>
                                        </th>
                                        <th style="width:50px;"></th>                                
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                           </table>    

                        </div>
                        <div role="tabpanel" class="tab-pane" id="dvGenerarRes">

                            <h4>Grafico de Integralidad por Representantes</h4>

                            <div style="display:block; width:50%; float:left; min-width:500px; text-align:left;" >                                                            

                                <table id="tblRepresentantesCheck" class="table table-hover table-bordered" style="width:90%;">
                                    <thead>
                                        <tr>                                            
                                            <td colspan="2">
                                                <label>Seleccione los representantes a comparar:</label>
                                            </td>
                                        </tr>
                                    </thead>
                                     <tbody></tbody>
                                    <tfoot>
                                        <tr>                                            
                                            <td colspan="2" class="text-right" >
                                                <table style="width:100%; border-spacing: 2px; border-collapse: separate;" >
                                                    <tr>
                                                        <td></td>
                                                        <td style="width:100px;">
                                                            <button 
                                                                id="btnCompararRepresentantes" 
                                                                onclick="Integralidad.GenerarResultados();" 
                                                                type="button" class="btn btn-default btn-sm" style="width:100px;">Mostrar
                                                            </button>
                                                        </td>
                                                        <td style="width:20px;" class="text-center" >
                                                            <img id="spinner_Mostrar" style="display: none; margin-top: 0px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                                        </td>
                                                    </tr>
                                                    </table>                                                
                                            </td>
                                            <tr>
                                    </tfoot>
                                </table>                                

                            </div>

                            <div style="display:block; width:50%; float:left; min-width:500px; text-align:center;" >                            
                                <canvas id="canvas1" style="display: block; width: 100%; height: 563px;" width="1126" height="563" class="chartjs-render-monitor"></canvas>
                            </div>

                        </div>
                    </div>
                        
                </div>           
            </div>
                        
            <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">


                    </div>
                </div>

        </div>            
    </div>  
    
    
<!--Modal -->
<div id="modalIntegralidad" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document" style="width:90%;">
        <div class="modal-content" >
            <div class="modal-header">                
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h2">Herramienta de Integralidad</h4></td>
                    <td></td>
                    <td>                        
                        <button id="Button3" class="btn btn-success btn-xs blink_text" style="display:none;" >Cargando...</button>                        
                    </td>                    
                    <td>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>                
                    </td>
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div3">

                    <div class="row" style="margin:10px;" >
                        <div class="col-sd-12" >
                            
                            <h3><strong>Datos de Representante</strong></h3>
                            <input type="hidden" id="hf_Id_Cte" value="0" />
                            <input type="hidden" id="hf_Id_Rik" value="0" />
                            <input type="hidden" id="hf_Id_Seg" value="0" />

                            <table class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center">RIK</th>
                                        <th class="text-center">Cliente</th>
                                        <th class="text-center">Territorio</th>
                                        <th class="text-center">Segmento</th>
                                        <th class="text-center">Val. Std.</th>
                                        <th class="text-center">Cantidad</th>
                                        <th class="text-center">Dimensión</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">                                            
                                            <label id="dr_rep"></label>
                                        </td>
                                        <td class="text-center"><label id="dr_cliente"></label></td>
                                        <td class="text-center"><label id="dr_id_ter"></label></td>
                                        <td class="text-center"><label id="dr_segmento"></label></td>
                                        <td class="text-center"><label id="dr_val_std"></label></td>
                                        <td class="text-center"><label id="dr_cantidad"></label></td>
                                        <td class="text-center"><label id="dr_seg_unidades"></label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>                        
                    </div>

                    <div class="row" style="margin:10px;">
                        <div class="col-sd-12">
                            
                            <table style="border-spacing: 5px; border-collapse: separate;">
                                <tr>
                                    <td>
                                        <h3><strong>Historial de Integralidad</strong></h3>
                                    </td>
                                    <td>
                                        <img id="spinner_HI" style="display:block; margin-top:-2px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />                    
                                    </td>
                                </tr>
                            </table>
                               <table id="tblHistorialTotales" class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center"></th>
                                        <th class="text-center">Venta (Est. de venta)</th>
                                        <th class="text-center">VPT</th>
                                        <th class="text-center">VPO</th>
                                        <th class="text-center">VPO Meta</th>
                                        <th class="text-center">Venta (Aplicación-Proyecto)</th>
                                        <th class="text-center" style="background-color:#f5c542">GAP vs Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542">GAP vs Observado</th>
                                        <th class="text-center" style="background-color:#f5c542">Integralidad vs Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542">Integralidad vs Observado</th>
                                        <th class="text-center" style="background-color:#f5c542">Oportunidad. Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542; width:100px;">Oportunidad. Observado</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>

                            <table id="tblHistorial" class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th class="text-center">Mes</th>
                                        <th class="text-center">Venta (Est. de venta)</th>
                                        <th class="text-center">VPT</th>
                                        <th class="text-center">VPO</th>
                                        <th class="text-center">VPO Meta</th>
                                        <th class="text-center">Venta (Aplicación-Proyecto)</th>
                                        <th class="text-center" style="background-color:#f5c542">GAP vs Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542">GAP vs Observado</th>
                                        <th class="text-center" style="background-color:#f5c542">Integralidad vs Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542">Integralidad vs Observado</th>
                                        <th class="text-center" style="background-color:#f5c542">Oportunidad. Teórico</th>
                                        <th class="text-center" style="background-color:#f5c542; width:100px;">Oportunidad. Observado</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>                        
                    </div>

                    <div class="row" style="margin:10px;">
                        <div class="col-sd-12">

                            <h3><strong>Gráfico de Integralidad por Mes</strong></h3>

                            <div id="DivCanvas2" style="display:block; width:50%; float:left; min-width:500px; text-align:center;" >                            
                                <canvas id="canvas2" style="display: block; width: 100%; height: 563px;" width="1126" height="563" class="chartjs-render-monitor"></canvas>
                            </div>
                            
                        </div>                        
                    </div>
                                        
                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" id="modalBucarCliente_Cancelar" data-dismiss="modal">Cancelar</button>            
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div id="modalIntegralidadVPOMeta" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document" style="width:90%;">
        <div class="modal-content" >
            <div class="modal-header">                
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h2VPOMeta">Editar Valor Potencial Meta (VPM)</h4></td>
                    <td></td>
                    <td>                        
                        <button id="Button3VPOMeta" class="btn btn-success btn-xs blink_text" style="display:none;" >Cargando...</button>                        
                    </td>                    
                    <td>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                            <span aria-hidden="true">&times;</span>
                        </button>                
                    </td>
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div3VPOMeta">

                    <div class="row" style="margin:10px;" >
                        <div class="col-sd-12" >
                            
                            <input type="hidden" id="hf_Id_CteVPOMeta" value="0" />
                            <input type="hidden" id="hf_Id_RikVPOMeta" value="0" />
                            <input type="hidden" id="hf_Id_SegVPOMeta" value="0" />

                            <table class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center">RIK</th>
                                        <th class="text-center">Cliente</th>
                                        <th class="text-center">Territorio</th>
                                        <th class="text-center">Segmento</th>
                                        <th class="text-center">Val. Std.</th>
                                        <th class="text-center">Cantidad</th>
                                        <th class="text-center">Dimensión</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">                                            
                                            <label id="dr_repVPOMeta"></label>
                                        </td>
                                        <td class="text-center"><label id="dr_clienteVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_id_terVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_segmentoVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_val_stdVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_cantidadVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_seg_unidadesVPOMeta"></label></td>
                                    </tr>
                                </tbody>
                            </table>

                                        <div class="panel-body;">
                                        <div style="width:100%;">
                                        <table class="table_ter" style="border-spacing:5px; border-collapse:separate;" >
                                            <tr>
                                                <td style="text-align:left">
                                                    <label>Valor Potencial Meta (VPM)</label>
                                                </td>
                                                <td style="text-align:left">
                                                    <input type="text" id="txtPotencialMeta" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                                                </td>                                                
                                            </tr>
                                        </table>
                        </div>                        
                    </div>


                        </div>                        
                    </div>


                                        
                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" id="modalBucarCliente_CancelarVPOMeta" data-dismiss="modal">Cancelar</button> 
                        <button type="button" id="btnProspectoEditarGuardarVPOMeta" class="btn btn-primary" onclick="Integralidad.actualizarVPOMeta()">Guardar</button>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    <!--Toast messages-->
    <!--Login dialog-->
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
                    <form id="frmDvDialogoInicioSesion">
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
                        onclick="login(jQuery)">
                        Confirmar
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--Login dialog-->
    <script type="text/javascript">
        var _ApplicationUrl = '<%=ApplicationUrl %>';
        var hfId_TU = '<%=Id_TU1 %>';
        var hfId_CD = '<%=Id_CD %>';
        var hfId_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
    </script>

    <%--JQuery UI --%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>                
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20220810")%>"></script>
    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <%--date format --%>
    <script src="<%=Page.ResolveUrl("~/js/date.format.js")%>"></script>        
    <%--alertify--%>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
    <%-- CHART --%>
    <script src="<%=Page.ResolveUrl("~/Librerias/Chart.js-master/dist/Chart.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/Chart.js-master/dist/Chart.bundle.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/Chart.js-master/samples/utils.js")%>"></script>   
    <script src="<%=Page.ResolveUrl("~/js/placeholder-setup.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>                
    <!--script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script-->
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/MapaAplicaciones.js")%>"></script>
    <!--script src="<%=Page.ResolveUrl("~/js/CRM2/Informe.js")%>"></!--script-->
    <script src="<%=Page.ResolveUrl("~/js/Integralidad/IntExcel.js?v=20220822")%>"></script>    
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Integralidad/IntAutocompletar.js?v=20220822")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Integralidad/Integralidad2.js?v=20220825")%>"></script>
   
</asp:Content>