<%@ Page Title="Dashboard Kpi Diario" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.master"
    AutoEventWireup="true" CodeBehind="CapDashboardKpiDiario.aspx.cs" Inherits="SIANWEB.CapDashboardKpiDiario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

    <script src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.js")%>"></script>

    <%-- CHART <script src="https://www.chartjs.org/docs/2.9.4/gitbook/gitbook-plugin-chartjs/Chart.bundle.js"></script> --%>
    <script src="js/chart.umd.min.js"></script>
    <script src="js/chartjs-plugin-datalabels.min.js"></script>
    <script src="<%=Page.ResolveUrl("~/js/DashboardKpi/CapDashboardKpiDiario.js?ver=2.28")%>"></script>
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
     <script type="text/javascript">

         var ApplicationUrl = "<%=ApplicationUrl%>";

     </script>
          <style type="text/css">
    .dvstyle {        
        /**
            height: 1300px !important;

        */
        overflow: scroll;
    }
     .bg-gris-claro {
         background-color: LightGrey;
     }

     .bg-negro {
         background-color: #000033;
     }

     .bg-azul-oscuro {
         background-color: #003366;
         color: white;
     }

     .bg-azul-oscuro2 {
         background-color: #336699;
         color: white;
     }

     .bg-blanco-opaco {
         background-color: GhostWhite;
     }

     .text-blanco {
         color: white;
     }

     .etiqueta-grande {
         background-color: #337ab7;
         padding: .2em .6em .3em;
         font-size: 16px;
         font-weight: 700;
         line-height: 1;
         color: #fff;
         text-align: center;
         white-space: nowrap;
         vertical-align: baseline;
         border-radius: .25em;
     }

     .etiqueta-default {
         background-color: #337ab7;
         padding: .2em .6em .3em;
         font-weight: bold;
         line-height: 1;
         color: #fff;
         text-align: center;
         white-space: nowrap;
         vertical-align: baseline;
         border-radius: .25em;
     }

     .moneda-titulo {
         font-size: 16px;
         font-weight: bold;
     }

     .dato-kpi-rik {
         font-size: 14px;
         font-weight: bold;
     }

     .panel {
         margin-bottom: 10px !important;
     }

     .panel-body {
         padding: 10px !important;
     }

     .panel-cartera {
         min-height: 120px;
         max-height: 150px;
     }

     .grafica-header {
         width: 75px;
         height: 80px;
         padding-top: 5px;
     }

     .col-slim {
         padding-right: 5px !important;
         padding-left: 5px !important;
         /*  border: 2px solid red;*/
     }

     .col-clean {
         padding-right: 0px !important;
         padding-left: 0px !important;
     }

     .row {
         margin-right: 0px !important;
         
         margin-left: 0px !important;
         
     }

     .g-content-factura{
        width: 220px;
        margin-left: -20px !important;
    }

     .g-content{
         max-height: 200px;
         max-width: 200px;
         
     }
     .g-content-cartera, .g-content-rik{
            max-height: 100px;
            max-width:  100px;
         
     }

     #canvas-holder {
  width: 100%;
  margin-top: 50px;
  text-align: center;
}

#chartjs-tooltip {
  opacity: 1;
  position: absolute;
  background: rgba(0, 0, 0, .7);
  color: white;
  border-radius: 3px;
  -webkit-transition: all .1s ease;
  transition: all .1s ease;
  pointer-events: none;
  -webkit-transform: translate(-50%, 0);
  transform: translate(-50%, 0);
}

.chartjs-tooltip-key {
  display: inline-block;
  width: 10px;
  height: 10px;
}

 </style>
 </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div style="height:auto !important;">
        <div class="bg-negro ">
            <!-- Inicia Header -->
            <div class="row bg-azul-oscuro">
                <div class="col-xs-12 col-sm-4 col-slim">
                    <div class="" style="">
                        <div class="" style="float: left; width: 160px; height: 90px; padding-top: 5px;padding-left: 5px; margin-top: -10px;margin-left:-5px">
                            <img src="Imagenes/logo_blanco_Key.png" width="130" height="65" />
                        </div>
                        <div class="" style="">
                            <h4 class="text-center">DASHBOARD DIARIO DE KPI´S<br>
                                <span id="cdi_nombre"></span></h4>
                        </div>
                    </div>

                </div>
                <div class="col-xs-12 col-sm-8 col-clean ">
                    <div class="">
                        <div class="col-xs-12 col-sm-5 col-clean">
                            <div class="row ">
                                <div class="col-xs-8 col-slim text-center">
                                    <p>KPI'S DE UTILIDAD BRUTA </p>
                                    <p>Ppto. % utilidad bruta  <span id="utilidadPPTOPorcentaje" class="etiqueta-default">0%</span></p>
                                    <p>% utilidad bruta al día <span id="utilidadDiaPorcentaje" class="etiqueta-default">0%</span></p>
                                </div>
                                <div class="col-xs-4">
                                    <div class="grafica-header" >
                                        <canvas id="graficaHeader1" style="width: 75px; height: 75px;"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-5 col-clean">
                            <div class="row">
                                <div class="col-xs-8 col-slim text-center">
                                    <p>&nbsp;</p>
                                    <p>Ppto. utilidad bruta  <span id="utilidadPptoMoneda" class="etiqueta-default">$0</span></p>
                                    <p>Utilidad bruta al día <span id="utilidadDiaMoneda" class="etiqueta-default">$0</span></p>

                                </div>
                                <div class="col-xs-4">
                                    <div class="grafica-header">
                                        <canvas id="graficaHeader2" style="width: 75px; height: 75px;"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-2 text-center">
                            <p><span id="fechaDashBoard"  class="moneda-titulo"></span></p>
                            <p>Fecha del Dashboard</p>

                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin Header -->
            <!-- Inicia Contenedor Principal -->
            <div class="row" style="margin-top: 10px;">
                <!-- Inicia columna Izquierda 
          
          -->

                <div class="col-sm-4 col-clean" style="padding-right: 5px  !important;">
                    <div class="panel panel-default">
                        <div class="panel-body bg-blanco-opaco">
                            <div class="row">
                                <div class="col-sm-2 col-slim text-left">
                                    <img src="Imagenes/DS_tiro_al_blanco.png" width="60" height="60" />
                                </div>
                                <div class="col-sm-4  col-slim  text-center">
                                    <p>Presupuesto</p>
                                    <span id="presupuestoGnrl" class="moneda-titulo">$0</span>
                                </div>
                                <div class="col-sm-6  col-slim  text-center">
                                    <p>Restante para Presupuesto</p>
                                    <span id="presupuestoGnrlRestante" class="moneda-titulo">$0</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-body bg-blanco-opaco">
                            <div class="row">
                                <div class="col-sm-2  col-slim text-left">
                                    <img src="Imagenes/Ds_Grafica_up.png" width="60" height="60" />
                                </div>
                                <div class="col-sm-5  col-slim  text-center">
                                    <p>Cumplimiento al día</p>
                                    <span id="cumplimientoGnrl" class="moneda-titulo">$0</span>
                                </div>
                                <div class="col-sm-5  col-slim  text-center">
                                    <p>&nbsp;</p>
                                    <span id="cumplimientoGnrlPorcentaje" class="etiqueta-grande">0%</span>
                                </div>
                            </div>
                        </div>
                    </div>
                      

                    <div class="panel panel-primary ">
                        <div class="panel-body bg-blanco-opaco">
                            <div class="row text-center">
                                <div class="col-sm-12 ">
                                    <p>KPI'S de Remisiones PXF</p>
                                </div>
                                <div class="col-sm-6 ">
                                    <p>Remisiones Pendientes de Facturar Vigentes</p>
                                    <p><span id="remisionesPxFVigentes" class="etiqueta-default"></span></p>

                                     <div class="g-content">
                                        <canvas id="graficaRemisionesVigentes" width="80" height="80"></canvas>
                                     </div>
                                </div>
                                <div class="col-sm-6 ">
                                    <p>Remisiones Pendientes de Facturar Vencidas </p>
                                    <p><span id="remisionesPxFVencidas" class="etiqueta-default"></span></p>
                                    <div class="g-content">
                                        <canvas id="graficaRemisionesVencidas" width="80" height="80"></canvas>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <br />
                                    <p>Total de Remisiones PXF <span id="remisionesPxFGnrl" class="etiqueta-default"></span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary ">
                        <div class="panel-body bg-blanco-opaco">
                            <div class="row">
                                <div class="col-sm-12 text-center">
                                    <p>% Cumplimiento de Venta por RIK</p>
                                    <canvas id="graficaCumplimientoVentaRik" style="width: 100%; max-width: 600px"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary ">
                        <div class="panel-body bg-blanco-opaco">
                            <div class="row text-center">
                                <div class="col-sm-12 ">
                                    <p>KPI'S de Facturas</p>
                                </div>
                                <div class="col-sm-12 ">
                                   <table class="table-bordered" >
                                       <thead> 
                                           <tr>
                                                <th class="text-left">Estatus</th>
                                                <th class="text-right">Total facturas</th>
                                                <th class="text-right">Suma de Importe</th>
                                           </tr>
                                       </thead>
                                       <tbody>
                                           <tr>
                                               <td class="text-left">Canceladas</td>
                                               <td class="text-right"><span id="spnNumBaja"></span></td>
                                               <td class="text-right"><span id="spnImporteBaja"></span></td>
                                           </tr>
                                            <tr>
                                                <td class="text-left">Facturas</td>
                                                <td class="text-right"><span id="spnNumFacturas"></span></td>
                                                <td class="text-right"><span id="spnImporteFacturas"></span></td>
                                            </tr>
                                            <tr>
                                                <td class="text-left">Refacturado</td>
                                                <td class="text-right"><span id="spnNumRefacturado"></span></td>
                                                <td class="text-right"><span id="spnImporteRefacturado"></span></td>
                                            </tr>
                                       </tbody>
                                      <tfoot>
                                            <tr>
                                              <th colspan="2" class="text-right">Total General</th>
                                              <th class="text-right"><span id="spnImporteGeneral"></span></th>
                                          </tr>
                                      </tfoot>
                                   </table>
                                </div>
                                <div class="col-sm-6" style="margin-top: 20px; ">  
                                    <span >Gráfico total de facturas</span>
                                    <div class="g-content-factura">
                                        <canvas id="graficaNumFacturas"></canvas>
                                    </div>
                                </div>
                                 <div class="col-sm-6 " style="margin-top: 20px; ">   
                                     <span >Gráfico de Importes</span>
                                     <div class="g-content-factura">
                                         <canvas id="graficaImporteFacturas"></canvas>
                                     </div>
                                 </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Fin columna Izquierda -->
                <!-- Incia columna Derecha -->

                <div class="col-sm-8 col-clean">
                    <!-- Incia Tolates Generales -->
                    <div class="row">
                        <div class="col-sm-3 col-slim">
                            <div class="panel panel-info panel-cartera" style="background-color: #d9edf7;">
                                <div class="panel-body">
                                    <p>Total de cartera de cobranza</p>
                                    <div class="row text-center">
                                        <div class="col-sm-8 col-slim">
                                            <p class="moneda-titulo"><span id="ttlCarteraCobranza">$0</span></p>
                                        </div>
                                        <div class="col-sm-4 col-slim">
                                            <img src="Imagenes/Ds_Billetes.png" width="50" height="40" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 col-slim">
                            <div class="panel panel-success panel-cartera" style="background-color: #dff0d8;">
                                <div class="panel-body">
                                    <p>Total cartera en tiempo</p>
                                    <div class="row text-center">
                                        <div class="col-sm-12 col-lg-7 col-slim">
                                            <p class="moneda-titulo"><span id="ttlCarteraTiempo">$0</span></p>
                                            <img src="Imagenes/Ds_Billetes.png" width="40" height="30" />
                                        </div>
                                        <div class="col-sm-12 col-lg-5 col-slim">
                                           <div class="g-content-cartera">
                                                <canvas id="graficaTtlCarteraTiempo" style="width: 75px; height: 75px;" ></canvas>
                                           </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 col-slim">
                            <div class="panel panel-warning panel-cartera" style="background-color: #fcf8e3;">
                                <div class="panel-body">
                                    <p>Total de cartera de 1 a 30 días</p>
                                    <div class="row text-center">
                                        <div class="col-sm-6 col-lg-7 col-slim">
                                            <p class="moneda-titulo"><span id="ttlCarteraMenos30dias">$0</span></p>
                                            <img src="Imagenes/Ds_Billetes.png" width="40" height="30" />
                                        </div>
                                        <div class="col-sm-6 col-lg-5 col-slim">
                                            <div class="g-content-cartera">
                                                <canvas id="graficaTtlCarteraMenos30dias" style="width: 75px; height: 75px;" ></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 col-slim">
                            <div class="panel panel-dangert panel-cartera" style="background-color: #f2dede;">
                                <div class="panel-body">
                                    <p>Total cartera más de 30 días</p>
                                    <div class="row text-center">
                                        <div class="col-sm-6 col-lg-7 col-slim">
                                            <p class="moneda-titulo"><span id="ttlCarteraMas30dias">$0</span></p>
                                            <img src="Imagenes/Ds_Billetes.png" width="40" height="30" />
                                        </div>
                                        <div class="col-sm-6 col-lg-5 col-slim">
                                            <div class="g-content-cartera">
                                                <canvas id="graficaTtlCarteraMas30dias"style="width: 75px; height: 75px;" ></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Fin Tolates Generales -->
                    <div class="row">
                    </div>

                    <div class="row" id="targetaRik" style="font-size: 13px;">
                    </div>
                <!-- Fin columna Derecha -->
            </div>
            <!-- Fin Contenedor Principal -->
        </div>
    </div>
    <div id="chartjs-tooltip"></div>
</asp:Content>
