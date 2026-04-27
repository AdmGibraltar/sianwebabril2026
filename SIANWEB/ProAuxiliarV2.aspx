<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/MasterPage/MasterPageNewDesign.Master"
    AutoEventWireup="true"
    CodeBehind="ProAuxiliarV2.aspx.cs"
    Inherits="SIANWEB.ProAuxiliarV2" %>

<asp:Content ContentPlaceHolderID="CPH" runat="server">
    <link rel="stylesheet" href="ProAuxiliarResources/Main.css" />
    <style>
        .custom-select {
            position: relative;
            width: 100%;
        }

        .autocomplete-input {
            width: 100%;
            border: 0px;
        }

        .autocomplete-list {
            position: absolute;
            top: 100%;
            left: 0;
            width: 100%;
            background-color: #fff;
            border: 1px solid #ccc;
            list-style-type: none;
            padding: 0;
            margin: 0;
            display: none;
            z-index: 100;
            height: 300px;
            overflow-y: auto;
        }

            .autocomplete-list li {
                padding: 5px;
                cursor: pointer;
            }

                .autocomplete-list li:hover {
                    background-color: #f0f0f0;
                }
    </style>

    <div id="spinner">
        <img src="Imagenes/ajax-loader.gif" />
    </div>

    <div class="container">
        <div class="row">
            <div class="col-md-4 offset-2">
                <div class="form-group">
                    <label>Fecha entrega inicial</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="TxtFechaEntregaInicial" />
                </div>
                <div class="form-group">
                    <label>Ruta</label>
                    <select class="form-control" id="rutas">
                        <% if (Rutas != null && Rutas.Count > 0)
                            { %>
                        <% foreach (var ruta in Rutas)
                            { %>

                        <option value="<%=ruta.Descripcion %>">
                            <%= ruta.Descripcion %>
                        </option>

                        <% } %>
                        <% } %>
                    </select>
                    <asp:HiddenField runat="server" ID="cmbRuta" />
                </div>
                <div class="form-group">
                    <label>Cliente</label>
                    <div class="custom-select">
                        <input type="text" class="autocomplete-input" placeholder="Escribe aquí...">
                        <asp:HiddenField ID="id_cliente2" runat="server" Value="0" />

                        <ul class="autocomplete-list">
                            <li id="0">--Seleccionar--
                            </li>
                            <% if (Clientes != null && Clientes.Count > 0)
                                { %>
                            <% foreach (var cliente in Clientes)
                                { %>

                            <li id="<%=cliente.Id %>">
                                <%= cliente.Descripcion %>
                            </li>

                            <% } %>
                            <% } %>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Fecha entrega final</label>
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="TxtFechaEntregaFinal" />
                </div>
                <div class="form-group">
                    <label>Auxiliar almac&eacute;n</label>
                    <select class="form-control" id="auxiliares">
                        <% if (Auxiliar != null && Auxiliar.Count > 0)
                            { %>
                        <% foreach (var ruta in Auxiliar)
                            { %>

                        <option value="<%=ruta.Id %>">
                            <%= ruta.Descripcion %>
                        </option>

                        <% } %>
                        <% } %>
                    </select>
                    <asp:HiddenField runat="server" ID="cmbAuxiliar" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 offset-2">
                <button onclick="search(); return false" class="btn w-100 btn-search">Buscar</button>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid mt-4">
                <div class="row">
                    <div class="col">
                        <nav>
                            <div class="nav nav-tabs" id="myTab" role="tablist">
                                <a class="nav-item nav-link active" id="por-producto-tab" data-toggle="tab" href="#nav-por-producto" role="tab" aria-controls="nav-por-producto" aria-selected="true">Por Producto</a>
                                <a class="nav-item nav-link" id="por-pedido-tab" data-toggle="tab" href="#nav-por-pedido" role="tab" aria-controls="nav-por-pedido" aria-selected="false">Por Pedido</a>
                                <a class="nav-item nav-link" id="por-sin-identificar" data-toggle="tab" href="#nav-sin-identificar" role="tab" aria-controls="nav-sin-identificar" aria-selected="false">Sin Identificar</a>
                            </div>
                        </nav>
                        <div class="tab-content mt-2">
                            <div class="tab-pane fade show active" id="nav-por-producto" role="tabpanel" aria-labelledby="por-producto-tab">
                                <asp:CheckBox ID="chAgruparProducto2" runat="server" AutoPostBack="true" OnCheckedChanged="chAgruparProducto_CheckedChanged" Text="Agrupar" />
                                <table class="table tablesize table-hover table-bordered table-sm">
                                    <thead>
                                        <tr>
                                            <th>Operar</th>
                                            <th>(+)Pick</th>
                                            <th>(-)Pick</th>
                                            <th>Incomp.</th>
                                            <th id="thTipo" runat="server">Tipo Pedido</th>
                                            <th id="thCredito" runat="server">Crédito</th>
                                            <th id="thParcialidades" runat="server">Parcialidades</th>
                                            <th>Ruta</th>
                                            <th>Producto</th>
                                            <th>Presentación</th>
                                            <th>Cant. Pedido</th>
                                            <th>Cant. Pendiente</th>
                                            <th>Cant. Asignada</th>
                                            <th style="color: red">Cant. Picking</th>
                                            <th>Cantidades</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% if (AsignacionPorProducto != null)
                                            { %>

                                        <% string color = ""; %>
                                        <% foreach (var item in AsignacionPorProducto)
                                            { %>

                                        <% color = ""; %>
                                        <% if (item.Ped_Asignado == item.Ped_Picking) { color = "#BDEcb6"; } %>
                                        <% else if (item.Ped_Picking > 0 && (item.Ped_Picking < item.Ped_Asignado)) { color = "#fdfd96"; } %>

                                        <tr style="background-color: <%=color %>">
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_ProAsignPedxPrd(
                                                            <%=item.Id_Prd%>,
                                                            '<%=item.Ruta%>',
                                                            '<%=item.CreditoStr%>',
                                                            '<%=item.Ped_PermiteParcialidades%>',
                                                            '<%=item.TipoPedido %>'); return false">
                                                    <i style="color: blue" class="bi bi-pencil-square"></i>
                                                </button>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="guardar(
                                                            <%=item.Id_Prd %>,
                                                            '<%=item.Ruta %>',
                                                            '<%=item.CreditoStr%>',
                                                            '<%=item.Ped_PermiteParcialidades%>',
                                                            '<%=item.TipoPedido %>',
                                                            '<%=item.Agrupado%>'
                                                            ); return false">
                                                    <i style="color: green" class="bi bi-check2-circle"></i>
                                                </button>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="cancelar(
                                                            <%=item.Id_Prd %>,
                                                            '<%=item.Ruta %>',
                                                            '<%=item.CreditoStr%>',
                                                            '<%=item.Ped_PermiteParcialidades%>',
                                                            '<%=item.TipoPedido %>',
                                                            '<%=item.Agrupado%>'
                                                            ); return false">
                                                    <i style="color: red" class="bi bi-x-lg"></i>
                                                </button>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_Icompleto(
                                                            <%=item.Id_Prd%>,
                                                            <%=item.Ped_Asignado %>,
                                                            <%=item.Prd_Disponible %>); return false">
                                                    <i style="color: green" class="bi bi-check2-circle"></i>
                                                </button>
                                            </td>
                                            <% if (item.Agrupado == false)
                                                { %>
                                            <td>
                                                <span><%=item.TipoPedido %></span>
                                            </td>
                                            <td>
                                                <span><%=item.CreditoStr %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_PermiteParcialidades %></span>
                                            </td>
                                            <% } %>
                                            <td>
                                                <span><%=item.Ruta %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Prd %> - <%=item.Prd_Descripcion %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Prd_Presentacion %>&nbsp;<%=item.Prd_UniNe %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Cantidad %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Pendiente %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Asignado %></span>
                                            </td>
                                            <td style="color: red">
                                                <span><%=item.Ped_Picking %></span>
                                            </td>
                                            <td>
                                                <a href="#" onclick="VerMasCantidadesPorProducto(
                                                            <%=item.Id_Prd %>,
                                                            '<%=item.Prd_Descripcion.Replace("\"", "") %>',
                                                            <%=item.Ped_Cantidad %>,
                                                            <%=item.Ped_Facturado %>,
                                                            <%=item.Ped_Remisionado %>,
                                                            <%=item.Ped_Pendiente %>,
                                                            <%=item.Ped_Asignado %>,
                                                            <%=item.Prd_Disponible %>,
                                                            <%=item.Ped_Picking %>,
                                                            <%=item.Ped_CantidadDisponible %>
                                                            )">Ver todo</a>
                                            </td>
                                        </tr>
                                        <% } %>

                                        <% } %>
                                    </tbody>
                                    <% if (AsignacionPorProducto != null && AsignacionPorProducto.Count == 0)
                                        { %>
                                    <tfoot>
                                        <tr>
                                            <td colspan="15">
                                                <p class="notrows">No se encontraron registros</p>
                                            </td>
                                        </tr>
                                    </tfoot>
                                    <% } %>
                                </table>
                            </div>
                            <div class="tab-pane fade" id="nav-por-pedido" role="tabpanel" aria-labelledby="por-pedido-tab">
                                <button type="button" id="btnCambiarRutaMasiva" class="btn btn-primary mt-2" onclick="abrirCambioRutaMasivo()" style="margin-top: -50px !important; float: right">
                                    Cambiar Ruta Masiva
                                </button>
                                <table class="table tablesize table-hover table-bordered table-sm">
                                    <thead>
                                        <tr>
                                            <th>
                                                <input type="checkbox" id="checkTodos" />
                                            </th>
                                            <th>Ruta</th>
                                            <th>Tipo Pedido</th>
                                            <th>Pedido</th>
                                            <th>Fecha Pedido</th>
                                            <th>Fecha Entrega</th>
                                            <th style="background-color: orange">Num.</th>
                                            <th>Cliente</th>
                                            <th>Cr&eacute;dito</th>
                                            <th>Parcialidades</th>
                                            <th>Cant. Pedido</th>
                                            <th>Cant. Pendiente</th>
                                            <th>Cant. Asignada</th>
                                            <th>Cant. Picking</th>
                                            <th>Cantidades</th>
                                            <%--<th>Cambiar Ruta</th>--%>
                                            <th>Definir Ruta</th>
                                            <th>Operar</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% if (AsignacionPorPedido != null)
                                            { %>

                                        <% foreach (var item in AsignacionPorPedido)
                                            { %>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="pedido-checkbox"
                                                    data-id="<%=item.Id_Ped %>"
                                                    data-idcte="<%=item.Id_Cte %>"
                                                    data-nomcte="<%=item.Cte_NomComercial %>"
                                                    data-ruta="<%=item.Ruta %>" />
                                            </td>
                                            <td>
                                                <span><%=item.Ruta %></span>
                                            </td>
                                            <td>
                                                <span><%=item.TipoPedido %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Ped %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Fecha.ToString("dd/MM/yyyy") %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %></span>
                                            </td>
                                            <td style="background-color: #f2f2f2">
                                                <span><%=item.Id_Cte %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Cte_NomComercial %></span>
                                            </td>
                                            <td>
                                                <span><%=item.CreditoStr %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_PermiteParcialidades %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Cantidad %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Pendiente %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Asignado %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Picking %></span>
                                            </td>
                                            <td>
                                                <a href="#" onclick="VerMasCantidadesPorPedido(
                                                            <%=item.Id_Cte %>,
                                                            '<%=item.Cte_NomComercial.Replace("\"", "") %>',
                                                            <%=item.Ped_Cantidad %>,
                                                            <%=item.Ped_Facturado %>,
                                                            <%=item.Ped_Remisionado %>,
                                                            <%=item.Ped_Pendiente %>,
                                                            <%=item.Ped_Asignado %>,
                                                            <%=item.Ped_Picking %>,
                                                            <%=item.Ped_CantidadDisponible %>
                                                            )">Ver Todo</a>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_PlaneacionReparto_DireccionEntrega(
                                                    <%=item.Id_Ped %>,
                                                    <%=item.Id_Cte %>,
                                                    '<%=item.Cte_NomComercial %>',
                                                    '<%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %>',
                                                    0,
                                                    '<%=item.CreditoStr %>',
                                                    '<%=item.Ruta %>'
                                                    ); return false">
                                                    <i style="color: blue" class="bi bi-pencil-square"></i>
                                                </button>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_PlaneacionReparto(
                                                            <%=item.Id_Ped %>,
                                                            <%=item.Id_Cte %>,
                                                            '<%=item.Cte_NomComercial %>',
                                                            '<%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %>',
                                                            0,
                                                            '<%=item.CreditoStr %>',
                                                            '<%=item.Ruta %>'
                                                            ); return false">
                                                    <i style="color: blue" class="bi bi-pencil-square"></i>
                                                </button>
                                            </td>
                                        </tr>
                                        <% } %>

                                        <% } %>
                                    </tbody>
                                    <% if (AsignacionPorPedido != null && AsignacionPorPedido.Count == 0)
                                        { %>
                                    <tfoot>
                                        <tr>
                                            <td colspan="16">
                                                <p class="notrows">No se encontraron registros</p>
                                            </td>
                                        </tr>
                                    </tfoot>
                                    <% } %>
                                </table>
                            </div>

                            <div class="tab-pane fade" id="nav-sin-identificar" role="tabpanel" aria-labelledby="por-sin-identificar">
                                <table class="table tablesize table-hover table-bordered table-sm">
                                    <thead>
                                        <tr>
                                            <th>Ruta</th>
                                            <th>Tipo Pedido</th>
                                            <th>Pedido</th>
                                            <th>Fecha Pedido</th>
                                            <th>Fecha Entrega</th>
                                            <th style="background-color: orange">Num.</th>
                                            <th>Cliente</th>
                                            <th>Cr&eacute;dito</th>
                                            <th>Parcialidades</th>
                                            <th>Cant. Pedido</th>
                                            <th>Cant. Pendiente</th>
                                            <th>Cant. Asignada</th>
                                            <th>Cant. Picking</th>
                                            <th>Cantidades</th>
                                            <th>Definir Ruta</th>
                                            <th>Cambiar Fecha Entrega</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% if (PedidosSinIdentificar != null)
                                            { %>

                                        <% foreach (var item in PedidosSinIdentificar)
                                            { %>
                                        <tr>
                                            <td>
                                                <span><%=item.Ruta %></span>
                                            </td>
                                            <td>
                                                <span><%=item.TipoPedido %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Id_Ped %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Fecha.ToString("dd/MM/yyyy") %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %></span>
                                            </td>
                                            <td style="background-color: #f2f2f2">
                                                <span><%=item.Id_Cte %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Cte_NomComercial %></span>
                                            </td>
                                            <td>
                                                <span><%=item.CreditoStr %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_PermiteParcialidades %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Cantidad %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Pendiente %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Asignado %></span>
                                            </td>
                                            <td>
                                                <span><%=item.Ped_Picking %></span>
                                            </td>
                                            <td>
                                                <a href="#" onclick="VerMasCantidadesPorPedido(
                                                            <%=item.Id_Cte %>,
                                                            '<%=item.Cte_NomComercial.Replace("\"", "") %>',
                                                            <%=item.Ped_Cantidad %>,
                                                            <%=item.Ped_Facturado %>,
                                                            <%=item.Ped_Remisionado %>,
                                                            <%=item.Ped_Pendiente %>,
                                                            <%=item.Ped_Asignado %>,
                                                            <%=item.Ped_Picking %>,
                                                            <%=item.Ped_CantidadDisponible %>
                                                            )">Ver Todo</a>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_PlaneacionReparto_DireccionEntrega(
                                                            <%=item.Id_Ped %>,
                                                            <%=item.Id_Cte %>,
                                                            '<%=item.Cte_NomComercial %>',
                                                            '<%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %>',
                                                            0,
                                                            '<%=item.CreditoStr %>',
                                                            '<%=item.Ruta %>'
                                                            ); return false">
                                                    <i style="color: blue" class="bi bi-pencil-square"></i>
                                                </button>
                                            </td>
                                            <td>
                                                <button class="btn" onclick="AbrirVentana_PlaneacionReparto_FechaEntrega(
                                                            <%=item.Id_Ped %>,
                                                            <%=item.Id_Cte %>,
                                                            '<%=item.Cte_NomComercial %>',
                                                            '<%=item.Ped_FechaEntrega.ToString("dd/MM/yyyy") %>',
                                                            0,
                                                            '<%=item.CreditoStr %>',
                                                            '<%=item.Ruta %>'
                                                            ); return false">
                                                    <i style="color: blue" class="bi bi-pencil-square"></i>
                                                </button>
                                            </td>
                                        </tr>
                                        <% } %>

                                        <% } %>
                                    </tbody>
                                    <% if (PedidosSinIdentificar != null && PedidosSinIdentificar.Count == 0)
                                        { %>
                                    <tfoot>
                                        <tr>
                                            <td colspan="16">
                                                <p class="notrows">No se encontraron registros</p>
                                            </td>
                                        </tr>
                                    </tfoot>
                                    <% } %>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <asp:Button runat="server" Style="display: none" ID="refresh" OnClick="Refresh_Click" />
            <asp:Button runat="server" ID="imbBuscar" Style="display: none" OnClick="ImbBuscar_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false" id="iframeModal">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Asignaci&oacute;n Picking</h5>
                    <button type="button" class="close" onclick="clearIframe()" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body p-0">
                    <div class="container">
                        <div class="row">
                            <div class="col">
                                <div class="holds-the-iframe">
                                    <iframe style="border: 0;" width="100%" id="iframeContent"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false" id="cantidadesPorProducto">
        <div class="modal-dialog modal-lg modal-dialog-centered" style="max-width: 90% !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 id="cantidadesPorProductoTitle" class="modal-title"></h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body pb-0 pt-0">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Cant. Pedido</th>
                                <th>Cant. Facturado</th>
                                <th>Cant. Remisionado</th>
                                <th>Cant. Pendiente</th>
                                <th>Cant. Asignada</th>
                                <th>Cant. Disponible</th>
                                <th style="color: red">Cant. Picking</th>
                                <th>Cant. Sugerida</th>
                            </tr>
                        </thead>
                        <tbody id="tcantidadesPorProducto"></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false" id="cantidadesPorPedido">
        <div class="modal-dialog modal-lg modal-dialog-centered" style="max-width: 90% !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 id="cantidadesPorPedidoTitle" class="modal-title"></h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body pb-0 pt-0">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Cant. Pedido</th>
                                <th>Cant. Facturado</th>
                                <th>Cant. Remisionado</th>
                                <th>Cant. Pendiente</th>
                                <th>Cant. Asignada</th>
                                <th>Cant. Picking</th>
                                <th>Cant. Sugerida</th>
                            </tr>
                        </thead>
                        <tbody id="tcantidadesPorPedido"></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="ProAuxiliarResources/Main.js"></script>
    <%--//BRB--------------------------%>
    <script>
        //BRB------------------------
        function actualizarDatosDesdeIframe() {
            __doPostBack('ActualizarDatos', '');
        }
        function inicializarEventosPedidos() {
            const btnCambiarRuta = document.getElementById('btnCambiarRutaMasiva');
            const checkTodos = document.getElementById('checkTodos');
            const checkboxes = document.querySelectorAll('.pedido-checkbox');

            function verificarCheckboxSeleccionado() {
                const algunoSeleccionado = Array.from(checkboxes).some(cb => cb.checked);
                btnCambiarRuta.disabled = !algunoSeleccionado;

                if (checkTodos) {
                    const todosMarcados = Array.from(checkboxes).every(cb => cb.checked);
                    checkTodos.checked = todosMarcados;
                }
            }

            btnCambiarRuta.disabled = true;

            checkboxes.forEach(cb => {
                cb.removeEventListener('change', verificarCheckboxSeleccionado); // prevenir duplicados
                cb.addEventListener('change', verificarCheckboxSeleccionado);
            });

            if (checkTodos) {
                checkTodos.removeEventListener('change', seleccionarTodos);
                checkTodos.addEventListener('change', seleccionarTodos);
            }

            function seleccionarTodos() {
                const marcarTodos = checkTodos.checked;
                checkboxes.forEach(cb => {
                    cb.checked = marcarTodos;
                    cb.dispatchEvent(new Event('change'));
                });
            }
        }

        Sys.Application.add_load(inicializarEventosPedidos);



        function abrirCambioRutaMasivo() {
            const checkboxes = document.querySelectorAll('.pedido-checkbox:checked');
            if (checkboxes.length === 0) {
                alert('Selecciona al menos un pedido.');
                return;
            }

            const pedidos = [];

            checkboxes.forEach(chk => {
                pedidos.push({
                    Id_Ped: parseInt(chk.dataset.id),
                    Id_Cte: parseInt(chk.dataset.idcte),
                    Cte_NomComercial: chk.dataset.nomcte,
                    Ruta: chk.dataset.ruta
                });
            });

            AbrirVentana_PlaneacionReparto_DireccionEntrega_Masivo(pedidos);
        }
        function setIframeMasivo(src, height, parameters) {
            window.reload = false
            var iframe = document.getElementById('iframeContent')
            iframe.style.height = height
            iframe.src = src + (parameters || '');

            jQuery('#iframeModal').modal('show')
        }
        function AbrirVentana_PlaneacionReparto_DireccionEntrega_Masivo(pedidos) {
            sessionStorage.setItem("PedidosSeleccionados", JSON.stringify(pedidos));

            fetch('ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo.aspx/GuardarPedidosEnSession', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ pedidos: pedidos })
            })
                .then(response => response.json())
                .then(data => {
                    console.log("Respuesta del servidor:", data);

                    setIframeMasivo('ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo.aspx', '400px', '');
                })
                .catch(error => {
                    console.error("Error en fetch:", error);
                });
        }

        //BRB------------------------
    </script>
    <%--//BRB--------------------------%>
    <script type="text/javascript">

        function AbrirVentana_ProAsignPedxPrd(id_prd, ruta, credito, parcialidades, tipoPedido) {
            var checkboxEstado = document.getElementById('<%= chAgruparProducto2.ClientID %>').checked;
            var id_cliente = document.getElementById('<%= id_cliente2.ClientID %>').value;
            var parameters = '?Id_Prd=' + id_prd
            parameters += '&PermisoGuardar=<%=PermisoGuardar%>'
            parameters += '&PermisoModificar=<%=PermisoModificar%>'
            parameters += '&PermisoEliminar=<%=PermisoEliminar%>'
            parameters += '&PermisoImprimir=<%=PermisoImprimir%>'
            parameters += '&Ruta=' + ruta
            parameters += '&Credito=' + credito
            parameters += '&Parcialidades=' + parcialidades
            parameters += '&TipoPedido=' + tipoPedido
            parameters += '&ckAgrupador=' + checkboxEstado;
            parameters += '&id_cliente=' + id_cliente;

            setIframe('ProAsignPedxPrd_PickingV2.aspx', '450px', parameters)
        }

        function AbrirVentana_Icompleto(id_prd, cantidadAsignada, cantidadDisponible) {
            var parameters = '?Id_Prd=' + id_prd
            parameters += '&PermisoGuardar=<%=PermisoGuardar%>'
            parameters += '&PermisoModificar=<%=PermisoModificar%>'
            parameters += '&PermisoEliminar=<%=PermisoEliminar%>'
            parameters += '&PermisoImprimir=<%=PermisoImprimir%>'
            parameters += '&CantidadAsignada=' + cantidadAsignada
            parameters += '&CantidadDisponible=' + cantidadDisponible

            setIframe('ProIncompleto_PickingV2.aspx', '260px', parameters)
        }

        function AbrirVentana_PlaneacionReparto_DireccionEntrega(id, id_cte, nom_cte, fecha, territorio, credito, ruta) {
            var parameters = '?Id=' + id
            parameters += '&Id_Cte=' + id_cte
            parameters += '&Nom_Cte=' + nom_cte
            parameters += '&Fecha=' + fecha
            parameters += '&Territorio=' + territorio
            parameters += '&Credito=' + credito
            parameters += '&PermisoGuardar=<%=PermisoGuardar%>'
            parameters += '&PermisoModificar=<%=PermisoModificar%>'
            parameters += '&PermisoEliminar=<%=PermisoEliminar%>'
            parameters += '&PermisoImprimir=<%=PermisoImprimir%>'
            parameters += '&Ruta=' + ruta

            setIframe('ProPlaneacionRepartoDireccionEntregaRutaV2.aspx', '400px', parameters)
        }



        function AbrirVentana_PlaneacionReparto_FechaEntrega(id, id_cte, nom_cte, fecha, territorio, credito, ruta) {
            var parameters = '?Id=' + id
            parameters += '&Id_Cte=' + id_cte
            parameters += '&Nom_Cte=' + nom_cte
            parameters += '&Fecha=' + fecha
            parameters += '&Territorio=' + territorio
            parameters += '&Credito=' + credito
            parameters += '&PermisoGuardar=<%=PermisoGuardar%>'
            parameters += '&PermisoModificar=<%=PermisoModificar%>'
            parameters += '&PermisoEliminar=<%=PermisoEliminar%>'
            parameters += '&PermisoImprimir=<%=PermisoImprimir%>'
            parameters += '&Ruta=' + ruta

            setIframe('ProPlaneacionRepartoFechaEntregaRutaV2.aspx', '400px', parameters)
        }
        function AbrirVentana_PlaneacionReparto(id, id_cte, nom_cte, fecha, territorio, credito, ruta) {
            var parameters = '?Id=' + id
            parameters += '&Id_Cte=' + id_cte
            parameters += '&Nom_Cte=' + nom_cte
            parameters += '&Fecha=' + fecha
            parameters += '&Territorio=' + territorio
            parameters += '&Credito=' + credito
            parameters += '&PermisoGuardar=<%=PermisoGuardar%>'
            parameters += '&PermisoModificar=<%=PermisoModificar%>'
            parameters += '&PermisoEliminar=<%=PermisoEliminar%>'
            parameters += '&PermisoImprimir=<%=PermisoImprimir%>'
            parameters += '&Ruta=' + ruta

            setIframe('ProPlaneacionRepartoV2.aspx', '400px', parameters)
        }

        function search() {
            showLoader()
            confirmModal = false

            document.getElementById('<%=cmbRuta.ClientID%>').value = jQuery('#rutas').val()
            document.getElementById('<%=cmbAuxiliar.ClientID%>').value = jQuery('#auxiliares').val()
            document.getElementById('<%=imbBuscar.ClientID%>').click()
        }

        function closeModalWindow() {
            jQuery('#iframeModal').modal('hide')
            clearIframe()
        }

        function refreshTable() {
            document.getElementById('<%=refresh.ClientID%>').click()
        }

        document.addEventListener('DOMContentLoaded', function () {
            var input = document.querySelector('.autocomplete-input');
            var id_cliente = document.getElementById('<%= id_cliente2.ClientID %>');
            var list = document.querySelector('.autocomplete-list');
            var listItems = list.querySelectorAll('li');

            var objetos = [];

            listItems.forEach(function (li) {
                var texto = li.textContent.trim();

                var id = li.getAttribute('id');

                var objeto = { texto: texto, id: id };
                objetos.push(objeto);
            });


            var options = objetos;

            var customSelect = document.querySelector('.custom-select');

            input.addEventListener('input', function () {


                var inputValue = normalize(input.value.toLowerCase());
                if (inputValue.trim() == '') {
                    id_cliente.value = 0;
                }
                console.log('---2', inputValue);
                var filteredOptions = options.filter(function (option) {
                    return normalize(option.texto.toLowerCase()).match(inputValue);
                });
                populateList(filteredOptions);
            });

            input.addEventListener('click', function () {
                var inputValue = normalize(input.value.toLowerCase());
                if (inputValue.trim() == '--seleccionar--') {
                    inputValue = '';
                }
                console.log('---1', inputValue);
                var filteredOptions = options.filter(function (option) {
                    return normalize(option.texto.toLowerCase()).match(inputValue);
                });
                populateList(filteredOptions);
            });

            document.addEventListener('click', function (event) {
                var target = event.target;
                if (!customSelect.contains(target) && !list.contains(target)) {
                    list.style.display = 'none';
                }
            });

            function populateList(options) {
                list.innerHTML = '';

                options.slice(0, 10000000).forEach(function (option) {
                    var li = document.createElement('li');
                    li.textContent = option.texto;
                    li.addEventListener('click', function () {
                        input.value = option.texto;

                        id_cliente.value = option.id;

                        list.style.display = 'none';
                    });
                    list.appendChild(li);
                });
                list.style.display = 'block';
            }

            // Función para normalizar texto (elimina acentos y convierte a minúsculas)
            function normalize(text) {
                return text.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
            }
        });

    </script>
</asp:Content>
