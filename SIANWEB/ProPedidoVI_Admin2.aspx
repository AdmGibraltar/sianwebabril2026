<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI_Admin2.aspx.cs" Inherits="SIANWEB.ProPedidoVI_Admin2" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <style type="text/css">
        .red {
            color: red;
        }

        .rightText {
            text-align: right;
        }

        .leftText {
            text-align: left;
        }

        .orange {
            color: orange;
        }

        .black {
            color: black;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }


        .dxbs-calendar {
            position: absolute;
        }

        .centerText {
            text-align: center;
        }

        .RadForm_Outlook.rfdHeading h4 {
            border-bottom: solid 0px #6788be !important;
        }

        .modal.modal-fullscreen .modal-dialog {
            margin: 0;
            width: 100%;
            max-width: none;
            height: 100vh; /* ocupa todo el viewport */
        }

        .modal.modal-fullscreen .modal-content {
            height: 100%;
            border-radius: 0;
            display: flex;
            flex-direction: column;
        }

        .modal.modal-fullscreen .modal-header,
        .modal.modal-fullscreen .modal-footer {
            flex-shrink: 0;
        }

        .modal.modal-fullscreen .modal-body {
            flex: 1 1 auto;
            overflow: auto; /* scroll interno del modal */
            padding: 0; /* el iframe aprovecha todo el espacio */
        }

        /* Contenedor para iframes que ocupan todo el cuerpo del modal */
        .modal-iframe-wrapper {
            position: relative;
            width: 100%;
            height: 100%;
        }

            .modal-iframe-wrapper iframe {
                position: absolute;
                inset: 0;
                width: 100%;
                height: 100%;
                border: 0;
            }

        /* Quita padding-bottom forzado en todos los modales con embed-responsive */
        #Detalles .embed-responsive-16by9,
        #ModalRastreo .embed-responsive-16by9,
        #modalCliente .embed-responsive-16by9,
        #protocolos .embed-responsive-16by9,
        #Imprimir .embed-responsive-16by9,
        #Remisiones .embed-responsive-16by9,
        #modalImprimir .embed-responsive-16by9,
        #ModalRepHPedido .embed-responsive-16by9 {
            padding-bottom: 0 !important;
        }
    </style>

    <script type="text/javascript">
        // Broad cast that your're opening a page.
        localStorage.openpages = Date.now();
        var onLocalStorageEvent = function (e) {
            if (e.key == "openpages") {
                // Listen if anybody else opening the same page!
                localStorage.page_available = Date.now();
            }
            if (e.key == "page_available") {
                alert("Esta pantalla esta abierta en otra sesion..");
                window.history.back();
            }
        };
        window.addEventListener('storage', onLocalStorageEvent, false);
        $(document).ready(function () {
            $("#Detalles").on('hidden.bs.modal', function () {
                var btn = document.getElementById('<%=btnBuscar.ClientID%>');
                btn.click();
            });
            $('#btnUploadCancel').click(function () {
                $('#modalAcysMensaje').modal('toggle');
                $("#modalAcysMensaje").appendTo("body");
                $("#modalAcysMensaje").modal({ "backdrop": "static" });
            });
        });
        function NuevoCliente(guardar, modificar) {
            document.getElementById('<%=FrameClientenuevo.ClientID%>').src = "CapPedidoCaptadoNuevo.aspx?PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar;
            $("#modalCliente").appendTo("body");
            $("#modalCliente").modal({ "backdrop": "static" });
            $('#modalCliente').modal('show');
            $('#modalCliente').find('.modal-dialog').css({
                width: '50%',
                height: '50% !Important'
            });
        }
        function AbrirReportePadre() {
            document.getElementById('<%=IFrameImprimirPEdidos.ClientID%>').src = "Ventana_ReportViewer2.aspx";
            $("#Imprimir").appendTo("body");
            $("#Imprimir").modal({ "backdrop": "static" });
            $('#Imprimir').modal('show');
            $('#Imprimir').find('.modal-dialog').css({
                width: '80%', //probably not needed
                height: 'auto', //probably not needed
                'max-height': '100%'
            });
        }
        function Protocolos() {
            $("#protocolos").appendTo("body");
            $("#protocolos").modal({ "backdrop": "static" });
            $('#protocolos').modal('show');
            $('#protocolos').find('.modal-dialog').css({
                width: '80%', //probably not needed
                height: 'auto', //probably not needed
                'max-height': '100%'
            });
        }
        function EditarPedido() {
            $("#modalEditar").appendTo("body");
            $("#modalEditar").modal({ "backdrop": "static" });
            $('#modalEditar').modal('show');
        }
        function consultareditarpedido() {
            var id = $("#" + '<%=txtPedido.ClientID%>').val();
            $("#" + '<%=hdneditarPedido.ClientID%>').val(id);
            $("#" + '<%=btnconsultareditar.ClientID%>').click();
        }
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
            $("#modalAcysMensaje").appendTo("body")
            $("#modalAcysMensaje").modal({ "backdrop": "static" });
            $('#modalAcysMensaje').modal('show');
        }
        function modalRastreo(Id, Pedido) {
            $('#ModalRastreo').modal('hide');
            document.getElementById('<%=iFrameRastreo.ClientID%>').src = "ProPedidoVIRastreo.aspx?Id=" + Id + "&idP=" + Pedido;
            $("#ModalRastreo").appendTo("body");
            $("#ModalRastreo").modal({ "backdrop": "static" });
            $('#ModalRastreo').modal('show');
        }
        function modalDetalleSinPedido(Id, guardar, modificar, eliminar, imprimir, Anio, semana, Id_TG, IDDireccionEntrega) {
            $('#modalCliente').modal('hide');
            var idTgQueryComponent = '';
            if (typeof (Id_TG) != 'undefined' && typeof (Id_TG) != undefined) {
                idTgQueryComponent = "&Id_TG=" + Id_TG;
            }
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana + idTgQueryComponent + "&IdDireccion=" + IDDireccionEntrega;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function modalDetalleOrdenCompra(Id, guardar, modificar, eliminar, imprimir, Anio, semana, Id_TG, IDDireccion) {
            $('#modalCliente').modal('hide');
            var idTgQueryComponent = '';
            if (typeof (Id_TG) != 'undefined' && typeof (Id_TG) != undefined) {
                idTgQueryComponent = "&Id_TG=" + Id_TG;
            }
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?Id=" + Id + "&OrdenCompra=" + true + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana + idTgQueryComponent + "&IdDireccion=" + IDDireccion;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function modalDetalle(Id, guardar, modificar, eliminar, imprimir, Anio, Pedido, semana, Id_TG, IDDireccionEntrega) {
            $('#modalCliente').modal('hide');
            var idTgQueryComponent = '';
            if (typeof (Id_TG) != 'undefined' && typeof (Id_TG) != undefined) {
                idTgQueryComponent = "&Id_TG=" + Id_TG;
            }
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&idP=" + Pedido + "&Semana=" + semana + idTgQueryComponent + "&IdDireccion=" + IDDireccionEntrega;;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }


        function modalDetalleAuto(idSol, Id, guardar, modificar, eliminar, imprimir, Anio, Pedido, semana, Id_TG, IDDireccionEntrega) {
            $('#modalCliente').modal('hide');
            var idTgQueryComponent = '';
            if (typeof (Id_TG) != 'undefined' && typeof (Id_TG) != undefined) {
                idTgQueryComponent = "&Id_TG=" + Id_TG;
            }
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?Id=" + Id + "&IdAutorizacion=" + idSol + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&idP=" + Pedido + "&Semana=" + semana + idTgQueryComponent + "&IdDireccion=" + IDDireccionEntrega;;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }

        function modalDetallePedido(Pedido, guardar, modificar, eliminar, imprimir) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?idP=" + Pedido + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }

        function modalDetallePedidoOC(Pedido, guardar, modificar, eliminar, imprimir) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVIOC.aspx?idP=" + Pedido + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }

        function AbrirVentana_ProPedido_InternetInsert(Id, tipoPedido, URL, guardar, modificar, eliminar, imprimir, Anio, semana) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVIInternet2.aspx?IdPeInt=" + Id + "&tipoPedido=" + tipoPedido + "&UrlPedido=" + URL + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function AbrirVentana_ProPedido_Internet(Id, tipoPedido, numPedido, guardar, modificar, eliminar, imprimir, Anio, semana) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVIInternet2.aspx?IdPeInt=" + Id + "&tipoPedido=" + tipoPedido + "&numPedido=" + numPedido + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function AbrirVentana_VerRemisiones_OC(Id) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=IRemisiones.ClientID%>').src = "VentanaRemisionesOC2.aspx?IdOC=" + Id;
            $("#Remisiones").appendTo("body");
            $("#Remisiones").modal({ "backdrop": "static" });
            $('#Remisiones').modal('show');
        }
        function AbrirVentana_ProPedido_OC(Id, guardar, modificar, eliminar, imprimir, Anio, semana) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVIOC.aspx?IdOC=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function AbrirVentana_ProPedido_OCAutorizacion(Id, guardar, modificar, eliminar, imprimir, Anio, semana) {
            $('#modalEditar').modal('hide');
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVIOC.aspx?IdAutorizacion=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function mantaintabs(datosTabs) {
            $('#Tabs a[href="#' + datosTabs + '"]').tab('show');
        }
        function mantaintabsAndOpenLink(link) {
            console.log(link);
            setTimeout(() => {
                $('#Tabs a[href="#tabInternet"]').tab('show');
                console.log(link);
                window.open(link);
            }, 500);
        }
        function OnDescargarInternetOC(value) {
            console.log(value);
            window.open(value);
        }

        function onCustomButtonClick(s, e) {

            if (e.buttonID == "btnInternetOC") {
                rowVisibleIndex = e.visibleIndex;
                s.GetRowValues(e.visibleIndex, "Url", OnDescargarInternetOC);
            }
        }
        function gridviewOrderCompra_onCustomButtonClick(s, e) {

            if (e.buttonID == "btnDownload") {
                rowVisibleIndex = e.visibleIndex;
                s.GetRowValues(e.visibleIndex, "pedido", OnbtnDownload);
            }
        }

        function OnbtnDownload(value) {

            document.getElementById("<%=hdnDescargaOC.ClientID%>").value = value;
            var e = document.getElementById("<%=btnDescargarOC.ClientID%>");
            e.click();
        }

        window.closeModal = function (idCTe, guardar, modificar) {
            $('#modalCliente').modal('hide');
            if (idCTe != null) {
                document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?idPNuevo=" + idCTe + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar;
                $("#Detalles").appendTo("body");
                $("#Detalles").modal({ "backdrop": "static" });
                $('#Detalles').modal('show');
            }
        }
        function AbrirNuevoDetalles(guardar, modificar, eliminar, imprimir) {
            $('#modalCliente').modal('hide');
            console.log(guardar, modificar, eliminar, imprimir);
            document.getElementById('<%=frameDetalle.ClientID%>').src = "ProPedidoVI2.aspx?&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        window.closeModalDetalle = function () {
            $('#Detalles').modal('hide');
        }
        window.closeModalRastreo = function (strPedido, FechaInicial, FechaFinal) {
            strPedido, FechaInicial, FechaFinal
            $('#ModalRastreo').modal('hide');
            var url = "RepHPedidoDX.aspx?RepHPedidoDX=" + strPedido + " &exFecIni= " + FechaInicial + "&exFecFin=" + FechaFinal;
            var win = window.open(url, '_blank');
            win.focus
        }
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tabCliente";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });

        function ajustarAltoModal(modalId) {
            var $modal = $(modalId);
            var $content = $modal.find('.modal-content');
            var $header = $content.find('.modal-header');
            var $footer = $content.find('.modal-footer');
            var $body = $content.find('.modal-body');

            if (!$body.length) return;

            var total = $content.innerHeight();
            var headerH = $header.length ? $header.outerHeight() : 0;
            var footerH = $footer.length ? $footer.outerHeight() : 0;
            var bodyH = total - headerH - footerH;

            $body.css({ height: bodyH + 'px', overflow: 'auto' });
        }

        $(function () {
            var modalesAjustables = ['#Detalles', '#Remisiones', '#modalImprimir', '#ModalRepHPedido', '#ModalRastreo', '#modalCliente', '#Imprimir', '#protocolos'];
            modalesAjustables.forEach(function (id) {
                $(id).on('shown.bs.modal', function () { ajustarAltoModal(id); });
            });
            $(window).on('resize', function () {
                modalesAjustables.forEach(function (id) {
                    if ($(id).is(':visible')) ajustarAltoModal(id);
                });
            });
        });

    </script>
    <div class="container-fluid">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel runat="server" ID="UPDPanelPagos">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="row" style="margin-top: 15px;">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title"></h3>
                            </div>
                            <div class="panel-body">

                                <div class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-8">

                                        <div class="form-group">
                                            <div class="col-md-2">
                                                Razón Social Cliente
                                            </div>
                                            <div class="col-md-10">
                                                <dx:BootstrapComboBox ID="ddlRazonSocialCliente" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Número de Pedido
                                            </div>
                                            <div class="col-md-8">
                                                <input type="text" class="form-control" runat="server" id="txtnumeroPed"
                                                    maxlength="9" onkeypress="return isNumber(event)" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Año
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtanio" runat="server" CssClass="form-control" OnTextChanged="txt_OnTextChanged"
                                                    MaxLength="4" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4" style="display: none">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Frecuencia de Pedido
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="ddlfrecuencia" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="--Todos--" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="1" Text="Semanal" />
                                                        <dx:BootstrapListEditItem Value="2" Text="Mensual" />
                                                        <dx:BootstrapListEditItem Value="3" Text="Bimestral" />
                                                        <dx:BootstrapListEditItem Value="4" Text="Trimestral" />
                                                        <dx:BootstrapListEditItem Value="5" Text="Semestral" />
                                                    </Items>
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { SetComboBoxImage(s); }" />
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="DivTodos" class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 1%;">
                                            <div class="col-md-4">
                                                semana Inicial
                                            </div>
                                            <div class="col-md-6">
                                                <dx:BootstrapDateEdit ID="txtsemanainicial" runat="server" AutoPostBack="true" OnDateChanged="txtsemanainicial_DateChanged">
                                                </dx:BootstrapDateEdit>
                                            </div>
                                            <div class="col-md-2">
                                                <input type="text" runat="server" id="txtsemanainicialnum" style="width: 50px" class="form-control"
                                                    readonly="readonly" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 1%;">
                                            <div class="col-md-4">
                                                semana Final
                                            </div>
                                            <div class="col-md-6">
                                                <dx:BootstrapDateEdit ID="txtsemanafinal" runat="server" AutoPostBack="true" OnDateChanged="txtsemanafinal_DateChanged">
                                                </dx:BootstrapDateEdit>
                                            </div>
                                            <div class="col-md-2">
                                                <input type="text" runat="server" id="txtsemanafinalnum" style="width: 50px" class="form-control"
                                                    readonly="readonly" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="DivSemana" class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Semana
                                            </div>
                                            <div class="col-md-6">
                                                <dx:BootstrapDateEdit ID="rdSemana" runat="server" AutoPostBack="true" OnDateChanged="btnconsultarSemana_Click">
                                                </dx:BootstrapDateEdit>
                                            </div>
                                            <div class="col-md-2">
                                                <input type="text" runat="server" id="textIdSemana" style="width: 50px" class="form-control"
                                                    readonly="readonly" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="DivMes" class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Mes
                                            </div>
                                            <div class="col-md-6">
                                                <dx:BootstrapComboBox ID="ddlMes" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="1" Text="Enero" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="2" Text="Febrero" />
                                                        <dx:BootstrapListEditItem Value="3" Text="Marzo" />
                                                        <dx:BootstrapListEditItem Value="2" Text="Abril" />
                                                        <dx:BootstrapListEditItem Value="5" Text="Mayo" />
                                                        <dx:BootstrapListEditItem Value="6" Text="Junio" />
                                                        <dx:BootstrapListEditItem Value="7" Text="Julio" />
                                                        <dx:BootstrapListEditItem Value="8" Text="Agosto" />
                                                        <dx:BootstrapListEditItem Value="9" Text="Septiembre" />
                                                        <dx:BootstrapListEditItem Value="10" Text="Octubre" />
                                                        <dx:BootstrapListEditItem Value="11" Text="Noviembre" />
                                                        <dx:BootstrapListEditItem Value="12" Text="Diciembre" />
                                                    </Items>
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="DivMeses" class="col-md-12" style="margin-top: 1%;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Mes Inicial
                                            </div>
                                            <div class="col-md-6">
                                                <dx:BootstrapComboBox ID="DdlMeses" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="1" Text="Enero" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="2" Text="Febrero" />
                                                        <dx:BootstrapListEditItem Value="3" Text="Marzo" />
                                                        <dx:BootstrapListEditItem Value="2" Text="Abril" />
                                                        <dx:BootstrapListEditItem Value="5" Text="Mayo" />
                                                        <dx:BootstrapListEditItem Value="6" Text="Junio" />
                                                        <dx:BootstrapListEditItem Value="7" Text="Julio" />
                                                        <dx:BootstrapListEditItem Value="8" Text="Agosto" />
                                                        <dx:BootstrapListEditItem Value="9" Text="Septiembre" />
                                                        <dx:BootstrapListEditItem Value="10" Text="Octubre" />
                                                        <dx:BootstrapListEditItem Value="11" Text="Noviembre" />
                                                        <dx:BootstrapListEditItem Value="12" Text="Diciembre" />
                                                    </Items>
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Mes final
                                            </div>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" runat="server" id="txtmsFinal" readonly="readonly"
                                                    enableviewstate="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 1%;">
                                    <%--   <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Estatus Pedido
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="DdlEstatus" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="T" Text="--Todos--" />
                                                        <dx:BootstrapListEditItem Value="P" Text="Pendiente por Captar" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="C" Text="Pedido Captado" />
                                                        <dx:BootstrapListEditItem Value="X" Text="Pedido Rechazado" />
                                                    </Items>
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Tipo de  Pedido
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="ddlTipoPedido" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="Todos" Text="--Todos--" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Internet" Text="Pedidos Internet" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Orden Centralizada" Text="Pedidos Orden Centralizada" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Orden de Compra" Text="Pedidos Orden de Compra" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Portal de Cliente" Text="Pedidos Portal de Cliente" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Venta Instalada" Text="Pedidos Venta Instalada" />
                                                        <dx:BootstrapListEditItem Value="Pedidos Venta nuevo y/o esporádicos" Text="Pedidos Venta nuevo y/o esporádicos" />
                                                    </Items>
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Vigencia Pedido
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="ddlVigencia" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="3" Text="--Todos --" Selected="true" />
                                                        <dx:BootstrapListEditItem Value="0" Text="Vencido" />
                                                        <dx:BootstrapListEditItem Value="1" Text="Vigente" />
                                                    </Items>
                                                </dx:BootstrapComboBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton CssClass="btn btn-default btn-sm" ID="btnBuscar" runat="server" OnClick="btnBuscar_Click">
                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 1%;">
                    <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-sm" id="btnNuevo" runat="server"
                            onserverclick="btnNuevo_Click">
                            <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Nuevo /o Esporadico
                        </button>
                        <button type="submit" class="btn btn-primary btn-sm" id="btnEditar" runat="server"
                            onserverclick="btnEditar_Click">
                            <i class="fa fa-pencil" aria-hidden="true"></i>&nbsp;Editar
                        </button>
                        <button type="submit" class="btn btn-primary btn-sm" id="BtnRechazados" onserverclick="btnrechazarLista_Click"
                            runat="server">
                            <i class="fa fa-remove" aria-hidden="true"></i>&nbsp;Rechazar Seleccionados
                        </button>
                        <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnExcel"
                            onserverclick="ButtonImprimir_Click">
                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                        </button>
                        <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnImprimir"
                            visible="false" onserverclick="ButtonReporte_Click">
                            <i class="fa fa-print" aria-hidden="true"></i>&nbsp;Imprimir
                        </button>
                        <button type="button" class="btn btn-default btn-sm" runat="server" id="btnProtocolos"
                            onserverclick="btnProtocolos_Click">
                            <i class="fa fa-question" aria-hidden="true"></i>&nbsp;Protocolos Acción
                        </button>
                    </div>
                </div>
                <div id="Tabs" role="tabpanel">
                    <ul class="nav nav-tabs" id="tabPage">
                        <li class="active"><a href="#tabhome" aria-controls="tabhometab" data-toggle="tab" accesskey="G">Pedidos VI (<asp:Label runat="server" ID="lblCPEd" ForeColor="Blue"></asp:Label>)</a> </li>
                        <li><a href="#tabInternet" aria-controls="tabInternettab" data-toggle="tab" accesskey="E">Pedidos Internet (<asp:Label runat="server" ID="lblCPEdInt" ForeColor="Blue"></asp:Label>)</a> </li>
                        <li><a href="#tabOrdenCompra" aria-controls="tabOrdenCompratab" data-toggle="tab" accesskey="I">OC Centralizado (<asp:Label runat="server" ID="lblcPrdOC" ForeColor="Blue"></asp:Label>)</a> </li>
                        <li><a href="#tabPedidoOrdenCompra" aria-controls="tabPedidoOrdenCompratab" data-toggle="tab" accesskey="C">Monitor de Pedidos</a> </li>
                    </ul>
                </div>
                <div class="row" style="margin-top: 1%;">
                    <div class="tab-content">
                        <div id="tabhome" class="tab-pane fade in active">
                            <div class="col-md-12 text-center">
                                <dx:BootstrapGridView ID="gridpedidoVI" ClientInstanceName="grid" runat="server"
                                    KeyFieldName="Id_Acs;pedido;Acs_Semana;Acs_Anio" Width="100%" AutoGenerateColumns="False">
                                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                    <SettingsBehavior AllowSort="true" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                    <SettingsEditing Mode="Batch" />
                                    <Settings ShowHeaderFilterButton="true" />
                                    <SettingsPager PageSize="10" NumericButtonCount="6">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                    </SettingsPager>
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Vigencia" CssClasses-HeaderCell="centerText" HorizontalAlign="Center"
                                            Caption=" " Width="20px" />
                                        <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" Width="20px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Credito" CssClasses-HeaderCell="centerText" Caption="Credito" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Acs" CssClasses-HeaderCell="centerText" Width="20px" Caption="Id Acuerdo" Visible="true" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Estatus" CssClasses-HeaderCell="centerText" Caption="Estatus" Visible="false" />

                                        <dx:BootstrapGridViewTextColumn FieldName="EstatusSOl" CssClasses-HeaderCell="centerText" Caption="Estatus" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Vigencia" CssClasses-HeaderCell="centerText" Caption="Vigencia" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Identificador" CssClasses-HeaderCell="centerText" Caption="Identificador" Width="60px" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" CssClasses-HeaderCell="centerText" Caption="Núm. cte." Width="60px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Nom" CssClasses-HeaderCell="centerText" Caption="Cliente" Width="300px"
                                            HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="id_cteDirEntrega" CssClasses-HeaderCell="centerText" Caption="Dirección" Width="300px"
                                            HorizontalAlign="Left" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Direccion" CssClasses-HeaderCell="centerText" Caption="Dirección" Width="300px"
                                            HorizontalAlign="Left" />


                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ter" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Terr." Width="60px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Cantidad" CssClasses-HeaderCell="centerText" Caption="Venta instalada"
                                            Width="50px" Visible="false">
                                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Semana" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Semana de entrega"
                                            Width="50px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Anio" CssClasses-HeaderCell="centerText" Caption="Año" Visible="false"
                                            Width="50px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_CreditoLetra" CssClasses-HeaderCell="centerText" Caption="Crédito" Width="100px"
                                            Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_VigenciaStr" CssClasses-HeaderCell="centerText" Caption="Vigencia" Width="50px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_EstatusStr" CssClasses-HeaderCell="centerText" Caption="Estatus" Width="50px" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="ID_sol" CssClasses-HeaderCell="centerText" Caption="Solicitud" />
                                        <dx:BootstrapGridViewTextColumn FieldName="EstatusStr" CssClasses-HeaderCell="centerText" Caption="Estatus" />

                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Captar Pedido">
                                            <DataItemTemplate>
                                                <button type="button" class="btn btn-link" title="Captar Pedido" runat="server" onserverclick="btnLoadEditar_Click">
                                                    <span class="fa fa-pencil"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Cancelar">
                                            <DataItemTemplate>
                                                <button id="Button1" type="button" class="btn btn-link" title="Rechazar Pedido" runat="server"
                                                    onserverclick="btnRechazar_Click">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Captar Pedido OC">
                                            <DataItemTemplate>
                                                <button id="BtnOrdenCompra" type="button" class="btn btn-link" title="Orden de Compra" runat="server"
                                                    onserverclick="BtnOrdenCompra_ServerClick">
                                                    <span class="fa fa-folder"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:BootstrapGridView ID="gridpedidoVIProducto" OnCustomSummaryCalculate="GridViewCustomSummary_CustomSummaryCalculate" runat="server" Width="80%" OnBeforePerformDataSelect="detailGrid_DataSelect"
                                                AutoGenerateColumns="False">
                                                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                <SettingsEditing Mode="Batch" />
                                                <Settings ShowHeaderFilterButton="true" />
                                                <SettingsPager PageSize="10" NumericButtonCount="6">
                                                    <Summary Visible="false" />
                                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                                </SettingsPager>
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" CssClasses-HeaderCell="centerText" Visible="false" />
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Right" Caption="Núm." CssClasses-HeaderCell="centerText">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" ReadOnly="true" CssClasses-DataCell="leftText"
                                                        Caption="Producto">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Presen.">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Unidad">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Cant.">
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Precio vta.">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        ReadOnly="true" Caption="Importe">
                                                        <PropertiesTextEdit DisplayFormatString="{0:C}">
                                                        </PropertiesTextEdit>
                                                    </dx:BootstrapGridViewTextColumn>
                                                </Columns>
                                                <TotalSummary>

                                                    <dx:ASPxSummaryItem FieldName="Prd_Precio" SummaryType="Custom" DisplayFormat="Total:" />
                                                    <dx:ASPxSummaryItem FieldName="Prd_Importe" SummaryType="Sum" DisplayFormat="c2"
                                                        Tag="total" />
                                                </TotalSummary>
                                            </dx:BootstrapGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                        <div class="tab-pane" id="tabInternet">
                            <div class="col-md-12 text-center">
                                <dx:BootstrapGridView ID="rgInternet" runat="server" Width="100%" AutoGenerateColumns="False" OnCustomButtonInitialize="rgInternet_CustomButtonInitialize"
                                    KeyFieldName="Id_Emp;Id_Cd;Num_Pedido;tipoPedido">
                                    <SettingsBehavior AllowSort="true" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                    <SettingsEditing Mode="Batch" />
                                    <Settings ShowHeaderFilterButton="true" />
                                    <SettingsPager PageSize="10" NumericButtonCount="6">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                    </SettingsPager>
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="UnidadNegocio_Id" CssClasses-HeaderCell="centerText" Caption="UnidadNegocio_Id" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Estatus_Id" CssClasses-HeaderCell="centerText" Caption="Estatus_Id" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Credito" CssClasses-HeaderCell="centerText" Caption="crédito" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Emp" CssClasses-HeaderCell="centerText" Caption="Id_Emp" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cd" CssClasses-HeaderCell="centerText" Caption="Id_Cd" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ter" CssClasses-HeaderCell="centerText" Caption="Id_Ter" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Num_Pedido" CssClasses-HeaderCell="centerText" Caption="Núm. Requisición" HorizontalAlign="Right" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" CssClasses-HeaderCell="centerText" Caption="Núm. cte." HorizontalAlign="Right" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_NomComercial" CssClasses-HeaderCell="centerText" Caption="Nombre Cliente" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="UnidadNegocio_Nombre" CssClasses-HeaderCell="centerText" Caption="Nombre Unidad de Negocio" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Observaciones" CssClasses-HeaderCell="centerText" Caption="Observaciones" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Nombre_Usuario" CssClasses-HeaderCell="centerText" Caption="Usuario" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewDateColumn FieldName="Fecha_Requisicion" CssClasses-HeaderCell="centerText" Caption="Fecha de Requisicion" HorizontalAlign="Center">
                                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yy}" EditFormatString="{0:dd/MM/yy}">
                                            </PropertiesDateEdit>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_CreditoLetra" CssClasses-HeaderCell="centerText" Caption="Crédito" HorizontalAlign="Center" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Total" CssClasses-HeaderCell="centerText" Caption="Total" HorizontalAlign="Right" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Estatus_Nombre" CssClasses-HeaderCell="centerText" Caption="Estatus" HorizontalAlign="Right" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ped" CssClasses-HeaderCell="centerText" Caption="Pedido" HorizontalAlign="Right" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="tipoPedido" CssClasses-HeaderCell="centerText" Caption="tipoPedido" HorizontalAlign="Right" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Url" CssClasses-HeaderCell="centerText" Caption="url" Visible="false" />

                                        <dx:BootstrapGridViewCommandColumn Width="80px" Caption="Descargar O.C.">
                                            <CustomButtons>
                                                <dx:BootstrapGridViewCommandColumnCustomButton IconCssClass="fa fa-download" ID="btnInternetOC" />
                                            </CustomButtons>
                                        </dx:BootstrapGridViewCommandColumn>
                                        <dx:BootstrapGridViewDataColumn Width="80px" Caption="Captar">
                                            <DataItemTemplate>
                                                <button type="button" id="btnCaptarPInternet" class="btn btn-link" title="Captar Pedido" runat="server" onserverclick="btnCaptarPInternet_ServerClick">
                                                    <span class="fa fa-pencil"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="80px" Caption="Imprimir" Visible="false">
                                            <DataItemTemplate>
                                                <button id="btnImpPInternet" type="button" class="btn btn-link" title="Imprimir" runat="server"
                                                    onserverclick="Button1_ServerClick">
                                                    <span class="fa fa-print"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="40px" Caption="Cancelar">
                                            <DataItemTemplate>
                                                <button id="BtnCancelaPedido" type="button" class="btn btn-link" title="Rechazar Pedido" runat="server"
                                                    onserverclick="BtnCancelaPedido_ServerClick">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDataColumn>
                                    </Columns>
                                    <ClientSideEvents CustomButtonClick="onCustomButtonClick" />
                                    <Templates>
                                        <DetailRow>
                                            <dx:BootstrapGridView ID="gridpedidoVIInternetProducto" OnCustomSummaryCalculate="GridViewCustomSummary_CustomSummaryCalculate" runat="server" Width="80%" OnBeforePerformDataSelect="gridpedidoVIInternetProducto_BeforePerformDataSelect"
                                                AutoGenerateColumns="False">
                                                <SettingsPager PageSize="5">
                                                </SettingsPager>
                                                <Settings ShowFooter="True" />
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" CssClasses-HeaderCell="centerText" Visible="false" />
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Right" Caption="Núm." CssClasses-HeaderCell="centerText">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" ReadOnly="true" CssClasses-DataCell="leftText"
                                                        Caption="Producto">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Presen.">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Unidad">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Cant.">
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Precio vta.">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        ReadOnly="true" Caption="Importe">
                                                        <PropertiesTextEdit DisplayFormatString="{0:C}">
                                                        </PropertiesTextEdit>
                                                    </dx:BootstrapGridViewTextColumn>
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="Prd_Precio" SummaryType="Custom" DisplayFormat="Total:" />
                                                    <dx:ASPxSummaryItem FieldName="Prd_Importe" SummaryType="Sum" DisplayFormat="c2"
                                                        Tag="total" />
                                                </TotalSummary>
                                            </dx:BootstrapGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                    <FormatConditions>
                                        <dx:GridViewFormatConditionHighlight FieldName="tipoPedido" Expression="[tipoPedido] = 1" Format="LightGreenFill" ApplyToRow="true" />
                                    </FormatConditions>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                        <div class="tab-pane" id="tabOrdenCompra">
                            <div class="col-md-12 text-center">
                                <dx:BootstrapGridView ID="rgDatosOC" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="Id_OC">
                                    <SettingsBehavior AllowSort="true" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                    <SettingsEditing Mode="Batch" />
                                    <Settings ShowHeaderFilterButton="true" />
                                    <SettingsPager PageSize="10" NumericButtonCount="6">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                    </SettingsPager>
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_OC" CssClasses-HeaderCell="centerText" Caption="Núm OC Centralizada" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Estatus" CssClasses-HeaderCell="centerText" Caption="Estatus" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="EstatusSOl" CssClasses-HeaderCell="centerText" Caption="Estatus" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id" CssClasses-HeaderCell="centerText" Caption="Núm Pedido" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="ID_sol" CssClasses-HeaderCell="centerText" Caption="Solicitud" Visible="false" />
                                        <dx:BootstrapGridViewDateColumn FieldName="FechaAlta" CssClasses-HeaderCell="centerText" Caption="Fecha" HorizontalAlign="Center">
                                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yy}" EditFormatString="{0:dd/MM/yy}">
                                            </PropertiesDateEdit>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" CssClasses-HeaderCell="centerText" Caption="Num. Cliente" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_NomComercial" CssClasses-HeaderCell="centerText" Caption="Nombre Cliente" HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ter" CssClasses-HeaderCell="Total" Caption="Territorio" HorizontalAlign="Center" />
                                        <dx:BootstrapGridViewTextColumn FieldName="VentaInstalada" CssClasses-HeaderCell="centerText" Caption="Total">
                                            <PropertiesTextEdit DisplayFormatString="{0:C}">
                                            </PropertiesTextEdit>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ped" CssClasses-HeaderCell="centerText" Caption="Pedido" />
                                        <dx:BootstrapGridViewTextColumn FieldName="EstatusStr" CssClasses-HeaderCell="centerText" Caption="Estatus" />
                                        <dx:BootstrapGridViewDateColumn Width="80px" Caption="Ver Remisiones">
                                            <DataItemTemplate>
                                                <button id="BtnRemisionOC" type="button" class="btn btn-link" title="Remisiones"
                                                    runat="server" onserverclick="BtnRemisionOC_ServerClick">
                                                    <span class="fa fa-search"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Width="80px" Caption="Captar">
                                            <DataItemTemplate>
                                                <button id="CaptarImgInternetOC" type="button" class="btn btn-link" title="Captar Pedido"
                                                    runat="server" onserverclick="CaptarImgInternetOC_ServerClick">
                                                    <span class="fa fa-pencil"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                        <div class="tab-pane" id="tabPedidoOrdenCompra">
                            <div class="col-md-12 text-center">
                                <dx:BootstrapGridView ID="gridviewOrderCompra" ClientInstanceName="grid" runat="server" OnCustomButtonInitialize="gridviewOrderCompra_CustomButtonInitialize"
                                    KeyFieldName="Id_Acs;pedido;Acs_Semana;Acs_Anio" Width="120%" AutoGenerateColumns="False">
                                    <SettingsBehavior AllowSort="true" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                    <SettingsEditing Mode="Batch" />
                                    <Settings ShowHeaderFilterButton="true" />
                                    <SettingsPager PageSize="10" NumericButtonCount="6">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                    </SettingsPager>
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="pedido" Caption="Núm. Pedido" CssClasses-HeaderCell="centerText" Width="60px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Credito" CssClasses-HeaderCell="centerText" Caption="Credito" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Acs" CssClasses-HeaderCell="centerText" Width="20px" Caption="Id Acuerdo" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Estatus" CssClasses-HeaderCell="centerText" Caption="Estatus" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Vigencia" CssClasses-HeaderCell="centerText" Caption="Vigencia" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" CssClasses-HeaderCell="centerText" Caption="Núm. cte." Width="60px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Nom" CssClasses-HeaderCell="centerText" Caption="Cliente" Width="250px"
                                            HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="id_cteDirEntrega" CssClasses-HeaderCell="centerText" Caption="Dirección" Width="250px"
                                            HorizontalAlign="Left" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Direccion" CssClasses-HeaderCell="centerText" Caption="Dirección" Width="300px"
                                            HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ter" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Terr." Width="60px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Cantidad" CssClasses-HeaderCell="centerText" Caption="Venta instalada"
                                            Width="50px" Visible="false">
                                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Semana" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Semana de entrega"
                                            Width="50px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Anio" CssClasses-HeaderCell="centerText" Caption="Año" Visible="false"
                                            Width="50px" />

                                        <dx:BootstrapGridViewTextColumn FieldName="OrdenCompra" Caption="Orden de Compra" CssClasses-HeaderCell="centerText" Width="120px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_CreditoLetra" CssClasses-HeaderCell="centerText" Caption="Crédito" Width="100px"
                                            Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="str_Tipo_pedido" Caption="Tipo de Pedido" CssClasses-HeaderCell="centerText" Width="100px" Visible="true" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Acs_EstatusStr" CssClasses-HeaderCell="centerText" Caption="Estatus" Width="50px" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Ped" Caption="Id_Ped" CssClasses-HeaderCell="centerText" Visible="false" />
                                        <dx:BootstrapGridViewTextColumn FieldName="IsTieneOC" Caption="TieneOC" Visible="false" />
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Rastreo">
                                            <DataItemTemplate>
                                                <button id="Button2" type="button" class="btn btn-link" title="Rastreo de documentos"
                                                    runat="server" onserverclick="Button2_ServerClick1">
                                                    <span class="fa fa-book"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewCommandColumn Width="40px" Caption="Descargar O.C.">
                                            <CustomButtons>
                                                <dx:BootstrapGridViewCommandColumnCustomButton IconCssClass="fa fa-download" ID="btnDownload" />
                                            </CustomButtons>
                                        </dx:BootstrapGridViewCommandColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Imprimir">
                                            <DataItemTemplate>
                                                <button id="Button3" type="button" class="btn btn-link" title="Imprimir Pedido" runat="server"
                                                    onserverclick="Button1_ServerClick2">
                                                    <span class="fa fa-print"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Cancelar">
                                            <DataItemTemplate>
                                                <button id="Button4" type="button" class="btn btn-link" title="Rechazar Pedido" runat="server"
                                                    onserverclick="btnRechazar_Click">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Captar Pedido OC">
                                            <DataItemTemplate>
                                                <button id="Button5" type="button" class="btn btn-link" title="Orden de Compra" runat="server"
                                                    onserverclick="BtnOrdenCompra_ServerClick">
                                                    <span class="fa fa-folder"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>

                                    </Columns>
                                    <ClientSideEvents CustomButtonClick="gridviewOrderCompra_onCustomButtonClick" />
                                    <Templates>
                                        <DetailRow>
                                            <dx:BootstrapGridView ID="gridpedidoVIProductoOrdenCompra" OnCustomSummaryCalculate="GridViewCustomSummary_CustomSummaryCalculate" runat="server" Width="80%" OnBeforePerformDataSelect="detailGrid_DataSelect2"
                                                AutoGenerateColumns="False">
                                                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                <SettingsEditing Mode="Batch" />
                                                <Settings ShowHeaderFilterButton="true" />
                                                <SettingsPager PageSize="10" NumericButtonCount="6">
                                                    <Summary Visible="false" />
                                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                                </SettingsPager>
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" CssClasses-HeaderCell="centerText" Visible="false" />
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Right" Caption="Núm." CssClasses-HeaderCell="centerText">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" ReadOnly="true" CssClasses-DataCell="leftText"
                                                        Caption="Producto">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Presen.">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" HorizontalAlign="Center" CssClasses-HeaderCell="centerText"
                                                        ReadOnly="true" Caption="Unidad">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Cant.">
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Precio vta.">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        ReadOnly="true" Caption="Importe">
                                                        <PropertiesTextEdit DisplayFormatString="{0:C}">
                                                        </PropertiesTextEdit>
                                                    </dx:BootstrapGridViewTextColumn>
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="Prd_Precio" SummaryType="Custom" DisplayFormat="Total:" />
                                                    <dx:ASPxSummaryItem FieldName="Prd_Importe" SummaryType="Sum" DisplayFormat="c2"
                                                        Tag="total" />
                                                </TotalSummary>
                                            </dx:BootstrapGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                <div style="display: none">
                    <asp:Button runat="server" OnClick="btnAbrirDetalle_Click" ID="btnHiddenabrirDetalle" />
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="visibility: hidden">
            <asp:Button ID="btnDescargarOC" runat="server" OnClick="btnDescargarOC_Click" />
        </div>
        <asp:HiddenField runat="server" ID="hdnidcliente" />
        <asp:HiddenField runat="server" ID="hdnnombrecliente" />
        <asp:HiddenField runat="server" ID="hdneditarPedido" />
        <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
        <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
        <asp:HiddenField ID="HD_Semana" runat="server" Value="0" />
        <asp:HiddenField ID="HD_Anio" runat="server" Value="0" />
        <asp:HiddenField ID="HD_Frec" runat="server" Value="0" />
        <asp:HiddenField ID="HPermisoGuardar" runat="server" Value="" />
        <asp:HiddenField ID="HPermisoModificar" runat="server" Value="" />
        <asp:HiddenField ID="HPermisoEliminar" runat="server" Value="" />
        <asp:HiddenField ID="HPermisoImprimir" runat="server" Value="" />
        <asp:HiddenField runat="server" ID="hdnid" />
        <asp:HiddenField ID="hdnDescargaOC" runat="server" />
        <asp:HiddenField ID="TabName" runat="server" />
        <div style="display: none">
            <span id="lblcte" runat="server"></span>
        </div>
        <div id="modalCliente" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        Pedido nuevo y/o esporádico
                    </div>
                    <div class="modal-body" style="padding: 25px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9 z-depth-1-half" style="padding-bottom: 33% !Important;">
                            <iframe class="embed-responsive-item" id="FrameClientenuevo" runat="server"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalEditar" class="modal" data-toggle="modal" style="z-index: 2220!important" role="dialog">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <!-- Modal content-->
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                Pedido a editar
                            </div>
                            <div class="modal-body" style="padding: 25px !important;" id="Div3">
                                <div class="col-md-3">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1">
                                        Pedido
                                    </div>
                                    <div class="col-md-5">
                                        <input type="text" class="form-control" runat="server" id="txtPedido" maxlength="9" />
                                    </div>
                                </div>
                                <div id="successAndErrorMessages">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="submit" class="btn btn-default btn-sm" id="btnedita" runat="server"
                                    onclick="consultareditarpedido()">
                                    <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                </button>
                                <button type="submit" class="btn btn-default btn-sm" id="btnconsultareditar" runat="server"
                                    onserverclick="btnconusltareditarPedido_Click" style="display: none">
                                    Consultar
                                </button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="modalAcysMensaje" class="modal" data-toggle="modal" role="dialog" tabindex="-1"
            style="z-index: 5220!important;" style="display: none;">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                    </div>
                    <div class="modal-body" style="padding: 25px !important;" id="Div11">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                            </div>
                            <div class="col-md-10">
                                <span id="lblmensaje" runat="server"></span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-md-12 ">
                                <table>
                                    <tr>
                                        <td>
                                            <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                                Ok</button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="modalImprimir" data-toggle="modal" style="height: 800px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h4">Visor de Reporte
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="iframeVisorReporte" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="dvDialogoInicioSesion" tabindex="-1" role="dialog" data-toggle="modal"
            aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="btndvDialogoInicioSesionCerrar" type="button" class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H7">Iniciar sesion
                        </h4>
                    </div>
                    <div class="modal-body" style="padding: 25px !important;">
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
                            onclick="login_ajax(jQuery)">
                            Confirmar
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal modal-fullscreen" id="Detalles" data-toggle="modal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        Captación de Pedidos
                    </div>
                    <div class="modal-body" style="padding: 25px !important;" id="Div10">
                        <div class="modal-iframe-wrapper">
                            <iframe class="embed-responsive-item" id="frameDetalle" runat="server"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="Remisiones" data-toggle="modal" style="height: 450px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h1">Ver Remisiones
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="IRemisiones" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="ModalRepHPedido" data-toggle="modal" style="height: 800px !important"
            role="dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h5">Reporte de Pedidos
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="FrameIRepHPedido" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div id="ModalRastreo" style="height: 350px !important;" data-toggle="modal" class="modal"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <div class="col-md-10">
                            <h4 id="h2">Rastreo de Documentos
                            </h4>
                        </div>
                    </div>
                    <div>
                        <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 35% !important;">
                            <iframe class="embed-responsive-item" id="iFrameRastreo" style="height: 200px !important;"
                                runat="server"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="protocolos" data-backdrop="static" data-keyboard="false fade" data-toggle="modal"
            class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important; display: none;">
            <div class="modal-dialog" role="document" style="height: 120px !important;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <div class="col-md-12">
                            <h4 id="h3">Protocolos
                            </h4>
                        </div>
                    </div>
                    <div class="embed-responsive embed-responsive-16by9 z-depth-1-half">
                        <iframe class="embed-responsive-item" src="VentanaProtocolos.aspx"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal modal-fullscreen" id="Imprimir" data-toggle="modal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <div class="col-md-12">
                            <h4 id="print">Reporte
                            </h4>
                        </div>
                    </div>
                    <div class="modal-body" style="padding: 25px !important;" id="IFrameImprimirPed">
                        <div class="modal-iframe-wrapper">
                            <iframe class="embed-responsive-item" id="IFrameImprimirPEdidos" runat="server"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
