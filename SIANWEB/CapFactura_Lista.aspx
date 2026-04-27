<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapFactura_Lista.aspx.cs" Inherits="SIANWEB.CapFactura_Lista" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"> 
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

       // Broad cast that your're opening a page.
        localStorage.openpages = Date.now();
        var onLocalStorageEvent = function(e){
            if(e.key == "openpages"){
                // Listen if anybody else opening the same page!
                localStorage.page_available = Date.now();
            }
            if(e.key == "page_available"){
                alert("No se puede tener abierta más de una sesion de Facturas o tener abierta la opcion de Devolución de Remisiones o Facturación de Venta Instalada");

		window.history.back();


            }
        };


        window.addEventListener('storage', onLocalStorageEvent, false);


            // ---------------------
            // Variables de permiso
            // ---------------------

            var permisoGuardar = '<%= PermisoGuardar %>'
            var permisoModificar = '<%= PermisoModificar %>'
            var permisoEliminar = '<%= PermisoEliminar %>'
            var permisoImprimir = '<%= PermisoImprimir %>'

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                //validar rango correcto de fechas.
                if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                    var mensage = 'La fecha inicial no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 330, 150, tituloMensajes);
                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');

                var notaInicio = 0;
                if (txtCliente1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtCliente1.get_textBoxValue());
                }

                var notaFin = 0;
                if (txtCliente2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtCliente2.get_textBoxValue());
                }

                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('El cliente inicial no debe ser mayor al cliente final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtCliente1.focus();
                    });
                    return false;
                }

                var txtFactura1 = $find('<%= txtFactura1.ClientID %>');
                var txtFactura2 = $find('<%= txtFactura2.ClientID %>');

                notaInicio = 0;
                if (txtFactura1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtFactura1.get_textBoxValue());
                }

                notaFin = 0;
                if (txtFactura2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtFactura2.get_textBoxValue());
                }

                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('La factura inicial no debe ser mayor a la factura final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtFactura1.focus();
                    });
                    return false;
                }

                var txtPedido1 = $find('<%= txtPedido1.ClientID %>');
                var txtPedido2 = $find('<%= txtPedido2.ClientID %>');

                notaInicio = 0;
                if (txtPedido1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtPedido1.get_textBoxValue());
                }

                notaFin = 0;
                if (txtPedido2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtPedido2.get_textBoxValue());
                }

                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('El pedido inicial no debe ser mayor al pedido final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtPedido1.focus();
                    });
                    return false;
                }
                return true;
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------

            function Pruebas() {
                
                //Ingresamos un mensaje a mostrar
                var mensaje = confirm("¿Te gusta Desarrollo Geek?");
                //Detectamos si el usuario acepto el mensaje
                if (mensaje) {
                    alert("¡Gracias por aceptar!");
                }
                //Detectamos si el usuario denegó el mensaje
                else {
                    alert("¡Haz denegado el mensaje!");
                }
            }

            function ToolBar_ClientClick(sender, args) {
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'new':
                        continuarAccion = false;
                        refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                        AbrirVentana_Factura(0, 0, 0, '1', permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                        break;

                    case 'facPedido':
                        refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                        AbrirVentana_FacturaPedido();
                        continuarAccion = false;
                        break;

                    case 'facRemision':
                        refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                        AbrirVentana_FacturaRemisiones();
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function AbrirFacturaPDF(oWnd, eventArgs) {
                var oWnd1 = radopen(oWnd.argument, "AbrirVentana_ImpresionPDFFactura");
                oWnd1.set_showOnTopWhenMaximized(false);
                oWnd1.center();
                oWnd.remove_close(AbrirFacturaPDF);
            }
            function AbrirFacturaPDFCN(WebURL, WebURLCN) {
               
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                if (WebURLCN != '') {
                    oWnd.argument = WebURLCN
                    oWnd.add_close(AbrirFacturaPDF);
                }
                oWnd.center();
            }
            function Mensaje(oWnd, eventArgs) {
                //Your code here.
                //Remove the OnClientClose function to avoid
                //adding it for a second time when the window is shown again.
                alert('');
                oWnd.remove_close(Mensaje);
            }

            function AbrirVentana_FacturaPedido() {
                var oWnd = radopen("CapFacturaPedido.aspx", "AbrirVentana_FacturaPedido");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirVentana_FacturaRemisiones() {
                var oWnd = radopen("CapFacturaRemisiones.aspx", "AbrirVentana_FacturaRemisiones");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }


            function confirmCallBackFn(arg) {

                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg == true) {
                    ajaxManager.ajaxRequest("Reenviarcorreo");
                }
            }

            function RefacturaEliminar() {

                //Id_Emp,Id_Cd,Id_Fac
                var oWnd = radopen("VentanaFacturas.aspx");
                oWnd.center();
            }

            function RefacturaEliminarMotivo(Id_Emp, Id_Cd, Id_Fac, NotificarCorreo) {
                var oWnd = radopen("VentanaFacturasMotivo.aspx?Id_Emp=" + Id_Emp + "&Id_Cd=" + Id_Cd + "&Id_Fac=" + Id_Fac + "&NotificarCorreo=" + NotificarCorreo);
                oWnd.set_width(650);
                oWnd.set_height(450);
                oWnd.center();
            }

            //Abrir la ventana de depuracion de facturas
            function AbrirVentana_FacturaDepurar(Id_Emp, Id_Cd, Id_Fac) {
                var oWnd = radopen("ventana_FacIncobrable.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    , "AbrirVentana_FacturaDepurar");
                oWnd.center();
            }

            function OpenAlert(mensaje, Id_Emp, Id_Cd, Id_Fac_Editar, facModificable) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                    });
            }
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Factura_Edicion(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable) {
                AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                var oWnd = radopen("CapFactura.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&facModificable=" + facModificable
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    + "&reFactura=0"
                    , "AbrirVentana_Factura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();


            }


            function AbrirVentana_EnviarDocumentos(Id_Emp, Id_Cd, Id_Fac) {
                var oWnd = radopen("Ventana_EnviarDocumentos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Doc=" + Id_Fac
                    + "&Tipo=FACTURA"
                    , "AbrirVentana_EnviarDocumentos");
                oWnd.center();
            }



            //--------------------------------------------------------------------------------------------------
            //Abre la ventana para eliminar factura y cancelar la venta
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_eliminafactura(Id_Emp, Id_Cd, Id_Fac) {
                var oWnd = radopen("Profactura_Eliminar.aspx?Id_Emp=" + Id_Emp
                + "&Id_Cd=" + Id_Cd
                + "&Id_Fac=" + Id_Fac);
            }
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura para refacturar cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Factura_EdicionRefacturar(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable) {
                AbrirVentana_FacturaRefacturar(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura para refacturar
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_FacturaRefacturar(Id_Emp, Id_Cd, Id_Fac, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                var oWnd = radopen("CapFactura.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&facModificable=" + facModificable
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    + "&reFactura=1"
                    , "AbrirVentana_Factura");
                // oWnd.restore();
                //oWnd.center();
                //oWnd.show();
                oWnd.Maximize();
                //HideOverlay();
            }
            //            function HideOverlay() {
            //                var overlay = $telerik.$('div.TelerikModalOverlay');
            //                if (overlay != null) {
            //                    overlay.hide();
            //                }
            //            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_Fac(accion) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    refreshGrid_Fac('RebindGrid');
                }
                else {
                    refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                }
            }

            function LimpiarBanderaRebind(sender, eventArgs) {
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_FacturaPedido() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('FacturaPedido');
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura-Pedido se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_FacturaPedido(sender, eventArgs) {

                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_FacturaPedido.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    ModificaBanderaRebind_FacturaPedido('0');
                    //a diferencia de la facturacion de remisiones en la que si se requiere ir al servidor para validar
                    //la variable de sesion de facturacion de remisiones, aqui se invoca directamente la pantalla de 
                    //facturacion ya que en ella se valida directamente si el usuario eligio un pedido haciendo clic en el boton 'Aceptar'
                    //de la pantalla de 'factura pedido'. Si el pedido era válido, la variable de sesion de pedido trae un valor de lo cntrario en nula.
                    AbrirVentana_Factura(0, 0, 0, '1', permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                }
            }

            function LimpiarBanderaRebind_FacturaPedido(sender, eventArgs) {
                ModificaBanderaRebind_FacturaPedido('0');
            }

            function ActivarBanderaRebind_FacturaPedido() {
                ModificaBanderaRebind_FacturaPedido('1');
            }

            function ModificaBanderaRebind_FacturaPedido(valor) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_FacturaPedido.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_FacturaRemisiones() {

                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('FacturaRemisiones');
            }

        <%--    /*
            function refreshGrid_FacturaRemisiones() {

            var ajaxManager = $find("<%= RAM1.ClientID %>");
            ajaxManager.ajaxRequest('FacturaDepuracion');
            }
            */--%>
            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura-Remisiones se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_FacturaRemisiones(sender, eventArgs) {
                refreshGrid_FacturaRemisiones();
            }

            function LimpiarBanderaRebind_FacturaRemisiones(sender, eventArgs) {
                ModificaBanderaRebind_FacturaRemisiones('0');
            }

            function ActivarBanderaRebind_FacturaRemisiones() {
                ModificaBanderaRebind_FacturaRemisiones('1');
            }

            function ModificaBanderaRebind_FacturaRemisiones(valor) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_FacturaRemisiones.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //cuando el campo de texto pirde el foco
            function txtCliente1_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente1.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtCliente2_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente2.ClientID %>'));
            }

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, 'XML', opciones);
            }

            function abrirArchivoCN(pagina, paginaCN) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, 'XML', opciones);
                window.open(paginaCN, 'XML Cuenta Nacional', opciones);
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$rgFactura$ctl00$ctl02$ctl00$ImgExportar") != -1)
                    args.set_enableAjax(false);
            }

            function SeleccinarOpcion(param) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }

            function CheckAllTimbrar(sender) {
                var grid = $find('<%=rgFactura.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;
                var importeTotal = 0;
                var importe;
                var totalChk = 0;
                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    var chk = row.findElement("ChkTimbrar");
                    var estatus = row.get_cell('Fac_EstatusStr').innerText;

                    if (chk != null) {
                        //chk.checked = sender.checked;
                        if (sender.checked) {
                            if (estatus.includes("Capturado") || estatus.includes("Embarque") || estatus.includes("Entregado") || estatus.includes("Solicitado")) {
                                chk.checked = sender.checked;
                                totalChk++;
                            }
                        } else {
                            chk.checked = sender.checked;
                        }
                    }
                }
                if (totalChk == 0) {
                    // seleccoone facturas con estatus 
                    radalert("La lista no contiene facturas con estatus Capturado. Si requiere ver el archivo pdf seleccione la columna PDF.", 380, 200);
                    sender.checked = false;
                }
            }
            //RBM Marzo-2023
            //Se invoca administrador de CFDI
            function AbrirVentana_CFDITimbre(Id_Doc, Id_Cte, Nombre, CodigoPostal, Colonia, TipoDoc, permitirModificar) {
                debugger;
                AbrirVentana_CFDITimbre(Id_Doc, Id_Cte, CodigoPostal, Colonia, TipoDoc, permitirModificar);
                return false;
            }

            var folioDescargaPDF = 0;

            function CheckItem(sender) {
                var v = $(sender).closest("tr");
                var rowColumns = v.querySelectorAll("td");
                var estatus = rowColumns[8].innerHTML;
                var folio = rowColumns[4].innerHTML;

                if (!(estatus.includes("Capturado"))) {
                    if (estatus.includes("Impreso") || estatus.includes("Embarque") || estatus.includes("Entregado")) {
                        //radalert("El estatus de la factura es " + estatus + " no se puede timbrar, ¿Desea descargar el archivo PDF?", 380, 200);
                        //console.log("timbra y descarga");
                        folioDescargaPDF = folio;
                        radconfirm("El estatus de la factura es " + estatus + " no se puede timbrar, ¿Desea descargar el archivo PDF?", ConfirmDescargaPDF, 400, 160);
                        sender.checked = false;  
                        return false;
                    }
                    else if (estatus.includes("Baja")) {
                        radalert("Documento con estatus invalido y no es posible mandar a timbrar, " + estatus + ".", 380, 200);
                    } else {
                        radalert("El estatus de la factura seleccionada no es valido para timbrar. Folio " + folio + " estatus " + estatus + ".", 380, 200);
                    }
                    sender.checked = false;                     
                }
            }

            function ConfirmDescargaPDF(arg) {
                if (arg) {
                    var hdnIdFacPdf = document.getElementById("<%=hdnIdFacPDF.ClientID%>");
                    hdnIdFacPdf.value = folioDescargaPDF;
                    var e = document.getElementById("<%=btnSoloDescargarPDF.ClientID%>");
                    e.click();
                }
            }
                
            function AbrirVentana_CFDITimbre(Id_Doc, Id_Cte, Nombre, CodigoPostal, Colonia, TipoDoc, permitirModificar) {
                var oWnd = radopen("CapCFDITimbres.aspx?Id_Doc=" + Id_Doc
                    + "&Id_Cte=" + Id_Cte
                    + "&Nombre=" + Nombre
                    + "&CodigoPostal=" + CodigoPostal
                    + "&Colonia=" + Colonia
                    + "&TipoDoc=" + TipoDoc
                    + "&permitirModificar=" + permitirModificar
                    , "AbrirVentana_CFDITimbre");
                oWnd.center();
                oWnd.Maximize();
            }

            var contadorCiclo = 1;

            // Funcion recursiva controlada, con segundos de espera entre peticiones y espera de respuesta http.
            // finaliza al notificar termino de proceso en segundo plano o no existe pendiente en segundo plano.
            function FacturasSegundoPlano(nPeticion) {
                var idUsuario = document.getElementById('<%= HD_IdUser.ClientID %>').value;
                var etiquetaMensaje = document.getElementById('<%= lblSegundoPlano.ClientID %>');
                var urlApp = "<%=ApplicationUrl %>" + "/api/FacturaMonitoreo/ConsultarFacturas";

                //eval("radalert('consultando monitor segundo plano', 330, 150);");

                fetch(urlApp, {
                    method: "POST",
                    mode: "cors",
                    cache: "no-cache",
                    credentials: "same-origin",
                    redirect: "follow",
                    referrer: "no-referrer",
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(function (response) {
                    if (response.status !== 404 && response.status !== 500) {
                        return response.json();
                    }
                    return Promise.reject(response.statusText)
                }).then(function (data) {

                    console.log(data.dataResultado);
                    
                    if (data.intActivo > 0 ) {                        
                        //etiquetaMensaje.Text = data.strRespuesta;
                        etiquetaMensaje.innerHTML = data.strRespuesta;
                        //console.log("Volver a consultar, peticion: " + nPeticion);
                        // consulta recursiva
                        setTimeout(function () {
                            contadorCiclo = nPeticion + 1;
                            FacturasSegundoPlano(contadorCiclo);
                        }, 8000);
                    } else {
                        let strMensjeError = "";
                        var i = 0;
                        let ancho;
                        if (data.dataResultado.length > 0) {
                            for (; i < data.dataResultado.length; i++) {

                                if (data.dataResultado[i].strRstScript.length>0) {
                                    eval(data.dataResultado[i].strRstScript);
                                }

                                if (data.dataResultado[i].strRstAlerta.length > 0) {
                                    strMensjeError += data.dataResultado[i].strRstAlerta + "<br>";
                                }

                                if (data.dataResultado[i].DescargarPDF == 1) {
                                    if (data.dataResultado[i].strUrlPdf.length > 0) {
                                        //AbrirFacturaPDF(data.dataResultado[i].strUrlPdf, ''); // data.dataResultado[i].strRstAlerta + "<br>";
                                        let filename = data.dataResultado[i].strUrlPdf.replace(/^.*[\\/]/, '');
                                        let anchor = document.createElement('a');
                                        anchor.href = data.dataResultado[i].strUrlPdf;
                                        anchor.target = '_blank';
                                        anchor.download = filename;
                                        anchor.click();
                                    }
                                }

                            }

                            if (strMensjeError.length > 0) {
                                ancho = ((100 + i) + 150);
                                strMensjeError = "Error al timbrar documentos: <br><br>" + strMensjeError;
                                radalert(strMensjeError, 500, ancho);
                            }

                            SeleccinarOpcion("RebindGrid");

                        }else if (contadorCiclo > 1) {                            
                            radalert(data.strRespuesta, 330, 150); 
                            SeleccinarOpcion("RebindGrid");
                        } 

                        etiquetaMensaje.innerHTML = "";                        
                        console.log("Fin");
                        contadorCiclo = 1;
                    }
                        
                }).catch(function (error) {

                    if (typeof error !== "undefined") {
                        console.log(error);
                    }

                }).finally(function () {

                });


            };

            window.addEventListener("load", (event) => {
                FacturasSegundoPlano(contadorCiclo);
            });

            function ActivaDescargarPDF() {
                console.log("confirma");
                radconfirm("¿Desea abrir los Pdf al terminar el proceso?\n", ConfirmTimbres, 400, 160);
                return false;
            }

            function ConfirmTimbres(arg) {
                //var ajaxManager = $find(" RAM1.ClientID ");
                if (arg) {
                    
                    //ajaxManager.ajaxRequest('TimbrarDescargar');
                    var e = document.getElementById("<%=btnTimbrarDescargar.ClientID%>");
                    e.click();
                }
                else {
                    //ajaxManager.ajaxRequest('SoloTimbrar');
                    console.log("Solo timbra");

                    var e = document.getElementById("<%=btnSoloTimbrar.ClientID%>");
                    e.click();
                }
                // btnSoloTimbrar 
               

              }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFactura">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="btnSoloTimbrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnTimbrarDescargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnSoloDescargarPDF">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <%-- Factura (Detalle de factura) --%>
            <telerik:RadWindow ID="AbrirVentana_Factura" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Factura"
                Modal="True" OnClientClose="CerrarWindow_ClientEvent" OnClientPageLoad="LimpiarBanderaRebind"
                Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
                InitialBehaviors="Maximize">
            </telerik:RadWindow>
            <%-- Factura Pedido --%>
            <telerik:RadWindow ID="AbrirVentana_FacturaPedido" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="350px" Height="200px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Facturar pedido" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_FacturaPedido" OnClientPageLoad="LimpiarBanderaRebind_FacturaPedido">
            </telerik:RadWindow>
            <%-- Factura Remisiones --%>
            <telerik:RadWindow ID="AbrirVentana_FacturaRemisiones" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="500px" Height="400px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Facturar remisiones" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_FacturaRemisiones" OnClientPageLoad="LimpiarBanderaRebind_FacturaRemisiones">
            </telerik:RadWindow>
            <%-- Factura Depurar --%>
             <telerik:RadWindow ID="AbrirVentana_FacturaDepurar" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="400px" Height="350px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Depurar factura" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_FacturaRemisiones" OnClientPageLoad="LimpiarBanderaRebind_FacturaRemisiones">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton Enabled="false" CommandName="print" Value="print" CssClass="print"
                ToolTip="Imprimir" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton Enabled="false" CommandName="delete" Value="delete" CssClass="delete"
                ToolTip="Cancelación" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton runat="server" CommandName="facturaPedido" Value="facturaPedido"
                ToolTip="Factura de pedido" Text="" />
            <telerik:RadToolBarButton runat="server" CommandName="facturaRemisiones" Value="facturaRemisiones"
                ToolTip="Factura de remisiones" Text="" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="facPedido" Value="facPedido" Text="" CssClass="facPedido"
                ToolTip="Facturar pedido" ImageUrl="Imagenes/blank.png" />            
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td><asp:Label ID="lblSegundoPlano" runat="server"></asp:Label>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_IdUser" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_GridRebind_FacturaPedido" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_GridRebind_FacturaRemisiones" runat="server" Value="0" />
                   
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <div id="filtros" runat="server">
                        <table border="0">
                            <tr>
                                <td width="110">
                                    Nombre del cliente
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNombre" runat="server" Width="365px" MaxLength="70" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="rowUsuario">
                                <td width="110">
                                    Usuario
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbUsuario" runat="server" Width="279px" Height="200px"
                                        MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" OnClientBlur="Combo_ClientBlur">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tipo
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="200px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Venta instalada" Value="VI" />
                                            <telerik:RadComboBoxItem Text="Venta nueva" Value="VN" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                        <table border="0">
                            <tr>
                                <td width="110">
                                    Cliente inicial
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtCliente1_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="70">
                                    Cliente final
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtCliente2_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="110">
                                    Fecha inicial
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="70">
                                    Fecha final
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha2" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table border="0">
                            <tr>
                                <td width="110">
                                    Factura inicial
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFactura1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="23">
                                    &nbsp;
                                </td>
                                <td width="70">
                                    Factura final
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFactura2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Estatus&nbsp;&nbsp;
                                </td>
                                <td style="margin-left: 40px">
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Capturado" Value="C" />
                                            <telerik:RadComboBoxItem Text="Impreso" Value="I" />
                                            <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                            <telerik:RadComboBoxItem Text="Embarque" Value="E" />
                                            <telerik:RadComboBoxItem Text="Entregado" Value="N" />
                                            <telerik:RadComboBoxItem Text="Re-Factura" Value="RF" />
                                            <telerik:RadComboBoxItem Text="Solicitado" Value="S" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="margin-left: 40px">
                                    &nbsp;
                                </td>                                
                                <td>
                                    Complementaria&nbsp;&nbsp;
                                </td>
                                <td style="margin-left: 40px">
                                    <telerik:RadComboBox ID="CmbComplementaria" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" Selected="True" />
                                            <telerik:RadComboBoxItem Text="Si" Value="1" />
                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                      
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    Desplegar estatus SAT
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxEstatusSAT" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Si" Value="Si" />
                                            <telerik:RadComboBoxItem Text="No" Value="No" Selected="True"/>
                                      
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="margin-left: 40px">
                                    &nbsp;</td>
                                <td style="margin-left: 40px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pedido inicial
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPedido1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Pedido final
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPedido2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    Acuse&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAcuse" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Sí" Value="1" />
                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td></td>
                                <td>
                                    Depuración&nbsp;&nbsp;
                                </td>
                                <td style="margin-left: 40px">
                                    <telerik:RadComboBox ID="CmbDepuracion" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" Selected="True" />
                                            <telerik:RadComboBoxItem Text="Si" Value="1" />
                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                      
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    Orden de compra&nbsp;
                                </td>
                                <td >
                                    <telerik:RadTextBox runat="Server"  ID="TxtOC" Width="70px">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                        ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                <td>
                                  
                                </td> <td>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <a href="javascript: void(0)" onclick="ActivaDescargarPDF();" ><img id="btnConfirmaTimbrar" src="images/check.png" /></a> Timbrar seleccionados
                                   
                                     <div style="visibility: hidden"> 
                                                        <asp:Button ID="btnSoloTimbrar" runat="server" OnClick="btnSoloTimbrar_Click" />
                                                        <asp:Button ID="btnSoloDescargarPDF" runat="server" OnClick="btnSoloDescargarPDF_Click" />
                                                        <asp:Button ID="btnTimbrarDescargar" runat="server" OnClick="btnTimbrarDescargar_Click" />
                                                       <asp:HiddenField runat="server" ID="hdnIdFacPDF" Value="0" />
                                                    </div>
                                </td>
                               
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                <td>
                                      
                                </td>
                                 <td>
                                  
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="900px" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadGrid ID="rgFactura" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            AllowPaging="True" HeaderStyle-HorizontalAlign="Center"
                            OnNeedDataSource="rgFactura_NeedDataSource" OnPageIndexChanged="rgFactura_PageIndexChanged"
                            OnItemCommand="rgFactura_ItemCommand" BorderStyle="None">
                            <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                                SortToolTip="Clic para reordenar" />
                            <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Fac" ClientDataKeyNames="Id_Fac" CommandItemDisplay="Top">
                           
                               <CommandItemTemplate> 
                                             <asp:ImageButton ID="ImgExportar" runat="server" ImageUrl="Imagenes/icono_excel.png"
                                     AlternateText="Exportar facturas" ToolTip="Exportar excel" onclick="ImgExportar_Click" Width="24px" Height="24px" />                         
                                        </CommandItemTemplate> 
                 
                                <Columns>
                                     <telerik:GridTemplateColumn UniqueName="TimbrarVarios">
                                        <HeaderTemplate>
                                            <input onclick="CheckAllTimbrar(this);" type="checkbox" id="ChkTimbrarHeader" runat="server" />
                                            Timbrar
                                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkTimbrar" runat="server" Style="cursor: hand" onclick="CheckItem(this);"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                    </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie" Display="false" UniqueName="Serie">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Factura" UniqueName="Id_Fac">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_TipoStr" HeaderText="Tipo" UniqueName="Fac_TipoStr">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Nombre" UniqueName="U_Nombre">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_EstatusStr" HeaderText="Estatus SIAN" UniqueName="Fac_EstatusStr">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_EstatusSAT" HeaderText="Estatus SAT" UniqueName="Fac_EstatusSAT">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="Id_Ped">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPedido" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ped").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ped").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Fac_Fecha" HeaderText="Fecha" UniqueName="Fac_Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Cliente" UniqueName="Id_Cte">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Nombre" UniqueName="Cte_NomComercial">
                                        <HeaderStyle Width="400px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Fac_FolioFiscal">
                                        <HeaderStyle Width="280px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_SubTotal" HeaderText="Subtotal" DataFormatString="{0:N2}"
                                        UniqueName="Fac_SubTotal">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_ImporteIva" HeaderText="I.V.A." DataFormatString="{0:N2}"
                                        UniqueName="Fac_ImporteIva">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_Importe" HeaderText="Total" DataFormatString="{0:N2}"
                                        UniqueName="Fac_Importe">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_Saldo" HeaderText="Saldo" DataFormatString="{0:N2}"
                                        UniqueName="Fac_Saldo">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fac_DepuracionStr" HeaderText="Depuración" 
                                        UniqueName="Fac_DepuracionStr" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TienePagos" HeaderText="TienePagos" UniqueName="TienePagos"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Refacturar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageRefacturar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Refacturar" CommandName="Refacturar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="PDF" HeaderText="TienePDF" 
                                        UniqueName="FPDF" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FXML" HeaderText="TieneXML" UniqueName="FXML"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FXMLCN" HeaderText="TieneXML" UniqueName="FXMLCN"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                        UniqueName="PDF">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                        UniqueName="XML">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Depurar" 
                                        UniqueName="Depurar">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                         <ItemTemplate>
                                            <asp:ImageButton ID="btndepurar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Depurar" CommandName="depurar" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                         <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" Text="Enviar" UniqueName="Enviar"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </telerik:GridButtonColumn>

                                    <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja"  
                                        ConfirmDialogHeight="150px"
                                        ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    </telerik:GridButtonColumn>

                                     <%--RBM 03/2022--%> 
                                        <telerik:GridButtonColumn CommandName="AdminCFDI" HeaderText="Administrador de CFDI" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="CFDI" UniqueName="CFDI"
                                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                        </telerik:GridButtonColumn>
                                      <%--fin--%>

                                    <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                        ConfirmText="Se imprimirá la factura, tenga listo el formato en la impresora"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    </telerik:GridButtonColumn>

                                </Columns>
                            </MasterTableView>
                            <HeaderStyle HorizontalAlign="Center" />
                            <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
