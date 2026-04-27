<%@ Page Title="Usuarios Facturas Canceladas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.master" AutoEventWireup="true" CodeBehind="CapUsuariosFacturaCancelada.aspx.cs" Inherits="SIANWEB.CapUsuariosFacturaCancelada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Bootstrap, jQuery, Alertify, DataTable CSS -->
    <link href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" rel="stylesheet" />

    <style>
  
        /* Estilos para Loading */
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(255, 255, 255, 0.8);
            z-index: 9999;
            display: none;
            justify-content: center;
            align-items: center;
        }
        .loading-spinner {
            border: 16px solid #f3f3f3;
            border-top: 16px solid #3498db;
            border-radius: 50%;
            width: 120px;
            height: 120px;
            animation: spin 2s linear infinite;
        }
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        /* Fin Estilos Loading */
        /* Estilos más específicos para asegurar que se apliquen a las alertas de error */
.alertify-logs > .error {
    width: 360px !important;
    min-width: 360px !important;
    max-width: 360px !important;
    white-space: normal !important;
    line-height: 1.4 !important;
    padding: 10px !important;
    color: white !important;
    background-color: #F44336 !important;
    border-radius: 4px !important;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2) !important;
}

/* Estilos para warning y success */
.alertify-logs > .success,
.alertify-logs > .warning {
    width: 360px !important;
    min-width: 360px !important;
    max-width: 360px !important;
    white-space: normal !important;
    line-height: 1.4 !important;
    padding: 10px !important;
    border-radius: 4px !important;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">
        
        <div class="row" style="margin-top:10px;">
            
            <!-- Contenedor Usuarios Activos -->
            <div class="col-md-10">
                <div class="panel panel-primary">
                    <div class="panel-heading">Destinatarios de notificaciones del informe de facturas canceladas</div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tblUsuariosActivos" class="table table-bordered table-striped" style="width:100%">
                                <thead>
                                    <tr>
                                        <th>Ingrese los correos use el separador ";" <br />
                                            Ejemplo: "jos.cdc@key.com.mx;facturacion.cdc@key.com.mx;cobranza.cdc@key.com.mx"</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                         <td>
                                             <textarea id="txtCorreo" class="form-control" rows="3"></textarea>
                                         </td>
                                         <td><button id="btnGuardar" class="btn btn-primary" > 
                                             <i class="fa fa-save"></i> Guardar</button></td>
                                     </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Loading -->
    <div id="loading" class="loading-overlay">
        <div class="loading-spinner"></div>
    </div>

    <!-- jQuery, Bootstrap, Alertify, DataTable JS -->
    <script src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Facturas/CapUsuariosFacturaCancelada.js?ver=250701.1")%>"></script>
    <script type="text/javascript">
        var ApplicationUrl = "<%=ApplicationUrl%>";
        // Configurar Alertify para que se muestre correctamente
        
       
        // Si warning no existe, crearlo
        if (typeof alertify.warning !== 'function') {
            alertify.warning = function (message) {
                return alertify.log(message, "warning");
            };
        }

        // Reemplazo para alertify.error
        var originalError = alertify.error;
        alertify.error = function (message, timeout) {
            if (timeout === undefined) timeout = 5000;

            // Fix para problemas de visualización
            var result = originalError(message);

            // Intentar forzar el ancho directamente en el elemento creado
            setTimeout(function () {
                var errorElements = document.querySelectorAll('.alertify-logs > .error');
                if (errorElements.length > 0) {
                    var lastError = errorElements[errorElements.length - 1];
                    lastError.style.width = '360px';
                    lastError.style.maxWidth = '360px';
                    lastError.style.whiteSpace = 'normal';
                }
            }, 10);

            return result;
        };
    </script>
</asp:Content>  