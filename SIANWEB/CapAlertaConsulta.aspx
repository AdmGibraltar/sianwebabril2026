<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CapAlertaConsulta.aspx.cs" Inherits="SIANWEB.CapAlertaConsulta" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
 
        <%@ Register Assembly="DevExpress.Data.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    
    <!-- Minified JS library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
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
    </style>

    <script type="text/javascript">

        function cerrarpantalla() {
            window.open("CapAlertaAutorizacion.aspx", "_self");
            //              location.reload();

            function modalMensaje(mensaje) {
                document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
                $("#modalMensaje").appendTo("body");
                $("#modalMensaje").modal({ "backdrop": "static" });
                $('#modalMensaje').modal('show');
            }


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
                            <h3 class="panel-title">Consulta de Solicitudes de precios</h3>
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
                                                    <asp:Label ID="Label16" runat="server" Text="Estatus" />
                                                </div>
                                                <div class="col-md-8">
                                                                 <dx:BootstrapComboBox ID="cmbEstatus" runat="server" AutoPostBack="true" >
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

                                  

                                        <%--ROW--%>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label15" runat="server" Text="Tipo de solicitud" />
                                        </div>
                                        <div class="col-md-8">
                                            <dx:BootstrapComboBox ID="CmbTipoSolicitud" runat="server" AutoPostBack="true">
                                            </dx:BootstrapComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label30" runat="server" Text="Fecha Inicial" />
                                        </div>
                                        <div class="col-md-8">
                                            <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date"
                                                ID="txtfechaInicialBuscarInformacion" PickerType="Months">
                                                <CalendarProperties>
                                                    <FastNavProperties />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <button id="btnRegresar" type="button" class="btn btn-warning btn-sm w35" style="margin-top: 8px!important;"
                                        title="Regresar" runat="server" onclick="cerrarpantalla()">
                                        <i class="fa fa-arrow-left"></i> <span>&nbsp;Regresar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></button>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label25" runat="server" Text="Clave producto" />
                                        </div>
                                        <div class="col-md-8">
                                            <dx:BootstrapTextBox ID="txtProducto" runat="server">
                                            </dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label26" runat="server" Text="Fecha Final" />
                                        &nbsp;</div>
                                        <div class="col-md-8">
                                            <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date"
                                                ID="txtfechaFinalBuscarInformacion" PickerType="Months">
                                                <CalendarProperties>
                                                    <FastNavProperties />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                            <%-- <FastNavProperties InitialView="Months" MaxView="Years" />--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <button id="btnDescargarInformacion" type="button" class="btn btn-warning" title="Descargar Detalle"
                                        runat="server" onserverclick="btnDescargarInformacion_ServerClick">
                                        <i class="fa fa-download"></i> <span>&nbsp;Descargar </span>
                                    </button>
                                </div>

                             </div>
 
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label1" runat="server" Text="Clave cliente" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapTextBox ID="txtIdCte" runat="server">
                                                </dx:BootstrapTextBox>
                                            </div>
                                        </div>
                                    </div>

                                     </div> 

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <dx:ASPxGridViewExporter ID="GridViewExporter1" runat="server" GridViewID="gridBuscar">
                                        </dx:ASPxGridViewExporter>
                                        <dx:BootstrapGridView ID="gridBuscar" ClientInstanceName="grid" runat="server" KeyFieldName="IdAutorizacionPrecio"
                                            Width="100%" AutoGenerateColumns="False">
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsBehavior AutoExpandAllGroups="true" />
                                            <Columns>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="Id_Emp" Caption="Id_Emp"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="IdAutorizacionPrecio" Caption="Id"
                                                    Visible="true" SortOrder="Descending">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="Id_Cd" Caption="Id_Cd" Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="IdRepresentante" Caption="IdRepresentante"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200" FieldName="Nom_Representante" Caption="Nombre representante"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="Id_Cte" Caption="No. Cte"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="Cte_NomComercial" Caption="Nombre comercial"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:bootstrapgridviewtextcolumn width="50px" fieldname="Tipo_Cliente" caption="Tipo Cliente" visible="true"></dx:bootstrapgridviewtextcolumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="TipoAutorizacion" Caption="Tipo de solicitud"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="NomTipoSolicitud" Caption="Tipo de solicitud"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="50px" FieldName="Id_Prd" Caption="Id_Prd"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Prd_Descripcion" Caption="Descripcion"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <%--falta descripcion producto y descripcion del estatus --%>
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="Precio_Vta" Caption="Precio de venta">
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn Width="150px" FieldName="PrecioLista" Caption="Precio Lista">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="Precio_MinimoRik" Caption="Precio minimo Rik"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn Width="150px" FieldName="PrecioObjetivo" Caption="Precio Objetivo"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="250px" FieldName="Precio_VtaAutorizado" Caption="Precio de Vta Autorizado"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaSolicitud" Caption="Fecha solicitud">
                                                </dx:BootstrapGridViewTextColumn>
                                                  <dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaAutorizacionGte" Caption="Fecha Aut Gte">
                                                </dx:BootstrapGridViewTextColumn>
                                                  <dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaAutorizacionDir" Caption="Fecha Aut Dir">
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Id_Tamaño" Caption="Tamaño" >
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Req_Aut_Director" Caption="Requiere aut director"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewCheckColumn Caption="Aut. Director" FieldName="Req_Aut_Director"
                                                    Width="50px" />
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Estatus" Caption="Estatus"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="NomEstatus" Caption="Estatus"
                                                    Visible="true">
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="IdUSolicita" Caption="IdUSolicita"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>
                                                
                                                <dx:BootstrapGridViewTextColumn Width="150px" FieldName="Nom_CDI" Caption="Sucursal"
                                                    Visible="false">
                                                </dx:BootstrapGridViewTextColumn>

                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="Justificacion" Caption="Justificación" >
                                                </dx:BootstrapGridViewTextColumn>

                                                <dx:BootstrapGridViewTextColumn Width="300px" FieldName="MotivoRechazo" Caption="Comentarios Rechazo" >
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaRechazoGte" Caption="Fecha Rec Ger">
                                                </dx:BootstrapGridViewTextColumn>
                                                   <dx:BootstrapGridViewTextColumn Width="200px" FieldName="FechaRechazoDir" Caption="Fecha Rec Dir">
                                                </dx:BootstrapGridViewTextColumn>


                                            </Columns>
                                            <SettingsPager PageSize="50" />
                                        </dx:BootstrapGridView>
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

    </div>
</asp:Content>