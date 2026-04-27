<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ProPedidoProductosAcys2.aspx.cs" Inherits="SIANWEB.ProPedidoProductosAcys2" %>

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
        
        
        .dropdown-toggle
        {
            height: 34px !important;
        }
        
        .caret
        {
            margin-top: 10px !important;
        }
    </style>
       <script type="text/javascript"> 
           
           function onSelectedIndexChanged(cmbId_Prd) {
               column = cmbId_Prd.focusedColumn;
               visibleIndex = cmbId_Prd.visibleIndex;
               var idProd = cmbId_Prd.GetValue().toString();
               var IdTer = $("#" + '<%=txtIdTer.ClientID%>').val();
               var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
               var IdRik = $("#" + '<%=txtIdRik.ClientID%>').val();
               var clave = $("#" + '<%=txtClave.ClientID%>').val();
               var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
               var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
               var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();
               var pedidoProg = $("#" + '<%=HF_pedido.ClientID%>').val();

               cmbId_Prd.SetEnabled(false);

               var dataValue = "{ IdProd: '" + idProd + "', idterr: '" + IdTer + "'  , idCte: '" + IdCte + "', IdRik: '" + IdRik + "', clave: '" + clave + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "', pedidoProg: '" + pedidoProg + "'}";
               $.ajax({
                   type: "POST",
                   url: "ProPedidoVI.aspx/cmbProductoDetRestos",
                   data: dataValue,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       cmbId_Prd.SetEnabled(true);
                       modalMensaje(errorThrown);
                   },
                   success: function (response) {
                       cmbId_Prd.SetEnabled(true);

                       if (response != null && response.d != null) {
                           var data = response.d;

                           data = $.parseJSON(data);

                           var id = data.id;
                           console.log(data)
                           if (id == -1) {
                             
                               modalMensaje(data.men);
                           }
                           if (id == 1) {
                               modalMensaje('Por favor, capture un territorio en la vista \"Datos Generales\"');
                           }
                           if (id == 2) {
                               modalMensaje('Por favor, capture un cliente en la vista \"Datos Generales\"');
                           }
                           if (id == 3) {
                               modalMensaje('Por favor, capture un representante de ventas en la vista \"Datos Generales\"');
                           }
                           if (id == 4) {
                               modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                           }
                           else {
                               data.Presentacion
                               var cmbDesc = CPH_rg1_Restos.GetEditor("Prd_Descripcion");
                               cmbDesc.SetValue(data.Descripcion);
                               var cmbPres = CPH_rg1_Restos.GetEditor("Prd_Presentacion");
                               cmbPres.SetValue(data.Presentacion);
                               var cmbUni = CPH_rg1_Restos.GetEditor("Prd_Unidad");
                               cmbUni.SetValue(data.PrdUni);
                               var cmbCant = CPH_rg1_Restos.GetEditor("Prd_Cantidad");
                               cmbCant.SetValue(data.Cant);
                               var cmbPre = CPH_rg1_Restos.GetEditor("Prd_Precio");
                               cmbPre.SetValue(data.Precio); s
                           }
                       }
                   }
               });
           }


           function onSelectedCantidadIndexChanged(cmbCant) {
               column = cmbCant.focusedColumn;
               visibleIndex = cmbCant.visibleIndex;
               var cantidad = cmbCant.GetValue().toString();

               var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
               var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
               var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
               var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();

               var cmbidProd = CPH_rg1_Restos.GetEditor("Id_Prd");

               var idProd = cmbidProd.GetValue();

               var cmbPre = CPH_rg1_Restos.GetEditor("Prd_Precio");
               var precio = cmbPre.GetValue();

               var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";
               $.ajax({
                   type: "POST",
                   url: "ProPedidoVI.aspx/txtCantidad_TextChanged",
                   data: dataValue,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       cmbCant.SetEnabled(true);
                       modalMensaje(errorThrown);
                   },
                   success: function (response) {
                       cmbCant.SetEnabled(true);

                       if (response != null && response.d != null) {
                           var data = response.d;

                           data = $.parseJSON(data);
                           var id = data.id;

                           if (id == -1) {

                               modalMensaje(data.men);
                           }
                           if (id == 1) {
                               modalMensaje('La cantidad debe ser mayor a 0');
                           }

                           if (id == 3) {
                               modalMensaje('El producto cuenta con precio AAA especial');
                           }
                           if (id == 4) {
                               modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                           }
                           else {




                               var cmbimporte = CPH_rg1_Restos.GetEditor("Prd_Importe");
                               cmbimporte.SetValue(data.importe);

                               if (id == 2) {
                                   modalMensaje('Inventario disponible insuficiente, <br>Inventario final: ' + data.final + ' <br>Asignado: ' + data.asignado + ' <br>Disponible: ' + data.disponible);
                               }
                           }
                       }
                   }
               });
           }


           function onSelectedPrecioIndexChanged(cmbPre) {
               column = cmbPre.focusedColumn;
               visibleIndex = cmbPre.visibleIndex;
               var precio = cmbPre.GetValue().toString();

               var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
               var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
               var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
               var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();

               var cmbidProd = CPH_rg1_Restos.GetEditor("Id_Prd");

               var idProd = cmbidProd.GetValue();

               var cmbcant = CPH_rg1_Restos.GetEditor("Prd_Cantidad");
               var cantidad = cmbcant.GetValue();

               var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";
               $.ajax({
                   type: "POST",
                   url: "ProPedidoVI.aspx/txtPrecio_TextChanged",
                   data: dataValue,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       cmbPre.SetEnabled(true);
                       modalMensaje(errorThrown);
                   },
                   success: function (response) {
                       cmbPre.SetEnabled(true);

                       if (response != null && response.d != null) {
                           var data = response.d;

                           data = $.parseJSON(data);
                           var id = data.id;

                           if (id == -1) {

                               modalMensaje(data.men);
                           }
                           if (id == 1) {
                               modalMensaje('El producto cuenta con precio AAA especial');
                           }
                           else {
                               var cmbimporte = CPH_rg1_Restos.GetEditor("Prd_Importe");
                               cmbimporte.SetValue(data.importe);
                           }
                       }
                   }
               });
           }


           function onSelectedCantidadIndexChangedVI(cmbCant) {
               column = cmbCant.focusedColumn;
               visibleIndex = cmbCant.visibleIndex;
               var cantidad = cmbCant.GetValue().toString();

               var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
               var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
               var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
               var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();

               var cmbidProd = CPH_rg1.GetEditor("Id_Prd");
               var idProd = cmbidProd.GetValue();

               var cmbPre = CPH_rg1.GetEditor("Prd_Precio");
               var precio = cmbPre.GetValue();

               var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";

               console.log(dataValue);
               $.ajax({
                   type: "POST",
                   url: "ProPedidoVI.aspx/txtCantidad_TextChanged",
                   data: dataValue,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       cmbCant.SetEnabled(true);
                       modalMensaje(errorThrown);
                   },
                   success: function (response) {
                       cmbCant.SetEnabled(true);

                       if (response != null && response.d != null) {
                           var data = response.d;

                           data = $.parseJSON(data);
                           var id = data.id;

                           if (id == -1) {

                               modalMensaje(data.men);
                           }
                           if (id == 1) {
                               modalMensaje('La cantidad debe ser mayor a 0');
                           }
                           if (id == 2) {
                               modalMensaje('Inventario disponible insuficiente, <br>Inventario final: ' + data.final + ' <br>Asignado: ' + data.asignado + ' <br>Disponible: ' + data.disponible);
                           }
                           if (id == 3) {
                               modalMensaje('El producto cuenta con precio AAA especial');
                           }
                           if (id == 4) {
                               modalMensaje('Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.');
                           }
                           else {
                               var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                               cmbimporte.SetValue(data.importe);
                           }
                       }
                       else {
                           modalMensaje('Error en la consulta');
                       }
                   }
               });
           }


           function onSelectedPrecioIndexChangedVI(cmbPre) {
               column = cmbPre.focusedColumn;
               visibleIndex = cmbPre.visibleIndex;
               var precio = cmbPre.GetValue().toString();

               var IdCte = $("#" + '<%=txtIdCte.ClientID%>').val();
               var IdCd = $("#" + '<%=HF_IdCd.ClientID%>').val();
               var IdEmp = $("#" + '<%=HF_IdEmp.ClientID%>').val();
               var EmpCnx = $("#" + '<%=HF_Emp_Cnx.ClientID%>').val();

               var cmbidProd = CPH_rg1.GetEditor("Id_Prd");

               var idProd = cmbidProd.GetValue();

               var cmbcant = CPH_rg1.GetEditor("Prd_Cantidad");
               var cantidad = cmbcant.GetValue();

               var dataValue = "{ cantidad: '" + cantidad + "', precio: '" + precio + "'  , idCte: '" + IdCte + "', Id_prd: '" + idProd + "', IdCd: '" + IdCd + "', IdEmp: '" + IdEmp + "', EmpCnx: '" + EmpCnx + "'}";


               $.ajax({
                   type: "POST",
                   url: "ProPedidoVI.aspx/txtPrecio_TextChanged",
                   data: dataValue,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       cmbPre.SetEnabled(true);
                       modalMensaje(errorThrown);
                   },
                   success: function (response) {
                       cmbPre.SetEnabled(true);

                       if (response != null && response.d != null) {
                           var data = response.d;

                           data = $.parseJSON(data);
                           var id = data.id;

                           if (id == -1) {

                               modalMensaje(data.men);
                           }
                           if (id == 1) {
                               modalMensaje('El producto cuenta con precio AAA especial');
                           }
                           else {

                               var cmbimporte = CPH_rg1.GetEditor("Prd_Importe");
                               cmbimporte.SetValue(data.importe);
                           }
                       }
                       else {
                           modalMensaje('Error en la consulta');
                       }
                   }
               });
           }

           function modalMensaje(mensaje) {
               document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
               $("#modalAcysMensaje").appendTo("body")
               $("#modalAcysMensaje").modal({ "backdrop": "static" });
               $('#modalAcysMensaje').modal('show');
           }
    </script>
    <div class="modal-body" id="Div2">
      <div class="col-md-12">
       <dx:BootstrapButton ID="btncaptacion_Guardar" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Primary"
                        OnClick="btnguardar_Click">
                        
                    </dx:BootstrapButton>
      </div>
        <div class="col-md-12">
            <dx:BootstrapGridView ID="rg1" runat="server" KeyFieldName="Id_Prd" Width="100%"
                EnableRowsCache="false" OnRowInserting="rg1_RowInserting" OnRowUpdating="rg1_RowUpdating"
                OnRowDeleting="rg1_RowDeleting" EnableCallBacks="true"  >
                <SettingsEditing Mode="Inline">
                </SettingsEditing>
                <SettingsDataSecurity AllowEdit="true" AllowDelete="true" />
                <SettingsCommandButton>
                    <EditButton IconCssClass="fa fa-edit" Text=" " />
                    <DeleteButton IconCssClass="fa fa-remove" Text=" " />
                    <CancelButton IconCssClass="fa fa-ban" Text=" " />
                    <UpdateButton IconCssClass="fa fa-check" Text=" " />
                    <NewButton IconCssClass="fa fa-plus" Text=" " />
                </SettingsCommandButton>
               
                <Columns>
                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true"
                        Caption=" " />
                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" Caption="Código." ReadOnly="true">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                            <ClientSideEvents TextChanged="onSelectedIndexChanged" />
                        </PropertiesTextEdit>
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" ReadOnly="true" Caption="Descripción">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Presentacion" ReadOnly="true" Caption="Presentación.">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Unidad" ReadOnly="true" Caption="Unidad">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" Caption="Cantidad">
                        <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                            <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                        </PropertiesSpinEdit>
                    </dx:BootstrapGridViewSpinEditColumn>
                    <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Precio" Caption="Precio vta.">
                        <PropertiesSpinEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                            <ClientSideEvents NumberChanged="onSelectedPrecioIndexChangedVI" />
                        </PropertiesSpinEdit>
                    </dx:BootstrapGridViewSpinEditColumn>
                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Importe" ReadOnly="true" Caption="Importe">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:BootstrapGridViewTextColumn>
                    <dx:BootstrapGridViewComboBoxColumn Caption="Frecuecnia" FieldName="Acs_Frecuencia">
                        <PropertiesComboBox TextField="Acs_Frecuencia" ValueField="Acs_Frecuencia" EnableSynchronization="False">
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                            <Items>
                                <dx:BootstrapListEditItem Value="1" Text="Semanal" />
                                <dx:BootstrapListEditItem Value="2" Text="Mensual" />
                                <dx:BootstrapListEditItem Value="3" Text="Bimestral" />
                                <dx:BootstrapListEditItem Value="4" Text="Trimestral" />
                                <dx:BootstrapListEditItem Value="5" Text="Semestral" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:BootstrapGridViewComboBoxColumn>
                    <dx:BootstrapGridViewComboBoxColumn Caption="Doc. de entrega" FieldName="Acs_Doc">
                        <PropertiesComboBox TextField="Acs_Doc" ValueField="Acs_Doc" EnableSynchronization="False">
                            <ValidationSettings RequiredField-IsRequired="true">
                            </ValidationSettings>
                            <Items>
                                <dx:BootstrapListEditItem Text="Factura" Value="F" />
                                <dx:BootstrapListEditItem Text="Remisión" Value="R" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:BootstrapGridViewComboBoxColumn>
                </Columns>
            </dx:BootstrapGridView>

            <asp:HiddenField ID="HF_IdCd" runat="server" />
    <asp:HiddenField ID="HF_IdEmp" runat="server" />
    <asp:HiddenField ID="HF_Emp_Cnx" runat="server" />
    <asp:HiddenField ID="HF_ID" runat="server" />
    <asp:HiddenField ID="HF_Sem" runat="server" />
    <asp:HiddenField ID="HF_pedido" runat="server" />
    <asp:HiddenField ID="HF_Anio" runat="server" />
    <asp:HiddenField ID="HF_Param" runat="server" />
     <asp:HiddenField ID="txtIdTer" runat="server" />
    <asp:HiddenField ID="txtIdCte" runat="server" />
    <asp:HiddenField ID="txtIdRik" runat="server" />
    <asp:HiddenField ID="txtClave" runat="server" />
     
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
                <div class="modal-body" id="Div11" style="overflow-y: hidden !Important;">
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
        </div>
    </div>
</asp:Content>
