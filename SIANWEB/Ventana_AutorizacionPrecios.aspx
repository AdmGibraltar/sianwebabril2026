<%@ Page Title="Alerta" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_AutorizacionPrecios.aspx.cs"
    Inherits="SIANWEB.Ventana_AutorizacionPrecios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head> 
  <!-- ALERTIFY 0.3.11 NUEVO -->

    <script src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
        
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        

    <%--BOOTSTRAP bootstrap--%>
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>    
<%--    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">    --%>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script> 

    <%-- ZEBRA DATEPICKER --%>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    
    <%-- FONT AWESOME --%>
<%--    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">    --%>

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">

    <%--TIME PICKER--%>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>

    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>

    
<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
 
        <%@ Register Assembly="DevExpress.Data.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data" TagPrefix="dx" %>

    <!-- Minified JS library -->
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
      <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

<script type="text/javascript">


    //cuando el combo de edición del Grid de TerritorioPartida cambia de indice
    function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
        ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
    }
    function SoloNumericoYDiagonal(sender, eventArgs) {
        var c = eventArgs.get_keyCode();
        if (c && c == 13)
            eventArgs.set_cancel(true);
        if ((c < 48 || c > 57))//si no es numero
            if (c != 47) //si no es punto
                eventArgs.set_cancel(true);
    }

    
</script>
 <style type="text/css">
     .borderbottom
            {
	            color: #000099;
	            background-color: Transparent;
	            padding: 0px;
	            border: 0px solid #000099;
	            font-size:X-Small;
	            font-weight:bold;
	            -moz-border-radius: 0px 0px 0px 0px;
	            
            }

      
     .style1
     {
         height: 56px;
     }  
    </style>
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PnlLogin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

          <%-- ////JFCV PRUEBA --%>
             <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                   <%-- <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="divGenerales" UpdatePanelHeight="" />--%>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalles" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <%--<telerik:AjaxUpdatedControl ControlID="btnFacturaEspecial" 
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtSub" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtIva" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PnlLogin" runat="server" >
        <table style="font-family: Verdana; font-size: 8pt">

            <tr>
            <td colspan="2">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label> 
            </td>
            </tr>
    
       
    
                <tr>
                <td align="center">
                
                </td>
                <td align="left">
                    <asp:Label ID="LblMensaje1" runat="server" Text="Los precios de venta de los siguientes productos están por debajo del precio minimo autorizado. Si quieres continuar con este precio debes alimentar el motivo." Width="500px" Font-Bold="True"></asp:Label>
                </td>
                    <td align="center">
                
                </td>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="Trimestre Anterior." Width="500px" Font-Bold="True"></asp:Label>
                </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <th>

                    </th>
                <th>
                        Cliente
                </th>
                  
                <th>
                        Nombre 
                </th>
                    <th>
                        Tipo Cliente 
                </th>

                      <th>
                        Territorio
                </th>

                 <th>
                        Ventas 
                </th>
               
                 <th>
                       Utilidad Prima
                </th>
                  <th>
                       % Utilidad Prima
                </th>

                    <th>
                       UAFIR Mensual
                </th>
                        <th>
                       UAFIR Anual
                </th>
                      <th>
                       % UAFIR del Cliente
                </th>

                    <th>
                       Utilidad Remanente
                </th>


                </tr>
                    <tr>
                         <td align="center">
                &nbsp;
                </td>
                <td>
                        <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MinValue="1"
                            MaxLength="9" AutoPostBack="True"  >
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                             
                        </telerik:RadNumericTextBox>
                </td>
                <td>
                        <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>
                <td>
                        <telerik:RadTextBox ID="txtTipoCliente" runat="server" Width="50px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>

                <td>
                        <telerik:RadTextBox ID="txtTerritorio" runat="server" Width="200px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>

                 <td>
                        <telerik:RadTextBox ID="txtVentas" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>
                
                 <td>
                       <telerik:RadTextBox ID="txtUtilidadPrima" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>
                  <td>
                       <telerik:RadTextBox ID="txtPorcUtilidadPrima" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>

                <td>
                       <telerik:RadTextBox ID="txtUafirmes" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>
                        <td>
                        <telerik:RadTextBox ID="txtUafirAnual" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>

                    <td>
                        <telerik:RadTextBox ID="txtPorcUafirCte" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>

                    <td>
                        <telerik:RadTextBox ID="txtUtilRemanente" runat="server" Width="120px" ReadOnly="True">
                        </telerik:RadTextBox>  
                </td>


                </tr>

                
                  
           </table>
            <table style="font-family: Verdana; font-size: 8pt">   


                    <tr>
                <td align="center">
                &nbsp;
                </td>
                <td align="left">
                 
                
                    
                     <telerik:RadGrid ID="rgDetalles" class="table table-hover table-bordered RadGrid_Outlook" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                         OnDeleteCommand="rgDetalles_DeleteCommand"  
                                                        OnItemCommand="rgDetalles_ItemCommand" OnItemDataBound="rgDetalles_ItemDataBound"
                                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="rgDetalles_UpdateCommand"
                                                        PageSize="8"    OnPageIndexChanged="rgDetalles_PageIndexChanged" AllowPaging="True">

                                                        <%--  Width="1210px">--%>
                                                        <ClientSettings>
                                                            <ClientEvents OnCommand="onCommand" />
                                                           
                                                        </ClientSettings>

                                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="Id_Prd" EditMode="InPlace"
                                                            NoMasterRecordsText="No se encontraron registros.">
                                                            <%--<CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                                                ShowRefreshButton="true" />--%>
                                                            <Columns>
                                                     <%--           <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelar" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Desea eliminar esta clave del convenio?" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px" Text="Cancelar" UniqueName="Cancelar" Display="True"
                                                                    ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridButtonColumn>--%>
                                                               
                                                                  <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"   
                                                                    HeaderText="Editar" InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ImageUrl="~/Imagenes/delete2.png"
                                                                    ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Desea eliminar la siguiente clave del convenio?"  
                                                                    ConfirmTitle="" HeaderText="Eliminar Solicitud" Text="Borrar" UniqueName="DeleteColumn">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridButtonColumn>
                                                              <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="enviar" ImageUrl="Imagenes/flecha.jpg" 
                                                            Text="Enviar Solicitud" UniqueName="DetailColumn2" ConfirmText="¿Desea Enviar a autorizar este Producto?" >
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>


                                                               <%-- <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="enviar" ConfirmDialogHeight="150px" ImageUrl="~/Imagenes/check3.png"
                                                                    ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Desea Enviar a autorizar este Producto?"  
                                                                    ConfirmTitle="" HeaderText="Enviar" Text="Enviar" UniqueName="EmailColumn" ButtonCssClass="aceptar">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridButtonColumn>--%>
  
                                                             
                                                                 <%--JFCV 08 sep 2020 agregar campo de bennetts codigo equivalente --%>
                                                              
                                                                  <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Clave del Producto" UniqueName="Id_Prd">
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Id_PrdLabel" runat="server" Text='<%# Eval("Id_Prd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="90px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                    

                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripción">
                                                                <ItemTemplate>
                                                                        <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="120px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>
                    
                                      


                                                                <%--///JFCV agregar Campos --%>

                                                             
                                                             <telerik:GridTemplateColumn DataField="IdAutorizacionAnterior" HeaderText="Estatus" UniqueName="IdAutorizacionAnterior">
                                              <ItemTemplate>
                                                <span id="lbIdAutorizacionAnterior" class="label label-success"
                                                      style='<%# (Eval("IdAutorizacionAnterior") != DBNull.Value && Eval("IdAutorizacionAnterior") != null && Eval("IdAutorizacionAnterior").ToString() != "0" ? "" : "display:none;") %>'>
                                                  Sol.Enviada
                                                </span>
                                                <span id="lbIdAutorizacionAnterior2" class="label label-warning"
                                                      style='<%# (Eval("IdAutorizacionAnterior") == DBNull.Value || Eval("IdAutorizacionAnterior") == null || Eval("IdAutorizacionAnterior").ToString() == "0" ? "" : "display:none;") %>'>
                                                  Enviar Sol.
                                                </span>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                              <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>



                                                                <telerik:GridTemplateColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad">
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:Label ID="lblCantidad" runat="server"
                                                                        Text='<%# Eval("Cantidad") == DBNull.Value || Eval("Cantidad") == null ? "" : string.Format("{0:###,##0.00}", Eval("Cantidad"))%>'>
                                                                    </asp:Label>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Right" /> </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio_Vta" HeaderText="P. Venta <br> Ingresado" UniqueName="Precio_Vta">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecio_Vta" runat="server" 
                                                                        Text='<%# Eval("Precio_Vta") == DBNull.Value || Eval("Precio_Vta") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Precio_Vta")))%>'>
                                                                     </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                                <ItemStyle HorizontalAlign="Right" /> </telerik:GridTemplateColumn>
 

                                                                <telerik:GridTemplateColumn DataField="PrecioLista" HeaderText="Precio  <br> Lista" UniqueName="PrecioLista">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecioLista" runat="server" 
                                                                        Text='<%# Eval("PrecioLista") == DBNull.Value || Eval("PrecioLista") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("PrecioLista")))%>'>
                                                                    </asp:Label>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>
                                                               
                                                                <%--  precio precioObjetivo --%>

                                                                 <telerik:GridTemplateColumn DataField="Id_Tamaño" HeaderText="Tamaño" UniqueName="Id_Tamaño">
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Id_TamañoLabel" runat="server" Text='<%# Eval("Id_Tamaño") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="PrecioObjetivo" HeaderText="Precio <br> Objetivo" UniqueName="PrecioObjetivo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecioObjetivo" runat="server" 
                                                                         Text='<%# Eval("PrecioObjetivo") == DBNull.Value || Eval("PrecioObjetivo") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("PrecioObjetivo")))%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio_MinimoRik" HeaderText="P. Venta <br> Min Rik" UniqueName="Precio_MinimoRik">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecioMinimoRik" runat="server" 
                                                                        
                                                                        Text='<%# Eval("Precio_MinimoRik") == DBNull.Value || Eval("Precio_MinimoRik") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Precio_MinimoRik")))%>'>

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>


                      
                                                               <telerik:GridTemplateColumn DataField="Precio_MinimoGte" HeaderText="P. Venta <br> Min Gerente" UniqueName="Precio_MinimoGte" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecio_MinimoGte" runat="server" 
                                                                        Text='<%# Eval("Precio_MinimoGte") == DBNull.Value || Eval("Precio_MinimoGte") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Precio_MinimoGte")))%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>


                                                                <telerik:GridTemplateColumn DataField="Utilidad" HeaderText="Utilidad <br> Prima" UniqueName="Utilidad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUtilidad" runat="server"  
                                                                        Text='<%# Eval("Utilidad") == DBNull.Value || Eval("Utilidad") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Utilidad")))%>'>
                                                                    </asp:Label>
 
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>



                                                                <telerik:GridTemplateColumn DataField="Porc_Utilidad" HeaderText="% Ut" UniqueName="Porc_Utilidad"  >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPorc_Utilidad" runat="server"  
                                                                    Text='<%# Eval("Porc_Utilidad") == DBNull.Value || Eval("Porc_Utilidad") == null ? "" : string.Format("{0:P}", Convert.ToInt64(Eval("Porc_Utilidad")))%>'>
                                                                        </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridTemplateColumn>
                                                               

                                                                 <telerik:GridTemplateColumn DataField="Importe_Utilidad" HeaderText="Importe <br> Venta" 
                                                                    UniqueName="Importe">
                                                                    <EditItemTemplate>
                                                                          <asp:Label ID="lblImporteedit" runat="server"
                                                                               Text ='<%# Eval("Importe") == DBNull.Value || Eval("Importe") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Importe")))%>'>
                                                                          </asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                       <asp:Label ID="lblImporte" runat="server" 
                                                                           Text ='<%# Eval("Importe") == DBNull.Value || Eval("Importe") == null ? "" : string.Format("{0:###,##0.00}", Convert.ToInt64(Eval("Importe")))%>'> 
                                                                          </asp:Label> </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Importe_Utilidad" HeaderText="Total <br> Utilidad Prima" 
                                                                    UniqueName="Importe_Utilidad2">
                                                                    <EditItemTemplate>
                                                                          <asp:Label ID="lblImporte_Utilidad2" runat="server" Text='<%# Eval("Importe_Utilidad","{0:##,##0.00}") %>'></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblImporte_Utilidad" runat="server" Text='<%# Eval("Importe_Utilidad","{0:##,##0.00}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>

                                          
                                                                 <telerik:GridTemplateColumn DataField="FechaVigencia" HeaderText="Fecha vigencia" UniqueName="FechaVigenciaB"  >
                                                                <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label109" runat="server" Text='<%# Bind("FechaVigencia",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                <EditItemTemplate>                                                                        
                                                                        <telerik:RadDatePicker ID="tpFechaVigencia" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaVigencia_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001"  Enabled="true"
                                                                            DbSelectedDate ='<%# Eval("FechaVigencia") %>' >
                                                                        <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                            ViewSelectorText="x" ShowRowHeaders="false">
                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                TodayButtonCaption="Hoy" />
                                                                        </Calendar>
                                                                        <DateInput ID="DateInput1" runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                    </telerik:RadDatePicker>                                                                      
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>


                                                                 
                                                                 
                                                                 <telerik:GridTemplateColumn HeaderText="Motivo" DefaultInsertValue="Otro" HeaderStyle-Width="100px" UniqueName="CategoryID" DataField="Id_Motivo">
                                                                    <ItemTemplate>
                                                                        <%# Eval("MotivoRechazo") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="MotivoDropDown" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" DataValueField="Id_Motivo"
                                                                           MarkFirstMatch="true" LoadingMessage="Cargando..." DataTextField="MotivoRechazo" OnSelectedIndexChanged="cmbMotivo_SelectedIndexChanged" AutoPostBack="true"  >
                                                                             <items>
                                                                                <telerik:RadComboBoxItem Value="-1" Text="Seleccione un motivo" />
                                                                                <telerik:RadComboBoxItem Value="1" Text="No se tenía en existencia y se sustituyó" />
                                                                                <telerik:RadComboBoxItem Value="2" Text="Así se negoció con el cliente por el RIK" />
                                                                                <telerik:RadComboBoxItem Value="3" Text="Otro" />
                                                                            </items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Justificacion" HeaderText="Otro Motivo" 
                                                                    UniqueName="Justificacion">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="RadTextBoxJustificacion" runat="server" MaxLength="200"
                                                                            MinValue="0" Text='<%# Bind("Justificacion") %>' Width="100%" Enabled="false" Visible="false">
                                                                             
                                                                        </telerik:RadTextBox>
                                                                    
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblJustificacion" runat="server" Text='<%# Eval("Justificacion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="JustificacionMemo" HeaderText="Justifique Brevemente" 
                                                                    UniqueName="JustificacionMemo">
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="RadTextBoxJustificacionMemo" runat="server" MaxLength="400"
                                                                            MinValue="0" Text='<%# Bind("JustificacionMemo") %>' Width="100%">
                                                                             
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblJustificacionMemo" runat="server" Text='<%# Eval("JustificacionMemo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>
 

                                                                  <telerik:GridTemplateColumn DataField="Id_Cpr" HeaderText="Tipo" UniqueName="Id_Cpr" Visible="false">
                                                                <ItemTemplate>
                                                                        <asp:Label ID="Id_CprLabel" runat="server" Text='<%# Eval("Id_Cpr") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                
                                                                


                                                            </Columns>
                                                            <EditFormSettings>
                                                                <EditColumn UniqueName="EditCommandColumn1">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <%--<Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"  />--%>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>



                &nbsp; 

                </td>
                </tr>
            
            
            <tr>
                <td align="center" colspan="2">
                  <%--  <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" 
                        Text="Ignorar" />--%>
        
                       <button id="btnCancelar" type="button" class="btn btn-default" title="Guardar sin enviar solicitud."
                                                ClientInstanceName="btnCancelar" runat="server"  onserverclick="btnCancelar_Click" visible="true">
                                                 <span id="span1">&nbsp;Guardar Sin Enviar Sol</span>
                                            </button>

</td>
               <td align="center">
                    &nbsp;
                </td>
               </tr>
        </table>
        <table>

                <tr>
            <td>
                <asp:Label Width="350px">Motivo </asp:Label>
            </td>
               <td>
                <asp:Label ID="lblotromotivo"  runat="server" Width="250px" visible="false" >Capture Otro Motivo>  </asp:Label>
                 
            </td>
            <td>
                <asp:Label Width="350px">Capture Justificación </asp:Label>
            </td>
                <td>
                <asp:Label Width="350px">Aplicar a todos los productos </asp:Label>
            </td>
            </tr>

                <tr>
                <td align="center" >
 
                    <dx:BootstrapComboBox ID="cmbMotivoTodo" AutoPostBack="true"  Width="350px"
                         runat="Server"  ClientInstanceName="cmbMotivoTodo" OnSelectedIndexChanged="cmbMotivoTodo_SelectedIndexChanged1"  > </dx:BootstrapComboBox>
  

         
                </td>
                <td>

                    <telerik:RadTextBox ID="txtOtroMotivo" runat="server" Width="250px"
                        MaxLength="400" AutoPostBack="False" Visible="false">
                    </telerik:RadTextBox>


                </td>
                <td>

                   <%-- <telerik:RadTextBox ID="" runat="server" Width="300px"
                        MaxLength="400" AutoPostBack="False">
                    </telerik:RadTextBox>--%>
                     <telerik:RadTextBox ID="txtJustificacion" runat="server"  
                        TextMode="MultiLine" Rows="5" Columns="80"  Style="resize:vertical; min-width:400px;"  MaxLength="400" AutoPostBack="False">
                     </telerik:RadTextBox>



                </td>

                <td>

                    <button id="btnActualizar" type="button" class="btn btn-primary" title="Actualizar"
                                                runat="server"  onserverclick="btnActualizar_Click" >
                                               <i class="fa fa-share"></i> <span>&nbsp;Actualizar</span>
                    </button>


                </td>
                <td>
                    <button id="btnenviartodo" type="button" class="btn btn-primary" title="Enviar "
                        runat="server" onserverclick="btnEnviartodo_Click" visible="true">
                        <i class="fa fa-envelope-o"></i><span>&nbsp;Enviar</span>
                    </button>
                </td>
                <td></td>
            </tr>

            <tr>

                <td>
                    <asp:Label ID="lblgrabarfactura" style="font-size:18px;color:red" Font-Bold="True" runat="server">En clientes de tamaño B,C,D y E. No podrá grabar la factura si cuenta con precios no autorizados.</asp:Label>
                </td>
                <td> <asp:Label Width="350px">Capture Justificación General </asp:Label></td>
                <td>

                     <telerik:RadTextBox ID="txtComentarioGeneral" runat="server"  
                        TextMode="MultiLine" Rows="5" Columns="80"  Style="resize:vertical; min-width:400px;"  MaxLength="400" AutoPostBack="False">
                     </telerik:RadTextBox>
                     </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

        </table>
      <div id="divResumen" runat="server">
          <asp:hiddenfield id="tipoAutorizacion" 
              value="" 
              runat="server"/>
          <%-- Que no se grabe factura 19 junio --%>
           <asp:hiddenfield id="hdId_Tamaño" 
              value="" 
              runat="server"/>
          <asp:hiddenfield id="hdBloquea_Facturas" 
              value="" 
              runat="server"/>
          <asp:hiddenfield id="hdId_ReporteGP" 
            value="" 
            runat="server"/>
           <asp:hiddenfield id="hdId_Rik" 
           value="" 
           runat="server"/>
    </div>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }


            function AlertaFocus(mensaje, control) {

                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.RadWindow) {
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog     
                }
                else if (window.frameElement.RadWindow) {
                    oWindow = window.frameElement.RadWindow;  //IE (and Moz as well)  
                }


                if (oWindow == null) {
                    if (window.radWindow) {
                        oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog     
                    }
                    else if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;  //IE (and Moz as well)  
                    }
                }
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre

            window.closeModalalerta = function () {
                $('#ModalAlerta').modal('hide');
            };

            function CloseAndRebind(param) {
                window.parent.closeModalalerta();
            };

            function CloseGpma() {
                $('#ModalAlerta').modal('hide');
                if (window.opener && typeof window.opener.windowsClose === 'function') {
                    console.log('opcion1');
                    window.opener.windowsClose();
                } else if (window.parent && typeof window.parent.windowsClose === 'function') {
                    console.log('opcion2');
                    window.parent.windowsClose();
                } else {
                    console.log('opcion3');
                    $('.modal').modal('hide'); // Fallback: cerrar la ventana si no encuentra la función
                }
            };
            

           //// jfcv 22sep 2022 function CloseAndRebind2(param) {
           ////     GetRadWindow().Close();
           ////}

            //jfcv 22sep2022 Hace un refresh sobre un control especifico, requiere una función en la ventana padr
            //lo utilizo en facturas para que grabe la factura después de cerrar la pantalla
            function CloseAndRebind2(param) {
                debugger;
                var radWindow = GetRadWindow();
                if (radWindow != null) {
                    radWindow.Close();
                    if (radWindow.BrowserWindow && typeof radWindow.BrowserWindow.GrabaAlerta === "function") {
                        radWindow.BrowserWindow.GrabaAlerta(param);
                    }
                } else {
                    // Manejo alternativo si no se encuentra la ventana
                    console.warn("No se encontró la RadWindow.");
                }
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            //remisiones para que grabe la remision aunque tenga alertas de precios 
            function CloseAndRebindRem(param) {
                debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.ClienteSeleccionado(param);
            }

            //jfcv 23sep22 para cerrar la pantalla de pedido cuando el tipo autorización es 2 ( pedidos) 
            function CloseWindowPedido() {
                 window.parent.closeModalAlertaPed();
            };

            ///Rgdetalles prueba jfcv 

            function onCommand(sender, eventargs) {
                if (eventargs.get_commandName() == "PerformInsert" || eventargs.get_commandName() == "Update" || eventargs.get_commandName() == "Delete") {
                    var radGrid = $find('<%= rgDetalles.ClientID %>');
                    var table = radGrid.get_masterTableView();
                    var column = table.getColumnByUniqueName("EditCommandColumn");
                    table.hideColumn(column.get_element().cellIndex);

                    column = table.getColumnByUniqueName("DeleteColumn");
                    table.hideColumn(column.get_element().cellIndex);
                }
            }

            

            function showcolum() {
                var radGrid = $find('<%= rgDetalles.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("EditCommandColumn");
                table.showColumn(column.get_element().cellIndex)

                column = table.getColumnByUniqueName("DeleteColumn");
                table.showColumn(column.get_element().cellIndex); 
            }



        </script>
    </telerik:RadCodeBlock>
    </form>
</body>

</html>