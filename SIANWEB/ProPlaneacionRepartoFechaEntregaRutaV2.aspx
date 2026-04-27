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
                <p>Fecha de Entrega: <b><%=Fecha %></b></p>
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
                    <label>Fecha de Entrega</label>
                    <asp:TextBox runat="server" class="form-control" TextMode="Date" id="fechaentrega" />
                </div>
            </div>
            <div class="col-6 align-self-center text-right mt-3">
                <button class="btn btn-success" onclick="ActualizaFechaEntrega(); return false">Guardar Fecha Entrega</button>
            </div>
        </div>
    </div>

    <script>
        function ActualizaFechaEntrega() {

            var fec = document.getElementById('<%=fechaentrega.ClientID%>').value;


                //jQuery('#fechaentrega').text();

            showLoader()



            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify({
                    fechae: fec
                }),
                url: 'ProPlaneacionRepartoFechaEntregaRutaV2.aspx/ActualizaFechaEntrega',
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
