<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="ProAsignPedxPrd_PickingV2.aspx.cs"
    Inherits="SIANWEB.ProAsignPedxPrd_PickingV2" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <style>
        body {
            font-size:13px;
        }
        .table td {
            vertical-align: middle;
            text-align: center;
        }
        .table th {
            text-align: center;
        }
        .table input[type=text] {
            width: 70px;
            display:inline;
        }
        .btns {
            background-color: #039fdb;
            color: white;
        }
    </style>
    <div class="container-fluid">
        <div class="row mt-2">
            <div class="col-6">
                <p>Producto: <b><%=HF_Ped %> - <%=ProductoNombre %></b></p>
            </div>
            <div class="col-6">
                <% if (ckAgrupador==false) { %>
                <p>Tipo pedido: <b><%=TipoPedido %></b></p>
                 <%} %>
            </div>
            <div class="col-6">
                <p>Ruta: <b><%=Ruta %></b></p>
            </div>
            <div class="col-3">
                <% if (ckAgrupador==false) { %>
                <p>Parcialidades: <b><%=Parcialidades %></b></p>
                <%} %>
            </div>
            <div class="col-3">
                <% if (ckAgrupador==false) { %>
                <p>Cr&eacute;dito: <b><%=Credito %></b></p>
                <%} %>
            </div>
            <%--<div class="col-4">
                <div class="form-group">
                    <label>Inventario</label>
                    <input type="text" class="form-control" />
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <label>Asignado</label>
                    <input type="text" class="form-control" />
                </div>
            </div>--%>
            <div class="col-6">
                <button class="btn btns" onclick="cancel(); return false">Cancelar</button>
                <button class="btn btns" onclick="confirm(); return false">Confirmar</button>
            </div>
            <% if (MostrarBotonGuardar) { %>
                <div class="col text-right">
                    <button class="btn btn-success" onclick="guardar(); return false">Guardar</button>
                </div>
            <% } %>
        </div>

        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row mt-3">
                    <div class="col">
                        <table class="table table-bordered table-sm">
                            <thead>
                                <tr>
                                    <th>Pedido</th>
                                    <th>Fecha</th>
                                    <th>Terr.</th>
                                    <th>N&uacute;m. cte.</th>
                                    <th>Cliente</th>
                                    <th>Cr&eacute;dito</th>
                                    <th>Ord.</th>
                                    <th>Disp.</th>
                                    <th>Asig.</th>
                                    <th style="color:red">Cantidad Picking</th>
                                    <th>Faltante</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (Pedidos != null) { %>
                            
                                    <% int i = 0; %>
                                    <% foreach (var item in Pedidos) { %>
                                        <tr>
                                            <td>
                                                <span><%=item.Id_Ped %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Fecha.ToString("dd/MM/yyyy") %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Ter %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Cte %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Cte_NomComercial %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Cte_CreditoStr %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Ord %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Disp %></span>
                                            </td>
                                            <td>
                                                <input type="text"
                                                    id="asig_<%=i %>"
                                                    oninput="asigOnInput(this)"
                                                    onchange="asigOnchange()"
                                                    onfocus="asigOnFocus(this, <%=item.Ped_Asignar %>)"
                                                    onblur="asigOnblur(this, <%=item.Ped_Disp%>, <%=item.Ped_Asignar %>, <%=item.Prd_Disp %>, <%=i %>)"
                                                    class="form-control text-center"
                                                    value="<%=item.Ped_Asignar %>" />
                                            </td>
                                            <td>
                                                <input type="text"
                                                    id="picking_<%=i %>"
                                                    maxlength="9"
                                                    oninput="asigOnInput(this)"
                                                    onblur="pickingOnblur(this, <%=item.Ped_Asignar %>, <%=item.Ped_Picking %>)"
                                                    style="text-align:center; color:red"
                                                    value="<%=item.Ped_Picking %>"
                                                    class="form-control" />
                                            </td>
                                            <td>
                                                <span id="faltante_<%=i %>"><%=item.Ped_Faltante %></span>
                                            </td>
                                        </tr>
                                    <% i++; %>
                                    <% } %>
                               
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <asp:Button runat="server" ID="refreshTable" style="display:none" OnClick="RefreshTable_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
            
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
    <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />

    <script>
        function asigOnInput(elm) {
            elm.value = elm.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1')
        }

        var evaluado = 0;
        var alertaEnviada = false

        function asigOnchange() {
            alertaEnviada = false
        }

        function asigOnFocus(elm, Ped_AsignarOld) {
            evaluado = evaluado - (elm.value - Ped_AsignarOld)
        }

        function asigOnblur(elm, ped_Disp, ped_AsignarOld, prd_Disp, index) {

            var HF = document.getElementById('<%=HF_Guardar.ClientID%>')
            var HFRB = document.getElementById('<%=HiddenRebind.ClientID%>')
            var asignado = elm.value

            if (asignado > ped_Disp) {
                if (!alertaEnviada) {
                    elm.value = ped_AsignarOld
                    asignado = ped_AsignarOld
                    showAlertError('Cantidad asignada es mayor que la ordenada')
                }

                HF.value = 'false'
            }
            else if (asignado - ped_AsignarOld + evaluado > prd_Disp) {
                if (!alertaEnviada) {
                    elm.value = ped_AsignarOld
                    asignado = ped_AsignarOld
                    alertaEnviada = true
                    showAlertError('No se cuenta con el inventario suficiente')
                }

                HF.value = 'false'
            }
            else HF.value = 'true'

            jQuery('#faltante_' + index).text(ped_Disp - asignado)
            evaluado = asignado - ped_AsignarOld + evaluado
            HFRB.value = 'true'
        }

        function pickingOnblur(elm, ped_AsignarOld, ped_PickingOld) {

            var HF = document.getElementById('<%=HF_Guardar.ClientID%>')
            var HFRB = document.getElementById('<%=HiddenRebind.ClientID%>')

            if (elm.value > ped_AsignarOld) {
                elm.value = ped_PickingOld
                showAlertError('Cantidad picking es mayor que la cantidad asignada')
                HF.value = 'false'
            }
            else if (elm.value - ped_PickingOld > ped_AsignarOld) {
                elm.value = ped_PickingOld
                showAlertError('No se cuenta con el asignado suficiente')
                HF.value = 'false'
            }

            HFRB.value = 'true'
        }

        function cancel() {
            request('CancelarPedido', {})
        }

        function confirm() {
            request('ConfirmarPedido', {})
        }

        function guardar() {
            var HF = document.getElementById('<%=HF_Guardar.ClientID%>')
            var HFRB = document.getElementById('<%=HiddenRebind.ClientID%>')

            var count = (<%=Pedidos != null ? Pedidos.Count : 0%>)
            var asign = []
            var picking = []
            for (var n = 0; n < count; n++) {
                asign.push(jQuery('#asig_' + n).val())
                picking.push(jQuery('#picking_' + n).val())
            }

            var data = {
                HF_Guardar: HF.value,
                HiddenRebind: HFRB.value,
                asign: asign,
                picking: picking
            }

            console.log(data)
            request('Guardar', data)
        }

        function request(method, data) {
            showLoader()
            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify(data),
                url: 'ProAsignPedxPrd_PickingV2.aspx/' + method,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    hideLoader()
                    if (data.d.Rebind) {
                        window.parent.reload = true
                        document.getElementById('<%=refreshTable.ClientID%>').click()
                    }

                    if (!data.d.Status) {
                        if (data.d.Message == 'connection close') closeThisWindow()
                        else if (data.d.Message == 'hf_guardar=true') {
                            document.getElementById('<%=HF_Guardar.ClientID%>').value = 'true'
                        }
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
    

