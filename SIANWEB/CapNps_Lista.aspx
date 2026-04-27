<%@ Page Title="NPS" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="CapNps_Lista.aspx.cs" Inherits="SIANWEB.PortalRIK.Nps_Sian.CapNps_Lista" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <!-- ALERTIFY 0.3.11 NUEVO -->
    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>"> 

    <script type="text/javascript" src="../../Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="../../Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
   
    <style>
        .elm-w130 {
            width: 130px;
        }

        .elm-w150 {
            width: 150px;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../images/load.gif" AlternateText="Favor de Esperar..."
                        ToolTip="Favor de Esperar..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="dvLoading">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <img id="imgUpdateProgress" src="../../images/load.gif" alt="Favor de Esperar..." style="position: fixed; top: 45%; left: 40%;" />
        </div>

        </div>
            
    <form id="Form1" runat="server">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="divPrincipal" runat="server">
            <h3>NPS</h3>

            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptGlobalization="True"
                EnableScriptLocalization="True" EnableTheming="true" AsyncPostBackTimeout="36000">
            </telerik:RadScriptManager>   
            <div id="idFiltros">
                <div class="row">
                    <div class="col-md-5">
                        <div class="col-md-3 text-right">
                             Rik                       
                        </div>
                        <div class="col-md-9">   
                             <dx:BootstrapComboBox ID="dxCmbRikFiltro" ClientInstanceName="dxCmbRikFiltro" runat="server">
                                  <ClientSideEvents  ValueChanged="OnChangedRikFiltro" />
                             </dx:BootstrapComboBox>
                        </div>
                    </div>
                    <div class="col-md-5" >
                         <div class="col-md-3 text-right">
                              Fecha Inicial                       
                        </div>
                        <div class="col-md-9"> 
                            <dx:BootstrapDateEdit runat="server" ID="dxFechaInicialFiltro" ClientInstanceName="dxFechaInicialFiltro">
                            </dx:BootstrapDateEdit>
                        </div>
                    </div>
                    
                    <div class="col-md-2">
                         <a id="btnLimpiarFiltros" class="btn btn-primary btn-sm elm-w150 text-left" onclick="JsCapNps_Lista.LimpiarFiltros()">
                                <i class="fa fa-eraser"></i>&nbsp;LimpiarFiltros</a>
                    </div>
                </div>

                <div class="row" style="margin-top: 5px;">
                    <div class="col-md-5">
                        <div class="col-md-3 text-right">
                            Cliente
                        </div>
                        <div class="col-md-9">  
                            <select id="cmbClienteFiltro" class="form-control">
                            </select>
                        </div>
                    </div>
                 <div class="col-md-5">
                      <div class="col-md-3 text-right">
                               Fecha Final                       
                        </div>
                        <div class="col-md-9"> 
                            <dx:BootstrapDateEdit runat="server" ID="dxFechaFinalFiltro" ClientInstanceName="dxFechaFinalFiltro" EditFormat="Date">
                            </dx:BootstrapDateEdit>                                               
                        </div>
                    </div>
                    <div class="col-md-2">
                        <a class="btn btn-success btn-sm elm-w150 text-left" id="btnBuscarFiltros" onclick="JsCapNps_Lista.BuscarNps()">
                                <i class="fa fa-search"></i>&nbsp;Buscar</a>
                    </div>
                </div>

                <div class="row"  style="margin-top: 5px;">
                    <div class="col-md-5">
                        <div class="col-md-3 text-right">
                            NPS
                        </div>
                        <div class="col-md-9">                         
                            <select id="cmbNpsFiltro" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="col-md-5">
                        
                    </div>
                    <div class="col-md-2">                    
                         <button id="btnExportarFiltros" type="button" class="btn btn-primary btn-sm elm-w150 text-left"  onclick="JsCapNps_Lista.DescargarNps()">
                                <i class="fa fa-download"></i>&nbsp;Descargar Reporte</button>
                    </div>
                </div>

                <div class="row"  style="margin-top: 5px;">
                    <div class="col-md-5">
                        <div class="col-md-3 text-right">
                            Estatus 
                        </div>
                        <div class="col-md-9">                                            
                            <select id="cmbEstatusFiltro" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="col-md-4">

                        </div>
                        <div class="col-md-8">

                        </div>
                    </div>
                    <div class="col-md-2">

                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <table class="table table-hover table-bordered" id="tblNpsReporte">
                        <thead>
                            <tr>
                                <th style="text-align: center; width: 90px;">Estatus
                                </th>
                                <th style="text-align: center; width: 90px;">Fecha de entrevista
                                </th>
                                <th style="text-align: center; width: 90px;">RIK
                                </th>
                                <th style="text-align: center; width: 90px;">Cliente
                                </th>
                                <th style="text-align: center; width: 90px;">NPS
                                </th>
                                <th style="text-align: center; width: 90px;">Tema
                                </th>
                                <th style="text-align: center; width: 90px;">Plan de Accion
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
        </div>


        <!-- Modal  -->
       

        <div id="modalAgregarPlan" data-backdrop="static" data-keyboard="false" class="modal">
            <div class="modal-dialog" role="document" style="width: 900px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <h4 id="mdTitulo">NPS Plan de Accíon</h4>
                                </td>
                                <td style="width: 30px;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-body">

                    <div  style="background-color: #f5f5f5; padding-top: 15px; padding-bottom: 15px; border-bottom: 1px solid #e5e5e5; margin-right: -15px; margin-left: -15px; ">
                        <input type="hidden" id="hdnIdNpsPlan" value="" />
                        <input type="hidden" id="hdnDateEntrevista" value="" />
                        <input type="hidden" id="hdnPlanConsecutivo" value="" />
                        <div class="row">
                            <div class="col-sm-3 text-right" >
                                 <label>Cliente</label> 
                            </div>
                            <div class="col-sm-9">
                                <span id="spnCliente"></span>
                            </div> 
                            
                     </div>
                        <div class="row">
                            <div class="col-sm-3 text-right">
                                <label>RIK</label>
                            </div>
                            <div class="col-sm-9">
                                  <span id="spnRik"></span>
                                 
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-sm-3 text-right">
                                <label>NPS</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnValor"></span>
                            </div>                       
                        </div>
                        <div class="row segundaOportunidad" >
                            <div class="col-sm-3 text-right">
                                <label>NPS Segundo</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnValorSegundo"></span>
                            </div>                       
                         </div>
                       <div class="row">
                            <div class="col-sm-3 text-right">
                                 <label>Entrevistado</label> 
                            </div>
                            <div class="col-sm-9">
                                <span id="spnEntrevistado"></span>
                            </div>
                         </div>
                        <div class="row">
                              <div class="col-sm-3 text-right">
                                 <label>Puesto/área</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnPuesto"></span>
                            </div>
                         </div>
                       
                   </div>    
                    <div class="tblComenarioHide" id="tblComentrio0">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema0"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan0" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja0"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo0"></span>
                            </div>
                        </div>
                        <div class="row segundaOportunidad">
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción Anterior</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnPlanAnterior0"></span>
                            </div>
                        </div>
                        <div class="row segundaOportunidad" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha Compromiso Anterior</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnCompromisoAnterior0"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan0" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso0" ClientInstanceName="dxFechaCompromiso0" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio1">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema1"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan1" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja1"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo1"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan1" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso1" ClientInstanceName="dxFechaCompromiso1" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio2">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema2"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan2" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja2"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo2"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan2" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso2" ClientInstanceName="dxFechaCompromiso2" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio3">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema3"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan3" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja3"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo3"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan3" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso3" ClientInstanceName="dxFechaCompromiso3" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio4">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema4"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan4" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja4"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo4"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan4" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso4" ClientInstanceName="dxFechaCompromiso4" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio5">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema5"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan5" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja5"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo5"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan5" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso5" ClientInstanceName="dxFechaCompromiso5" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio6">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema6"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan6" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja6"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo6"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan6" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso6" ClientInstanceName="dxFechaCompromiso6" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio7">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema7"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan7" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja7"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo7"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan7" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso7" ClientInstanceName="dxFechaCompromiso7" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio8">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema8"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan8" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja8"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo8"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan8" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso8" ClientInstanceName="dxFechaCompromiso8" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    <div class="tblComenarioHide" id="tblComentrio9">
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Tema</label>
                            </div>
                            <div class="col-sm-9">
                                <b><span id="spnTema9"></span></b>
                                <input type="hidden" id="hdnIdQuejaPlan9" />
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Comentario</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnQueja9"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Protocolo Sugerido</label>
                            </div>
                            <div class="col-sm-9">
                                <span id="spnProtocolo9"></span>
                            </div>
                        </div>
                         <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Plan de Acción</label>
                            </div>
                            <div class="col-sm-9">
                               <textarea id="txtPlan9" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-sm-3 text-right">
                                <label>Fecha compromiso</label>
                            </div>
                            <div class="col-sm-9">
                               <dx:BootstrapDateEdit runat="server" ID="dxFechaCompromiso9" ClientInstanceName="dxFechaCompromiso9" EditFormat="Date">
                                </dx:BootstrapDateEdit>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="modal-footer" style="margin-right: 20px;">
                        <div class="row">
                            <div class="col s12">
                                <a class="btn btn-primary elm-w200 text-left" id="btnGuardarPlan" onclick="JsCapNps_Lista.GuardarPlan()"><i class="fa fa-save"></i> &nbsp; Guardar plan de acción
                                </a>
                                <a class="btn btn-danger elm-w200 text-left" id="btnConcluir" onclick="JsCapNps_Lista.ConcluirPlan()"><i class="fa fa-book"></i> &nbsp; Concluir
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</ContentTemplate>
         </asp:UpdatePanel>
    </form>
            
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="../../js/NPS/CapNps_Lista.js?Vercion=1.1.16"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
