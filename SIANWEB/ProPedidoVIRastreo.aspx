<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ProPedidoVIRastreo.aspx.cs" Inherits="SIANWEB.ProPedidoVIRastreo" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>"> 
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>"> 
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script> 
    <style type="text/css">
        .green
        {
            color: Green;
            font-size: xx-large;
            margin-left: 10%;
            margin-top: 10%;
            margin-bottom: 10%;
        }
        
        
        .blue
        {
            color: blue;
            font-size: xx-large;
            margin-left: 10%;
            margin-top: 10%;
            margin-bottom: 10%;
        }
        
        
        .black
        {
            color: black;
            font-size: xx-large;
            margin-left: 10%;
            margin-top: 10%;
            margin-bottom: 10%;
        }
        
        .border
        {
            border-style: groove;
        }
        
        .labelposition
        {
            margin-top: -50px;
            margin-left: 8px;
            font-size: 12px;
            position: absolute;
            color: white;
        }
    </style>
    <script type="text/javascript">
        function AbrirReporteGeneral(strPedido, FechaInicial, FechaFinal) {
            window.parent.closeModalRastreo(strPedido, FechaInicial, FechaFinal);
        };
    </script>
    <div class="modal-body" id="Div2">
    <asp:UpdatePanel runat="server" ID="updpanle" >
    <ContentTemplate>
        <div class="col-md-12">
            <div class="row">
                <div class="col-xs-8">
                    <div class="form-group">
                        Pedido:
                        <asp:Label runat="server" ID="lblPedido"></asp:Label>
                    </div>
                </div>
                <div class="col-xs-4">
                    <button type="submit" class="btn btn-primary btn-sm" id="btnReportePedido" onserverclick="btnReporte_Click"
                        runat="server">
                        <i aria-hidden="true"></i>&nbsp;Reporte de pedido
                    </button>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <div class="form-group">
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -11px;">
                            Captado
                        </h4>
                    </div>
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -9px;">
                            Asignado
                        </h4>
                    </div>
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -13px;">
                            Facturado</h4>
                    </div>
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -10px;">
                            Embarque</h4>
                    </div>
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -10px;">
                            Entregado</h4>
                    </div>
                    <div class="col-xs-2 border">
                        <h4 style="margin-left: -9px;">
                            Cobranza</h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <div class="col-xs-2 border">
                        <div id="divCaptado" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle blue"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="Label1">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNoCaptado" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                    <div class="col-xs-2 border">
                        <div id="divAsigando" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle blue"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="lblAsignado">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNoAsignado" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                    <div class="col-xs-2 border">
                        <div id="divfacturacion" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="lblfacturacion">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNofacturacion" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                    <div class="col-xs-2 border">
                        <div id="divembarque" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle blue"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="lblEmbarque">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNoEmbarque" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                    <div class="col-xs-2 border">
                        <div id="divEntregado" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle blue"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="lblEntregado">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNoEntregado" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                    <div class="col-xs-2 border">
                        <div id="divCobranza" runat="server" visible="false">
                            <span class="fa-stack blue"><i class="fa fa-circle"></i><span class="fa fa-stack"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                    <asp:Label CssClass="labelposition" runat="server" ID="lblcobranza">
                                    </asp:Label>
                                </span></span></span>
                        </div>
                        <div id="divNoCobranza" runat="server" visible="false">
                            <span class="fa-stack green"><i class="fa fa-circle-thin"></i><span class="fa fa-stack-1x"
                                style="color: red;"><span style="font-size: 35px; margin-top: 2px; color: Gray">
                                </span></span></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnReportePedido" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>