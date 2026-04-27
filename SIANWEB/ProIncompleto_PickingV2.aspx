<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="ProIncompleto_PickingV2.aspx.cs"
    Inherits="SIANWEB.ProIncompleto_PickingV2" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <style>
        body {
            font-size:14px;
        }
    </style>

    <div class="container-fluid">
        <div class="row mt-4">
            <div class="col-8">
                <p>Producto: <b><%=HF_Ped %> - <%=ProductoNombre %></b></p>
            </div>
            <div class="col-4">
                <p>Presentanci&oacute;n: <b><%=Presentacion %></b></p>
            </div>
            <div class="col-4">
                <p>Inventario: <b><%=Inventario %></b></p>
            </div>
            <div class="col-4">
                <p>Asignado: <b><%=Asignado %></b></p>
            </div>
            <div class="col-4">
                <p>Disponible: <b><%=Disponible %></b></p>
            </div>
        </div>
        <hr class="mt-2 mb-2" />
        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    <label>No Encontrado</label>
                    <input type="number" id="noEncontrado" min="0" max="9" value="0" oninput="onlyNumbers(this)" class="form-control" />
                    <asp:HiddenField runat="server" ID="TxtNoEncontrado" />
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <label>No Conforme</label>
                    <input type="number" id="noConforme" min="0" max="9" value="0" oninput="onlyNumbers(this)" class="form-control" />
                    <asp:HiddenField runat="server" ID="TxtNoConforme" />
                </div>
            </div>
            <div class="col-4 text-right align-self-center mt-3">
                <button class="btn btn-success" onclick="assignValues(); return false">Confirmar</button>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function assignValues() {
            var noEncontrado = parseInt(document.getElementById('noEncontrado').value)
            var noConforme = parseInt(document.getElementById('noConforme').value)

            if (noEncontrado > 0 || noConforme > 0) {
                if (noEncontrado > 0 && noConforme > 0) {
                    if (<%=Inventario%> < (noEncontrado + noConforme))
                        showAlertError("Favor de revisar el producto No Encontrado + No Conforme no puede ser mayor al inventario")
                    else {
                        request(noEncontrado, noConforme);
                    }

                } else {
                    if (noEncontrado > 0) {
                        if (<%=Inventario%> < (noEncontrado))
                            showAlertError("Favor de revisar el Producto No Encontrado no puede ser mayor al inventario")
                        else {
                            request(noEncontrado, 0)
                        }
                    }

                    if (noConforme > 0) {
                        if (<%=Inventario%> < (noConforme))
                            showAlertError("Favor de revisar el Producto No Conforme no puede ser mayor al inventario")
                        else {
                            request(0, noConforme)
                        }
                    }
                }

            } else {
                if (noEncontrado == 0 && noConforme == 0)
                    showAlertError('Favor de capturar la cantidad del producto No Encontrado o No Conforme')
                else {
                    if (noEncontrado == 0) showAlertError('Favor de capturar la cantidad del producto No Encontrado')
                    if (noConforme == 0) showAlertError('Favor de capturar la cantidad del producto No Conforme')
                }
            }
        }

        function onlyNumbers(elm) {
            elm.value = elm.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1')
        }

        function request(noEncontrado, noConforme) {
            showLoader()
            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify({
                    noEncontrado: noEncontrado,
                    noConforme: noConforme
                }),
                url: 'ProIncompleto_PickingV2.aspx/Confirmar',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    hideLoader()
                    if (!data.d.Status) {
                        if (data.d.Message == 'connection close') closeThisWindow()
                        else showAlertError(data.d.Message)
                    }
                    else showAlertSuccess(data.d.Message)
                },
                error: function () {
                    hideLoader()
                }
            })
        }
    </script>
</asp:Content>