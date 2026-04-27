<%@ Page Title="Ver Remisiones" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="VentanaRemisionesOC2.aspx.cs" Inherits="SIANWEB.VentanaRemisionesOC2" %>

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

    <style type="text/css">
        .modal-body {
            max-height: calc(100%);
            overflow-y: scroll;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        .centerText {
            text-align: center;
        }
    </style>

     <div class="modal-body" id="Div2">
            <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                 <div class="col-md-12">
                     <h3>Ver Remisiones</h3>
                 </div>
                <div class="col-md-12">
                    <dx:BootstrapGridView ID="rgRemisiones" runat="server" KeyFieldName="Id_Prd" Width="100%"
                        EnableRowsCache="false" EnableCallBacks="true">
                        <Columns>
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Emp" Caption="Id_Emp" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Cd" Caption="Id_Cd" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Ped" Caption="Id_Ped" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Ter" Caption="Id_Ter" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Tm" Caption="Id_Tm" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Rem" Caption="Id_Rem" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_U" Caption="Usuario" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Rem_Estatus" Caption="Rem_Estatus" Visible="false" />
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Rem" Caption="Número" />
                            <dx:BootstrapGridViewDateColumn FieldName="Rem_Fecha" Caption="Fecha">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yy}" EditFormatString="{0:dd/MM/yy}">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" Caption="Núm. cte."></dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Rem_Subtotal" Caption="Subtotal">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Rem_Iva" Caption="I.V.A.">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Rem_Total" Caption="Total">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
                </ContentTemplate>
            </asp:UpdatePanel> 
         </div> 
</asp:Content>