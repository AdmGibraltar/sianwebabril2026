<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.master" AutoEventWireup="true" CodeBehind="Pro_CN_Vinculacion_Lista.aspx.cs" Inherits="SIANWEB.Pro_CN_Vinculacion_Lista" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"> 
   
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>


     <style type="text/css">
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }

         .modal-body {
            padding: 0px !important;
        }

        .panel-body {
            padding: 5px !important;
        }

        .dxbs-footer-row {
            background-color: lightcyan;
            font-weight: bold;
        }


        .list-group {
            height: 212px !Important;
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


        .dxbs-spin-btn-group .increment, .dxbs-spin-btn-group .increment-l {
            display: inline !important;
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
            height: 180px;
        }


        .boxes {
            height: 100px;
            overflow: auto;
            width: 300px;
        }

        .boxes2 {
            height: 200px;
            overflow: auto;
            width: 300px;
        }

        .checkbox, .radio {
            margin-top: 2px !important;
            margin-bottom: 10px !important;
        }

        .content2 {
            height: 310px;
            overflow: auto;
        }

        .dx-custom-style {
            border: dotted 3px;
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

        .centerText {
            text-align: center;
        }

        .bg-primary {
            background-color: red !important;
        }

        .BlackTextClass {
            color: black;
        }

        .RedBackgroundClass {
            background-color: red;
        }

        .GreenBackgroundClass {
            background-color: forestgreen;
        }

        .YellowBackgroundClass {
            background-color: yellow;
        }

        .WhiteTextClass {
            color: white;
        }

        .BlackTextClass {
            color: black;
        }

        .dx-overlay-wrapper {
            z-index: 5700 !important;
        }

        .dxbs-date-edit .dropdown-menu.panel {
            margin-top: 20px;
            position:fixed !important;
        }

        .dropdown-menu > li > a 
        {
            white-space: pre-wrap !important;
        }
    </style>


      <script type="text/javascript"> 
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

          function AbrirPantallaACYS(Id, IdMatriz) { 
              document.getElementById('<%=iFrameAcys.ClientID%>').src = "Cat_CN_ClienteMatriz_ACYS.aspx?Id=" + Id + "&IdMatriz=" + IdMatriz;
              $("#modalAcys").appendTo("body");
              $("#modalAcys").modal({ "backdrop": "static" });
              $('#modalAcys').modal('show');
          }

          function AbrirPantallaVinculacion(Id, IdMatriz, Nombre) { 
              document.getElementById('<%=iFrameVinculaciones.ClientID%>').src = "Pro_CN_Vinculacion.aspx?Id=" + Id + "&IdMatriz=" + IdMatriz + "&Nombre=" + Nombre;
              $("#modalVinculaciones").appendTo("body");
              $("#modalVinculaciones").modal({ "backdrop": "static" });
              $('#modalVinculaciones').modal('show');
          }
          function AbrirPantallaDesVinculacion(Id, IdMatriz) {
             document.getElementById('<%=iFrameVinculaciones.ClientID%>').src = "Pro_CN_Vinculacion.aspx?Id=" + Id + "&IdMatriz=" + IdMatriz + "&DesVinc=1";
              $("#modalVinculaciones").appendTo("body");
              $("#modalVinculaciones").modal({ "backdrop": "static" });
              $('#modalVinculaciones').modal('show');
          }
          function AbrirPantallaSolicitudes() { 
              document.getElementById('<%=iFrameSolicitud.ClientID%>').src = "Pro_CN_Solicitudes.aspx";
              $("#modalSolicitud").appendTo("body");
              $("#modalSolicitud").modal({ "backdrop": "static" });
              $('#modalSolicitud').modal('show');
          }
          function confirmar() {
              return confirm('¿Está seguro que desea Cancelar la Solicitud?');
          }

          $(document).ready(function () {
              $('#modalSolicitud').on('hidden.bs.modal', function () {
                  location.reload();
              })


              $('#modalVinculaciones').on('hidden.bs.modal', function () {
                  location.reload();
              }) 
          });

      </script>

    <div class="container-fluid">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="UP1">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 5px;">

                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Vinculación Cuentas Nacionales</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            Matriz
                                        </div>
                                        <div class="col-md-10">
                                            <dx:BootstrapComboBox ID="cmbMatriz" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="cmbMatriz_SelectedIndexChanged1" AutoPostBack="true">
                                            </dx:BootstrapComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <a href="#" onclick=" AbrirPantallaSolicitudes()">Mis Solicitudes</a>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">

                                <dx:BootstrapGridView ID="dgClienteMatriz" runat="server" AutoGenerateColumns="false" KeyFieldName="id">
                                    <Columns>
                                          <dx:BootstrapGridViewTextColumn  Caption="Id" FieldName="id" Visible="false" />
                                          <dx:BootstrapGridViewTextColumn  Caption="Nombre" FieldName="nombrenodo"  />
                                          <dx:BootstrapGridViewTextColumn  Caption="Matriz" FieldName="NombreMatriz"  />
                                          <dx:BootstrapGridViewTextColumn  Caption="ACYS CN" FieldName="AcysNombre"  />
                                          <dx:BootstrapGridViewTextColumn  Caption="Estatus" FieldName="nombreEstatus"  />
                                          <dx:BootstrapGridViewTextColumn  Caption="Núm Cliente" FieldName="id_Cte"  /> 
                                          <dx:BootstrapGridViewTextColumn  Caption="Nombre Cliente" FieldName="nombreCliente"  /> 
                                          <dx:BootstrapGridViewTextColumn  Caption="Ver ACYS">
                                            <DataItemTemplate> 
                                                <img src="Img/find16.png" id="imgEditar" style="cursor: pointer" onclick="AbrirPantallaACYS(<%# DataBinder.Eval(Container.DataItem, "id_acys") %>, <%# DataBinder.Eval(Container.DataItem, "id_matriz") %> );" />
                                           </DataItemTemplate>
                                       </dx:BootstrapGridViewTextColumn> 
                                          <dx:BootstrapGridViewTextColumn Caption="Vincular">
                                            <DataItemTemplate>
                                                <img <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==0|| Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==6  ? "src='Img/Ic_2.jpg'  ": "  style='display:none' "  %> id="imgPermisos" style="cursor: pointer" onclick="AbrirPantallaVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "id_matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "nombrenodo") %>' );" />
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn> 
                                          <dx:BootstrapGridViewTextColumn Caption="Desvincular">
                                            <DataItemTemplate>
                                                <img <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==2   ? "src='Img/x.gif' " : "style='display:none'"  %> id="imgPermisos" style="cursor: pointer" onclick="AbrirPantallaDesVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "id_matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "nombrenodo") %>' );" />
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>

                                          <dx:BootstrapGridViewTextColumn Caption="Ver Solicitud">
                                            <DataItemTemplate>
                                                <img <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1 || Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5  ? "src='img/ic_1.gif' ": "style='display:none'"  %> id="imgPermisos" style="cursor: pointer" onclick="AbrirPantallaVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "id_matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "nombrenodo") %>');" />
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>

                                          <dx:BootstrapGridViewTextColumn Caption="Cancelar Solicitud">
                                            <DataItemTemplate>
                                                <asp:ImageButton CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Cancelar" OnClientClick="return confirmar();" runat="server" ID="btnCancelar" ImageUrl='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1 || Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? "img/quitar1.png": ""  %>' Visible='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1|| Int32.Parse( DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? true: false  %>' />
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>


                                          <dx:BootstrapGridViewTextColumn Caption="Reenviar Solicitud">
                                            <DataItemTemplate>
                                                <asp:ImageButton CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Reenviar" runat="server" ID="btnReenviar" ImageUrl='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1 || Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? "imagenes/flecha.jpg": ""  %>' Visible='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1|| Int32.Parse( DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? true: false  %>' />
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>

                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         <!-- Acys-->
        <div id="modalAcys" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 560px !important;  width: 80%;""> 
                <div class="modal-content" style="height: 550px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                        ACyS Cuenta Nacionales
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameAcys" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
           <!-- Vinculaciones-->
        <div id="modalVinculaciones" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 560px !important;  width: 80%;""> 
                <div class="modal-content" style="height: 550px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                      Vinculación Cuenta Nacionales
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameVinculaciones" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
           <!-- Solicitud-->
        <div id="modalSolicitud" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 560px !important;  width: 80%;""> 
                <div class="modal-content" style="height: 550px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                       Solicitud Cuenta Nacionales
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameSolicitud" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="messagealert" id="alert_container">
        </div>
    </div>
</asp:Content>
