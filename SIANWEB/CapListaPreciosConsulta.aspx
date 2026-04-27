<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true" CodeBehind="CapListaPreciosConsulta.aspx.cs" Inherits="SIANWEB.CapListaPreciosConsulta" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>"
        rel="stylesheet" />
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
   
    <style>
        .panel-success > .panel-heading
        {
            color: #F9F9F9 !important;
            background-color: #59b2f1 !important;
        }
        
        .panel-success
        {
            border-color: #d1d1d1 !important;
        }
        
        .caret
        {
            margin-top: 10px !important;
        }
        
        .row
        {
            margin-top: 40px;
            padding: 0 10px;
        }
        
        .clickable
        {
            cursor: pointer;
        }
        
        .panel-heading span
        {
            margin-top: -20px;
            font-size: 15px;
        }
        
        form-control
        {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }
        
        .dropdown-toggle
        {
            height: 34px !important;
        }
        
        .dropdown-toggle-date
        {
            height: 30px !important;
            margin-top: -6px;
            padding-left: 12px;
            padding-right: 10px;
            margin-right: -13px;
        }
         
        .customHeaderStyle {  
            -webkit-tap-highlight-color: rgba(0,0,0,0);
                color: #333;
                font: 12px/16px "segoe ui",arial,sans-serif;
                border-spacing: 0;
                border-collapse: collapse;
                box-sizing: border-box;
                text-decoration: none;
                text-align: center;
                background-color: #f0f0f0;
                border-right: 1px solid #caccd1!important;
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: bottom;
                border: 1px solid #ddd;
                border-bottom-width: 2px;
                border-top: 0;
        }  
        .PMLInvisible {
            display: none;
        }
        .PMLVisible {
            display: table-cell;
        }
        
        
    </style>
    <script type="text/javascript">
        function SetIdColumnVisibility(visible, s) {
            var disp = visible ? 'PMLVisible' : 'PMLInvisible';
            if (s.name == 'CPH_chkPaaa_ant') {
                $('.id-column').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-column').addClass(disp);
            }
            if (s.name == 'CPH_ckplista_ant') {
                $('.id-plista_ant').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-plista_ant').addClass(disp);
            }
            if (s.name == 'CPH_chkpaaa_futuro') {
                $('.id-paaa_futuro').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-paaa_futuro').addClass(disp);
            }
            if (s.name == 'CPH_chkplista_futuro') {
                $('.id-plista_futuro').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-plista_futuro').addClass(disp);
            }
            if (s.name == 'CPH_chkplaneacion_abasto') {
                $('.id-planeacion_abasto').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-planeacion_abasto').addClass(disp);
            }
            if (s.name == 'CPH_chkporc_paaa') {
                $('.id-porc_paaa').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-porc_paaa').addClass(disp);
            }
            if (s.name == 'CPH_chkporc_plista') {
                $('.id-porc_plista').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-porc_plista').addClass(disp);
            }
            if (s.name == 'CPH_chkfecha_pfuturo') {
                $('.id-fecha_pfuturo').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-fecha_pfuturo').addClass(disp);
            }
            if (s.name == 'CPH_chkfecha_pactual') {
                $('.id-fecha_pactual').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-fecha_pactual').addClass(disp);
            }
            if (s.name == 'CPH_chkresponsable') {
                $('.id-responsable').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-responsable').addClass(disp);
            }
            if (s.name == 'CPH_chkmargenred') {
                $('.id-margenred').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-margenred').addClass(disp);
            }
            if (s.name == 'CPH_chklistaprecios') {
                $('.id-listaprecios').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-listaprecios').addClass(disp);
            }
            if (s.name == 'CPH_chkpresentacion') {
                $('.id-presentacion').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-presentacion').addClass(disp);
            }
            if (s.name == 'CPH_chkunidadmedida') {
                $('.id-unidadmedida').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-unidadmedida').addClass(disp);
            }
            if (s.name == 'CPH_chkporc_paaafuturo') {
                $('.id-porc_paaafuturo').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-porc_paaafuturo').addClass(disp);
            }
            if (s.name == 'CPH_chkporc_plistafuturo') {
                $('.id-porc_plistafuturo').removeClass('PMLVisible').removeClass('PMLInvisible');
                $('.id-porc_plistafuturo').addClass(disp);
            }
        }
        function onCheckedChanged(s, e) {
            SetIdColumnVisibility(s.GetChecked(), s);
        }
        function onEndCallback(s, e) {
            SetIdColumnVisibility(cb.GetChecked());
        }
        function onValuechanged(s, e) {
            //SetIdColumnVisibility(s.GetChecked(), s);
            alert('hola');
            if (s.name == 'CPH_cmbSubFamilia') {
                alert(this.value);
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;
                alert(valueSelected);
            }
        }
        function fTextChanged(s, e) {
            alert("cambiar el texto");
            callback.PerformCallback();
        }
//  $(document).ready(function () {
//                $('#cmbSubFamilia').on('change', function (e) {
//                    alert('hola33');
//                    var optionSelected = $("option:selected", this);
//                    var valueSelected = this.value;
//                    alert(valueSelected);
//                });
//            });
    </script>
    <div class="container-fluid">
        <div class="row" style="margin-top: 15px;">
                <div class="panel-body">
                
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-md-1">
                         <dx:BootstrapCheckBox ID="chkPaaa_ant" runat="server" AutoPostBack="false" Text=" PAAA anterior" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" />
                        </dx:BootstrapCheckBox>

                        </div>
                        <div class="col-md-1">
                        <dx:BootstrapCheckBox ID="ckplista_ant" runat="server" AutoPostBack="false" Text=" PLISTA anteriorr" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" />
                        </dx:BootstrapCheckBox>

                           <%-- <dx:ASPxCheckBox ID="ckplista_ant" Text="PLISTA anterior" runat="server">
                            </dx:ASPxCheckBox>--%>
                        </div>
                        <div class="col-md-1">
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox2" Text="PAAA futuro" runat="server"></dx:ASPxCheckBox>--%>
                             <dx:BootstrapCheckBox ID="chkpaaa_futuro" runat="server" AutoPostBack="false" Text="PAAA futuro" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" />
                        </dx:BootstrapCheckBox>
                        </div>
                        <div class="col-md-1">
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox3" Text="PLISTA futuro" runat="server"> </dx:ASPxCheckBox>--%>
                            <dx:BootstrapCheckBox ID="chkplista_futuro" runat="server" AutoPostBack="false" Text="PLISTA futuro" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
                        <div class="col-md-1  col-sm-12">
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox4" Text="Planeación de abasto" runat="server"></dx:ASPxCheckBox>--%>
                            <dx:BootstrapCheckBox ID="chkplaneacion_abasto" runat="server" AutoPostBack="false" Text="Planeación de abasto" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" />  </dx:BootstrapCheckBox>
                        </div>
                        <div class="col-md-1 col-sm-10">
                              <dx:BootstrapCheckBox ID="chkmargenred" runat="server" AutoPostBack="false" Text="Margén de red" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
                         
                     <div class="col-md-1   col-sm-10">
                              <dx:BootstrapCheckBox ID="chkunidadmedida" runat="server" AutoPostBack="false" Text="Unidad Medida" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
                         <div class="col-md-1   col-sm-10">
                              <dx:BootstrapCheckBox ID="chkporc_paaafuturo" runat="server" AutoPostBack="false" Text="% Var. PAAA Futuro" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>


                         <div class="col-md-1" >
                            <button id="Button2" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                                title="Información" runat="server" onserverclick="btnInformacion_ServerClick">
                                <i class="fa fa-question-circle"></i>&nbsp;&nbsp;Info&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </button>
                        </div>

                        <div class="col-md-1" >
                               <%--No se configura en SIANWEB solo en SIANCENTRAL
                                   <button id="Button3" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                                title="Configurar" runat="server" onserverclick="btnHabilitar_ServerClick">
                               
                                <i class="fa fa-pencil-square-o"></i>&nbsp;&nbsp;
                            </button>--%>
                            
                         
                                <button id="Button4" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                                title="Exportar" runat="server" onserverclick="btnexportar_Click">
                                <i class="fa fa-download"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </button>

                            

                             

                        </div>

                        

                    </div>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-md-1 col-sm-12">
                            <dx:BootstrapCheckBox ID="chkporc_paaa" runat="server" AutoPostBack="false" Text=" % Var. PAAA" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>  

                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox5" Text="% Var. PAAA" runat="server">
                            </dx:ASPxCheckBox>--%>
                        </div>
                        <div class="col-md-1  col-sm-12">
                        <dx:BootstrapCheckBox ID="chkporc_plista" runat="server" AutoPostBack="false" Text=" % Var PLISTA" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" /></dx:BootstrapCheckBox>
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox6" Text="% Var PLISTA" runat="server">
                            </dx:ASPxCheckBox>--%>
                        </div>
                        <div class="col-md-1  col-sm-12">
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox7" Text="Fecha inicio precio futuro" runat="server"> </dx:ASPxCheckBox>--%>
                              <dx:BootstrapCheckBox ID="chkfecha_pfuturo" runat="server" AutoPostBack="false" Text="Fecha inicio precio futuro" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
                        <div class="col-md-1   col-sm-12">
                            <%--<dx:ASPxCheckBox ID="ASPxCheckBox8" Text="Fecha Inicio precio actual" runat="server"></dx:ASPxCheckBox>--%>
                               <dx:BootstrapCheckBox ID="chkfecha_pactual" runat="server" AutoPostBack="false" Text="Fecha Inicio precio actual" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
                        <div class="col-md-1">
                              <dx:BootstrapCheckBox ID="chkresponsable" runat="server" AutoPostBack="false" Text="Responsable del producto" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>
               
                        <div class="col-md-1">
                              <dx:BootstrapCheckBox ID="chklistaprecios" runat="server" AutoPostBack="false" Text="Lista Precios" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>

                       <div class="col-md-1">
                              <dx:BootstrapCheckBox ID="chkpresentacion" runat="server" AutoPostBack="false" Text="Presentación" Checked="true" ClientInstanceName="cb">
                              <ClientSideEvents CheckedChanged="onCheckedChanged" /> </dx:BootstrapCheckBox>
                        </div>

                        <div class="col-md-1  col-sm-12">
                        <dx:BootstrapCheckBox ID="chkporc_plistafuturo" runat="server" AutoPostBack="false" Text=" % Var PLISTA Futuro" Checked="true" ClientInstanceName="cb">
                            <ClientSideEvents CheckedChanged="onCheckedChanged" /></dx:BootstrapCheckBox>
                        </div>
                       

                         <div class="col-md-1">
                            <button id="Button1" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                                title="Información" runat="server" onserverclick="btnLimpiar_ServerClick">
                               <i class="fa fa-eraser" ></i>&nbsp;Limpiar Filtros
                            </button>
                        </div>
                        <%--no se requiere la opción de buscar --%>
                         <%-- <div class="col-md-1">
                            <button id="btnConsultar" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                                title="Consultar" runat="server" onserverclick="btnConsultar_ServerClick">
                                <i class="fa fa-search"></i>&nbsp;Buscar&nbsp;&nbsp;&nbsp;&nbsp;
                            </button>
                        </div>--%>
 </div>
                    <div class="row">
                         
                        <div class="col-md-4 col-sm-12">

                                   <div class="col-2 col-sm-4">
                                        <label> Aplicación</label>
                    </div>
                                    <div class="col-10 col-sm-8">
                                      <dx:BootstrapComboBox runat="server" ID="cmbAplicacion" class="form-control w400"
                                            EnableCallbackMode="true" CallbackPageSize="80"   AutoPostBack="true" OnValueChanged="cmbAplicacion_Click">
                                        </dx:BootstrapComboBox>
                        </div>
                               
                        </div>


                    
                          <div class="col-md-4 col-sm-12">
                           
                                   <div class="col-2 col-sm-4">
                                        <label> Sub Familia </label>
                                  </div>
                                    <div class="col-8 col-sm-8">
                                       <dx:BootstrapComboBox runat="server" ID="cmbSubFamilia"   
                                            EnableCallbackMode="true"     AutoPostBack="true" OnValueChanged = "cmbReload_Click" >
                                           <%--  <ClientSideEvents ValueChanged ="onValuechanged" />--%>
                                        </dx:BootstrapComboBox>
                        </div>
                               
                        </div>


                         <div class="col-md-4 col-sm-12">
                            
                                   <div class="col-2 col-sm-4">
                                        <label>Tipo Producto</label>
                                  </div>
                                    <div class="col-8 col-sm-8">
                                        <dx:BootstrapComboBox runat="server" ID="cmbTipoProducto" EnableCallbackMode="true"  AutoPostBack="true" OnValueChanged="cmbReload_Click"
                                            CallbackPageSize="25">
                                        </dx:BootstrapComboBox>
                        </div>
                               
                        </div>
                        </div>
                            <%-- Agregue un remglon para que sean 3 filtros arriba y 3 abajo--%>
                         <div class="row">

                         <div class="col-md-4 col-sm-12">
                           
                                   <div class="col-2 col-sm-4">
                                        <label>Lista de Precios</label>
                                  </div>
                                    <div class="col-10 col-sm-8">
                                        <dx:BootstrapComboBox runat="server" ID="cmbLista" EnableCallbackMode="true"  AutoPostBack="true" OnValueChanged="cmbReload_Click"
                                            CallbackPageSize="25">
                                        </dx:BootstrapComboBox>
                        </div>

                        </div>

                        <div class="col-md-4 col-sm-12">
                                   <div class="col-2 col-sm-4">
                                        <label>Proveedor</label>
                                  </div>
                                    <div class="col-8 col-sm-8">
                                        <dx:BootstrapComboBox runat="server" ID="cmbProveedor" EnableCallbackMode="true"  AutoPostBack="true" OnValueChanged = "cmbReload_Click" 
                                            CallbackPageSize="25">
                                        </dx:BootstrapComboBox>
                        </div>
                      
                             
                        </div>
                      
                        <div class="col-md-4 col-sm-12">
                            <div class="form-group">
                                 <div class="col-2 col-sm-4">
                                         
                                  </div>
                                     <div class="col-8 col-sm-8">
                                <div class="input-group">
                                    <span class="input-group-addon" id="Span1"><i class="fa fa-filter"></i></span>
                                    <dx:BootstrapTextBox ID="Text1" runat="server" class="form-control" AutoPostBack="true"  ontextchanged="fTextChanged" 
                                        placeholder=" "  >
                                          <%-- <ClientSideEvents TextChanged="fTextChanged" />  --%>
                                    </dx:BootstrapTextBox>
                                </div> </div>
                                </div>
                               
                            </div>

                           
                    </div>
                        
                      


                          <%--  <div class="col-md-1 col-sm-2 mt5">                   
            <span style="position:absolute;" >                    
                    <i id="spinner_Indice" class="fa fa-spinner fa-pulse fa-2x fa-fw" style="display:block; margin-top:3px;"></i>
            </span>        
        </div>--%>
                        <%--  <div class="col-md-1 col-sm-12">
            <button id="btnTerr_DescargarReporte" type="button" class="btn btn-default btn-sm w100" style="margin-top:8px!important;">
                <i class="fa fa-cloud-download" aria-hidden="true"></i>&nbsp;Reporte
            </button>          
        </div>--%>

                    </div>
                    <asp:UpdatePanel runat="server" ID="updpanel2">
                        <ContentTemplate>
                            <div id="Div1" class="col-md-12" runat="server" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="grdServicio" ClientInstanceName="grid" runat="server" Width="100%"
                                    AutoGenerateColumns="False" style = "color: #333; font: 12px/16px 'segoe ui',arial,sans-serif; border-spacing: 0;">
                                    <CssClasses HeaderRow="customHeaderStyle"  />  
                                    <SettingsBehavior EnableRowHotTrack="true" />
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="APLICACION" Caption="Aplicacion" Width="300px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="SUBFAMILIA" Caption="Sub Familia" Width="250px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="TIPOPRODUCTO" Caption="Tipo Producto" Width="100px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="NOMBREPROVEEDOR" Caption="Nombre Proveedor"
                                            Width="200px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Center" Caption="Código Key"
                                            Width="50px" />
                                         <%--23 junio requerimiento lista precios --%>
                                        <dx:BootstrapGridViewTextColumn FieldName="Descripcion" Caption="Descripcion" Width="200px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="NODEARTDEPROVEEDOR" Caption="Codigo Proveedor" Width="200px" />
                                        <%--23 junio requerimiento lista precios --%>
                                        <dx:BootstrapGridViewTextColumn FieldName="UnidaddeVenta" Caption="Unidad de Venta al Cliente"  Width="50px" CssClasses-DataCell= "id-unidadmedida" CssClasses-HeaderCell = "id-unidadmedida"  > </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Presentacion" Caption="Presentación de unidad de venta"  Width="50px" CssClasses-DataCell= "id-presentacion" CssClasses-HeaderCell = "id-presentacion"  > </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PAAAACTUAL" Caption="PAAA ACTUAL" Width="50px"  > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PLISTAACTUAL" Caption="PLISTA ACTUAL" Width="50px" > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PAAAAnterior" Caption="PAAA ANTERIOR"  Width="50px" CssClasses-DataCell= "id-column" CssClasses-HeaderCell = "id-column"  > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PLISTAANTERIOR" Caption="PLISTA ANTERIOR"  Width="50px" CssClasses-DataCell= "id-plista_ant" CssClasses-HeaderCell = "id-plista_ant"> <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PAAAFUTURA" Caption="PAAA FUTURO" Width="50px" CssClasses-DataCell= "id-paaa_futuro" CssClasses-HeaderCell = "id-paaa_futuro"   > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PLISTAFUTURA" Caption="PLISTA FUTURO" Width="50px" CssClasses-DataCell= "id-plista_futuro" CssClasses-HeaderCell = "id-plista_futuro"   > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PVariacionPAAA"   Caption="% Var. PAAA"  Width="50px" CssClasses-DataCell= "id-porc_paaa" CssClasses-HeaderCell = "id-porc_paaa"  ><PropertiesTextEdit DisplayFormatString="p0" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PVariacionPLISTA"   Caption="% Var. PLISTA" Width="50px" CssClasses-DataCell= "id-porc_plista" CssClasses-HeaderCell = "id-porc_plista" > <PropertiesTextEdit DisplayFormatString="p0" /> </dx:BootstrapGridViewTextColumn>     
                                         <%--23 junio requerimiento lista precios --%>
                                        <dx:BootstrapGridViewTextColumn FieldName="PVariacionPAAAFUTURO"   Caption="% Var. PAAA Futuro"  Width="50px" CssClasses-DataCell= "id-porc_paaafuturo" CssClasses-HeaderCell = "id-porc_paaafuturo"  ><PropertiesTextEdit DisplayFormatString="p0" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PVariacionPLISTAFUTURO"   Caption="% Var. PLISTA Futuro" Width="50px" CssClasses-DataCell= "id-porc_plistafuturo" CssClasses-HeaderCell = "id-porc_plistafuturo" > <PropertiesTextEdit DisplayFormatString="p0" /> </dx:BootstrapGridViewTextColumn>     
                                   
                                        <dx:BootstrapGridViewTextColumn FieldName="margenred"   Caption="Margen de red" Width="50px" CssClasses-DataCell= "id-margenred" CssClasses-HeaderCell = "id-margenred" > <PropertiesTextEdit DisplayFormatString="p0" /> </dx:BootstrapGridViewTextColumn>     
                                        <dx:BootstrapGridViewCheckColumn Caption="Tiene Precio Futuro" FieldName="TIENEPRECIOFUTURO"  Width="50px" />
                                        <dx:BootstrapGridViewDateColumn FieldName="FECHAINICIOVIG" Caption="Fecha Inicio Actual" Width="100px" CssClasses-DataCell= "id-fecha_pactual" CssClasses-HeaderCell = "id-fecha_pactual" />
                                        <dx:BootstrapGridViewDateColumn FieldName="FECHAINICIOVIGFUT" Caption="Fecha inicio Precio Futuro"  Width="100px" CssClasses-DataCell= "id-fecha_pfuturo" CssClasses-HeaderCell = "id-fecha_pfuturo" />
                                        <dx:BootstrapGridViewTextColumn FieldName="LISTADEPRECIOS" Caption="Lista de Precios" Width="60px" CssClasses-DataCell= "id-listaprecios" CssClasses-HeaderCell = "id-listaprecios"  /> 
                                        
                                        <dx:BootstrapGridViewTextColumn FieldName="RESPONSABLE" Caption="Responsable" Width="200px" CssClasses-DataCell= "id-responsable" CssClasses-HeaderCell = "id-responsable"  /> 
                                        <dx:BootstrapGridViewTextColumn FieldName="PLANEACION" Caption="Planeacion" Width="50px" CssClasses-DataCell= "id-planeacion_abasto" CssClasses-HeaderCell = "id-planeacion_abasto"  /> 
                                        <%-- 
                                         lista.PVariacionPAAA = Item.PVariacionPAAA;
                lista.PVariacionPLISTA = Item.PVariacionPLISTA;
                                        
                                         <PropertiesSpinEdit DisplayFormatString="p0" /> 
                                <dx:BootstrapGridViewTextColumn FieldName="TIENEPRECIOFUTURO" Caption="Tiene Precio Futuro" Width="50px" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "0" ? false : true  %>'  >
                                  </dx:BootstrapGridViewTextColumn>
                               <span class="badge badge-success" style="margin-top:4px; background-color:#2dde98;" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "1" ? true: false %>' >Si</span>
                                <span class="badge badge-secondary" style="margin-top:4px;" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "1" ? false : true  %>' >No</span>
    
                               <dx:BootstrapGridViewTextColumn FieldName="xxxx" Caption="Planeación Abasto" Width="50px" /> --%>
                                        <%-- lista.PVariacionPAAA = Item.PVariacionPAAA;
                lista.PVariacionPLISTA = Item.PVariacionPLISTA;
                lista.FECHAINICIOVIG = Item.FECHAINICIOVIG;
                lista.FECHAFINVIG = Item.FECHAFINVIG;--%>
                                    </Columns>
                                     <SettingsPager  PageSize="50" />
                                </dx:BootstrapGridView>

                                 <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdServicio">
                                                                        <%--OnRenderBrick = " gridExporter_RenderBrick " >--%>
                                 </dx:ASPxGridViewExporter>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdServicio" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
        </div>
  
</asp:Content>