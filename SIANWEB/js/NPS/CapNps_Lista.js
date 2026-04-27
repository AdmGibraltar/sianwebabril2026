$(document).ready(function () {
    $('.modal').modal(
        'hide'
    );

    JsCapNps_Lista.ConsultarCatalogos();

});

var GeneralFn = (function () {

    function HttpRequest(url, params) {
        $('#dvLoading').css("display", "block");
        return fetch(url, {
            method: "POST",
            mode: "cors",
            cache: "no-cache",
            credentials: "same-origin",
            redirect: "follow",
            referrer: "no-referrer",
            body: JSON.stringify(params),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            if (response.status !== 404 && response.status !== 500) {
                return response.json();
            }
            return Promise.reject(response.statusText)
        }).then(function (data) {
            if (typeof data.d.boolResultado !== "undefined") {
                if (data.d.boolResultado == false) {
                    if (data.d.intValor == -1) {

                        setTimeout(() => {
                            window.location.href = data.d.objResultado.redirect;
                        }, 3000);
                        return Promise.reject(data.d.strMensaje);
                    } else {
                        return Promise.reject(data.d.strMensaje);
                    }
                }
            }
            return data;
        }).catch(function (error) {

            if (typeof error !== "undefined") {
                alertify.error(error);
            }
            return null;

        }).finally(function () {
            $('#dvLoading').css("display", "none");
        });
    }

    function GetStrFecha(jsDate) {
        var strFecha = "";
        if (jsDate != null) {
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                strFecha = `${day}/0${month}/${year}`;
            } else {
                strFecha = `${day}/${month}/${year}`;
            }
        }
        return strFecha;
    }

    return {
        HttpRequest: function (url, params) {
            return HttpRequest(url, params);
        },
        GetStrFecha: function (jsDate) {
            return GetStrFecha(jsDate);
        }
    }

})();

var JsCapNps_Lista = (function () {
    var clase = "CapNps_Lista.aspx/";
    var dataNpsTipo;
    var dataNpsTema;
    var dataNpsEstatus;

    function LlenarSelect(elemento, data, itemTodos = false) {

        $(elemento).empty()

        if (itemTodos) {
            $(elemento).append("<option value='0'> Todos </option>");
        } else {
            $(elemento).append("<option value='0'> Seleccione </option>");
        }

        for (var i = 0; i < data.length; i++) {
            var opt = elemento[i];
            $(elemento).append('<option value=' + data[i].intValue + '>' + data[i].strText + '</option>');
        }

        $(elemento).val("0");
    }

    function LlenarTCmb(combo, data, itemTodos = false) {
        //Llenar combo
        combo.clearItems();
        var comboItem = new Telerik.Web.UI.RadComboBoxItem();

        if (itemTodos) {
            comboItem.set_value('0');
            comboItem.set_text(' Todos ');
            combo.trackChanges();
            combo.get_items().add(comboItem);
            comboItem.select();
            combo.commitChanges();
        }

        for (i = 0; i < data.length; i++) {
            comboItem = new Telerik.Web.UI.RadComboBoxItem();
            comboItem.set_value(data[i].intValue);
            comboItem.set_text(data[i].strText);
            combo.trackChanges();
            combo.get_items().add(comboItem);
            combo.commitChanges();
        }

    }
    function LlenarDxCmb(combo, data, itemTodos = false) {

        //Llenar combo                   
        for (i = 0; i < data.length; i++) {
            combo.AddItem(data[i].strText, data[i].intValue);
        }

        if (itemTodos) {
            combo.AddItem(" Todos ", 0);
        }
    }

    function ConsultarCatalogos() {
        var dataEnvio = {};
        GeneralFn.HttpRequest(
            clase + "ConsultarCatalogos",
            dataEnvio
        ).then(function (data) {
            if (data === null)
                return false;
            dataNpsTipo = data.d.objResultado.dataNpsTipo;
            dataNpsTema = data.d.objResultado.dataNpsTema;
            dataNpsEstatus = data.d.objResultado.dataNpsEstatus;

            LlenarSelect("#cmbNpsFiltro", data.d.objResultado.dataNpsTipo, true);
            LlenarSelect("#cmbTemaFiltro", data.d.objResultado.dataNpsTema, true);
            LlenarSelect("#cmbEstatusFiltro", data.d.objResultado.dataNpsEstatus, true);
            LlenarSelect("#cmbClienteFiltro", data.d.objResultado.dataNpsCliente, true);
            if (data.d.objResultado.EsGerente === 1) {
                LlenarDxCmb(dxCmbRikFiltro, data.d.objResultado.dataRik, true);
                dxCmbRikFiltro.SetValue("0");
            } else {
                LlenarDxCmb(dxCmbRikFiltro, data.d.objResultado.dataRik, false);
                dxCmbRikFiltro.SetValue(data.d.objResultado.dataRik[0].intValue);
            }
            
            

        });

    }

    function ConsultarCliente(valueRik) {
        var data = {
            "strIdRiK": valueRik
        };
        GeneralFn.HttpRequest(
            clase + "ConsultarCliente",
            data
        ).then(function (data) {
            if (data === null)
                return false;
            LlenarSelect("#cmbClienteFiltro", data.d.objResultado.dataCliente, true);
        });

    }

    function LLenarSelectQueja(numero) {
        LlenarSelect("#slcTipoQueja" + numero, dataNpsTipo, false);
        LlenarSelect("#slcTemaQueja" + numero, dataNpsTema, false);
    }

    function BuscarNps() {

        var strFechaInicial = GeneralFn.GetStrFecha(dxFechaInicialFiltro.GetDate());
        var strFechaFinal = GeneralFn.GetStrFecha(dxFechaFinalFiltro.GetDate());

        var envioDatos = {
            "strIdRik": dxCmbRikFiltro.GetValue(),
            "strIdCliente": $("#cmbClienteFiltro").val(),
            "strIdNpsTipo": $("#cmbNpsFiltro").val(),
            "strIdEstatus": $("#cmbEstatusFiltro").val(),
            "strFechaInicial": strFechaInicial,
            "strFechaFinal": strFechaFinal
        };

        GeneralFn.HttpRequest(
            clase + "CargarReporteNps",
            envioDatos
        ).then(function (data) {
            if (data === null)
                return false;
            if (data.d.objResultado.dataReporte.length > 0) {
                LLenarReporte(data.d.objResultado.dataReporte);
            } else {
                alertify.error("No se encontraron registros");
            }            
            
        });
    }

    function LLenarReporte(data) {
        var html = "";
        var btnPlan = "";
        for (var i = 0; i < Object.keys(data).length; i++) {

            if (data[i].Id_Nps_Estatus == 1 || data[i].Id_Nps_Estatus == 2 || data[i].Id_Nps_Estatus == 5) {
                btnPlan = '<a ' +
                    'onclick="JsCapNps_Lista.AgregarPlan(' + data[i].Id_Nps + ')" ' +
                    'class="text-primary" >' +
                    '<i class="fa fa-pencil-square-o fa-2x"></i>' +
                    '</a>';
            } else {
                btnPlan = '<a ' +
                    'onclick="JsCapNps_Lista.ConsultarPlan(' + data[i].Id_Nps + ')" ' +
                    'class="text-secondary" >' +
                    '<i class="fa fa-pencil-square-o fa-2x"></i>' +
                    '</a>';
            }

            // btn btn-default btn-sm
            html += "<tr><td>" + data[i].Nps_Estatus + "</td>" +
                "<td> " + data[i].Fecha_Entrevista + "</td>" +
                "<td> " + data[i].Rik_Nombre + "</td>" +
                "<td> " + data[i].Cte_Nomcomercial + "</td>" +
                "<td> " + data[i].Nps_Descripcion + " </td>" +
                "<td> " + data[i].Nps_Tema + "</td>" +
                "<td> " + btnPlan + " </td> </tr>";
            //private int _Id_Nps_Queja;
            // private int _Id_Nps;
        }
        $("#tblNpsReporte tbody").html(html);

    }

    function AgregarPlan(strIdNps) {
        var envioDatos = {
            "strIdNps": strIdNps
        };

        GeneralFn.HttpRequest(
            clase + "SeleccionarNps",
            envioDatos
        ).then(function (data) {
            if (data === null)
                return false;
            LlenarAgregarPlan(data.d.objResultado);
        });
    }

    function LimpirPlan() {

        for (var i = 0; i < 10; i++) {
            $("#spnTema" + i).html("");
            $("#spnQueja" + i).html("");
            $("#spnProtocolo" + i).html("");
            $("#txtPlan" + i).val("");
            $("#tblComentrio" + i).hide();
        }
        $(".segundaOportunidad").hide();
        $("#PlanConsecutivo" + i).val("1"); 


        dxFechaCompromiso0.SetText();
        dxFechaCompromiso1.SetText();
        dxFechaCompromiso2.SetText();
        dxFechaCompromiso3.SetText();
        dxFechaCompromiso4.SetText();
        dxFechaCompromiso5.SetText();
        dxFechaCompromiso6.SetText();
        dxFechaCompromiso7.SetText();
        dxFechaCompromiso8.SetText();
        dxFechaCompromiso9.SetText();

        dxFechaCompromiso0.SetEnabled(true);
        dxFechaCompromiso1.SetEnabled(true);
        dxFechaCompromiso2.SetEnabled(true);
        dxFechaCompromiso3.SetEnabled(true);
        dxFechaCompromiso4.SetEnabled(true);
        dxFechaCompromiso5.SetEnabled(true);
        dxFechaCompromiso6.SetEnabled(true);
        dxFechaCompromiso7.SetEnabled(true);
        dxFechaCompromiso8.SetEnabled(true);
        dxFechaCompromiso9.SetEnabled(true);

    }


    function LlenarAgregarPlan(data) {
        LimpirPlan();
        var activaPlan = false;
        var activaSegundaOportunidad = false;

        $("#hdnIdNpsPlan").val(data.dataNps.Id_Nps);
        $("#spnSucursal").html(data.dataNps.Cd_Nombre);
        $("#spnRik").html(data.dataNps.Rik_Nombre);
        $("#spnCliente").html(data.dataNps.Cte_NomComercial);
        $("#spnValor").html(data.dataNps.Nps_Valor_Inicial + ", " + data.dataNps.Nps_Tipo_Inicial);
        $("#spnEntrevistado").html(data.dataNps.Entrevistado);
        $("#spnPuesto").html(data.dataNps.Puesto);

       
        $("#hdnDateEntrevista").val(data.dataNps.strFecha_Entrevista);

        if (data.dataNps.Nps_Valor_Segundo > 0) {
            $("#spnValorSegundo").html(data.dataNps.Nps_Valor_Segundo + ", " + data.dataNps.Nps_Tipo_Segundo);
            $("#hdnPlanConsecutivo").val("2");
            activaSegundaOportunidad = true;
            $(".segundaOportunidad").show();
        } else {
            $("#hdnPlanConsecutivo").val("1");
        }       

        switch (data.dataNps.Id_Nps_Estatus) {
            case 1:
                $("#btnGuardarPlan").removeClass("disabled");
                $("#btnConcluir").addClass("disabled");
                activaPlan = true;
                break;
            case 2:
                $("#btnGuardarPlan").removeClass("disabled");
                $("#btnConcluir").removeClass("disabled");
                activaPlan = true;
                break;
            case 3:
                $("#btnGuardarPlan").addClass("disabled");
                $("#btnConcluir").addClass("disabled");
                break;
            case 4:
                $("#btnGuardarPlan").addClass("disabled");
                $("#btnConcluir").addClass("disabled");
                //activaSegundaOportunidad = false;
                break;
            case 5:
                $("#btnGuardarPlan").removeClass("disabled");
                $("#btnConcluir").addClass("disabled");                
                activaPlan = true;                
                break;
        }

        for (var i = 0; i < data.dataQueja.length; i++) {

            $("#tblComentrio" + i).show();

            $("#hdnIdQuejaPlan" + i).val(data.dataQueja[i].Id_Nps_Queja);

            if (data.dataQueja[i].Id_Nps_Tema == 10) {
                $("#spnTema" + i).html(data.dataQueja[i].Nps_Tema + ": " + data.dataQueja[i].Otro_Tema);
            } else {
                $("#spnTema" + i).html(data.dataQueja[i].Nps_Tema);
            }

            $("#spnQueja" + i).html(data.dataQueja[i].Nps_Queja);
            $("#spnProtocolo" + i).html(data.dataQueja[i].Nps_Protocolo);

            if (activaPlan) {
                $("#txtPlan" + i).prop("disabled", false);
            } else {
                $("#txtPlan" + i).prop("disabled", true);
            }

            
            
            if (activaSegundaOportunidad) {
                $("#spnPlanAnterior" + i).html(data.dataQueja[i].Nps_PlanAccion);
                let dateCompromiso = new Date(data.dataQueja[i].strFecha_Compromiso);                
                $("#spnCompromisoAnterior" + i).html(dateCompromiso.toLocaleDateString('en-GB'));
                $("#txtPlan" + i).val(data.dataQueja[i].Nps_PlanAccion_Segundo);
                if (data.dataQueja[i].strFecha_Compromiso_Segundo.length > 0) {
                    let dateAlta = new Date(data.dataQueja[i].strFecha_Compromiso_Segundo);
                    MostrarFecha(i, dateAlta, activaPlan);
                }
            } else {
                $("#txtPlan" + i).val(data.dataQueja[i].Nps_PlanAccion);
                if (data.dataQueja[i].strFecha_Compromiso.length > 0) {
                    let dateAlta = new Date(data.dataQueja[i].strFecha_Compromiso);
                    MostrarFecha(i, dateAlta, activaPlan);
                }
            }            


          
        }

        $('#modalAgregarPlan').appendTo("body").modal('show');
    }

    function MostrarFecha(opc, fecha, isEnabled) {

        switch (opc) {
            case 0:
                dxFechaCompromiso0.SetDate(fecha);
                dxFechaCompromiso0.SetEnabled(isEnabled);
                break;
            case 1:
                dxFechaCompromiso1.SetDate(fecha);
                dxFechaCompromiso1.SetEnabled(isEnabled);
                break;
            case 2:
                dxFechaCompromiso2.SetDate(fecha);
                dxFechaCompromiso2.SetEnabled(isEnabled);
                break;
            case 3:
                dxFechaCompromiso3.SetDate(fecha);
                dxFechaCompromiso3.SetEnabled(isEnabled);
                break;
            case 4:
                dxFechaCompromiso4.SetDate(fecha);
                dxFechaCompromiso4.SetEnabled(isEnabled);
                break;
            case 5:
                dxFechaCompromiso5.SetDate(fecha);
                dxFechaCompromiso5.SetEnabled(isEnabled);
                break;
            case 6:
                dxFechaCompromiso6.SetDate(fecha);
                dxFechaCompromiso6.SetEnabled(isEnabled);
                break;
            case 7:
                dxFechaCompromiso7.SetDate(fecha);
                dxFechaCompromiso7.SetEnabled(isEnabled);
                break
            case 8:
                dxFechaCompromiso8.SetDate(fecha);
                dxFechaCompromiso8.SetEnabled(isEnabled);
                break;
            case 9:
                dxFechaCompromiso9.SetDate(fecha);
                dxFechaCompromiso9.SetEnabled(isEnabled);
                break;
            default:
                return false;
        }
    }

    function ConsultarPlan(strIdNps) {
        // vista de solo lectura
        AgregarPlan(strIdNps)
    }

    function GuardarPlan(EstatusConcluido) {
        //   
        var strIdNps = $("#hdnIdNpsPlan").val();
        var PlanConsecutivo = $("#hdnPlanConsecutivo").val();  

        var strIdQueja;
        var strPlan;
        var strFecha;
        var arrQueja = [];
        var itemQueja = {};
        let fechaCompromisoJs;
        let dateEntrevistaJs = new Date($("#hdnDateEntrevista").val());
        let strFechaentrevista = GeneralFn.GetStrFecha(dateEntrevistaJs);

        for (var i = 0; i < 10; i++) {
            if ($("#tblComentrio" + i).is(":visible")) {

                strIdQueja = $("#hdnIdQuejaPlan" + i).val();
                strPlan = $("#txtPlan" + i).val();

                if (strPlan.trim().length == 0) {
                    alertify.error("Ingrese el Plan de Accion");
                    //alert("Ingrese el Plan de Accion");
                    $("#txtPlan" + i).focus();
                    return false;
                }

                strFecha = ObtenerFecha(i);

                if (strFecha == "") {
                    alertify.error("Ingrese la Fecha Compromiso");
                    return false;
                }

                fechaCompromisoJs = ObtenerDateJS(i);

                if (fechaCompromisoJs < dateEntrevistaJs) {
                    alertify.error("la Fecha Compromiso es menor que la fecha de entrevista(" + strFechaentrevista +")");
                    return false;
                }


                itemQueja = {
                    "Id_Nps_Queja": strIdQueja,
                    "Nps_PlanAccion": strPlan,
                    "strFecha_Compromiso": strFecha
                };

                arrQueja.push(itemQueja);
            } else {
                break;
            }

        }

        var envioDatos = {
            "strIdNps": strIdNps,
            "strDataQueja": JSON.stringify(arrQueja),
            "strConcluido": EstatusConcluido,
            "strPlanConsecutivo": PlanConsecutivo
        };

        GeneralFn.HttpRequest(
            clase + "GuardarPlan",
            envioDatos
        ).then(function (data) {
            if (data === null)
                return false;
            alertify.success(data.d.strMensaje);
            BuscarNps();
            $('#modalAgregarPlan').appendTo("body").modal('hide');

        });
    }

    function ObtenerFecha(opc) {

        var elmFecha;

        switch (opc) {
            case 0:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso0.GetDate());
                break;
            case 1:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso1.GetDate());
                break;
            case 2:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso2.GetDate());
                break;
            case 3:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso3.GetDate());
                break;
            case 4:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso4.GetDate());
                break;
            case 5:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso5.GetDate());
                break;
            case 6:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso6.GetDate());
                break;
            case 7:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso7.GetDate());
                break
            case 8:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso8.GetDate());
                break;
            case 9:
                elmFecha = GeneralFn.GetStrFecha(dxFechaCompromiso9.GetDate());
                break;
        }

        return elmFecha;
    }
    
    function ObtenerDateJS(opc) {

        var elmFecha;

        switch (opc) {
            case 0:
                elmFecha = dxFechaCompromiso0.GetDate();
                break;
            case 1:
                elmFecha = dxFechaCompromiso1.GetDate();
                break;
            case 2:
                elmFecha = dxFechaCompromiso2.GetDate();
                break;
            case 3:
                elmFecha = dxFechaCompromiso3.GetDate();
                break;
            case 4:
                elmFecha = dxFechaCompromiso4.GetDate();
                break;
            case 5:
                elmFecha = dxFechaCompromiso5.GetDate();
                break;
            case 6:
                elmFecha = dxFechaCompromiso6.GetDate();
                break;
            case 7:
                elmFecha = dxFechaCompromiso7.GetDate();
                break
            case 8:
                elmFecha = dxFechaCompromiso8.GetDate();
                break;
            case 9:
                elmFecha = dxFechaCompromiso9.GetDate();
                break;
        }

        return elmFecha;
    }

    function LimpiarCmbClientes() {
        var data = [];
        LlenarSelect("#cmbClienteFiltro", data, true);
    }

    function LimpiarFiltros() {

        dxFechaInicialFiltro.SetText("");
        dxFechaFinalFiltro.SetText("");

        if (dxCmbRikFiltro.GetItemCount() > 1) {
            dxCmbRikFiltro.SetValue("0");
        }

        $("#cmbClienteFiltro").val("0");
        $("#cmbNpsFiltro").val("0");
        $("#cmbEstatusFiltro").val("0");       

    }

    function DescargarNps() {
        var strFechaInicial = GeneralFn.GetStrFecha(dxFechaInicialFiltro.GetDate());
        var strFechaFinal = GeneralFn.GetStrFecha(dxFechaFinalFiltro.GetDate());

        var envioDatos = {
            "strIdRik": dxCmbRikFiltro.GetValue(),
            "strIdCliente": $("#cmbClienteFiltro").val(),
            "strIdNpsTipo": $("#cmbNpsFiltro").val(),
            "strIdEstatus": $("#cmbEstatusFiltro").val(),
            "strFechaInicial": strFechaInicial,
            "strFechaFinal": strFechaFinal,
        };

        GeneralFn.HttpRequest(
            clase + "CargarReporteNpsDetallado",
            envioDatos
        ).then(function (data) {
            if (data == null)
                return false;
            if (data.d.objResultado.dataReporte.length > 0) {
                DescargarExcel(data.d.objResultado.dataReporte);
            } else {
                alertify.error("No se encontraron registros para descargar");
            }
            
        });
    }

    
     
     function DescargarExcel(data) {
        var tab_text = "<table border='2px'> <tr bgcolor='#87AFC6'>";
        var textRange;
        var j = 0;

        tab_text = tab_text +
            "<td>Envio de alerta por correo </td>" +
            "<td>Estatus </td>" +
            "<td>Fecha de entrevista </td>" +
            "<td>Cliente </td>" +
            "<td>NPS Cliente 1</td>" +
            "<td>NPS Calificacion 1</td>" +
            "<td>RIK </td>" +
            "<td>Entrevistado </td>" +
            "<td>Puesto/Area </td>" +
            "<td>Tema </td>" +
            "<td>Comentario </td>" +
            "<td>Fecha asignado </td>" +
            "<td>Plan de accion 1</td>" +
            "<td>Fecha compromiso 1</td>" +
            "<td>Fecha en desarrollo 1 </td>" +
            "<td>Fecha atendido 1 </td>" +
            "<td>Fecha de reenvio </td>" +
            "<td>Plan de accion 2 </td>" +
            "<td>Fecha compromiso 2 </td>" +
            "<td>Fecha en desarrollo 2 </td>" +
            "<td>Fecha atendido 2</td>" +
            "<td>Fecha cerrado</td>" +
            "<td>NPS Cliente 2</td>" +
            "<td>NPS Calificacion 2</td>" +
            "<td>NPS cierre de ciclos </td>" +
            "<td>NPS cierre de ciclos Calificacion </td>" +
            "<td>Comentario cierre de ciclos</td>" +
            "</tr>";


        for (j = 0; j < data.length; j++) {
            tab_text = tab_text +
                "<td>" + data[j].Envio_Correo + "</td>" +
                "<td>" + data[j].Nps_Estatus + "</td>" +
                "<td>" + data[j].Fecha_Entrevista + "</td>" +
                "<td>" + data[j].Cte_Nomcomercial + "</td>" +
                "<td>" + data[j].Nps_Tipo_Inicial + "</td>" +
                "<td>" + data[j].Nps_Valor_Inicial + "</td>" +
                "<td>" + data[j].Rik_Nombre + "</td>" +
                "<td>" + data[j].Entrevistado + "</td>" +
                "<td>" + data[j].Puesto + "</td>" +
                "<td>" + data[j].Nps_Tema + "</td>" +                
                "<td>" + data[j].Nps_Queja + "</td>" +                
                "<td>" + data[j].Fecha_Asignado + "</td>" +
                "<td>" + data[j].Nps_PlanAccion + "</td>" +
                "<td>" + data[j].Fecha_Compromiso + "</td>" +
                "<td>" + data[j].Fecha_EnDesarrollo + "</td>" +
                "<td>" + data[j].Fecha_Atendido + "</td>" +
                "<td>" + data[j].Fecha_Reenviado + "</td>" +
                "<td>" + data[j].Nps_PlanAccion_Segundo + "</td>" +
                "<td>" + data[j].Fecha_Compromiso_Segundo + "</td>" +
                "<td>" + data[j].Fecha_EnDesarrollo_Segundo + "</td>" +
                "<td>" + data[j].Fecha_Atendido_Segundo + "</td>" +
                "<td>" + data[j].Fecha_CierreCiclo + "</td>" +                
                "<td>" + data[j].Nps_Tipo_Segundo + "</td>" +
                "<td>" + data[j].Nps_Valor_Segundo + "</td>" +
                "<td>" + data[j].Nps_Tipo_Final + "</td>" + 
                "<td>" + data[j].Nps_Valor_Final + "</td>" +
                "<td>" + data[j].Comentario + "</td>" +
                "</tr>";
        }


        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table border="2px">{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }


        var ctx = { worksheet: 'Reporte' || 'Worksheet', table: tab_text }
        window.location.href = uri + base64(format(template, ctx));


    }
    

    return {
        ConsultarCatalogos: function () {
            ConsultarCatalogos();
        },
        AgregarPlan: function (strIdNps) {
            AgregarPlan(strIdNps);
        },
        GuardarPlan: function () {
            var concluido = -1;
            GuardarPlan(concluido);
        },
        ConcluirPlan: function () {
            alertify.confirm('Una vez concluido no podra ser editable,&iquest;Desea continuar?',
                function (e) {
                    if (e) {
                        GuardarPlan(1);
                    }
                }
            );
        },
        LLenarSelectQueja: function (numero) {
            LLenarSelectQueja(numero);
        },
        AgregarQueja: function () {
            AgregarQueja();
        },
        QuitarQueja: function (numero) {

            $(".quitarTr" + numero).remove();
        },
        CambiaRik: function () {
            var value = $("#slcRikFlitro").val();
            //ConsultarCliente(value);
        },
        ConsultarCliente: function (valueRik) {
            if (valueRik != "-1") {
                ConsultarCliente(valueRik);
            } else {
                LimpiarCmbClientes();
            }
        },
        ValidarNps: function () {
            ValidarNps();
        },
        BuscarNps: function () {
            BuscarNps();
        },
        EditarNps: function (idNps, idCD) {
            EditarNps(idNps, idCD);
        },
        MostrarCierreCiclo: function (IdQueja, idNps, IdSucursal) {
            MostrarCierreCiclo(IdQueja, idNps, IdSucursal);
        },
        CierreCiclo: function () {
            CierreCiclo();
        },
        MostrarModalAgregar: function () {
            MostrarModalAgregar();
        },
        CambiaTema: function (fila) {
            CambiaTema(fila);
        },
        ConsultarPlan: function (strIdNps) {
            ConsultarPlan(strIdNps);
        },
        LimpiarFiltros: function () {
            LimpiarFiltros();
        },
        DescargarNps: function () {
            DescargarNps();
        }
    }
})();

function keyPress(sender, args) {
    var text = sender.get_value().concat(args.get_keyCharacter());

    if (!text.match('^[0-9]+$'))
        args.set_cancel(true);

}

function OnClientBuscarRik(sender, eventArgs) {
    var item = eventArgs.get_item();
    //sender.set_text("You selected " + item.get_text());
    console.log(item.get_value());
}

function validaNumero(e) {
    if (e.keyCode < 45 || e.keyCode > 57) {
        e.returnValue = false;
        return false;
    }
    return true;
}


function SoloNumericoYDiagonal(sender, eventArgs) {
    var c = eventArgs.get_keyCode();

    if (c && c == 13)
        eventArgs.set_cancel(true);
    if ((c < 48 || c > 57))//si no es numero
        if (c != 47) //si no es punto
            eventArgs.set_cancel(true);
}

function OnChangedRikFiltro(s, e) {
    JsCapNps_Lista.ConsultarCliente(s.GetValue())
}
