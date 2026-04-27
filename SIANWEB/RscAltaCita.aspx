<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="RscAltaCita.aspx.cs" Inherits="SIANWEB.RscAltaCita" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
     <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <style type="text/css">
        .modal-body {
            height: 100px;
        }

        .nav-tabs a, .nav-tabs a:hover, .nav-tabs a:focus {
            outline: 0;
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

        #dropZone {
            padding: 20px;
            margin: -20px;
        }

        .ResultFileName {
            text-overflow: ellipsis;
        } 
 

        .margen {
            margin-left: 10px;
        }

        .RadForm_Outlook.rfdHeading h4 {
            border-bottom: solid 0px #6788be !important;
        }

        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }

        .panel-body {
            padding: 5px !important;
        }

        .dxbs-footer-row {
            background-color: lightcyan;
            font-weight: bold;
        }


        .list-group {
            height: 212px !Important;
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


        .dxbs-spin-btn-group .increment, .dxbs-spin-btn-group .increment-l {
            display: inline !important;
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
            height: 180px;
        }


        .boxes {
            height: 100px;
            overflow: auto;
            width: 300px;
        }

        .boxes2 {
            height: 200px;
            overflow: auto;
            width: 300px;
        }

        .checkbox, .radio {
            margin-top: 2px !important;
            margin-bottom: 10px !important;
        }

        .content2 {
            height: 310px;
            overflow: auto;
        }

        .dx-custom-style {
            border: dotted 3px;
        }

        .dxbs-date-edit .dropdown-menu.panel {
            margin-top: 60px;
            padding-top: 0px;
            position: fixed !important;
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

        .centerText {
            text-align: center;
        }

        .bg-primary {
            background-color: red !important;
        }

        .BlackTextClass {
            color: black;
        }

        .modal-body .cmbItemWrap span {
            word-wrap: break-word;
            white-space: normal;
        }
        
        .dropdown-menu > li > a 
        {
            white-space: pre-wrap !important;
        }
    </style>
    <script type="text/javascript">
        function onCustomDisabledDate(s, e) {
            var f = new Date();
            var f2 = new Date(e.date);
            var fecha = new Date(f.getFullYear(), f.getMonth(), f.getDate());
            var fecha2 = new Date(f2.getFullYear(), f2.getMonth(), f2.getDate());
            console.log(new Date(fecha2))
            console.log(new Date(fecha))
            if (fecha2 < fecha)
                e.isDisabled = true;
        }

        function closeModalDetalle() { 
            window.parent.closeModalDetalle();
        };
        function closeModalDetalleExito() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function modalMensajeExito(mensaje) {
            document.getElementById('<%=lblmensajeExito.ClientID%>').innerHTML = mensaje;
            $("#modalmensajeExito").appendTo("body")
            $("#modalmensajeExito").modal({ "backdrop": "static" });
            $('#modalmensajeExito').modal('show');
            closeModalDetalle();
        }
         
         function modalMensaje(mensaje) {
             document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
             $("#modalMensaje").appendTo("body")
             $("#modalMensaje").modal({ "backdrop": "static" });
             $('#modalMensaje').modal('show');
         }
    </script>

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
                    <div class="col-md-6">

                        <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Rol
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapComboBox ID="DllRol" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllRol_SelectedIndexChanged1" AutoPostBack="true">
                                    </dx:BootstrapComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Usuario
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapComboBox ID="DllUSuario" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllUSuario_SelectedIndexChanged1" AutoPostBack="true">
                                    </dx:BootstrapComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Cliente
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapComboBox ID="DllCLiente" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllCLiente_SelectedIndexChanged" AutoPostBack="true">
                                    </dx:BootstrapComboBox>
                                </div>
                            </div>
                        </div>
                          <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Bracket
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapTextBox ID="txtBracket" runat="server" Enabled="false" ReadOnly="true">
                                    </dx:BootstrapTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Actividad General   
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapComboBox ID="DllActividadGeneral" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" AutoPostBack="true" OnSelectedIndexChanged="DllActividadGeneral_SelectedIndexChanged">
                                    </dx:BootstrapComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 1%;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    Actividad
                                </div>
                                <div class="col-md-8">
                                    <dx:BootstrapComboBox ID="DllActividad" runat="server" CallbackPageSize="25" DropDownStyle="DropDown"> 
                                    </dx:BootstrapComboBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-6">
                        <fieldset>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Fecha Inicio
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="Fecha" EditFormat="Date" AutoPostBack="true" OnDateChanged="Fecha_DateChanged">
                                            <TimeSectionProperties Visible="false" />
                                               <ClientSideEvents CalendarCustomDisabledDate="onCustomDisabledDate" />
                                        </dx:BootstrapDateEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Horario
                                    </div>
                                    <div class="col-md-4">
                                        <dx:BootstrapTimeEdit runat="server" ClientInstanceName="horarioInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="HorarioInicial">
                                        </dx:BootstrapTimeEdit>
                                    </div>
                                    <div class="col-md-4">
                                        <dx:BootstrapTimeEdit runat="server" ClientInstanceName="horarioFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="HorarioFinal">
                                        </dx:BootstrapTimeEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Fecha Final
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="dllFechaFinal" EditFormat="Date">
                                            <TimeSectionProperties Visible="false" />
                                               <ClientSideEvents CalendarCustomDisabledDate="onCustomDisabledDate" />
                                        </dx:BootstrapDateEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapCheckBox runat="server" Text=" Repetir Evento" ID="chkRecurrente" OnCheckedChanged="chkRecurrente_CheckedChanged" AutoPostBack="true"></dx:BootstrapCheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        Frecuencia
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="DdlFrecuencia" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged" AutoPostBack="true" Enabled="false" ReadOnly="true">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="0" Text="Ninguno" Selected="true" />
                                                <dx:BootstrapListEditItem Value="1" Text="Diariamente" />
                                                <dx:BootstrapListEditItem Value="2" Text="Semanal" />
                                                <dx:BootstrapListEditItem Value="3" Text="Mensual" />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="form-group" id="idDia" runat="server" visible="false">
                                    <div class="col-md-4">
                                        Día
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="ddlDia" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="1" Text="1 Día" Selected="true" />
                                                <dx:BootstrapListEditItem Value="2" Text="2 Días" />
                                                <dx:BootstrapListEditItem Value="3" Text="3 Días" />
                                                <dx:BootstrapListEditItem Value="4" Text="4 Días" />
                                                <dx:BootstrapListEditItem Value="5" Text="5 Días" />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                                <div class="form-group" id="idSemana" runat="server" visible="false">
                                    <div class="col-md-4">
                                        Semana
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="ddlSemana" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="7" Text=" 1 Semana" Selected="true" />
                                                <dx:BootstrapListEditItem Value="14" Text="2 Semanas" />
                                                <dx:BootstrapListEditItem Value="21" Text="3 Semanas" />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                                <div class="form-group" id="idMes" runat="server" visible="false">
                                    <div class="col-md-4">
                                        Mes
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="ddlMes" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="1" Text="1 Mes" Selected="true" />
                                                <dx:BootstrapListEditItem Value="2" Text="2 Meses" />
                                                <dx:BootstrapListEditItem Value="3" Text="3 Meses" />
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 1%;">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-9">
                        <dx:BootstrapMemo runat="server" ID="txtObservaciones" NullText="Observaciones . . ." MaxLength="200" Rows="5">
                        </dx:BootstrapMemo>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 10px;">
                    <div class="col-md-8">
                    </div>
                    <div class="col-md-4">
                        <button id="BtnGuardar" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnGuardar_ServerClick">
                            <i aria-hidden="true"></i>&nbsp;Guardar
                        </button>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div> 
      <div id="modalMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="height: 240px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" data-dismiss="modal" id="Button9">
                            Ok</button>
                    </div>
                </div>
            </div>
        </div>
      </div>
    <div id="modalmensajeExito" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="height: 240px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensajeExito" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12 ">
                        <button class="btn btn-default" id="Button9" onclick="closeModalDetalleExito()">
                            Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
