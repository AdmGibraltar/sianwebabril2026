<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master" AutoEventWireup="true" CodeBehind="CapNps_Reportes.aspx.cs" Inherits="SIANWEB.CapNps_Reportes" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-colorschemes.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <!-- 
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    -->
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.PieceLabel.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    <script src="js/NPS/CapNps_Reportes.js?ref=v1.1.9"></script>

    <style type="text/css">
        /**/
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .dropdown-toggle-date {
            height: 30px !important;
            margin-top: -6px;
            padding-left: 12px;
            padding-right: 10px;
            margin-right: -13px;
        }

        .panel-success > .panel-heading {
            color: #F9F9F9 !important;
            background-color: #59b2f1 !important;
        }

        .panel-success {
            border-color: #d1d1d1 !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        .row {
            margin-top: 40px;
            padding: 0 10px;
        }

        .clickable {
            cursor: pointer;
        }

         .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }

        /*
 
            */
        .elm-w160 {
            width: 160px;
        }


        .elm-w180 {
            width: 180px;
        }

        .elm-w200 {
            width: 200px;
        }

        .elm-w250 {
            width: 250px;
        }

        .text-left {
            text-align: left !important;
        }

        .table-sinborde tbody tr td, .table-sinborde tr td {
            border-top: none !important;
        }
        .table-text-center tr th, .table-text-center tr td{
            text-align:center;
        }
        
        .list-group-item {
            padding: 5px 15px !important;
        }
    </style>
    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Favor de Esperar..."
                        ToolTip="Favor de Esperar..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="dvLoading">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <img id="imgUpdateProgress" src="../../images/load.gif" alt="Favor de Esperar..." style="position: fixed; top: 45%; left: 40%;" />
        </div>

        </div>
        <div class="col-md-12">
            <h2 style="font-weight: bolder">NPS Reportes</h2>
        </div>
           
        <ul class="nav nav-tabs" id="tabPage">
            <li class="active">
                <a href="#tabDatos" data-toggle="tab" id="tabIndicadores" >
                <h5>Generar Indicadores</h5>
                </a>
            </li>
            <li>
                <a href="#tabInfo" data-toggle="tab" id="tabDescarga">
                    <h5>Descargar Reporte Global</h5>
                </a>                    
            </li>
        </ul>
        <div class="tab-content">
            <%--TAB Generar Indicadores --%>
            <div class="tab-pane fade in active" id="tabDatos">

                <%-- Indicador de Trazabilidad --%>

                <div class="row" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Indicador de Trazabilidad</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed">
                            <asp:UpdatePanel runat="server" ID="UPdBusacarinfo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: 5px;">
                                         <div class="col-md-3" style="padding: 0px 7px !important;">
                                            <dx:BootstrapListBox runat="server" ID="dxLstBoxEstatusTrazabilidad" ClientInstanceName="dxLstBoxEstatusTrazabilidad"  SelectionMode="CheckColumn" EnableSelectAll="true" SelectAllText="Todos los Estatus" >  
                                                <Items> 
                                                </Items>
                                            </dx:BootstrapListBox>
                                        </div> 
                                        <div class="col-md-3"  style="padding: 0px 7px !important;">
                                            <dx:BootstrapListBox runat="server" ID="dxLstBoxUENTrazabilidad" ClientInstanceName="dxLstBoxUENTrazabilidad"  SelectionMode="CheckColumn" EnableSelectAll="true" SelectAllText="Todos los UEN">  
                                                <Items>
                                                </Items>
                                            </dx:BootstrapListBox>
                                        </div> 
                                        <div class="col-md-6"  style="padding: 0px 7px !important;">
                                            <div class="row" style="margin-top: 5px;">
                                            <div class="col-md-12">
                                                Rik
                                                <dx:BootstrapComboBox ID="dxCmbRikTrazabilidad" ClientInstanceName="dxCmbRikTrazabilidad" runat="server">
                                                </dx:BootstrapComboBox>
                                             </div>                                            
                                                  
                                             </div> 
                                            <div class="row" style="margin-top: 5px;">

                                                 <div class="col-md-6">
                                           
                                               
                                                        <asp:Label ID="Label25" runat="server" Text="Fecha Inicial" />
                                               
                                                        <dx:aspxdateedit runat="server" clientinstancename="fechaIniTrazabilidad" cssclass="form-control" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtFechaIniTrazabilidad" pickertype="Months">
                                                            <calendarproperties>
                                                                <fastnavproperties initialview="Years" maxview="Years" />
                                                            </calendarproperties>
                                                        </dx:aspxdateedit>
                                                
                                                 </div>
                                       
                                                <div class="col-md-6">
                                         
                                               
                                                            <asp:Label ID="Label26" runat="server" Text="Fecha Final" />
                                             
                                                            <dx:aspxdateedit runat="server" clientinstancename="fechaFinTrazabilidad" cssclass="form-control" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtFechaFinTrazabilidad" pickertype="Months">
                                                                <calendarproperties>
                                                                    <fastnavproperties initialview="Years" maxview="Years" />
                                                                </calendarproperties>
                                                            </dx:aspxdateedit>
                                          
                                           
                                                </div>
                                            </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="col-md-12 text-right">
                                            <br />
                                            <button id="btnTrazabilidad" type="button" runat="server" class="btn btn-primary" onclick="ConsultaTrazabilidad();">
                                                <span>Generar Estadistica</span>
                                            </button>
                                        </div>
                                        </div>
                                         </div>
                                       
                                    </div>
                                    
                                    <div class="col-md-8" style="margin-top: 5px;">
                                        <canvas id="chartTrazabilidad" style="width: 700px; height: 250px; display: none;"></canvas>
                                    </div>
                                    <div class="col-md-8" style="margin-top: 5px;">
                                          <div class="col-md-12">
                                                <button id="btnExportarTrazabilidadGlobal" type="button" class="btn btn-success" onclick="JsCapNps_Reporte.DescargarTrazabilidadGlobal()"  style="display: none;">
                                                    <i class="fa fa-download"></i>
                                                </button>
                                          </div>                                        
                                        <table id="tblTrazabilidad" class="table table-striped table-bordered table-text-center" style="display: none;">
                                            <thead>
                                                <tr class="bg-primary">
                                                    <th>Trazabilidad</th>
                                                    <th colspan="2">Asignado</th>
                                                    <th colspan="2">En desarrollo</th>
                                                    <th colspan="2">Atendido</th>
                                                    <th colspan="2">Reenviado</th>
                                                    <th colspan="2">Cerrado</th>
                                                </tr>
                                                <tr class="bg-primary">
                                                    <th></th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Global</td>
                                                    <td><span id="spanStatus1Valor">0</span></td>
                                                    <td><span id="spanStatus1Porcentaje">0 %</span></td>
                                                    <td><span id="spanStatus2Valor">0</span></td>
                                                    <td><span id="spanStatus2Porcentaje">0 %</span></td>
                                                    <td><span id="spanStatus3Valor">0</span></td>
                                                    <td><span id="spanStatus3Porcentaje">0 %</span></td>
                                                    <td><span id="spanStatus5Valor">0</span></td>
                                                    <td><span id="spanStatus5Porcentaje">0 %</span></td>
                                                    <td><span id="spanStatus4Valor">0</span></td>
                                                    <td><span id="spanStatus4Porcentaje">0 %</span></td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <a href="javascript: void(0);" onclick="ShowTrazabilidadCliente();" id="iconTrazabilidad" style="display: none;">
                                            <i class="fa fa-plus-square" id="iconTrazabilidadCliente"></i> <span> Ver detalle por cliente</span>
                                        </a>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div id="contenedorTblTrazabilidadCliente" style="display: none;">
                                            <div class="col-md-12">
                                                <button id="btnExportarTrazabilidadCliente" type="button" class="btn btn-success" onclick="JsCapNps_Reporte.DescargarTrazabilidadCliente()"  style="display: none;">
                                                    <i class="fa fa-download"></i>
                                                </button>
                                            </div>
                                            <table id="tblTrazabilidadCliente" class="table table-striped table-bordered table-text-center">
                                            <thead>
                                                <tr class="bg-primary">
                                                    <th>Cliente</th>
                                                    <th colspan="2">Asignado</th>
                                                    <th colspan="2">En desarrollo</th>
                                                    <th colspan="2">Atendido</th>
                                                    <th colspan="2">Reenviado</th>
                                                    <th colspan="2">Cerrado</th>
                                                </tr>
                                                <tr class="bg-primary">
                                                    <th></th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                    <th>#</th>
                                                    <th>%</th>                                                    
                                                    <th>#</th>
                                                    <th>%</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>

                                    <asp:AsyncPostBackTrigger ControlID="btnTrazabilidad" />

                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
                <%-- Indicador General NPS--%>

                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success active" >
                        <div class="panel-heading">
                            <h3 class="panel-title">Indicador General NPS
                            </h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" id="panelGeneral">
                            <asp:UpdatePanel runat="server" ID="updObservarTot" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -5px">
                                        
                                        <div class="col-md-3"  style="padding: 0px 7px !important;">
                                            <dx:BootstrapListBox runat="server" ID="dxLstBoxUENGeneral" ClientInstanceName="dxLstBoxUENGeneral"  SelectionMode="CheckColumn" EnableSelectAll="true" SelectAllText="Todos los UEN">  
                                                <Items>
                                                </Items>
                                            </dx:BootstrapListBox>
                                        </div> 
                                        <div class="col-md-6">
                                        <div class="col-md-6">
                                            Fecha Inicial
                                            <dx:aspxdateedit runat="server" clientinstancename="AnioIniIndicadorGeneral" cssclass="form-control" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtAnioIniIndicadorGeneral" pickertype="Months">
                                                <calendarproperties>
                                                    <fastnavproperties initialview="Years" maxview="Years" />
                                                </calendarproperties>
                                            </dx:aspxdateedit>
                                                
                                        </div>
                                        <div class="col-md-6">
                                            Fecha Final
                                            <dx:aspxdateedit runat="server" clientinstancename="AnioFinIndicadorGeneral" cssclass="form-control" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtAnioFinIndicadorGeneral" pickertype="Months">
                                                <calendarproperties>
                                                    <fastnavproperties initialview="Years" maxview="Years" />
                                                </calendarproperties>
                                            </dx:aspxdateedit>
                                        </div>
                                            <div class="col-md-12">
                                                 <br />
                                                 Rik
                                                <dx:BootstrapComboBox ID="dxCmbRikIndicadorGeneral" ClientInstanceName="dxCmbRikIndicadorGeneral" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                     
                                       
                                        <div class="col-md-3 text-right">
                                            <button id="btnIndicadorGeneral" type="button" class="btn btn-primary" title="Generar estadistica"
                                                runat="server" onclick="ConsultaIndicadorGeneral()">
                                                <span>Generar Estadistica</span>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">
                                        <div class="col-md-8">
                                            <canvas id="ChartIndicadorNps" style="width: 700px; height: 250px; display: none;"></canvas>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">
                                          <div class="col-md-12">
                                                <button id="btnExportarNps" type="button" class="btn btn-success" onclick="JsCapNps_Reporte.DescargarIndicadorNps()"  style="display: none;">
                                                    <i class="fa fa-download"></i>
                                                </button>
                                            </div>
                                        <div class="col-md-8">
                                            <table id="tblGeneral" class="table table-striped table-bordered table-text-center" style="display: none;">
                                                <thead>
                                                    <tr class="bg-primary">
                                                        <th>Total Global</th>
                                                        <th>Promotor</th>
                                                        <th>Pasivo</th>
                                                        <th>Detractor</th>
                                                        <th><b><ins>NPS</ins></b></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><span id="spanNpsTotal"></span></td>
                                                        <td><span id="spanValorPromotor"></span></td>
                                                        <td><span id="spanValorPasivo"></span></td>
                                                        <td><span id="spanValorRetractor"></span></td>
                                                        <td rowspan="2" ><h3><b><span id="spanPorcentajeNps"></span></b></h3></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span id="spanNpsPorcentaje"></span></td>
                                                        <td><span id="spanPorcentajePromotor"></span></td>
                                                        <td><span id="spanPorcentajePasivo"></span></td>
                                                        <td><span id="spanPorcentajeRetractor"></span></td>
                                                    </tr> 
                                                </tbody>
                                            </table>
                                        </div>
                                       
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnIndicadorGeneral" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


                <%-- Indicador de Tasa de Conversión --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success active" >
                        <div class="panel-heading">
                            <h3 class="panel-title">Indicador de Tasa de Conversión</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" id="panelConversion">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: 5px">
                                         <div class="col-md-3"  style="padding: 0px 7px !important;">
                                            <dx:BootstrapListBox runat="server" ID="dxLstBoxUENConversion" ClientInstanceName="dxLstBoxUENConversion"  SelectionMode="CheckColumn" EnableSelectAll="true" SelectAllText="Todos los UEN">  
                                                <Items>
                                                </Items>
                                            </dx:BootstrapListBox>
                                        </div> 
                                        <div class="col-md-6">
                                            <div class="col-md-6">
                                                Fecha Inicial
                                                <dx:aspxdateedit runat="server" cssclass="form-control" clientinstancename="FechaIniConversion" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtFechaIniConversion" pickertype="Months">
                                                    <calendarproperties>
                                                        <fastnavproperties initialview="Years" maxview="Years" />
                                                    </calendarproperties>
                                                </dx:aspxdateedit>
                                            </div>
                                            <div class="col-md-6">
                                                Fecha Final
                                                <dx:aspxdateedit runat="server" cssclass="form-control" clientinstancename="FechaFinConversion" buttonstyle-cssclass=" btn  dropdown-toggle-date" id="txtFechaFinConversion" pickertype="Months">
                                                    <calendarproperties>
                                                        <fastnavproperties initialview="Years" maxview="Years" />
                                                    </calendarproperties>
                                                </dx:aspxdateedit>
                                            </div>
                                             <div class="col-md-12">
                                                 <br />
                                                 Rik
                                                 <dx:BootstrapComboBox ID="dxCmbRikConversion" ClientInstanceName="dxCmbRikConversion" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                          </div>
                                        
                                        <div class="col-md-3 text-right">
                                            <button id="btnBucaConversion" type="button" class="btn btn-primary" title="Generar Estadistica" onclick="ConsultaConversion();">
                                                <span>Generar Estadistica</span>
                                            </button>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-12">
                                                <button id="btnExportarConversion" type="button" class="btn btn-success" onclick="JsCapNps_Reporte.DescargarConversion()"  style="display: none;">
                                                    <i class="fa fa-download"></i>
                                                </button>
                                          </div>
                                        <table id="tblConversion" class="table table-striped table-bordered table-text-center" style="display: none;">
                                            <thead>
                                                <tr class="bg-primary">
                                                    <th></th>
                                                    <th colspan="2">1era Entrevista</th>
                                                    <th colspan="2">2da Entrevista</th>
                                                    <th colspan="2">Tasa de Conversión</th>
                                                </tr>
                                                <tr class="bg-primary">
                                                    <th><span id="cdiSucursal"></span></th>
                                                    <th>#</th>
                                                    <th></th>
                                                    <th>#</th>
                                                    <th></th>
                                                    <th>#</th>
                                                    <th>%</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                            <br />
                                        <br />
                                        <br />
                        </div>
                    </div>
                </div>
                <%-- Acceso Dashboard NPS --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Acceso Dashboard NPS</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -5px">
                                         <div class="col-md-12" >
                                            <a href="https://app.powerbi.com/view?r=eyJrIjoiMGYwZTM2NmMtNGFiNi00NzExLTkzYzYtYTE2NTVjYjAwYjJmIiwidCI6IjJkODcwNTVkLTBlYmItNDdmNy05OTU5LWU3OWYwYzBhMzNkNyJ9" target="_blank"> Abrir Dashboard NPS </a>
                                         </div>
                                    </div>
                                   
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <%--TAB Descargar informacion --%>
            <div class="tab-pane fade" id="tabInfo">
                <div class="row" style="margin-top: 15px">
                    <div class="panel panel-success active">
                        <div class="panel-heading">
                            <h3 class="panel-title">Descargar Reporte Global
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="upddescargar">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-3"  style="padding: 0px 7px !important;">
                                            <dx:BootstrapListBox runat="server" ID="dxLstBoxUENGlobal" ClientInstanceName="dxLstBoxUENGlobal"  SelectionMode="CheckColumn" EnableSelectAll="true" SelectAllText="Todos los UEN">  
                                                <Items>
                                                   
                                                </Items>
                                            </dx:BootstrapListBox>
                                        </div> 
                                        <div class="col-md-3">
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                Rik
                                                <dx:BootstrapComboBox ID="dxCmbRikGlobal" ClientInstanceName="dxCmbRikGlobal" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                NPS
                                                <select id="cmbNpsFiltro" class="form-control">
                                                </select>
                                            </div>
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                Estatus
                                                <select id="cmbEstatusFiltro" class="form-control">
                                                </select>
                                            </div>
                                       </div> 
                                       <div class="col-md-3">
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                Fecha Inicial
                                                <br />
                                                <dx:BootstrapDateEdit runat="server" ID="dxFechaInicialFiltro" ClientInstanceName="dxFechaInicialFiltro">
                                                </dx:BootstrapDateEdit>
                                            </div>
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                Fecha Final
                                                <br />
                                                <dx:BootstrapDateEdit runat="server" ID="dxFechaFinalFiltro" ClientInstanceName="dxFechaFinalFiltro" EditFormat="Date">
                                                </dx:BootstrapDateEdit>                                               
                                            </div>
                                            
                                        </div>
                                        <div class="col-md-3 text-right">
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                <a id="btnLimpiarFiltros" class="btn btn-primary elm-w160 text-left" onclick="JsCapNps_Reporte.LimpiarFiltros()">
                                                <i class="fa fa-eraser"></i>&nbsp;LimpiarFiltros</a>
                                            </div>
                                             <div class="col-md-12" style="padding: 5px 0px;">
                                                <a class="btn btn-success elm-w160 text-left" id="btnBuscarFiltros" onclick="JsCapNps_Reporte.BuscarNps()">
                                                <i class="fa fa-search"></i>&nbsp;Buscar</a>
                                            </div>
                                            <div class="col-md-12" style="padding: 5px 0px;">
                                                <button id="btnExportarFiltros" type="button" class="btn btn-primary  elm-w160 text-left" onclick="JsCapNps_Reporte.DescargarNps()">
                                                <i class="fa fa-download"></i>&nbsp;Descargar Reporte</button>
                                            </div>
                                        </div>
                                  </div>
                                    
                                 
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table table-hover table-bordered" id="tblNpsReporte">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width: 90px;">Fecha de entrevista
                                                            </th>
                                                            <th style="text-align: center; width: 90px;">Estatus
                                                            </th>
                                                            <th style="text-align: center; width: 90px;">NPS
                                                            </th>
                                                            <th style="text-align: center; width: 90px;">Tema
                                                            </th>
                                                            <th style="text-align: center; width: 90px;">Cliente
                                                            </th>
                                                            <th style="text-align: center; width: 90px;">Sucursal
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div id="ModalUpload" data-keyboard="false" data-backdrop="static" data-toggle="modal"
        style="height: 800px !important" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-dialog modal-dialog2" role="document" style="height: 120px !important;">
            <!-- Modal content-->
            <div class="modal-content modal-content2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    Cargar Documento
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 35% !Important;">
                        <htmliframe class="embed-responsive-item" id="iFrameCargar" runat="server" src=""></htmliframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important; display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje2"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                Ok</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalQuestion" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important; display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo1">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div111" style="overflow-y: hidden
    !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblquestion" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
                            <button class="btn btn-default" data-dismiss="modal" id="btnSi" runat="server">
                                Sí</button>
                            <button class="btn
    btn-default"
                                data-dismiss="modal" id="btnNo">
                                No</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
      
    </script>
</asp:Content>
