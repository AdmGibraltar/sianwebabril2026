<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/PortalRIK.Master" CodeBehind="Integralidadv2.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Integralidadv2" %>

<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/Integralidad/integralidadv2.css")%>">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/font-awesome.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    
    <style>
        .searchbutton {
            margin-top: -24px;
            margin-left: 160px;
            border: none !important;
            background-color: #ffffff !important;
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
            width: 205px;
        }
        .table_detailinfo {
            display: none;
            background:#ffffff;
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
                        <li style="display:none">                            
                            <a href="#dvGenerarRes" data-toggle="tab">Generar Resultados</a>                            
                        </li>                        
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="dvIntegralidad">

                            <table style="border-spacing: 5px; border-collapse: separate; width:100%;">
                                <tbody>
                                    <tr>
                                        <td style="width: 30px;">Región</td>
                                        <td style="width: 50px;">
                                            <label id="lblZona">-----</label>
                                        </td>
                                        <td style="width: 30px;">CDI</td>
                                        <td style="width: 30px;">
                                            <label id="lblCd" >-----</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px;">Periodo</td>
                                        <td style="width: 30px;">
                                            <input type="text" id="tbPeriodoInicio" class="form-control"  style="width: 100px;"/>
                                        </td>
                                        <td style="width: 50px;">Representante</td>
                                        <td style="width: 50px;">
                                            <select id="ddlRepresentanteV2" class="form-control" style="width: 150px;">
                                              </select>
                                        </td>
    
                                        <td style="width: 100px;">
                                                
        
                                        </td>
                                    </tr>
                                    <tr>
                                           
                                        <td>UEN</td>
                                        <td>
                                            <select id="ddlFiltroUen" class="form-control"  style="width: 250px;">
                                            </select>
                                        </td>
                                         <td>Segmento</td>
                                          <td>
                                              <select id="ddlFiltroSegmentos" class="form-control"  style="width: 250px;">
                                              </select>
                                          </td>    
                                       
                                        <td>
                                               <button id="btnAplicarConsulta" onclick="Integralidadv2.btnAplicarv2_Click();" type="button" class="btn btn-primary btn-sm">
                                                  <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                                              </button>  
                                        </td>
                                        <td> 
                                            <img id="spinner_Cargando" style="display: block; margin-top: 5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                                        </td>
                                    </tr>
                                    <tr id="trbtndownload">
                                        <td style="width: 150px;">Cliente
                                            <button id="btnCargarClientes" 
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
                                        <td>
                                           <button id="btnBajarReporteExcel" 
                                               onclick="Integralidadv2.btnBajarReporteExcelV2_Click();" 
                                               type="button" 
                                               class="btn btn-default btn-sm"                                                 
                                               style="width:100%;">
                                               <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                                           </button>   
                                       </td>
                                       <td>
                                             <button id="btnAplicarConsultadetalle" onclick="Integralidadv2.onClickDescargaExcelAplicacionesDetalle();" type="button" class="btn btn-default btn-sm">
                                                     <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte detallado
                                           </button>
                                       </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="max-height:400px;overflow-y: auto">
                                 <table class="table table-hover table-bordered RadGrid_Outlook" id="tblIntegralidad" >
                                    <thead>
                                    <tr>                                
                                        <!--th class="text-center" style="width:70px;" title="Tipo de Cliente">Rik</!--th-->
                                         <th class="text-center" style="width:70px;">
                                            <label id="ColumnaOrden_semaforo" style="cursor:pointer;" data-col_id="0" data-orden="0" data-dir="0" onclick="Integralidad.btnColumna_Click(this);"></label>
                                        </th>
                                        <th class="text-center" style="width:70px;">
                                            <label id="ColumnaOrden_1" style="cursor:pointer;" data-col_id="1" data-orden="1" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Num. Cte.</label>
                                        </th>
                                        <th class="text-center">                                            
                                            <label id="ColumnaOrden_2" style="cursor:pointer;" data-col_id="2" data-orden="2" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Cliente</label>
                                        </th>
                                        <th class="text-center">                                            
                                            <label id="ColumnaOrden_23" style="cursor:pointer;" data-col_id="2" data-orden="2" data-dir="0" onclick="">Tamaño Cliente</label>
                                        </th>
                                        <th class="text-center">                                            
                                            <label id="ColumnaOrden_224" style="cursor:pointer;" data-col_id="2" data-orden="2" data-dir="0" onclick="">UEN</label>
                                        </th>
                                        <th class="text-center">                                            
                                            <label id="ColumnaOrden_22" style="cursor:pointer;" data-col_id="2" data-orden="2" data-dir="0" onclick="">Segmento</label>
                                        </th>
                       
                                        <th class="text-center" style="width:150px;background-color:#0a86c9;color:white">
                                            <label id="ColumnaOrden_6" style="cursor:pointer;" data-col_id="6" data-orden="6" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Venta</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_VPT" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">VPT <br/> Valor Potencial Teórico</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_61" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">VPO <br/> Valor Potencial Observado</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_612" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">% Cobertura VPT</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_613" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">% Cobertura VPO</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_62" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">% Integralidad  Aplicaciones</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_GAPVPO" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">% Potencial Integralidad  Aplicaciones</label>
                                        </th>
                                        <th class="text-center" style="width:85px; background-color:#0a86c9;color:white" >
                                            <label id="ColumnaOrden_GAPVPT" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">Editar Valores Potenciales</label>
                                        </th>                             
                                    </tr>
                                    <tr>                                
                                         <!--th class="text-center" style="width:70px;" title="Tipo de Cliente">Rik</!--th-->
                                          <th colspan="6"  class="text-center" style="width:70px;">
                                             <label id="ColumnaOrden_semaforo" style="cursor:pointer;" data-col_id="0" data-orden="0" data-dir="0" onclick="Integralidad.btnColumna_Click(this);"></label>
                                         </th>
                       
                                         <th class="text-center" style="width:150px;">
                                             <label id="lbltotVenta" style="cursor:pointer;" data-col_id="6" data-orden="6" data-dir="0" >0</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_VPT" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_VPO" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_PorcCoberturaVPT" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0%</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_PorcCoberturaVPO" style="cursor:pointer;" data-col_id="61" data-orden="61" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0%</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_PorcIntegralidadApp" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0%</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                             <label id="lbl_PorcPotencialIntegralidadApp" style="cursor:pointer;" data-col_id="62" data-orden="62" data-dir="0" onclick="Integralidad.btnColumna_Click(this);">0%</label>
                                         </th>
                                         <th class="text-center" style="width:85px; " >
                                         </th>                            
                                     </tr>
                                </thead>
                                <tbody>
                                </tbody>
                           </table>  
                            </div>
                             
                           
                          
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
                            
                    
                            

                            <table class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center">RIK</th>
                                        <th class="text-center">Cliente</th>
                                        <th class="text-center">Territorio</th>
                                        <th class="text-center">Segmento</th>
<%--                                        <th class="text-center">Val. Std.</th>
                                        <th class="text-center">Cantidad</th>
                                        <th class="text-center">Dimensión</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">                                            
                                            <label id="dr_repVPOMeta"></label>
                                        </td>
                                        <td class="text-center"><label id="dr_clienteVPOMeta"></label></td>
                                        <%--<td class="text-center"><label id="dr_id_terVPOMeta"></label></td>--%>
                                        <td class="text-center"><label id="dr_segmentoVPOMeta"></label></td>
<%--                                        <td class="text-center"><label id="dr_val_stdVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_cantidadVPOMeta"></label></td>
                                        <td class="text-center"><label id="dr_seg_unidadesVPOMeta"></label></td>--%>
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
                                                  <%--  <input type="text" id="txtPotencialMeta" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />--%>
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
                        <button type="button" id="btnProspectoEditarGuardarVPOMeta" class="btn btn-primary" onclick="Integralidadv2.actualizarVPOMetav2()">Guardar</button>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

    <div id="modalIntegralidadPotencial" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document" style="width:90%;">
        <div class="modal-content" >
            <div class="modal-header">    
                 
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h2VPOMeta">Editar Valores Potenciales</h4></td>
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
                <div class="modal-body" id="Div34POMeta">
                    <input type="hidden" id="hf_Id_CteVPOMeta" value="0" />
                    <input type="hidden" id="hf_Id_RikVPOMeta" value="0" />
                    <input type="hidden" id="hf_Id_SegVPOMeta" value="0" />
                    <input type="hidden" id="txtAnio" value="0" />
                    <input type="hidden" id="txtMes" value="0" />
                    <input type="hidden" id="dr_id_terVPOMeta" value="0" />
                    
                    <div class="col-lg-12">
                          <h3 style="color:dodgerblue"> <strong>VPT (VALOR POTENCIAL TEÓRICO):</strong></h3>
                        <div class="form-group">
                            <label for="selUEN">CLIENTE</label>&nbsp
    
                            <input type="text"  readonly disabled id="txtClienteV2" class="form-control"/>
                        </div>
                        <div class="form-group">
                            <label for="selUEN">UEN</label>&nbsp
                            <i class="fa fa-industry" aria-hidden="true"></i>
                    <%--        <select id="selUEN" name="Uen" disabled class="selectpicker form-control">
                            </select>--%>
                            <input type="text"  readonly disabled id="txtUen" class="form-control"/>
                            <input type="hidden"  readonly disabled id="txtIdUen" class="form-control"/>
                        </div>
                        <div class="form-group">
                            <label for="selSegmento">Segmento&nbsp
                                <i class="fa fa-tasks" aria-hidden="true"></i>
                                <img id="imgProcesandoSegmentoDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /> 
                             </label>
                       <%--     <select id="selSegmento" name="Segmento" disabled class="selectpicker form-control">
                            </select>--%>
                            <input type="text"  readonly disabled id="txtSegmento" class="form-control"/>
                            <input type="hidden"  readonly disabled id="txtIdSegmento" class="form-control"/>
                        </div>

                        <div class="row">
                            <div class="col-md-5">
                                <div class="input-group tooltip-demo">
                                    <small>Dimensión:</small>
                                    <input id="txtDimension" type="text" class="form-control" disabled placeholder="Dimension" title="Unidad de la dimensión del Segmento" data-toggle="tooltip" />
                                    <!--<button class="input-group-addon" data-toggle="modal" data-target="#dvModalDimension" type="button"><i class="fa fa-search fa-fw"></i></button>-->
                                    <input type="hidden" id="hdnDim_Id_Uen" name="Dim_Id_Uen" />
                                    <input type="hidden" id="hdnDim_Id_Seg" name="Dim_Id_Seg" />
                                </div>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                    <small>Precio:</small>
                                    <input id="txtPrecioUnidad" type="text" class="form-control" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" placeholder="$0.0" title="Precio por unidad de dimensión" data-toggle="tooltip" disabled/>
                                </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>Cantidad:</small>
                                <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="0" title="Cantidad de la unidad elegida" data-toggle="tooltip" data-inputmask="'alias': 'numeric', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true', 'allowMinus':'false' " onchange="Integralidadv2.onChangeCantidadVPT(this)"/>
                            </div>
                            <div class="col-md-3 tooltip-demo">
                                <small><strong>VPT (VALOR POTENCIAL TEORICO):</strong>
                                </small>
                                <input placeholder="$"   id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" disabled placeholder="$0.0" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" title="Venta Promedio Mensual Esperada" data-toggle="tooltip"/>
                            </div>
                        </div>
                        <div class="row">
                             <div class="col-md-3 tooltip-demo">
                              <h3 style="color:dodgerblue"> <strong>VPO</strong></h3> <h6 style="color:dodgerblue"><strong>(VALOR POTENCIAL OBSERVADO):</strong></h6>
                              <input   placeholder="$" type="text" id="txtPotencialMeta2" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                             </div>
                        </div>
                    </div>
                                        
                </div>
                
                
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" id="modalBucarCliente_CancelarPotenciales" data-dismiss="modal">Cancelar</button> 
                        <button type="button" id="btnProspectoEditarGuardarPotenciales" class="btn btn-primary" onclick="Integralidadv2.actualizarVPOMetav2()">Guardar</button>                        
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
        $(document).ready(function () {

            $('#tbPeriodoInicio').Zebra_DatePicker({
                format: 'm Y',
            });
        });
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
        <!-- ZEBRA -->
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/default/zebra_datepicker.css")%>" rel="stylesheet">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Integralidad/IntAutocompletar.js?v=20220822")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Integralidad/Integralidadv2.js?v=15")%>"></script>
   
</asp:Content>