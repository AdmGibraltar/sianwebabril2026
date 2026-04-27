<%@ Page Title="Estadistica de ventas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="Rep_VenEstadisticaVentas.aspx.cs" Inherits="SIANWEB.Rep_VenEstadisticaVentas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <%--BRB-------------------------%>

        <script type="text/javascript">
            function inicializarSwitchFecha() {
                const toggle = document.getElementById("togglePeriodoAnio");
                const rowAnio = document.getElementById("<%= rowAnio.ClientID %>");
                const rowPeriodo = document.getElementById("<%= rowPeriodo.ClientID %>");
                const hiddenModo = document.getElementById("<%= HiddenModoFecha.ClientID %>");
                const ckbSemanal = document.getElementById("<%= ckbSemanal.ClientID %>");

                if (!toggle || !rowAnio || !rowPeriodo || !hiddenModo) return;

                const modoInicial = hiddenModo.value || "anio";
                toggle.checked = modoInicial === "anio";

                function actualizarVisibilidad() {
                    const modo = hiddenModo.value;
                    const mostrarAnio = modo === "anio";
                    rowAnio.style.display = mostrarAnio ? "" : "none";
                    rowPeriodo.style.display = mostrarAnio ? "none" : "";
                    toggle.checked = mostrarAnio;
                }

                toggle.addEventListener("change", function () {
                    if (ckbSemanal.checked) {
                        toggle.checked = true;
                        hiddenModo.value = "anio";
                    } else {
                        hiddenModo.value = toggle.checked ? "anio" : "periodo";
                    }
                    actualizarVisibilidad();
                });

                ckbSemanal.addEventListener("change", function () {
                    if (ckbSemanal.checked) {
                        hiddenModo.value = "anio";
                        toggle.checked = true;
                        actualizarVisibilidad();
                    }
                });

                actualizarVisibilidad();
            }

            function inicializarMonthPickers() {
                const monthPickerInicio = document.getElementById('monthPickerInicio');
                const monthPickerFin = document.getElementById('monthPickerFin');
                const hdnFechaInicio = document.getElementById('<%= hdnFechaInicio.ClientID %>');
                const hdnFechaFin = document.getElementById('<%= hdnFechaFin.ClientID %>');

                if (!monthPickerInicio || !monthPickerFin || !hdnFechaInicio || !hdnFechaFin) return;

                function convertirFormatoAOriginal(fechaHTML5) {
                    if (!fechaHTML5) return '';
                    const [año, mes] = fechaHTML5.split('-');
                    return `${mes}-${año}`;
                }

                function convertirFormatoAHTML5(fechaOriginal) {
                    if (!fechaOriginal) return '';
                    const [mes, año] = fechaOriginal.split('-');
                    return `${año}-${mes}`;
                }

                if (hdnFechaInicio.value) {
                    monthPickerInicio.value = convertirFormatoAHTML5(hdnFechaInicio.value);
                } else {
                    const hoy = new Date();
                    const mesActual = String(hoy.getMonth() + 1).padStart(2, '0');
                    const añoActual = hoy.getFullYear();
                    monthPickerInicio.value = `${añoActual}-${mesActual}`;
                    hdnFechaInicio.value = `${mesActual}-${añoActual}`;
                }

                if (hdnFechaFin.value) {
                    monthPickerFin.value = convertirFormatoAHTML5(hdnFechaFin.value);
                } else {
                    const hoy = new Date();
                    const mesActual = String(hoy.getMonth() + 1).padStart(2, '0');
                    const añoActual = hoy.getFullYear();
                    monthPickerFin.value = `${añoActual}-${mesActual}`;
                    hdnFechaFin.value = `${mesActual}-${añoActual}`;
                }

                monthPickerInicio.addEventListener('change', function () {
                    hdnFechaInicio.value = convertirFormatoAOriginal(this.value);
                    /*console.log('Fecha Inicio actualizada:', hdnFechaInicio.value);*/
                });

                monthPickerFin.addEventListener('change', function () {
                    hdnFechaFin.value = convertirFormatoAOriginal(this.value);
                    /*console.log('Fecha Fin actualizada:', hdnFechaFin.value);*/
                });

                function validarRango() {
                    if (!monthPickerInicio.value || !monthPickerFin.value) return true;

                    const fechaInicio = new Date(monthPickerInicio.value + '-01');
                    const fechaFin = new Date(monthPickerFin.value + '-01');

                    if (fechaInicio > fechaFin) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Rango de fechas inválido',
                            text: 'El orden de las fechas es incorrecto. Se ajustará automáticamente.',
                            confirmButtonText: 'Aceptar'
                        }).then(() => {
                            const nuevaFechaFin = new Date(fechaInicio);
                            nuevaFechaFin.setMonth(nuevaFechaFin.getMonth() + 1);

                            const año = nuevaFechaFin.getFullYear();
                            const mes = (nuevaFechaFin.getMonth() + 1).toString().padStart(2, '0');

                            monthPickerFin.value = `${año}-${mes}`;
                            hdnFechaFin.value = convertirFormatoAOriginal(`${año}-${mes}`);

                        });

                        return false;
                    }

                    return true;
                }

                monthPickerInicio.addEventListener('change', validarRango);
                monthPickerFin.addEventListener('change', validarRango);
            }

            Sys.Application.add_load(function () {
                inicializarSwitchFecha();
                inicializarMonthPickers();
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                inicializarSwitchFecha();
                inicializarMonthPickers();
            });

        </script>
        <%--BRB-------------------------%>
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
                        var txtTerritorio = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerritorio != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerritorio);
                        var txtCliente = $find("<%= txtCliente.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        if (txtProducto != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal"
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td></td>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt"
                        runat="server"
                        width="99%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="1000px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Order por"></asp:Label>
                            </td>
                            <td width="100"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbCliente" runat="server" Text="Cliente" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbCliente_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbProducto" runat="server" Text="Producto" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbProducto_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbTerritorio" runat="server" Text="Territorio" AutoPostBack="true"
                                                GroupName="por" OnCheckedChanged="rbTerritorio_CheckedChanged" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbRepresentante" runat="server" Text="Representante" AutoPostBack="true"
                                                GroupName="por" OnCheckedChanged="rbRepresentante_CheckedChanged" Checked="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td valign="top">
                                <table>
                                    <tr runat="server" id="Filtro_Territorio">
                                        <td width="60">
                                            <asp:Label ID="LblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Filtro_Cliente" visible="false">
                                        <td width="60">
                                            <asp:Label ID="LblCliente" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtCliente" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Filtro_Producto">
                                        <td width="60">
                                            <asp:Label ID="LblProducto" runat="server" Text="Producto"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtProducto" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="60">
                                            <asp:Label ID="Label4" runat="server" Text="Categoría"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbCategoria" runat="server" Width="300px" MaxHeight="250px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="100"></td>
                                    </tr>
                                    <%----BRB---------------------------%>
                                    <!-- Switch -->
                                    <tr>
                                        <td colspan="3" style="padding-bottom: 5px;">
                                            <label class="switch-label">Solo año</label>
                                            <label class="switch">
                                                <input type="checkbox" id="togglePeriodoAnio" />
                                                <span class="slider round"></span>
                                            </label>
                                            <asp:HiddenField ID="HiddenModoFecha" runat="server" />
                                        </td>
                                    </tr>

                                    <!-- Año -->
                                    <tr id="rowAnio" style="display: none;" runat="server">
                                        <td width="60">
                                            <asp:Label ID="LblAnio" runat="server" Text="Año"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbAño" runat="server" Width="130px" MaxHeight="250px"></telerik:RadComboBox>
                                        </td>
                                        <td width="100">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True" ControlToValidate="cmbAño"
                                                ValidationGroup="Mostrar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <!-- Periodo -->
                                    <%--<tr id="rowPeriodo" runat="server">
                                        <td>
                                            <label>Periodo</label></td>
                                        <td colspan="2">
                                            <div class="float-label-group">
                                                <input type="text" id="litepickerInicio" placeholder=" " />
                                                <label for="litepickerInicio">Fecha Inicio</label>
                                            </div>
                                            <div class="float-label-group" style="margin-left: 10px;">
                                                <input type="text" id="litepickerFin" placeholder=" " />
                                                <label for="litepickerFin">Fecha Fin</label>
                                            </div>
                                            <span style="font-size: 11px; color: gray; display: block; margin-top: 5px;">* Solo se tomará en cuenta el mes/año seleccionado</span>
                                            <asp:HiddenField ID="hdnFechaInicio" runat="server" />
                                            <asp:HiddenField ID="hdnFechaFin" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr id="rowPeriodo" runat="server">
                                        <td>
                                            <label>Periodo</label>
                                        </td>
                                        <td colspan="2">
                                            <div style="display: flex; gap: 15px; align-items: flex-start;">
                                                <div class="float-label-group">
                                                    <input type="month" id="monthPickerInicio" class="month-input" min="1998-01" max="<%= DateTime.Now.ToString("yyyy-MM") %>" />
                                                    <label for="monthPickerInicio" class="month-label">Fecha Inicio</label>
                                                </div>
                                                <div class="float-label-group">
                                                    <input type="month" id="monthPickerFin" class="month-input" min="1998-01" max="<%= DateTime.Now.ToString("yyyy-MM") %>" />
                                                    <label for="monthPickerFin" class="month-label">Fecha Fin</label>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnFechaInicio" runat="server" />
                                            <asp:HiddenField ID="hdnFechaFin" runat="server" />
                                        </td>
                                    </tr>
                                    <%--BRB-------------------------%>
                                    <tr runat="server" id="TrMes">
                                        <td width="60">
                                            <asp:Label ID="LblAnio0" runat="server" Text="Mes"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbMes" runat="server" Width="130px" MaxHeight="250px">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="0" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" Owner="cmbMes" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="100">&nbsp;
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Semanal">
                                        <td></td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="ckbSemanal" runat="server" Text="Venta Semanal" AutoPostBack="true"
                                                OnCheckedChanged="CkbSemanal_CheckedChanged" />
                                        </td>
                                    </tr>

                                    <tr runat="server" id="Mov80">
                                        <td></td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="ChkMov80" runat="server" Text="Considerar ventas con remisiones de tipo Mov 80" AutoPostBack="true"
                                                OnCheckedChanged="ChkMov80_CheckedChanged" />
                                        </td>
                                    </tr>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblMostrar" runat="server" Text="Mostrar en"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:RadioButton ID="rbPesos" runat="server" Text="Pesos" Checked="true" GroupName="pesos" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:RadioButton ID="rbUnidades" runat="server" Text="Unidades" GroupName="pesos" />
                </td>
            </tr>
            <tr runat="server" id="Ambos">
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <asp:RadioButton ID="rbAmbos" runat="server" Text="Ambos" GroupName="pesos" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr runat="server" id="Nivel">
                <td colspan="2">
                    <asp:Label ID="LblNivel" runat="server" Text="Nivel"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr runat="server" id="Nivel_Cliente">
                <td></td>
                <td colspan="2">
                    <asp:CheckBox ID="cbCliente" runat="server" Text="Cliente" />
                </td>
            </tr>
            <tr runat="server" id="Nivel_Producto">
                <td></td>
                <td colspan="2">
                    <asp:CheckBox ID="cbProducto" runat="server" Text="Producto" />
                </td>
            </tr>
        </table>
        </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            height: 26px;
        }

        /*--BRB-------------------------*/

        .float-label-group {
            position: relative;
            font-size: 14px;
            display: inline-block;
        }

            .float-label-group input {
                width: 120px;
                padding: 10px 5px 5px 5px;
                border: 1px solid #ccc;
                border-radius: 4px;
                background-color: white;
            }

            .float-label-group label {
                position: absolute;
                top: 10px;
                left: 8px;
                color: #aaa;
                font-size: 13px;
                pointer-events: none;
                transition: 0.2s ease all;
                background: white;
                padding: 0 3px;
            }

            .float-label-group input:focus + label,
            .float-label-group input:not(:placeholder-shown) + label {
                top: -8px;
                left: 5px;
                font-size: 11px;
                color: #555;
            }

        .switch {
            position: relative;
            display: inline-block;
            width: 34px;
            height: 18px;
            margin-left: 8px;
            vertical-align: middle;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .switch-label {
            font-size: 14px;
            margin-right: 10px;
            vertical-align: middle;
        }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            transition: .4s;
            border-radius: 34px;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 14px;
                width: 14px;
                left: 2px;
                bottom: 2px;
                background-color: white;
                transition: .4s;
                border-radius: 50%;
            }

        input:checked + .slider {
            background-color: #039fdb;
        }

            input:checked + .slider:before {
                transform: translateX(16px);
            }

        .litepicker .container__days {
            display: none !important;
        }


        input[type="month"] {
            width: 200px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        .btn {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
        }

            .btn:hover {
                background-color: #0056b3;
            }

        .result {
            margin-top: 20px;
            padding: 10px;
            background-color: #f8f9fa;
            border-radius: 4px;
        }
    </style>
    <%--/*--BRB-------------------------*/--%>
</asp:Content>
