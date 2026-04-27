<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master"     AutoEventWireup="true" CodeBehind="CapAlertaAutorizacionEditar.aspx.cs" Inherits="SIANWEB.CapAlertaAutorizacionEditar" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <style type="text/css">
        .modal-body {
            max-height: calc(100% - 120px);
            overflow-y: scroll;
             padding-right: 2px;
            padding-left: 2px;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }
        .col.md{
            font-size : 8pt;
           
        }
        .col-md{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
        .col-md-1{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
        .col-md-2{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
        .col-md-3{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
         .col-md-4{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
          
      .col-md-8{
            font-size : 8pt;
            padding-right: 2px;
            padding-left: 2px;
        }
      
       
    </style>

    <script type="text/javascript">


        //esta función es llamada desde el botón de autorizar dentro del grid 
        //mando llamar la función que abre el modal de autorizar solicitud donde solicita la justificación.
        function onCustomButtonClick(s, e) {

            debugger;
            if (e.buttonID == 'Autorizar') {
                inicializarModalAutorizarSolicitud(s.GetRowKey(e.visibleIndex))
            };
            if (e.buttonID == 'Cancelar') {
                inicializarModalCancelarSolicitud(s.GetRowKey(e.visibleIndex))
            };

        }

        function onRechazarClick(s, e) {
            debugger
            var idautorizacionprecio = $("#" + '<%=txtIdAutorizacionPrecio.ClientID%>').val();
            inicializarModalCancelarSolicitud(idautorizacionprecio)
        }

        function onAutorizarClick(s, e) {
            var idautorizacionprecio = $("#" + '<%=txtIdAutorizacionPrecio.ClientID%>').val();
            inicializarModalAutorizarSolicitud(idautorizacionprecio)
        }



        function OnGetRowValues(values) {

            alert('buscar datos');
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'IdAutorizacionPrecio', OnGetRowValues);
            alert('obteniendo justificacion');
            var notes = document.getElementById("Justificacion");
            notes.value = values[1];
            alert(notes);

        }

        //JFCV Autorizar una solicitud 
        //Muestro la pantalla de autorizar donde solicito la justificación
        //y al aceptar realiza la autorización
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalAutorizarSolicitud(idSolicitud) {

            leadActualAutorizar = idSolicitud;

            document.getElementById('<%=txtCliente.ClientID%>').innerHTML = '';
            $("#dvModalAutorizarSolicitud").appendTo("body")
            $("#dvModalAutorizarSolicitud").modal({ "backdrop": "static" });


            $('#dvModalAutorizarSolicitud #HF_IdAutSolicitudaut').val(idSolicitud);
            $('#dvModalAutorizarSolicitud').modal('show');
        }

        //JFCV Autoriza la solictud 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function autorizarsolicitud($) {

            debugger;
            var actualAutorizar = $('#dvModalAutorizarSolicitud #HF_IdAutSolicitudaut').val();
            var motivoautorizar = $('#dvModalAutorizarSolicitud #txtJustificacion').val();
            var precio_vta = document.getElementById("CPH_txtPrecio_Vta_I").value;
            var fechavigencia = document.getElementById("CPH_DateFechaVigencia_I").value;
            var id_cd = $("#" + '<%=txtId_Cd.ClientID%>').val();
            var reqaut = document.getElementById("CPH_chkReq_Aut_Director_RB0_I").value;

            if (reqaut = 'on') {
                reqaut = '1';
            }
            else {
                reqaut = '0';
            }

            var usuario = $("#" + '<%=txtUsuario.ClientID%>').val();

            AutorizarSolicitud(actualAutorizar, motivoautorizar, usuario, precio_vta, fechavigencia, id_cd, reqaut);

            $('#dvModalAutorizarSolicitud').modal('hide');


        }
        function AutorizarSolicitud(folio, leadmotivoautorizar, usuario, precio_vta, fechavigencia, id_cd, reqaut) {


            var dataValue = "{parametro: '" + folio + "', justificacion: '" + leadmotivoautorizar + "', usuario: '" + usuario + "', Precio_Vta: '" + precio_vta + "', fechavigencia: '" + fechavigencia + "', id_cd: '" + id_cd + "', reqaut: '" + reqaut + "'}";

            $.ajax({
                type: "POST",
                url: "CapAlertaAutorizacionEditar.aspx/AutorizarFolio",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('error');
                },
                success: function (response) {
                    alert('Solicitud de precio autorizada');
                    CloseWindow();
                },
                complete: function (response) {

                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                }
            });

            location.reload();

        }
        //JFCV Cancelar Solicitud
        //Muestro la pantalla de cancelar solicitud donde solicito el motivo de la cancelación
        //y al aceptar realiza la autorización
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalCancelarSolicitud(idSolicitud) {
            debugger;
            leadActualCancelar = idSolicitud;



            document.getElementById('<%=txtCliente.ClientID%>').innerHTML = '';
            $("#dvModalCancelarSolicitud").appendTo("body")
            $("#dvModalCancelarSolicitud").modal({ "backdrop": "static" });


            $('#dvModalCancelarSolicitud #HF_IdAutSolicitudcan').val(idSolicitud);

            var trigger = $(event.relatedTarget);
            var rowId = trigger.data('rowid');

            $('#dvModalCancelarSolicitud').modal('show');

        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cancelarsolicitud($) {

            var actualCancelar = $('#dvModalCancelarSolicitud #HF_IdAutSolicitudcan').val();
            var motivorechazo = $('#dvModalCancelarSolicitud #txtMotivoRechazo').val();
            var usuariocan = $("#" + '<%=txtUsuario.ClientID%>').val();
            var id_cd = $("#" + '<%=txtId_Cd.ClientID%>').val();

            var idmotivorechazo = cb.GetValue();
            // var selectedIndex = cb.GetSelectedIndex();
            
            //motivorechazo = cb.GetText() + " " + motivorechazo;
            var justificarechazo = motivorechazo;
            motivorechazo = cb.GetText();

            CancelarSolicitud(actualCancelar, motivorechazo, idmotivorechazo, justificarechazo, usuariocan, id_cd);

            //CancelarSolicitud(actualCancelar, motivorechazo, usuariocan, id_cd);

            $('#dvModalCancelarSolicitud').modal('hide');

        }


        function CancelarSolicitud(folio, motivorechazo, idmotivorechazo, justificarechazo, usuario, id_cd) {

            var dataValue = "{parametro: '" + folio + "', motivorechazo: '" + motivorechazo + "', idmotivorechazo: '" + idmotivorechazo + "', justificarechazo: '" + justificarechazo + "', usuario: '" + usuario + "', idcd: '" + id_cd + "'}";

            $.ajax({
                type: "POST",
                url: "CapAlertaAutorizacionEditar.aspx/CancelarFolio",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('error');
                },
                success: function (response) {
                    alert('Solicitud de precio rechazada');
                    CloseWindow();

                },
                complete: function (response) {

                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                }
            });

            location.reload();

        }

        function closeModalDetalle() {
            window.parent.closeModalDetalle();
        };

        window.closeModalInvIns = function () {
            $('#modalInventario').modal('hide');
        };

        function CloseWindow() {
            window.parent.closeModalDetalle();
        };

        //abre la pantalla de mensaje cuando ocurre algún error 
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensajealerta.ClientID%>').innerHTML = mensaje;
            $("#modalAcysMensaje").appendTo("body")
            $("#modalAcysMensaje").modal({ "backdrop": "static" });
            $('#modalAcysMensaje').modal('show');
        }

        //JFCV TODO esta función revisarla si es necesaria 
        $(document).ready(function () {
            $('#btnUploadCancel').click(function () {
                $('#modalAcysMensaje').modal('toggle');
                $("#modalAcysMensaje").appendTo("body");
                $("#modalAcysMensaje").modal({ "backdrop": "static" });
            });
        })


    </script>
    <div class="modal-body" id="Div2" style="font-size: 8pt;">

        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>




        <div id="divPrincipal" style="font-family: verdana; font-size: 8pt" runat="server">
 
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <div class="col-md-1">
                            <b></b>
                            </div>
                        <div class="col-md-3">
                            
                               Fecha de la Solicitud
                             </div>
                        <div class="col-md-4">
                            <dx:bootstrapdateedit id="txtFecha" runat="server" style="text-align: right" readonly="true">
                            </dx:bootstrapdateedit>
                        </div>
                        

                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <div class="col-md-3">
                            Núm. de Solicitud 
                     
                        </div>
                        <div class="col-md-3">
                            <input type="text" id="txtSolicitudAutorizacion" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>

                        <div class="col-md-3">
                             Tipo de solicitud
                        </div>
                        <div class="col-md-3">
                            <dx:bootstraptextbox id="txtTipoAutorizacion" runat="server" maxlength="9" readonly="true" visible="false" enabled="true">
                            </dx:bootstraptextbox>
                            <dx:bootstraptextbox id="txtNomTipoSolicitud" runat="server" readonly="true" enabled="true">
                            </dx:bootstraptextbox>
                        </div>


                    </div>
                </div>
                 <div class="col-md-4">
                       
                       <div class="col-md-3">
                                    Estatus:
                                </div>
                                <div class="col-md-3">
                                    <dx:bootstraptextbox id="txtNomEstatus" runat="server" readonly="true"
                                        enabled="true">
                                    </dx:bootstraptextbox>
                                </div>


                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <b></b>
                </div>
            </div>
          
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-1">
                            Cliente
                        </div>
                        <div class="col-md-2">
                            Nombre
                        </div>
                        <div class="col-md-1">
                            Territorio
                        </div>
                        <div class="col-md-1">
                            Ventas Trimestre Ant.
                        </div>
                        <div class="col-md-1">
                            Utilidad Prima
                        </div>
                        <div class="col-md-1">
                            % Utilidad Prima
                        </div>
                        <div class="col-md-1">
                            UAFIR Mensual
                        </div>
                        <div class="col-md-1">
                            UAFIR Anual
                        </div>
                        <div class="col-md-1">
                            % UAFIR Cliente
                        </div>
                        <div class="col-md-1">
                            Ut. Remanente
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-1">
                            <input type="text" id="txtCliente" class="form-control" runat="server" onpaste="return false"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-2">
                            <input type="text" id="txtClienteNombre" class="form-control" runat="server" onpaste="return false"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">

                            <input type="text" id="txtTerritorio" class="form-control" runat="server" onpaste="return false"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtVentas" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtUtilidadPrima" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtPorcUtilidadPrima" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtUafirmes" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtUafirAnual" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtPorcUafirCte" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                        <div class="col-md-1">
                            <input type="text" id="txtUtilRemanente" class="form-control" runat="server" onpaste="return false" style="text-align: right"
                                readonly="readonly" />
                        </div>
                    </div>
                </div>
            </div>

            <b></b>


 
            
            <div class="row">

                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading titulo_blod">
                            Datos de la Solicitud 
                        </div>
                        <div class="panel-body">

                        <div class="col-md-8">

                                <div class="col-md-2">
                                    Código del producto:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox id="txtId_Prd" runat="server" readonly="true" enabled="true">
                                    </dx:bootstraptextbox>

                                </div>
                                <div class="col-md-1">
                                    Descripción:
                                </div>
                                <div class="col-md-6">
                                    <dx:bootstraptextbox id="txtDescripcion" runat="server" readonly="true" enabled="true">
                                    </dx:bootstraptextbox>
                                </div>

                            </div>


                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Nombre Representante:
                                </div>
                                <div class="col-md-3">
                                    <dx:bootstraptextbox id="txtNom_Representante" runat="server" maxlength="9" readonly="true" enabled="true">
                                    </dx:bootstraptextbox>
                                </div>
                                <div class="col-md-1">
                                        Desc. S/Plista
                                 </div>
                                <div class="col-md-1">
                                     
                                 </div>
                                 <div class="col-md-1">
                                     Mg%
                                 </div>
                               <div class="col-md-4">
                                 </div>


                            </div>
          
                            <div class="col-md-8">

                                <div class="col-md-2">
                                    P. Venta Ingresado:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox id="txtPrecio_Vta" runat="server" maxlength="15" readonly="false"
                                        enabled="true" autopostback="true"    onvaluechanged="txtPrecio_Vta_ValueChanged">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>
                                 <div class="col-md-2" align="right">
                                     <dx:bootstraptextbox id="txtPrecio_Vta_Desc" runat="server" maxlength="10" width="50%" readonly="true"
                                        enabled="true"  />
 
                                 </div>
                                 <div class="col-md-2" align="right">
                                      <dx:bootstraptextbox  id="txtPrecio_Vta_Porc" runat="server" maxlength="10" width="50%" readonly="true"
                                        enabled="true"  />
                                 </div>
                               <div class="col-md-4">
                                 </div>


                            </div>
                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Precio Lista:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox id="txtPrecioLista" runat="server" maxlength="15" readonly="true"
                                        enabled="true">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>
                                <div class="col-md-2" align="right">
                                     <dx:bootstraptextbox id="txtPrecioLista_Desc" runat="server"   width="50%" readonly="true"
                                        enabled="true"  />
 
                                 </div>
                                 <div class="col-md-2" align="right">
                                      <dx:bootstraptextbox id="txtPrecioLista_Porc" runat="server" width="50%"  readonly="true"
                                        enabled="true"  />
                                 </div>
                               <div class="col-md-4">
                                 </div>

                            </div>

                            <div class="col-md-8">
                                <div class="col-md-2">
                                    P. Venta Min Rik:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox  id="txtPrecio_MinimoRik" runat="server" maxlength="15" readonly="true"
                                        enabled="true">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>
                                 <div class="col-md-2" align="right">
                                     <dx:bootstraptextbox  id="txtPrecio_MinimoRik_Desc" runat="server"  width="50%"   readonly="true"
                                        enabled="true"  />
 
                                 </div>
                                 <div class="col-md-2" align="right">
                                      <dx:bootstraptextbox  id="txtPrecio_MinimoRik_Porc" runat="server" width="50%" readonly="true"
                                        enabled="true"  />
                                 </div>
                               <div class="col-md-4">
                                 </div>

                            </div>
                            <%--Precio Objetivo JFCV 28nov--%>
                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Precio Objetivo:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox  id="txtPrecioObjetivo" runat="server" maxlength="15" readonly="true"
                                        enabled="true">
                                    </dx:bootstraptextbox>
                                </div>
                                
                                 <div class="col-md-2" align="right">
                                     <dx:bootstraptextbox id="txtPrecioObjetivo_Desc" runat="server" width="50%" readonly="true"
                                        enabled="true"  />
 
                                 </div>
                                 <div class="col-md-2" align="right">
                                      <dx:bootstraptextbox  id="txtPrecioObjetivo_Porc" runat="server" width="50%" readonly="true"
                                        enabled="true"  />
                                 </div>










                            </div>

                            <div class="col-md-8">
                                <div class="col-md-2">
                                    P. Venta Min Gerente:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox   id="txtPrecio_MinimoGte" runat="server" maxlength="15" readonly="true"
                                        enabled="true" style="text-align:right">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>
                                <div class="col-md-2" align="right">
                                     <dx:bootstraptextbox  id="txtPrecio_MinimoGte_Desc" runat="server" width="50%" readonly="true"
                                        enabled="true" style="text-align:right" />
 
                                 </div>
                                 <div class="col-md-2" align="right">
                                      <dx:bootstraptextbox id="txtPrecio_MinimoGte_Porc" runat="server" width="50%" readonly="true"
                                        enabled="true" style="text-align:right" />
                                 </div>


                            </div>
                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Utilidad Pvta - Precio AAA:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraptextbox   id="txtUtilidad" runat="server" maxlength="15" readonly="true"
                                        enabled="true">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>
                                <div class="col-md-2" align="right">
                                    % Ut:
                                </div>
                                <div class="col-md-2" align="right">
                                    <dx:bootstraptextbox  id="txtPorc_Utilidad" runat="server" width="50%"  readonly="true"
                                        enabled="true">
                                        <validationsettings validationgroup="Validation">
                                            <requiredfield isrequired="true" errortext="Campo requerido" />
                                        </validationsettings>
                                    </dx:bootstraptextbox>
                                </div>

                            </div>


                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Fecha Vigencia:
                                </div>
                                <div class="col-md-2">
                                    <dx:bootstraplayoutitem caption="Fecha Vigencia" colspanlg="4" colspanmd="6">
                                        <contentcollection>
                                            <dx:contentcontrol>
                                                <dx:bootstrapdateedit runat="server" id="DateFechaVigencia" datechanged="DateFechaVigencia_DateChanged">
                                                    <cssclasses icondropdownbutton="fa fa-calendar" />
                                                </dx:bootstrapdateedit>
                                            </dx:contentcontrol>
                                        </contentcollection>
                                    </dx:bootstraplayoutitem>
                                </div>
                                 <%--Precio Objetivo JFCV 28nov--%>
                                <div class="col-md-2" align="right">
                                    Tamaño:
                                </div>
                                <div class="col-md-2" align="right">
                                    <dx:bootstraptextbox  id="txtId_Tamaño" runat="server" maxlength="1"  width="50%"  readonly="true">
                                    </dx:bootstraptextbox>
                                </div>
                            </div>


                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Motivo:
                                </div>
                                <div class="col-md-8">
                                    <dx:bootstraptextbox id="txtMotivo" runat="server" maxlength="4" readonly="true"
                                        enabled="true">
                                    </dx:bootstraptextbox>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Comentarios
                                </div>
                                <div class="col-md-8">
                                    <dx:bootstraptextbox id="txtJustificaBreve" runat="server" maxlength="400" readonly="true"
                                        enabled="true">
                                    </dx:bootstraptextbox>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <div class="col-md-2">
                                    Requiere aut director:
                                </div>
                                <div class="col-md-1">

                                    <dx:bootstrapcheckboxlist id="chkReq_Aut_Director" runat="server" readonly="true">
                                        <items>
                                            <dx:bootstraplistedititem text="Req. Aut Dir." value="0" />

                                        </items>
                                    </dx:bootstrapcheckboxlist>

                                </div>
                                <div class="col-md-2">
                                    <asp:Label id="lblConvenio" runat="server"></asp:Label>
                                </div>
                            </div>

                              <div class="col-md-12">

                    <dx:bootstrapbutton runat="server" text="Autorizar" autopostback="false" id="Autorizar">
                        <clientsideevents click="onAutorizarClick" />
                        <settingsbootstrap renderoption="Success" />
                    </dx:bootstrapbutton>
                    <dx:bootstrapbutton runat="server" text="Rechazar" autopostback="false" id="Rechazar">
                        <clientsideevents click="onRechazarClick" />
                        <settingsbootstrap renderoption="Danger" />
                    </dx:bootstrapbutton>
                    <dx:bootstrapbutton runat="server" text="Regresar" autopostback="false" id="btnRegresar">
                        <clientsideevents click="closeModalDetalle" />
                        <settingsbootstrap renderoption="warning" />
                    </dx:bootstrapbutton>
                    <%--  <button id="" type="button" class="btn btn-warning btn-sm w35" style="margin-top: 8px!important;"
                                        title="Regresar" runat="server" onclick="closeModalDetalle()">
                                        <i class="fa fa-arrow-left"></i> <span>Regresar</span></button>--%>
                </div>

 
                        </div>
                    </div>
                </div>


            </div>
            <div class="row">
          

                <div class="col-md-12">
                    <div class="col-md-4">
                        <dx:bootstraptextbox class='text-right' id="txtPrecio_VtaAutorizado" runat="server" maxlength="400" readonly="true"
                            enabled="false" visible="false">
                        </dx:bootstraptextbox>
                    </div>
                    <div class="col-md-1">
                        <dx:bootstraptextbox class='text-right'  id="txtEstatus" runat="server" maxlength="400" readonly="true"
                            enabled="false" visible="false">
                        </dx:bootstraptextbox>
                    </div>
                    <div class="col-md-1">
                        <dx:bootstraptextbox id="txtIdUSolicita" runat="server" maxlength="400" readonly="true"
                            enabled="false" visible="false">
                        </dx:bootstraptextbox>
                    </div>
                    <div class="col-md-1">
                        <dx:bootstraptextbox id="txtNom_CDI" runat="server" maxlength="400" readonly="true"
                            enabled="false" visible="false">
                        </dx:bootstraptextbox>
                    </div>

                </div>


            </div>
            &nbsp; 
              <div class="col-md-12" style="margin-top: 5px;">
                  <dx:bootstrapgridview id="gridAutorizacion" clientinstancename="grid" runat="server" keyfieldname="IdAutorizacionPrecio"
                      width="100%" autogeneratecolumns="False" visible="False">
                      <settingsediting mode="Inline">
                      </settingsediting>
                      <settingsdatasecurity allowedit="true" allowdelete="false" />
                      <settingscommandbutton>
                          <editbutton iconcssclass="fa fa-edit" text=" " />
                          <deletebutton iconcssclass="fa fa-remove" text=" " />
                          <cancelbutton iconcssclass="fa fa-ban" text=" " />
                          <updatebutton iconcssclass="fa fa-check" text=" " />
                          <newbutton iconcssclass="fa fa-plus" text=" " />
                      </settingscommandbutton>
                      <columns>
                          <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Emp" caption="Id_Emp" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="50px" fieldname="IdAutorizacionPrecio" caption="Id" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Cd" caption="Id_Cd" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="50px" fieldname="IdRepresentante" caption="IdRepresentante" visible="false" sortorder="Descending"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="300px" fieldname="Nom_Representante" caption="Nombre representante" visible="true"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="50px" fieldname="TipoAutorizacion" caption="Tipo de solicitud" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="150px" fieldname="NomTipoSolicitud" caption="Tipo de solicitud" visible="true"></dx:bootstrapgridviewtextcolumn>

                          <dx:bootstrapgridviewtextcolumn width="100px" fieldname="Id_Prd" caption="Código del producto" visible="true"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="300px" fieldname="Prd_Descripcion" caption="Descripción" visible="true"></dx:bootstrapgridviewtextcolumn>
                          <%--<dx:BootstrapGridViewTextColumn Width="100px" FieldName="Cantidad" Caption="Cantidad" Visible="true"></dx:BootstrapGridViewTextColumn>--%>
                          <%--<dx:BootstrapGridViewTextColumn Width="200px" FieldName="Precio_Vta" Caption="P. Venta Ingresado"><PropertiesTextEdit DisplayFormatString="c" /></dx:BootstrapGridViewTextColumn>--%>
                          <dx:bootstrapgridviewspineditcolumn cssclasses-headercell="centerText" fieldname="Precio_Vta" caption="P. Venta Ingresado">
                              <propertiesspinedit displayformatstring="c">
                                  <validationsettings requiredfield-isrequired="true">
                                  </validationsettings>
                              </propertiesspinedit>
                          </dx:bootstrapgridviewspineditcolumn>
                          <dx:bootstrapgridviewtextcolumn width="200px" fieldname="PrecioLista" caption="Precio Lista">
                              <propertiestextedit displayformatstring="c" />
                          </dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="100px" fieldname="Precio_MinimoRik" caption="P. Venta Min Rik" visible="true">
                              <propertiestextedit displayformatstring="c" />
                          </dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="100px" fieldname="Precio_MinimoGte" caption="P. Venta Min Gerente" visible="true">
                              <propertiestextedit displayformatstring="c" />
                          </dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Utilidad" caption="Utilidad Pvta - Precio AAA" visible="true">
                              <propertiestextedit displayformatstring="c" />
                          </dx:bootstrapgridviewtextcolumn>

                          <dx:bootstrapgridviewtextcolumn width="100px" fieldname="Porc_Utilidad" caption="% Ut" visible="true">
                              <propertiestextedit displayformatstring="p0" />
                          </dx:bootstrapgridviewtextcolumn>
                          <%-- <dx:BootstrapGridViewTextColumn Width="300px" FieldName="Importe" Caption="Importe Venta" Visible="true"></dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="300px" FieldName="Importe_Utilidad" Caption="Total Utilidad" Visible="true"></dx:BootstrapGridViewTextColumn>--%>
                          <dx:bootstrapgridviewtextcolumn width="300px" fieldname="MotivoCancelacion" caption="Motivo" visible="true"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="300px" fieldname="Justificacion" caption="Comentarios" visible="true"></dx:bootstrapgridviewtextcolumn>



                          <dx:bootstrapgridviewtextcolumn width="250px" fieldname="Precio_VtaAutorizado" caption="Precio de Vta Autorizado" visible="false">
                              <propertiestextedit displayformatstring="c" />
                          </dx:bootstrapgridviewtextcolumn>
                          <%--<dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaSolicitud" Caption="Fecha solicitud"></dx:BootstrapGridViewTextColumn>--%>
                          <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Req_Aut_Director" caption="Requiere aut director" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewcheckcolumn caption="Aut. Director" fieldname="Req_Aut_Director" width="50px" />

                          <dx:bootstrapgridviewtextcolumn width="150px" fieldname="Estatus" caption="Estatus" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="200px" fieldname="NomEstatus" caption="Estatus" visible="true"></dx:bootstrapgridviewtextcolumn>

                          <dx:bootstrapgridviewtextcolumn width="200px" fieldname="IdUSolicita" caption="IdUSolicita" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="300px" fieldname="MotivoRechazo" caption="Comentarios" visible="false"></dx:bootstrapgridviewtextcolumn>
                          <dx:bootstrapgridviewtextcolumn width="150px" fieldname="Nom_CDI" caption="Sucursal" visible="false"></dx:bootstrapgridviewtextcolumn>


                          <dx:bootstrapgridviewcommandcolumn>
                              <custombuttons>
                                  <dx:bootstrapgridviewcommandcolumncustombutton iconcssclass="fa fa-check" id="ShowNewWindow" />
                                  <dx:bootstrapgridviewcommandcolumncustombutton iconcssclass="fa fa-times" id="Cancelar" />
                              </custombuttons>
                          </dx:bootstrapgridviewcommandcolumn>


                      </columns>
                      <clientsideevents custombuttonclick="onCustomButtonClick" />
                      <settingsdatasecurity allowedit="true" />

                  </dx:bootstrapgridview>


              </div>

                       <div class="row">
                <div class="col-md-11">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>

                </div>
                <div class="col-md-1">
                    <input type="text" id="txtUsuario" width="1" class="form-control" runat="server" onpaste="return false"
                        readonly="readonly" style="display: none" />
                    <input type="text" id="txtIdAutorizacionPrecio" width="1" class="form-control" runat="server" onpaste="return false"
                        readonly="readonly" style="display: none" />
                    <input type="text" id="txtId_Cpr" width="1" class="form-control" runat="server" onpaste="return false"
                        readonly="readonly" style="display: none" />
                    <input type="text" id="txtId_Cd" width="1" class="form-control" runat="server" onpaste="return false"
                        readonly="readonly" style="display: none" />

                </div>

            </div>


            <div id="divResumen" runat="server">
            </div>

        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalAutorizarSolicitud" class="modal fade bg-dark" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="padding: 10px;">

                    <input type="hidden" id="HF_IdAutSolicitudaut" name="HF_IdAutSolicitudaut" />

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H12">Autorizar Solicitud de Precio
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <label for="lblJustificacion">
                                Justificación
                            </label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="text" id="txtJustificacion" name="txtMotivo" class="form-control" placeholder="Justificación" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="$('#dvModalAutorizarSolicitud').modal('hide');">
                        Cerrar</button>

                    <button type="button" class="btn btn-primary"
                        id="btnModCancelarlead" onclick="autorizarsolicitud(jQuery)">
                        Confirmar
                    </button>

                </div>
            </div>
        </div>
    </div>



    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalCancelarSolicitud" class="modal fade bg-dark" role="dialog" aria-labelledby="myModalcancela">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header"  style="padding: 10px;">

                    <input type="hidden" id="HF_IdAutSolicitudcan" name="HF_IdAutSolicitudcan" />

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H12">Cancelar Solicitud de Precio
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label for="lblMotivo">
                                Motivo
                            </label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                               <dx:BootstrapComboBox id="cmbAutorizacion" ClientInstanceName="cb" runat="server" SelectedIndex="0">
                                    <Items>
                                        <dx:BootstrapListEditItem Text="Vinculate a la matriz de CN" Value="1" />
                                        <dx:BootstrapListEditItem Text="Utilidad Baja para la categoría del cliente" Value="2" />
                                        <dx:BootstrapListEditItem Text="Amplie la justificación de la solicitud" Value="3" />
                                        <dx:BootstrapListEditItem Text="Vinculate a Convenio de Precios Esp." Value="5" />
                                        <dx:BootstrapListEditItem Text="Otro" Value="4" /> 
                                    </Items>
                                </dx:BootstrapComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="lblMotivoRechazo">
                                Motivo de Rechazo
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="text" id="txtMotivoRechazo" name="txtMotivoRechazo" class="form-control" placeholder="Motivo de Rechazo" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="$('#dvModalCancelarSolicitud').modal('hide');">
                        Cerrar</button>

                    <button type="button" class="btn btn-primary"
                        id="btnModCancelarSol" onclick="cancelarsolicitud(jQuery)">
                        Confirmar
                    </button>

                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="modalAcysMensaje" class="modal" data-toggle="modal" role="dialog" tabindex="-1"
        style="z-index: 5220!important;" style="display: none;">
        <div class="modal-dialog" role="document" style="height: 120px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" style="padding: 25px !important;" id="Div11">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensajealerta" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
                            <table>
                                <tr>
                                    <td>
                                        <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                            Ok</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
