<%@ Page Language="C#"
    AutoEventWireup="true"
     MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="ProPlaneacionRepartoFechaEntregaRutaV2.aspx.cs"
    Inherits="SIANWEB.ProPlaneacionRepartoFechaEntregaRutaV2" %>

<asp:Content ContentPlaceHolderID="content" runat="server">

    <div class="container-fluid">
        <div class="row mt-4">
            <div class="col-4">
                <p>Pedido: <b><%=HF_Ped %></b></p>
            </div>
            <div class="col-4">
                <p>Fecha: <b><%=Fecha %></b></p>
            </div>
            <div class="col-4">
                <p>Credito: <b><%=Credito %></b></p>
            </div>
            <div class="col-12">
                <p>Cliente: <%=Id_Cte %> - <%=Nom_Cte %></p>
                <hr />
            </div>
            <div class="col-6">
                <p>Calle: <b><%=Peddido.Ped_ConsignadoCalle %></b></p>
            </div>
            <div class="col-3">
                <p>N&uacute;mero: <b><%=Peddido.Ped_ConsignadoNo %></b></p>
            </div>
            <div class="col-3">
                <p>CP: <b><%=Peddido.Ped_ConsignadoCp %></b></p>
            </div>
            <div class="col-6">
                <p>Colonia: <b><%=Peddido.Ped_ConsignadoColonia %></b></p>
            </div>
            <div class="col-6">
                <p>Municipio: <b><%=Peddido.Ped_ConsignadoMunicipio %></b></p>
            </div>
            <div class="col-6">
                <p>Estado: <b><%=Peddido.Ped_ConsignadoEstado %></b></p>
            </div>
            <div class="col-6">
                <p>Tel&eacute;fonos: <b><%=Peddido.acs_telefono2 %></b></p>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col-6">
                <div class="form-group">
                    <label>Ruta</label>
                    <select class="form-control" id="rutas">
                        <% if (Rutas != null && Rutas.Count > 0) { %>
                            <% foreach (var ruta in Rutas) { %>

                                <% if (Peddido.Id_Rut == ruta.Id) { %>
                                    <option selected value="<%=ruta.Id %>">
                                        <%=ruta.Descripcion %>
                                    </option>
                                <% }
                                    else
                                    { %>
                                        <option value="<%=ruta.Id %>">
                                            <%=ruta.Descripcion %>
                                        </option>
                                <% } %>

                            <% } %>
                        <% } %>
                    </select>
                </div>
            </div>
            <div class="col-6 align-self-center text-right mt-3">
                <button class="btn btn-success" onclick="agregarDirEntrega(); return false">Guardar Ruta</button>
            </div>
        </div>
    </div>

    <script>
        function agregarDirEntrega() {
            showLoader()
            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify({
                    ruta: jQuery('#rutas').val()
                }),
                url: 'ProPlaneacionRepartoFechaEntregaRutaV2.aspx/AgregarDirEntrega',
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
