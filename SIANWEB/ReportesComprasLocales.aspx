<%@ Page Title="Reportes Compras Locales" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master" 
    AutoEventWireup="true" CodeBehind="ReportesComprasLocales.aspx.cs" Inherits="SIANWEB.ReportesCompasLocales" EnableEventValidation="false"%>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
   
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-colorschemes.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.PieceLabel.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    
    <script type="text/javascript">
        function ModalUpload() {
            document.getElementById('<%=iFrameCargar.ClientID%>').src = "CargarArchivos.aspx?tipo=1"
            $('#ModalUpload').modal('hide');
            $("#ModalUpload").appendTo("body");
            $("#ModalUpload").modal({ backdrop: 'static', keyboard: false });
            $('#ModalUpload').modal('show');
        }
        function ModalUploadMultiplicador() {
            document.getElementById('<%=iFrameCargar.ClientID%>').src = "CargarArchivos.aspx?tipo=2"
            $('#ModalUpload').modal('hide');
            $("#ModalUpload").appendTo("body");
            $("#ModalUpload").modal({ backdrop: 'static', keyboard: false });
            $('#ModalUpload').modal('show');
        }
        function modalQuestion(mensaje) {
            document.getElementById('<%=lblquestion.ClientID%>').innerHTML = mensaje;
            $("#modalQuestion").appendTo("body")
            $("#modalQuestion").modal({ "backdrop": "static" });
            $('#modalQuestion').modal('show');
        }
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            }
        })
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

    <%--<dx:ASPxGlobalEvents ID="ASPxGlobalEvents" runat="server">
            <ClientSideEvents ControlsInitialized="function(s, e) {
                debugger;
                var baseGVPagerOnClick = ASPx.GVPagerOnClick;
                ASPx.GVPagerOnClick = function (name, value) {
                    hiddenField.Set('showDropDownAfterPostBack', true)
                    baseGVPagerOnClick.call(null, name, value)
                }
            }" />
         </dx:ASPxGlobalEvents>--%>
    

    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
         </asp:UpdateProgress>
        <div class="col-md-12">
            <h2 style="font-weight: bolder">Reportes de Compras Locales</h2>
        </div>
        <ul class="nav nav-tabs" id="tabPage">
            <li class="active"><a href="#tabDatos" data-toggle="tab">
                <h5>Generar Resultados</h5>
            </a>
            </li>
            <li><a href="#tabInfo" data-toggle="tab">
                <h5>Descargar Información</h5>
            </a></li>
        </ul>
        <div class="tab-content">
            <%--TAB Datos generales --%>
            <div class="tab-pane fade in active" id="tabDatos">
                <%--1.- Resultado Compras Locales por Motivo --%>
                <div class="row" style="margin-top: 15px;">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales por Motivo
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                                     <asp:UpdatePanel runat="server" ID="updCLpormotivo" UpdateMode="Conditional">
                                      <ContentTemplate>
                                      <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrupoM" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbGrupoM" ClientInstanceName="GrupoM" runat="server" AutoPostBack="true" >
                                                    </dx:bootstrapcombobox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label4" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalM" ClientInstanceName="SucursalM" runat="server">
                                                    </dx:BootstrapComboBox>
                                                     <%--<dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hiddenField" />
                                                 </div>
                                                 <div>
                                                    <dx:ASPxGridLookup ID="cmbSucursales" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Sucursal">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label12" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialM" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit1" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinM" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalM" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit2" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoCL" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbMotivoM" ClientInstanceName="MotivoM" runat="server">
                                                    </dx:BootstrapComboBox>   
                                               <%--   <dx:ASPxGridLookup ID="cmbMotivoRBM" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" heigth="125px">

                                                     <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                        <dx:GridViewDataColumn FieldName="Id" Width="50px" />
                                                        <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px">
                                                               <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <GridViewProperties>
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                </dx:ASPxGridLookup>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                                <dx:BootstrapRadioButtonList ID="RblMostrar" ClientInstanceName="MostrarM" runat="server" RepeatColumns="1">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                        <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                    </Items>
                                                </dx:BootstrapRadioButtonList>
                                            </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLMotivo" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLMotivo()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>                                        
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <canvas id="myChartMPastel" width="700" height="200"></canvas>
                                    </div>  

                                    <div class="col-md-8">
                                        <canvas id="myChartMBarras" width="700" height="200"></canvas>
                                    </div> 
                                    <div>
                                          <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetalleM" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                       <th style="background-color:deepskyblue; color:white"></th>
                                                       <th colspan="2" style="background-color:deepskyblue; color:white">Activación de código por falta de producto</th>
                                                       <th colspan="2" style="background-color:deepskyblue; color:white">Código central con abasto local</th>
                                                       <th colspan="2" style="background-color:deepskyblue; color:white">Solicitud del cliente</th>
                                                       <th style="background-color:deepskyblue; color:white"></th>
                                                   </tr>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Cantidad</th>
                                                     
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Cantidad</th>
                                                     
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Cantidad</th>

                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultM"> 
       
                                                 </tbody>
                                               </table>
                                        </div>
                                    </div>

                               </ContentTemplate>
                             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnCLMotivo" />
                              </Triggers>
                            </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                <%--2.- Resultado Compras Locales por Sucursal --%>
                <div class="row" style="margin-top: 15px;">
                  <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">Resultado Compras Locales por Sucursal
                                </h3>
                                <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                </span>
                            </div>
                            <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="updCLporsucursal" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <div class="col-md-4">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblGrupoS" runat="server" Text="Grupo" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbGrupos" ClientInstanceName="GrupoS" runat="server" AutoPostBack="true" >
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblSucursalS" runat="server" Text="Sucursal" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbSucursals" ClientInstanceName="SucursalS" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblFechaIniS" runat="server" Text="Fecha Inicial" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialS" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit3" PickerType="Months">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblFEchaFinS" runat="server" Text="Fecha Final" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalS" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit4" PickerType="Months">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="row" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblMotivoCLS" runat="server" Text="Motivo de Compra Local" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbMotivoss" ClientInstanceName="MotivoS" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                            <dx:BootstrapRadioButtonList ID="RblMostrarS" ClientInstanceName="MostrarS" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                     </div>
                                    <div class="row" style="margin-top: 5px">
                                        <div class="col-md-2">
                                            <button id="BtnCLSucursal" type="button" class="btn btn-primary" title="Consultar"
                                                runat="server" onclick="BtnCLSucursal()">
                                                <span>Consultar</span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                     <div class="col-md-8">
                                        <canvas id="myChartSPastel" width="700" height="200"></canvas>
                                    </div>  
                                     <div class="col-md-8">
                                        <canvas id="myChartSBarras" width="700" height="200"></canvas>
                                    </div>
                                     <div>
                                          <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetalleS" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">Venta Total</th>
                                                     <th style="background-color:deepskyblue; color:white">Unidades</th>
                                                     <th style="background-color:deepskyblue; color:white">Núm. Proveedores Locales</th>
                                                     <th style="background-color:deepskyblue; color:white">Núm. Códigos de Compra Local</th>
                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultS"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                                    </div>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnCLSucursal" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                </div>

                <%--3.- Resultado Compras Locales por Tipo de Producto --%>
                <div class="row" style="margin-top: 15px;">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales por Tipo de Producto
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                               <asp:UpdatePanel runat="server" ID="updCLporTipoProducto" UpdateMode="Conditional">
                               <ContentTemplate>
                                      <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrupoTP" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbGrupoTP" ClientInstanceName="GrupoTP" runat="server" AutoPostBack="true" >
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label2" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalTP" ClientInstanceName="SucursalTP" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaInicialTP" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialTP" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit5" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinTP" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalTP" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit6" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoTP" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbMotivoTP" ClientInstanceName="MotivoTP" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblTipoProductoTP" runat="server" Text="Tipo de Producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTipoProducto" ClientInstanceName="TipoProductoTP" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-md-12">
                                            <dx:BootstrapRadioButtonList ID="rbtMostrarTP" ClientInstanceName="MostrarTP" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                     </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLTipoProducto" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLTipoProducto()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                         <div class="col-md-8">
                                        <canvas id="myChartTPPastel" width="700" height="200"></canvas>
                                    </div>  
                                     <div class="col-md-8">
                                        <canvas id="myChartTPBarras" width="700" height="200"></canvas>
                                    </div>  
                                     <div>
                                          <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetalleP" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">1. Quimicos</th>
                                                     <th style="background-color:deepskyblue; color:white">2. Otros</th>
                                                       <th style="background-color:deepskyblue; color:white">3. Papel</th>
                                                     <th style="background-color:deepskyblue; color:white">4. Suplementos</th>
                                                     <th style="background-color:deepskyblue; color:white">5. Dosif/Desp</th>
                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultTP"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                                    </div>
                               </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnCLTipoProducto" />
                    </Triggers>
                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                <%--4.- Resultado Compras Locales por Proveedor Local --%>
                <div class="row" style="margin-top: 15px;">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales por Proveedor Local
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                               <asp:UpdatePanel runat="server" ID="updCLporProvLocal" UpdateMode="Conditional">
                               <ContentTemplate>
                                      <div class="col-md-4">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label1" runat="server" Text="Grupo" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbGrupoPL" ClientInstanceName="GrupoPL" runat="server" AutoPostBack="true" >
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblSucursalPL" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalPL" ClientInstanceName="SucursalPL" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFEchaInicialPL" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialPL" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit7" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinPL" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalPL" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit8" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoPL" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbMotivoPL" ClientInstanceName="MotivoPL" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label3" runat="server" Text="Proveedor Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbProveedorLocalPL" ClientInstanceName="ProveedorLocalPL" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label5" runat="server" Text="Tipo de Producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTipoProductoPL" ClientInstanceName="TipoProductoPL" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" Text="Codigo de Producto Central" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbProdutoCentralPL" ClientInstanceName="ProductoCentralPL" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-md-12">
                                            <dx:BootstrapRadioButtonList ID="rblMostrarPL" ClientInstanceName="MostrarPL" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                         </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLProvLocal" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLProvLocal()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                         <div class="col-md-8">
                                        <canvas id="myChartPLPastel" width="700" height="200"></canvas>
                                    </div>  
                                     <div class="col-md-8">
                                        <canvas id="myChartPLBarras" width="700" height="200"></canvas>
                                    </div> 
                                     <div>
                                          <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetallePL" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">Proveedor</th>
                                                     <th style="background-color:deepskyblue; color:white">Nombre Proveedor</th>
                                                     <th style="background-color:deepskyblue; color:white">Unidades</th>
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultPL"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                                    </div>

                               </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnCLProvLocal" />
                    </Triggers>
                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                <%--5.- Resultado Compras Locales por Proveedor Central --%>
                <div class="row" style="margin-top: 15px;">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales por Proveedor Central
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                               <asp:UpdatePanel runat="server" ID="updCLporProvCentral" UpdateMode="Conditional">
                               <ContentTemplate>
                                      <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label13" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbGrupoPC" ClientInstanceName="GrupoPC" runat="server" AutoPostBack="true" >
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label14" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalPC" ClientInstanceName="SucursalPC" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaInicialPC" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialPC" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit9" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinalPC" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalPC" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit10" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoPC" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox style="width:250px;" ID="cmbMotivoPC" ClientInstanceName="MotivoPC" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label8" runat="server" Text="Proveedor Central" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox  style="width:250px;" ID="cmbProveedorCentralPC" ClientInstanceName="ProveedorCentralPC" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblTipoProductoPC" runat="server" Text="Tipo de Producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTipoProductoPC" ClientInstanceName="TipoProductoPC" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label9" runat="server" Text="Código de Producto Central" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbProductoCentralPC" ClientInstanceName="ProductoCentralPC" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="col-md-12">
                                            <dx:BootstrapRadioButtonList ID="rblMostrarPC" ClientInstanceName="MostrarPC" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                         </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLProvCentral" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLProvCentral()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                         <div class="col-md-8">
                                        <canvas id="myChartPCPastel" width="700" height="200"></canvas>
                                    </div>  
                                     <div class="col-md-8">
                                        <canvas id="myChartPCBarras" width="700" height="200"></canvas>
                                    </div> 
                                    <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetallePC" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">Proveedor Central </th>
                                                     <th style="background-color:deepskyblue; color:white">Nombre Proveedor</th>
                                                     <th style="background-color:deepskyblue; color:white">Cantidad</th>
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultPC"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                               </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnCLProvCentral" />
                    </Triggers>
                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                <%--6.- Resultado Compras Locales por Aplicación --%>
                <div class="row" style="margin-top: 5px;">
                   <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales por Aplicación
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                               <asp:UpdatePanel runat="server" ID="updCLAplicacion" UpdateMode="Conditional">
                               <ContentTemplate>
                                      <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrupoA" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbGrupoA" ClientInstanceName="GrupoA" runat="server" AutoPostBack="true" >
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblSucursalA" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalA" ClientInstanceName="SucursalA" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaInicialA" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialA" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit11" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinalA" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalA" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="ASPxDateEdit12" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoA" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbMotivoA" ClientInstanceName="MotivoA" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label10" runat="server" Text="Tipo de Producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTipoProductoA" ClientInstanceName="TipoProductoA" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label11" runat="server" Text="Aplicación de Producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbAplicacionProductoA" ClientInstanceName="AplicacionProductoA" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <dx:BootstrapRadioButtonList ID="rblMostrarA" ClientInstanceName="MostrarA" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="0" Text="Monto" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="1" Text="Cantidad(Unidades)"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                         </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLAplicacion" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLAplicacion()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                         <div class="col-md-8">
                                        <canvas id="myChartAPastel" width="700" height="200"></canvas>
                                    </div>  
                                         <div class="col-md-8">
                                        <canvas id="myChartABarras" width="700" height="200"></canvas>
                                    </div> 
                                         <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetalleA" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">Aplicación</th>
                                                     <th style="background-color:deepskyblue; color:white">Nombre</th>
                                                     <th style="background-color:deepskyblue; color:white">Cantidad</th>
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                     <th style="background-color:deepskyblue; color:white">Porcentaje</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultA"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                               </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnCLSucursal" />
                        </Triggers>
                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                <%--7.- Resultado Compras Locales Diferentes en Precio AAA --%>
                <div class="row" style="margin-top: 5px;">
                   <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Resultado Compras Locales  Diferentes en Precio AAA
                                    </h3>
                                    <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                                    </span>
                                </div>
                                <div class="panel-body panel-collapsed" style="display: none;">
                                <asp:UpdatePanel runat="server" ID="UpdCLAAA"  UpdateMode="Conditional">
                               <ContentTemplate>
                                      <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label23" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbGrupoAAA" ClientInstanceName="GrupoAAA" runat="server" AutoPostBack="true" >
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label24" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbSucursalAAA" ClientInstanceName="SucursalAAA" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaIniAAA" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialAAA" ButtonStyle-CssClass=" btn  dropdown-toggle-date" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinAAA" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalAAA" ButtonStyle-CssClass=" btn  dropdown-toggle-date" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoAAA" runat="server" Text="Motivo de Compra Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbMotivoAAA" ClientInstanceName="MotivoAAA" runat="server">
                                                       <Items>
                                                            <dx:BootstrapListEditItem Text="Todos" Value="0" />
                                                            <dx:BootstrapListEditItem Text="Activación de código por falta de producto" Value="1" />
                                                            <dx:BootstrapListEditItem Text="Código central con abasto local" Value="2" />
                                                        </Items>
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                      
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblPrecioAAA" runat="server" Text="Tipo de Precio AAA" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTipoPrecioAAA" ClientInstanceName="TipoPrecioAAA" runat="server">
                                                        <Items>
                                                            <dx:BootstrapListEditItem Text="Todos" Value="0" />
                                                            <dx:BootstrapListEditItem Text="Precio Mayor a AAA" Value="1" />
                                                            <dx:BootstrapListEditItem Text="Precio Menor a AAA" Value="2" />
                                                        </Items>
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="BtnCLPrecioAAA" type="button" class="btn btn-primary" title="Consultar"
                                                    runat="server" onclick="BtnCLPrecioAAA()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                         <div class="col-md-8">
                                        <canvas id="myChartAAAPastel" width="700" height="200"></canvas>
                                    </div>  
                                     <%--    <div class="col-md-8">
                                        <canvas id="myChartAAABarras" width="700" height="200"></canvas>
                                    </div> --%>
                                         <div class="col-md-12" style="margin-top: 5px;">
                                             <table id="DetalleAAA" border ="1" class="display" style="width:100%">
                                                 <thead>
                                                   <tr>
                                                     <th style="background-color:deepskyblue; color:white">Sucursal</th>               
                                                     <th style="background-color:deepskyblue; color:white">Producto</th>
                                                     <th style="background-color:deepskyblue; color:white">Nombre Producto</th>
                                                     <th style="background-color:deepskyblue; color:white">Tipo de AAA</th>
                                                     <th style="background-color:deepskyblue; color:white">Precio AAA CL</th>
                                                     <th style="background-color:deepskyblue; color:white">Pecio AAA KEY</th>
                                                     <th style="background-color:deepskyblue; color:white">Unidades</th>
                                                     <th style="background-color:deepskyblue; color:white">Unidad</th>
                                                     <th style="background-color:deepskyblue; color:white">Venta</th>
                                                   </tr>
                                                 </thead>
                                                  <tbody id="DataResultAAA"> 
       
                                                 </tbody>
                                               </table>

                                        </div>
                               </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnCLPrecioAAA" />
                                </Triggers>
                             </asp:UpdatePanel>
                                <%--  <div class="col-md-12" style="margin-top: 5px;">
                                            <button id="BtnCLPrecioAAA" type="button" class="btn btn-primary" title="Consultar"
                                            runat="server" onserverclick="BtnExcelCLPrecioAAA_ServerClick">
                                            <span>Excel</span>
                                        </button>
                                    </div>
                                </div>--%>
                   </div> 
                </div> 
            
             </div>
            </div>
            <%--TAB Descargar informacion --%>
            <div class="tab-pane fade" id="tabInfo">
                <div class="row" style="margin-top: 15px">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte Compras Locales
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="upddescargar">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                       <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrupoDes" runat="server" Text="Grupo" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbGrupoDes" runat="server" AutoPostBack="false" >
                                                    </dx:bootstrapcombobox>
                                                </div>
                                            </div>
                                        </div> 
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblSucursalDes" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbSucursalDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaIniDes" runat="server" Text="Fecha Inicio" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaInicialDescargar" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblFechaFinDes" runat="server" Text="Fecha Fin" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaFinalDescargar" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                       <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblMotivoDes" runat="server" Text="Motivo de compra local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbMotivoDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                 <%--<dx:ASPxHiddenField ID="ASPxHiddenField5" runat="server" ClientInstanceName="hiddenField" />
                                             
                                                    <dx:ASPxGridLookup ID="cmbMotivoCompraRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Motivo Compra">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblTipoProductoDes" runat="server" Text="Tipo de producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbTipoProductoDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                 <%--<dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hiddenField" />
                                                 
                                                    <dx:ASPxGridLookup ID="cmbTipoProductoRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Tipo Producto">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                       <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblAplicacionDes" runat="server" Text="Aplicación de producto" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbAplicacionDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                    <%--<dx:ASPxHiddenField ID="ASPxHiddenField6" runat="server" ClientInstanceName="hiddenField" />
                                                 
                                                    <dx:ASPxGridLookup ID="cmbAplicacionRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" OnValueChanged="GridAplicaciones_ValueChanged" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Folio"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Aplicacion">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true"/>
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblProductoCentralDes" runat="server" Text="Código de producto central" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbProductoCentralDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                    <%--<dx:ASPxHiddenField ID="ASPxHiddenField2" runat="server" ClientInstanceName="hiddenField" />
                                                    <dx:ASPxGridLookup ID="cmbProductoCentralRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="TipoProducto">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                       <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblProvedorCentralDes" runat="server" Text="Proveedor Central" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbProveedorCentralDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                    <%--<dx:ASPxHiddenField ID="ASPxHiddenField3" runat="server" ClientInstanceName="hiddenField" />
                                                
                                                    <dx:ASPxGridLookup ID="cmbProveedorCentralRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Proveedor Central">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblProvedorLocalDes" runat="server" Text="Proveedor Local" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:bootstrapcombobox ID="cmbProveedorLocalDes" runat="server">
                                                    </dx:bootstrapcombobox>
                                                </div>
                                                   <%--<dx:ASPxHiddenField ID="ASPxHiddenField4" runat="server" ClientInstanceName="hiddenField" />

                                                    <dx:ASPxGridLookup ID="cmbProveedorLocalRep" runat="server" SelectionMode="Multiple" KeyFieldName="Id" Width="275px" NullText="SELECCIONAR" AutoGenerateColumns="False" AutoPostBack="true" >                                                   
                                                          <ClientSideEvents Init="function(s) {
                                                                if (hiddenField.Get('showDropDownAfterPostBack')) {
                                                                    hiddenField.Set('showDropDownAfterPostBack', false)
                                                                    s.ShowDropDownArea();
                                                                }
                                                            }" 
                                                             />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                            <dx:GridViewDataColumn FieldName="Id" Width="50px"  Caption="Num"/>
                                                            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" Caption="Proveedor Local ">
                                                                   <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                        <SettingsBehavior AllowDragDrop="False" EnableRowHotTrack="True" />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                        
                                                    </dx:ASPxGridLookup> --%>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <button id="Button5" type="button" class="btn btn-primary" title="Consultar"
                                    runat="server" onserverclick="BtnDescargar_ServerClick">
                                    <span>Excel</span>
                                </button>
                            </div>
                        </div>
                    </div>
                  </div>
              </div>
    </div>
</div>
       
    <div id="ModalUpload" data-keyboard="false" data-backdrop="static" data-toggle="modal"
        style="height: 800px !important" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-dialog modal-dialog2" role="document" style="height: 120px !important;">
            <!-- Modal content-->
            <div class="modal-content modal-content2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    Cargar Documento
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 35% !Important;">
                        <iframe class="embed-responsive-item" id="iFrameCargar" runat="server" src=""></iframe>
                    </div>
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
    
    <div id="modalQuestion" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modal_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblquestion" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
                            <button class="btn btn-default" data-dismiss="modal" id="btnSi" runat="server">
                                Sí</button>
                            <button class="btn btn-default" data-dismiss="modal" id="btnNo">
                                No</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
            $("#modalMensaje").appendTo("body");
            $("#modalMensaje").modal({ "backdrop": "static" });
            $('#modalMensaje').modal('show');
        }
        function BtnCLMotivo() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalM.GetValue();
            var Motivo = MotivoM.GetValue();
            var MostrarPor = MostrarM.GetValue();
            if (FechaInicialM.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialM.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalM.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalM.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', Mostrar: '" + MostrarPor + "'}";
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLporMotivo_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartMPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }
                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartMBarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }
                        if (id == 3)
                        {
                            var datos = data.datos2;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {
                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + '$' + datos[i].unoP + '</td>' +
                                    '<td>' + datos[i].unoC + '</td>' +
                                    '<td>' + '$' + datos[i].dosP + '</td>' +
                                    '<td>' + datos[i].dosC + '</td>' +
                                    '<td>' + '$' + datos[i].tresP + '</td>' +
                                    '<td>' + datos[i].tresC + '</td>' +
                                    '<td>' + datos[i].Porcentaje + '</td>' +
                                    '</tr>';
                            }
                        $('#DataResultM').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

        function BtnCLSucursal() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalS.GetValue();
            var Motivo = MotivoS.GetValue();
            var MostrarPor = MostrarS.GetValue();
            if (FechaInicialS.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
                 }
            var jsDate = FechaInicialS.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalS.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                   modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                   return;
               }
            var jsDate2 = FechaFinalS.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', Mostrar: '" + MostrarPor + "'}";
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLporSucursal_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartSPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }
                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartSBarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }

                        if (id == 3) {
                            var datos = data.datos2;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {
                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + '$' + datos[i].Pesos + '</td>' +
                                    '<td>' + datos[i].Unidades + '</td>' +
                                    '<td>' + datos[i].TotalProveedores + '</td>' +
                                    '<td>' + datos[i].TotalProductos + '</td>' +
                                    '<td>' + datos[i].Porcentaje + '%' + '</td>' +
                                    '</tr>';
                            }
                            $('#DataResultS').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

        function BtnCLTipoProducto() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalTP.GetValue();
            var Motivo = MotivoTP.GetValue();
            var TipoProducto = TipoProductoTP.GetValue();
            var MostrarPor = MostrarTP.GetValue();
            if (FechaInicialTP.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialTP.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalTP.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalTP.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', TipoProducto: '" + TipoProducto + "', Mostrar: '" + MostrarPor + "'}";
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLTipoProducto_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                   $("#modalMensaje").appendTo("body");
                   $("#modalMensaje").modal({ "backdrop": "static" });
                   $('#modalMensaje').modal('show');
               },
               success: function (response) {
                   if (response != null && response.d != null) {
                       var data = response.d;
                       data = $.parseJSON(data);
                       var id = data.id;
                       if (id == -1) {
                           $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartTPPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }
                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartTPBarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                       }
                        if (id == 3) {
                           var datos = data.datos2;
                           var i;
                           var html = '';
                           for (i = 0; i < datos.length; i++) {
                               html += '<tr>' +
                                   '<td>' + datos[i].Cd_Nombre + '</td>' +
                                   '<td>' + '$' + datos[i].Quimicos + '</td>' +
                                   '<td>' + '$' + datos[i].Otros + '</td>' +
                                   '<td>' + '$' + datos[i].Papel + '</td>' +
                                   '<td>' + '$' + datos[i].Dosif + '</td>' +
                                   '<td>' + '$' + datos[i].Suplementos + '</td>' +
                                   '<td>' + datos[i].Porcentaje + '</td>' +
                                   '</tr>';
                           }
                            $('#DataResultTP').html(html);
                       }
                    }
                },
                complete: function (response) {
                   $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
               },
           });
        }

        function BtnCLProvLocal() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalTP.GetValue();
            var Motivo = MotivoTP.GetValue();
            var ProveedorLocal = ProveedorLocalPL.GetValue();
            var TipoProducto = TipoProductoPL.GetValue();
            var ProductoCentral = ProductoCentralPL.GetValue();
            var MostrarPor = MostrarPL.GetValue();

            if (FechaInicialPL.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialPL.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalPL.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalPL.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', ProveedorLocal: '" + ProveedorLocal + "', TipoProducto: '" + TipoProducto + "', ProductoCentral: '" + ProductoCentral + "', Mostrar: '" + MostrarPor + "'}";
            debugger;
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLProvLocal_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartPLPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartPLBarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }

                        if (id == 3)
                        {
                            debugger;
                            var datos = data.datos2;
                            var total = data.total;
                            var unidades = data.unidades;
                            var Porcentaje = 0;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {
                                Porcentaje = 0;
                                if (unidades == 1)
                                     { Porcentaje = ((datos[i].Unidades * 100) / total).toFixed(2) }
                                else { Porcentaje = ((datos[i].Pesos * 100) / total).toFixed(2) }

                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + datos[i].IdProveedorLocal + '</td>' +
                                    '<td>' + datos[i].ProveedorLocal + '</td>' +
                                    '<td>' + datos[i].Unidades + '</td>' +
                                    '<td>' + '$ ' + datos[i].Pesos + '</td>' +
                                    '<td> ' + Porcentaje + ' %' + '</td>' +
                                    '</tr>';
                            }
                            $('#DataResultPL').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

        function BtnCLProvCentral() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalPC.GetValue();
            var Motivo = MotivoPC.GetValue();
            var ProveedorCentral = ProveedorCentralPC.GetValue();
            var TipoProducto = TipoProductoPC.GetValue();
            var ProductoCentral = ProductoCentralPC.GetValue();
            var MostrarPor = MostrarPC.GetValue();
            if (FechaInicialPC.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialPC.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalPC.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalPC.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', ProveedorCentral: '" + ProveedorCentral + "', TipoProducto: '" + TipoProducto + "', ProductoCentral: '" + ProductoCentral + "', Mostrar: '" + MostrarPor + "'}";
            debugger;
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLProvCentral_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartPCPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }
                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartPCBarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }
                        if (id == 3) {
                            var datos = data.datos2;
                            var Porcentaje = 0;
                            var unidades = data.unidades;
                            var total = data.total;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {

                                Porcentaje = 0;
                                if (unidades == 1) { Porcentaje = ((datos[i].Unidades * 100) / total).toFixed(2) }
                                else { Porcentaje = ((datos[i].Pesos * 100) / total).toFixed(2) }

                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + datos[i].IdProveedorCentral + '</td>' +
                                    '<td>' + datos[i].ProveedorCentral + '</td>' +
                                    '<td>' + datos[i].Unidades + '</td>' +
                                    '<td>' + '$ ' + datos[i].Pesos + '</td>' +
                                    '<td> ' + Porcentaje + ' %' + '</td>' +
                                    '</tr>';
                            }
                            $('#DataResultPC').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

        function BtnCLAplicacion() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalA.GetValue();
            var Motivo = MotivoA.GetValue();
            var TipoProducto = TipoProductoA.GetValue();
            var Aplicacion = AplicacionProductoA.GetValue();
            var MostrarPor = MostrarA.GetValue();
            if (FechaInicialA.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialA.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalA.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalA.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', TipoProducto: '" + TipoProducto + "', Aplicacion: '" + Aplicacion + "', Mostrar: '" + MostrarPor + "'}";
            debugger;
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/btnCLAplicacion_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                var Pesos = data
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartAPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart5;
                            var ctx = document.getElementById('myChartABarras');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    if (MostrarPor == 0) {
                                                        return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    } else {
                                                        return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                    }
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }

                        if (id == 3) {
                            var datos = data.datos2;
                            var total = data.total;
                            var unidades = data.unidades;
                            var Porcentaje = 0;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {

                                Porcentaje = 0;
                                if (unidades == 1) { Porcentaje = ((datos[i].Unidades * 100) / total).toFixed(2) }
                                else { Porcentaje = ((datos[i].Pesos * 100) / total).toFixed(2) }

                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + datos[i].IdAplicacion + '</td>' +
                                    '<td>' + datos[i].Aplicacion + '</td>' +
                                    '<td>' + datos[i].Unidades + '</td>' +
                                    '<td>' + '$ ' + datos[i].Pesos + '</td>' +
                                    '<td>' + Porcentaje  + ' %' + '</td>' +
                                    '</tr>';
                            }
                            $('#DataResultA').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

        function BtnCLPrecioAAA() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var Sucursal = SucursalAAA.GetValue();
            var Motivo = MotivoAAA.GetValue();
            var TipoPrecio = TipoPrecioAAA.GetValue();
            var MostrarPor = 0;
           
            if (FechaInicialAAA.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Inicial para generar el reporte.");
                return;
            }
            var jsDate = FechaInicialAAA.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            if (FechaFinalAAA.GetDate() == null) {
                $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                modalMensaje("Debes seleccionar una Fecha Final para generar el reporte.");
                return;
            }
            var jsDate2 = FechaFinalAAA.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Id_Cd: '" + Sucursal + "', Id_Motivo: '" + Motivo + "', TipoPrecio: '" + TipoPrecio + "'}";
            debugger;
            $.ajax({
                type: "POST",
                url: "ReportesComprasLocales.aspx/BtnCLPrecioAAA_ServerClick",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == -1){
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje("Las fechas seleccionadas deben ser menor al mes actual.");
                            return;
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };

                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                var Pesos = data
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChartAAAPastel');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                if (MostrarPor == 0) {
                                                    return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + ' Unidades';
                                                }
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            if (MostrarPor == 0) {
                                                return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            } else {
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }

                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                                coloR.push(dynamicColors());
                            });
                            //var myChart5;
                            //var ctx = document.getElementById('myChartAAABarras');
                            //if (window.myChart5 != undefined) {
                            //    window.myChart5.destroy();
                            //}
                            //window.myChart5 = new Chart(ctx, {
                            //    type: 'bar',
                            //    data: {
                            //        labels: tituloArr,
                            //        datasets: [{
                            //            data: datosArr,
                            //            borderWidth: 1
                            //        }]
                            //    },
                            //    options: {
                            //        responsive: true,
                            //        display: false,
                            //        labels: {
                            //            fontSize: 10,
                            //            display: false,
                            //        },
                            //        legend: {
                            //            display: false,
                            //        },
                            //        title: {
                            //            display: true,
                            //            text: 'Ventas',
                            //        },
                            //        scaleshowvalue: true,
                            //        scales: {
                            //            xAxes: [{
                            //                stacked: false,
                            //                beginAtZero: true,
                            //                ticks: {
                            //                    stepSize: 1,
                            //                    min: 0,
                            //                    autoSkip: false,
                            //                    beginAtZero: true
                            //                }
                            //            }],
                            //            yAxes: [{
                            //                ticks: {
                            //                    beginAtZero: true
                            //                }
                            //            }]
                            //        },
                            //        tooltips: {
                            //            callbacks: {
                            //                label: function (tooltipItem, data) {
                            //                    var indice = tooltipItem.index;
                            //                    if (MostrarPor == 0) {
                            //                        return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            //                    } else {
                            //                        return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','), + ' Unidades';
                            //                    }
                            //                }
                            //            }
                            //        },
                            //        plugins: {
                            //            colorschemes: {
                            //                scheme: 'brewer.Paired3',
                            //            },
                            //            labels: [
                            //                {
                            //                    render: function (args) {
                            //                        if (MostrarPor == 0) {
                            //                            return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            //                        } else {
                            //                            return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            //                        }
                            //                    },
                            //                    arc: false,
                            //                    fontColor: '#000',
                            //                    position: 'outside',
                            //                },
                            //            ],
                            //        },
                            //    },
                            //});
                        }

                        if (id == 3) {
                            var datos = data.datos2;
                            var i;
                            var html = '';
                            for (i = 0; i < datos.length; i++) {
                                html += '<tr>' +
                                    '<td>' + datos[i].Cd_Nombre + '</td>' +
                                    '<td>' + datos[i].Id_Prd + '</td>' +
                                    '<td>' + datos[i].Prd_Descripcion + '</td>' +
                                    '<td>' + datos[i].PrecioMayor + '</td>' +
                                    '<td> $ ' + datos[i].PrecioAAACL + '</td>' +
                                    '<td> $ ' + datos[i].PrecioAAAKey + '</td>' +
                                    '<td>' + datos[i].Unidades + '</td>' +
                                    '<td>' + datos[i].Prd_Presentacion + '</td>' +
                                    '<td> $ ' + datos[i].Pesos + '    </td>' +
                                    '</tr>';
                            }
                            $('#DataResultAAA').html(html);
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }

    </script>

    <script type="text/javascript">
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        function CloseGridLookup() {
            cmbAplicacionRep.ConfirmCurrentSelection();
            cmbAplicacionRep.HideDropDown();
            cmbAplicacionRep.Focus();
            //gridLookup.ConfirmCurrentSelection();
            //gridLookup.HideDropDown();
            //gridLookup.Focus();
        }
    </script>

</asp:Content>
