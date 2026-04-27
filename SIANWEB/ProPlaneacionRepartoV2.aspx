<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="ProPlaneacionRepartoV2.aspx.cs"
    Inherits="SIANWEB.ProPlaneacionRepartoV2" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <style>
        body {
            font-size:12px;
        }
        .table td {
            vertical-align: middle;
            text-align: center;
        }
        .table th {
            text-align: center;
        }
        .btns {
            background-color: #039fdb;
            color: white;
        }
    </style>
    <div class="container-fluid">
        <div class="row mt-4">
            <div class="col-3">
                <p>Pedido: <b><%=pedidoId.Value %></b></p>
            </div>
            <div class="col-3">
                Fecha: <b><asp:Label runat="server" ID="fecha"></asp:Label></b>
            </div>
            <div class="col-3">
                Territorio: <b><asp:Label runat="server" ID="territorio"></asp:Label></b>
            </div>
            <div class="col-3">
                Credito: <b><asp:Label runat="server" ID="credito"></asp:Label></b>
            </div>
            <div class="col-8">
                <p>Cliente: <b><asp:Label runat="server" ID="idCliente"></asp:Label> - <asp:Label runat="server" ID="cliente"></asp:Label></b></p>
            </div>
            <div class="col-4">
                <p>Ruta:</p>
            </div>
        </div>
        <hr class="mt-1 mb-1" />
        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    <label>Producto</label>
                    <input type="text" runat="server" id="producto" class="form-control" />
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <label>Producto inicial</label>
                    <input type="text" runat="server" id="productoInicial" oninput="asigOnInput(this)" class="form-control" />
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <label>Producto final</label>
                    <input type="text" runat="server" id="productoFinal" oninput="asigOnInput(this)" class="form-control" />
                </div>
            </div>
            <div class="col-6 mt-2">
                <button class="btn btns" onclick="search(); return false">Buscar</button>
            </div>
            <div class="col-6 mt-2 text-right">
                <button class="btn btn-success" onclick="asignar(); return false">Confirmar</button>
            </div>
        </div>

        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row mt-2">
                    <div class="col">
                        <table id="tblPicking" class="table table-bordered table-sm">
                            <thead>
                                <tr>
                                    <th>Terr.</th>
                                    <th>Prod.</th>
                                    <th>Descripci&oacute;n</th>
                                    <th>Cant. Pedido</th>
                                    <th>Cant. Facturada</th>
                                    <th>Cant. Remisionada</th>
                                    <th>Cant. Asignada</th>
                                    <th>Cant. Picking</th>
                                    <th>Faltante</th>
                                    <th>Disponible</th>
                                    <th>Pzas No Conf</th>
                                    <th>Pzas No Enc</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (PedidoDets != null) { %>
                            
                                    <% int i = 0; %>
                                    <% foreach (var item in PedidoDets) { %>
                                        <tr>
                                            <td>
                                                <span><%=item.Id_Ter %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Prd %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Prd_Desc %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Prd_Ord %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_CantF %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_CantR %></span>
                                            </td>
                                            <td>
                                                <span id="asig_<%=i %>">
                                                    <%=item.Prd_Asig %>
                                                </span>
                                            </td>
                                            <%--<td>
                                                <input type="text"
                                                    id="asig_<%=i %>"
                                                    oninput="asigOnInput(this)"
                                                    class="form-control text-center"
                                                    onblur="asigOnblur(this, <%=item.Prd_Disponible %>, <%=item.Prd_Asig %>, <%=item.Prd_Faltante %>)"
                                                    value="<%=item.Prd_Asig %>"/>
                                            </td>--%>
                                            <td>
                                                <input type="text"
                                                    id="picking_<%=i %>"
                                                    oninput="asigOnInput(this)"
                                                    class="form-control text-center"
                                                    onblur="pickingOnblur(this, <%=item.Prd_Asig %>, <%=item.Ped_Picking %>)"
                                                    value="<%=item.Ped_Picking %>"/>
                                            </td>
                                            <td>
                                                <span><%=item.Prd_Faltante %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Prd_Disponible %></span>
                                            </td>
                                            <td>
                                                <input type="text"
                                                    id="noconf_<%=i %>"
                                                    class="form-control text-center"
                                                    value="<%=item.Prd_NoConf %>"/>
                                            </td>
                                            <td>
                                                <input type="text"
                                                    id="noenc_<%=i %>"
                                                    class="form-control text-center"
                                                    value="<%=item.Prd_NoEnc %>"/>
                                            </td>
                                             <td style="display:none;">
                                                 <input type="hidden" id="productoId_<%=i %>" value="<%=item.Id_Prd %>" />
                                             </td>
                                        </tr>
                                    <% i++; %>
                                    <% } %>
                            
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <asp:Button runat="server" style="display:none" ID="refresh" OnClick="Refresh_Click" />
                <asp:Button runat="server" ID="search" OnClick="Search_Click" style="display:none" />
           </ContentTemplate>
       </asp:UpdatePanel>

        <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
        <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />
        <asp:HiddenField ID="pedidoId" runat="server" />
    </div>
    <%--BRB--%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const rows = document.querySelectorAll('#tblPicking tbody tr');
            rows.forEach((row, i) => {
                const disponible = parseFloat(row.cells[9].innerText.trim()) || 0;
                const faltante = parseFloat(row.cells[8].innerText.trim()) || 0;
                const asignada = parseFloat(row.cells[6].innerText.trim()) || 0;
                const picking = parseFloat(row.querySelector(`#picking_${i}`)?.value) || 0;
                const noconf = row.querySelector(`#noconf_${i}`);
                const noenc = row.querySelector(`#noenc_${i}`);
                const validarValor = () => {
                    const valorNoconf = parseFloat(noconf?.value) || 0;
                    const valorNoenc = parseFloat(noenc?.value) || 0;
                    const totalIngresado = valorNoconf + valorNoenc;
                    // permitir si asignada y picking son 0
                    if (asignada > 0 || picking > 0) {
                        if (totalIngresado > 0) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Acción no permitida',
                                text: 'Debe desasignar y despickear antes de ingresar valores.',
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Entendido'
                            }).then(() => {
                                if (noconf) noconf.value = 0;
                                if (noenc) noenc.value = 0;
                            });
                            return;
                        }
                    }
                    // Validacion contra disponible
                    if (totalIngresado > disponible) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Cantidad no válida',
                            text: `La suma de No Conforme y No Encontrado no puede ser mayor que la cantidad disponible (${disponible})`,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Entendido'
                        }).then(() => {
                            if (noconf) noconf.value = 0;
                            if (noenc) noenc.value = 0;
                        });
                        return;
                    }
                    // si faltante es 0 y no entra en condición
                    if (faltante === 0) {
                        if (totalIngresado > 0) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'No permitido',
                                text: 'No hay faltante, no se puede ingresar No Conforme o No Encontrado.',
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Entendido'
                            }).then(() => {
                                if (noconf) noconf.value = 0;
                                if (noenc) noenc.value = 0;
                            });
                            return;
                        }
                    }
                    // Si asignada=0 y picking=0 y faltante>0, permitir hasta faltante
                    if (asignada === 0 && picking === 0 && faltante > 0) {
                        if (totalIngresado > faltante) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Cantidad no válida',
                                text: `La suma no puede ser mayor que el faltante (${faltante})`,
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Entendido'
                            }).then(() => {
                                if (noconf) noconf.value = 0;
                                if (noenc) noenc.value = 0;
                            });
                        }
                        return;
                    }
                    // faltante=0 pero con asignada y picking
                    if (faltante === 0 && asignada > 0 && picking > 0) {
                        if (totalIngresado > asignada) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Cantidad no válida',
                                text: `La suma no puede ser mayor que la cantidad asignada (${asignada})`,
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Entendido'
                            }).then(() => {
                                if (noconf) noconf.value = 0;
                                if (noenc) noenc.value = 0;
                            });
                        }
                        return;
                    }
                };
                if (noconf) {
                    noconf.addEventListener('blur', validarValor);
                }
                if (noenc) {
                    noenc.addEventListener('blur', validarValor);
                }
            });
        });
    </script>
    <%--BRB-END--%>
    <script>   
        function asigOnInput(elm) {        
            elm.value = elm.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1')
        }

        function search() {
            showLoader();
            document.getElementById('<%=search.ClientID%>').click()
        }

        function asigOnblur(elm, prd_disponible, prd_asig, prd_faltante) {

            var HF = document.getElementById('<%= HF_Guardar.ClientID %>')
            var HFRB = document.getElementById('<%= HiddenRebind.ClientID %>')

            if (elm.value > prd_disponible) {
                elm.value = prd_asig
                showAlertError('Cantidad asignada es mayor que la disponible ordenada')
                HF.value = 'false'
            }
            else if (elm.value - prd_asig > prd_disponible) {
                elm.value = prd_asig
                showAlertError('No se cuenta con el inventario suficiente')
                HF.value = 'false'
            }
            else if (elm.value > prd_faltante) {
                elm.value = prd_asig
                showAlertError('Cantidad asignada es mayor que lo que le falta al pedido')
                HF.value = 'false'
            }

            if (elm.value == '') elm.value = '0'
            HFRB.value = 'true'
        }

        function pickingOnblur(elm, prd_asig, ped_picking) {

            var HF = document.getElementById('<%= HF_Guardar.ClientID %>')
            var HFRB = document.getElementById('<%= HiddenRebind.ClientID %>')

            if (elm.value > prd_asig) {
                elm.value = ped_picking
                showAlertError('Cantidad picking es mayor que la cantidad asignada')
                HF.value = 'false'
            }
            else if (elm.value - ped_picking > prd_asig) {
                elm.value = ped_picking
                showAlertError('No se cuenta con el asignado suficiente')
                HF.value = 'false'
            }

            HFRB.value = 'true'
        }

        function asignar() {

            let model = []
            let i = 0;
            let clienteId = jQuery("#<%=idCliente.ClientID%>").text();
            jQuery('#tblPicking > tbody > tr').each((index, tr) => {
                model.push({
                    productoId: jQuery('#productoId_' + i).val(),
                    cantAsignada: jQuery('#asig_' + i).text().trim(),
                    cantPicking: jQuery('#picking_' + i).val(),
                    pzasNoConf: jQuery('#noconf_' + i).val(),
                    pzasNoEnc: jQuery('#noenc_' + i).val()
                })
                i++
            })

            var data = {
                HF_Guardar: jQuery("#<%=HF_Guardar.ClientID%>")[0].value,
                pedidoId: jQuery("#<%=pedidoId.ClientID%>")[0].value,
                clienteId: clienteId,
                model
            }

            request(data)
        }

        function request(data) {
            showLoader()
            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify(data),
                url: 'ProPlaneacionRepartoV2.aspx/Asignar',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    hideLoader()
                    if (data.d.Rebind) {
                        document.getElementById('<%=refresh.ClientID%>').click()
                    }

                    if (!data.d.Status) {
                        if (data.d.Message == 'connection close') closeThisWindow()
                        else if (data.d.Message == 'hf_guardar=true') {
                            document.getElementById('<%=HF_Guardar.ClientID%>').value = 'true'
                        }
                        else showAlertError(data.d.Message)
                    }
                    else showAlertSuccess(data.d.Message, true)
                },
                error: function () {
                    hideLoader()
                }
            })
        }
    </script>
</asp:Content>