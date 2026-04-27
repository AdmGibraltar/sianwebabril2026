<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="CapPedidoCaptadoNuevo.aspx.cs" Inherits="SIANWEB.CapPedidoCaptadoNuevo" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
    <script  type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>"> 
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>"> 
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script> 

    <style type="text/css"> 
        .dvstyle 
        {
                height: 180px !important;
        }
        
          .dropdown-toggle
        {
            height: 34px !important;
        } 
        
        .caret
        {
            margin-top: 10px !important;
        }
        
        .dropdown-menu
        
        {
            height: 80px !important;
        }
    </style> 
                        <div class="modal-body" id="Div10">
                            <div class="col-md-12">
                            <div class="form-group">
                                    <div class="col-md-4">
                                        Razón Social Cliente
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="ddlRazonClienteNva" runat="server" EnableCallbackMode="true"  CallbackPageSize="25"
                                            DropDownStyle="DropDown" >
                                            <ClearButton DisplayMode="Always" />
                                        </dx:BootstrapComboBox>
                                    </div>

                                     <div class="col-md-12" style="margin-top: 1%">
                            <span id="lblmensaje" runat="server" style="color :Red"></span>
                        </div>
                                </div> 
                            </div> 
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-default btn-sm" id="btnconusltar" runat="server"
                                onserverclick="btnconusltar_Click">
                                <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                            </button>
                          
                        </div>
                      
     
    <script type="text/javascript">
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
        }
        function close(idCTe, guardar, modificar) {
            window.parent.closeModal(idCTe, guardar, modificar);
        };
    </script>
    </asp:Content>