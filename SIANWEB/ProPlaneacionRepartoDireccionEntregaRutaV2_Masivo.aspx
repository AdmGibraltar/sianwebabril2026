<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo.aspx.cs"
    Inherits="SIANWEB.ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <style>
        .floating-label {
            position: absolute;
            top: 0.6rem;
            left: 0.75rem;
            padding: 0 0.25rem;
            background: white;
            color: #777;
            font-size: 0.85rem;
            transition: all 0.2s ease;
            pointer-events: none;
        }

        .form-control:focus + .floating-label,
        .form-control:not(:placeholder-shown) + .floating-label {
            top: -0.6rem;
            left: 0.65rem;
            background: white;
            font-size: 0.75rem;
            color: #007bff;
        }
    </style>
    <div class="container-fluid">
        <h5 class="mb-3">Cambiar ruta para los siguientes pedidos</h5>
        <div class="table-responsive mb-4">
            <table class="table table-bordered table-hover table-sm">
                <thead class="thead-dark">
                    <tr>
                        <th>Pedido</th>
                        <th>Cliente</th>
                        <th>Ruta actual</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var ped in Pedidos)
                        { %>
                    <tr>
                        <td><b><%= ped.Id_Ped %></b>
                            <input type="hidden" class="pedido-id" value="<%= ped.Id_Ped %>" />
                        </td>
                        <td><%= ped.Id_Cte %> - <%= ped.Cte_NomComercial %></td>
                        <td><%= ped.Ruta %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
        <div class="form-row">
            <div class="col-md-6">
            </div>
            <div class="col-md-4" style="padding-right: 5px;">
                <div class="form-group position-relative">
                    <select class="form-control" id="rutaGlobal" onchange="verificarSeleccionRuta(this)">
                        <% foreach (var ruta in Rutas)
                            { %>
                        <option value="<%= ruta.Id %>"><%= ruta.Descripcion %></option>
                        <% } %>
                        <option value="nueva">-- Nueva ruta... --</option>
                    </select>
                    <label for="rutaGlobal" class="floating-label">Ruta a aplicar</label>
                </div>
            </div>
            <div class="col-md-2 text-right" style="padding-left: 5px;">
                <button class="btn btn-success" onclick="guardarRutaMasiva(); return false;">
                    Aplicar nueva ruta
                </button>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalNuevaRuta" tabindex="-1" role="dialog" aria-labelledby="modalNuevaRutaLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Crear nueva ruta</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-row">
                        <div class="form-group col-md-8 position-relative">
                            <input type="text" id="claveRuta" class="form-control" placeholder=" " readonly />
                            <label for="claveRuta" class="floating-label">Clave</label>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnClaveRuta" ClientIDMode="Static" />
                        <%--<div class="form-group col-md-4 d-flex align-items-center">
                            <div class="form-check mt-2">
                                <input type="checkbox" class="form-check-input" id="rutaActiva" checked />
                                <label class="form-check-label" for="rutaActiva">Activa</label>
                            </div>
                        </div>--%>
                    </div>

                    <div class="form-group position-relative">
                        <input type="text" id="descripcionRuta" class="form-control" placeholder=" " />
                        <label for="descripcionRuta" class="floating-label">Descripción</label>
                    </div>

                    <div class="form-group position-relative">
                        <select class="form-control" id="auxiliares" required>
                            <% if (Auxiliar != null && Auxiliar.Count > 0)
                                {
                                    foreach (var ruta in Auxiliar)
                                    { %>
                            <option value="<%= ruta.Id %>"><%= ruta.Descripcion %></option>
                            <% }
                                } %>
                        </select>
                        <label for="auxiliares" class="floating-label">Auxiliar almacén</label>
                        <asp:HiddenField runat="server" ID="cmbAuxiliar" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary" onclick="guardarNuevaRuta()">Guardar</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HF_ClaveRuta" runat="server" />
    <asp:HiddenField ID="HF_DescripcionRuta" runat="server" />
    <asp:HiddenField ID="HF_Auxiliar" runat="server" />
    <asp:HiddenField ID="HF_Activa" runat="server" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function verificarSeleccionRuta(selectElement) {
            if (selectElement.value === 'nueva') {
                $('#modalNuevaRuta').modal('show');
                const clave = document.getElementById('hdnClaveRuta').value;
                document.getElementById('claveRuta').value = clave;
            }
        }
    </script>
    <script>
        function guardarNuevaRuta() {
            const clave = document.getElementById('claveRuta').value.trim();
            const descripcion = document.getElementById('descripcionRuta').value.trim();
            const auxiliar = document.getElementById('auxiliares').value;

            if (!clave || !descripcion || !auxiliar || auxiliar === "-1") {
                Swal.fire({
                    icon: 'warning',
                    title: 'Campos obligatorios',
                    html: 'Por favor completa todos los campos requeridos:<b>Descripción</b> y <b>Auxiliar</b>.',
                    confirmButtonText: 'Aceptar'
                });
                return;
            }

            const select = document.getElementById('rutaGlobal');
            const option = document.createElement('option');
            option.value = clave;
            option.text = descripcion;
            option.selected = true;

            const nuevaOption = select.querySelector('option[value="nueva"]');
            select.insertBefore(option, nuevaOption);

            document.getElementById('claveRuta').value = '';
            document.getElementById('descripcionRuta').value = '';
            

            $.ajax({
                type: "POST",
                url: "ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo.aspx/GuardarRuta",
                data: JSON.stringify({ clave, descripcion, auxiliar: parseInt(auxiliar) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: '¡Éxito!',
                        text: 'Ruta creada correctamente.',
                        timer: 2000,
                        showConfirmButton: false
                    }).then(() => {
                        $('#modalNuevaRuta').modal('hide');
                    });
                },
                error: function (xhr, status, error) {
                    alert('Error al guardar ruta: ' + error);
                }
            });
        }
    </script>
    <script>
        function guardarRutaMasiva() {
            const nuevaRutaId = document.getElementById('rutaGlobal').value;

            if (!nuevaRutaId || nuevaRutaId === "-1") {
                Swal.fire({
                    icon: 'warning',
                    title: 'Ruta no seleccionada',
                    text: 'Por favor selecciona una ruta válida.',
                    confirmButtonText: 'Aceptar'
                });
                return;
            }

            const pedidoIds = [];
            document.querySelectorAll('.pedido-id').forEach(el => {
                pedidoIds.push(el.value);
            });

            if (pedidoIds.length === 0) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Sin pedidos',
                    text: 'No se encontraron pedidos para actualizar.',
                    confirmButtonText: 'Aceptar'
                });
                return;
                return;
            }

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Se aplicará la nueva ruta a los pedidos seleccionados.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, aplicar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch('ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo.aspx/CambiarRutaEntrega', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            ruta: nuevaRutaId,
                            pedidosIds: pedidoIds
                        })
                    })
                        .then(response => response.json())
                        .then(data => {
                            console.log('Respuesta del servidor:', data);
                            if (data.d && data.d.Status) {
                                Swal.fire({
                                    icon: 'success',
                                    title: '¡Éxito!',
                                    text: 'Ruta actualizada correctamente.',
                                    timer: 2000,
                                    showConfirmButton: false
                                }).then(() => {
                                    if (window.parent && typeof window.parent.search === 'function') {
                                        window.parent.search();
                                    }
                                    if (window.parent && typeof window.parent.closeModalWindow === 'function') {
                                        window.parent.closeModalWindow();
                                    }
                                });
                            } else {
                                alert('Error al actualizar rutas: ' + (data.d.Message || 'Desconocido'));
                            }
                        })
                        .catch(error => {
                            console.error('Error en la solicitud fetch:', error);
                            alert('Error en la solicitud.');
                        });
                }
            });
        }
    </script>
</asp:Content>
