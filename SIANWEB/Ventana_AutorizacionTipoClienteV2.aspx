<%@ Page Title="Autorización Tipo Cliente"
    Language="C#"
    CodeBehind="Ventana_AutorizacionTipoClienteV2.aspx.cs"
    MasterPageFile="~/MasterPage/MasterPageModal.Master"
    Inherits="SIANWEB.Ventana_AutorizacionTipoClienteV2" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager runat="server" />
    <div class="container-fluid">
        <div class="row mt-2">
            <div class="offset-3 col-6">
                <div class="row">
                    <div class="col-12">
                        <div class="form-group">
                            <label>Usuario</label>
                            <input id="user" class="form-control" type="text" />
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="form-group">
                            <label>Contrase&ntilde;a</label>
                            <input id="password" class="form-control" type="password" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <small style="display:none;color:red;"" id="error"></small>
                        <br />
                        <button style="float:right" onclick="logIn(); return false;" class="btn btn-success ml-3 mt-3 mb-2">Aceptar</button>
                        <button onclick="closeThis(); return false;" class="btn btn-danger float-right mt-3 mb-2">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        const logIn = () => {

            var error = jQuery('#error')
            const user = jQuery('#user').val()
            const password = jQuery('#password').val()

            jQuery.ajax({
                type: 'POST',
                data: JSON.stringify({ usuario: user, password: password }),
                url: 'Ventana_AutorizacionTipoClienteV2.aspx/LogIn',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: (data) => {
                    if (data.d.Status == null) closeThisWindow('autorizaciontipocliente')
                    else {
                        error.text(data.d.Status)
                        error.show()
                    }
                },
            })
        }

        const closeThis = () => {
            closeThisWindow('onlyclose')
        }
    </script>
</asp:Content>