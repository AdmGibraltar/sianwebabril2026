<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    CodeBehind="VentanaComentariosTerritoriosV2.aspx.cs"
    Inherits="SIANWEB.VentanaComentariosTerritoriosV2" %>


<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h6 class="mt-3">Se debe especificar el motivo del cambio solicitado, este dato es obligatorio.</h6>
                <div class="form-group">
                    <label>Comentario</label>
                    <textarea rows="6" class="form-control" id="comentario"></textarea>
                </div>
                <div id="error" class="alert alert-danger d-none" role="alert">
                    Este campo es necesario para procesar la solicitud
                </div>
                <button class="btn btn-success mt-3 float-right" onclick="guardar(); return false">Enviar comentarios</button>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(() => { jQuery('#comentario').val('') })

        const guardar = () => {

            var error = jQuery('#error')
            var comentario = jQuery('#comentario')

            if (!comentario.val()) {
                error.removeClass('d-none')
                error.addClass('d-block')
                comentario[0].rows = "4"
                return
            }

             jQuery.ajax({
                 type: 'POST',
                 data: JSON.stringify({ comentario: comentario.val() }),
                 url: 'VentanaComentariosTerritoriosV2.aspx/GuardarComentario',
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: (data) => {
                     closeThisWindow()
                 }
             })
         }
    </script>
</asp:Content>