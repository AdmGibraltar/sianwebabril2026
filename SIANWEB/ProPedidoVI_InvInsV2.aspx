<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI_InvInsV2.aspx.cs" Inherits="SIANWEB.ProPedidoVI_InvInsV2" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
       <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>"> 
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <style type="text/css">
        /* Aseguramos que la página dentro del iframe use el viewport disponible y permita scroll interno */
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        /* Contenedor principal: usa la altura del viewport del iframe y permite desplazamiento */
        #divPrincipal {
            box-sizing: border-box;
            font-family: verdana;
            font-size: 8pt;
            max-height: calc(100vh - 120px); /* deja espacio para header/footer si los hubiese */
            overflow-y: auto;
            padding: 10px;
        }

        /* Si dentro hay .modal-body, mantener comportamiento de scroll */
        .modal-body {
            max-height: calc(100vh - 120px);
            overflow-y: auto;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        /* Ajustes para que tablas/grids se comporten y no corten contenido */
        .dxgv {
            table-layout: auto;
            width: 100%;
        }
    </style>
    <div class="modal-body" id="Div2">
    <div id="divPrincipal" style="font-family: verdana; font-size: 8pt" runat="server">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <div class="col-md-4">
                        Fecha de factura
                    </div>
                    <div class="col-md-8">
                        <dx:BootstrapDateEdit ID="txtFecha" runat="server">
                        </dx:BootstrapDateEdit>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <div class="col-md-4">
                        Orden de compra
                    </div>
                    <div class="col-md-8">
                        <input type="text" id="txtOrden" class="form-control" runat="server" onpaste="return false"
                            readonly="readonly" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                Los siguientes productos no tienen suficiente disponible
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <dx:BootstrapGridView ID="RadGrid1" runat="server" KeyFieldName="Id_Prd" Width="100%"
                    EnableRowsCache="false" EnableCallBacks="true">
                    <Columns>
                        <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" Caption="Código" ReadOnly="true">
                            <PropertiesTextEdit>
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="onSelectedIndexChanged" />
                            </PropertiesTextEdit>
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" ReadOnly="true" Caption="Producto">
                            <PropertiesTextEdit>
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Cantidad" Caption="Cant. capt..">
                            <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Asignado" Caption="Asignado">
                            <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_InvFinal" Caption="Inv. final">
                            <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Prd_Disponible" Caption="Disponible">
                            <PropertiesSpinEdit NumberType="Integer" NumberFormat="Number">
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents NumberChanged="onSelectedCantidadIndexChangedVI" />
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                        <dx:BootstrapGridViewDateColumn Width="80px" Visible="false">
                            <DataItemTemplate>
                                <asp:LinkButton ID="hlEquivalencias" OnClick="lnkEquivalencia_Click" runat="server">Reemplazar</asp:LinkButton>
                            </DataItemTemplate>
                        </dx:BootstrapGridViewDateColumn>
                    </Columns>
                </dx:BootstrapGridView>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label3" runat="server" Text="En los siguientes códigos no se está respetando los precios convenidos "></asp:Label>
            </div>
            <div class="col-md-12">
                <dx:BootstrapGridView ID="RadGrid2" runat="server" KeyFieldName="Id_Prd" Width="100%"
                    EnableRowsCache="false" EnableCallBacks="true">
                    <Columns>
                        <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" Caption="Código" ReadOnly="true">
                            <PropertiesTextEdit>
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="onSelectedIndexChanged" />
                            </PropertiesTextEdit>
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" ReadOnly="true" Caption="Producto">
                            <PropertiesTextEdit>
                                <ValidationSettings RequiredField-IsRequired="true">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Precio_Convenido" Caption="Precio convenido">
                            <PropertiesSpinEdit NumberType="Float" NumberFormat="Number">
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                        <dx:BootstrapGridViewSpinEditColumn FieldName="Precio_Captado" Caption="Precio captado">
                            <PropertiesSpinEdit NumberType="Float" NumberFormat="Number">
                            </PropertiesSpinEdit>
                        </dx:BootstrapGridViewSpinEditColumn>
                    </Columns>
                </dx:BootstrapGridView>
                <asp:HiddenField runat="server" id="HF_Param" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label1" runat="server" Text="¿Desea hacer alguna corrección?"></asp:Label>
            </div>
            <div class="col-md-12">
                <asp:Button ID="Button1" runat="server" Text="SI" Width="80px" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="NO" Width="80px" OnClick="Button2_Click" />
            </div>
        </div>
        </div>

          <div class="modal fade" id="ModalEquivalencias" class="modal" role="dialog" style="z-index: 2220!important;
        width: 75%; margin-left: 15%;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="col-md-10">
                    <h4 id="h3">
                       Equivalencia del producto
                    </h4>
                </div>
            </div>
            <div>
                <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 20% !Important;">
                 
                     <iframe class="embed-responsive-item" id="IframeEquivalencia" runat="server" src="">
                    </iframe>
                </div>
            </div>
        </div>
    </div>
        </div>
        <script type="text/javascript"> 

            // Ajusta altura del contenedor principal dentro del iframe al tamaño real del iframe
            function ajustarAlturaPrincipal() {
                var el = document.getElementById('divPrincipal');
                if (!el) return;
                var h = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
                // Resta 120px para headers/footers; modifica si necesitas más/menos
                el.style.maxHeight = (h - 120) + 'px';
                el.style.overflowY = 'auto';
            }
            window.addEventListener('load', ajustarAlturaPrincipal);
            window.addEventListener('resize', ajustarAlturaPrincipal);


            function AbrirEquivalencia(Id_Prd, Id_Acs, Id_Cte) {

                var Ruta = "Ventana_EquivalenciasV2.aspx?Id_Prd=" + Id_Prd + "&Id_Acs=" + Id_Acs + "&Id_Cte=" + Id_Cte;
                $('#ModalEquivalencias').modal('hide');
                document.getElementById('<%=IframeEquivalencia.ClientID%>').src = Ruta;
                $("#ModalEquivalencias").appendTo("body");
                $("#ModalEquivalencias").modal({ "backdrop": "static" });
                $('#ModalEquivalencias').modal('show');
            }
            window.closeModalInvIns = function () {
                $('#modalInventario').modal('hide');
            };
            function CloseWindow() {
                window.parent.closeModalInvIns();
            };
            window.closeModalEquivalencia = function (param) {
                $('#ModalEquivalencias').modal('hide');
                document.getElementById('<%=HF_Param.ClientID%>').value = param;
            };
        </script>
</asp:Content>