<%@ Page Title="Reporte GAP Rik"   Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    CodeBehind="ReporteGAPRik.aspx.cs" Inherits="SIANWEB.ReporteGAPRik" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH" runat="server">
     <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>"rel="stylesheet" />
     
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
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
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    
    
        <script type="text/javascript">
      window.closeModalalerta = function () {
            $('#ModalAlerta').modal('hide');
            }

           
            function OncmbRikChanged(cmbCountry) {
            
                /* alert(CPH_txtRik.GetValue().toString());*/
                CPH_txtRik.value = CPH_cmbRik_L.GetValue().toString();
                var textBox = document.getElementById('CPH_txtRik');
                //CPH_txtRik.PerformCallback(this, CPH_cmbRik_L.GetValue().toString())

               //CPH_txtRik.PerformCallback(this, CPH_cmbRik_L.GetValue().toString());
                //if (cmbCity.InCallback())
                //    lastCountry = cmbRik.GetValue().toString();
                //else
                //    CPH_cmbRik_L.PerformCallback(CPH_cmbRik_L.GetValue().toString());
            }
           
            function cerrarpantalla() {
                //window.history.back();
                window.open("ReporteGAPGerente.aspx", "_self");

                

            }
           

     function modalMensaje(mensaje) {
         debugger;
            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
            $("#modalmensaje").appendTo("body")
            $("#modalmensaje").modal({ "backdrop": "static" });
            $('#modalmensaje').modal('show');
     }
     function closeModalDetalle() {
         $('#modalmensaje').modal('hide');
         window.parent.closeModalDetalle();
     };
     function modalMensajeAlerta(cleanmessage) {
         alert(cleanmessage);

     }

     function CloseWindowmensaje(mensaje) {
         alert(mensaje);
         //debugger;
         //si deseo cerrar la pantalla cuando muestre el mensaje le descomentarizo las sig lineas 
         //GetRadWindow().BrowserWindow.location.reload();
         //GetRadWindow().Close();
     }

     function GetRadWindow() {
         var oWindow = null;
         if (window.radWindow)
             oWindow = window.radWindow;
         else if (window.frameElement && window.frameElement.radWindow)
             oWindow = window.frameElement.radWindow;
         return oWindow;
     }
            function onCustomButtonClick(s, e) {
                alert(s.GetRowKey(e.visibleIndex));
          window.open("ReporteGapDetalle.aspx?Id=" + s.GetRowKey(e.visibleIndex), "mywindow", "menubar=1,resizable=1,width=820,height=650");
         //AbrirVentana_VincularSuc(s.GetRowKey(e.visibleIndex));
     }

     //var oWnd = radopen("CapLeads.aspx?Id=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr, "Ventana_VincularSucursal");

     function AbrirVentana_VincularSuc(Id_Leads) {
         //debugger;
         var radWindow = radopen("ReporteGapDetalle.aspx?Id=" + Id_Leads, "Alta de nuevo Leads");
         radWindow.setSize(900, 900);
         //            radWindow.set_initialBehaviors(Telerik.Web.UI.WindowBehaviors.None);
         radWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Resize);
         radWindow.setActive(true);
         radWindow.SetModal(true);
         radWindow.center();
         radWindow.set_visibleStatusbar(false);
         radWindow.set_keepInScreenBounds(true);
         //            radWindow.set_minWidth(850);
         //            radWindow.set_minHeight(640);
         radWindow.set_destroyOnClose(true);
         //            radWindow.add_close(closeMyDialog); //after closing the RadWindow, closeMyDialog will be called
         //  radWindow.argument = args; //you can pass the value from parent page to RadWindow dialog as this line

         radWindow.show();

     }


        </script>
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

        #wizHeader li .prevStep {
            background-color: #26617f;
        }

            #wizHeader li .prevStep:after {
                border-left-color: #26617f !important;
            }

        #wizHeader li .currentStep {
            background-color: #39a5dc;
        }

            #wizHeader li .currentStep:after {
                border-left-color: #39a5dc !important;
            }

        #wizHeader li .nextStep {
            background-color: #C2C2C2;
        }

            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }

        #wizHeader {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
            margin: 0px;
            padding: 0px;
        }

            #wizHeader li {
                float: left;
            }

                #wizHeader li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 55px;
                    background: brown; /* fallback color */
                    background: hsla(34,85%,35%,1);
                    position: relative;
                    display: block;
                    float: left;
                }

                    #wizHeader li a:after {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid hsla(34,85%,35%,1);
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        left: 100%;
                        z-index: 2;
                    }

                    #wizHeader li a:before {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent;
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid white;
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        margin-left: 1px;
                        left: 100%;
                        z-index: 1;
                    }

                #wizHeader li:first-child a {
                    padding-left: 10px;
                }

                #wizHeader li:last-child {
                    padding-right: 50px;
                }

                #wizHeader li a:hover {
                    background: #FE9400;
                }

                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }

        .content {
            height: 220px;
        }

        .boxes2 {
            height: 200px;
            overflow: auto;
            width: 300px;
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

        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }

          bootstrapgridviewtextcolumn
            {
                font: 12px Helvetica, Arial, Sans-Serif;
            }
    </style>
    
    <%-- <div>
      <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
   
            </tr> 
        </table>
         </div>--%>


        <div>
            <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
            
        
            <div class="options">
                <div class="options-item">
                    <dx:ASPxComboBox runat="server" ID="ddlEditMode" AutoPostBack="true" Width="200"
                        Caption="Edit Mode" Visible ="false">
                        <RootStyle CssClass="OptionsBottomMargin" />
                    </dx:ASPxComboBox>
                </div>
            </div>
        <%--Theme="MaterialCompactOrange"--%>

           <%--https://demos.devexpress.com/ASPxGridViewDemos/GridEditing/EditModes.aspx--%>


<%--Filtros de busqueda --%>

           


           <div class="col-md-12" style="margin-top: 5px;">
                   <div class="col-md-4">
                          <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="lblAño" runat="server" Text="Año" />
                            </div>
                            <div class="col-md-8">
                               <dx:BootstrapComboBox ID="CmbAnio" runat="server" AutoPostBack="true">
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4">
                          <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="lblMes" runat="server" Text="Mes" />
                            </div>
                            <div class="col-md-8">
                              <dx:BootstrapComboBox ID="CmbMes" runat="server" AutoPostBack="true">
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>
            </div>

            <div class="col-md-12" style="margin-top: 5px;">
                    
                    <div class="col-md-4">
                          <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="Label2" runat="server" Text="Tamaño" />
                            </div>
                            <div class="col-md-8">
                               <dx:BootstrapComboBox ID="CmbTamaño" runat="server" AutoPostBack="true">
                            </dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>
                 <div class="col-md-4">
                         <div class="form-group">
                        <div class="col-md-4">
                            
                            <asp:Label ID="Label3" runat="server" Text="Representante" />
                        </div>
                        <div class="col-md-4">
                            
                             <dx:BootstrapTextBox ID="txtRik" runat="server" ClientInstanceName="txtRik"  EnableCallbackMode="true" oncustomcallback="cmbRik_CustomCallback" >
                             </dx:BootstrapTextBox> 
                                            
                        </div>
                         <div class="col-md-4">
                            <%--<dx:BootstrapComboBox ID="cmbRik" runat="server" AutoPostBack="true"  OnClientBlur="Combo_ClientBlur" onchange="CmbId_Rik_ClientSelectedIndexChanged()">--%>
                             <dx:BootstrapComboBox ID="cmbRik" runat="server"   ClientInstanceName="cmbRik" EnableCallbackMode="true" oncustomcallback="cmbRik_CustomCallback" >
                                 <ClientSideEvents SelectedIndexChanged="function(s, e) { OncmbRikChanged(s); }" />
                        </dx:BootstrapComboBox>

                    </div>
                </div>

            </div>
            

            <div class="col-md-12" style="margin-top: 5px;">
                <div class="col-md-4">
                    <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="Label7" runat="server" Text="Cliente" />
                            </div>
                            <div class="col-md-8">
                               <dx:BootstrapTextBox ID="txtCliente" runat="server"></dx:BootstrapTextBox> 
                             
                            
                             
                            </div>
                        </div>
                </div>
                <div class="col-md-4">
                         <div class="form-group">
                        <div class="col-md-4">
                            
                            <asp:Label ID="Label8" runat="server" Text="Producto" />
                        </div>
                        <div class="col-md-8">
                            
                             <dx:BootstrapTextBox ID="txtProducto" runat="server"></dx:BootstrapTextBox> 
                                            
                        </div>
                    </div>
                </div>
            </div>



          
               <div class="col-md-12" style="margin-top: 5px;">
                                       <div class="col-md-4">
                                           
                                        <div class="form-group">
                                            <%--
                                            <div class="col-md-4">
                                                <asp:Label ID="Label4" runat="server" Text="Tipo de Reporte" />
                                            </div>
                                            <div class="col-md-8">
                                                 <dx:BootstrapComboBox  ID="cmbTipoReporte" runat="server" Width="250px"   AutoPostBack="True">
                                            <Items>
                                                <dx:BootstrapListEditItem runat="server" Text="Reporte Previo" Value="-1"   />
                                                <dx:BootstrapListEditItem runat="server" Text="Generar archivos del rik" Value="0"   />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                                </div>
                                                --%>
                                           <button id="btnConsultar" type="button" class="btn btn-warning btn-sm w100" style="margin-top: 8px!important;"
                                                title="Consulta" runat="server" onserverclick="btnConsultarEx">
                                                <i class="fa fa-print"></i>
                                                    <span>Consultar</span>
                                                    </button>

                                        </div>
                                             
                                        </div>

                                        <div class="col-md-2">
                                            <button id="btnRegresar" type="button" class="btn btn-warning btn-sm w35" style="margin-top: 8px!important;"
                                                title="Regresar"   onclick="cerrarpantalla()">
                                                <i class="fa fa-arrow-left"></i> <span>&nbsp;Regresar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></button>
                                        </div>

                                     <div class="col-md-4">
                                         <div class="form-group">
                                            <div class="col-md-4">
                                                 
                                            </div>
                                            <div class="col-md-8">
                                                    <button id="btnGenerarExcel" type="button" class="btn btn-warning btn-sm w100" style="margin-top: 8px!important;"
                                                title="Consultar" runat="server" onserverclick="btnGenerarEx">
                                                <i class="fa fa-print"></i>
                                                    <span>Generar Excell</span>
                                                    </button>

                                            </div>
                                        </div>
                                       

                                     </div>
                               
                  
                                </div>
               

 


            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-12" style="margin-top: 5px;">

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="modalMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important; display: none;">
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


              <asp:UpdatePanel runat="server" ID="updpanel2">
                        <ContentTemplate>
                            <div id="Div1" class="col-md-12" runat="server" style="margin-top: 5px;">
                                <dx:bootstrapgridview id="grdServicio" clientinstancename="grid" runat="server" width="100%"
                                    autogeneratecolumns="False" style="color: #333; font: 12px/16px 'segoe ui',arial,sans-serif; border-spacing: 0;"
                                    enablecallbacks="true" keyfieldname="Id_Rik" showfilterrow="True" OnCustomSummaryCalculate="grdServicio_CustomSummaryCalculate"
                                    enablerowscache="false" onrowinserting="rg1_RowInserting" onrowupdating="rg1_RowUpdating"
                                    onrowdeleting="rg1_RowDeleting">
                                    <cssclasses headerrow="customHeaderStyle" />
                                    <settingsbehavior enablerowhottrack="true" />
                                    <settings showheaderfilterbutton="true" />
                                    <settings showfooter="True" />
                                    <columns>


                                        <dx:bootstrapgridviewtextcolumn fieldname="IdReporteGAP" caption="IdReporteGAP" width="300px" visible="false" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Id_Cd" caption="Id_Cd" width="300px" visible="false" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="NomCDI" caption="CDI" width="200px" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Id_Rik" caption="Id_Rik" width="300px" visible="true" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Nombre_Rik" caption="Representante" width="300px" visible="true" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Id_Cte" caption="Cliente" width="30px" visible="true" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Cte_NomComercial" caption="Nombre del Cliente" width="200px" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="Id_Tamaño" caption="Tamaño" width="10px" />
                                        <dx:bootstrapgridviewtextcolumn fieldname="TipoCuenta" caption="Tipo" width="10px" />
                                        <%--<dx:bootstrapgridviewtextcolumn FieldName="NomCategoria" Caption="Categoria" Width="80px" />
                            <dx:bootstrapgridviewtextcolumn FieldName="Id_Prd" Caption="No Producto" Width="30px" Visible="true" />
                            <dx:bootstrapgridviewtextcolumn FieldName="NomProducto" Caption="Descripción del Producto" Width="200px" />
                            <dx:bootstrapgridviewtextcolumn FieldName="Unidades" Caption="Unidades Vendidas" Width="30px" Visible="true" />--%>

                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="Ventas" caption="Ventas" visible="true">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="VentasPO" caption="Ventas Objetivo" visible="true">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="GAPIngresos_Monto" caption="Gap Ingresos $" visible="true" sortorder="Ascending">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="50px" fieldname="GAPIngresos_Porc" caption="Gap de Ingresos %" visible="true">
                                            <propertiestextedit displayformatstring="p2" />
                                        </dx:bootstrapgridviewtextcolumn>

                                        <%-- <dx:bootstrapgridviewtextcolumn Width="50px" FieldName="POvsPV" Caption="PO vs PV " Visible="false">
                            <propertiestextedit DisplayFormatString="p2" />
                            </dx:bootstrapgridviewtextcolumn>--%>
                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="MgRed_Monto" caption="Margen CDI $" visible="true">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="100px" fieldname="MgRedPO_Monto" caption="Margen CDI Objetivo $" visible="true">
                                            <propertiestextedit displayformatstring="c" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <%--       <dx:bootstrapgridviewtextcolumn Width="100px" FieldName="GAPMgRed_Monto" Caption="Gap Mg Red" Visible="true">
                            <propertiestextedit DisplayFormatString="c" />
                            </dx:bootstrapgridviewtextcolumn>

                            <dx:bootstrapgridviewtextcolumn Width="80px" FieldName="GAPMgRed_Porc" Caption="% Gap Mg Red" Visible="true">
                            <propertiestextedit DisplayFormatString="p2" />
                            </dx:bootstrapgridviewtextcolumn>--%>
                                        <dx:bootstrapgridviewtextcolumn width="80px" fieldname="MgRed_Porc" caption="Margen CDI %" visible="true">
                                            <propertiestextedit displayformatstring="p2" />
                                        </dx:bootstrapgridviewtextcolumn>
                                        <dx:bootstrapgridviewtextcolumn width="80px" fieldname="MgRedPO_Porc" caption="Margen CDI Objetivo %" visible="true">
                                            <propertiestextedit displayformatstring="p2" />
                                        </dx:bootstrapgridviewtextcolumn>


                                        <%--  Id_Zona, NomZona ,Id_Rik ,, Activo ,Id_Cte , ,Id_Tamaño, 
                            ,  ,   ,  ,  MgRedPO_Porc   ,    ,    ,IdUsuario  , FechaAlta  , FechaUltMod  , Mes  , Año  ,FechaInicial  , FechaFinal  ,    
                                        --%>

                                        <dx:bootstrapgridviewtextcolumn fieldname="NomEstatus" caption="Estatus" width="100px" visible="false" />
                                        <dx:bootstrapgridviewcommandcolumn caption="Editar" width="100px"  visible="false">
                                            <custombuttons>

                                                <dx:bootstrapgridviewcommandcolumncustombutton iconcssclass="fa fa-edit" id="ShowEditWindow" />

                                            </custombuttons>
                                        </dx:bootstrapgridviewcommandcolumn>



                                        <%-- <dx:BootstrapGridViewDateColumn FieldName="FECHAINICIOVIG" Caption="Fecha Inicio Actual" Width="100px" CssClasses-DataCell= "id-fecha_pactual" CssClasses-HeaderCell = "id-fecha_pactual" />--%>

                                        <%-- 

                                                                                     lista.PVariacionPAAA = Item.PVariacionPAAA;
                                                            lista.PVariacionPLISTA = Item.PVariacionPLISTA;

                                                                                     <PropertiesSpinEdit DisplayFormatString="p0" /> 
                                                                            <dx:bootstrapgridviewtextcolumn FieldName="TIENEPRECIOFUTURO" Caption="Tiene Precio Futuro" Width="50px" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "0" ? false : true  %>'  >
                                                                              </dx:bootstrapgridviewtextcolumn>
                                                                           <span class="badge badge-success" style="margin-top:4px; background-color:#2dde98;" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "1" ? true: false %>' >Si</span>
                                                                            <span class="badge badge-secondary" style="margin-top:4px;" Visible='<%# Eval(Container.DataItem, "TIENEPRECIOFUTURO").ToString() == "1" ? false : true  %>' >No</span>
    
                                                                           <dx:bootstrapgridviewtextcolumn FieldName="xxxx" Caption="Planeación Abasto" Width="50px" /> --%>
                                        <%-- lista.PVariacionPAAA = Item.PVariacionPAAA;
                                                            lista.PVariacionPLISTA = Item.PVariacionPLISTA;
                                                            lista.FECHAINICIOVIG = Item.FECHAINICIOVIG;
                                                            lista.FECHAFINVIG = Item.FECHAFINVIG;--%>
                                    </columns>
                                    <settingsdatasecurity allowinsert="false" />
                                    <clientsideevents custombuttonclick="onCustomButtonClick" />
                                    <settingsdatasecurity allowedit="true" />

                                    <settingspager pagesize="20" />

                                    <settingspopup>
                                        <editform width="500">
                                            <settingsadaptivity mode="OnWindowInnerWidth" switchatwindowinnerwidth="668" />
                                        </editform>
                                    </settingspopup>
                                    <settingssearchpanel customeditorid="SearchBox" highlightresults="True"></settingssearchpanel>



                                </dx:bootstrapgridview>

                                 <dx:aspxgridviewexporter ID="gridExport" runat="server" GridViewID="grdServicio">
                                                                        <%--OnRenderBrick = " gridExporter_RenderBrick " >--%>
                                 </dx:aspxgridviewexporter>
                               

                            </div>
      

                        </ContentTemplate>
                        <triggers>
                            <asp:asyncpostbacktrigger ControlID="grdServicio" />
                        </triggers>
                    </asp:UpdatePanel>

         </div>

     </div>
</asp:Content>

       
    
 

