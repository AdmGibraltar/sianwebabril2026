<%@ Page Title="Captación de pedidos" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI2.aspx.cs" Inherits="SIANWEB.ProPedidoVI2" ValidateRequest="false" %>

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
        .nav-tabs a, .nav-tabs a:hover, .nav-tabs a:focus {
            outline: 0;
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
        #dropZone {
            padding: 20px;
            margin: -20px;
        }
        .ResultFileName {
            text-overflow: ellipsis;
        }
        .contentFooter {
            clear: both;
            padding-top: 20px;
        }
        .margen {
            margin-left: 10px;
        }
        .RadForm_Outlook.rfdHeading h4 {
            border-bottom: solid 0px #6788be !important;
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
        function addDays(date, days) {
            var date = new Date(this.valueOf());
            date.setDate(date.getDate() + days);
            return date;
        }

        function closeModalDetalle() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function closeModalDetalleExito() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function modalMensajeExito(mensaje) {
            document.getElementById('<%=lblmensajeExito.ClientID%>').innerHTML = mensaje;
            $("#modalmensajeExito").appendTo("body")
            $("#modalmensajeExito").modal({ "backdrop": "static" });
            $('#modalmensajeExito').modal('show');
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

        //JFCV 12feb2021 convenios de precios validar antes de grabar 
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
        window.closeModalAcysCliente = function (Id_Acs, Semana, Pedido, anio) {
            $('#ModalAcys').modal('hide');
            document.getElementById('<%=HF_ID.ClientID%>').value = Id_Acs;
            document.getElementById('<%=HF_Sem.ClientID%>').value = Semana;
            document.getElementById('<%=HF_pedido.ClientID%>').value = Pedido;
            document.getElementById('<%=HF_Anio.ClientID%>').value = anio;
            var btn = document.getElementById('<%=BtnHiddenacys.ClientID%>');
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
        function AbrirClienteAcys(idAcs, IdCte) {
            console.log(idAcs);
            var Ruta = "PropedidoVI_AcysCliente2.aspx?DirEnt=true&IdCte=" + IdCte + " &IdAcs = " + idAcs;
            $('#ModalAcys').modal('hide');
            document.getElementById('<%=IframeModalAcys.ClientID%>').src = Ruta;
            $("#ModalAcys").appendTo("body");
            $("#ModalAcys").modal({ "backdrop": "static" });
            $('#ModalAcys').modal('show');
        }
        function mantaintabs(datosTabs) {
            $('#Tabs a[href="#' + datosTabs + '"]').tab('show');
        }
        function AbrirVentana_InvIns(fecha, orden, Acys) {
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            $('#modalInventario').modal('hide');
            document.getElementById('<%=FrameInventario.ClientID%>').src = "ProPedidoVI_InvInsV2.aspx?fecha=" + fecha + "&orden=" + orden + "&Id_Acs=" + Acys + "&cte=" + IdCte;
            $("#modalInventario").appendTo("body");
            $("#modalInventario").modal({ "backdrop": "static" });
            $('#modalInventario').modal('show');
        }
        window.closeModalInvIns = function () {
            $('#modalInventario').modal('hide');
            var btn = document.getElementById('<%=btnHiddenCorreccion.ClientID%>');
            btn.click();
        };
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
            var dataValue = "{IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', IVA: '" + Iva + "', EmpCnx: '" + EmpCnx + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/CalcularTotalVisible",
                data: dataValue,
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
            var dataValue = "{ IdProd: '" + idProd + "', idterr: '" + IdTer + "'  , idCte: '" + IdCte + "', IdRik: '" + IdRik + "', clave: '" + clave + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "', pedidoProg: '" + pedidoProg + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/cmbProductoDetRestos",
                data: dataValue,
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
                        if (data?.Prd_Activo == 2 && data?.Prd_InvFinal <= 0) {
                            modalMensaje('Producto inactivo, reemplazalo con otra alternativa/consulta con el area operativa/CEDIS');
                            return false;
                        }
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
                        else {
                            data.Presentacion
                            var cmbDesc = CPH_rg1_Restos.GetEditor("Prd_Descripcion");
                            cmbDesc.SetValue(data.Descripcion);
                            var cmbPres = CPH_rg1_Restos.GetEditor("Prd_Presentacion");
                            cmbPres.SetValue(data.Presentacion);
                            var cmbUni = CPH_rg1_Restos.GetEditor("Prd_Unidad");
                            cmbUni.SetValue(data.PrdUni);
                            var cmbCant = CPH_rg1_Restos.GetEditor("Prd_Cantidad");
                            cmbCant.SetValue(data.Cant);
                            var cmbPre = CPH_rg1_Restos.GetEditor("Prd_Precio");
                            cmbPre.SetValue(data.Precio);
                            var CMBLista = CPH_rg1_Restos.GetEditor("Prd_PrecioLista");
                            CMBLista.SetValue(data.PRecioLista);
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
            var cmbidProd = CPH_rg1_Restos.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbPre = CPH_rg1_Restos.GetEditor("Prd_Precio");
            var precio = cmbPre.GetValue();
            //var cmbOriginal = CPH_rg1_Restos.GetEditor("Prd_Original");
            //var Original = cmbOriginal.GetValue();
            //if (Original == null || Original == '') { Original = 0; }
            //var cmbAsignar = CPH_rg1_Restos.GetEditor("Ped_Asignar");
            //var Asignado = cmbAsignar.GetValue();
            //if (Asignado == null || Asignado == '') { Asignado = 0; }
            var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', original: '" + 0 + "', asignado: '" + 0 + "', EmpCnx: '" + EmpCnx + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/txtCantidad_TextChanged",
                data: dataValue,
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
                        if (id == 5) {
                            modalMensaje('No se puede modificar si existe producto Asignado, favor de desasignar para continuar.');
                        }
                        if (id == 3) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        if (id == 4) {
                            modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                        }
                        else {
                            var cmbimporte = CPH_rg1_Restos.GetEditor("Prd_Importe");
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
            var cmbidProd = CPH_rg1_Restos.GetEditor("Id_Prd");
            var idProd = cmbidProd.GetValue();
            var cmbcant = CPH_rg1_Restos.GetEditor("Prd_Cantidad");
            var cantidad = cmbcant.GetValue();
            var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/txtPrecio_TextChanged",
                data: dataValue,
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
                            var cmbimporte = CPH_rg1_Restos.GetEditor("Prd_Importe");
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
            var cmbOriginal = CPH_rg1.GetEditor("Prd_Original");
            var Original = cmbOriginal.GetValue();
            if (Original == null || Original == '') { Original = 0; }
            var cmbAsignado = CPH_rg1.GetEditor("Ped_Asignar");
            var Asignado = cmbAsignado.GetValue();
            if (Asignado == null || Asignado == '') { Asignado = 0; }
            var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', original: '" + Original + "', asignado: '" + Asignado + "', EmpCnx: '" + EmpCnx + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/txtCantidad_TextChanged",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    cmbCant.SetEnabled(true);
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    cmbCant.SetEnabled(true);
                    debugger;
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
                        if (id == 2) {
                            modalMensaje('Este pedido esta asignado y no puede ser modificado, desasignar para continuar.');
                            var cmbCantidad = CPH_rg1.GetEditor("Prd_Cantidad");
                            cmbCantidad.SetValue(data.cantidad);
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                        if (id == 3) {
                            modalMensaje('El producto cuenta con precio AAA especial');
                        }
                        if (id == 4) {
                            modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                        }
                        if (id == 5) {
                            modalMensaje('Este pedido esta asignado y no puede ser modificado, desasignar para continuar.');
                            var cmbCantidad = CPH_rg1.GetEditor("Prd_Cantidad");
                            cmbCantidad.SetValue(data.cantidad);
                            var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                            cmbimporte.SetValue(data.importe);
                        }
                        if (id == 6) {
                            modalMensaje('Este pedido esta asignado y no puede ser modificado, desasignar para continuar.');
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
            var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";
            $.ajax({
                type: "POST",
                url: "ProPedidoVI2.aspx/txtPrecio_TextChanged",
                data: dataValue,
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


        function modalMensajesolicitud(mensaje) {
            document.getElementById('<%=lblSolicitud.ClientID%>').innerHTML = mensaje;
            $("#mensajeSoliitud").appendTo("body")
            $("#mensajeSoliitud").modal({ "backdrop": "static" });
            $('#mensajeSoliitud').modal('show');
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
                <div class="modal-footer" style="margin-top: 3px; padding: 3px;">
                    <div class="row">
                        <div class="col-md-12 " style="margin-top: 5px;">
                            <button class="btn btn-default" id="Button1" onclick="closeModalDetalle()">
                                Cancelar</button>
                            <dx:bootstrapbutton ID="btncaptacion_Guardar" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Primary"
                                OnClick="btnguardar_Click">
                                <clientsideevents Click="function(s, e) {
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
                                <cssclasses Icon="fa fa-check" />
                            </dx:bootstrapbutton>
                        </div>
                    </div>
                </div>
                <div id="Tabs" role="tabpanel">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#tabCliente" aria-controls="tabClienteTab" data-toggle="tab">Datos Generales</a> </li>
                        <li><a href="#tabAcuerdoEconomico" aria-controls="tabAcuerdoEconomicoTab" data-toggle="tab">Detalle</a> </li>
                    </ul>
                </div>
                <div class="tab-content">
                    <%--TAB CLIENTE --%>
                    <div class="tab-pane active" id="tabCliente">
                        <div class="col-md-12">
                            <div class="row mt5">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                Folio del pedido:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtFolio" runat="server" MaxLength="9" ReadOnly="true" Enabled="false">
                                                    <ValidationSettings ValidationGroup="Validation">
                                                        <RequiredField IsRequired="true" ErrorText="Folio requerido" />
                                                    </ValidationSettings>
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                # ACyS:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtClave" runat="server" MaxLength="9" ReadOnly="true" Enabled="false">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                Orden compra / Requisición:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="TxtPed_ReqAcys" runat="server" MaxLength="50">
                                                </dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-md-1">
                                                Captado Por:
                                            </div>
                                            <div class="col-md-2">
                                                <dx:BootstrapTextBox ID="txtPedCaptadorPor" runat="server" MaxLength="50" ReadOnly="true"
                                                    Enabled="false">
                                                    <ValidationSettings ValidationGroup="Validation">
                                                        <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                    </ValidationSettings>
                                                </dx:BootstrapTextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-6">
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
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                    </dx:BootstrapTextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Puesto
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapTextBox ID="txtContactoPuesto" runat="server" MaxLength="50" Enabled="false">
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                    </dx:BootstrapTextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    Correo
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapButtonEdit ID="txtContactoMail" ClearButton-DisplayMode="OnHover" runat="server"
                                                        Enabled="false">
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="campo requerido" />
                                                        </ValidationSettings>
                                                    </dx:BootstrapButtonEdit>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Teléfono
                                                </div>
                                                <div class="col-md-4">
                                                    <dx:BootstrapTextBox ID="txtContactoTel" runat="server" Enabled="false">
                                                        <ValidationSettings ValidationGroup="Validation">
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
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
                                                    <input type="text" class="form-control" id="txtIdCte" runat="server" maxlength="9"
                                                        readonly="readonly" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="ddlClienteNom" runat="server" EnableCallbackMode="true"
                                                        CallbackPageSize="25" DropDownStyle="DropDown">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Territorio
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="text" class="form-control" id="txtIdTer" runat="server" maxlength="9"
                                                        readonly="readonly" onblur="txt2_OnBlur" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="ddlTerritorioNom" runat="server" CallbackPageSize="25"
                                                        DropDownStyle="DropDown" IncrementalFilteringMode="Contains" AutoPostBack="true" OnSelectedIndexChanged="ddlTerritorioNom_SelectedIndexChanged">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                        </ValidationSettings>
                                                        <ClearButton DisplayMode="Always" />
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    RIK
                                                </div>
                                                <div class="col-md-2">
                                                    <input type="text" class="form-control" id="txtIdRik" runat="server" maxlength="9"
                                                        readonly="readonly" onkeypress="handleClickEvent" />
                                                </div>
                                                <div class="col-md-8">
                                                    <input type="text" id="txtRikNom" class="form-control" runat="server" onpaste="return false"
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
                                                        <ClientSideEvents CalendarCustomDisabledDate="onCustomDisabledDate" DateChanged="onFechaEntregaChanged"  />
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
                                                    <div class="row ">
                                                       <div class="col-md-10">
                                                                Dirección de entrega producto
                                                        </div> 
                                                        <div class="col-md-2 text-right">
                                                            <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" 
                                                                    ToolTip="Buscar"  ValidationGroup="buscar" Visible="True" OnClick="ImgBuscarDireccionEntrega_Click" />
                                                        </div> 
                                                    </div>                                                
                                                   </div>
                                                <div class="panel-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Calle
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtCalle" runat="server" MaxLength="40" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings>
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            No.
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dx:BootstrapTextBox ID="txtNo" runat="server" MaxLength="15" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings>
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            C.P.
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dx:BootstrapTextBox ID="txtCp" runat="server" MaxLength="5" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings>
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                                <ValidationSettings>
                                                                    <RegularExpression ValidationExpression="^\d+$" ErrorText="Codigo Postal invalido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Colonia
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtColonia" runat="server" MaxLength="40" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings ValidationGroup="Validation">
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            Municipio
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtMunicipio" runat="server" MaxLength="40" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings ValidationGroup="Validation">
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            Estado
                                                        </div>
                                                        <div class="col-md-4">
                                                            <dx:BootstrapTextBox ID="txtEstado" runat="server" MaxLength="20" ReadOnly="true" Enabled="false">
                                                                <ValidationSettings ValidationGroup="Validation">
                                                                    <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                                </ValidationSettings>
                                                            </dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                       
                                            <div class="panel panel-default">
                                                <div class="panel-heading titulo_blod">
                                                    <div class="row ">
                                                        <div class="col-md-10">
                                                        </div> 
                                                        <div class="col-md-2 text-right">
                   
                                                        </div> 
                                                    </div>                                                
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-md-12">
                                                            <div class="col-md-2">
                                                                USO de CFDI
                                                            </div>
                                                            <div class="col-md-4">
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

                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading titulo_blod">
                                                    Documentación requerida para entrega
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="CHKFacKey" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Factura KEY  
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblFacturakey" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="ChkOrdCompra" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Orden de compra / Release  
                                                              
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblOrdenCompraCopias" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="ChkOrdReposicion" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Orden de reposición  
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblOrdenRepo" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="chkFolio" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Folio  
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblFolio" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="CHKCopiaPed" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Copia de Pedido
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblCopia" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                            <input type="checkbox" runat="server" class="form-control chb" id="CHKRemision" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            Remisión  
                                                        </div>
                                                        <div class="col-md-1">
                                                            #:
                                                        </div>
                                                        <div class="col-md-1">
                                                            <dx:BootstrapSpinEdit runat="server" ID="lblremision" Width="50px" class="form-control" Number="0" MinValue="0" MaxValue="10"></dx:BootstrapSpinEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="col-md-1">
                                                        </div>
                                                        <div class="col-md-2">
                                                            Otro
                                                        </div>
                                                        <div class="col-md-8">
                                                            <input type="text" id="TxtEOtro" class="form-control" runat="server" maxlength="225" />
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
                                                Contactos del Cliente
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-3">
                                                        Nombre
                                                    </div>
                                                    <div class="col-md-3">
                                                        Teléfono
                                                    </div>
                                                    <div class="col-md-3">
                                                        Correo
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        Pagos
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtPagoNombre" runat="server" MaxLength="50">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtPagoTelefono" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapButtonEdit ID="txtPagoCorreo" ClearButton-DisplayMode="OnHover" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="campo requerido" />

                                                            </ValidationSettings>
                                                        </dx:BootstrapButtonEdit>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        Compras
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtComprasNombre" runat="server" MaxLength="50">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtComprasTelefono" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapButtonEdit ID="txtComprasCorreo" ClearButton-DisplayMode="OnHover" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                                <RequiredField IsRequired="true" ErrorText="Campo requerido" />

                                                            </ValidationSettings>
                                                        </dx:BootstrapButtonEdit>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        Almacen
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtAlmacenNombre" runat="server" MaxLength="50">
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="txtAlmacenTelefono" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapButtonEdit ID="txtAlmacenCorreo" ClearButton-DisplayMode="OnHover" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                            </ValidationSettings>
                                                        </dx:BootstrapButtonEdit>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        Mantenimiento
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="TxtMtoNombre" runat="server" MaxLength="50">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapTextBox ID="TxtMtoTelefono" runat="server" MaxLength="10">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                            </ValidationSettings>
                                                        </dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <dx:BootstrapButtonEdit ID="TxtMtoCorreo" ClearButton-DisplayMode="OnHover" runat="server">
                                                            <ValidationSettings ValidationGroup="Validation">
                                                            </ValidationSettings>
                                                        </dx:BootstrapButtonEdit>
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
                                                            <dx:BootstrapMemo runat="server" ID="txtObservaciones" NullText="Enter Your Description" MaxLength="200" Rows="5">
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
                    <%--TAB ACUERDO ECONOMICO --%>
                    <asp:UpdatePanel runat="server" ID="updetalle"></asp:UpdatePanel>
                    <div class="tab-pane" id="tabAcuerdoEconomico">
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
                                        <div class="col-md-2">
                                            Requiere Orden de compra:
                                        </div>
                                        <div class="col-md-3">
                                            <dx:BootstrapComboBox runat="server" ID="dddlRequiereOrdenCompra" AutoPostBack="true" OnSelectedIndexChanged="dddlRequiereOrdenCompra_SelectedIndexChanged">
                                                <Items>
                                                    <dx:BootstrapListEditItem Text="--seleccionar--" Value="-1" Selected="true" />
                                                    <dx:BootstrapListEditItem Text="Sí" Value="1" />
                                                    <dx:BootstrapListEditItem Text="No" Value="0" />
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
                                                                            AllowedFileExtensions=".pdf, .xlsx, .jpg, .jpeg, .png"
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
                                                                        <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text="Permite Extensiones: .PDF, .XLXS,  .JPG, .JPEG, .PNG." Font-Size="8pt" />
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
                            <div class="col-md-12" style="margin-top: 1%">
                                <button type="submit" runat="server" class="btn btn-primary" id="Button2" onserverclick="Button2_ServerClick">
                                    Eliminar producto
                                </button>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%">
                            <dx:BootstrapGridView ID="rg1" runat="server" KeyFieldName="Id_Prd" Width="100%"
                                EnableRowsCache="false" OnRowInserting="rg1_RowInserting" OnRowUpdating="rg1_RowUpdating"
                                OnRowDeleting="rg1_RowDeleting" EnableCallBacks="true"  OnDataBound="rg1_DataBound" OnHtmlDataCellPrepared="rg1_HtmlDataCellPrepared">
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                              

                                <SettingsDataSecurity AllowEdit="true" AllowDelete="true" />
                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />

                                <SettingsCommandButton>
                                    <EditButton IconCssClass="fa fa-edit" Text=" " />
                                    <DeleteButton IconCssClass="fa fa-remove"  Text=""   />
                                    <CancelButton IconCssClass="fa fa-ban" Text=" " />
                                    <UpdateButton IconCssClass="fa fa-check" Text=" " />
                                    <NewButton IconCssClass="fa fa-plus" Text=" " />
                                </SettingsCommandButton>
                                <ClientSideEvents EndCallback="function(s, e) { 
                 Subtotal();  }" />
                                <Columns>
                                    <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" Width="50px" />
                                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true"
                                        Caption=" " />
                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" Visible="false" />
                                     <dx:BootstrapGridViewTextColumn FieldName="Prd_Activo" Caption="Prd_Activo" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" CssClasses-HeaderCell="centerText" Caption="Núm." ReadOnly="true">
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
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Presentacion" ReadOnly="true" Caption="Presen." Width="200">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Unidad"  ReadOnly="true" Caption="Unidad">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Original" ReadOnly="true" Caption="Original">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Cantidad" Caption="Cant." Width="80">
                                        <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                                        </PropertiesSpinEdit>
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_Precio" Caption="Precio vta.">
                                        <PropertiesSpinEdit DisplayFormatString="c">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedPrecioIndexChangedVI" />
                                        </PropertiesSpinEdit>
                                        <Settings AllowFilterBySearchPanel="False" />
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
                                        <Settings AllowFilterBySearchPanel="False" />
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
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewComboBoxColumn>
                                    <dx:BootstrapGridViewComboBoxColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" Caption="Requiere Orden de Compra" FieldName="ACS_ReqOC" Width="100px">
                                        <PropertiesComboBox TextField="Acs_reDoc" ValueField="Acs_reDoc" EnableSynchronization="False" AllowNull="false">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Sí" Value="1" />
                                                <dx:BootstrapListEditItem Text="No" Value="0" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewComboBoxColumn>
                                    <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Ped_Asignar" ReadOnly="true" Caption="Asignado">
                                    </dx:BootstrapGridViewTextColumn>
                                </Columns>
                                <SettingsPager PageSize="20" NumericButtonCount="6">
                                    <Summary Visible="false" />
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                </SettingsPager>
                            </dx:BootstrapGridView>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%">
                            Producto Venta nueva y/o esporadicos 
                       
                            <div class="col-md-12" style="margin-top: 1%">
                                <button type="submit" runat="server" class="btn btn-primary" id="btneliminarSelccionar" onserverclick="btneliminarSelccionar_ServerClick">
                                    Eliminar producto
                                </button>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%">
                            <dx:BootstrapGridView ID="rg1_Restos" runat="server" KeyFieldName="Id_Prd" Width="100%"
                                EnableRowsCache="false" OnRowInserting="rg1_Restos_RowInserting" OnRowUpdating="rg1_Restos_RowUpdating"
                                OnRowDeleting="rg1_Restos_RowDeleting" EnableCallBacks="true" OnHtmlDataCellPrepared="rg1_Restos_HtmlDataCellPrepared">
                               
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                <SettingsDataSecurity AllowInsert="true" AllowEdit="true" AllowDelete="true" />
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
                                    <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" Width="50px" />
                                    <dx:BootstrapGridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true"
                                        ShowDeleteButton="true" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Activo" Caption="Prd_Activo" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" CssClasses-HeaderCell="centerText" Caption="Núm.">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents TextChanged="onSelectedIndexChanged" />
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" ReadOnly="true" Caption="Producto" Width="200">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" HorizontalAlign="Center" CssClasses-HeaderCell="centerText" ReadOnly="true" Caption="Presen.">
                                        <PropertiesTextEdit>
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" HorizontalAlign="Center" CssClasses-HeaderCell="centerText" ReadOnly="true" Caption="Unidad">
                                    </dx:BootstrapGridViewTextColumn>
                                    <%--<dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Prd_Original" ReadOnly="true" Caption="Original">
                                    </dx:BootstrapGridViewTextColumn>--%>
                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" CssClasses-HeaderCell="centerText" Caption="Cant." Width="80">
                                        <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedCantidadIndexChanged" />
                                        </PropertiesSpinEdit>
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" CssClasses-HeaderCell="centerText" Caption="Precio vta.">
                                        <PropertiesSpinEdit DisplayFormatString="c">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <ClientSideEvents NumberChanged="onSelectedPrecioIndexChanged" />
                                        </PropertiesSpinEdit>
                                    </dx:BootstrapGridViewSpinEditColumn>
                                          <dx:BootstrapGridViewSpinEditColumn CssClasses-HeaderCell="centerText" FieldName="Prd_PrecioLista" ReadOnly="true" Caption="Precio de lista">
                                        <PropertiesSpinEdit DisplayFormatString="c"> 
                                        </PropertiesSpinEdit>
                                        <Settings AllowFilterBySearchPanel="False" />
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" CssClasses-HeaderCell="centerText" ReadOnly="true" Caption="Importe">
                                        <PropertiesTextEdit DisplayFormatString="c">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewComboBoxColumn Caption="Doc. de entrega" HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Acs_Doc">
                                        <PropertiesComboBox TextField="Acs_Doc" ValueField="Acs_Doc" EnableSynchronization="False">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Factura" Value="F" />
                                                <dx:BootstrapListEditItem Text="Remisión" Value="R" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:BootstrapGridViewComboBoxColumn>
                                    <dx:BootstrapGridViewComboBoxColumn Caption="Requiere Orden de Compra" HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="ACS_ReqOC" Width="100px">
                                        <PropertiesComboBox TextField="Acs_reDoc" ValueField="Acs_reDoc" EnableSynchronization="False">
                                            <ValidationSettings RequiredField-IsRequired="true">
                                            </ValidationSettings>
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Sí" Value="1" />
                                                <dx:BootstrapListEditItem Text="No" Value="0" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:BootstrapGridViewComboBoxColumn>
<%--                                      <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" CssClasses-HeaderCell="centerText" FieldName="Ped_Asignar" ReadOnly="true" Caption="Asignado">
                                    </dx:BootstrapGridViewTextColumn>--%>
                                </Columns>
                                <SettingsPager PageSize="20" NumericButtonCount="6">
                                    <Summary Visible="false" />
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                </SettingsPager>
                            </dx:BootstrapGridView>
                        </div>
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


                <asp:HiddenField ID="HiddenRebind" runat="server" />
                <asp:HiddenField ID="HF_InicioSemana" runat="server" />
                <asp:HiddenField ID="HF_FinSemana" runat="server" />
                <asp:HiddenField ID="HF_FechaActual" runat="server" />
                <asp:HiddenField ID="HiddenHeight" runat="server" />
                <asp:HiddenField ID="HF_IdCd" runat="server" />
                <asp:HiddenField ID="HF_IdEmp" runat="server" />
                <asp:HiddenField ID="HF_Emp_Cnx" runat="server" />
                <asp:HiddenField ID="HF_ID" runat="server" />
                <asp:HiddenField ID="HF_Sem" runat="server" />
                <asp:HiddenField ID="HF_pedido" runat="server" />
                <asp:HiddenField ID="HF_Anio" runat="server" />
                <asp:HiddenField ID="HF_Param" runat="server" />
                <asp:HiddenField ID="TabName" runat="server" />
                <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                <div style="display: none">
                    <asp:Button runat="server" OnClick="btnActualizarDireccion_Click" ID="BtnHiddenDireccion" />
                    <asp:Button runat="server" OnClick="btnActualizardatos_Click" ID="BtnHiddenacys" />
                    <asp:Button runat="server" OnClick="btnCoreccion" ID="btnHiddenCorreccion" />
                </div>
                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
            </ContentTemplate>
        </asp:UpdatePanel>
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
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
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
    <div class="modal fade" id="modalInventario" style="height: 1000px !important" class="modal"
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
                        <asp:Label runat="server" ID="Label2"></asp:Label>
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
    <div class="modal fade" id="ModalVI" class="modal" role="dialog" style="height: 700px !important; z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h4">
                        <asp:Label runat="server" ID="Label1"></asp:Label>
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
     <div class="modal fade" id="ModalAlerta" class="modal" role="dialog" style="height: 1000px !important; z-index: 2220!important">
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
            <div id="mensajeSoliitud" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important; width: 700px; margin-left:auto; margin-right: auto;"
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
                    <div class="col-md-12">
                        <span id="lblSolicitud" runat="server"></span>
                    </div>
                </div>
                
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-5 ">

                    </div>
                    <div class="col-md-4 ">
                        <table>
                            <tr>
                                <td>
                                    <button class="btn btn-primary" data-dismiss="modal" id="Button3" runat="server" onserverclick="ButtonSolicitud_ServerClick">
                                        Solicitar Autorización</button>
                                </td>
                                 <td>
                                    <button class="btn btn-warning" data-dismiss="modal" id="Button9">
                                        Cancelar</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
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