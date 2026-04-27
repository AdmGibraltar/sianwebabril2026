<%@ Page Title="Facturas Canceladas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true" CodeBehind="CapFacturaCancelada.aspx.cs" Inherits="SIANWEB.CapFacturaCancelada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/datatables/datatables.min.css")%>">
    <style>
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(255, 255, 255, 0.8);
            z-index: 9999;
            display: none;
        }

        .loading-overlay .spinner {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 text-right" style="margin: 10px 0px 10px 0px;">
                <button id="btnDescargarExcel" class="btn btn-primary">Descargar en Excel</button>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table id="tblFacturas" class="table table-bordered">
                    <thead>
                        <tr>
                            <th>RFC Receptor</th>
                            <th>Razón Social</th>
                            <th>Serie</th>
                            <th>Folio</th>
                            <th>Folio Fiscal</th>
                            <th>Fecha de Emisión</th>
                            <th>Fecha de Sol. Canc.</th>
                            <th>Tipo Documento</th>
                            <th>Estado SAT</th>
                            <th>Subtotal</th>
                            <th>IVA</th>
                            <th>Total</th>
                            <th>Folio Relacionado</th>
                            <th>Serie Relacionada</th>
                            <th>Folio Fiscal Relacionado</th>
                            <th>Tipo Documento Relacionado</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="loading-overlay">
        <div class="spinner">
            <img src="<%=Page.ResolveUrl("~/Imagenes/ajax-loader.gif")%>" alt="Cargando...">
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
     <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/datatables/datatables.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Facturas/CapFacturaCancelada.js?ver=250701.01")%>"></script>
    <script>
        var ApplicationUrl = "<%=ApplicationUrl%>";
    </script>
</asp:Content>
