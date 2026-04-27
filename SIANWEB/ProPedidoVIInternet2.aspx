  
<%@ Page Title="Captación de pedidos" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.master"
    AutoEventWireup="true" CodeBehind="ProPedidoVIInternet2.aspx.cs" Inherits="SIANWEB.ProPedidoVIInternet2" ValidateRequest="false" %>


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
        .modal-body {
            max-height: calc(100%);
            overflow-y: scroll;
        }
        .dropdown-toggle {
            height: 34px !important;
        }
        .caret {
            margin-top: 10px !important;
        }
        .centerText {
            text-align: center;
        }
                .alert-warning-box {
            background-color: transparent;
            padding: 8px 12px;
            border: 1px solid #dc3545;
            border-radius: 4px;
            display: inline-block;
            font-weight: 500;
            color: #dc3545;
        }

        .alert-info-box {
            background-color: transparent;
            padding: 8px 12px;
            border: 1px solid #0066cc;
            border-radius: 4px;
            display: inline-block;
            font-weight: 500;
            color: #0066cc;
        }
    </style>
    <script type="text/javascript">
        var uploadInProgress = false,
            submitInitiated = false,
            uploadErrorOccurred = false;
        uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            var callbackData = e.callbackData.split("|"),
                uploadedFileName = callbackData[0],
                isSubmissionExpired = callbackData[1] === "True";
            uploadedFiles.push(uploadedFileName);
            if (e.errorText.length > 0 || !e.isValid)
                uploadErrorOccurred = true;
            if (isSubmissionExpired && UploadedFilesTokenBox.GetText().length > 0) {
                var removedAfterTimeoutFiles = UploadedFilesTokenBox.GetTokenCollection().join("\n");
                alert("The following files have been removed from the server due to the defined 5 minute timeout: \n\n" + removedAfterTimeoutFiles);
                UploadedFilesTokenBox.ClearTokenCollection();
            }
        }
        function onFileUploadStart(s, e) {
            uploadInProgress = true;
            uploadErrorOccurred = false;
            UploadedFilesTokenBox.SetIsValid(true);
        }
        function onFilesUploadComplete(s, e) {
            uploadInProgress = false;
            for (var i = 0; i < uploadedFiles.length; i++)
                UploadedFilesTokenBox.AddToken(uploadedFiles[i]);
            updateTokenBoxVisibility();
            uploadedFiles = [];
            if (submitInitiated) {
                SubmitButton.SetEnabled(true);
                SubmitButton.DoClick();
            }
        }
        function onSubmitButtonInit(s, e) {
            s.SetEnabled(true);
        }
        function onSubmitButtonClick(s, e) {
            ASPxClientEdit.ValidateGroup();
            if (!formIsValid())
                e.processOnServer = false;
            else if (uploadInProgress) {
                s.SetEnabled(false);
                submitInitiated = true;
                e.processOnServer = false;
            }
        }
        function onTokenBoxValidation(s, e) {
            var isValid = DocumentsUploadControl.GetText().length > 0 || UploadedFilesTokenBox.GetText().length > 0;
            e.isValid = isValid;
            if (!isValid) {
                e.errorText = "No se han subido ningun archivo(s). Se requiere al menos un archivo.";
            }
        }
        function onTokenBoxValueChanged(s, e) {
            updateTokenBoxVisibility();
        }
        function updateTokenBoxVisibility() {
            var isTokenBoxVisible = UploadedFilesTokenBox.GetTokenCollection().length > 0;
            UploadedFilesTokenBox.SetVisible(isTokenBoxVisible);
        }
        function formIsValid() {
            return !ValidationSummary.IsVisible() && UploadedFilesTokenBox.GetIsValid() && !uploadErrorOccurred;
        }
        function AbrirVentana_InvIns(fecha, orden, Acys) {
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            $('#modalInventario').modal('hide');
            document.getElementById('<%=FrameInventario.ClientID%>').src = "ProPedidoVI_InvInsV2.aspx?fecha=" + fecha + "&orden=" + orden + "&Id_Acs=" + Acys + "&cte=" + IdCte;
            $("#modalInventario").appendTo("body");
            $("#modalInventario").modal({ "backdrop": "static" });
            $('#modalInventario').modal('show');
        }
        function closeModalDetalle() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function closeModalDetalleExito() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        window.closeModalalerta = function () {
            $('#ModalAlerta').modal('hide');
        }
        function modalMensajeExito(mensaje) {
            document.getElementById('<%=lblmensajeExito.ClientID%>').innerHTML = mensaje;
            $("#modalmensajeExito").appendTo("body")
            $("#modalmensajeExito").modal({ "backdrop": "static" });
            $('#modalmensajeExito').modal('show');
        }
        function AbrirReportePadre() {
            GetRadWindow().BrowserWindow.AbrirReporte();
            CloseWindow();
        }
        window.closeModalInvIns = function () {
            $('#modalInventario').modal('hide');
            var btn = document.getElementById('<%=btnHiddenCorreccion.ClientID%>');
            btn.click();
        };
        function popup() {
            var Ruta = "Ventana_BuscarV2.aspx?";
            $('#ModalDireccion').modal('hide');
            document.getElementById('<%=FrameDireccion.ClientID%>').src = Ruta;
            $("#ModalDireccion").appendTo("body");
            $("#ModalDireccion").modal({ "backdrop": "static" });
            $('#ModalDireccion').modal('show');
        }
        function onCustomDisabledDate(s, e) {
            var f = new Date();
            var f2 = new Date(e.date);
            var fecha = new Date(f.getFullYear(), f.getMonth(), f.getDate());
            var fecha2 = new Date(f2.getFullYear(), f2.getMonth(), f2.getDate());
            console.log(new Date(fecha2))
            console.log(new Date(fecha))
            if (fecha2 < fecha)
                e.isDisabled = true;
        }
        function abrirBuscar() {
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var Ruta = "Ventana_BuscarV2.aspx?DirEnt=true&cte=" + IdCte;
            $('#ModalDireccion').modal('hide');
            document.getElementById('<%=FrameDireccion.ClientID%>').src = Ruta;
            $("#ModalDireccion").appendTo("body");
            $("#ModalDireccion").modal({ "backdrop": "static" });
            $('#ModalDireccion').modal('show');
        }
        //JFCV convenios de precios validar antes de grabar 
        function AbrirVentana_AlertaPrecios() {
            var Ruta = "Ventana_AlertaPrecios2.aspx";
            $('#ModalAlerta').modal('hide');
            document.getElementById('<%=frameAlerta.ClientID%>').src = Ruta;
            $("#ModalAlerta").appendTo("body");
            $("#ModalAlerta").modal({ "backdrop": "static" });
            $('#ModalAlerta').modal('show');
        }

        //JFCV convenios de precios validar antes de grabar 
        function AbrirVentana_AlertaAutorizacionPrecios() {
            var Ruta = "Ventana_AutorizacionPrecios.aspx";
            $('#ModalAlerta').modal('hide');
            document.getElementById('<%=frameAlerta.ClientID%>').src = Ruta;

            $("#ModalAlerta").appendTo("body");
            $("#ModalAlerta").modal({ "backdrop": "static" });
            $('#ModalAlerta').modal('show');
        }

        window.closeModalalerta = function () {
            $('#ModalAlerta').modal('hide');
        }
        //JFCV convenios de precios validar antes de grabar 

        function closeModalDetalle() {
            window.parent.closeModalDetalle();
        };
        $(document).keydown(function (evt) {
            if (evt.keyCode == 115) {
                evt.preventDefault();
                if (!($('#ModalVI').is(':visible'))) {
                    var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
                    var Ruta = "Ventana_BuscarV2.aspx?Precio=true&cte=" + IdCte;
                    document.getElementById('<%=IframeVI.ClientID%>').src = Ruta;
                    $("#ModalVI").appendTo("body");
                    $("#ModalVI").modal({ "backdrop": "static" });
                    $('#ModalVI').modal('show');
                }
                else {
                    $('#ModalVI').modal('hide').data('bs.modal', null);
                }
            }
        });
        window.closeModal = function (param) {
            if (($('#ModalDireccion').is(':visible'))) {
                $('#ModalDireccion').modal('hide');
            }
            if (($('#ModalVI').is(':visible'))) {
                $('#ModalVI').modal('hide')
            }
            document.getElementById('<%=HF_Param.ClientID%>').value = param;
            var btn = document.getElementById('<%=BtnHiddenDireccion.ClientID%>');
            btn.click();
        };
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
            $("#modalAcysMensaje").appendTo("body")
            $("#modalAcysMensaje").modal({ "backdrop": "static" });
            $('#modalAcysMensaje').modal('show');
        }
        function AbrirBuscarDireccionEntrega(IdCte) {
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var Ruta = "Ventana_BuscarV2.aspx?DirEnt=true&cte=" + IdCte;
            $('#ModalDireccion').modal('hide');
            document.getElementById('<%=FrameDireccion.ClientID%>').src = Ruta;
            $("#ModalDireccion").appendTo("body");
            $("#ModalDireccion").modal({ "backdrop": "static" });
            $('#ModalDireccion').modal('show');
        }
        function mantaintabs(datosTabs) {
            $('#Tabs a[href="#' + datosTabs + '"]').tab('show');
        }
        function confirmCallBackFnGuardar() {
            document.getElementById('<%=lblConfirm.ClientID%>').innerHTML = mensaje;
            $("#modalConfirm").appendTo("body")
            $("#modalConfirm").modal({ "backdrop": "static" });
            $('#modalConfirm').modal('show');
        }
        function confirmCallBackFnPrint() {
            document.getElementById('<%=lblConfirm2.ClientID%>').innerHTML = mensaje;
            $("#modalConfirm2").appendTo("body")
            $("#modalConfirm2").modal({ "backdrop": "static" });
            $('#modalConfirm2').modal('show');
        }

        /*JFCV 23sep22 cierro la pantalla modal de aut de alertas y ejecuto el grabado*/
        window.closeModalAlertaPed = function () {
            debugger;
            $('#ModalAlerta').modal('hide');
            var btn = document.getElementById('<%=btnHiddenCorreccion.ClientID%>');
            btn.click();
        };
        function Subtotal() {
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var Iva = $("#" + '<%=HD_IVAfacturacion.ClientID%>').val();
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/CalcularTotalVisible",
                data: JSON.stringify({
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    IVA: Iva,
                    EmpCnx: EmpCnx
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            $("#" + '<%=txtIva.ClientID%>').val(data.iva);
                            $("#" + '<%=txtTotal.ClientID%>').val(data.total);
                            $("#" + '<%=txtSubtotal.ClientID%>').val(data.subtotal);
                        }
                    }
                }
            });
        }
        function onSelectedIndexChanged(cmbId_Prd) {
            column = cmbId_Prd.focusedColumn;
            visibleIndex = cmbId_Prd.visibleIndex;
            var idProd = cmbId_Prd.GetValue().toString();
            var IdTer = $("#" + '<%=txtIdTer.ClientID%>').val();
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var IdRik = $("#" + '<%=txtIdRik.ClientID%>').val();
            var clave = $("#" + '<%=txtClave.ClientID%>').val();
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var pedidoProg = $("#" + '<%=HF_pedido.ClientID%>').val();
            cmbId_Prd.SetEnabled(false);
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/cmbProductoDetRestos",
                data: JSON.stringify({
                    IdProd: idProd,
                    idterr: IdTer,
                    idCte: IdCte,
                    IdRik: IdRik,
                    clave: clave,
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    EmpCnx: EmpCnx,
                    pedidoProg: pedidoProg
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbId_Prd.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbId_Prd.SetEnabled(true);
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje('Por favor, capture un territorio en la vista \"Datos Generales\"');
                        }
                        if (id == 2) {
                            modalMensaje('Por favor, capture un cliente en la vista \"Datos Generales\"');
                        }
                        if (id == 3) {
                            modalMensaje('Por favor, capture un representante de ventas en la vista \"Datos Generales\"');
                        }
                        if (id == 4) {
                            modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                        }
                        if (id == 5) {
                            modalMensaje('Solamente se permite el registro de un producto en su captura.');
                        }
                        if (id == 7) {
                            modalMensaje('Solamente se permiten productos registrados para el portal de clientes.');
                        }
                        else {
                            data.Presentacion
                            var cmbDesc = CPH_rg1.GetEditor("Prd_Descripcion");
                            cmbDesc.SetValue(data.Descripcion);
                            var cmbPres = CPH_rg1.GetEditor("Prd_Presentacion");
                            cmbPres.SetValue(data.Presentacion);
                            var cmbUni = CPH_rg1.GetEditor("Prd_Unidad");
                            cmbUni.SetValue(data.PrdUni);
                            var cmbCant = CPH_rg1.GetEditor("Prd_Cantidad");
                            cmbCant.SetValue(data.Cant);
                            var cmbPre = CPH_rg1.GetEditor("Prd_Precio");
                            cmbPre.SetValue(data.Precio);
                            var CMBLista = CPH_rg1.GetEditor("Prd_PrecioLista");
                            CMBLista.SetValue(data.PRecioLista);
                            console.log(data.PRecioLista);
                        }
                    }
                }
            });
        }
        function onSelectedCantidadIndexChanged(cmbCant) {
            column = cmbCant.focusedColumn;
            visibleIndex = cmbCant.visibleIndex;
            var cantidad = cmbCant.GetValue().toString();
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var cmbidProd = CPH_rg1.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbPre = CPH_rg1.GetEditor("Prd_Precio");
            var precio = cmbPre.GetValue();
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/txtCantidad_TextChanged",
                data: JSON.stringify({
                    cantidad: cantidad,
                    precio: precio,
                    idCte: IdCte,
                    Id_prd: idProd,
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    EmpCnx: EmpCnx
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbCant.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbCant.SetEnabled(true);
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje('La cantidad debe ser mayor a 0');
                        }
                        if (id == 3) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        if (id == 4) {
                            modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                        }
                        else {
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                    }
                }
            });
        }
        function onSelectedPrecioIndexChanged(cmbPre) {
            column = cmbPre.focusedColumn;
            visibleIndex = cmbPre.visibleIndex;
            var precio = cmbPre.GetValue().toString();
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var cmbidProd = CPH_rg1.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbcant = CPH_rg1.GetEditor("Prd_Cantidad");
            var cantidad = cmbcant.GetValue();
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/txtPrecio_TextChanged",
                data: JSON.stringify({
                    cantidad: cantidad,
                    precio: precio,
                    idCte: IdCte,
                    Id_prd: idProd,
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    EmpCnx: EmpCnx
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbPre.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbPre.SetEnabled(true);
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        else {
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                    }
                }
            });
        }
        function onSelectedCantidadIndexChangedVI(cmbCant) {
            column = cmbCant.focusedColumn;
            visibleIndex = cmbCant.visibleIndex;
            var cantidad = cmbCant.GetValue().toString();
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var cmbidProd = CPH_rg1.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbPre = CPH_rg1.GetEditor("Prd_Precio");
            var precio = cmbPre.GetValue();
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/txtCantidad_TextChanged",
                data: JSON.stringify({
                    cantidad: cantidad,
                    precio: precio,
                    idCte: IdCte,
                    Id_prd: idProd,
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    EmpCnx: EmpCnx
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbCant.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbCant.SetEnabled(true);
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje('La cantidad debe ser mayor a 0');
                        }
                        if (id == 3) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        if (id == 4) {
                            modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                        }
                        else {
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                    }
                    else {
                        modalMensaje('Error en la consulta');
                    }
                }
            });
        }
        function onSelectedPrecioIndexChangedVI(cmbPre) {
            column = cmbPre.focusedColumn;
            visibleIndex = cmbPre.visibleIndex;
            var precio = cmbPre.GetValue().toString();
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
            var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
            var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
            var cmbidProd = CPH_rg1.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbcant = CPH_rg1.GetEditor("Prd_Cantidad");
            var cantidad = cmbcant.GetValue();
            $.ajax({
                type: "POST",
                url: "ProPedidoVIInternet2.aspx/txtPrecio_TextChanged",
                data: JSON.stringify({
                    cantidad: cantidad,
                    precio: precio,
                    idCte: IdCte,
                    Id_prd: idProd,
                    IdCd: IdCd,
                    IdEmp: IdEmp,
                    EmpCnx: EmpCnx
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbPre.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbPre.SetEnabled(true);
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        else {
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                    }
                    else {
                        modalMensaje('Error en la consulta');
                    }
                }
            });
        }

        function onFechaEntregaChanged(s, e) {
            var fechaSeleccionada = s.GetDate();
            var fechaCompromiso = $("#" + '<%=HF_FechaEntregaCompromiso.ClientID%>').val();
            console.log('Fecha compromiso:', fechaCompromiso);
            var lblFechaEntrega = document.getElementById('<%=lblFechaEntregaCompromiso.ClientID%>');
            lblFechaEntrega.innerHTML = '';
            lblFechaEntrega.style.display = 'none';

            if (fechaCompromiso && fechaCompromiso !== '') {
                var fechaCompromisoDate = new Date(fechaCompromiso + 'T00:00:00');

                console.log('Fecha compromiso date:', fechaCompromisoDate);
                console.log('Fecha seleccionada:', fechaSeleccionada);


                if (fechaSeleccionada > fechaCompromisoDate) {
                    lblFechaEntrega.innerHTML = 'Importante que la fecha compromiso sea igual o menor a ' +
                        fechaCompromisoDate.toLocaleDateString('es-MX');
                    lblFechaEntrega.style.display = 'inline-block';
                } else {
                    lblFechaEntrega.innerHTML = '';
                    lblFechaEntrega.style.display = 'none';
                }
            }
        }
    </script>


    <div class="modal-body" id="Div2">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <ul class="nav nav-tabs" id="tabPage">
                    <li class="active"><a href="#tabCliente"  aria-controls="tabClienteTab" data-toggle="tab">Datos Generales</a> </li>
                    <li><a href="#tabDetalle" aria-controls="tabDetalleTab" data-toggle="tab">Detalle</a> </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="tabCliente">
                        <div class="col-md-12">
                            <div class="row mt5">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                Folio:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtFolio" runat="server" ReadOnly="true" Enabled="false">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                Acuerdo:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtClave" runat="server" ReadOnly="true">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                Versión
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="TxtVersion" runat="server" MaxLength="50">
                                                </dx:BootstrapTextBox>
                                            </div>

                                            <div class="col-md-1">
                                               Requisición
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="TxtPed_ReqAcys" runat="server" MaxLength="50">
                                                </dx:BootstrapTextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                Captado Por:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtPedCaptadorPor" runat="server" ReadOnly="true"
                                                    Enabled="false">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            
                                            <div class="col-md-1">
                                                Orden compra
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="TxtPed_OCAcys" runat="server" MaxLength="50" Visible="true">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1" style="display: none;">
                                                Pedido
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="TxtPed_PedAcys" runat="server" MaxLength="50" Visible="false">
                                                </dx:BootstrapTextBox>
                                            </div>
 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="row mt5">
                                <div class="col-md-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading titulo_blod">
                                            Contacto
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                Encargado de enviar el pedido
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <dx:BootstrapTextBox ID="txtContactoNom" runat="server" MaxLength="30" Enabled="false">
                                                    </dx:BootstrapTextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Puesto
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapTextBox ID="txtContactoPuesto" runat="server" MaxLength="50" Enabled="false">
                                                    </dx:BootstrapTextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    Correo
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapButtonEdit ID="txtContactoMail" ClearButton-DisplayMode="OnHover" runat="server"
                                                        Enabled="false">
                                                    </dx:BootstrapButtonEdit>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Teléfono
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapTextBox ID="txtContactoTel" runat="server" Enabled="false">
                                                    </dx:BootstrapTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Cliente
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="text" class="form-control" id="txtIdCte" runat="server"
                                                        readonly="readonly" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="txtClienteNom" runat="server" AutoPostBack="true" EnableCallbackMode="true"
                                                        CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="ddlClienteNom_TextChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Territorio
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="text" class="form-control" id="txtIdTer" runat="server"
                                                        readonly="readonly" onblur="txt2_OnBlur" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="txtTerritorioNom" runat="server" CallbackPageSize="25"
                                                        DropDownStyle="DropDown" IncrementalFilteringMode="Contains">

                                                        <ClearButton DisplayMode="Always" />
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    RIK
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="text" class="form-control" id="txtIdRik" runat="server"
                                                        readonly="readonly" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <input type="text" id="txtRikNom" class="form-control" runat="server" onpaste="return false"
                                                        readonly="readonly" />
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label2" runat="server" Text="Semana actual"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <input type="text" id="txtSemana" class="form-control" runat="server"
                                                        readonly="readonly" />
                                                </div>
                                                <div class="col-md-4">

                                                    <input type="text" id="txtFecha" class="form-control" runat="server"
                                                        readonly="readonly" />
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    Fecha de facturación
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapDateEdit ID="rdFechaF" runat="server">
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                            <ClientSideEvents CalendarCustomDisabledDate="onCustomDisabledDate" />
                                                    </dx:BootstrapDateEdit>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    Fecha compromiso entrega
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapDateEdit ID="rdFechaE" runat="server">
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                        <ClientSideEvents CalendarCustomDisabledDate="onCustomDisabledDate" DateChanged="onFechaEntregaChanged" />
                                                    </dx:BootstrapDateEdit>
                                                </div>
                                                <div class="col-md-4">                                                     
                                                    <asp:HiddenField ID="HF_FechaEntregaCompromiso" runat="server" />
                                                    <asp:Label  ID="lblDiasEntrega" Text="" runat="server" Visible="false" CssClass="alert-info-box" />
                                                    <asp:Label  ID="lblFechaEntregaCompromiso" Text="" runat="server" ClientIDMode="Static" CssClass="alert-warning-box" style="display:none"  />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row mt5">
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading titulo_blod">
                                                    Dirección de entrega producto
                                            <dx:BootstrapButton ID="ImgBuscarDireccionEntrega" SettingsBootstrap-RenderOption="Default"
                                                runat="server" ToolTip="Buscar Dirección cliente" OnClick="btnDireccion_Click">
                                                <CssClasses Icon="fa fa-search" />
                                            </dx:BootstrapButton>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Calle
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtCalle" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            No.
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dx:BootstrapTextBox ID="txtNo" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            C.P.
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dx:BootstrapTextBox ID="txtCp" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Colonia
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtColonia" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            Municipio
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtMunicipio" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Estado
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtEstado" runat="server" ReadOnly="true">
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="panel-body">
                                                     <div class="col-md-3">
                                                            USO de CFDI
                                                        </div>
                                                        <div class="col-md-6">
                                                              <dx:BootstrapComboBox ID="ddUsoCfdi" runat="server" CallbackPageSize="25"
                                                                 DropDownStyle="DropDown" IncrementalFilteringMode="Contains" AutoPostBack="true" >
                                                                 <ValidationSettings>
                                                                     <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                 </ValidationSettings>
                                                                 <ClearButton DisplayMode="Always" />
                                                             </dx:BootstrapComboBox>
                                                        </div>
                                                 </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading titulo_blod">
                                                    Documentación requerida para entrega
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="ChkOrdCompra" disabled="disabled" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Orden de compra / Release   
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-contro chb" id="ChckOrdReposicion" disabled="disabled" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Orden de reposición  
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="ChckFolio" disabled="disabled" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Folio  
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                        </div>
                                                        <div class="col-md-2">
                                                            Otro
                                                        </div>
                                                        <div class="col-md-8">
                                                            <input type="text" id="LblEOtro" class="form-control" runat="server" maxlength="225" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row mt5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading titulo_blod">
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label36" runat="server" Text="Horario de recepeción"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <input type="text" id="txtRHoraam1" class="form-control" runat="server" readonly="readonly" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label74" runat="server" Text="a"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <input type="text" id="txtRHoraam2" class="form-control" runat="server" readonly="readonly" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label75" runat="server" Text="y"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <input type="text" id="txtRHorapm1" class="form-control" runat="server" readonly="readonly" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label76" runat="server" Text="a"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <input type="text" id="txtRHorapm2" class="form-control" runat="server" readonly="readonly" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="col-md-12">
                                    <div class="row mt5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading titulo_blod">
                                                <asp:Label ID="Label16" runat="server" Text="MODALIDAD DE PEDIDO"></asp:Label></b>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <asp:RadioButton ID="rdModFrencuenciaEstablecida" runat="server"
                                                            Text="Frecuencia Establecida" GroupName="ModalidadPedido" Enabled="False" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:RadioButton ID="rdModOrdenAbierta" runat="server"
                                                            Text="Orden Abierta con Reposición / Release" GroupName="ModalidadPedido"
                                                            Enabled="False" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:RadioButton ID="rdModConsignacion" runat="server" Text="Consignación"
                                                            GroupName="ModalidadPedido" Enabled="False" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:RadioButton ID="rdModInternet" runat="server" Text="Pedido de Internet"
                                                            GroupName="ModalidadPedido" Enabled="False" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <dx:BootstrapGridView ID="rgAcuerdos" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="Id_Emp;Id_Cd;Num_Pedido">
                                                        <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                        <SettingsEditing Mode="Batch" />
                                                        <Columns>
                                                            <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" CssClasses-HeaderCell="centerText" Caption="Num. Producto" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" Caption="Producto" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Lunes" CssClasses-HeaderCell="centerText" Caption="L" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Martes" CssClasses-HeaderCell="centerText" Caption="M" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Miercoles" CssClasses-HeaderCell="centerText" Caption="M" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Jueves" CssClasses-HeaderCell="centerText" Caption="J" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Viernes" CssClasses-HeaderCell="centerText" Caption="V" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Sabado" CssClasses-HeaderCell="centerText" Caption="S" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Acs_Documento" CssClasses-HeaderCell="centerText" Caption="Doc. de entrega" />
                                                        </Columns>
                                                        <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                                    </dx:BootstrapGridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row mt5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading titulo_blod">
                                                Contactos del Cliente
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label18" runat="server" Text="Almacén" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtContactoClientealmacen" runat="server" MaxLength="50" Enabled="false">
                                                        </dx:BootstrapTextBox>

                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label28" runat="server" Text="Teléfono" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtContactoClientealmacenTel" runat="server" MaxLength="50" Enabled="false">
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label29" runat="server" Text="E-mail" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtContactoClientealmacenEmail" runat="server" MaxLength="50" Enabled="false">
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row mt5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading titulo_blod">
                                                Observaciones
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        Observaciones:
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <dx:BootstrapMemo runat="server" ID="txtNotas" NullText="Enter Your Description" MaxLength="200" Rows="5">
                                                            </dx:BootstrapMemo>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="tab-pane" id="tabDetalle">

                         <div class="col-md-12" style="margin-top: 1%">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            Doc. de entrega:
                                        </div>
                                        <div class="col-md-3">
                                            <dx:BootstrapComboBox runat="server" ID="ddlDocEntrega" AutoPostBack="true" OnSelectedIndexChanged="ddlDocEntrega_SelectedIndexChanged">
                                                <Items>
                                                    <dx:BootstrapListEditItem Text="--seleccionar--" Value="-1" Selected="true" />
                                                    <dx:BootstrapListEditItem Text="Factura" Value="F" />
                                                    <dx:BootstrapListEditItem Text="Remisión" Value="R" />
                                                </Items>
                                            </dx:BootstrapComboBox>
                                        </div> 
                                    </div>
                                </div>
                            </div>
                        </div>

                            <div class="col-md-12" runat="server" id="divupload">
                            <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
                            <dx:ASPxFormLayout ID="FormLayout" runat="server" Width="100%" ColCount="2" UseDefaultPaddings="false">
                                <Items>
                                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" Width="50%" UseDefaultPaddings="false">
                                        <Items>

                                            <dx:LayoutGroup Caption="">
                                                <Items>
                                                    <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <div id="dropZone">
                                                                    <dx:ASPxUploadControl runat="server" ID="DocumentsUploadControl" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                                                        AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="false" BrowseButton-Text="Subir documento(s)" FileUploadMode="OnPageLoad"
                                                                        OnFileUploadComplete="DocumentsUploadControl_FileUploadComplete">
                                                                        <BrowseButtonStyle CssClass="btn btn-default"></BrowseButtonStyle>
                                                                        <AdvancedModeSettings EnableMultiSelect="false" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" />
                                                                        <ValidationSettings
                                                                            AllowedFileExtensions=".jpg, .jpeg, .xls, .xlsx, .pdf,  .xlsx"
                                                                            MaxFileSize="4194304"
                                                                            DisableHttpHandlerValidation="true" >
                                                                        </ValidationSettings>
                                                                        <ClientSideEvents
                                                                            FileUploadComplete="onFileUploadComplete"
                                                                            FilesUploadComplete="onFilesUploadComplete"
                                                                            FilesUploadStart="onFileUploadStart" />
                                                                    </dx:ASPxUploadControl>
                                                                    <br />
                                                                    <dx:ASPxTokenBox runat="server" Width="100%" ID="UploadedFilesTokenBox" ClientInstanceName="UploadedFilesTokenBox"
                                                                        NullText="Seleccione los documentos a subir" AllowCustomTokens="false" ClientVisible="false">
                                                                        <ClientSideEvents Init="updateTokenBoxVisibility" ValueChanged="onTokenBoxValueChanged" Validation="onTokenBoxValidation" />
                                                                        <ValidationSettings EnableCustomValidation="true" />
                                                                    </dx:ASPxTokenBox>
                                                                    <br />
                                                                    <p class="Note">
                                                                        <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text="Permite Extensiones: .PDF, .XLXS, .JPG, .JPEG, .PNG." Font-Size="8pt" />
                                                                        <br />
                                                                        <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="Maximo tamaño del archivo: 4 MB." Font-Size="8pt" />
                                                                    </p>
                                                                    <dx:ASPxValidationSummary runat="server" ID="ValidationSummary" ClientInstanceName="ValidationSummary"
                                                                        RenderMode="Table" Width="250px" ShowErrorAsLink="false" />
                                                                </div>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="left">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:EmptyLayoutItem Height="5" />
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                            </dx:ASPxFormLayout>
                        </div>

                        <div class="col-md-12" style="margin-top: 1%">
                            Producto Venta Instalada
                        </div>
                        <div class="col-md-12">
                             <dx:BootstrapGridView ID="rg1" runat="server" KeyFieldName="Id_Prd" Width="100%"
                                EnableRowsCache="false"  OnRowUpdating="rg1_RowUpdating"  OnRowDeleting="rg1_RowDeleting"
                                  EnableCallBacks="true" OnHtmlDataCellPrepared="rg1_HtmlDataCellPrepared">
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing> 
                                <SettingsDataSecurity AllowEdit="true" AllowDelete="true" />
                                 
                                <SettingsCommandButton>
                                    <EditButton IconCssClass="fa fa-edit" Text=" " />
                                    <DeleteButton IconCssClass="fa fa-remove" Text=" " />
                                    <CancelButton IconCssClass="fa fa-ban" Text=" " />
                                    <UpdateButton IconCssClass="fa fa-check" Text=" " />
                                    <NewButton IconCssClass="fa fa-plus" Text=" " />
                                </SettingsCommandButton>
                                <ClientSideEvents EndCallback="function(s, e) { 
                 Subtotal();  }" />
                                <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true"  Caption=" " />
                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Activo" Caption="Prd_Activo" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" CssClasses-HeaderCell="centerText" Caption="Núm.">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents TextChanged="onSelectedIndexChanged" />
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Descripcion" ReadOnly="true" Caption="Producto">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Presentacion" ReadOnly="true" Caption="Presen.">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Unidad" ReadOnly="true" Caption="Unidad">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Cantidad" Caption="Cant.">
                                        <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                                        </PropertiesSpinEdit>
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Precio" Caption="Precio vta.">
                                        <PropertiesSpinEdit DisplayFormatString="c">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedPrecioIndexChangedVI" />
                                        </PropertiesSpinEdit>
                                    </dx:BootstrapGridViewSpinEditColumn>
                                      <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_PrecioLista" ReadOnly="true" Caption="Precio de lista">
                                        <PropertiesSpinEdit DisplayFormatString="c"> 
                                        </PropertiesSpinEdit>
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewTextColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Importe" ReadOnly="true" Caption="Importe">
                                        <PropertiesTextEdit DisplayFormatString="c">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    
                                    <dx:BootstrapGridViewComboBoxColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" Caption="Doc. de entrega" FieldName="Acs_Doc">
                                        <PropertiesComboBox TextField="Acs_Doc" ValueField="Acs_Doc" EnableSynchronization="False" AllowNull="false">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Factura" Value="F" />
                                                <dx:BootstrapListEditItem Text="Remisión" Value="R" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:BootstrapGridViewComboBoxColumn>
                                    <dx:BootstrapGridViewComboBoxColumn Caption="Requiere Orden de Compra" HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="ACS_ReqOC" Width="100px" Visible="false">
                                        <PropertiesComboBox TextField="Acs_reDoc" ValueField="Acs_reDoc" EnableSynchronization="False">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Sí" Value="1" />
                                                <dx:BootstrapListEditItem Text="No" Value="2" Selected="true" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:BootstrapGridViewComboBoxColumn>
                                </Columns>
                                   <SettingsPager PageSize="20" NumericButtonCount="6">
                                    <Summary Visible="false" />
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                </SettingsPager>
                            </dx:BootstrapGridView>
                        </div>
                        <div id="divTotales" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-8">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1">
                                        Subtotal
                                    </div>
                                    <div class="col-md-2">
                                        <input type="text" id="txtSubtotal" runat="server" class="form-control" disabled="disabled" style="text-align:right; opacity: 100 !important;"
                                            width="70px" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-8">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1">
                                        <asp:Label ID="IVATEXTO" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <input type="text" id="txtIva" runat="server" class="form-control" disabled="disabled" style="text-align:right; opacity: 100 !important;"
                                            width="70px" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-8">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1">
                                        Total
                                    </div>
                                    <div class="col-md-2">
                                        <input type="text" id="txtTotal" runat="server" class="form-control" disabled="disabled" style="text-align:right; opacity: 100 !important;"
                                            width="70px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer" style="margin-top: 3%;">
                    <div class="row">
                        <div class="col-md-12 ">
                            <button class="btn btn-default" id="Button1" onclick="closeModalDetalle()">
                                Cancelar</button>
                            <dx:BootstrapButton ID="btncaptacion_Guardar" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Primary"
                                OnClick="btncaptacion_Guardar_Click">
                                <ClientSideEvents Click="function(s, e) {
                                           setTimeout(function () {  
                            s.SetEnabled(false);  
                            }, 10); 
                            if(ASPxClientEdit.ValidateGroup('Validation'))
                            {
                          
                             e.processOnServer = true;
                              
                            }
                            else
                            { 
                            e.processOnServer = false;
                                  
                            } ; 
                                      setTimeout(function () {  
                            s.SetEnabled(true);  
                            }, 15000);}" />
                                <CssClasses Icon="fa fa-check" />
                            </dx:BootstrapButton>
                        </div>
                    </div>
                </div>
 

        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" Value="N" />

            </ContentTemplate>
        </asp:UpdatePanel>
        
                 <asp:HiddenField ID="HF_InicioSemana" runat="server" />
        <asp:HiddenField ID="HF_FinSemana" runat="server" />
        <asp:HiddenField ID="HF_FechaActual" runat="server" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
        <asp:HiddenField ID="HF_ID" runat="server" Value="" />
        <asp:HiddenField ID="HF_Param" runat="server" />
        <asp:HiddenField ID="HF_IdCd" runat="server" />
        <asp:HiddenField ID="HF_IdEmp" runat="server" />
        <asp:HiddenField ID="HF_Emp_Cnx" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="HF_Sem" runat="server" />
        <asp:HiddenField ID="HF_pedido" runat="server" />
        <asp:HiddenField ID="HF_Anio" runat="server" /> 
        <asp:HiddenField ID="TabName" runat="server" />
        <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
         <asp:HiddenField ID="PedExterno" runat="server" />
        <div style="display: none">
            <asp:Button runat="server" OnClick="btnActualizarDireccion_Click" ID="BtnHiddenDireccion" />

            <asp:Button runat="server" OnClick="btnCoreccion" ID="btnHiddenCorreccion" />
    </div>
    <div id="modalAcysMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
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
    <div class="modal fade" id="modalInventario" style="height: 500px !important" class="modal"
        role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h1">Inventario
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 ">
                    <iframe class="embed-responsive-item" id="FrameInventario" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalDireccion" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h2">
                        <asp:Label runat="server" ID="lblDir"></asp:Label>
                        Dirección del cliente
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 20% !Important;">
                    <iframe class="embed-responsive-item" id="FrameDireccion" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalProductoRegistro" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h5">
                        <asp:Label runat="server" ID="Label1"></asp:Label>
                        Registrar Productos en Acys
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 20% !Important;">
                    <iframe class="embed-responsive-item" id="iframeProductoRegistro" runat="server"
                        src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalVI" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h4">
                        <asp:Label runat="server" ID="Label3"></asp:Label>
                        Productos
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 20% !Important;">
                    <iframe class="embed-responsive-item" id="IframeVI" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalAcys" class="modal" role="dialog" style="z-index: 2220!important; width: 75%; margin-left: 15%;">
        <div class="modal-content" style="height: 100%;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h3">Pedidos de venta instaladas pendientes
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 40% !Important; height: height: 300px !important;">
                    <iframe class="embed-responsive-item" id="IframeModalAcys" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <span id="lblConfirm" runat="server"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                    <button type="button" class="btn btn-primary" runat="server" id="btnGuardar" data-dismiss="modal" onserverclick="btnGuardar_ServerClick">Sí</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConfirm2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <span id="lblConfirm2" runat="server"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                    <button type="button" class="btn btn-primary" runat="server" id="Buttbtnimprimiron2" data-dismiss="modal" onserverclick="Buttbtnimprimiron2_ServerClick">Sí</button>
                </div>
            </div>
        </div>
    </div>
   <div id="modalmensajeExito" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close"   aria-label="Close" onclick="closeModalDetalleExito()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensajeExito" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
                            <table>
                                <tr>
                                    <td>
                                        <button class="btn btn-default" id="Button9" onclick="closeModalDetalleExito()">
                                            Aceptar</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalAlerta" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h2">
                        <asp:Label runat="server" ID="Label4"></asp:Label>
                        Alerta de Precio
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 20% !Important;">
                    <iframe class="embed-responsive-item" id="frameAlerta" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>


        <script type="text/javascript">
            $(function () {
                var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tabCliente";
                $('#Tabs a[href="#' + tabName + '"]').tab('show');
                $("#Tabs a").click(function () {
                    $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                });
            });
        </script>
</asp:Content>