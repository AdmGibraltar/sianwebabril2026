<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="AutorizacionVinculacionV2.aspx.cs"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    Inherits="SIANWEB.AutorizacionVinculacionV2" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div id="step1" class="col text-center">
                <h6 class="mt-3">¿Este cliente pertenece a una cuenta corporativa?</h6>
                <button onclick="no(); return false;" class="btn btn-danger mt-3 mr-5">No</button>
                <button onclick="si(); return false;" class="btn btn-success mt-3">Si</button>
            </div>
            <div id="step2" class="col text-center" style="display:none">
                <h6>Solicitar vinculaci&oacute;n a matriz</h6>
                <p class="mb-0">RFC: <b><%=RFC %></b></p>

                 <button onclick="solicitar(); return false;" class="btn btn-success mt-3 mb-2">Solicitar</button>
                 <br />
                <small>En breve podr&aacute; acceder al m&oacute;dulo de vinculaciones para concluir el proceso.</small>
            </div>
        </div>
    </div>
    <script>
        const no = () => {
            guardarValor(1)
        }

        const si = () => {
            jQuery('#step1').hide()
            jQuery('#step2').show()
        }

        const solicitar = () => {
            guardarValor(2)
        }

        const guardarValor = (v) => {
            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify({ valor: v }),
                url: 'AutorizacionVinculacionV2.aspx/GuardarValor',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: (data) => {
                    closeThisWindow('vinculacioncliente')
                }
            })
        }
    </script>
</asp:Content>