<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master"
    AutoEventWireup="true" CodeBehind="Ventana_BuscarV2.aspx.cs" Inherits="SIANWEB.Ventana_BuscarV2" %>

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
        
          .modal-body
        {
            max-height: calc(100%);
            overflow-y: scroll;
        }
    </style>
    <div class="modal-body" id="form1">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="form-group">
                        <div class="col-md-4">
                            <asp:Label ID="Lbl" runat="server" Text="Clave" />
                        </div>
                        <div class="col-md-8">
                            <dx:BootstrapTextBox ID="txtClave" runat="server" MaxLength="9">
                            </dx:BootstrapTextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="col-md-4">
                            <asp:Label ID="Label9" runat="server" Text="Nombre" />
                        </div>
                        <div class="col-md-8">
                            <dx:BootstrapTextBox ID="txtNombre" runat="server" MaxLength="9">
                            </dx:BootstrapTextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:ImageButton ID="btnBuscar1" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar"
                        OnClick="btnBuscar1_Click" Style="height: 16px" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="margin-top: 5px">
                <dx:BootstrapGridView ID="RadGrid1" runat="server" KeyFieldName="Id_Prd" Width="100%"
                    EnableRowsCache="false">
                    <Columns>
                        <dx:BootstrapGridViewTextColumn FieldName="IdStr" VisibleIndex="0" Caption="Clave" ReadOnly="true">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="Descripcion" VisibleIndex="1" ReadOnly="true" Caption="Descripcion">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="ValorStr" VisibleIndex="2" Visible="false" ReadOnly="true"
                            Caption="direccion">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewTextColumn FieldName="ValorDoble" VisibleIndex="3" Visible="false" ReadOnly="true"
                            Caption="Precio">
                        </dx:BootstrapGridViewTextColumn>
                        <dx:BootstrapGridViewDateColumn Width="80px" VisibleIndex="4">
                            <DataItemTemplate>
                                <button id="btnSeleccionar" type="button" class="btn btn-link" runat="server" onserverclick="btnSelecionar_Click">
                                    <span>Seleccionar</span>
                                </button>
                            </DataItemTemplate>
                        </dx:BootstrapGridViewDateColumn>
                    </Columns>
                </dx:BootstrapGridView>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function close(param) {
            window.parent.closeModal(param);
        }; 
    </script>
</asp:Content>
