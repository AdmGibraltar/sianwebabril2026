<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" 
AutoEventWireup="true" CodeBehind="Compras_Locales.aspx.cs" Inherits="SIANWEB.CL.Compras_Locales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

     <!-- Acutalizado MAR4-2020 RFH -->

   <!-- ALERTIFY 0.3.11 NUEVO -->
    <!--script language="JavaScript" type="text/javascript" src="<?php echo base_url ('publico/alertify.js-0.3.11/src/alertify.js'); ?>"></script-->																						      
    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <!--link rel="stylesheet" type="text/css" href="<?php echo base_url ('publico/alertify.js-0.3.11/themes/alertify.core.css'); ?>" /-->    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <!--link rel="stylesheet" type="text/css" href="<?php echo base_url ('publico/alertify.js-0.3.11/themes/alertify.default.css'); ?>" /-->    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
        
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        
    
    <script type="text/javascript">
        document.title = 'Compras Locales';
        var _ApplicationUrl = '<%=ApplicationUrl %>';
        var _Usuario_Tipo = "<%=Usuario_Tipo %>";
        var iCDI = "<%=iCDI %>";
        var sCDI_Nombre = "<%=sCDI_Nombre %>";
    </script>
        
    <%--JQuery UI --%>
    <link type="text/css" rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>              
        
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

    <%--TIME PICKER--%>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>

    <%--exportar excel--%>
    <script type="text/jscript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/jscript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/jscript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
        
    <style type="text/css">        
        /* Controles AUTOCOMPLETAR*/
        /* El Ancho del Control esta En CL_Autocompletar*/ 
        .searchbutton {
            margin-top: -24px;
            margin-left: 500px;
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
        .chosen-container {
            width:100%!important;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
          }
          .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
          }
          .custom-combobox-input {
            margin: 0;
            padding: 8px 18px;
          }
    </style>
            
<div class="container">
            
<%--    
    - - - - - - - - - - - - - -
    CAMPOS GNERALES DE LA FORMA 
    - - - - - - - - - - - - - -
--%>

<div class="row mt5" style="display:none;" >    
    <div class="col-md-3">
        <input id="hfId_Prd" type="hidden" value="" />  
        <input id="hfId_PrdGenerado" type="hidden" value="" />  
        <input id="hfPrd_Descripcion" type="hidden" value="" />  
    </div>
</div>

<%--OCULTO--%>
<div class="row mt5" style="display:none;">
    <div class="col-md-12">
        <table class="table">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px"></td>
                <td width="150px" style="font-weight: bold">
                    <div id="dvCmbCentros" runat="server" >            
                    </div>
                    <input type="hidden" id="txtCentrosDist" name="txtCentrosDist" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 90%; text-align: center" colspan="4">                
                </td>
            </tr>
        </table>
    </div>
</div>

<div class="row mt5">
    <div class="col-md-11 text-center">
        <h2 id="lblTituloProducto" forecolor="Red" cssclass="tituloProducto" style="color:Red;font-size:28px;">...</h2 >                
    </div>
    <div class="col-md-1">        
        <input id="hfCDI" type="hidden" value="" />        
        <table>
            <tr>
                <td>Centro de distribucion</td>
                <td><label id="txtCDINombre">...</label></td>
            </tr>
        </table>
    </div>
</div>

<div class="row mt5">    
    <div class="col-md-3">
        <label>Motivo para la Compra Local</label>                                            
    </div>
    <div class="col-md-5">
        <select id="cmbCategorias" class="form-control">                                    
        </select>
    </div>    
    <div class="col-md-1">                
        <i id="SPINNER_CL" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:none; margin-top:3px;"></i>
    </div>        
</div>

<%--
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    AREA 1 - Activacion de codigo por desabasto
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -     
--%>

<div id="divMotivo_Area_1" style="display:none;">
        
    <div class="row mt5" id="row_CodigoKey">
        <div class="col-md-3">
            <label>Código Key del Producto en Desabasto</label>
        </div>
        <div class="col-md-6">
            <input id="txtSearch" class="form-control" disabled ="disabled"/>  
        </div>        
        <div class="col-md-1">
            <button id="btnBuscarProducto" type="button" class="btn btn-primary" title="Búsqueda de producto">
                Buscar&nbsp;<i class="fa fa-search clickable"></i>     
            </button>
            <input id="hfCustomerId"  class="form-control hidden" disdisabled ="disabled"/>  
        </div>                
    </div>

    <%--Código de abasto del producto--%>
    <div class="row mt5" id="row_CodigoAbasto">
        <div class="col-md-3">
            <label>Código de abasto del producto</label>
        </div>
        <div class="col-md-3">
            <input id="txtCodigoUsadoProd" class="form-control" disabled ="disabled"/>  
            <input id="hfNumSolicitud"  type="hidden" />  
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblNumSolicitud" runat="server"  CssClass="tituloProducto"></asp:Label>
        </div>
    </div>
            
    <%--Aplicación Familia--%>
    <div class="row mt5" id="row_AplicacionSubFam" style="display:none;">
        <div class="col-md-1">        
            Aplicación:
        </div>
        <div class="col-md-4">            
            <select id="cmbAplicacionSoli" class="form-control">                                                    
            </select>
        </div>
        <div class="col-md-1">
            SubFamilia:
        </div>
        <div class="col-md-4">            
            <select id="cmdSubFamiliaSoli" class="form-control">                                                    
            </select>
        </div>
        <div class="col-md-1">            
            <button id="btnSeleccionaProdSol" type="button" onclick="CL.btnSeleccionaProdSol_Click();" class="btn btn-primary">
                Aplicar&nbsp;<i class="fa fa-check clickable"></i>     
            </button>
        </div>
    </div>
        
    <div class="row mt5" id="row_AbstoLocal">
        <div class="col-md-3">
            <label>Código del Producto (Solo Local)</label>
        </div>
        <div class="col-md-7">                
            <select id="cmbProductosHabiliCompraLocal" class="form-control w100"> 
            </select>
        </div>        
        <div class="col-md-2">
            <button id="btnProducto_AvastoLocal_Ok" type="button" class="btn btn-primary" title="Aplicar la seleccion">
                Seleccionar&nbsp;<i class="fa fa-check clickable"></i>     
            </button>
        </div>        
    </div>
     
</div>
        
<%--
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    AREA 2 - Codigo central con abasto local
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -     
--%>

<div id="divMotivo_Area_2" class="row mt5" style="display:none; height:500px; ">
    <div class="col-md-12">
     <%--TABS --%>
    <ul class="nav nav-tabs mt5" role="tablist">
        <li class="nav-item active">
            <a class="nav-link active" href="#DatosGenerales" data-toggle="tab">Datos generales</a>                
        </li>        
        <li class="nav-item">
            <a class="nav-link" href="#Precios" data-toggle="tab">Precios</a>
        </li>            
        <li class="nav-item">
            <a class="nav-link" href="#SAT" data-toggle="tab">SAT</a>
        </li>                        
        <li class="nav-item" style="display:block;">
            <a class="nav-link" href="#ClientesExclusivos" data-toggle="tab">Clientes Exclusivos</a>
        </li>                        
    </ul>

    <div class="tab-content">

            <%--TAB DATOS GENERALES --%>

            <div class="tab-pane active" id="DatosGenerales">                    

                <div class="row mt10">
                    <div class="col-md-3">Código del producto</div>
                    <div class="col-md-2">
                        <input id="TextId_Prd" class="form-control" />
                        <input type="hidden" id="Id_Cpr" value=""/>
                    </div>
                    <div class="col-md-2">
                        <p id="lbCodProd">Código de abasto local</p>                        
                    </div>
                    <div class="col-md-2">                        
                        <input id="txtCodProd" class="form-control" value="" />
                    </div>
                    <div class="col-md-1 hidden">                        
                        <input id="chkActivo" class="form-contol" type="checkbox"  />Activo
                    </div>
               </div>
                   
                <div class="row">
                    <div class="col-md-2"><label id="lbl_Val_TextId_Prd" ></label></div>
                    <div class="col-md-2"><label id="lbl_Val_txtCodProd" ></label></div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Descripción</div>
                    <div class="col-md-2 hidden">
                        <input id="chkProductoNuevo" class="form-contol" type="checkbox"  /> Producto nuevo
                    </div>
                    <div class="col-md-4">                        
                        <input id="TextPrd_Descrpcion" class="form-control" value="" onblur="EstablecerLabelTituloProductoDesAbasto_2();" />
                    </div>
                    
                    <div class="col-md-1" id="col_lbVigencia" style="display:none;" >
                        <asp:Label ID="Label889" runat="server" Text="Vigencia Inicio"/>
                    </div>
                    <div class="col-md-2" id="col_tbVigencia" style="display:none;">                        
                        <input id="rdpVigencia" class="form-control datepicker wfecha" value="" />
                    </div>   
                </div>

                <div class="row mt5">
                    <div class="col-md-3"><label id="lbl_Val_TextPrd_Descripcion"></label></div> 
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Presentación</div>
                    <div class="col-md-4"><select id="cmbPresentacion" class="form-control" style="display:none"></select>
                    <label id="lbl_Val_txtPresentacion" style="display:none"></label> 
                    <input id="txtPresentacion" class="form-control" value=""  /> 
                    </div>
<%--                </div>--%>

                
                    <%-- RBM Vigencias  --%>   
                    <div class="col-md-1" id="col_lbVigenciaFin" style="display:none;">
                        <asp:Label ID="lblVigenciaFin" runat="server" Text="Vigencia Fin"/>
                    </div>
                    <div class="col-md-2" id="col_tbVigenciaFin" style="display:none;">                        
                        <input  id="rdpVigenciaFin" class="form-control datepicker wfecha" value="" />
                    </div>                  

                </div>

                <div class="row mt5">
                    <div class="col-md-3">Tipo de producto</div>
                    <div class="col-md-1">
                        <input id="txtTipoProducto" class="form-control" value="" />
                    </div>
                    <div class="col-md-4">
                        <select id="cmbTipoProducto" class="form-control w100"></select>                                                
                        <asp:Label ID="lbl_Val_txtTipoProducto" runat="server" ForeColor="Red"></asp:Label>                        
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Aplicación de producto</div>
                    <div class="col-md-6">                        
                        <select id="cmbFam" class="form-control"></select>                        
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Sub-familia de producto</div>
                    <div class="col-md-4">
                        <select id="cmbSubFam" class="form-control"></select>
                    </div>
                </div>

                 <div class="row mt5">
                    <div class="col-md-3" id="divProvCentral">Proveedor Central</div>
                    <div class="col-md-4">
                    <input id="txtProveedorCentral" class="form-control" value=""  />
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3"  id="divProvLocal">Proveedor Local</div>
                    <div class="col-md-1">
                        <input id="txtProveedor" class="form-control" disabled="disabled"/>
                    </div>
                    <div class="col-md-1" style="display:none;" >
                        <input id="txtProductosMismoPadre" class="form-control" type="text" disabled="disabled"/>
                    </div>
                    <div class="col-md-4">                        
                        <select id="cmbProveedor" class="form-control" ></select>
                    </div>                    
                    <div class="col-md-1">                        
                        <label id="lbl_Val_txtProveedor" ></label>
                    </div>
                </div>
                
                <div class="row mt5">
                    <div class="col-md-3">Código de producto proveedor</div>
                    <div class="col-md-4">
                    <input id="txtCodProveedor" class="form-control" value="" />
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Descripción de producto proveedor</div>
                    <div class="col-md-4">
                    <input id="txtDesProveedor" class="form-control" value="" />
                    </div>
                </div>

                <div class="row mt5">
                    <div class="col-md-3">Presentación de producto proveedor</div>
                    <div class="col-md-4"><select id="cmbPresentacionProv" class="form-control"></select></div>
                    <div><label id="lbl_Val_txtpresproveedor"></label> </div>
                </div>          

                <div class="row mt5">
                    <div class="col-md-3">                        
                        Unidad de entrada
                    </div>
                    <div class="col-md-2">                        
                        <select id="cmbUentrada" class="form-control">
                        </select>
                    </div>
                       <div class="col-md-2">                        
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
                    <div class="col-md-3">
                        Unidad de salida
                    </div>
                    <div class="col-md-2">                        
                        <select id="cmbUsalida" class="form-control">
                        </select>
                    </div>
                    <div class="col-md-2">
                        Unidades de empaque
                    </div>
                    <div class="col-md-2">                        
                        <input id="txtUempaque" class="form-control"/>
                    </div>
                </div>
                                
                <div class="row mt5" id="row_CausaDesabasto">
                    <div class="col-md-3">
                        Motivo del desabasto
                    </div>
                    <div class="col-md-6">                                                        
                        <select id="cmbCausaDesabasto" class="form-control">                                                    
                        </select>                                            
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
                    <div class="col-md-6 text-center">

                            <p style="font-family: Verdana; font-size:small; font-style:italic; display:none;">
                            <a href="JavaScript:HistorialPrecios_()" id="lnkHistorialPreciosCodigo_" >Historial de Precios</a>
                            </p>
                                                        
                            <button id="lnkHistorialPreciosCodigo" type="button" class="btn btn-primary">Historial de Precios</button>

                    </div>
                </div>

                <div class="row mt10">
                    <div class="col-md-6 text-right">

                        <table id="tbl_rgPrecios" class="table table-bordered">
                            <thead>
                                <tr> 
                                    
                               <%--<th class="text-center">Fec. inicial</th>
                                    <th class="text-center">Fec. final</th>--%>
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
                            <%--<tr>
                                <td>Fecha inicial</td>
                                <td>
                                    <input id="tbModificaTipoPrecio_FechaIni" type="text" class="form-control wfecha datepicker" value="" />
                                </td>
                            </tr>
                            <tr>
                                <td>Fecha final</td>
                                <td>
                                    <input id="tbModificaTipoPrecio_FechaFin" type="text" class="form-control wfecha datepicker" value="" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td>Tipo de precio</td>
                                <td class="text-center">
                                    <label id="lbModificaTipoPrecio">..</label>
                                </td>
                            </tr>
                            <tr>
                                <td>Precio</td>
                                <td>                                    
                                    <input id="tbModificaTipoPrecio_Precio"  class="form-control" value="" onkeypress="return event.charCode >= 46 && event.charCode <= 57" />
                                </td>
                            </tr>
                           <%-- <tr>
                                <td>Comentario</td>
                                <td>
                                    <input id="tbModificaTipoPrecio_Comentario" type="text" class="form-control" value="" />
                                </td>
                            </tr>--%>
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
               <%--     <div class="col-md-3">Unidad de Medida (SAT)</div>--%>
                    <div class="col-md-6">                        
                        <select id="cmbUnidadMedidaSATDesabasto" class="form-control"></select>                        
                    </div>
                </div>
                <%--<div class="row mt5">
                    <div class="col-md-6">
                        <select id="cmbUnidadMedidaSATDesabasto" class="form-control">
                            <option></option>
                        </select>
                        <label id="lblcmbUnidadMedidaSATDesabasto"></label>
                                  
                    </div>
                </div>--%>

                 
                <div class="row mt5">
                    <div class="col-md-6">
                        Producto/Servicios (SAT):
                    </div>
                </div>

                 <div class="row mt5">
                   <%-- <div class="col-md-3">Producto/Servicios (SAT):</div>--%>
                    <div class="col-md-6">                        
                        <select id="ddlProdServicio_SATDesabasto" class="form-control"></select>                        
                    </div>
                </div>

                <%--<div class="row mt5">
                    <div class="col-md-6">
                </div>    

                <div class="row mt5">
                    <div class="col-md-6">
                        </div>                                 
                    </div>    
                </div>

                <div class="row mt5">
                    <div class="col-md-6">
                  
                    <select id="ddlProdServicio_SATDesabasto"  data-placeholder="Seleccione..." class="chosen-select" tabindex="7">
                      <option value=""></option>
                      <option>A</option>
                      <option>B</option>                      
                    </select>

                    </div>    
                </div>--%>

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
</div>

<hr />  

<div id="divArea_Guardar" class="row mt5" style="display:none; margin-top:10px; margin-bottom:10px;">
    <div class="col-md-2 text-left">    
        <button ID="btnGuardarCompraLocal" type="button" class="btn btn-primary" title="Aplicar la seleccion" 
            onclick="CL.btnGuardar_CompraLocal();">
            Guardar documento&nbsp;
            <i class="fa fa-save clickable"></i>     
        </button>
    </div>
    <div class="col-md-1">    
        <i id="SPINNER_Guardar" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:none; margin-top:3px;"></i>
    </div>
</div>

<%--
CONTROLES NO VISIBLES
--%>

<div class="row mt5" style="display:none;">    
    <div class="col-md-12">
        <asp:CheckBox ID="chkComprasLocales" runat="server" Text="Compras locales" Enabled="false" />
        <asp:CheckBox ID="chkComprasLocalesCte" runat="server" Text="Compras locales" Enabled="false" />
    </div>
</div>


<%--
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    AREA 2 - Solicitud del cliente
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -     
--%>


<div id="divMotivo_Area_3" class="row mt5" style="display:none;">
    <div class="col-md-12">

    <div class="row mt5">
        <div class="col-md-12">

               <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item active">
                    <a class="nav-link active" href="#DatosGenerales_Op3" data-toggle="tab">Datos generales</a>                
                </li>        
                <li class="nav-item">
                    <a class="nav-link" href="#Precios_Op3" data-toggle="tab">Precios</a>
                </li>            
                <li class="nav-item">
                    <a class="nav-link" href="#SAT_Op3" data-toggle="tab">SAT</a>
                </li>                        
                <li class="nav-item">
                    <a class="nav-link" href="#SAT_Op3" data-toggle="tab">Clientes Exclusivos</a>
                </li>                        
            </ul>

            <div class="tab-content">
                <div class="tab-pane active" id="DatosGenerales_Op3">                    

                
                                                                    <table border="0" cellpadding="1" cellspacing="1">
                                                                        <!--Tab 1  Tabla 1-->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label681" runat="server" Text="Código del producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                
                                                                                <%--<telerik:RadNumericTextBox ID="TextId_PrdCte" runat="server" Width="150px" MaxLength="16"  enabled="false">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="TextId_PrdCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            --%>

                                                                                <asp:Label ID="Label682" runat="server" Text="Código usado del producto" Visible="false"></asp:Label>
                                                                            
                                                                               <asp:HiddenField  ID="txtCodProdCte" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <div style="visibility:hidden">
                                                                                <%--<asp:CheckBox ID="chkActivoCte" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                                                    AutoPostBack="True" /> </div>--%>

                                                                                <input type="checkbox" id="chkActivoCte" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_TextId_PrdCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label97" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label96" runat="server" Text="Descripción"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">

                                                                                <%--<telerik:RadTextBox ID="TextPrd_DescrpcionCte" runat="server" Width="306px" MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SinComilla" OnBlur="TextPrd_DescrpcionCte_OnBlur" />
                                                                                </telerik:RadTextBox>--%>

                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                
                                                                                <%--<asp:CheckBox ID="chkProdNuevoCte" runat="server" Text="Producto nuevo"  Checked="True"  /></div>--%>

                                                                                <input type="checkbox" id="chkProdNuevoCte" value="" />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_TextPrd_DescrpcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label98" runat="server" Text="Presentación"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadNumericTextBox ID="txtPresentacionCte" runat="server" Width="70px" MaxLength="5"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                                <asp:Label ID="lbl_Val_txtPresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label100" runat="server" Text="Tipo de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadNumericTextBox ID="txtTipoProductoCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtTipoProductoCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <%--<telerik:RadComboBox ID="cmbTipoProductoCte" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbTipoProductoCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>--%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtTipoProductoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label106" runat="server" Text="Sistemas propietarios"  Visible="false"></asp:Label>
                                                                                <%--<telerik:RadNumericTextBox ID="TextId_SpoCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1"  Visible="false" >
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="TextId_SpoCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbSisPropCte" runat="server" Width="250px" Filter="Contains"  Visible="false"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbSisPropCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>--%>
                                                                                <asp:Label ID="lbl_Val_TextId_SpoCte" runat="server" ForeColor="Red" visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label1106" runat="server" Text="Categoría de producto"  visible="true"></asp:Label>
                                                                                <div>
                                                                              <%--  <telerik:RadNumericTextBox ID="txtCategoriaCte" runat="server"  visible="false" Width="70px" MaxLength="9" MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtCategoriaCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                </div>
                                                                                <telerik:RadComboBox ID="cmbCategoriaCte" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                                    OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbCategoriaCte_ClientSelectedIndexChanged"
                                                                                    Width="250px" LoadingMessage="Cargando..." MaxHeight="200px"  visible="false">
                                                                                </telerik:RadComboBox>--%>
                                                                                <asp:Label ID="lbl_Val_txtCategoriaCte" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label108" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2" nowrap>
                                                                                <%--<telerik:RadComboBox ID="cmbFamCte" runat="server" Width="450px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                                                    MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFamCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>--%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFam" runat="server" ForeColor="Red"></asp:Label>
                                                                                 <div style='visibility:hidden'>
                                                                                    <%--<telerik:RadNumericTextBox ID="txtFamCte" runat="server" Width="70px" MaxLength="9"
                                                                                        MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                        <ClientEvents OnBlur="txtFamCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    </telerik:RadNumericTextBox>--%>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label109" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">

                                                                                <%--<telerik:RadComboBox ID="cmbSubFamCte" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbSubFamCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
--%>
                                                                            </td>
                                                                            <td>
                                                                            <div style='visibility:hidden'>
                                                                                    <%--<telerik:RadNumericTextBox ID="txtSubFamCte" runat="server" Width="70px" MaxLength="9"
                                                                                        MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                        <ClientEvents OnBlur="txtSubFamCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    </telerik:RadNumericTextBox>--%>
                                                                                </div>
                                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label111" runat="server" Text="Proveedor Local"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadNumericTextBox ID="txtProveedorCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtProveedorCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                                <asp:TextBox ID="txtProductosMismoPadreCte" runat="server" style='width:10px; visibility:hidden;' ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadComboBox ID="cmbProveedorCte" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px"
                                                                                    Autopostback="false"
                                                                                    >
                                                                                </telerik:RadComboBox>--%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtProveedorCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <!--Tab 1 Tabla 3-->
                                                                        </table>

                                                                        
                                                                        <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label113" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadComboBox ID="cmbUentradaCte" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbUentradaCte_OnClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                                </telerik:RadComboBox>--%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_cmbUentradaCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label115" runat="server" Text="Factor de conversión"  Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadNumericTextBox ID="txtFactorConversionCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0"  Visible="false">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label116" runat="server" Text="Unidad de salida"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadComboBox ID="cmbUsalidaCte" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                                </telerik:RadComboBox>--%>
                                                                                <asp:Label ID="lbl_Val_cmbUsalidaCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label117" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<telerik:RadNumericTextBox ID="txtUempaqueCte" runat="server" Width="50px" MaxLength="9" >
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                                <asp:Label ID="lbl_Val_txtUempaque" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label2" runat="server" Text="Unidad de salida"></asp:Label>
                                                                            </td>
                                                                            <td>                                     
                                                                                <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr><td colspan="4">&nbsp;</td></tr>
                                                                      
                                                                    </table>


                       


                    


                </div>
                <div class="tab-pane active" id="Precios_Op3">                    
                </div>
                <div class="tab-pane active" id="SAT_Op3">                    
                </div>
           </div>
            
        </div>
    </div>
    

    </div>
</div>
  

</div>
 <%--CONTAINER--%>
  <%--CONTAINER--%>
   <%--CONTAINER--%> 
  

<div class="row mt5" id="row_Activacion" style="display:none;">
    <div class="col-md-12">
        
        <%--divActivacion--%>
        <%--divActivacion--%>
        <%--divActivacion--%>

        <div runat="server" id="divActivacion" style="font-family: Verdana; font-size: 8pt" visible="false">
        
        <table>           
            <tr>
                <td>&nbsp;</td>
                <td colspan="4">
                    <div runat="server" id="formularioProductos" style="margin-left: 10px; margin-right: 10px;">
                        <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                            SelectedIndex="0" TabIndex="-1">
                            <Tabs>
                                <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewParametros" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewIndicadores" Text="<u>E</u>xistencia Inv" AccessKey="E" visible="false" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewDetalles" Text="<u>P</u>recios" AccessKey="P" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewSAT" Text="SA<u>T</u>" AccessKey="T" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewCompLocal" Text="Proveedor <u>C</u> Locales" AccessKey="C" visible="false" >
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                            Width="800px">
                            <!-- Aqui empieza el contenido de los tabs--->
                            <telerik:RadPageView ID="RadPageViewDGrales" runat="server" BorderStyle="Solid" BorderWidth="1px">

                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewParametros" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">

                                <table style="font-family: vernada; font-size: 8;">
                                    <!-- Tabla principal--->
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table>
                                                <!--Tab 2 Tabla 1 -->
                                                <tr>
                                                    <td colspan="4">
                                                        <strong>
                                                            <asp:Label ID="Label17" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label18" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                    </td>
                                                    <td>

                                                        <%--<telerik:RadNumericTextBox ID="txtInvSeguridad" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
--%>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSistProp" runat="server" Text="Aparato de sistema propietario" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label19" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                    </td>
                                                    <td>
                                                        
                                                        <%--<telerik:RadNumericTextBox ID="txtTentrega" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label46" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                    </td>
                                                    <td>

                                                    <%--    <telerik:RadTextBox ID="txtPlanAbasto" runat="server" Width="150px" MaxLength="20">
                                                        <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico"></ClientEvents>
                                                            </telerik:RadTextBox>--%>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label47" runat="server" Text="Minimo de compra"></asp:Label>
                                                    </td>
                                                    <td>

                                                    <%--    <telerik:RadNumericTextBox ID="txtMinCompra" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                    </td>
                                                    <td>

                                                    <%--    <telerik:RadNumericTextBox ID="txtTtransporte" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label21" runat="server" Text="Rentabilidad"></asp:Label>

                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtRentabilidad" runat="server" Width="20px"
                                                            MaxLength="1">
                                                            <ClientEvents OnKeyPress="SoloAlfabetico" OnBlur="txtRentabilidad_OnBlur" />
                                                        </telerik:RadTextBox>
                                                        <telerik:RadComboBox ID="cmbRentabilidad" runat="server" Width="200px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                            DataValueField="Id" OnClientSelectedIndexChanged="cmbRentabilidad_ClientSelectedIndexChanged"
                                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                        </telerik:RadComboBox>--%>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_Rentabilidad" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: right">
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <!--Tab 2 Tabla 1 -->
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label22" runat="server" Text="Meses de amortización"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtAmortizacion" runat="server" Width="70px" MaxLength="3"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_Amortizacion" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtPesos" runat="server" Width="70px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label24" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtExistencia" runat="server" Width="70px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" Text="Ubicación"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtUbicacion" runat="server" Width="70px"
                                                            MaxLength="5">
                                                            <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label44" runat="server" Text="Contribuci&oacute;n"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtContribucion" runat="server" Width="70px" MaxLength="9"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPorUtilidades" runat="server" Text="Porcentaje de participaci&oacute;n de utilidades"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtPorUtilidades" runat="server" Width="70px" MaxLength="3"
                                                            MinValue="0">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewIndicadores" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <!-- Tabla principal--->
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <strong>
                                                            <asp:Label ID="Label26" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td colspan="2" style="text-align: center">
                                                        <strong>
                                                            <asp:Label ID="Label27" runat="server" Text="Inventarios"></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text="Asignado"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtAsignado" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Inicial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtInicial" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text="Ordenado"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtOrdenado" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label33" runat="server" Text="Final"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtFinal" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label31" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtTransito" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label32" runat="server" Text="F&iacute;sico"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadNumericTextBox ID="txtFisico" runat="server" Width="50px" Enabled="false"
                                                            MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewDetalles" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td><asp:HiddenField ID="hdfAAA" runat="server" />
                                            <asp:HiddenField ID="hdFechaInicial" runat="server" />
                                        </td>
                                        <td>
                               
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewSAT" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td><br /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>



                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br /><br />
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewCompLocal" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label34" runat="server" Text="Fabricante"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label35" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtFnombre" runat="server" Width="150px"
                                                            MaxLength="100">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFnombre" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label36" runat="server" Text="Código de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtFcodigo" runat="server" Width="100px"
                                                            MaxLength="30">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFcodigo" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label37" runat="server" Text="Descripción de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtFdescripcion" runat="server" Width="150px"
                                                            MaxLength="100">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label38" runat="server" Text="Presentación de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtFpresentacion" runat="server" Width="100px"
                                                            MaxLength="20">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label39" runat="server" Text="Proveedor Local"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label40" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtPnombre" runat="server" Width="150px" visible="false"
                                                            MaxLength="100" >
                                                        </telerik:RadTextBox>--%>
                                                            <asp:TextBox ID="txtSearchProv" runat="server" Width="300px" MaxLength="6" />
                                                            <asp:HiddenField ID="hfProviderId" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPnombre" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label41" runat="server" Text="Código de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtPcodigo" runat="server" Width="100px"
                                                            MaxLength="30">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPcodigo" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label42" runat="server" Text="Descripción de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtPdescripcion" runat="server" Width="150px"
                                                            MaxLength="100">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label43" runat="server" Text="Presentación de producto"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<telerik:RadTextBox onpaste="return false" ID="txtPpresentacion" runat="server" Width="100px"
                                                            MaxLength="20">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>  
                        </telerik:RadMultiPage>
                        <br />
                        <asp:HiddenField ID="hddPrecioCostoCodigo" runat="server" />
                        <asp:HiddenField ID="hddPrecioAAACodigo" runat="server" />
                        <br />
                        <span style="text-align:right">
                            <div id="ver_off1" style="display: none">

                            <%--<asp:Button ID="btnEnviaSolicitud" Text="." runat="server" OnClick="EnviarSolicitud" Autopostback="true" 
                                style=" width:0; height:0; border-style:none; display:none; border:0; "  />
--%>
                            <button id="btnEnviaSolicitud" class="btn">
                                .
                            </button>


                            </div>
                        </span>
                                                
                    </div>
                </td>
            </tr>
        </table>

        </div>

         


    </div>
</div>
    
<div class="row mt5" id="row_Abasto" style="display:none;">
    <div class="col-md-12">
        
        <%--divAbasto--%>
        <%--divAbasto--%>
        <%--divAbasto--%>

<%--      
        <telerik:radtoolbar id="toolbarop2" runat="server" width="100%" dir="rtl" onbuttonclick="EnviarSolicitudAbasto" onclientbuttonclicked="EnviarSolicitudAbas" AutoPostBack="true">
            <Items>
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
            </Items>
        </telerik:radtoolbar>--%>

        <button>Regresar</button>
        <button>Guardar</button>
        
        <table>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtProductoLocal" runat="server" Width="450px" MaxLength="20" Visible=false/>
                    <asp:HiddenField ID="hfProductoLocal" runat="server" />
                    <asp:HiddenField ID="hddPrecioAAAOriginal" runat="server" />
                    <asp:HiddenField ID="hddPrecioCostoOriginal" runat="server" />
                    <asp:HiddenField ID="hfNumSolicitudAbasto" runat="server" />
                    <div style="visibility:hidden" id="divgenabastopost">

                        <telerik:RadComboBox ID="cmbGeneAbasto" runat="server" DataTextField="Descripcion" DataValueField="Id" />

                        <telerik:RadComboBox ID="cmbGeneAbastoSubFam" runat="server" DataTextField="Descripcion" DataValueField="Id" />

                        <%--<asp:Button ID="btnBuscaProductoLocal" Text="Seleccionar" runat="server" OnClick="SubmitProductoLocal" />--%>

                        <button ID="btnBuscaProductoLocal">
                            Seleccionar
                        </button>

                    </div>

                </td>
                <td></td>
            </tr>
           
            <tr>
                <td colspan="5" align="right">&nbsp;
                    <asp:Label ID="lblNumSolicitudAbasto" runat="server"  CssClass="tituloProducto"></asp:Label></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="4">

                        <telerik:RadTabStrip ID="RadTabStripAbasto" runat="server" MultiPageID="RadMultiPagePrincipalAbasto"
                            SelectedIndex="0" TabIndex="-1">
                            <Tabs>
                                <telerik:RadTab PageViewID="RadPageViewDGralesAbasto" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewParametrosAbasto" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewIndicadoresAbasto" Text="<u>E</u>xistencia Inv" AccessKey="E" visible="false" >
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewDetallesAbasto" Text="<u>P</u>recios" AccessKey="P">
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewSATAbasto" Text="SA<u>T</u>" AccessKey="T">
                                </telerik:RadTab>
                                <telerik:RadTab PageViewID="RadPageViewCompLocalAbasto" Text="Proveedor <u>C</u> Locales" AccessKey="C" visible="false" >
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>

                        <telerik:RadMultiPage ID="RadMultiPagePrincipalAbasto" runat="server" SelectedIndex="0"
                            Width="800px">
                            <!-- Aqui empieza el contenido de los tabs--->
                            <telerik:RadPageView ID="RadPageViewDGralesAbasto" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 16;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table border="0" cellpadding=1 cellspacing=1>
                                                <!--Tab 1  Tabla 1-->
                                                <tr >
                                                    <td>
                                                        <%--<asp:Label ID="Label49" runat="server" Text="Código del producto"></asp:Label>--%>                                                        
                                                        Código del producto
                                                    </td>
                                                    <td width="200px">&nbsp;
                                                        <%--<asp:Label runat="server" ID="lblId_Prd"></asp:Label>--%>
                                                        <label ID="lblId_Prd"></label>
                                                    </td>
                                                    <td>
                                                        <%--<asp:Label ID="Label50" runat="server" Text="Código Compra Local del producto"></asp:Label>--%>                                                        
                                                        Código Compra Local del producto
                                                    </td>
                                                    <td width="200px">&nbsp;
                                                        <%--<asp:Label runat="server" ID="lblCodProd" ></asp:Label>--%>
                                                        <label ID="lblCodProd"></label>
                                                    </td>
                                                    <td ><div style="visibility:hidden">
                                                        <%--<asp:CheckBox ID="chkActivoAbasto" Checked="True" runat="server" Text="Activo"/></div>--%>
                                                        <input type="checkbox" id="chkActivoAbasto" />Activo
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <%--<asp:Label ID="Label51" runat="server" ForeColor="Red"></asp:Label>--%>
                                                        <label ID="Label51"></label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <%--<asp:Label ID="Label52" runat="server" ForeColor="Red"></asp:Label>--%>
                                                        <label ID="Label52"></label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--<asp:Label ID="Label53" runat="server" Text="Descripción"></asp:Label>--%>                                                        
                                                        Descripción
                                                    </td>
                                                    <td colspan="2">&nbsp;
                                                        <asp:Label runat="server" ID="lblPrd_Descrpcion" ></asp:Label>
                                                        <input type="hidden" name="lblPrd_DescrpcionAbasto" runat="server" id="lblPrd_DescrpcionAbasto" />
                                                    </td>
                                                    <td><div style="visibility:hidden">
                                                        <asp:CheckBox ID="chkProductoNuevoAbasto" runat="server" Text="Producto nuevo" Enabled="false" /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="Label54" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--<asp:Label ID="Label55" runat="server" Text="Presentación"></asp:Label>--%>
                                                        Presentación
                                                    </td>
                                                    <td colspan="2"">&nbsp;
                                                        <asp:Label runat="server" ID="lblPresentacion" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label56" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label57" runat="server" Text="Tipo de producto"></asp:Label>
                                                    </td>
                                                    <td >&nbsp;
                                                        <asp:Label runat="server" ID="lblTipoProducto"  ></asp:Label>
                                                    </td>
                                                    <td >
                                                        <asp:Label runat="server" ID="lblcmbTipoProducto" ></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                        <asp:Label ID="Label58" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="Label59" runat="server" Text="Sistemas propietarios"  visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblId_Spo"  visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblcmbSisProp"  visible="false"></asp:Label>
                                                        <asp:Label ID="Label60" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                        <asp:Label ID="Label61"  visible="false" runat="server" Text="Agrupado de equipos de sistemas propietarios"></asp:Label>
                                                        <asp:Label runat="server" ID="lblAgrupadoSpo"  visible="false" ></asp:Label>
                                                        <asp:Label ID="Label62" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                        <asp:Label ID="Label63" runat="server" Text="Categoría de producto"  visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblCategoria"  visible="false" ></asp:Label>
                                                        <asp:Label runat="server" ID="lblcmbCategoria"  visible="false" ></asp:Label>
                                                        <asp:Label ID="Label64" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label65" runat="server" Text="Aplicación de producto"></asp:Label>
                                                    </td>
                                                    <td colspan="3">&nbsp;
                                                        <asp:Label runat="server" ID="lblFam" visible="false"></asp:Label>
                                                    <!-- </td>
                                                    <td Width="270px">&nbsp; -->
                                                        <asp:Label runat="server" ID="lblcmbFam" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label66" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                    </td>
                                                    <td colspan="2">&nbsp;
                                                        <asp:Label runat="server" ID="lblSubFam" visible="false" ></asp:Label>
                                                    <!-- </td>
                                                    <td Width="270px">&nbsp;-->
                                                        <asp:Label runat="server" ID="lblcmbSubFam" ></asp:Label>
                                                        <asp:HiddenField ID="hddFamiliaAbasto" runat="server" />
                                                    </td>
                                                    <td>&nbsp; 
                                                        <asp:Label ID="Label67" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="Label68" runat="server" Text="Proveedor" Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblProveedor"  Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblcmbProveedor"  Visible="false"></asp:Label>
                                                        <asp:Label ID="Label69" runat="server" ForeColor="Red"  Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label811" runat="server" Text="Proveedor"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <input type="text" id="txtProveeAba" value="" />
<%--
                                                        <telerik:RadNumericTextBox ID="txtProveeAba" runat="server" Width="70px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtProveedorAba_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>--%>
                                                        <asp:HiddenField ID="hidRpoveedorOriginal" runat="server" />
                                                        <asp:HiddenField ID="hidProductoOriginal" runat="server" />
                                                        <asp:TextBox ID="txtProductosMismoPadreAbasto" runat="server" style='width:10px;visibility:hidden;'  ></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbProveedorAba" runat="server" Width="350px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                            DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorAba_ClientSelectedIndexChanged"
                                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtProveedorAba" runat="server" ForeColor="Red"></asp:Label>
                                                        <div id="FijarProveedor" style="visibility:hidden">
                                                        <%--<asp:Button ID="btnRefTituloAba"   runat="server" OnClick="ActualizaTItuloAba" Text="Fijar Proveedor" />--%>
                                                        <button ID="btnRefTituloAba">Fijar Proveedor</button>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <!--Tab 1 Tabla 3-->
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label70" runat="server" Text="Unidad de entrada"></asp:Label>
                                                    </td>
                                                    <td width="220px" >&nbsp;
                                                        <asp:Label runat="server" ID="lblcmbUentrada" ></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                        <asp:Label ID="Label71" runat="server" ForeColor="Red"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="Label72" runat="server" Text="Factor de conversión"></asp:Label>
                                                    </td>
                                                    <td Width="12px">&nbsp;
                                                        <asp:Label runat="server" ID="lbltxtFactorConversion" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label73" runat="server" Text="Unidad de salida"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                        <asp:Label runat="server" ID="lblcmbUsalida" ></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                        <asp:Label ID="Label74" runat="server" Text="Unidades de empaque"></asp:Label>
                                                    </td>
                                                    <td Width="200px">&nbsp;
                                                        <asp:Label runat="server" ID="lbltxtUempaque" ></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewParametrosAbasto" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <!-- Tabla principal--->
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table cellpadding="5px">
                                                <!--Tab 2 Tabla 1 -->
                                                <tr>
                                                    <td colspan="4">
                                                        <strong>
                                                            <asp:Label ID="Label75" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label76" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtInvSeguridad" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkPropietarioAbasto" runat="server" Text="Aparato de sistema propietario" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label77" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:Label runat="server" ID="lbltxtTentrega" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label78" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                    </td>
                                                    <td width="170px">
                                                        <asp:Label runat="server" ID="lbltxtPlanAbasto" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label79" runat="server" Text="Minimo de compra"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:Label runat="server" ID="lbltxtMinCompra" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label80" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:Label runat="server" ID="lbltxtTtransporte" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <!--
                                                    <td width="40px">
                                                        <asp:Label ID="Label81" runat="server" Text="Rentabilidad"></asp:Label>
                                                        <asp:Label runat="server" ID="lbltxtRentabilidad" ></asp:Label>
                                                        <asp:Label runat="server" ID="lbllblRentabilidad" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label82" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: right">
                                                        <asp:CheckBox ID="chkComprasLocalesAbasto" runat="server" Text="Compras locales" Enabled="false" />
                                                    </td>
                                                </tr>
                                                -->
                                            </table>
                                            <table cellpadding="5px">
                                                <!--Tab 2 Tabla 1 -->
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label83" runat="server" Text="Meses de amortización"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtAmortizacion" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label84" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label85" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtPesos" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label86" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtExistencia" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label87" runat="server" Text="Ubicación"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtUbicacion" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <!--
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label88" runat="server" Text="Contribuci&oacute;n"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtContribucion" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label89" runat="server" Text="Porcentaje de participaci&oacute;n de utilidades"></asp:Label>
                                                    </td>
                                                    <td width="90px">
                                                        <asp:Label runat="server" ID="lbltxtPorUtilidades"></asp:Label>
                                                    </td>
                                                </tr>
                                                -->
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewIndicadoresAbasto" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <!-- Tabla principal--->
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table cellpadding="5px">
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <strong>
                                                            <asp:Label ID="Label90" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td colspan="2" style="text-align: center">
                                                        <strong>
                                                            <asp:Label ID="Label91" runat="server" Text="Inventarios"></asp:Label></strong>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label92" runat="server" Text="Asignado"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtAsignado"></asp:label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label93" runat="server" Text="Inicial"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtInicial" ></asp:label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label94" runat="server" Text="Ordenado"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtOrdenado"></asp:label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label95" runat="server" Text="Final"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtFinal" ></asp:label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1131" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtTransito"></asp:label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label1132" runat="server" Text="F&iacute;sico"></asp:Label>
                                                    </td>
                                                    <td width="70px">
                                                        <asp:label runat="server" ID="lbltxtFisico"></asp:label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewSATAbasto" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td><br /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:HiddenField ID="HiddenField4" runat="server" />
                                            <asp:HiddenField ID="HiddenField5" runat="server" />
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label828" runat="server" Text="Unidad de Medida (//SAT):"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <select id="cmbUnidadMedidaSATAbasto" class="form-control">
                                                            <option></option>
                                                        </select>
                                                        <asp:Label ID="lbl_Val_cmbUnidadMedidaSATAbasto" runat="server" ForeColor="Red"  ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--<telerik:RadComboBox ID="cmbProdServicioSATAbasto" runat="server" Width="5px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                            DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="10px" visible="false" onSelectedIndexChanged="cmbProdServicioSATDesabasto_SelectedIndexChanged" AutoPostBack="True">
                                                        </telerik:RadComboBox>--%>
                                                        <select id="cmbProdServicioSATAbasto" class="form-control">
                                                            <option></option>
                                                        </select>
                                                    <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label839" runat="server" Text="Producto///Servicios (SAT):"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <input ID="txtSearchProdServSATAbasto"  type="text" runat="server" name="txtSearchProdServSATAbasto" style='width:650px' onchange="cambiatexto()"/>
                                                        <input id="hfCveProdServSATAbasto" type="hidden"  name="hfCveProdServSATAbasto" runat="server" />
                                                        <asp:Label ID="lbl_Val_cmbProdServicioSATAbasto" runat="server" ForeColor="Red"  ></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br /><br />
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewDetallesAbasto" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td align="right">
                                                        <p style="font-family: Verdana; font-size:small; font-style:italic">
                                                            <a href="JavaScript:HistorialPrecios()" id="lnkHPreciosAbasto" >Historial de Precios</a>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table ID="ajaxFormPanelAbasto" class="table" >
                                                            <thead>
                                                                <tr>
                                                                    <th>Empresa</th>
                                                                    <th>Cd</th>
                                                                    <th>Producto</th>
                                                                    <th>TP</th>
                                                                    <th>Prod_Actual</th>
                                                                    <th>Fec. Inicial</th>
                                                                    <th>Fec. Final</th>
                                                                    <th>Tipo de precio</th>
                                                                    <th>Pesos</th>
                                                                    <th>Comentario</th>
                                                                    <th>btnEditar</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                      <%--  <telerik:RadAjaxPanel ID="ajaxFormPanelAbasto" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                            <telerik:RadGrid ID="rgPreciosAbasto" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                OnNeedDataSource="grdPreciosAbasto_NeedDataSource" OnUpdateCommand="grdPreciosAbasto_UpdateCommand"
                                                                OnPreRender="grdPreciosAbasto_PreRender" OnItemDataBound="grdPreciosAbasto_ItemDataBound"
                                                                OnPageIndexChanged="grdPreciosAbasto_PageIndexChanged">
                                                                <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                    EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                    Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                    <ExpandCollapseColumn Visible="True">
                                                                    </ExpandCollapseColumn>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                            Display="false" ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                            ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                            Display="false" ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                            ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                            Display="false" ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                            UniqueName="Prd_FechaInicio">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFechaInicioAba" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %> '
                                                                                    Width="200px" />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadDatePicker ID="datePickerFechaInicioAba" runat="server" MinDate="1900-01-01"  enabled="false"
                                                                                    DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                    <DatePopupButton ToolTip="Abrir calendario" />
                                                                                    <Calendar ID="dateCalendarFechaInicioAba" runat="server" RangeMinDate="1900-01-01">
                                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                            TodayButtonCaption="Hoy" />
                                                                                    </Calendar>
                                                                                </telerik:RadDatePicker>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFechaFinAba" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                    Width="200px" />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadDatePicker ID="datePickerFechaFinAba" runat="server" MinDate="1900-01-01"  enabled="false"
                                                                                    DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                    <DatePopupButton ToolTip="Abrir calendario" />
                                                                                    <Calendar ID="dateCalendarFechaFinAba" runat="server" RangeMinDate="1900-01-01">
                                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                            TodayButtonCaption="Hoy" />
                                                                                    </Calendar>
                                                                                </telerik:RadDatePicker>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                            UniqueName="Pre_Descripcion">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTipoPrecioAba" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lblTipoPrecioEditAba" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                    Font-Bold="true" />
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPrd_PesosAba" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="txtPrd_PesosAba" runat="server" Width="100px" MaxLength="9"
                                                                                    MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                            UniqueName="Prd_PreDescripcion" Display="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPrd_PreDescripcionAba" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionAba" runat="server"
                                                                                    Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                            EditText="Editar" HeaderText="Editar">
                                                                        </telerik:GridEditCommandColumn>
                                                                    </Columns>
                                                                    <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                        InsertCaption="Agregar nuevo precio de producto">
                                                                        <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                        <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                            BorderColor="#000000" BorderWidth="1" />
                                                                        <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                        <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                        <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                        <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                            EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                        </EditColumn>
                                                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                    </EditFormSettings>
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                    LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                    PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                <GroupingSettings CaseSensitive="False" />
                                                                <ClientSettings>
                                                                    <ClientEvents OnRowDblClick="rgPreciosAbasto_ClientRowDblClick" />
                                                                    <Selecting AllowRowSelect="true" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </telerik:RadAjaxPanel>
--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewCompLocalAbasto" runat="server" BorderStyle="Solid"
                                BorderWidth="1px">
                                <table style="font-family: vernada; font-size: 8;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label1134" runat="server" Text="Fabricante"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1135" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td width="170px">
                                                        <asp:label runat="server" ID="lbltxtFnombre"></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFnombre1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1136" runat="server" Text="Código de producto"></asp:Label>
                                                    </td>
                                                    <td width="120px">
                                                        <asp:label runat="server" ID="lbltxtFcodigo" ></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFcodigo1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1137" runat="server" Text="Descripción de producto"></asp:Label>
                                                    </td>
                                                    <td width="170px">
                                                        <asp:label runat="server" ID="lbltxtFdescripcion"></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFdescripcion1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1138" runat="server" Text="Presentación de producto"></asp:Label>
                                                    </td>
                                                    <td width="120px">
                                                        <asp:label runat="server" ID="lbltxtFpresentacion"></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtFpresentacion1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label1139" runat="server" Text="Proveedor"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1140" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox onpaste="return false" ID="txtPnombreAbasto" runat="server" Width="150px" visible="false"
                                                            MaxLength="100" >
                                                        </telerik:RadTextBox>
                                                            <asp:TextBox ID="txtSearchProvAbasto" runat="server" Width="300px" MaxLength="6" />
                                                            <asp:HiddenField ID="hfProviderAbastoId" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPnombre1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1141" runat="server" Text="Código de producto"></asp:Label>
                                                    </td>
                                                    <td width="120px">
                                                        <asp:label runat="server" ID="lbltxtPcodigo"></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPcodigo1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1142" runat="server" Text="Descripción de producto"></asp:Label>
                                                    </td>
                                                    <td width="120px">
                                                        <asp:label runat="server" ID="lbltxtPdescripcion"></asp:label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPdescripcion1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1143" runat="server" Text="Presentación de producto"></asp:Label>
                                                    </td>
                                                    <td width="120px">
                                                        <asp:label runat="server" ID="lbltxtPpresentacion" ></asp:label> 
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Val_txtPpresentacion1" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
        <br />
        <br />
            <div id="ver_off2" style="display: none" runat="server" >
            <%--    <asp:Button ID="btnEnviarAbasto" Text="Envío de Solicitud" runat="server" OnClick="EnviarSolicitudAbasto" style="visibility:hidden" />
                <asp:Button ID="btnCancelarAbasto" Text="Cancelar" runat="server"  OnClientClick="LimpiarControlesProductoAbasto" />
--%>
                <button ID="btnEnviarAbasto">Envío de Solicitud</button>
                <button ID="btnCancelarAbasto">Cancelar</button>
            </div>
       
                
    </div>
</div>
<div class="row mt5" id="div_SolicitudCliente" style="display:none;">
    <div class="col-md-12">
    
                                <%--divSolicitudCliente--%>
                                <%--divSolicitudCliente--%>
                                <%--divSolicitudCliente--%>
                              
                                <%--<telerik:radtoolbar id="toolbarop3" runat="server" width="100%" dir="rtl" onbuttonclick="EnviarSolicitudCliente" onclientbuttonclicked="EnviarSolicitudClients" AutoPostBack="True">
                                    <Items>
                                        <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false"/>
                                        <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                    </Items>
                                </telerik:radtoolbar>
--%>
                                <button ID="Button1">Regresar</button>
                                <button ID="Button3">Guardar</button>
                                   <table>
                                      
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td colspan="3">
                                                <table class="table" id="RadComboBoxProduct" >
                                                    <thead>
                                                        <tr><td>Id</td></tr>
                                                        <tr><td>Producto</td></tr>
                                                    </thead>
                                                </table>
                                                
                                               <%-- <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProduct" runat="server" Height="200" Width="450px" 
                                                    DropDownWidth="480" EmptyMessage="Elige un Producto" HighlightTemplatedItems="true"
                                                    EnableLoadOnDemand="true" Filter="StartsWith"  enabled="false"  onSelectedItem="SubmitProductoSoli" 
                                                    Label="Producto: " visible="false">
                                                    <HeaderTemplate>
                                                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    ID
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    Producto
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Ids']")%>
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Producto']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
--%>
                                            </td>
                                            <td rowspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblCodigoProductoCteF4F" runat="server" Text="Código del Producto:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIdProductoCteF4F" runat="server" Width="550px" Enabled="true" Visible="false"/>
                                                <asp:HiddenField ID="hdfProductoCteF4F" runat="server" />
                                                <asp:HiddenField ID="hdSubFamiliaSoli" runat="server" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height:30px" align="right">&nbsp;
                                            <asp:HiddenField ID="hfNumSolicitudCte" runat="server" />
                                             <asp:Label ID="lblNumSolicitudCte" runat="server" text="" CssClass="tituloProducto"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="4">
                                            <div runat="server" id="PestanasCte" style="margin-left: 10px; margin-right: 10px;" Visible="false">
                                                <telerik:RadTabStrip ID="RadTabStripPrincipalCte" runat="server" MultiPageID="RadMultiPagePrincipalCte"
                                                    SelectedIndex="0" TabIndex="-1">
                                                    <Tabs>
                                                        <telerik:RadTab PageViewID="RadPageViewDGralesCte" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewParametrosCte" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewIndicadoresCte" Text="<u>E</u>xistencia Inv" AccessKey="E"  visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewDetallesCte" Text="<u>P</u>recios" AccessKey="P">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewSATCte" Text="SA<u>T</u>" AccessKey="T">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewCompLocalCte" Text="Proveedor <u>C</u> Locales" AccessKey="D" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewClientesAut" Text="<u>C</u>lientes Exclusivos" AccessKey="C" >
                                                        </telerik:RadTab>
                                                    </Tabs>
                                                </telerik:RadTabStrip>
                                                <telerik:RadMultiPage ID="RadMultiPagePrincipalCte" runat="server" SelectedIndex="0"
                                                    Width="800px">
                                                    <!-- Aqui empieza el contenido de los tabs--->
                                                    <telerik:RadPageView ID="RadPageViewDGralesCte" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td nowrap>

                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewParametrosCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <strong>
                                                                                    <asp:Label ID="Label518" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label519" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInvSeguridadCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>

                                                                                <asp:CheckBox ID="chkSistPropCte" runat="server" Text="Sistema propietario" />                                                                               

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label520" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTentregaCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label521" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtPlanAbastoCte" runat="server" Width="150px" MaxLength="20">
                                                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                                                 <ClientEvents OnKeyPress="SoloAlfanumerico"></ClientEvents>
                                                                                 </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label522" runat="server" Text="Minimo de compra"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtMinCompraCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label523" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTtransporteCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td colspan="2">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" style="text-align: right">
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table >
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label526" runat="server" Text="Meses de amortización"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAmortizacionCte" runat="server" Width="70px" MaxLength="3"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label127" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label528" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtPesosCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label529" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtExistenciaCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label530" runat="server" Text="Ubicación"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtUbicacionCte" runat="server" Width="70px"
                                                                                    MaxLength="5">
                                                                                    <ClientEvents OnKeyPress="SoloAlfabetico" ></ClientEvents>
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewIndicadoresCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label533" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                            <td style="width: 20px">
                                                                            </td>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label534" runat="server" Text="Inventarios"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label535" runat="server" Text="Asignado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAsignadoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label536" runat="server" Text="Inicial"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInicialCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label537" runat="server" Text="Ordenado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtOrdenadoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label433" runat="server" Text="Final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFinalCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label431" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTransitoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label432" runat="server" Text="F&iacute;sico"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFisicoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewDetallesCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                            
                                                                                <table ID="ajaxFormPanelCte" class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Empresa</th>
                                                                                            <th>Cd</th>
                                                                                            <th>Producto</th>
                                                                                            <th>TP</th>
                                                                                            <th>Prod_Actual</th>
                                                                                            <%--<th>Fec. Inicial</th>
                                                                                            <th>Fec. Final</th>--%>
                                                                                            <th>Tipo de precio</th>
                                                                                            <th>Pesos</th>
                                                                                            <th>Comentario</th>
                                                                                            <th>btnEditar</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                    </tbody>
                                                                                </table>


                                                                              <%--  <telerik:RadAjaxPanel ID="ajaxFormPanelCte" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                    <telerik:RadGrid ID="rgPreciosCte" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                        PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                        OnNeedDataSource="rgPreciosCte_NeedDataSource" OnUpdateCommand="rgPreciosCte_UpdateCommand"
                                                                                        OnPreRender="rgPreciosCte_PreRender" OnItemDataBound="rgPreciosCte_ItemDataBound"
                                                                                        OnPageIndexChanged="rgPreciosCte_PageIndexChanged">
                                                                                        <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                            EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                            Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                            <ExpandCollapseColumn Visible="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                    UniqueName="Prd_FechaInicio">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaInicioCte" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaInicioCte" runat="server" MinDate="1900-01-01" enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaInicioCte" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaFinCte" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaFinCte" runat="server" MinDate="1900-01-01" enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="datePickerFechaFinCte" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                    UniqueName="Pre_Descripcion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioCte" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioEditCte" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                            Font-Bold="true" />
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                
                                                                                                <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PesosCte" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadNumericTextBox ID="txtPrd_PesosCte" runat="server" Width="100px" MaxLength="9"
                                                                                                            MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                        </telerik:RadNumericTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                    UniqueName="Prd_PreDescripcion" Display="false" >
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PreDescripcionCte" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionCte" runat="server"
                                                                                                            Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                        </telerik:RadTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                    EditText="Editar" HeaderText="Editar">
                                                                                                </telerik:GridEditCommandColumn>
                                                                                            </Columns>
                                                                                            <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                                InsertCaption="Agregar nuevo precio de producto">
                                                                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                    BorderColor="#000000" BorderWidth="1" />
                                                                                                <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                                <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                    EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                                </EditColumn>
                                                                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                            </EditFormSettings>
                                                                                        </MasterTableView>
                                                                                        <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                            LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                            PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                        <GroupingSettings CaseSensitive="False" />
                                                                                        <ClientSettings>
                                                                                            <ClientEvents OnRowDblClick="rgPreciosCte_ClientRowDblClick" />
                                                                                            <Selecting AllowRowSelect="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </telerik:RadAjaxPanel>
--%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewSATCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><asp:HiddenField ID="HiddenField6" runat="server" />
                                                                    <asp:HiddenField ID="HiddenField7" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label99" runat="server" Text="Unidad de Medida (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUnidadMedidaSATCte" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="300px" >
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_cmbUnidadMedidaSATCte" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><telerik:RadComboBox ID="cmbProdServicioSATCte" runat="server" Width="5px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="10px" visible="false" >
                                                                                </telerik:RadComboBox>
                                                                            <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label101" runat="server" Text="Producto/Servicios (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <input ID="txtSearchProdServSATACte"  type="text" name="txtSearchProdServSATACte" style='width:650px' />
                                                                                <input id="hfCveProdServSATCte" type="hidden"  name="hfCveProdServSATCte" runat="server" />
                                                                                <asp:Label ID="lbl_Val_cmbProdServicioSATCte" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewCompLocalCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label434" runat="server" Text="Fabricante"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label435" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFnombreCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFnombreCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label436" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFcodigoCte" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFcodigoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label437" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFdescripcionCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFdescripcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label438" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFpresentacionCte" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFpresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label439" runat="server" Text="ProveedorCte"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label440" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPnombreCte" runat="server" Width="150px" visible="false"
                                                                                    MaxLength="100" >
                                                                                </telerik:RadTextBox>
                                                                                 <asp:TextBox ID="txtSearchProvCte" runat="server" Width="300px" MaxLength="6" />
                                                                                  <asp:HiddenField ID="hfProviderIdCte" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPnombreCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label441" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPcodigoCte" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPcodigoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label442" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPdescripcionCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPdescripcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label443" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPpresentacionCte" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPpresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                     <telerik:RadPageView ID="RadPageViewClientesAut" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                      
                                                    </telerik:RadPageView>
                                                </telerik:RadMultiPage>
                                                <br />
                                                <br />
                                                <span style="text-align:right">
                                                <div id="Div2" style="display: none">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                <%--<asp:Button ID="btnEnviaSolicitudCliente" Text="Envío de Solicitud" runat="server" OnClick="EnviarSolicitudCliente" style="visibility:hidden" />
                                                <asp:Button ID="btnCancelaSolicitudCliente" Text="Cancelar" runat="server" OnClientClick="LimpiarControlesProductoCliente"  />
--%>
                                                <button id="btnEnviaSolicitudCliente" class="btn">Envío de Solicitud</button>
                                                <button id="btnCancelaSolicitudCliente" class="btn">Cancelar</button>
                                                
                                                </div>
                                                </span>
                                            </div>
                                        </td>
                                        </tr>
                                 </table>
                             
    </div>
</div>
<div class="row mt5" id="div_ConsultaSolicitud" style="display:none;" >
    <div class="col-md-12">
        
                                <%--divConsultaSolicitud--%>
                                <%--divConsultaSolicitud--%>
                                <%--divConsultaSolicitud--%>
                             
                                <table width="950px" border="0" cellpadding="3" cellspacing="1" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td nowrap="nowrap">Número de Solicitud:</td>
                                                    <td><asp:TextBox ID="txtBuscaXSolCom" runat="server" Width="100px" Enabled="true" MaxLength="6" /></td>
                                                    <td>
                                                        
                                                        <%--<asp:Button ID="btnBuscaSoli" Text="Buscar por Solicitud" runat="server" OnClick="BuscaSolixSoli" Width="130px"  Visible="false"/>--%>
                                                        <button id="btnBuscaSoli" class="btn btn-primary">Buscar por Solicitud</button>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Codigo del producto:</td>
                                                    <td><asp:TextBox ID="txtBuscaXCodProd" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                                                    <td>
                                                    
                                                    <%--&nbsp;<asp:Button ID="btnBuscaCod" Text="Buscar por Producto" runat="server" OnClick="BuscaSoliXProdu" Width="130px"  Visible="false"/>
--%>
                                                    <button id="btnBuscaCod" class="btn btn-primary">Buscar por Producto</button>
                                                    </td>
                                                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaCodi" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Proveedor:</td>
                                                    <td><asp:TextBox ID="txtBuscaXProvee" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                                                    <td>
                                                    
                                                    <%--&nbsp;<asp:Button ID="btnBuscaProv" Text="Buscar por Proveedor" runat="server" OnClick="BuscaSoliXProve" Width="130px" Visible="false"/>
--%>
                                                    <button id="btnBuscaProv" class="btn btn-primary">Buscar por Proveedor</button>
                                                    </td>
                                                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaProv" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" >
                                                    
                                                        <%--&nbsp;<asp:Button ID="btnBuscaGen" Text="Buscar" runat="server" OnClick="BuscaCombinado" Width="100px"/>--%>
                                                        <button ID="btnBuscaGen" class="btn btn-primary">Buscar</button>
    
                                                        <%--&nbsp;&nbsp;
                                                        &nbsp;<asp:Button ID="btnRegresarCons" Text="Regresar" runat="server" OnClick="RegresaDeConsulta" Width="100px"/>
--%>
                                                        <button ID="btnRegresarCons" class="btn btn-primary">Regresar</button>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">
                                            <table class="table" ID="rgCompraLocal">
                                                <thead>
                                                    <tr>
                                                        <th>Clave</th>
                                                        <th>CDI</th>
                                                        <th>Solicito</th>
                                                        <th>IdTipoSolcitud</th>
                                                        <th>Tipo de solicitud</th>
                                                      <%--  <th>Fecha Autorización</th>
                                                        <th>Fecha Solicitud</th>--%>
                                                        <th>Partidas Autorizadas</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                          <%--  <telerik:RadGrid ID="rgCompraLocal" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCompraLocal_PageIndexChanged" OnNeedDataSource="rgCompraLocal_NeedDataSource"
                                                OnItemCommand="rgCompraLocal_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Comp" HeaderText="Clave" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcve" runat="server" Text='<%# Bind("Id_Comp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Cd_Nombre" HeaderText="CDI" UniqueName="column1" Visible="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Solicito_Nombre" HeaderText="Solicito" UniqueName="column2">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud" UniqueName="idTipoSolicitud" Visible="false">
                                                            <HeaderStyle Width="5px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TipoSolicitud" HeaderText="Tipo de Solicitud" UniqueName="column3">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FechaAut" HeaderText="Fecha Autorización" UniqueName="column4"
                                                            Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FechaSol" HeaderText="Fecha Solicitud" UniqueName="column5">
                                                            <HeaderStyle Width="180px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EstatusAut" HeaderText="Partidas Autorizadas" UniqueName="column6" >
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Detail" ImageUrl="Imagenes/iconos/book_blue_view.png" 
                                                            Text="Ver Detalle" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Autorizar" ImageUrl="Imagenes/flecha.jpg" 
                                                            Text="Autorizar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>--%>


                                         </td>
                                        <td >&nbsp;</td>
                                        <td valign="top" nowrap="nowrap">


                                          <table class="table" id="rgDetalleSolicitud">
                                                <thead>
                                                    <tr>
                                                        <th>Clave</th>
                                                        <th>CDI</th>
                                                        <th>Solicito</th>
                                                        <th>IdTipoSolcitud</th>
                                                        <th>Tipo de solicitud</th>
                                                        <th>Fecha Autorización</th>
                                                        <th>Fecha Solicitud</th>
                                                        <th>Partidas Autorizadas</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>



                                         <%--   <telerik:RadGrid ID="rgDetalleSolicitud" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                PageSize="30" AllowPaging="false" AllowSorting="false"  width="100%"
                                                OnItemCommand="rgDetalleSolicitud_ItemCommand"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                 visible="true">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Solicitud" HeaderText="Solicitud" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSolicitud" runat="server" Text='<%# Bind("Solicitud") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="TipoSol" HeaderText="" UniqueName="column5" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoSolicitud" runat="server" Text='<%# Bind("TipoSol") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud" UniqueName="idTipoSolicitud" Visible="false">
                                                            <HeaderStyle Width="5px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Num" HeaderText="Num" UniqueName="column1">
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Costo" HeaderText="Costo" UniqueName="column2">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="column3"
                                                            Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="column4">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="EditSol" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar Producto" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
--%>
                                        </td>
                                     </tr>
                                     <tr><td colspan="3">
                                        <asp:Label ID="titSolicitud" runat="server" CssClass="tituloProducto" Font-Size="28px" ForeColor="blue"></asp:Label>
                                     </td>
                                     </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" nowrap="nowrap">
                                        <div runat="server" id="divConsulta" style="margin-left: 10px; margin-right: 10px;" Visible="false">

                                         <%--   <telerik:radtoolbar id="toolbarop4" runat="server" width="100%" dir="rtl" onbuttonclick="GrabaSolicitudConsulta" onclientbuttonclicked="EnviarSolicitudConsClient"  AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" />
                                                    <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                                </Items>
                                            </telerik:radtoolbar>
--%>
                                            <button id="" class="btn" title="">Regresa</button>
                                            <button id="" class="btn" title="">Guardar</button>

                                            <button class="2btn">Regresar</button>
                                            <button class="2btn">Guardar</button>

                                            <telerik:RadTabStrip ID="RadTabStripPrincipalCons" runat="server" MultiPageID="RadMultiPagePrincipalCons"
                                                SelectedIndex="0" >
                                                <Tabs>
                                                    <telerik:RadTab PageViewID="RadPageViewDGralesCons" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab PageViewID="RadPageViewDetallesCons" Text="<u>P</u>recios" AccessKey="P">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab PageViewID="RadPageViewClientesAutCons" Text="<u>C</u>lientes Exclusivos" AccessKey="C" >
                                                    </telerik:RadTab>
                                                </Tabs>
                                            </telerik:RadTabStrip>
                                            <telerik:RadMultiPage ID="RadMultiPagePrincipalCons" runat="server" SelectedIndex="0" Width="800px">
                                                <telerik:RadPageView ID="RadPageViewDGralesCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                    <table style="font-family: vernada; font-size:small;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <table>
                                                                    <!--Tab 1  Tabla 1-->
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label134" runat="server" Text="Código del producto"></asp:Label>
                                                                        </td>
                                                                        <td>

                                                                        <%--    <telerik:RadNumericTextBox ID="TextId_PrdCons" runat="server" Width="150px" MaxLength="16"  enabled="false"
                                                                                    MinValue="1" MaxValue="999999999999999">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="TextId_PrdCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label135" runat="server" Text="Código usado del producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtCodProdCons" runat="server" Width="200px" MaxLength="16" enabled="false"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td><div style="visibility:hidden">

                                                                        <%--    <asp:CheckBox ID="chkActivoCons" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                                                AutoPostBack="True" /></div>
--%>
                                                                            <input id="chkActivoCons"  type="checkbox" class="form-control" />Activo

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td><asp:Label ID="lbl_Val_TextId_PrdCons" runat="server" ForeColor="Red"></asp:Label></td>
                                                                        <td></td>
                                                                        <td><asp:Label ID="Label137" runat="server" ForeColor="Red"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label138" runat="server" Text="Descripción"></asp:Label>
                                                                        </td>
                                                                        <td>

                                                                            <%--<telerik:RadTextBox ID="TextPrd_DescrpcionCons" runat="server" Width="306px" MaxLength="100">
                                                                                <ClientEvents OnKeyPress="SinComilla" OnBlur="TextPrd_DescrpcionCons_OnBlur" />
                                                                            </telerik:RadTextBox>--%>

                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkProdNuevoCons" runat="server" Text="Producto nuevo"  Checked="True"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_TextPrd_DescrpcionCoins" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label140" runat="server" Text="Presentación"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtPresentacionCons" runat="server" Width="70px" MaxLength="5" MinValue="1">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lbl_Val_txtPresentacionCons" runat="server" ForeColor="Red"></asp:Label>
                                                                            <asp:Label ID="lblDiceVigencia" runat="server" Text="Vigencia hasta"/>
                                                                        </td>
                                                                        <td>

                                                                        <%--<telerik:RadDatePicker Runat="server" id="rdpVigenciaCons"></telerik:RadDatePicker>--%>
                                                                        
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label143" runat="server" Text="Tipo de producto"></asp:Label>
                                                                        </td>
                                                                        <td>

                                                                            <%--<telerik:RadNumericTextBox ID="txtTipoProductoCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtTipoProductoCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbTipoProductoCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbTipoProductoCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtTipoProductoCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label145" runat="server" Text="Sistemas propietarios"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="TextId_SpoCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="TextId_SpoCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbSisPropCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSisPropCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtSisPropCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label147" runat="server" Text="Categoría de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtCategoriaCons" runat="server" MaxLength="9" MinValue="1"
                                                                                Width="70px">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtCategoriaCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbCategoriaCons" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                                DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                                OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbCategoriaCons_ClientSelectedIndexChanged"
                                                                                Width="250px" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtCategoriaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label149" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtFamCons" runat="server" Width="70px" MaxLength="9" MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtFamCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbFamCons" runat="server" Width="450px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                                                MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFamCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtFamiliaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label150" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtSubFamCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtSubFamCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbSubFamCons" runat="server" Width="450px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSubFamCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtSubFamiliaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label152" runat="server" Text="Proveedor"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtProveedorCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtProveedorCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbProveedorCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px"
                                                                                Autopostback="false"
                                                                                >
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtProveedorCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <!--Tab 1 Tabla 3-->
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label154" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbUentradaCons" runat="server" Width="200px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbUentradaCons_OnClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_cmbUentradaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label156" runat="server" Text="Factor de conversión"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtFactorConversionCons" runat="server" Width="50px" MaxLength="9"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label157" runat="server" Text="Unidad de salida"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadComboBox ID="cmbUsalidaCons" runat="server" Width="200px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                            </telerik:RadComboBox>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_cmbUsalidaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label158" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<telerik:RadNumericTextBox ID="txtUempaqueCons" runat="server" Width="50px" MaxLength="9"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;</td></tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="lblMotivoSolicitud" runat="server" Text="Motivo por el cual el cliente solicita este producto en especial"></asp:Label>
                                                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_Val_txtMotivoSolicitaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td colspan="3">
                                                                            <telerik:RadTextBox Runat=server ID="txtMotivoSolicitaCons" MaxLength="250" RenderMode="Lightweight" TextMode="MultiLine" Width="440px"  Height="70px" w ></telerik:RadTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;</td></tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCausaDEsabastoCons" runat="server" Text="Motivo del desabasto"></asp:Label>
                                                                        </td>
                                                                        <td  colspan="3">
                                                                            <%--<telerik:RadComboBox ID="cmbCausaDesabastoCons" runat="server" Width="400px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Desc_CausaDesAbasto"
                                                                                DataValueField="Id" MaxHeight="300px"  >
                                                                            </telerik:RadComboBox>--%>
                                                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_Val_cmbMotivoDEsabastoCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;
                                                                            <div>
                                                                                <asp:ListBox ID="lstbPedidosCons" runat="server" Width="10px" Rows="1" visible="false"></asp:ListBox>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <div id="divPedidosRefCons" runat="server">
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <asp:Label ID="Label816" runat="server" Text="Pedido Desabastecido"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblPedidoSeleccionadoCons" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td  colspan="3">
                                                                                <table border="0" cellpadding="1" cellspacing="1">
                                                                                    <tr>
                                                                                        <td colspan="3"> 
                                                                                            <div id="divSegmentoCons" style="width: 450px; height: 120px; overflow-y: scroll; ">
                                                                                                <asp:CheckBoxList runat="server" ID="chklstPedidosCons" AutoPostBack="false" 
                                                                                                    RepeatColumns="3" CellSpacing="3" CellPadding="3" Width="400px"/>
                                                                                            </div>    
                                                                                            <input id="hddPedidoAbastoCons" type="hidden"  name="hddPedidoAbastoCons" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                         <td>
                                                                                         </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>

                                                                        </div>                                                                        
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageViewDetallesCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                    <table style="font-family: vernada; font-size: 8;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <p style="font-family: Verdana; font-size:small; font-style:italic">
                                                                                <a href="JavaScript:HistorialPrecios()" id="lnkHPrecios" >Historial de Precios</a>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        
                                                                        <table class="table" id="ajaxFormPanelCons" >
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Empresa</th>
                                                                                    <th>Cd</th>
                                                                                    <th>Producto</th>
                                                                                    <th>TP</th>
                                                                                    <th>Prod_Actual</th>
                                                              <%--                  <th>Fec. inicial</th>
                                                                                    <th>Fec. final </th>--%>
                                                                                    <th>Tipo de Precio</th>
                                                                                    <th>Pesos</th>
                                                                                    <th>Comentario</th>
                                                                                    <th>btnEditar</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                                                                                                
                                                                       <%--     <telerik:RadAjaxPanel ID="ajaxFormPanelCons" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                <telerik:RadGrid ID="rgPreciosCons" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                    PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                    OnNeedDataSource="rgPreciosCons_NeedDataSource" OnUpdateCommand="rgPreciosCons_UpdateCommand"
                                                                                    OnPreRender="rgPreciosCons_PreRender" OnItemDataBound="rgPreciosCons_ItemDataBound"
                                                                                    OnPageIndexChanged="rgPreciosCons_PageIndexChanged">
                                                                                    <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                        <ExpandCollapseColumn Visible="True">
                                                                                        </ExpandCollapseColumn>
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                UniqueName="Prd_FechaInicio">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label160" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                                                        Width="200px" />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadDatePicker ID="datePickerFechaInicioCons" runat="server" MinDate="1900-01-01"
                                                                                                        DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                        <Calendar ID="dateCalendarFechaInicioCons" runat="server" RangeMinDate="1900-01-01">
                                                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                TodayButtonCaption="Hoy" />
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label161" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                        Width="200px" />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadDatePicker ID="datePickerFechaFinCons" runat="server" MinDate="1900-01-01"
                                                                                                        DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                        <Calendar ID="datePickerFechaFinCons" runat="server" RangeMinDate="1900-01-01">
                                                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                TodayButtonCaption="Hoy" />
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                UniqueName="Pre_Descripcion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTipoPrecioCons" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblTipoPrecioEditCons" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                        Font-Bold="true" />
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrd_PesosCons" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadNumericTextBox ID="txtPrd_PesosCons" runat="server" Width="100px" MaxLength="9"
                                                                                                        MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                    </telerik:RadNumericTextBox>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                UniqueName="Prd_PreDescripcion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrd_PreDescripcionCons" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionCons" runat="server"
                                                                                                        Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                    </telerik:RadTextBox>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                EditText="Editar" HeaderText="Editar">
                                                                                            </telerik:GridEditCommandColumn>
                                                                                        </Columns>
                                                                                        <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                            InsertCaption="Agregar nuevo precio de producto">
                                                                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                BorderColor="#000000" BorderWidth="1" />
                                                                                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                            <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                            </EditColumn>
                                                                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                        </EditFormSettings>
                                                                                    </MasterTableView>
                                                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                    <GroupingSettings CaseSensitive="False" />
                                                                                    <ClientSettings>
                                                                                        <ClientEvents OnRowDblClick="rgPreciosCte_ClientRowDblClick" />
                                                                                        <Selecting AllowRowSelect="true" />
                                                                                    </ClientSettings>
                                                                                </telerik:RadGrid>
                                                                            </telerik:RadAjaxPanel>
--%>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageViewClientesAutCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                <table style="font-family: vernada; font-size: 8;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="lstClientesAutorizadosCons" runat="server" Width="550px" Rows="4" OnDblClick="JavaScript: DelItemCons();"   ></asp:ListBox>
                                                                        <%--<input id="ddlElementsCons" type="hidden" name="ddlElementsCons" runat="server"  onblur="return ddlElementsCons_onblur()" />--%>
                                                                        <input id="ddlElementsFullCons" type="hidden" name="ddlElementsFullCons" runat="server"  />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                <td>&nbsp;</td>
                                                                <td> Cliente:</td>
                                                                <td><input id="txtNomCteListadoCons"  type="text" name="txtNomCteListadoCons" style='width:450px' />
                                                                <input id="hdtxtClienteListadoCons" type="hidden"  name="hdtxtClienteListadoCons" />
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                    <input onclick="JavaScript: AddItemCons();" type="button" value="Agregar Cliente" />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadPageView>
                                            </telerik:RadMultiPage>
                                            <br />
                                            <br />
                                            <span style="text-align:right">
                                                <div id="ver_off4" style="display: none">
                                                    <input id="hfNumSolicitudCons" type="hidden"  name="hfNumSolicitudCons" runat="server" />
                                                    <input id="hfTipooSolicitudCons" type="hidden"  name="hfTipooSolicitudCons" runat="server" />
                                                    <input id="hddListaClientesOriginal" type="hidden"  name="hddListaClientesOriginal" runat="server" />
                                                    
                                                    <%--<asp:Button ID="btnGrabasolicitud" Text="Actualizar Solicitud" runat="server" OnClick="GrabaSolicitudConsulta"   />
--%>
                                                    <button id="btnGrabasolicitud" class="btn">Actualizar Solicitud</button>

                                                    <%--<asp:Button ID="btnCancelaConsulta" Text="Regresar" runat="server" OnClick="CancelarSolicitudConsulta" />
--%>
                                                    <button id="btnCancelaConsulta" class="btn">Regresar</button>


                                                </div>
                                            </span>
                                        </div>
                                        </td>
                                    </tr>
                                </table>
                             
                                  
      
          
    </div>
</div>

<div class="row mt5" id="div_CatalogoDesabasto" style="display:none;">
    <div class="col-md-12">

    
                                <%--divCatalogoDesabasto--%>
                                <%--divCatalogoDesabasto--%>
                                <%--divCatalogoDesabasto--%>

                             
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td nowrap="nowrap">Motivo de Desabasto:</td>
                                                    <td><asp:TextBox ID="txtCatCausaDes" runat="server" Width="450px" Enabled="true" MaxLength="50" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="HiddenField2" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;
                                                        
                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="AgregarCausa" ID="btnAgregaCDes" runat="server" OnClick="btnAgregaCDes_Click" />
--%>
                                                        <button ID="btnAgregaCDes"  class="btn">Agregar causa</button>

                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarCausa" ID="btnCancelarCDes" runat="server" OnClick="btnCancelarCDes_Click" />
--%>
                                                        <button ID="btnCancelarCDes" class="btn">Cancealr</button>
                                                                                                                    
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">

                                            <table>
                                                <thead>
                                                    <tr>
                                                        <th>Clave</th>
                                                        <th>Descripcion</th>
                                                        <th></th> <!-- boton desactivar y eliminar  -->
                                                    </tr>
                                                </thead>
                                            </table>


                                         <%--   <telerik:RadGrid ID="rgCausasDesabasto" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCausasDesabasto_PageIndexChanged" OnNeedDataSource="rgCausasDesabasto_NeedDataSource"
                                                OnItemCommand="rgCausasDesabasto_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Causa" HeaderText="Clave" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCausaDes" runat="server" Text='<%# Bind("Id_Causa") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Desc_CausaDesAbasto" HeaderText="Descripción" UniqueName="column2">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Desactivar" ImageUrl="Imagenes/iconos/forbidden.png" 
                                                            Text="Desactivar" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Eliminar" ImageUrl="Imagenes/delete2.png" 
                                                            Text="Eliminar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
--%>

                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>
                            
                                

    </div>
</div>

<div class="row mt5" id="div_MotivosCompraLocal" style="display:none;">
    <div class="col-md-12">

    <%--divMotivosCompraLocal--%>
                                <%--divMotivosCompraLocal--%>
                                <%--divMotivosCompraLocal--%>

                              
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;
                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="ModificarMotivo" ID="btnAgregaCMotivo" runat="server" OnClick="btnAgregaCMotivo_Click" />
--%>
                                                        <img href="Imagenes/iconos/disk_blue.png" /> 
                                                                                                                
                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarMotivo" ID="btnCancelarCMotivo" runat="server" OnClick="btnCancelarCMotivo_Click" />
--%>
                                                        <img href="Imagenes/iconos/undo.png" /> 

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Motivo de Compra Local: </td>
                                                    <td><asp:TextBox ID="txtDescMotivoCL" runat="server" Width="450px" Enabled="true" MaxLength="50" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="hddIdMotivoCL" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Porcentaje AAA: </td>
                                                    <td><asp:TextBox runat="server" Width="100px" Enabled="true" MaxLength="50" ID="txtAAA"   /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:CheckBox ID="chkAplica" Checked="false" runat="server" Text="Aplica" visible="false" /> </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">

                                            <table ID="rgMotivosCL" class="table" >
                                                <thead>
                                                    <tr>
                                                        <th>Id</th>
                                                        <th>Descripción</th>
                                                        <th>Porcentaje</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                            
                                     <%--       <telerik:RadGrid ID="rgMotivosCL" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgMotivosCL_PageIndexChanged" OnNeedDataSource="rgMotivosCL_NeedDataSource"
                                                OnItemCommand="rgMotivosCL_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_MotivoCL" HeaderText="Id" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMotivoCL" runat="server" Text='<%# Bind("Id_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Desc_MotivoCL" HeaderText="Descripción" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescMotivoCL" runat="server" Text='<%# Bind("Desc_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PorcentajeAAA" HeaderText="Porcentaje" UniqueName="column2" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPorcentajeAAA" runat="server" Text='<%# Bind("PorcentajeAAA") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>--%>


                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>
                               


    </div>
</div>

<div class="row mt5" id="div_ConfiguraCorreos" style="display:none;">
    <div class="col-md-12">


                                <%--divConfiguraCorreos--%>
                                <%--divConfiguraCorreos--%>
                                <%--divConfiguraCorreos--%>

                            
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr> 
                                        <td colspan="3" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2" width="700px">
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;

                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/document_edit.png"  CommandName="NuevoMotivo" ID="ImageButton20" runat="server" OnClick="btnNuevoCatCorreo_Click" />
                                                        --%>
                                                        <button class="btn" id="ImageButton20">Editar</button>

                                                       <%-- <asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="ModificarMotivo" ID="ImageButton2" runat="server" OnClick="btnAgregaCatCorreo_Click" />
                                                       --%>
                                                        <button class="btn" id="ImageButton2">Modificar motivo</button>

                                                        <%--<asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarMotivo" ID="ImageButton3" runat="server" OnClick="btnCancelarCatCorreo_Click" />
--%>
                                                        <button class="btn" id="ImageButton3">Cancelar motivo</button>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">CDI: </td>
                                                    <td><telerik:RadComboBox ID="cmbCDIMotivoCL" MaxHeight="300px" runat="server" 
                                                            Width="350px" >
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="hddEmpresa" runat="server" /><asp:HiddenField ID="hddSecuencia" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Motivo de Compra Local</td>
                                                    <td>

                                                    <%--<telerik:RadComboBox ID="cmbMotivoCL" runat="server" Width="350px" MaxHeight="300px"
                                                        LoadingMessage="Cargando" 
                                                        OnClientSelectedIndexChanged="cmbMotivoCL_ClientSelectedIndexChanged">
                                                    </telerik:RadComboBox>
--%>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Autorizador </td>
                                                    <td>
                                                    <%--<telerik:RadTextBox runat="server" Width="350px" Enabled="true" ID="txtAutoriza1" OnBlur="validarEmail" >
                                                            <ClientEvents OnBlur="validarEmail" />
                                                        </telerik:RadTextBox>--%>

                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td nowrap="nowrap"><div id="divAplicacionOculta2" style="display: none" runat="server">
                                                        Aplicacion * </div></td>
                                                    <td>
                                                    <div id="divAplicacionOculta" style="display: none" runat="server">

                                                    <%--<telerik:RadComboBox ID="cmbAplicacionMotCL" runat="server" Width="350px" 
                                                            DropDownWidth="450" LoadingMessage="Cargando...">
                                                        </telerik:RadComboBox>--%>

                                                    </div>    
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                               
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">

                                          
                                           <telerik:RadGrid ID="rgCorreosAutorizadores" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros."                                                 
                                                >
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="columnH1" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Emp" runat="server" Text='<%# Bind("Id_Emp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="columnH2" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Cd" runat="server" Text='<%# Bind("Id_Cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Conf" HeaderText="Id_Conf" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Conf" runat="server" Text='<%# Bind("Id_Conf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_MotivoCL" HeaderText="Id_MotivoCL" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Motivo" runat="server" Text='<%# Bind("Id_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Aplicacion" HeaderText="Id_Aplicacion" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Aplicacion" runat="server" Text='<%# Bind("Id_Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Secuencia" HeaderText="Secuencia" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Desc_MotivoCL" HeaderText="Motivo" UniqueName="column1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc_MotivoCL" runat="server" Text='<%# Bind("Desc_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Aplicacion" HeaderText="Aplicacion" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Concepto" HeaderText="Concepto" UniqueName="column2" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Correo" HeaderText="Correo" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCorreo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>

                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                         </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Eliminar" ImageUrl="Imagenes/delete2.png" 
                                                            Text="Eliminar" UniqueName="DetailColumn3">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>



                                            <%--<telerik:RadGrid ID="rgCorreosAutorizadores" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCorreosAutorizadores_PageIndexChanged" OnNeedDataSource="rgCorreosAutorizadores_NeedDataSource"
                                                OnItemCommand="rgCorreosAutorizadores_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="columnH1" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Emp" runat="server" Text='<%# Bind("Id_Emp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="columnH2" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Cd" runat="server" Text='<%# Bind("Id_Cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Conf" HeaderText="Id_Conf" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Conf" runat="server" Text='<%# Bind("Id_Conf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_MotivoCL" HeaderText="Id_MotivoCL" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Motivo" runat="server" Text='<%# Bind("Id_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Aplicacion" HeaderText="Id_Aplicacion" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Aplicacion" runat="server" Text='<%# Bind("Id_Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Secuencia" HeaderText="Secuencia" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Desc_MotivoCL" HeaderText="Motivo" UniqueName="column1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc_MotivoCL" runat="server" Text='<%# Bind("Desc_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Aplicacion" HeaderText="Aplicacion" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Concepto" HeaderText="Concepto" UniqueName="column2" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Correo" HeaderText="Correo" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCorreo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                         </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Eliminar" ImageUrl="Imagenes/delete2.png" 
                                                            Text="Eliminar" UniqueName="DetailColumn3">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
--%>


                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>

    </div>

        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenRefrescapagina" runat="server" />
</div>

<!-- MODAL Buscar Producto -->
<div class="modal fade" id="modal_HistorialPrecios" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:600px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button5" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H2">
                    Historial de precios
                </h4>
            </div>
            <div class="modal-body">                                    

                <table id="Table1" class="table table-bordered">
                    <thead>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

            </div>
            <div class="modal-footer">
                <button id="Button9" type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                
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
  
  
<!-- Modal Buscar Producto -->
<div class="modal fade" id="modalBuscarProducto" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document" style="width:600px;">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button4" type="button" class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H1">Búsqueda de producto</h4>
            </div>
            <div class="modal-body">                                    
                <table>                    
                    <thead>
                        <tr>
                            <td>Texto</td>                        
                            <td>
                                <input id="tbTextoBuscar"  value="" style="width:250px;" class="form-control" />
                            </td>
                            <td>
                                    <button 
                                        id="btnEjecutarBusqueda" 
                                        type="button" 
                                        class="btn btn-primary" 
                                        style="width:50px;">
                                    Ok
                                </button>
                            </td>                        
                            <td>                                        
                                <i id="spinner_Buscar" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:none; margin-top:3px;"></i>
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>                        
                </table>

                <table id="tblProductoEncontrados" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Código</th>
                            <th>Descripcion</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

               <%-- <input type="hidden" id="Hidden1" value="" /> 
                <input type="hidden" id="Hidden2" value="" /> 
                <input type="hidden" id="Hidden3" value="" /> 
                <input type="hidden" id="Hidden4" value="" /> 
                <input type="hidden" id="Hidden5" value="" /> 
                <table class="center" style="width:100%;">
                <tr>
                <td align="center;">
                    <button id="Button5" type="button" 
                        class="btn btn-primary" 
                        style="width:350px;"
                        onclick="Enviar_Autorizacion_Gerente();" data-dismiss="modal">
                        Solicitar autorizaci&oacute;n para ACYS<br>Gerente 
                    </button>
                </td>
                </tr>
                <tr>
                <td align="center;">
                    <button id="Button7" type="button" 
                        class="btn btn-primary" 
                        style="width:350px; background-color:#fdd25f!important; color:black;"
                        onclick="Enviar_Autorizacion_JefeOp();" data-dismiss="modal">
                        Solicitar autorizaci&oacute;n para CONTROL DE ORDEN<br>Jefe de operacion
                    </button>
                </td>
                </tr>
                </table>   --%> 

            </div>
            <div class="modal-footer">
                <button id="Button8" type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>                
            </div>
        </div>
    </div>
</div>
  
<!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->                          
<!-- ENVIAR AUTORIZACION -->
<div class="modal fade" id="modalEnviarAutorizacion" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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
                <td align="center">

                    <button id="btnEnviar_Autorizacion_Gerente" type="button" 
                        class="btn btn-primary" 
                        style="width:350px;"
                        onclick="Enviar_Autorizacion_Gerente();" data-dismiss="modal">
                        Solicitar autorizaci&oacute;n para ACYS<br>Gerente 
                    </button>

                </td>
                </tr>
                <tr>
                <td align="center">

                    <button id="btnEnviarAutorizacion_ControlOrden" type="button" 
                        class="btn btn-primary" 
                        style="width:350px; background-color:#fdd25f!important; color:black;"
                        onclick="Enviar_Autorizacion_JefeOp();" data-dismiss="modal">
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
        
    <%--<script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/jQuery-Autocomplete-master/dist/jquery.autocomplete.js")%>"> </script>--%>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Ajax.js?v=20220420")%>"> </script>
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/Login.js")%>"> </script>
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Orig.js?v=20220420")%>"> </script>    
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Tools.js?v=20220420")%>"> </script>    
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Entidad.js?v=20220420")%>"> </script>        
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/Cliente_ajax.js?v=20220420")%>"> </script>    
<%--    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Autocompletar.js?v=20220420")%>"></script>    --%>    
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/CL/CL_Main.js?v=20220420")%>"> </script>    
    <script type="text/javascript">
        //console.log("Usuario_Tipo:" + _Usuario_Tipo);           
    </script>

</asp:Content>