 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true" CodeBehind="CapListaPreciosConsultaDet.aspx.cs" Inherits="SIANWEB.CapListaPreciosConsultaDet" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>"
        rel="stylesheet" />
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <style>
        .panel-success > .panel-heading
        {
            color: #F9F9F9 !important;
            background-color: #59b2f1 !important;
        }
        
        .panel-success
        {
            border-color: #d1d1d1 !important;
        }
        
        .caret
        {
            margin-top: 10px !important;
        }
        
        .row
        {
            margin-top: 40px;
            padding: 0 10px;
        }
        
        .clickable
        {
            cursor: pointer;
        }
        
        .panel-heading span
        {
            margin-top: -20px;
            font-size: 15px;
        }
        
        form-control
        {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }
        
        .dropdown-toggle
        {
            height: 34px !important;
        }
        
        .dropdown-toggle-date
        {
            height: 30px !important;
            margin-top: -6px;
            padding-left: 12px;
            padding-right: 10px;
            margin-right: -13px;
        }
         
        .customHeaderStyle {  
            -webkit-tap-highlight-color: rgba(0,0,0,0);
                color: #333;
                font: 12px/16px "segoe ui",arial,sans-serif;
                border-spacing: 0;
                border-collapse: collapse;
                box-sizing: border-box;
                text-decoration: none;
                text-align: center;
                background-color: #f0f0f0;
                border-right: 1px solid #caccd1!important;
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: bottom;
                border: 1px solid #ddd;
                border-bottom-width: 2px;
                border-top: 0;
        }  
        .PMLInvisible {
            display: none;
        }
        .PMLVisible {
            display: table-cell;
        }
        
        
    </style>
    <script type="text/javascript">

</script>
    <div class="container-fluid">
        <div class="row mt5">
            <div class="col-md-7 col-sm-12 mt5">
            <h1>Los siguientes productos cambiaron de precio:</h1>
            </div>
            
            <div class="col-md-2 col-sm-12 mt5">
            </div>
            <div class="col-md-2 col-sm-12 mt5">
             
            </div>
            <div class="col-md-1 col-sm-10">
                <button id="Button1" type="button" class="btn btn-primary btn-sm w100" style="margin-top: 8px!important;"
                    title="Regresar" runat="server" onserverclick="btnRegresar_ServerClick">
                    <i class="fa fa-arrow-left"></i>&nbsp;Regresar
                </button>
            </div>
        </div>
        <div class="row mt5">
            <asp:UpdatePanel runat="server" ID="updpanel2">
                <ContentTemplate>
                    <div id="Div1" class="col-md-12" runat="server" style="margin-top: 5px;">
                        <dx:BootstrapGridView ID="grdServicio" ClientInstanceName="grid" runat="server" Width="100%"
                            AutoGenerateColumns="False" Style="color: #333; font: 12px/16px 'segoe ui',arial,sans-serif;
                            border-spacing: 0;">
                            <CssClasses HeaderRow="customHeaderStyle" />
                            <SettingsBehavior EnableRowHotTrack="true" />
                            <Columns>
                                <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Center" Caption="Código Key"
                                    Width="50px" />
                                <dx:BootstrapGridViewTextColumn FieldName="Descripcion" Caption="Descripcion" Width="150px" />
                                <dx:BootstrapGridViewDateColumn FieldName="FECHAINICIOVIG" Caption="Fecha Inicio Actual"
                                    Width="100px" CssClasses-DataCell="id-fecha_pactual" CssClasses-HeaderCell="id-fecha_pactual" />
                                <dx:BootstrapGridViewTextColumn FieldName="PAAAACTUAL" Caption="PAAA ACTUAL" Width="50px">
                                    <PropertiesTextEdit DisplayFormatString="c" />
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="PLISTAACTUAL" Caption="PLISTA ACTUAL"
                                    Width="50px">
                                    <PropertiesTextEdit DisplayFormatString="c" />
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="PAAAAnterior" Caption="PAAA ANTERIOR"
                                    Width="50px" CssClasses-DataCell="id-column" CssClasses-HeaderCell="id-column">
                                    <PropertiesTextEdit DisplayFormatString="c" />
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="PLISTAANTERIOR" Caption="PLISTA ANTERIOR"
                                    Width="50px" CssClasses-DataCell="id-plista_ant" CssClasses-HeaderCell="id-plista_ant">
                                    <PropertiesTextEdit DisplayFormatString="c" />
                                </dx:BootstrapGridViewTextColumn>
                                <%-- <dx:BootstrapGridViewTextColumn FieldName="PAAAFUTURA" Caption="PAAA FUTURA" Width="50px" CssClasses-DataCell= "id-paaa_futuro" CssClasses-HeaderCell = "id-paaa_futuro"   > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PLISTAFUTURA" Caption="PLISTA FUTURA" Width="50px" CssClasses-DataCell= "id-plista_futuro" CssClasses-HeaderCell = "id-plista_futuro"   > <PropertiesTextEdit DisplayFormatString="c" /> </dx:BootstrapGridViewTextColumn>--%>
                                <dx:BootstrapGridViewTextColumn FieldName="PVariacionPAAA" Caption="% Var. PAAA"
                                    Width="50px" CssClasses-DataCell="id-porc_paaa" CssClasses-HeaderCell="id-porc_paaa">
                                    <PropertiesTextEdit DisplayFormatString="p0" />
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="PVariacionPLISTA" Caption="% Var. PLISTA"
                                    Width="50px" CssClasses-DataCell="id-porc_plista" CssClasses-HeaderCell="id-porc_plista">
                                    <PropertiesTextEdit DisplayFormatString="p0" />
                                </dx:BootstrapGridViewTextColumn>
                                <%--     <dx:BootstrapGridViewCheckColumn Caption="Tiene Precio Futuro" FieldName="TIENEPRECIOFUTURO"  Width="50px" />
                                        <dx:BootstrapGridViewDateColumn FieldName="FECHAFINVIG" Caption="Fecha inicio Precio Futuro"  Width="100px" CssClasses-DataCell= "id-fecha_pfuturo" CssClasses-HeaderCell = "id-fecha_pfuturo" />
                                        <dx:BootstrapGridViewTextColumn FieldName="RESPONSABLE" Caption="Responsable" Width="200px" CssClasses-DataCell= "id-responsable" CssClasses-HeaderCell = "id-responsable"  /> 
                                        <dx:BootstrapGridViewTextColumn FieldName="PLANEACION" Caption="Planeacion" Width="50px" CssClasses-DataCell= "id-planeacion_abasto" CssClasses-HeaderCell = "id-planeacion_abasto"  /> --%>
                            </Columns>
                            <SettingsPager PageSize="50" />
                        </dx:BootstrapGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grdServicio" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    

</asp:Content>