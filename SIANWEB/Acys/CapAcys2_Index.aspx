<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
    AutoEventWireup="true" CodeBehind="CapAcys2_Index.aspx.cs" Inherits="SIANWEB.CapAcys2_Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<%--    
	15FEB-2021 Actuaizacion RFH Renovacion 
	
    27ENE-2020 Actuaizacion RFH
    Se Agrega Identificacion de Cuentas Nacionales
    MAR2-2020 Actuaizacion RFH
    La busqueda de clientes se hace por el RIK (Rik-Terr-Cliente)
--%>
    
    <!-- ALERTIFY 0.3.11 NUEVO -->
    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
        
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        

    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>

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
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_acys.css?v=1")%>" rel="stylesheet">

    <%--TIME PICKER--%>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>

    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/xlsxl.min.js")%>"></script>

  <style>
      .custom-file-upload {
        display: inline-block;
        padding: 8px 16px;
        background-color: #007bff;
        color: #fff;
        border-radius: 4px;
        cursor: pointer;
    }

    .custom-file-upload span {
        font-weight: bold;
    }
  </style>
<div class="container-fluid">

<div class="row mt5">
    <div class="col-md-12">


        <table id="Table1">
            <tbody>
                <tr>
                    <td></td>
                     <td>Por Fechas
                         &nbsp; &nbsp; 
                     </td>
                     <td>Por Folios
                          &nbsp; &nbsp; 
                     </td>
                     <td>Por Cliente
                          &nbsp; &nbsp; 
                     </td>
                     <td>Estatus
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; 
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; 
                          &nbsp;  &nbsp; 
                     </td>
                     <td>Vencido
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;
                          &nbsp;  &nbsp; 
                     </td>
                    <td style="width:150px;">Tipo de Modalidad

                    </td>
                    <td>Tipo de Cuenta
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  
                          &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;
                          &nbsp;  &nbsp; 
                    </td>
                    
                    <td>
                    </td>
                    <td> 
                    </td>
                    <td>
                    </td>
                  
                </tr>
            </tbody>
        </table>

        <table id="tbl1">
            <tbody>
                <tr>
                     <td></td>
                     <td>
                          <input type="checkbox" id="chbPorFechas" />
                           &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                           &nbsp; &nbsp; 
                     </td>
                     <td>
                          <input type="checkbox" id="chbPorFolios" />
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                          &nbsp; &nbsp; 
                     </td>
                     <td>
                        <input type="checkbox" id="chbPorCliente" />
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                          &nbsp; &nbsp; 
                     </td>
                     <td>
                          <select id="ddlEstatus" class="form-control" style="width:100%;">
                            <option value="0">-- Todos --</option>
                            <option value="B">Baja</option>
                            <option value="C">Capturado</option>
                            <option value="S">Solicitado</option>
                            <option value="S1">Solicitado Gerente</option>
                            <option value="S2">Solicitado JO.</option>
                            <option value="A">Autorizado</option>
                            <option value="R">Rechazado</option>
                        </select>

                     </td>
                     <td>
                        <select id="ddlVencido" class="form-control" style="width:100%;margin-left:10px;">
                        <option value="0">-- Todos --</option>
                        <option value="1">SI</option>
                        <option value="2">NO</option>
                        </select>
                     </td>
                     <td style="width:160px;">                     
                        <select id="ddlModalidad" class="form-control" style="width:130px;margin-left:20px;">
                        <option value="0">-- Todos --</option>
                        <option value="1">REGULAR</option>
                        <option value="2">GARANTIA</option>                        
                        </select>
                     </td>
                    <td style="width:170px;">
                        <select id="DdlExcelTipoCuenta" class="form-control" style="width:150px;">
                             <option value="3">-- Todos --</option>
                             <option value="0">Local</option>
                             <option value="1">Nacional</option>
                             <option value="2">Coordinada</option>
                        </select>
                    </td>
                    <td>
                         <button id="btnCargarListado" type="button" class="btn btn-primary btn-sm" onclick="btnCargarListado_Evento();">
                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Aplicar
                        </button>
                    </td>
                    <td>
                        <button id="btnAcys_BajarReporteExcel" onclick="Acys_BajarReporteExcel();"  type="button" class="btn btn-default btn-sm" style="width:100%;" >
                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Bajar reporte
                        </button>
                    </td>
                    <td>
                        <button id="btnAcysNuevo" type="button" class="btn btn-primary btn-sm" style="width:100%;" >
                            <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Nuevo
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>   
</div>


<!-- FILTRO FECHA -->
<div class="row" id="rowFechas" style="margin-top:5px; display:none;">    
    <div class="col-md-4">        
        <table>
            <tr>
                <td style="vertical-align:middle; width:100px;">Fechas : de&nbsp; </td>                
                <td>    
                    <input type="text" id="tbFechaInicio" class="form-control datepicker wfecha" value="">                                                            
                </td>
                <td style="vertical-align:middle;">&nbsp;a&nbsp;</td>
                <td>
                    <input type="text" id="tbFechaFin" class="form-control datepicker wfecha" value="">                                            
                </td>
                <td id="ContenedorFechas"></td>
                <td style="">
                    <img id="spinner_AcysIndice" style="display:none; margin-top:5px;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />                    
                </td>
            </tr>
        </table>
    </div>    
</div>

<!-- FILTRO FOLIOS-->
<div class="row" id="rowFolios" style="margin-top:5px; display:none;">
    <div class="col-md-4">        
        <table>
            <tr>
                <td style="vertical-align:middle; width:100px;">Folio: de&nbsp;</td>
                <td><input type="text" id="tbFolioIncial" class="form-control" /></td>
                <td style="vertical-align:middle;">&nbsp;a&nbsp;</td>
                <td><input type="text" id="tbFolioFinal" class="form-control" /></td>
                <td id="ContenedorFolio"></td>
            </tr>
        </table>
    </div>    
</div>

<!-- FILTRO CLIENTE -->
<div class="row" id="rowCliente" style="margin-top:5px; display:none;">
    <div class="col-md-4">        
        <table>
            <tr>
                <td style="vertical-align:middle; width:100px;">Cliente: &nbsp;</td>
                <td><input type="text" id="tbNumeroCliente" class="form-control" style="width:100px;"/></td>                                
                <td><input type="text" id="tbNombreCliente" class="form-control" style="width:300px;"/></td>                                
                <td id="ContenedorCliente"></td>
            </tr>
        </table>
    </div>    
</div>

<div class="row mt5" style="margin-bottom:20px;">
    <div class="col-md-12 text-center">
        <!--div class="Loading" id="dvAcysIndex_Lading"></div-->    
       <table class="table table-hover table-bordered RadGrid_Outlook" id="tblAcysIndex" >
            <thead>
                <tr>
                    <% if (GLOBAL_Activo_AcysCuentasNacionales == 1) { %>
                        <th class="text-center" style="width:40px;" title="Tipo de Cliente" >Tipo de Cliente</th>
                    <% } %>
                    <th class="text-center">Folio</th>
                    <th class="text-center" style="width:40px;">Ver.</th>
                    <th class="text-center" style="width:70px;">Estatus</th>
                    <th class="text-center">Núm</th>
                    <th class="text-center">Cliente</th>
                    <th class="text-center" style="width:85px;">Terr.</th>
                    <th class="text-center">Rik</th>
                    <th class="text-center" style="width:85px;">Fecha</th>
                    <th class="text-center" style="width:90px;">Fecha Inicio</th>
                    <th class="text-center" style="width:90px;">Fecha Fin</th>
                    <th class="text-center" style="width:20px;" >Vencido</th>
                    <!--th style="width:20px; text-align:center;">CN</!--th-->
                    <!--th>Modalidad</th-->
                    <!--th>CN</!--th-->
                    <th style="width:100px;" colspan="8"></th>
                    <!--th>Editar</th>
                    <th>Baja</th>
                    <th>Imprimir</th>
                    <th>Renovar</th>
                    <th>Enviar</th>
                    <th>Versiones</th>
                    <th>Autorizar</th-->
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

<!--Modal * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  -->
<div id="modalAcys" data-backdrop="static" data-keyboard="false" class="modal" role="dialog" tabindex="-1" style="z-index:1010!important;" >
    <div class="modal-dialog" role="document" style="width:1050px;">
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
                        <% if (GLOBAL_Activo_AcysCuentasNacionales == 1) { %>
                        <td class="text-center"><label id="lbAcysCNTipo"></label>Tipo Cuenta</td>
                        <% } %>
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
                            <input type="hidden" id="hfId_Acys" value="0" />
                            <input type="hidden" id="hfId_Matriz" value="0" />
                            <input id="hfAcysCNTipo" type="hidden" value="0"></input>
                            <h4 id="tbId_Acs"></h4>                                            
                        </td>
                        <td><input type="text" id="tbAcs_Fecha" class="form-control datepicker wfecha" /></td>                                                                                    
                        <td><input type="text" id="tbAcs_FechaInicio" class="form-control datepicker wfecha" /></td>                                                                                    
                        <td><input type="text" id="tbAcs_FechaFin" class="form-control datepicker wfecha" /></td>
                        <% if (GLOBAL_Activo_AcysCuentasNacionales == 1) { %>
                        <td class="text-center"><div id="divAcysCN"></div></td>
                        <% } %>
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
                         <li>
                            <a id="tabOpcPrdNeg" href="#tabProductosNegociados" data-toggle="tab" style="display:none;">Productos Negociados</a>
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
                                                    <select id="lbEspAd_CuentaCorporativa" style="width:100px;" disabled="disabled" class="form-control" >
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

                                            <tr>
                                                 <td>Coordinador Cuenta</td>
                                                <td>
                                                    <label id="lblCordinadorCuenta"></label>
                                                </td>
                                            </tr>

                                            <tr>
                                                 <td>Correo</td>
                                                <td>
                                                    <label id="lblCordinadorCuentaCorreo"></label>
                                                </td>
                                           </tr>

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

                                    <div class="tab-pane active" id="DivtabProductos" style="height:500px; display:block; overflow-x:scroll;" >   
                                                                                                                   
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
                                                        <th class="h_cel_center" style="width:80px!important;">Pres.</th>
                                                        <th class="h_cel_center" style="width:50px!important;">Uni.</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Cantidad</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Lista</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Precio Venta</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Subtotal</th>
                                                        <th class="h_cel_center" style="width:120px!important">Frecuencia</th>
                                                        <th class="h_cel_center" style="width:120px!important">Fecha Ini.</th>
                                                        <th class="h_cel_center" style="width:100px!important">Doc. de entrega</th>
                                                        <th class="h_cel_center" style="width:70px!important;">Requiere Orden Compra</th>
                                                        <th style="width:50px!important;"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>                                       
                                                <tfoot>
                                                    <td colspan="7" align="right"><label>Suma</label></td>
                                                    <td align="right"><label id="lbAcysProductosSuma">0.00</label></td>
                                                    <td colspan="4"></td>
                                                    </tfoot>
                                            </table>  

                                    </div>
                                 
                                    <table style="border-collapse:separate; border-spacing:5px; width:100%;">
                                    <tr>
                                        <td>
                                            <!-- style="background-color:#caccd1;" -->
                                            <button class="btn btn-primary btn-xs btn-primary" id="btnAgregarRenglon" >
                                            <i class="fa fa-plus fa-lg"></i>&nbsp;Agregar producto 
                                            </button>
                                        </td>                                        
                                        <td>
                                            <button class="btn btn-primary btn-xs" id="btnAgregarRenglon_CC" style="background-color:#ffc845;" >
                                            <i class="fa fa-plus fa-lg"></i>&nbsp;Agregar producto 
                                            </button>
                                        </td>   
                                        <td>
                                            <button class="btn btn-primary btn-xs" id="btnUploadExcel"  >
                                               Cargar producto Excel
                                            </button>
                                        </td>  
                                        <td style="width:150px;">
                                            <button class="btn btn-primary btn-xs" id="btnActualizarColumna" style="width:100%;">
                                                <i class="fa fa-pencil-square-o"></i>&nbsp;Modificar por columna
                                            </button>
                                        </td>                                        
                                    </tr>
                                    </table>

                                    <% if (GLOBAL_Activo_AcysCuentasNacionales == 1) { %>
                                    <table style="border-collapse:separate; border-spacing:10px; width:100%;">
                                    <tr>
                                        <td style="width:50px;">
                                            <div style="width:30px; height:30px; background-color:#ffc845;">&nbsp;</div>
                                        </td>
                                        <td>
                                            <label>Productos negociados por cuenta corporativa.(ACYS central/matrices).</label>
                                        </td>
                                        </tr><tr>
                                        <td style="width:50px;">
                                            <div style="width:30px; height:30px; background-color:#caccd1;">&nbsp;</div>
                                        </td>
                                        <td>
                                            <label>Productos negociados de manera local</label>
                                        </td>
                                    </tr>
                                    </table>
                                    <% } %>
                                    
                                    <table>
                                    <tr>
                                        <td>                                            
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

                                </div>
                            </div>
                            
                        </div>
                             <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <%--TAB PRODUCTOS NEGOCIADOS --%>
                        <%--/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <div class="tab-pane" id="tabProductosNegociados">                    

                            <div class="panel panel-default mt5">                                
                                <div class="panel-heading titulo_blod">Buscar Producto</div>
                                <div class="panel-body">
                                    <table>
                                        <tr>
                                            <td style="width:650px;">                                                             
                                                <input id="txtProductoBuscar" type="text" class="form-control">
                                            </td>
                                            <td class="lb_z1"><button class="btn btn-primary btn-primary" id="btnBuscarProducto" onclick="ProdNgc.LimparBusqueda()">
                                            <i class="fa fa-eraser fa-lg"></i>&nbsp; 
                                            </button></td>
                                            <td>
                                               
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

                                    <div class="tab-pane active" id="DivtabProductosNegociados" style="height:500px; display:block; overflow-x:scroll;" >   
                                                                                                                   
                                            <table class="table table-hover RadGrid_Gis scroll" id="tblAcuerdoProdsNegociados">
                                                <thead>
                                                    <tr>     
                                                        <%--
                                                            
                                                            CLAQUIER CAMBIO AQUI MODIFICAR EN GC_RepCumplimientoVtaInstalada_Index.aspx
                                                            
                                                        --%>

                                                        <th style="width:5px!important;"></th>
                                                        <th class="h_cel_center" style="width:30px; display:none;" >#</th>
                                                        <th class="h_cel_center" style="width:50px;" >
                                                            <button class="btn btn-xs btn-warning" id="btnAllProductCN" onclick="ProdNgc.EnviarAcuerdoAll();" style="margin-bottom:10px;" >
                                                                <i class= "fa fa-plus fa-lg"></i >&nbsp;</button>
                                                            <input type="checkbox" id="chbAllProdsNegociados" onclick="ProdNgc.chkTodosProdsNegociados(this)" class="form-control chb" style="margin-left:2px;"/></th>
                                                        <th class="h_cel_center" style="width:100px;" >Código</th>
                                                        <th class="h_cel_center">Descripción</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Pres.</th>
                                                        <th class="h_cel_center" style="width:50px!important;">Uni.</th>
                                                        <th class="h_cel_center" style="width:80px!important;">Precio Lista</th>
                                                        <th class="h_cel_center" style="width:100px!important;">Precio Venta</th>
                                                        <th class="h_cel_center" style="width:100px!important">Doc. de entrega</th>
                                                        <th class="h_cel_center" style="width:70px!important;">Requiere Orden Compra</th>
                                                        <th class="h_cel_center" style="width:120px!important;">Añadir a Acuerdo económico de producto</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>                                       
                                                <tfoot>
                                                    <tr>
                                                    <td></td>
                                                    <td ></td>                                                   
                                                    <td colspan="10"></td>
                                                    </tr></tfoot>
                                            </table>  

                                    </div>
                                 
                                    <table style="border-collapse:separate; border-spacing:5px; width:100%;">
                                    <tr>
                                        <td> </td>                                        
                                        <td> </td>                                        
                                        <td> </td>                                        
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
<div id="modalActualizarDatos" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document" style="width:500px;">
        <div class="modal-content">
            <div class="modal-header">
                
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h2">Actualizar por columna</h4></td>
                    <td>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                    <span aria-hidden="true">&times;</span>
                </button>                
                    </td>                    
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div3">

                    <table style="width:100%;" >                        
                        <tr>
                            <td style="width:100px;"><label>Columna</label></td>
                            <td style="width:100px;"><label>donde el valor sea</label></td>
                            <td style="width:100px;"><label>Reemplazar con el valor</label></td>
                            <td></td>                                                        
                        </tr>
                        <tr>
                            <td>Cantidad</td>                                                        
                            <td>
                                <input type="text" id="tbActualizarCol_CantDonde" class="form-control" />
                            </td>                                                        
                            <td>
                                <input type="text" id="tbActualizarCol_CantPoner" class="form-control" />
                            </td>                                                        
                            <td>
                                <button onclick="ListadoProductos.Actualiza_Cant();" class="btn btn-primary">
                                    <i class="fa fa-check clickable"></i> Aplicar
                                </button>                                
                            </td>                                                        
                        </tr>
                        
                        <tr>
                            <td>No Frecuencia</td>                                                        
                            <td>                                
                                <input type="text" id="tbActualizarCol_FrecDonde" class="form-control" />
                            </td>                                                        
                            <td>
                                <input type="text" id="tbActualizarCol_FrecPoner" class="form-control" />
                            </td>                                                        
                            <td>
                                <button onclick="ListadoProductos.Actualiza_NoFrec();" class="btn btn-primary">
                                    <i class="fa fa-check clickable"></i> Aplicar
                                </button>                                
                            </td>                                                        
                        </tr>

                        <tr>
                            <td>Tipo de Frecuencia</td>                                                        
                            <td>                                
                                <select id="tbActualizarCol_TFrecDonde" class="form-control input-sm">
                                <option value="0">-</option>
                                <option value="1">Semanal</option>
                                <option value="2">Mensual</option>
                                <option value="3">Bimestral</option>
                                <option value="4">Trimestral</option>
                                <option value="5">Semestral</option>
                                </select>
                            </td>                                                        
                            <td>
                                <select id="tbActualizarCol_TFrecPoner" class="form-control input-sm">
                                <option value="0">-</option>
                                <option value="1">Semanal</option>
                                <option value="2">Mensual</option>
                                <option value="3">Bimestral</option>
                                <option value="4">Trimestral</option>
                                <option value="5">Semestral</option>
                                </select>
                            </td>                                                        
                            <td>
                                <button onclick="ListadoProductos.Actualiza_TipoFrec();"  class="btn btn-primary">
                                    <i class="fa fa-check clickable"></i> Aplicar
                                </button>                                
                            </td>                                                        
                        </tr>

                        <tr>
                            <td>Doc. de Entrega</td>                                                        
                            <td>
                                <select id="cmbActualizarCol_DocEntregaDonde" class="form-control input-sm" >
                                <option value="-">-</option>
                                <option value="F">Factura</option>
                                <option value="R">Remisión</option>
                                </select>
                            </td>                                                        
                            <td>
                                <select id="cmbActualizarCol_DocEntregaPoner" class="form-control input-sm" >
                                <option value="-">-</option>
                                <option value="F">Factura</option>
                                <option value="R">Remisión</option>
                                </select>
                            </td>                                                        
                            <td>
                                <button onclick="ListadoProductos.Actualiza_DocEntrega();" class="btn btn-primary">
                                    <i class="fa fa-check clickable"></i> Aplicar
                                </button>                                
                            </td>                                                        
                        </tr>
                        
                        <tr>
                            <td>Requiere Orden Compra</td>                                                        
                            <td>                                
                                <select id="cmbActualizarCol_ReqOC_Donde" class="form-control input-sm" >
                                <option value="">-</option>
                                <option value="1">Si</option>
                                <option value="0">No</option>
                                </select>
                            </td>                                                        
                            <td>
                                <select id="cmbActualizarCol_ReqOC_Poner" class="form-control input-sm" >
                                <option value="">-</option>
                                <option value="1">Si</option>
                                <option value="0">No</option>
                                </select>
                            </td>                                                        
                            <td>
                                <button onclick="ListadoProductos.Actualiza_ReqOC();" class="btn btn-primary">
                                    <i class="fa fa-check clickable"></i> Aplicar
                                </button>                                
                            </td>                                                        
                        </tr>

                    </table>
             
                                        
                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" data-dismiss="modal">Cancelar</button>            
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
                    <br/> <br/> <br/>
                    <label id="lbBuscarCliente_TieneACYS" style="color:red" ></label>
                    <br />
                    <label id="lbBuscarCliente_TieneACYSNoAcuerdo" style="color:red" ></label>

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

<!--Modal -->
<div id="modalAcysMensaje" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none;" >
    <div class="modal-dialog vertical-center" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                    <span aria-hidden="true">&times;</span>
                </button>                

                <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                
                </div>
                <div class="modal-body" id="Div4">
             
                    <table style="width:100%;">
                    <tr>
                        <td style="width:50px;">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>                            
                            <!--i class="fa fa-exclamation-triangle" aria-hidden="true"></i-->
                            <!--i class="fa fa-exclamation-triangle" aria-hidden="true"></i-->
                            <!--i class="fa fa-times" aria-hidden="true"></i-->
                        </td>
                        <td>
                            <label style="font-size:1.6em!important;" id="modalAcysMensaje_Texto"></label>
                        </td>
                    </tr>
                    </table>

                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">

                        <table>
                            <tr>                                
                                <td>
                                    <button class="btn btn-default" data-dismiss="modal" id="Button5">Ok</button>            
                                </td>
                                <!--td>
                                    <button class="btn btn-primary" id="modalBucarCliente_Aplicar">Aplicar</button>
                                </td-->
                            </tr>
                        </table>

                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

<!--Modal -->
<div id="modalImprimir" data-backdrop="static" data-keyboard="false fade" class="modal" role="dialog" tabindex="-1" style="z-index:1220!important;" style="display:none; width:100%;" >
    <div class="modal-dialog vertical-center" role="document" style="width:100%;" >
        <div class="modal-content" style="width:100%;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">                    
                    <span aria-hidden="true">&times;</span>
                </button>                
                <table style="width:100%;">
                <tr>
                    <td><h4 id="h4">Visor de Reporte</h4></td>
                    <td></td>
                    <td>                        
                        <button id="Button1" class="btn btn-success btn-xs blink_text" style="display:none;" >Cargando...</button>                        
                    </td>                    
                </tr>
                </table>
                </div>
                <div class="modal-body" id="Div5">

                   <iframe id="iframeVisorReporte" src="" width="100%" height="500"></iframe>

                </div>
                <div class="modal-footer">                
                
                <div class="row">
                    <div class="col-md-12 ">

                        <table>
                            <tr>                                
                                <td>
                                    <button class="btn btn-default" id="Button4">Cancelar</button>            
                                </td>
                                <!--td>
                                    <button class="btn btn-primary" id="modalBucarCliente_Aplicar">Aplicar</button>
                                </td-->
                            </tr>
                        </table>

                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
<div id="modalOrdenCliente" class="modal fade" tabindex="1" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog" role="document" style="width:800px">
        <div class="modal-content">
            <div class="modal-header">
                    
                <table style="width:100%;">
                <tr>
                    <td>
                        <h4 id="lbOrdenClienteTitulo">Control de Ordenes / Folio: 0</h4>
                    </td>
                    <td>                        
                        <img id="Img1" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>">
                    </td>
                    <td style="width:30px;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="img2" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </td>
                </tr>
                </table>

            </div>
            <div class="modal-body" id="dvCuerpoVentanaDimension">
    
                <input type="hidden" id="hfCO_Id_Acs" value="0" />
                <input type="hidden" id="hfCO_Acs_Version" value="0" />
                <input type="hidden" id="hfCO_Id_Cte" value="0" />
                <input type="hidden" id="hfCO_Id_Ter" value="0" />
                <input type="hidden" id="hfCO_Vencido" value="0" />
                                
                <ul class="nav nav-tabs role="tablist" id="Ul1">
                    <li class="active"><a href="#tab_Cliente" data-toggle="tab">Cliente</a></li>
                    <li><a href="#tabRecepcionPedidos" data-toggle="tab">Recepción de pedidos</a></li>            
                    <li><a href="#tabCondicionesPago" data-toggle="tab">Condiciones de pago</a></li>            
                    <li><a href="#tabServicioValor" data-toggle="tab">Servicio de valor (RSC/Asesor)</a></li>            
                </ul>
                    <div class="tab-content">
                        
                        <%-- TAB :: Cliente /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <div class="tab-pane active" id="tab_Cliente">                    
                        <div class="col-md-12">
                                                  
                            <div class="row mt5">
                                   <div class="panel panel-default">                                      
                                   <div class="panel-heading titulo_blod">Información de Cliente</div>
                                      <div class="panel-body">
                                        <table style="width:100%">
                                        <tr>
                                            <td class="lb_z1" style="width:90px">Número</td>
                                            <td style="width:100px">                                                    
                                                <label id="tbId_Cte"></label>
                                            </td>
                                            <td class="lb_z1">Nombre</td>
                                            <td>
                                                <label id="tbCte_Nombre"></label>
                                            </td>
                                        </tr>
                                        </table>
                                    
                                        <table cellpadding="1">
                                        <tr>
                                            <!--td><button type="button" class="btn btn-primary btn-sm">Especificaciones adicionales</button></td-->
                                            <td><button type="button" data-id_acs="" data-acs_version="" class="btn btn-primary btn-xs" id="btnConsultarAcys" >Consultar ACyS</button></td>
                                            <td><button type="button" class="btn btn-primary btn-xs" id="btnControlCambios" style="display:none;">Control Cambios</button></td>
                                        </tr>
                                        </table>                                            
                                       </div>
                                   </div>
                            </div>
                            
                            <div class="row mt5">                                                                
                            <div class="panel panel-default">
                            <div class="panel-heading titulo_blod">Especificaciones adicionales</div>
                            <div class="panel-body">
                                <table style="width:100%">
                                <tr>
                                    <td style="width:30%">CDI</td>
                                    <td style="width:30%">
                                        <!--input type="text" class="form-control" /-->
                                        <label id="tbCDI"></label>
                                    </td>
                                    <td style="width:30%"></td>
                                </tr>
                                <tr>
                                    <td>Cuenta corporativa</td>
                                    <td>                                                                                
                                        <select disabled="disabled" id="chbEspAd_CuentaCorporativa">
                                            <option value="0">No</option>
                                            <option value="1">Si</option>
                                        </select>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Nombre comercial</td>
                                    <td colspan="2">
                                        <!--input type="text" id="tbNombreComercial" class="form-control" /-->
                                        <label id="tbNombreComercial"></label>
                                    </td>                                    
                                </tr>
                                  <tr>
                                    <td>Tipo de Cuenta</td>
                                    <td colspan="6">
                                        <!--input type="text" id="tbNombreComercial" class="form-control" /-->
                                        <select disabled="disabled" id="chbTipoCuenta">
                                            <option value="-1">Seleccione</option>
                                            <option value="0">Local</option>
                                            <option value="1">Nacional</option>
                                           <option value="2">Coordinada</option>
                                        </select>
                                    </td>                                    
                                </tr>
                                <tr>
                                     <td>Coordinador Cuenta</td>
                                    <td>
                                        <label id="lblCordinadorCuentaControlOrdenes"></label>
                                    </td>
                                </tr>
                                    <tr>
                                     <td>Correo</td>
                                    <td>
                                        <label id="lblCordinadorCuentaCorreoControlOrdenes"></label>
                                    </td>
                                </tr>
                                </table>
                                        
                            </div>
                            </div>
                            </div>
                        </div>                          
                        </div>        
                        
                        <%--
                            /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                            tabRecepcionPedidos 
                            TAB :: RECEPCION DE PEDIDIOS
                            /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                         --%>
                                       
                        <div class="tab-pane" id="tabRecepcionPedidos">                    
                            <div class="panel panel-default mt5">
                                <div class="panel-heading titulo_blod">Modalidad de Pedido</div>
                                <div class="panel-body">
                                    <table style="width:100%;">
                                    <tr>
                                        <td style="width:33%;">                                        
                                            <table>
                                            <tr>
                                                <td><input type="checkbox" id="chbAcs_Modalidad" class="form-control chb"/></td>
                                                <td class="lb_z1">Frecuencia Establecida</td>
                                            </tr>
                                            </table>                                                
                                        </td>
                                        <td style="width:33%;">
                                            <table>
                                            <tr>
                                                <td><input type="checkbox" id="chbOrdenAbiertaConRep" class="form-control chb"/></td>
                                                <td class="lb_z1">Orden Abierta con Reposición</td>
                                            </tr>
                                            </table>                                                
                                        </td>
                                        <td style="width:33%;">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table style="width:100%; border: 2px solid #f3f3f3; padding:5px; display:block;" >
                                            <tr>
                                                <td>
                                                    <table style="width:280px;">
                                                    <tr>
                                                    <td>¿Aceptar parcialidades?</td>
                                                    <td style="width:150px;">
                                                        <select id="selAcs_ParcialidadesSi" class="form-control" style="width:100%" >
                                                            <option value="-1">-- Seleccione --</option>
                                                            <option value="0">No</option>
                                                            <option value="1">Si</option>
                                                        </select>
                                                    </td>
                                                    </tr>
                                                    </table>
                                               </td>
                                               <td>
                                                   <table id="tblAceptaParcialidades" style="display:none;">
                                                    <tr>
                                                        <td>Parcialidad se entrega:</td>
                                                        <td><input type="checkbox" id="cbParcialidad_Entrega_Rem" class="form-control chb" /></td>
                                                        <td>Remisionada</td>
                                                        <td><input type="checkbox" id="cbParcialidad_Entrega_Fac" class="form-control chb" /></td>
                                                        <td>Facturada</td>
                                                    </tr>
                                                    </table>
                                               </td>
                                            </tr>
                                            </table>
                                        </td>
                                        
                                    </tr>
                                    </table>                                    
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading titulo_blod">Forma de envío de pedido<span class="required">*</span></div>
                                <div class="panel-body">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:150px; vertical-align:top;">
                                                    <table style="width: 100%; border-spacing: 5px; border-collapse: separate;">
                                                        <tr>
                                                            <td><input type="checkbox" id="chbRecCorreo" class="form-control chb" /></td>
                                                            <td class="lb_z1">Email</td>
                                                        </tr><tr>
                                                            <td><input type="checkbox" id="chbRecTelefono" class="form-control chb" /></td>
                                                            <td class="lb_z1">Teléfono</td>
                                                        </tr><tr>
                                                            <td><input type="checkbox" id="chb_RecWhatsApp" class="form-control chb" /></td>
                                                            <td class="lb_z1">Whatsapp</td>
                                                        </tr><tr>
                                                            <td><input type="checkbox" id="chbRecRIK" class="form-control chb" /></td>
                                                            <td class="lb_z1">Recolectado por RIK/RSC</td>
                                                        </tr><tr>
                                                            <td><input type="checkbox" id="chbRecConfirmacion" class="form-control chb" /></td>
                                                            <td class="lb_z1">Requiere confirmación de pedido</td>
                                                        </tr><tr>
                                                            <td><input type="checkbox" id="chbRecOtro" class="form-control chb" /></td>
                                                            <td class="lb_z1">Otro</td>
                                                        </tr><tr>                                                            
                                                            <td colspan="2">                               
                                                            <input type="text" class="form-control" id="tbAcs_RecOtroDesc" class="display:none;" />
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <div class="panel panel-default" style="width:100%;">                                                        
                                                    <div class="panel-body">
                                                        <table style="width: 100%; border-spacing: 5px; border-collapse: separate;">
                                                        <tr>
                                                            <td style="width:100px;">Encargado de enviar pedido</td>
                                                            <td><input type="text" id="tbAcs_PedidoEncargadoEnviar" class="form-control" style="width:100%;"/></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Puesto</td>
                                                            <td><input type="text" id="tbAcs_PedidoPuesto" class="form-control" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Teléfono(s)</td>
                                                            <td><input type="text" id="tbAcs_PedidoTelefono" class="form-control" style="width:200px;"/></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td><input type="text" id="tbAcs_PedidoTelefono2" class="form-control" style="width:200px;"/></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Email</td>
                                                            <td><input type="text" id="tbAcs_PedidoEmail" class="form-control" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td><input type="text" id="tbAcs_PedidoEmail2" class="form-control" /></td>
                                                        </tr>
                                                        </table>
                                                    </div>
                                                    </div>
                                                    
                                                </td>
                                            </tr>                                            
                                        </table>
                                          
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading titulo_blod">Documentos para entrega y recepción<span class="required">*</span></div>
                                <div class="panel-body">
                                    <table style="width: 100%; border-spacing: 5px; border-collapse: separate;">
                                        <tr>
                                            <td style="width:150px;" rowspan="2">
                                                <label></label>Días de recepción</label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="width:150px;">
                                                <table style="width:100%" >
                                                    <tr>
                                                        <td>L</td>
                                                        <td>M</td>
                                                        <td>M</td>
                                                        <td>J</td>
                                                        <td>V</td>
                                                        <td>S</td>
                                                        <td>D</td>
                                                        <td>Cualquier día</td>
                                                    </tr>
                                                    <tr>                                                        
                                                        <td><input type="checkbox" id="chbAcs_RecRevLunes" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevMartes" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevMiercoles" class="form-control chb"  /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevJueves" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevViernes" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevSabado" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevDomingo" class="form-control chb" /></td>
                                                        <td><input type="checkbox" id="chbAcs_RecRevCualquierDia" class="form-control chb" /></td>
                                                    </tr>
                                                </table>
                                            </td>                                                                                        
                                        </tr>
                                        <tr>
                                            <td style="width:150px;">Horarios de entrega</td>
                                            <td><input type="text" class="form-control" id="tbAcs_TimePicker1" value="" /></td>
                                            <td><input type="text" class="form-control" id="tbAcs_TimePicker2" value="" /></td>
                                            <td></td>
                                            <td><input type="text" class="form-control" id="tbAcs_TimePicker3" value="" /></td>
                                            <td><input type="text" class="form-control" id="tbAcs_TimePicker4" value="" /></td>                                            
                                         </tr>
                                         <tr>
                                            <td>Persona que recibe</td>
                                            <td colspan="5">                                                                                             
                                                <input type="text" class="form-control" id="tbAcs_RecPersonaRecibe" value="" />
                                            </td>                                                                                        
                                         </tr>
                                    </table>
                                    <table style="width:100%;">                                        
                                        <tr>
                                            <td style="width:100px;">Cita para entrega</td>
                                            <td style="width:20px;"><input type="checkbox" id="chbAcs_RecCitaSinCita" class="form-control chb"  /></td>
                                            <td style="width:100px;">Sin cita</td>
                                            <td style="width:20px;"><input type="checkbox" id="chbAcs_RecCitaMismoDia" class="form-control chb" /></td>                                                                                        
                                            <td style="width:100px;">Mismo día</td>
                                            <td style="width:20px;"><input type="checkbox" id="chbAcs_RecCitaPrevia" class="form-control chb" value="" /></td>
                                            <td style="width:100px;">Previa</td>
                                         </tr>
                                         <tr>
                                            <td>Área de recibo</td>
                                            <td><input id="chbAcs_RecAreaPropia" type="checkbox" class="form-control chb" value="" /></td>                  
                                            <td>Propia</td>
                                            <td><input id="chbAcs_RecAreaPlaza" type="checkbox" class="form-control chb" value="" /></td>                  
                                            <td>Plaza</td>
                                            <td><input id="chbAcs_RecAreaCalle" type="checkbox" class="form-control chb" value="" /></td>                  
                                            <td>Calle</td>                                                                      
                                         </tr>
                                         <tr>
                                            <td>Estacionamiento</td>
                                            <td><input type="checkbox" id="chkAcs_RecEstCortesia"  class="form-control chb" value="" /></td>                  
                                            <td>Cortesia</td>
                                            <td><input type="checkbox" id="chkAcs_RecEstCosto" class="form-control chb" value="" /></td>                  
                                            <td>Costo</td>
                                            <td>Monto</td>                                                                       
                                            <td><input type="text" id="tbAcs_RecEstMonto" class="form-control" value="" /></td>                  
                                         </tr>                                                                                    
                                    </table>
                                    <table>
                                    <tr>                                                                                                   
                                        <td style="display:none;">
                                            <input type="checkbox" class="form-control chb" id="chbAcs_EspecsAdic1" value="" />
                                        </td>
                                        <td style="display:none;">                                            
                                            <i class="fa fa-plus-circle fa-lg"></i>
                                        </td>
                                        <td style="display:block;"> 
                                            <button id="btnAcs_EspecsAdic1" class="btn btn-default">
                                                <i id="faAcs_EspecsAdic1" class="fa fa-plus-circle fa-lg"></i>&nbsp;
                                                Especifiaciones adicionales
                                            </button>                                            
                                        </td>                                                                           
                                    </tr>
                                    </table>
                                    <div id="divAcs_EspecsAdic1" style="display:none;">
                                        <div class="panel panel-default" style="margin-top:5px;">
                                        <%--<div class="panel-heading titulo_blod">Especifiaciones adicionales</div>--%>
                                        <div class="panel-body">
                                        <%--//
                                        // Especificaciones 
                                        // Adicionales  (1)
                                        //--%>
                                
                                        <table style="border-spacing: 5px; border-collapse: separate;">
                                        <tr>
                                            <td align="center" style="width:100px;">Documento</td>
                                            <td align="center" style="width:50px;">Entrega</td>
                                            <td align="center" style="width:50px;">No. Copias</td>
                                            <td align="center" style="width:50px;">Recepción</td>
                                            <td align="center" style="width:50px;">No. Copias</td>                                            
                                        </tr>
                                        <tr>
                                            <td>Factura KEY</td>             
                                            <td align="center"><input id="chkAcs_RecDocFactKeyEnt" type="checkbox" class="chb" /></td>
                                            <td><input id="tbAcs_RecDocFactKeyEntCop" type="text" class="form-control" /></td>
                                            <td align="center"><input id="chkAcs_RecDocFactKeyRec" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocFactKeyRecCop" type="text" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td>Orden de compra/release</td>                                                                          
                                            <td align="center"><input id="chkAcs_RecDocOrdCompraEnt" type="checkbox" class="chb" /></td>                                                           
                                            <td><input id="tbAcs_RecDocOrdCompraEntCop" type="text" class="form-control" /></td>
                                            <td align="center"><input id="chkAcs_RecDocOrdCompraRec" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocOrdCompraRecCop" type="text" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td>Orden de reposición</td>
                                            <td align="center"><input id="chkAcs_RecDocOrdReposEnt" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocOrdReposEntCop" type="text" class="form-control" /></td>
                                            <td align="center"><input id="chkAcs_RecDocOrdReposRec" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocOrdReposRecCop" type="text" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td>Copia de pedido</td>                                                                          
                                            <td align="center"><input id="chkAcs_RecDocCopPedidoEnt" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocCopPedidoEntCop" type="text" class="form-control" /></td>
                                            <td align="center"><input id="chkAcs_RecDocCopPedidoRec" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocCopPedidoRecCop" type="text" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td>Remisión</td>
                                            <td align="center"><input id="chkAcs_RecDocRemisionEnt" type="checkbox" class="chb" /></td>
                                            <td><input id="tbAcs_RecDocRemisionEntCop" type="text" class="form-control" /></td>
                                            <td align="center"><input id="chkAcs_RecDocRemisionRec" type="checkbox" class="chb"/></td>
                                            <td><input id="tbAcs_RecDocRemisionRecCop" type="text" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td data-rnd="45687614256">Certificado de calidad</td>
                                            <td align="center">
                                                <input id="chkAcs_RecDocCertificadoEnt" type="checkbox" class="chb" />
                                            </td>
                                            <td>
                                                <input id="tbAcs_RecDocCertificadoEntCop" type="text" class="form-control" />
                                            </td>
                                            <td align="center">
                                                <input id="chkAcs_RecDocCertificadoRec" type="checkbox" class="chb"/>
                                            </td>
                                            <td>
                                                <input id="tbAcs_RecDocCertificadoRecCop" type="text" class="form-control" />
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                            
                        </div>     
                        
                        <%--tabCondicionesPago 
                            CONDICIONES DE PAGO 
                            /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ 
                        --%>
                                          
                        <div class="tab-pane" id="tabCondicionesPago">                    
                            <div class="panel panel-default">
                            <div class="panel-heading titulo_blod">Documentos para entrega y recepción</div>
                            <div class="panel-body">
                                <table>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="chkContado"  class="form-control chb" value="" />                                            
                                        </td>
                                        <td>Contado</td>                                        
                                        <td>
                                            <input type="checkbox" id="chkCredito"  class="form-control chb" value="" />
                                        </td>
                                        <td>Crédito</td>                                        
                                        <td>Días</td>                                                                                
                                        <td>
                                            <input type="text" id="txtDias" class="form-control" value="" />                                            
                                        </td>
                                        <td>Naturales</td>                                                                                
                                    </tr>                                     
                                </table>
                                <table>
                                    <tr>
                                        <td>Límite de crédito</td>
                                        <td>Banco nos deposita al cliente</td>
                                        <td>Número de cuenta</td>                                        
                                        <td>Referencia tecleada</td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" id="txtLimite" class="form-control" value="" />                                            
                                        </td>
                                        <td>                                                                           
                                            <select id="cmbCPBancoDeposita" class="form-control" >
                                                <option value="0">-- Seleccione --</option>                                                
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" id="txtCPNumCuenta" class="form-control" value="" />                                            
                                        </td>
                                        <td>                                  
                                            <input type="text" id="txtCPRefTecleada" class="form-control" value="" />                                            
                                        </td>
                                    </tr>
                                </table>
                                <!-- 
                                    ESTA PARTE NO 
                                    ESTA PARTE NO 
                                    ESTA PARTE NO 
                                -->
                                <table style="display:none;">
                                <tr>    
                                    <td>Método/forma de pago</td>
                                    <td>                                        
                                        <select id="cbAcs_DocEntregaFormaPago" class="form-control" >
                                            <option value="1">Efectivo</option>
                                            <option value="2">Cheque nominativo</option>
                                            <option value="3">Transferencia electrónica de fondos</option>
                                            <option value="4">Tarjeta de debito</option>
                                            <option value="5">Monedero electronico</option>
                                            <option value="6">Dinero electrónico</option>
                                            <option value="7">Vales de despensa</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>    
                                    <td>Adenda</td>                                    
                                    <td>                                            
                                        <select id="cbAdena">
                                            <option value="0">No</option>
                                            <option value="1">Si</option>
                                        </select>
                                    </td>                                    
                                </tr>
                                <tr>    
                                    <td>Uso de CFDI</td>
                                    <td>
                                        <select id="cbCte_PagoUsoCFDI">
                                        <option value="0">No</option>
                                        <option value="1">Si</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>                                                        
                                    <td style="display:none;">
                                        <input type="checkbox" class="form-control chb" id="chbDER_EspAdic2" value="" />
                                    </td>                                    
                                    <td style="display:none;">                                             
                                        <i class="fa fa-angle-double-down fa-lg"></i>
                                    </td>
                                     <td style="display:block;">                                             
                                     </td>                                                       
                                </tr>
                                </table>
                                    
                            </div>
                            </div>
                            <!-- Formas de pago -->
                            <!-- Formas de pago -->
                            <!-- Formas de pago -->
                            
                            <div class="panel panel-default">
                            <div class="panel-heading titulo_blod">Formas de pago</div>
                            <div class="panel-body">
                                <div id="DivLstFormasPago" style="width:100%; height:auto; float:left;">
                                </div>
                              <table style="width:100%; display:none;">
                                    <tr>                                        
                                        <td style="vertical-align: text-bottom;">
                                            <input type="checkbox" id="chkEfectivo"  class="form-control chb" value="" />
                                        </td>
                                        <td>Efectivo</td>
                                        <td style="vertical-align: text-bottom;">
                                            <input type="checkbox" id="chkFactoraje"  class="form-control chb" value="" />
                                        </td>
                                        <td>Transferencia electrónica de fondos</td>
                                        <td style="vertical-align: text-bottom;">
                                            <input type="checkbox" id="chkTransferencia"  class="form-control chb" value="" />
                                        </td>
                                        <td>Monedero electrónico</td>
                                        <td>
                                            <input type="checkbox" id="chkCheque"  class="form-control chb" value="" />
                                        </td>
                                        <td>Vales de despensa</td>                                    
                                    </tr><tr>                                                                                
                                        <td>
                                            <input type="checkbox" id="chkDeposito"  class="form-control chb" value="" />
                                        </td>
                                        <td>Cheque nominativo</td>                                                                                
                                        <td>
                                            <input type="checkbox" id="chkTarjetaDebito"  class="form-control chb" value="" />
                                        </td>
                                        <td>Tarjeta de debito</td>                                                                                
                                        <td>
                                            <input type="checkbox" id="chkTarjetaCredito"  class="form-control chb" value="" />
                                        </td>
                                        <td>Dinero electrónico</td>                                        
                                    </tr>                                    
                                </table>
                            <table style="width:100%;">
                                <tr>
                                <td style="vertical-align: text-bottom; width:200px;">
                                  
                                    <table style="width:100%;">
                                        <tr>
                                            <td>Unico día</td>         
                                            <td><input type="text" id="txtCPUnicoDia" class="form-control" value="" /></td>
                                        </tr>
                                        <tr>
                                            <td>Día máximo</td>
                                            <td><input type="text" id="txtCPDiaMaximo" class="form-control" value="" /></td>
                                        </tr>
                                        <tr>
                                            <td>Personal de cuentas por pagar</td>
                                            <td><input type="text" id="txtCPCuentasPagar" class="form-control" value="" /></td>
                                        </tr>
                                        <tr>
                                            <td>Ultimos 4 dig. cuenta de pago</td>
                                            <td><input type="text" id="txtCPCuentaPago" class="form-control" value="" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: text-bottom;">
                                    
                                    <table style="width:500px;" data-rnd="764654654">                                        
                                    <tr>
                                        <td>Dirección</td>
                                        <td>
                                            <input type="text" id="txtCPDiasPagoDireccion" class="form-control" value="" disabled/>
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td>Colonia</td>
                                        <td>
                                            <input type="text" id="txtCPColonia" class="form-control" value="" disabled/>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>Municipio</td>
                                        <td>                                            
                                            <input type="text" id="txtCPMunicipio" class="form-control" value="" disabled/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Estado</td>                                        
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width:150px;">
                                                        <input type="text" id="txtCPEstado" class="form-control" value="" disabled/>
                                                    </td>                                        
                                                    <td>CP</td>
                                                    <td>
                                                    <input type="text" id="txtCPCP" class="form-control" value="" style="width:70px;" disabled/>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                    <tr>                                                                            
                                        <td>Ciudad</td>
                                        <td>                                            
                                            <input type="text" id="txtCPCiudad" class="form-control" value="" disabled/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Teléfonos</td>
                                        <td>
                                            <input type="text" id="txtCPTelefonos" class="form-control" value="" disabled/>
                                        </td>
                                    </tr>
                                   </table>
                                </td>
    
                                </tr>                                
                            </table>
                            </div>
                            </div>
                            <div class="panel panel-default">
                            <div class="panel-heading titulo_blod">Revisión de facturas</div>
                            <div class="panel-body">
                                                                                  
                                <table style="width:100%;">
                                <tr>
                                        <td style="width:150px;" rowspan="2">Días de revisión</td>                                            
                                </tr>
                                <tr>
                                        <td colspan="6" style="width:150px;">
                                            <table style="width:100%" >
                                                <tr>
                                                    <td>L</td>
                                                    <td>M</td>
                                                    <td>M</td>
                                                    <td>J</td>
                                                    <td>V</td>
                                                    <td>S</td>
                                                    <td>D</td>
                                                    <td>Cualquier día</td>
                                                </tr>
                                                <tr>                                                        
                                                    <td><input type="checkbox" id="chkRevisionLunes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionMartes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionMiercoles" class="form-control chb"  /></td>
                                                    <td><input type="checkbox" id="chkRevisionJueves" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionViernes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionSabado" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionDomingo" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkRevisionCualquierDia" class="form-control chb" /></td>
                                                </tr>
                                            </table>
                                        </td>                                                                                        
                                </tr>
                                <tr>
                                    <td style="vertical-align: text-top;" style="width:150px;">Horarios de revisión</td>
                                    <td>
                                        <table>
                                            <tr>                                                    
                                                <td><input type="text" class="form-control" id="tpRevisionMañanaInicio" value="" /></td>
                                                <td>a</td>
                                                <td><input type="text" class="form-control" id="tpRevisionMañanaFin" value="" /></td>
                                                <td>y</td>
                                                <td><input type="text" class="form-control" id="tpRevisionTardeInicio" value="" /></td>
                                                <td>a</td>
                                                <td><input type="text" class="form-control" id="tpRevisionTardeFin" value="" /></td>                                                                                                
                                            </tr>
                                        </table>                                            
                                        </td>                                        
                                    </tr>                                    
                                    <tr>
                                        <td style="vertical-align: text-top;" >Documentos<br>adicionales</td>
                                        <td colspan="5">                                                                                             
                                            <%--TODO: Verificar 1--%>
                                            <table style="width:100%;">
                                                <tr>                                        
                                                    <td>
                                                        <input type="checkbox" id="chkRevFolio"  class="form-control chb" value="" />
                                                    </td>
                                                    <td>Folio</td>
                                                    <td>
                                                        <input type="checkbox" id="chkRevRntrada"  class="form-control chb" value="" />
                                                    </td>
                                                    <td>Entrada de almacén</td>
                                                    <td>
                                                        <input type="checkbox" id="chkRevOrden"  class="form-control chb" value="" />
                                                    </td>
                                                    <td>Orden de compra</td>
                                                </tr><tr>                                                                                
                                                    <td>
                                                        <input type="checkbox" id="chkRevReporte"  class="form-control chb" value="" />
                                                    </td>
                                                    <td>Reporte de consumo</td>                                    
                                                    <td>
                                                        <input type="checkbox" id="chkRevCopia"  class="form-control chb" value="" />
                                                    </td>
                                                    <td>Copia de factura</td>                                        
                                                </tr>                                    
                                            </table>
                                        </td>                                                                                        
                                    </tr>
                                    <tr>
                                        <td>Otro</td>
                                        <td colspan="5">
                                            <input type="text" id="tbRevFac_Otro"  class="form-control" value="" />
                                        </td>                                                                                
                                    </tr>
                                    
                                </table>
                                
                            </div>
                            </div>
                            <div class="panel panel-default">
                            <div class="panel-heading titulo_blod">Pago de facturas</div>
                            <div class="panel-body">
                                <table style="width:100%; border-spacing: 5px; border-collapse: separate;">
                                    <tr>
                                        <td style="width:200px;">Correo para recibir factura:</td>
                                        <td>
                                            <input type="text" id="tbAcs_CorreoRecibirFacturas" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Correo para recibir complemento:</td>
                                        <td>
                                            <input type="text" id="tbAcs_CorreoRecibirComplemento" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td colspan="2">
                                            <table style="border-spacing: 5px; border-collapse: separate;">
                                                <tr>
                                                    <td>N/A:</td>
                                                    <td><input type="checkbox" id="chbAcs_CorreoRecibir_NA"  class="form-control chb" value="" /></td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                </table>
                                <hr>
                                                            
                                <table style="width:100%; border-spacing: 5px; border-collapse: separate;">
                                    <tr>
                                        <td style="width:150px;"><strong>Días de pago</strong></td>                                            
                                        <td>
                                            <table style="width:100%">
                                                <tr>
                                                    <td>L</td>
                                                    <td>M</td>
                                                    <td>M</td>
                                                    <td>J</td>
                                                    <td>V</td>
                                                    <td>S</td>
                                                    <td>D</td>
                                                    <td>Cualquier dia</td>
                                                </tr>
                                                <tr>   
                                                    <td><input type="checkbox" id="chkPagoLunes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkPagoMartes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkPagoMiercoles" class="form-control chb"  /></td>
                                                    <td><input type="checkbox" id="chkPagoJueves" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkPagoViernes" class="form-control chb" /></td>
                                                    <td><input type="checkbox" id="chkPagoSabado" class="form-control chb" /></td>                                                                                                         
                                                    <td><input type="checkbox" id="chkPagoDomingo" class="form-control chb" /></td>                                                                                                         
                                                    <td><input type="checkbox" id="chkPagoCualquierDia" class="form-control chb" /></td>
                                                </tr>
                                            </table>
                                        </td>                                            
                                    </tr>
                                        
                                    <tr>
                                        <td style="width:150px;">Horarios de pago</td>
                                        <td>
                                            <table style="width:100%">                                                
                                                <td><input type="text" class="form-control" id="tpPagoMañanaInicio" value="" /></td>
                                                <td>a</td>
                                                <td><input type="text" class="form-control" id="tpPagoMañanaFin" value="" /></td>
                                                <td>y</td>
                                                <td><input type="text" class="form-control" id="tpPagoTardeInicio" value="" /></td>                                                
                                                <td>a</td>
                                                <td><input type="text" class="form-control" id="tpPagoTardeFin" value="" /></td>  
                                            </table>
                                        </td>                                        
                                    </tr>
                                </table>
                                
                                <hr>
                                <label>Presencial</label>
                                 <table>                                        
                                    <tr>
                                        <td style="width:30px;">                                                                       
                                            <input type="checkbox" id="chkRevFacContraEntrega"  class="form-control chb" value="" />
                                        </td>
                                        <td>Contra entega</td>
                                        <td style="width:30px;">                                                                       
                                            <input type="checkbox" id="chkRevFacVisGestorCobranza"  class="form-control chb" value="" />
                                        </td>
                                        <td>Visita del gestor de cobranza</td>
                                    </tr>
                                  </table>
                                   <table style="width:500px;">                                        
                                    <tr>
                                        <td>Dirección</td>
                                        <td>
                                            <input type="text" id="txtRevFacDireccion" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Colonia </td>
                                        <td>
                                            <input type="text" id="txtRevFacColonia" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Municipio</td>
                                        <td>                                            
                                            <input type="text" id="txtRevFacMunicipio" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Estado</td>                                        
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width:100px;">
                                                        <input type="text" id="txtRevFacEstado" class="form-control" value="" />
                                                    </td>
                                                    <td style="width:100px;">CP</td>
                                                    <td style="width:100px;">
                                                        <input type="text" id="txtRevFacCP" class="form-control" value="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ciudad</td>
                                        <td>                                            
                                            <input type="text" id="txtRevFacCiudad" class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Teléfonos</td>
                                        <td>
                                            <input type="text" id="txtRevFacTeléfonos" class="form-control" value="" />
                                        </td>
                                    </tr>
                                   </table>
                                   <hr>
                                   <label>Eléctronica</label>
                                   <table style="width:100%;">                                        
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="chkRevFacEmail"  class="form-control chb" value="" />
                                        </td>
                                        <td>Email</td>                                        
                                        <td>
                                            <input type="text" id="txtRevFacEmailTexto" class="form-control" value="" />
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>                                            
                                        </td>
                                        <td>Email 2</td>                                        
                                        <td>
                                            <input type="text" id="txtRevFacEmailTexto2" class="form-control" value="" />
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="chkRevFacPortal"  class="form-control chb" value="" />
                                        </td>
                                        <td>Portal</td>     
                                        <td>
                                            <input type="text" id="txtRevFacPortalTexto"  class="form-control" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Http://</td>     
                                        <td>
                                            <input type="text" id="txtRevFacHttp"  class="form-control" value="" />
                                        </td>
                                    </tr><tr>
                                        <td></td>
                                        <td>Usuario:</td>  
                                        <td>
                                            <input type="text" id="txtRevFacUsuario"  class="form-control" value="" style="width:150px;"  />
                                        </td>
                                    </tr><tr>
                                        <td></td>
                                        <td>Contraseña:</td>
                                        <td>
                                            <input type="text" id="txtRevFacContrasenia"  class="form-control" value="" style="width:150px;" />
                                        </td>                                                    
                                     </tr>
                                     </table>
                                <table style="display:none;">
                                    <tr>
                                        <td>
                                            <button id="btnDER_EspecsAdic2" class="btn btn-default">
                                                <i id="faDER_EspecsAdic2" class="fa fa-plus-circle fa-lg"></i>&nbsp;
                                                Especifiaciones adicionales
                                            </button>                                                                                        
                                        </td>
                                    </tr>
                                </table>
                                <div id="divDER_EspecsAdic2" style="display:none;">
                                    <div class="panel panel-default" style="margin-top:5px;">
                                    <!--div class="panel-heading titulo_blod">Especifiaciones adicionales</div-->
                                    <div class="panel-body">
                                    <%--//
                                    // Especificaciones Adicionales  (2)
                                    //--%>
                                                                    
                                    <table style="border-spacing: 5px; border-collapse: separate;">
                                    <tr>
                                        <td align="center">Documento</td>
                                        <td align="center">Entrega</td>
                                        <td align="center" style="width:100px;">No. Copias</td>
                                        <td align="center">Recepción</td>
                                        <td align="center" style="width:100px;">No. Copias</td>
                                    </tr>
                                    <tr>        
                                        <td>Factura KEY</td>
                                        <td align="center"><input id="chbACS_chk62DocFactKeyEnt" type="checkbox" class="chb" /></td>                                        
                                        <td><input id="tbACS_txt62DocFactKeyEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocFactKeyRec" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocFactKeyRecCop" type="text" class="form-control" /></td>
                                    </tr>
                                    <tr>
                                        <td>Orden de compra/release</td>
                                        <td align="center"><input id="chbACS_chk62DocOrdCompraEnt" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocOrdCompraEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocOrdCompraRec" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocOrdCompraRecCop" type="text" class="form-control" /></td>
                                    </tr>
                                    <tr>
                                        <td>Orden de reposición</td>
                                        <td align="center"><input id="chbACS_chk62DocOrdReposEnt" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocOrdReposEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocOrdReposRec" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocOrdReposRecCop" type="text" class="form-control" /></td>
                                    </tr>
                                    <tr>
                                        <td>Copia de pedido</td>
                                        <td align="center"><input id="chbACS_chk62DocCopPedidoEnt" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocCopPedidoEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocCopPedidoRec" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocCopPedidoRecCop" type="text" class="form-control" /></td>                                        
                                    </tr>
                                    <tr>
                                        <td>Remisión</td>
                                        <td align="center"><input id="chbACS_chk62DocRemisionEnt" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocRemisionEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocRemisionRec" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocRemisionRecCop" type="text " class="form-control" /></td>                                        
                                    </tr>
                                    <tr>
                                        <td>Certificado de calidad</td>
                                        <td align="center"><input id="chbACS_chk62DocCertificadoEnt" type="checkbox" class="chb" /></td>
                                        <td><input id="tbACS_txt62DocCertificadoEntCop" type="text" class="form-control" /></td>
                                        <td align="center"><input id="chbACS_chk62DocCertificadoRec" type="checkbox" class="chb"/></td>
                                        <td><input id="tbACS_txt62DocCertificadoRecCop" type="text" class="form-control" /></td>
                                    </tr>
                                    </table>
                                    
                                    </div>
                                    </div>
                                </div>
 
                            </div>
                            </div>
                            
                        </div>  
                        
                        <%-- /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                        <%--SERVICIOS DE VALOR --%>
                        <%-- /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                                             
                        <div class="tab-pane" id="tabServicioValor">                    
                            <%-- 6.1 Visita de Representante --%>
                            <div class="panel panel-default" style="margin-top:5px;">
                            <div class="panel-heading titulo_blod">Programación</div>
                            <div class="panel-body">
                                <table>
                                <tr>
                                    <td></td>
                                    <td>Frecuencia</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Visitas de representante</td>
                                    <td>                                        
                                        <select id="tbVis_Frecuencia" class="form-control" >
                                            <option value="0">-</option>                                            
                                        </select>
                                    </td>
                                    <td>*Especifica número de visitas</td>
                                </tr>
                                </table>
                            </div>
                            </div>                                
                            <%--DETALLE DE SERVICIO \/\ --%>
                            <%--DETALLE DE SERVICIO \/\ --%>
                            <%--DETALLE DE SERVICIO \/\ --%>
                            <div class="panel panel-default" style="margin-top:5px;">
                            <div class="panel-heading titulo_blod">Detalle de Servicios</div>
                            <div class="panel-body">
                                <p>¿Aplica?</p>
                                <!--table style="width:100%">
                                    <tr>
                                        <td><input type="checkbox" id="chb_ST" class="form-control" /></td>
                                        <td>Servicio Técnico</td>
                                    </tr>
                                    <tr>
                                        <td><input type="checkbox" id="chb_Cap" class="form-control" /></td>
                                        <td>Servicio Técnico</td>
                                    </tr>
                                    <tr>
                                        <td><input type="checkbox" id="Checkbox1" class="form-control" /></td>
                                        <td>Servicio Técnico</td>
                                    </tr>
                                </table-->
                                        
                                    <!--table style="width:100%">
                                    <tr>
                                        <td>¿Aplica?</td>
                                        <td></td>                                                                        
                                    </tr-->
                                    <%--
                                    /\/\/\/\/\/\/\/\/\/\/\/\/
                                    Servicio Técnico 
                                    Servicio Técnico 
                                    Servicio Técnico 
                                    /\/\/\/\/\/\/\/\/\/\/\/\/
                                     --%>
                                    <table style="width:100%">
                                    <tr>
                                        <td>
                                            <table class="tb_servicio_titulo">
                                            <tr>
                                                <td style="width:30px;">
                                                    <input type="checkbox" id="chb_ST_Aplicar" class="form-control" />
                                                </td>                                                
                                                <td><label>Servicio Técnico</label></td>                                                
                                                <td style="width:30px;"><i class="fa fa-wrench fa-lg"></i></td>	
                                            </tr>                                             
                                            </table>                                                                                        
                                        </td>                                        
                                    </tr>                                                                                                                                                
                                    <tr id="trReqServTec" style="display:none;">
                                        <td>                                            
                                            <div class="panel panel-default" style="margin-top:5px;">                                            
                                            <div class="panel-body">
                                                                                
                                                <table class="mt5">
                                                <tr>
                                                    <td style="width:150px;">Días de recepción</td>
                                                    <td>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>L</td>
                                                                <td>M</td>
                                                                <td>M</td>
                                                                <td>J</td>
                                                                <td>V</td>
                                                                <td>S</td>
                                                                <td>D</td>
                                                                <td>Cualquier día</td>
                                                            </tr>
                                                            <tr>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Lunes" value="" /></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Martes" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Miercoles" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Jueves" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Viernes" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Sabado" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_Domingo" value=""/></td>
                                                                <td><input type="checkbox" class="form-control chb" id="chb_ST_CualquierDia" value=""/></td>
                                                            </tr>
                                                        </table>                                                        
                                                    </td>                                            
                                                </tr>                                                
                                                <tr>
                                                    <td style="width:150px;">Horarios de Recepción</td>
                                                    <td>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td><input type="text" class="form-control" id="tb_ST_HorariosRecep1" value="" /></td>
                                                                <td>a</td>
                                                                <td><input type="text" class="form-control" id="tb_ST_HorariosRecep2" value="" /></td>
                                                                <td>y</td>
                                                                <td><input type="text" class="form-control" id="tb_ST_HorariosRecep3" value="" /></td>
                                                                <td>a</td>
                                                                <td><input type="text" class="form-control" id="tb_ST_HorariosRecep4" value="" /></td>                                                                
                                                            </tr>
                                                        </table>
                                                    </td>                                                    
                                                </tr>                                       
                                                </table>
                                                <table style="width:100%;" class="mt5">
                                                <tr>
                                                    <td style="width:150px;">Cita para Servicio</td>
                                                    <td style="width:20px;"><input type="checkbox" class="form-control chb" id="chb_ST_MismoDia" value="" /></td>
                                                    <td style="width:100px;">Mismo día</td>
                                                    <td style="width:20px;"><input type="checkbox" class="form-control chb" id="chb_ST_Previa" value="" /></td>
                                                    <td>Previa</td>                                            
                                                </tr>                                         
                                                </table>
                                                <table class="mt5">
                                                <tr>
                                                    <td><input type="checkbox" id="ChkServTecnicoRelleno" class="form-control chb" value=""/></td>
                                                    <td>Servicio de Relleno</td>                                                    
                                                    <td><input type="checkbox" id="ChkServMantenimiento" class="form-control chb" value=""/></td>                                                    
                                                    <td>Servicio Preventivo</td>
                                                </tr>
                                                </table>
                                                <%--OCT23-2019 RFH--%>
                                                <table style="width:100%;" class="mt5">
                                                <tr>
                                                    <td style="width:150px">¿Quién recibe?</td>
                                                    <td><input type="text" class="form-control" id="tbST_QuienRecibe" value="" /></td>                                                                                                       
                                                </tr>
                                                <tr>
                                                    <td>Función de la persona que recibe</td>
                                                    <td>
                                                        <input type="text" class="form-control" id="tbST_FuncionQuienRecibe" value="" />                                                        
                                                    </td>                                                    
                                                </tr>
                                                </table>
                                                <%--OCT24-2019--%>
                                                <table>                                                
                                                <tr>
                                                    <td>Frecuencia de visitas de representante</td>
                                                    <td>                                                        
                                                        <select id="ddlST_Frecuencia" class="form-control" >
                                                            <option value="0">-</option>                                            
                                                        </select>
                                                    </td>
                                                </tr>
                                                </table>
                                        
                                            </div>
                                            </div>                                            
                                    
                                        </td>                                    
                                        
                                    </tr>
                                    <%--
                                        /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                        2/4  SERVICIO CAPACITACION 
                                        CAPACITACION 
                                        CAPACITACION 
                                        CAPACITACION 
                                        /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                    --%>
                                    <tr>
                                        <td>                                            
                                            <table class="tb_servicio_titulo">
                                            <tr>                                                                               
                                                <td style="width:30px;">
                                                    <input type="checkbox" id="chb_ServCap_Aplicar" class="form-control" />
                                                </td>                                                
                                                <td><label>Capacitación</label></td>                                                
                                                <td style="width:30px;"><i class="fa fa-certificate fa-lg"></i></td>	
                                            </tr>                                            
                                            </table>                                            
                                        </td>
                                    </tr>                                            
                                    <tr id="trReqServCapacitacion" style="display:none;">                                    
                                        <td>
                                            <table>
                                            <tr>
                                                <td>Tipo de capacitación</td>
                                                <td>
                                                    <select class="form-control" id="sel_ServCap_Tipo1">
                                                        <option value="0">NA</option>                                                        
                                                    </select>
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;</td>
                                                <td>Subsegmento</td>
                                                <td>
                                                    <select class="form-control" id="sel_ServCap_Tipo2">
                                                        <option value="0">-</option>                                                        
                                                    </select>
                                                </td>
                                            </tr>
                                            </table>
                                            <div class="panel panel-default" style="margin-top:5px;">                                            
                                            <div class="panel-body">
                                    
                                            <table>                                        
                                            <tr>
                                                <td style="width:150px;">Días de recepción</td>                                            
                                                <td>
                                                    <table style="width:100%" >
                                                    <tr>
                                                        <td>L</td>
                                                        <td>M</td>
                                                        <td>M</td>
                                                        <td>J</td>
                                                        <td>V</td>
                                                        <td>S</td>
                                                        <td>D</td>
                                                        <td>Cualquier día</td>
                                                    </tr>
                                                    <tr>
                                                        <td><input type="checkbox" id="chb_ServCap_Lunes" class="form-control chb" value="" /></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Martes" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Miercoles" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Jueves" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Viernes" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Sabado" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_Domingo" class="form-control chb" value=""/></td>
                                                        <td><input type="checkbox" id="chb_ServCap_CualquierDia" class="form-control chb" value=""/></td>                                                        
                                                    </tr>
                                                    </table>
                                                </td>                                                                                        
                                            </tr>
                                            <tr>
                                                <td style="width:150px;">Horarios de Recepcion</td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                        <td><input type="text" id="tb_ServCap_HorariosRecep1" class="form-control" value="" /></td>
                                                        <td>a</td>
                                                        <td><input type="text" id="tb_ServCap_HorariosRecep2" class="form-control" value="" /></td>
                                                        <td>y</td>
                                                        <td><input type="text" id="tb_ServCap_HorariosRecep3" class="form-control" value="" /></td>                                                
                                                        <td>a</td>
                                                        <td><input type="text" id="tb_ServCap_HorariosRecep4" class="form-control" value="" /></td>                                            
                                                        </tr>
                                                    </table>
                                                </td>                                                    
                                            </tr>                                       
                                            </table>
                                            <table style="width:100%;">                                        
                                            <tr>
                                                <td style="width:150px;">Cita para capacitación</td>
                                                <td style="width:20px;"><input id="chb_ServCap_MismoDia" type="checkbox" class="form-control chb" value="" /></td>
                                                <td style="width:100px;">Mismo día</td>
                                                <td style="width:20px;"><input id="chb_ServCap_Previa" type="checkbox" class="form-control chb" value="" /></td>                                                                                        
                                                <td >Previa</td>                                            
                                            </tr>                                         
                                            </table>
                                                <%--OCT24-2019--%>
                                                <table>                                                
                                                <tr>
                                                    <td>Frecuencia de visitas de representante</td>
                                                    <td>                                                        
                                                        <select id="ddlServCap_Frecuencia" class="form-control" >
                                                            <option value="0">-</option>                                            
                                                        </select>
                                                    </td>
                                                </tr>
                                                </table>
                                            </div>
                                            </div>
                                    
                                        </td>                                    
                                    </tr>
                                    <%--
                                    /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                    3/4 Auditoría 
                                    Auditoría 
                                    Auditoría 
                                    Auditoría 
                                    /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                    --%>
                                    <tr>                                        
                                         <td>
                                            <table class="tb_servicio_titulo">
                                                <tr>                                                                               
                                                    <td style="width:30px;">
                                                        <input type="checkbox" id="chb_ServAud_Aplicar" class="form-control" />
                                                    </td>
                                                    <td><label>Auditoría</label></td>                                                  
                                                    <td style="width:30px;">
                                                        <i class="fa fa-balance-scale fa-lg"></i>
                                                    </td>	                                                    
                                                </tr>                                                
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trReqServAuditoria" style="display:none;">
                                        <td>
                                        
                                            <table>
                                            <tr>
                                                <td>Tipo de Auditoria</td>
                                                <td>
                                                    <select class="form-control" id="sel_ServAud_Tipo1">
                                                    <option value="0">-</option>                                                    
                                                    </select>
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;</td>
                                                <td>Subsegmento</td>
                                                <td>
                                                    <select class="form-control" id="sel_ServAud_Tipo2">
                                                    <option value="0">-</option>                                                    
                                                    </select>
                                                </td>
                                            </tr>
                                            </table>
                                            <div class="panel panel-default" style="margin-top:5px;">                                            
                                            <div class="panel-body">
                                    
                                                <table>                                        
                                                <tr>
                                                    <td style="width:150px;">Días de recepción</td>                                            
                                                    <td>
                                                        <table style="width:100%">
                                                        <tr>
                                                            <td>L</td>
                                                            <td>M</td>
                                                            <td>M</td>
                                                            <td>J</td>
                                                            <td>V</td>
                                                            <td>S</td>
                                                            <td>D</td>
                                                            <td>Cualquier día</td>
                                                        </tr>
                                                        <tr>
                                                                                           
                                                            <td><input type="checkbox" id="chb_ServAud_Lunes" class="form-control chb" value="" /></td>
                                                            <td><input type="checkbox" id="chb_ServAud_Martes" class="form-control chb" value=""/></td>
                                                            <td><input type="checkbox" id="chb_ServAud_Miercoles" class="form-control chb" value=""/></td>
                                                            <td><input type="checkbox" id="chb_ServAud_Jueves" class="form-control chb" value=""/></td>
                                                            <td><input type="checkbox" id="chb_ServAud_Viernes" class="form-control chb" value=""/></td>
                                                            <td><input type="checkbox" id="chb_ServAud_Sabado" class="form-control chb" value=""/></td>
                                                                                           
                                                            <td><input type="checkbox" id="chb_ServAud_Domingo" class="form-control chb" value=""/></td>
                                                            <td><input type="checkbox" id="chb_ServAud_CualquierDia" class="form-control chb" value=""/></td>                                                        
                                                        </tr>
                                                        </table>
                                                    </td>         
                                                </tr>
                                                <tr>
                                                    <td style="width:150px;">Horarios de Recepcion</td>
                                                    <td>
                                                        <table style="width:100%">
                                                            <tr>                                                                
                                                                <td><input type="text" id="tb_ServAud_HorariosRecep1" class="form-control" value="" /></td>
                                                                <td>a</td>
                                                                <td><input type="text" id="tb_ServAud_HorariosRecep2" class="form-control" value="" /></td>
                                                                <td>y</td>
                                                                <td><input type="text" id="tb_ServAud_HorariosRecep3" class="form-control" value="" /></td>
                                                                <td>a</td>
                                                                <td><input type="text" id="tb_ServAud_HorariosRecep4" class="form-control" value="" /></td>                                                                                                           
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    
                                                </tr>                                       
                                                </table>
                                                <table style="width:100%;" class="mt5">
                                                <tr>
                                                    <td style="width:150px;">Cita para Auditorias</td>
                                                    <td style="width:20px;">
                                                        <input type="checkbox" id="chb_ServAud_MismoDia" class="form-control chb" value="" />
                                                    </td>
                                                    <td style="width:100px;">Mismo día</td>
                                                    <td style="width:20px;">
                                                        <input type="checkbox" id="chb_ServAud_Previa" class="form-control chb" value="" />
                                                    </td>
                                                    <td>Previa</td>
                                                </tr>                                         
                                                </table>
                                                
                                                <%--OCT24-2019--%>
                                                <table>                                                
                                                <tr>
                                                    <td>Frecuencia de visitas de representante</td>
                                                    <td>                                                        
                                                        <select id="ddlServAud_Frecuencia" class="form-control" >
                                                            <option value="0">-</option>                                            
                                                        </select>
                                                    </td>
                                                </tr>
                                                </table>
                                            </div>
                                            </div>
                                        </td>                                                                            
                                    </tr>                                    
                                    <%--
                                    /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                    4/4 Asesoría 
                                    Asesoría 
                                    Asesoría 
                                    Asesoría 
                                    /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
                                    --%>
                                    <tr>                                        
                                        <td>
                                            <table class="tb_servicio_titulo">
                                            <tr>
                                                <td style="width:30px;">
                                                    <input type="checkbox" id="chb_ServAsesoria_Aplicar" class="form-control" />
                                                </td>
                                                <td><label>Asesoría</label></td>                                                
                                                <td style="width:30px;"><i class="fa fa-mortar-board fa-lg"></i></td>	                                                
                                            </tr>                                            
                                            </table>
                                        </td>
                                    </tr>                                                                       
                                    <tr id="trReqServAsesoria" style="display:none;">
                                        <td>
                                                                                
                                            <table>
                                                <tr>
                                                    <td>Tipo de Asesoria</td>
                                                    <td>
                                                        <select id="sel_ServAsesoria_Tipo1" class="form-control">
                                                            <option value="0">-</option>                                                            
                                                        </select>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                                    <td>UEN</td>
                                                    <td>
                                                        <select id="sel_ServAsesoria_Tipo2" class="form-control">
                                                            <option value="0">-</option>                                                            
                                                        </select>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="panel panel-default" style="margin-top:5px;">                                            
                                            <div class="panel-body">
                                    
                                            <table>                                        
                                            <tr>
                                                <td style="width:150px;">Días de recepción</td>                                            
                                                <td>
                                                    <table style="width:100%" >
                                                        <tr>
                                                            <td>L</td>
                                                            <td>M</td>
                                                            <td>M</td>
                                                            <td>J</td>
                                                            <td>V</td>
                                                            <td>S</td>
                                                            <td>D</td>
                                                            <td>Cualquier día</td>
                                                        </tr>
                                                        <tr>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Lunes" value="" /></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Martes" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Miercoles" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Jueves" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Viernes" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Sabado" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Domingo" value=""/></td>
                                                            <td><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_CualquierDia" value=""/></td>                                                        
                                                        </tr>
                                                     </table>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:150px;">Horarios de Recepcion</td>
                                                <td>
                                                    <table style="width:100%">
                                                        <tr>
                                                            <td><input type="text" class="form-control" id="tb_ServAsesoria_HorariosRecep1" value="" /></td>
                                                            <td>a</td>
                                                            <td><input type="text" class="form-control" id="tb_ServAsesoria_HorariosRecep2" value="" /></td>
                                                            <td>y</td>
                                                            <td><input type="text" class="form-control" id="tb_ServAsesoria_HorariosRecep3" value="" /></td>
                                                            <td>a</td>
                                                            <td><input type="text" class="form-control" id="tb_ServAsesoria_HorariosRecep4" value="" /></td>                                                                                                        
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                            </tr>                                       
                                            </table>
                                            <table style="width:100%;">                                        
                                            <tr>
                                                <td style="width:150px;">Cita para Servicio</td>
                                                <td style="width:20px;"><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_MismoDia" value="" /></td>
                                                <td style="width:100px;">Mismo día</td>
                                                <td style="width:20px;"><input type="checkbox" class="form-control chb" id="chb_ServAsesoria_Previa" value="" /></td>                                                                                        
                                                <td>Previa</td>                                            
                                            </tr>                                         
                                            </table>
                                                    <%--OCT24-2019--%>
                                                <table>                                                
                                                <tr>
                                                    <td>Frecuencia de visitas de representante</td>
                                                    <td>                                                        
                                                        <select id="ddlServAsesoria_Frecuencia" class="form-control" >
                                                            <option value="0">-</option>                                            
                                                        </select>
                                                    </td>
                                                </tr>
                                                </table>
                                            </div>
                                            </div>
                                    
                                        </td>                                        
                                    </tr>
                                    </table>
                                    <label>Comentarios / Recomendaciones (por RIK/Gte/JO)</label>
                                    <textarea id="tbAcs_ComentariosRecomendaciones" class="form-control" rows="5"></textarea>
                            </div>
                            </div>                               
                            </div>                            
                            
                            <%--FIN TABLS /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
                                                        
                        </div>
                    
              </div> <%--dvCuerpoVentanaDimension                      --%>
              
            <div class="modal-footer">                            
                <div class="row">
                    <div class="col-md-12 pull-right">
                        <table>
                        <tr>
                            <td>
                                <button id="btnOrden_Reducir" class="btn btn-default" style="display:none;" >
                                    <i class="fa fa-expand" aria-hidden="true"></i>&nbsp;Reducir
                                </button>                                    
                            </td><td>
                                <button id="btnOrden_Expandir" class="btn btn-default" style="display:block;">
                                    <i class="fa fa-expand" aria-hidden="true"></i>&nbsp;Expandir
                                </button>
                            </td><td>
                                <button id="btnOrdenCancelar" class="btn btn-default">Cancelar</button>
                            </td>
                            <td>
                                <button id="btnOrdenGuardar" class="btn btn-primary">Guardar</button>
                            </td>
                            <td>
                                <button 
                                id="btnOrdenAutorizarJefeOp" 
                                class="btn btn-primary btn_ControlOrden" 
                                onClick = "btnAutorizarControlDeOrden_JefeOp();"
                                style="display:none;">Autorizar Control de Orden
                                </button>
                            </td>
                            <td>
                                <button 
                                id="btnOrdenRechazarJefeOp" 
                                class="btn btn-danger" 
                                onClick = "btnRechazarControlDeOrden_JefeOp();"
                                style="display:none;">Rechazar
                                </button>
                            </td>
                            <td>
                                <img id="spinner_AcysOrden" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            </td>                            
                        </tr>
                        </table>

                    </div>
                </div>
            </div>
        </div>


    </div>
</div>

<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->                          
<!--MOTIVO RECHAZO -->
<div class="modal fade" id="modalMotivoRechazo" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:400px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button7" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H6">
                    Rechazo de documento
                </h4>
            </div>
            <div class="modal-body">                                    

                <input type="hidden" id="hfMotivoRechazo_TipoUsuario" value="0" />
            
                <table class="center" style="width:100%;">
                <tr>
                <td>
                    <label>Observaciones:</label>
                </td>
                </tr>               
                <tr>
                <td align="center">
                    <textarea id="taMotivoRechazo" class="form-control" rows="10" cols="30"></textarea>
                </td>
                </tr>               
                </table>    

            </div>
            <div class="modal-footer">
                <button id="Button10" type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar</button>                
                <button id="btnMotivoRechazo_Guarar" type="button" 
                        class="btn btn-primary"                         
                        onclick="btnAutorizarControlDeOrden_JefeOp_Rechazo();" data-dismiss="modal">
                        Aplicar
                    </button>
            </div>
        </div>
    </div>
</div>

<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->                          
<!-- ENVIAR AUTORIZACION -->
<div class="modal fade" id="modalEnviarAutorizacion" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:400px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button2" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H5">
                    Solicitar Autorizaci&oacute;n
                </h4>
            </div>
            <div class="modal-body">                                    

                <input type="hidden" id="hfAut_Id_Acs" value="" /> 
                <input type="hidden" id="hfAut_Acs_Version" value="" /> 
                <input type="hidden" id="hfAut_Vecido" value="" /> 
                <input type="hidden" id="hfAut_Id_Cte" value="" /> 
                <input type="hidden" id="hfAut_Id_Ter" value="" /> 

                <table class="center" style="width:100%;">
                <tr>
                <td align="center;">

                    <button id="btnEnviar_Autorizacion_Gerente" type="button" 
                        class="btn btn-primary" 
                        style="width:350px;"
                        onclick="Enviar_Autorizacion_Gerente();" 
                        <%--data-dismiss="modal"--%>
                        >
                        Solicitar autorizaci&oacute;n para ACYS<br>Gerente 
                    </button>

                </td>
                </tr>
                <tr>
                <td align="center;">

                    <button id="btnEnviarAutorizacion_ControlOrden" type="button" 
                        class="btn btn-primary" 
                        style="width:350px; background-color:#fdd25f!important; color:black;"
                        onclick="Enviar_Autorizacion_JefeOp();" 
                        <%--data-dismiss="modal"--%>
                        >
                        Solicitar autorizaci&oacute;n para CONTROL DE ORDEN<br>Jefe de operacion
                    </button>

                </td>
                </tr>
                </table>    

            </div>
            <div class="modal-footer">
                <button id="Button6" type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar</button>                
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
                        Alerta de Precio Acys
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 40% !Important;">
                    <iframe class="embed-responsive-item" id="frameAlerta" height="80%" width="100%" runat="server" src="../Ventana_AutorizacionPrecios.aspx"></iframe>
                </div>
            </div>
        </div>
    </div>
<!--\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->

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
</div>
    <script type="text/javascript">
        document.title = 'ACyS';
        var _ApplicationUrl = '<%=ApplicationUrl %>';
        var _Usuario_Tipo = "<%= Usuario_Tipo %>";
        var Id_Rik = '<%=Id_Rik %>';
        var Id_U = '<%=Id_U %>';
        // 19AGO-2021 RFH
        // 08SEP-2021 RFH
        var GLOBAL_Activo_AcysCuentasNacionales = '<%=GLOBAL_Activo_AcysCuentasNacionales %>';
        var GLOBAL_Activo_NoRequiereAutAcys = '<%=GLOBAL_Activo_NoRequiereAutAcys %>';
        $("#txtProductoBuscar").autocomplete({ source: null });
    </script>

    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_config.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_excel.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ClienteDet_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/tools.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Login.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Paginacion.js?v=20210822")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/CatSemana_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Cliente.js?v=20210822")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_index_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_index.js?v=20260324")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_ajax.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Func.js?v=20260407.09")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Main.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Orden_Save.js?v=20241028")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Orden_Ajax.js?v=20210822")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_Orden_Load.js?v=20210822")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Acys/Acys2_OrdenFunc.js?v=20241028")%>"></script>
    
    <script type="text/javascript">
        console.log("Usuario_Tipo:" + _Usuario_Tipo);
    </script>

</asp:Content>