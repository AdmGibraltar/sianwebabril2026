<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="MonitorActividadesDetalle.aspx.cs" Inherits="SIANWEB.MonitorActividadesDetalle" %>

 

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

    <style type="text/css">
        .dvstyle {
            height: 180px !important;
        }
        .dropdown-toggle {
            height: 34px !important;
        }
        .caret {
            margin-top: 10px !important;
        }
        .dropdown-menu {
            height: 80px !important;
        }
        .modal-body {
            padding: 0px !important;
        }
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style> 

    <div class="modal-body">
        <fieldset>
            <div class="col-md-12">
                Cliente: 
                    <label id="lblCliente" runat="server" style="margin-left: 2%; vertical-align: text-top;">
                    </label>
            </div>
            <div class="col-md-12">
                Tipo: 
                    <label id="lblTipo" runat="server" style="margin-left: 0%; vertical-align: text-top;">
                    </label>
            </div>
            <div class="col-md-12">
                 <dx:bootstrapgridview id="grdCliente" clientinstancename="grid"  runat="server" keyfieldname="id_cte"
                    width="100%" autogeneratecolumns="False">
                     
                    <settingspager pagesize="5" numericbuttoncount="4">
                        <summary visible="false" />
                        <pagesizeitemsettings visible="false" showallitem="true" />
                    </settingspager>
                    <columns>
                        <dx:bootstrapgridviewtextcolumn fieldname="VtaMesTot" caption="Venta Total del mes" cssclasses-datacell="RightText" cssclasses-headercell="centerText">
                            <propertiestextedit displayformatstring="c" />
                        </dx:bootstrapgridviewtextcolumn>
                        <dx:bootstrapgridviewtextcolumn fieldname="VtaMes" caption="Venta del Mes (Acys)" cssclasses-datacell="LeftText" cssclasses-headercell="centerText">
                            <propertiestextedit displayformatstring="c" />
                        </dx:bootstrapgridviewtextcolumn>
                        <dx:bootstrapgridviewtextcolumn fieldname="VtaInst" caption="Vta Instalada (Acys)" cssclasses-datacell="LeftText" cssclasses-headercell="centerText">
                            <propertiestextedit displayformatstring="c" />
                        </dx:bootstrapgridviewtextcolumn>
                        <dx:bootstrapgridviewtextcolumn fieldname="MESVI" caption="Vta del mes vs VI" cssclasses-datacell="LeftText" cssclasses-headercell="centerText">
                            <propertiestextedit displayformatstring="c" />
                        </dx:bootstrapgridviewtextcolumn>
                    </columns>
                </dx:bootstrapgridview>
            </div>
        </fieldset>
    </div>
</asp:Content>