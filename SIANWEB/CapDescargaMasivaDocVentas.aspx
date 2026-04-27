<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CapDescargaMasivaDocVentas.aspx.cs" Inherits="SIANWEB.CapDescargaMasivaDocVentas" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <style type="text/css">
       .dropdown-toggle {
            height: 34px !important;
        }

        input[type="checkbox"]{
            position: static !important;
            margin-right: 5px !important;
        }

    </style>
    <script type="text/javascript"> 
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0;position: fixed;top: 50%;left: 10%;width: 50%;-ms-transform: translateY(-50%);transform: translate(40%, -50%); -webkit-box-shadow: 3px 4px 6px #999; z-index: 500;" class="alert fade in ' + cssclass + ' text-center"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>  <span>' + message + '</span></div>');
        }

        function DescargarZip(  strNameFacPdfZip  ,  strUrlFacPdfZip  ,  strNameFacXmlZip  ,  strUrlFacXmlZip,
                                strNamePagoPdfZip  ,  strUrlPagoPdfZip  ,  strNamePagoXmlZip  ,  strUrlPagoXmlZip,
                                strNameNCargoPdfZip  ,  strUrlNCargoPdfZip  ,  strNameNCargoXmlZip  ,  strUrlNCargoXmlZip,
                                strNameNCreditoPdfZip  ,  strUrlNCreditoPdfZip  ,  strNameNCreditoXmlZip  ,  strUrlNCreditoXmlZip, strMensaje  ) {
            
            $('#alert_container').html("");

            if (strNameFacPdfZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlFacPdfZip;
                anchor.target = '_blank';
                anchor.download = strNameFacPdfZip;
                anchor.click();
            }

            if (strNameFacXmlZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlFacXmlZip;
                anchor.target = '_blank';
                anchor.download = strNameFacXmlZip;
                anchor.click();
            }

            if (strNamePagoPdfZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlPagoPdfZip;
                anchor.target = '_blank';
                anchor.download = strNamePagoPdfZip;
                anchor.click();
            }

            if (strNamePagoXmlZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlPagoXmlZip;
                anchor.target = '_blank';
                anchor.download = strNamePagoXmlZip;
                anchor.click();
            }
            if (strNameNCargoPdfZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlNCargoPdfZip;
                anchor.target = '_blank';
                anchor.download = strNameNCargoPdfZip;
                anchor.click();
            }

            if (strNameNCargoXmlZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlNCargoXmlZip;
                anchor.target = '_blank';
                anchor.download = strNameNCargoXmlZip;
                anchor.click();
            }

            if (strNameNCreditoPdfZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlNCreditoPdfZip;
                anchor.target = '_blank';
                anchor.download = strNameNCreditoPdfZip;
                anchor.click();
            }

            if (strNameNCreditoXmlZip.length > 0) {
                let anchor = document.createElement('a');
                anchor.href = strUrlNCreditoXmlZip;
                anchor.target = '_blank';
                anchor.download = strNameNCreditoXmlZip;
                anchor.click();
            }

            if (strMensaje.length > 0) {
                ShowMessage(strMensaje, "Warning");
            }


        }
        
        function Cambia_Filtro_P() {
            $('#alert_container').html("");
        }

        function Cambia_Filtro(s, e) {
            $('#alert_container').html("");
        }
    </script>
    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpGeneral" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="UpGeneral" ChildrenAsTriggers="true" UpdateMode="Always">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Descarga de documentos</h3>
                            <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 2%;">
                                  <div class="col-md-3">
                                    <dx:BootstrapComboBox ID="cmbCliente" runat="server"  DropDownStyle="DropDown" j="15" />
                                      <ClientSideEvents  SelectedIndexChanged="Cambia_Filtro" />
                                    </dx:BootstrapComboBox>
                                </div>
                                <div class="col-md-2">
                                            <asp:Label ID="Label4" runat="server" Text="Período"  />
                                    <dx:ASPxCalendar ID="datePeriodo" runat="server" PickerType="Months" ShowClearButton="False" ShowTodayButton="False" />
                                     <ClientSideEvents ValueChanged="Cambia_Filtro" />
                                    </dx:BootstrapComboBox>
                                </div>
                              
                                 
                                <div class="col-md-3"  style="padding: 0px 7px !important;">
                                   
                                       
                                            Tipo de documentos
                                                                     
                                                    <dx:bootstraplistbox Width="100%" hegth ID="listBoxTipoDoc" ClientInstanceName="listBoxTipoDoc" SelectionMode="CheckColumn" runat="server" Rows="5" EnableSelectAll="true" SelectAllText="Todos" class="lstBox">
                                                      
                                                        <Items>
                                                            <dx:BootstrapListEditItem  Text="Complemento de Pago" Value="1" />
                                                            <dx:BootstrapListEditItem  Text="Factura" Value="2" />
                                                            <dx:BootstrapListEditItem  Text="Nota Cargo" Value="3" />
                                                            <dx:BootstrapListEditItem  Text="Nota Crédito" Value="4" />
                                                        </Items>  
                                                        <ClientSideEvents SelectedIndexChanged="Cambia_Filtro" />
                                                    </dx:bootstraplistbox>                                                
                                                </DropDownWindowTemplate>
                                            </dx:ASPxDropDownEdit>
                               
                                </div>
                                <div class="col-md-2"  style="padding: 0px 7px !important;">
                                   
                                            Formatos
                                       
                                                    <dx:bootstraplistbox Width="100%" ID="ListBoxFormato" ClientInstanceName="ListBoxFormato" SelectionMode="CheckColumn" runat="server"  Rows="5" EnableSelectAll="true" SelectAllText="Todos" class="lstBox" >                                                       
                                                        <Items>
                                                            <dx:BootstrapListEditItem Text="PDF" Value="1" />
                                                            <dx:BootstrapListEditItem Text="XML" Value="2" />
                                                        </Items>    
                                                        <ClientSideEvents SelectedIndexChanged="Cambia_Filtro" />
                                                    </dx:bootstraplistbox> 
                                       
                                </div>
                                
                                <div class="col-md-2">
                                    <br />
                                    <button id="BtnDescargar" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnDescargar_ServerClick">
                                        <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                    </button>
                                </div>
                            </div>
                            <div class="col-md-12" enableviewstate="false" style="margin-top: 5px;">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </div>
    <div class="messagealert" id="alert_container">
    </div>
</asp:Content>
