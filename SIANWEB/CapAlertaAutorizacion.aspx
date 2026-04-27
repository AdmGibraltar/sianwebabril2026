<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CapAlertaAutorizacion.aspx.cs" Inherits="SIANWEB.CapAlertaAutorizacion" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- Minified JS library -->
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
      <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    
  <style type="text/css">
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .dropdown-toggle-date {
            height: 30px !important;
            margin-top: -6px;
            padding-left: 12px;
            padding-right: 10px;
            margin-right: -13px;
        }

        .panel-success > .panel-heading {
            color: #F9F9F9 !important;
            background-color: #59b2f1 !important;
        }

        .panel-success {
            border-color: #d1d1d1 !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        .row {
            margin-top: 40px;
            padding: 0 10px;
        }

        .clickable {
            cursor: pointer;
        }


        .form-control2 {
            display: block !important;
            width: 100% !important;
            height: 40px !important;
            padding: 0px 0px !important;
            line-height: 1.42857143 !important;
            color: #555 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075) !important;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075) !important;
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
        }

        .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }

        #wizHeader li .prevStep {
            background-color: #26617f;
        }

            #wizHeader li .prevStep:after {
                border-left-color: #26617f !important;
            }

        #wizHeader li .currentStep {
            background-color: #39a5dc;
        }

            #wizHeader li .currentStep:after {
                border-left-color: #39a5dc !important;
            }

        #wizHeader li .nextStep {
            background-color: #C2C2C2;
        }

            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }

          

        #wizHeader {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
            margin: 0px;
            padding: 0px;
        }

            #wizHeader li {
                float: left;
            }

                #wizHeader li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 55px;
                    background: brown; /* fallback color */
                    background: hsla(34,85%,35%,1);
                    position: relative;
                    display: block;
                    float: left;
                }

                    #wizHeader li a:after {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid hsla(34,85%,35%,1);
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        left: 100%;
                        z-index: 2;
                    }

                    #wizHeader li a:before {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent;
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid white;
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        margin-left: 1px;
                        left: 100%;
                        z-index: 1;
                    }

                #wizHeader li:first-child a {
                    padding-left: 10px;
                }

                #wizHeader li:last-child {
                    padding-right: 50px;
                }

                #wizHeader li a:hover {
                    background: #FE9400;
                }

                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }

        .content {
            height: 220px;
        }

        .content2 {
            height: 220px;
            overflow: auto;
        }


        .boxes {
            width: 350px;
        }

        .checkbox, .radio {
            margin-top: 2px !important;
            margin-bottom: 10px !important;
        }

        .list-group {
            height: 150px !important;
        }

        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }

          bootstrapgridviewtextcolumn
            {
                font: 12px Helvetica, Arial, Sans-Serif;
            }

    </style>

    <script type="text/javascript">

        var leadActualCancelar = null;
        var leadActualAutorizar = null;
        var leadMotivoAutorizar = null;
        var leadMotivoCancelar = null;
        //esta parte es para que cuando se cierre la pantalla de autorizar detalle se actualice la pantalla del grid 
        //
        $(document).ready(function () {
            $("#modalAutorizarSol").on('hidden.bs.modal', function () {
                var btn = document.getElementById('<%=btnBuscarInformacion.ClientID%>');
                btn.click();
            });
        });
        /*//jfcv autorización masiva refresca pantalla*/
        function CloseWindowmensaje(mensaje) {
            ShowMessage(mensaje, 'Success');

            debugger;
            var btn = document.getElementById('<%=btnBuscarInformacion.ClientID%>');
            btn.click();

            //GetRadWindow().BrowserWindow.location.reload();
            //GetRadWindow().Close();

        }

        //esta función es llamada desde el botón de autorizar dentro del grid 
        //mando llamar la función que abre el modal de autorizar solicitud donde solicita la justificación.
        function onCustomButtonClick(s, e) {


            if (e.buttonID == 'ShowNewWindow') {
                inicializarModalAutorizarSolicitud(s.GetRowKey(e.visibleIndex))
            }
            if (e.buttonID == 'ShowEditWindow') {
                inicializarModalEditarAutorización(s.GetRowKey(e.visibleIndex))
            }
            if (e.buttonID == 'ShowEditWindowGPMA') {

                var visibleIndex = e.visibleIndex;
                var idCte = s.GetRowValues(visibleIndex, 'Id_Cte', function (value) {
                    // Aquí puedes usar el valor como necesites
                    console.log("Id_Cte:", value);
                    abrirDetalleGPMA(s.GetRowKey(e.visibleIndex), value);
                });

            
                
            }
        }

        function OnGetRowValues(values) {

            grid.GetRowValues(grid.GetFocusedRowIndex(), 'IdAutorizacionPrecio', OnGetRowValues);
            var notes = document.getElementById("Justificacion");
            notes.value = values[1];
            alert(notes);

        }

        //JFCV Autorizar una solicitud masiva Inicio
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

        function onAutorizarClick(s, e) {

            inicializarModalAutorizarSolicitud(1)
        }


        //JFCV Autorizar una solicitud 
        //Muestro la pantalla de autorizar donde solicito la justificación
        //y al aceptar realiza la autorización
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalAutorizarSolicitud(idSolicitud) {

            leadActualAutorizar = idSolicitud;

           <%-- document.getElementById('<%=txtCliente.ClientID%>').innerHTML = '';--%>
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


            $("#HF_MotivoJustificacion").val(motivoautorizar);
            var motivojustificacion = $('#HF_MotivoJustificacion').val();


            $("#HF_MotivoJustificacion").val(motivojustificacion)


            var btn = document.getElementById('<%=Button1.ClientID%>');
            btn.click();

            $('#dvModalAutorizarSolicitud').modal('hide');

        }

        function onRechazarClick(s, e) {
            <%--var idautorizacionprecio = $("#" + '<%=txtIdAutorizacionPrecio.ClientID%>').val();--%>
            inicializarModalRechazarSolicitud(1)
        }

        function inicializarModalRechazarSolicitud(idSolicitud) {

            leadActualAutorizar = idSolicitud;

           <%-- document.getElementById('<%=txtCliente.ClientID%>').innerHTML = '';--%>
            $("#dvModalEditarAutorizarSolicitud").appendTo("body")
            $("#dvModalEditarAutorizarSolicitud").modal({ "backdrop": "static" });


            $('#dvModalEditarAutorizarSolicitud #HF_IdAutSolicitudaut').val(idSolicitud);
            $('#dvModalEditarAutorizarSolicitud').modal('show');
        }

        //JFCV Autoriza la solictud  
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function rechazarsolicitud($) {
            debugger;

            var actualAutorizar = $('#dvModalEditarAutorizarSolicitud #HF_IdAutSolicitudaut2').val();
            var motivoautorizar = $('#dvModalEditarAutorizarSolicitud #txtJustificacion2').val();
            /*var comboautorizar = $('#dvModalEditarAutorizarSolicitud #cmbAutorizacion').val();*/
            

            $("#HF_MotivoJustificacion").val(motivoautorizar);
            var motivojustificacion = $('#HF_MotivoJustificacion').val();

            //motivojustificacion = cb.GetText() + " " + motivojustificacion;

            $("#HF_MotivoJustificacion").val(motivojustificacion)

            /* autorización enviar id motivo agosto 2023 */
            var idmotivorechazo = cb.GetValue();
            $("#HF_IdMotivoRechazo").val(idmotivorechazo);
            motivoautorizar = cb.GetText();
            $("#HF_MotivoRechazo").val(motivoautorizar);


            var btn = document.getElementById('<%=btnRechazartodo.ClientID%>');
            btn.click();

            $('#dvModalEditarAutorizarSolicitud').modal('hide');

        }


           //JFCV Autorizar una solicitud masiva  FIN 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\



        //JFCV Autorizar una solicitud 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalAutorizarSolicitud(idSolicitud) {

            leadActualAutorizar = idSolicitud;

            document.getElementById('<%=txtProducto.ClientID%>').innerHTML = '';
            $("#dvModalAutorizarSolicitud").appendTo("body")
              $("#dvModalAutorizarSolicitud").modal({ "backdrop": "static" });


            $('#dvModalAutorizarSolicitud #HF_IdAutSolicitudaut').val(idSolicitud);
            $('#dvModalAutorizarSolicitud').modal('show');

             
            //var trigger = $(event.relatedTarget);
            //var rowId = trigger.data('rowid');
 
        }
        //JFCV 18 mayo 2022 esta función se llama desde la pantalla de capalertaautorizacioneditar y cierra esa pantalla
        window.closeModalDetalle = function () {
            $('#dvModalAutorizarSolicitud').modal('hide');
            $('#modalAutorizarSol').modal('hide');
            var btn = document.getElementById('<%=btnBuscarInformacion.ClientID%>');
            btn.click();
        }

        
        //JFCV Editar una solicitud de autorización 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalEditarAutorización(idSolicitud) {
           
            leadActualAutorizar = idSolicitud;


            AbrirVentana_InvIns('2021/02/01/', leadActualAutorizar, 100);

          <%--  document.getElementById('<%=txtProducto.ClientID%>').innerHTML = '';
            $("#dvModalEditarAutorizarSolicitud").appendTo("body")
            $("#dvModalEditarAutorizarSolicitud").modal({ "backdrop": "static" });


            $('#dvModalEditarAutorizarSolicitud #HF_IdAutSolicitudaut').val(idSolicitud);
            $('#dvModalEditarAutorizarSolicitud').modal('show');--%>
 

        }

        function AbrirVentana_InvIns(fecha, orden, Acys) {
            var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
            $('#modalAutorizarSol').modal('hide');
            document.getElementById('<%=FrameInventario.ClientID%>').src = "CapAlertaAutorizacionEditar.aspx?fecha=" + fecha + "&orden=" + orden + "&Id_Acs=" + Acys + "&cte=" + IdCte;
            $("#modalAutorizarSol").appendTo("body");
            $("#modalAutorizarSol").modal({ "backdrop": "static" });
            $('#modalAutorizarSol').modal('show');
        }


        
        //JFCV Editar una solicitud de autorización tipo GPMA 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function abrirDetalleGPMA(idSolicitud,idcte) {

            leadActualAutorizar = idSolicitud;
            AbrirVentana_DetalleGPMA('2021/02/01/', leadActualAutorizar, idcte);
        }

        function AbrirVentana_DetalleGPMA(fecha, orden,  idcte) {
            $('#modalAutorizarSol').modal('hide');
            document.getElementById('<%=FrameInventario.ClientID%>').src = "CapAlertaAutorizacionGPMADet.aspx?fecha=" + fecha + "&orden=" + orden + "&Id_Acs=" + 100 + "&cte=" + idcte;
            $("#modalAutorizarSol").appendTo("body");
            $("#modalAutorizarSol").modal({ "backdrop": "static" });
            $('#modalAutorizarSol').modal('show');
        }


        //JFCV Cancelar Solicitud
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarModalCancelarSolicitud(idLead) {
            $('#dvModalCancelarLeads #HF_IdAutSolicitudaut').val(idLead);

            debugger;

            leadActualCancelar = idLead;

            var trigger = $(event.relatedTarget);
            var rowId = trigger.data('rowid');

            $('#dvModalCancelarLeads').modal('show');

        }


        //JFCV LEADS 
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        //function autorizarsolicitud($) {

        //    var actualAutorizar = $('#dvModalAutorizarSolicitud #HF_IdAutSolicitudaut').val();
        //    var motivoautorizar = $('#dvModalAutorizarSolicitud #txtJustificacion').val();
          
        //    AutorizarSolicitud(actualAutorizar, motivoautorizar);

        //    $('#dvModalAutorizarSolicitud').modal('hide');
                      
             
        //}

        <%--function AutorizarSolicitud(folio, leadmotivoautorizar) {


            var dataValue = "{parametro: '" + folio + "', justificacion: '" + leadmotivoautorizar + "'}";
  
            $.ajax({
                type: "POST",
                url: "CapAlertaAutorizacion.aspx/AutorizarFolio",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('error');
                    //                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");

                    //                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    //                    $("#modalMensaje").appendTo("body");
                    //                    $("#modalMensaje").modal({ "backdrop": "static" });
                    //                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    alert('Solicitud de precio autorizada');

                    //                    if (response != null && response.d != null) {
                    //                        var data = response.d;

                    //                        data = $.parseJSON(data);

                    //                        var id = data.id;
                    //                    }


                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                }
            });

            location.reload();

        }--%>


        function onOpenConsultaButtonClick() {



            window.open("CapAlertaConsulta.aspx", "_self");


            //            $.ajax({
            //                type: "POST",
            //             url: "CapAlertaConsulta.aspx",
            //             async: true,
            //             data: "text=1",
            //             success: function (response) {
            //                 alert('ok');

            //                 
            //                },

            //             error: function () {

            //                 alert('error');
            //                }

            //         });
        }


        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0;position: fixed;top: 50%;left: 10%;width: 50%;-ms-transform: translateY(-50%);transform: translate(40%, -50%); -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + ' text-center"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>  <span>' + message + '</span></div>');
        }

        function solonumeros(e) {
            var key;
            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            if (key < 48 || key > 57) {
                return false;
            }
            return true;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

   <div class="panel panel-success" style="margin-top: 15px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Consulta de Solicitudes de precios por autorizar</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body">

                            <%-- <asp:UpdatePanel runat="server" ID="UPdBusacarinfo" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
											<%--//jfcv autorización masiva refresca pantalla--%>
                                                <asp:HiddenField ID="HF_MotivoJustificacion"   ClientIDMode="Static"  runat="server" />
                                                    <%--//jfcv para enviar el motivo rechazo--%>
                                                <asp:HiddenField ID="HF_IdMotivoRechazo"   ClientIDMode="Static"  runat="server" />
                                                <asp:HiddenField ID="HF_MotivoRechazo"   ClientIDMode="Static"  runat="server" />                    
                                                <asp:Label ID="Label24" runat="server" Text="Representante" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CMBSucursalRepresentante" AutoPostBack="true" 
                                                   ClientInstanceName="SucursalRepresentante" runat="server"> </dx:BootstrapComboBox>
<%--
                                                                          <dx:BootstrapComboBox ID="CmbSucursal" runat="server">
                                                    </dx:BootstrapComboBox>--%>
                                                    <%-- OnSelectedIndexChanged="CmbSucursalRepresentante_SelectedIndexChanged" --%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                                <div class="form-group">
                                                <div class="col-md-4">
                                                     <asp:Label ID="Label2" runat="server" Text="Tipo de solicitud" />
                                                </div>
                                                <div class="col-md-8">
                                                                 <dx:BootstrapComboBox ID="CmbTipoSolicitud" runat="server" AutoPostBack="true" >
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <button id="btnBuscarInformacion" type="button" class="btn btn-primary" title="Consultar "
                                                runat="server" onserverclick="btnBuscarInformacion_ServerClick">
                                               <i class="fa fa-search"></i> <span>&nbsp;Consultar</span>
                                            </button>
                                        </div>

                                    </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label15" runat="server" Text="Clave del Producto" />
                                        </div>
                                        <div class="col-md-8">
                                            <dx:bootstraptextbox id="txtProducto" runat="server" onkeypress="javascript:return solonumeros(event)"></dx:bootstraptextbox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label1" runat="server" Text="Clave cliente"   />
                                        </div>
                                        <div class="col-md-8">
                                            <dx:bootstraptextbox id="txtIdCte" runat="server" onkeypress="javascript:return solonumeros(event)"></dx:bootstraptextbox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <button id="btnHistorial" type="button" class="btn btn-success"
                                        title="historial de la consulta" runat="server" onclick="onOpenConsultaButtonClick()">
                                        <i class="fa fa-clock-o"></i>&nbsp;&nbsp;Historial&nbsp;&nbsp;&nbsp;
                                    </button>

                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                </div>

                                <div class="col-md-2">


                                    <button id="btnDescargarInformacion" type="button" class="btn btn-warning"
                                        title="Descargar Detalle" runat="server" onserverclick="btnDescargarInformacion_ServerClick">
                                        <i class="fa fa-download"></i>&nbsp;Descargar
                                    </button>
                                </div>
                                 <div class="col-md-1">
                                             <button id="Button2" type="button" class="btn btn-info"
                                                title="Autorizar los seleccionados" runat="server" onclick="onAutorizarClick()">
                                                <i class="bi bi-check-lg"></i>&nbsp;Autorizar
                                            </button>
                                     </div>
                                <div class="col-md-1">
                                            <button id="Button4" type="button" class="btn btn-default"
                                                title="Rechazar los seleccionados" runat="server" onclick="onRechazarClick()">
                                                <i class="bi bi-check-lg"></i>&nbsp;Rechazar
                                            </button>
                                      </div>
                            </div>

                            <div class="row">

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                        </div>
                                         <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">

                                            <div class="invisible">
                                                <button id="Button1" type="button" class="btn btn-warning"
                                                    runat="server" onserverclick="btnAutorizarTodo">
                                                </button>
                                                <button id="btnRechazartodo" type="button" class="btn btn-warning"
                                                    runat="server" onserverclick="btnRechazarTodo">
                                                </button>

                                            </div>
                                           
                                            
                                        </div>
                                    </div>
                                </div>

                            </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;  font: 12px Helvetica, Arial, Sans-Serif;">
                                <dx:aspxgridviewexporter id="GridViewExporter1" runat="server" gridviewid="gridBuscar"></dx:aspxgridviewexporter>
                                <dx:bootstrapgridview id="gridBuscar" clientinstancename="grid" runat="server" keyfieldname="IdAutorizacionPrecio"
                                    width="100%" autogeneratecolumns="False" enablerowscache="false" OnCustomButtonInitialize="gridBuscar_CustomButtonInitialize"  enablecallbacks="true" oncustomcallback="gridView_CustomCallback">
                                    <columns>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Emp" caption="Id_Emp" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Tipo" caption="Tipo" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="IdAutorizacionPrecio" caption="Id" visible="true" sortorder="Ascending"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="FechaSolicitud" caption="Fecha solicitud"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Cd" caption="Id_Cd" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="IdRepresentante" caption="IdRepresentante" visible="false"></dx:bootstrapgridviewtextcolumn>


                                        <dx:bootstrapgridviewtextcolumn width="200" fieldname="Nom_Representante" caption="Nombre representante" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Cte" caption="Cliente" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="150px" fieldname="Cte_NomComercial" caption="Nombre comercial" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Tipo_Cliente" caption="Tipo Cliente" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Id_Ter" Caption="Territorio" Visible="true"></dx:BootstrapGridViewTextColumn>
                                        <dx:bootstrapgridviewtextcolumn width="30px" fieldname="TipoAutorizacion" caption="Tipo de solicitud" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="150px" fieldname="NomTipoSolicitud" caption="Tipo de solicitud" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Id_Prd" caption="Código del producto" visible="true">
                                             <DataItemTemplate>
                                                 <%# Convert.ToInt64(Eval("Id_Prd")) == 0 ? "" : Eval("Id_Prd").ToString() %>
                                             </DataItemTemplate>
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Prd_Descripcion" caption="Descripcion" visible="true"></dx:bootstrapgridviewtextcolumn>
                                        <%--falta descripcion producto y descripcion del estatus --%>

                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Precio_Vta" caption="P. Venta Ingresado">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="PrecioObjetivo" caption="Precio Objetivo" visible="true">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
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


                                        <dx:bootstrapgridviewtextcolumn width="250px" fieldname="Precio_VtaAutorizado" caption="Precio de Vta Autorizado" visible="false"></dx:bootstrapgridviewtextcolumn>

                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Req_Aut_Director" caption="Requiere aut director" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewcheckcolumn caption="Aut. Director" fieldname="Req_Aut_Director" width="50px" />
                                        <dx:bootstrapgridviewtextcolumn caption="Tamaño" fieldname="Id_Tamaño" width="50px" />
                                        <dx:bootstrapgridviewtextcolumn width="150px" fieldname="Estatus" caption="Estatus" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="NomEstatus" caption="Estatus" visible="true"></dx:bootstrapgridviewtextcolumn>

                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="IdUSolicita" caption="IdUSolicita" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="300px" fieldname="MotivoRechazo" caption="Comentarios" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="150px" fieldname="Nom_CDI" caption="Sucursal" visible="false"></dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="Justificacion" caption="Justificación">
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="200px" fieldname="FechaVigencia" caption="Fecha vigencia" visible="false"></dx:bootstrapgridviewtextcolumn>

                                        <%--JFCV checkbox--%>

                                       <%-- <dx:bootstrapgridviewcheckcolumn unboundtype="Boolean" fieldname="Aut" name="CheckBoxColumn">
                                            <dataitemtemplate>
                                                <dx:bootstrapcheckbox id="checkValue" fieldname="opciones" oncheckedchanged="ASPxCheckBox1_CheckedChanged" runat="server" autopostback="false"></dx:bootstrapcheckbox>
                                            </dataitemtemplate>
                                        </dx:bootstrapgridviewcheckcolumn>--%>

                                       
                                        <dx:bootstrapgridviewcheckcolumn unboundtype="Boolean" fieldname="Aut" name="CheckBoxColumn">
                                            <dataitemtemplate>
                                                <asp:Panel runat="server" Visible='<%# Eval("Tipo") != null && Eval("Tipo").ToString() != "GPMA" %>'>
                                                    <dx:bootstrapcheckbox id="checkValue" fieldname="opciones" runat="server" autopostback="false"></dx:bootstrapcheckbox>
                                                </asp:Panel>
                                            </dataitemtemplate>
                                        </dx:bootstrapgridviewcheckcolumn>

                                        <dx:bootstrapgridviewcommandcolumn>
                                            <custombuttons>
                                                <dx:bootstrapgridviewcommandcolumncustombutton iconcssclass="fa fa-edit" id="ShowEditWindow" />
                                                <dx:bootstrapgridviewcommandcolumncustombutton iconcssclass="fa fa-check-circle" id="ShowEditWindowGPMA" />
                                            </custombuttons>
                                        </dx:bootstrapgridviewcommandcolumn>

                                    </columns>
                                    <clientsideevents custombuttonclick="onCustomButtonClick" />
                                    <settingsdatasecurity allowedit="true" />

                                    <settingsbehavior allowselectbyrowclick="true" />

                                    <settingspager pagesize="10" numericbuttoncount="5">
                                        <summary visible="true" />
                                        <pagesizeitemsettings visible="true" showallitem="true" />
                                    </settingspager>
                                </dx:bootstrapgridview>

                            </div>



                            <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarInformacion" />
                                </Triggers>
                            </asp:UpdatePanel>--%>

                            <div class="col-md-1">
                     
                                <dx:BootstrapComboBox ID="cmbBuscarRepresentante" runat="server" Visible="false">
                                                    </dx:BootstrapComboBox>
                            </div>

                        <%--    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                <ContentTemplate>
                                           
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarInformacion" />
                                </Triggers>
                            </asp:UpdatePanel>
--%>
                          
                       

</div>


       </div>
        <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
        <div id="dvModalEditarAutorizarSolicitud"  class="modal fade bg-dark"   role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">

                 <input type="hidden" id="HF_IdAutSolicitudaut2" name="HF_IdAutSolicitudaut2" />

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H12">Rechazo de Autorización
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
                            <label for="lblJustificacion2">
                                Justificación
                            </label>
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="text" id="txtJustificacion2" name="txtMotivo2" class="form-control" placeholder="Justificación" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="$('#dvModalEditarAutorizarSolicitud').modal('hide');" >
                        Cerrar</button>

                        <button type="button" class="btn btn-primary"
                        id="btnModCancelarlead2" onclick="rechazarsolicitud(jQuery)">
                            Confirmar
                        </button>
 
                </div>
            </div>
                    </div>
        </div>

   


        <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
        <div id="dvModalAutorizarSolicitud"  class="modal fade bg-dark"   role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">

                 <input type="hidden" id="HF_IdAutSolicitudaut" name="HF_IdAutSolicitudaut" />

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H12">Autorizar Solicitudes  de Precios 
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <label for="lblJustificacion">
                                Justificación para todas las solicitudes
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
                    <button type="button" class="btn btn-default" onclick="$('#dvModalAutorizarSolicitud').modal('hide');" >
                        Cerrar</button>

                        <button type="button" class="btn btn-primary"
                            id="btnModCancelarlead" onclick="autorizarsolicitud(jQuery)">
                            Confirmar
                        </button>
 
                </div>
            </div>
                    </div>
                             </div>

      
   <div id="modalMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje2" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                Ok</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

 
    <div class="modal fade" id="modalAutorizarSol" style="height: 1000px !important" class="modal"
        role="dialog" style="z-index: 2220!important">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 style="color: #45709b;" id="h1">Autorizar Solicitudes de Precios Desviados
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 ">
                    <iframe name="frminventario" class="embed-responsive-item" id="FrameInventario" runat="server" src=""></iframe>
                </div>
            </div>
        </div>
    </div>


  
     <div class="messagealert" id="alert_container">
    </div>

</asp:Content>