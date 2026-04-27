<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CatClienteEcommerce.aspx.cs" Inherits="SIANWEB.CatClienteEcommerce" %>

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
         function modalMensaje(mensaje) {
             document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
             $("#modalMensaje").appendTo("body");
             $("#modalMensaje").modal({ "backdrop": "static" });
             $('#modalMensaje').modal('show');
         }
         window.closeModalDetalle = function () {
             $('#Detalles').modal('hide');
         } 
         function AbrirModalDetalles() { 
            
            document.getElementById('<%=frameDetalle.ClientID%>').src = "CatClienteEcommerceInsert.aspx";
             $("#Detalles").appendTo("body");
             $("#Detalles").modal({ "backdrop": "static" });
             $('#Detalles').modal('show');
         }
         function AbrirModalDetallesUpdate(id_Cte) {
             document.getElementById('<%=frameDetalle.ClientID%>').src = "CatClienteEcommerceInsert.aspx?IDCte=" + id_Cte;
             $("#Detalles").appendTo("body");
             $("#Detalles").modal({ "backdrop": "static" });
             $('#Detalles').modal('show');
         }

         $(document).ready(function () {
             $('#Detalles').on('hidden.bs.modal', function () {
                 location.reload();
             }) 
         });
     </script>

    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" id="updpanel">
            <ContentTemplate>
                   <div class="panel panel-success" style="margin-top: 5px;">
            <div class="panel-heading" style="height: 40px;">
                <div class="col-md-10">
                    <h3 class="panel-title"></h3>
                </div>
                <div class="col-md-2" style="margin-top: -5px;">
                    <button type="submit" runat="server" class="btn btn-default" id="btnExcel"
                        onserverclick="btnExcel_ServerClick">
                        <span><i class="fa fa-file-excel-o" aria-hidden="true"></i></span>
                    </button> 
                    <button type="submit" runat="server" class="btn btn-default" id="btnInsert"
                        onserverclick="btnInsert_ServerClick">
                        <span><i class="fa fa-plus" aria-hidden="true"></i></span>
                    </button>
                </div>
            </div>
               <div class="panel-body  ">
                 
                    <div class="col-md-12" style="margin-top: 1%;">
                             <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Número de Cliente
                                    </div>
                                    <div class="col-md-6">
                                        <dx:BootstrapSpinEdit ID="TxtIdCte" runat="server" MaxLength="9">
                                            <SpinButtons Enabled="false" ShowIncrementButtons="false" />
                                        </dx:BootstrapSpinEdit>
                                    </div>
                                </div>
                              </div>
                          <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Estatus Cliente
                                    </div>
                                    <div class="col-md-6">
                                        <dx:BootstrapComboBox ID="DdlEstatus" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="T" Text="--Todos--"   />
                                                <dx:BootstrapListEditItem Value="C" Text="Capturado" Selected="true" />
                                                <dx:BootstrapListEditItem Value="A" Text="Autorizado" />
                                                <dx:BootstrapListEditItem Value="B" Text="Baja" />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                              </div>
                     
                         <div class="col-md-2">
                                        <button type="submit" class="btn btn-default btn-sm" id="btnBuscar" runat="server"
                                            onserverclick="btnBuscar_ServerClick">
                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Buscar
                                        </button>
                                    </div>

                            </div>
                     <div class="col-md-12" style="margin-top: 1%;">
                        <dx:BootstrapGridView ID="grdAdmon" ClientInstanceName="grid" runat="server" KeyFieldName="id_rik"
                            Width="100%" AutoGenerateColumns="False">
                            <Columns>
                                 <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Usu" Caption="Rik" Visible ="false">
                                </dx:BootstrapGridViewDataColumn>
                                 <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Cte" Caption="Número del cliente">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="RazonSocial" Caption="Razon Social">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Nombre" Caption="Usuario">
                                </dx:BootstrapGridViewDataColumn> 
                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Estatus" Caption="Estatus" Visible="false">
                                </dx:BootstrapGridViewDataColumn> 
                                   <dx:BootstrapGridViewDataColumn Width="50px" FieldName="EstatusStr" Caption="Estatus">
                                </dx:BootstrapGridViewDataColumn> 
                                   <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Contrasena" Caption="Pass" Visible="false">
                                </dx:BootstrapGridViewDataColumn> 
                                <dx:BootstrapGridViewDateColumn Width="40px" Caption="Editar">
                                    <DataItemTemplate>
                                        <button type="submit" class="btn btn-primary btn-sm" id="btnEditar" runat="server"
                                            onserverclick="btnEditar_ServerClick">
                                            <i class="fa fa-pencil" aria-hidden="true"></i>&nbsp;
                                        </button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewDateColumn>
                                <dx:BootstrapGridViewDateColumn Width="40px" Caption="Autorizar">
                                    <DataItemTemplate>
                                        <asp:LinkButton   CssClass="btn btn-primary btn-sm" ID="btnAutorizar" runat="server"
                                            OnClick="btnAutorizar_ServerClick">
                                            <i class="fa fa-check" aria-hidden="true"></i>&nbsp;
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewDateColumn>
                                <dx:BootstrapGridViewDateColumn Width="40px" Caption="Baja de Usuario">
                                    <DataItemTemplate>
                                     <asp:LinkButton CssClass ="btn btn-primary btn-sm" ID="btnBaja" runat="server"
                                            OnClick="btnBaja_ServerClick">
                                            <i class="fa fa-trash" aria-hidden="true"></i>&nbsp;
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewDateColumn> 

                            </Columns>
                        </dx:BootstrapGridView>
                         </div>
                    </div>
        </div>
            </ContentTemplate> 
        </asp:UpdatePanel>
      
      
               <div class="modal" id="Detalles" data-toggle="modal" style=" width: 600px; margin:auto;"
            class="modal" role="dialog" style="z-index: 2220!important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h1">Datos Cliente 
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="frameDetalle" runat="server" src=""></iframe>
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
    </div>
 

    

</asp:Content>