<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master"
    AutoEventWireup="true" CodeBehind="PropedidoVI_AcysCliente2.aspx.cs" Inherits="SIANWEB.PropedidoVI_AcysCliente2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/alertify.js-master/src/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>"
        rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <style type="text/css">
        .modal-body
        {
            max-height: calc(100% - 20px);
            overflow-y: scroll;
        }
    </style>
    <div class="modal-body" id="form1">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="col-md-4">
                            <asp:Label ID="Lbl" runat="server" Text="Cliente:" />
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="lblnombreCliente" runat="server" Text="Cliente:" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2" style="margin-top: 5px">
            </div>
            <div class="col-md-8" style="margin-top: 5px">
                <dx:BootstrapGridView ID="RadGrid1" runat="server" ClientInstanceName="grid" KeyFieldName="Id_Acs;pedido;Acs_Semana;Acs_Anio"
                    Width="100%" EnableRowsCache="false">
                     <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    <Columns>
                        <dx:BootstrapGridViewTextColumn FieldName="Id_Acs" Caption="Acuerdo" ReadOnly="true">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Anio" Caption="Año" ReadOnly="true">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Acs_Semana" Caption="Semana" ReadOnly="true">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="pedido" ReadOnly="true" Caption="Número de pedido">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Acs_EstatusStr" ReadOnly="true" Caption="Estatus">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Estatus" ReadOnly="true" Caption="Estatus"
                            Visible="false">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Acs_VigenciaStr" ReadOnly="true" Caption="Vigencia">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewDateColumn Width="80px">
                            <DataItemTemplate>
                                <button id="btnSeleccionar" type="button" class="btn btn-link" runat="server" onserverclick="btnSelecionar_Click">
                                    <span>Seleccionar</span>
                                </button>
                            </DataItemTemplate>
                        </dx:BootstrapGridViewDateColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dx:BootstrapGridView ID="gridpedidoVIProducto" runat="server" Width="80%" OnBeforePerformDataSelect="detailGrid_DataSelect"
                                AutoGenerateColumns="False">
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                <Columns>
                                    <dx:BootstrapGridViewTextColumn FieldName="Acs_FechaF" Caption="Acs_FechaF" Visible="false" />
                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" Caption="Núm." CssClasses-DataCell="leftText">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" ReadOnly="true" CssClasses-DataCell="leftText"
                                        Caption="Producto">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" CssClasses-DataCell="leftText"
                                        ReadOnly="true" Caption="Presen.">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" CssClasses-DataCell="leftText"
                                        ReadOnly="true" Caption="Unidad">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" CssClasses-DataCell="rightText"
                                        Caption="Cant.">
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" CssClasses-DataCell="rightText"
                                        Caption="Precio vta.">
                                    </dx:BootstrapGridViewSpinEditColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" CssClasses-DataCell="rightText"
                                        ReadOnly="true" Caption="Importe">
                                    </dx:BootstrapGridViewTextColumn>
                                </Columns>
                            </dx:BootstrapGridView>
                        </DetailRow>
                    </Templates>
                </dx:BootstrapGridView>
            </div>
        </div>
    </div>
    <div id="modalAcysMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">
                        Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true">
                            </i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje" runat="server"></span>
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
    <script type="text/javascript">
        function closeAcysCliente(Id_Acs, Semana, Pedido, anio) {
            window.parent.closeModalAcysCliente(Id_Acs, Semana, Pedido, anio);
        };


        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
            $("#modalAcysMensaje").appendTo("body")
            $("#modalAcysMensaje").modal({ "backdrop": "static" });
            $('#modalAcysMensaje').modal('show');
        }
    </script>
</asp:Content>
