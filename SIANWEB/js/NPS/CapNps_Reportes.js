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
$(document).ready(function () {
    $('.modal').modal(
        'hide'
    );

    $('#tabDescarga').on('shown.bs.tab', function () {
        JsCapNps_Reporte.LlenarDxCmbUEN();
    });

    JsCapNps_Reporte.ConsultarCatalogos();

});

var GeneralFn = (function () {
    //const USER = GeneralFn.GetUsrId();

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
                return response.json()
            }
            return Promise.reject(response)
        }).then(function (data) {
            if (typeof data.d.boolResultado !== "undefined") {
                if (data.d.boolResultado == false) {
                    if (data.d.intValor == -1) {
                        alertify.error(data.d.strMensaje);
                        setTimeout(() => {
                            window.location.href = data.d.objResultado.redirect;
                        }, 3000);
                        return Promise.reject(data.d);
                    } else {
                        return Promise.reject(data.d);
                    }
                }
            }
            return data;
        }).catch(function (error) {
            error.json().then(e => {
                error.responseJSON = e
                catchEx(error)
            })
            location.href = document.getElementById('hdnurlroot').value + 'Error.aspx'
            return error
        }).finally(function () {
            $('#dvLoading').css("display", "none");
        });
    }

    function ValidarDatos(data) {
        return true;
    }

    function MensajeAlerta(strMensaje) {
        document.getElementById("lblmensaje2").innerHTML = strMensaje;
        $("#modalMensaje").appendTo("body");
        $("#modalMensaje").modal({ "backdrop": "static" });
        $('#modalMensaje').modal('show');
    }

    function BarraProgreso_Cerrar() {
        $('#dvLoading').css("display", "none");
    }


    function BarraProgreso_Abrir() {
        $('#dvLoading').css("display", "block");
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
        MensajeAlerta: function (strMensaje) {
            MensajeAlerta(strMensaje);
        },
        BarraProgreso_Abrir: function () {
            BarraProgreso_Abrir();
        },
        BarraProgreso_Cerrar: function () {
            BarraProgreso_Cerrar();
        }, ValidarDatos: function (data) {
            ValidarDatos(data);
        },
        GetStrFecha: function (jsDate) {
            return GetStrFecha(jsDate);
        }
    }

})();

var JsCapNps_Reporte = (function () {
    var clase = "CapNps_Reportes.aspx/";
    var dataNpsTipo;
    var dataNpsTema;
    var dataNpsEstatus;
    var dataNpsUEN;
    var boolPrimerUENLlenado = true;

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

    function ConsultarCatalogos() {
        var data = {};
        GeneralFn.HttpRequest(
            clase + "ConsultarCatalogos",
            data
        ).then(function (data) {
            if (data === null)
                return false;

            dataNpsTipo = data.d.dataNpsTipo;
            dataNpsTema = data.d.dataNpsTema;
            dataNpsEstatus = data.d.dataNpsEstatus;
            dataNpsUEN = data.d.dataNpsUEN;

            LlenarSelect("#cmbNpsFiltro", data.d.dataNpsTipo, true);
            LlenarSelect("#cmbTemaFiltro", data.d.dataNpsTema, true);
            LlenarSelect("#cmbEstatusFiltro", data.d.dataNpsEstatus, true);
            LlenarSelect("#cmbUEnFiltro", data.d.dataNpsUEN, true);

            if (data.d.EsGerente === 1) {
                LlenarDxCmb(dxCmbRikTrazabilidad, data.d.dataRik, true);
                LlenarDxCmb(dxCmbRikIndicadorGeneral, data.d.dataRik, true);
                LlenarDxCmb(dxCmbRikConversion, data.d.dataRik, true);
                LlenarDxCmb(dxCmbRikGlobal, data.d.dataRik, true);
            } else{
                LlenarDxCmb(dxCmbRikTrazabilidad, data.d.dataRik, false);
                LlenarDxCmb(dxCmbRikIndicadorGeneral, data.d.dataRik, false);
                LlenarDxCmb(dxCmbRikConversion, data.d.dataRik, false);
                LlenarDxCmb(dxCmbRikGlobal, data.d.dataRik, false);

                dxCmbRikTrazabilidad.SetSelectedIndex(0);
                dxCmbRikIndicadorGeneral.SetSelectedIndex(0);
                dxCmbRikConversion.SetSelectedIndex(0);
                dxCmbRikGlobal.SetSelectedIndex(0);
            }
            
            LlenarDxCmb(dxLstBoxEstatusTrazabilidad, data.d.dataNpsEstatus, false);

            LlenarDxCmb(dxLstBoxUENTrazabilidad, data.d.dataNpsUEN, false);
            LlenarDxCmb(dxLstBoxUENGeneral, data.d.dataNpsUEN, false);
            LlenarDxCmb(dxLstBoxUENConversion, data.d.dataNpsUEN, false);
            LlenarDxCmb(dxLstBoxUENGlobal, data.d.dataNpsUEN, false);

            $("#panelConversion").hide();
            $("#panelGeneral").hide();

        });

    }

    function LlenarDxCmbUEN() {
        dxLstBoxUENGlobal.AdjustControl();
    }

    function LlenarDxCmb(combo, data, itemTodos = false) {

        //Llenar combo
        var i = 0;
        for (; i < data.length; i++) {
            combo.AddItem(data[i].strText, data[i].intValue);
        }

        if (itemTodos) {
            combo.AddItem(" Todos ", 0);
            combo.SetSelectedIndex(i);
        }
    }

    function DescargarNps() {

        var strFechaInicial = GeneralFn.GetStrFecha(dxFechaInicialFiltro.GetDate());
        var strFechaFinal = GeneralFn.GetStrFecha(dxFechaFinalFiltro.GetDate());

        var lstUEN = dxLstBoxUENGlobal.GetSelectedValues();
        var strRik = dxCmbRikGlobal.GetValue();

        if (lstUEN.length == 0) {
            GeneralFn.BarraProgreso_Cerrar();
            GeneralFn.MensajeAlerta("Seleccione al menos UEN.");
            return false;
        }

        var envioDatos = {
            "strIdRik": strRik,
            "strIdNpsTipo": $("#cmbNpsFiltro").val(),
            "strIdEstatus": $("#cmbEstatusFiltro").val(),
            "strFechaInicial": strFechaInicial,
            "strFechaFinal": strFechaFinal,
            "strUEN": lstUEN.join(', ')
        };

        GeneralFn.HttpRequest(
            clase + "CargarReporteNpsDetallado",
            envioDatos
        ).then(function (data) {
            if (data === null)
                return false;
            DescargarExcel(data.d.dataReporte);
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


    /*
    function DescargarExcel(data) {
        var tab_text = "<table border='2px'> <tr bgcolor='#87AFC6'>";
        var textRange;
        var j = 0;

        tab_text = tab_text +
            "<td>Estatus </td>" +
            "<td>Fecha de entrevista </td>" +
            "<td>Cliente </td>" +
            "<td>Sucursal </td>" +
            "<td>RIK </td>" +
            "<td>NPS Cliente </td>" +
            "<td>NPS Calificaccion </td>" +
            "<td>Tema </td>" +
            "<td>Entrevistado </td>" +
            "<td>Puesto/Area </td>" +
            "<td>Comentario </td>" +
            "<td>Plan de accion </td>" +
            "<td>Fecha compromiso </td>" +
            "<td>Fecha en desarrollo </td>" +
            "<td>Fecha atendido </td>" +
            "<td>Fecha cerrado </td>" +
            "<td>NPS cierre de ciclos </td>" +
            "<td>NPS cierre de ciclos Calificacion </td>" +
            "</tr>";


        for (j = 0; j < data.length; j++) {
            tab_text = tab_text +
                "<td>" + data[j].Nps_Estatus + "</td>" +
                "<td>" + data[j].Fecha_Entrevista + "</td>" +
                "<td>" + data[j].Cte_Nomcomercial + "</td>" +
                "<td>" + data[j].Cd_Nombre + "</td>" +
                "<td>" + data[j].Rik_Nombre + "</td>" +
                "<td>" + data[j].Nps_Descripcion + "</td>" +
                "<td>" + data[j].Nps_Clf + "</td>" +
                "<td>" + data[j].Nps_Tema + "</td>" +
                "<td>" + data[j].Entrevistado + "</td>" +
                "<td>" + data[j].Puesto + "</td>" +
                "<td>" + data[j].Nps_Queja_Tipo + "</td>" +
                "<td>" + data[j].Nps_Plan + "</td>" +
                "<td>" + data[j].Fecha_Compromiso + "</td>" +
                "<td>" + data[j].Fecha_EnDesarrollo + "</td>" +
                "<td>" + data[j].Fecha_Concluido + "</td>" +
                "<td>" + data[j].Fecha_CierreCiclo + "</td>" +
                "<td>" + data[j].Nps_Tipo_Final + "</td>" +
                "<td>" + data[j].Nps_Valor_Final + "</td>" +
                "</tr>";
        }

        tab_text = tab_text + "</table>";

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var name = 'Reporte_Global';

      
        var ctx = { worksheet: name || 'Worksheet', table: tab_text }
        window.location.href = uri + base64(format(template, ctx))

    }
    */

    function DescargarTablaExcel(Idtabla, nombreHoja) {

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table border="2px">{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        if (!Idtabla.nodeType) {
            Idtabla = document.getElementById(Idtabla)
        }

        var bodyTabla = Idtabla.innerHTML;

        //bodyTabla = bodyTabla.replace('<tr>', '<tr bgcolor="#87AFC6">')
        var ctx = { worksheet: nombreHoja || 'Worksheet', table: bodyTabla }
        window.location.href = uri + base64(format(template, ctx))
    }

    function LimpiarFiltros() {
        dxLstBoxUENGlobal.UnselectAll();

        if (dxCmbRikGlobal.GetItemCount() > 1) {
            dxCmbRikGlobal.SetValue("0");
        }

        $("#cmbNpsFiltro").val("0");
        $("#cmbEstatusFiltro").val("0");

        dxFechaInicialFiltro.SetText("");
        dxFechaFinalFiltro.SetText("");

    }

    function BuscarNps() {

        var strFechaInicial = GeneralFn.GetStrFecha(dxFechaInicialFiltro.GetDate());
        var strFechaFinal = GeneralFn.GetStrFecha(dxFechaFinalFiltro.GetDate());

        var lstUEN = dxLstBoxUENGlobal.GetSelectedValues();

        if (lstUEN.length == 0) {
            GeneralFn.BarraProgreso_Cerrar();
            GeneralFn.MensajeAlerta("Seleccione al menos UEN.");
            return false;
        }

        var envioDatos = {
            "strIdRik": dxCmbRikGlobal.GetValue(),
            "strIdNpsTipo": $("#cmbNpsFiltro").val(),
            "strIdEstatus": $("#cmbEstatusFiltro").val(),
            "strFechaInicial": strFechaInicial,
            "strFechaFinal": strFechaFinal,
            "strUEN": lstUEN.join(', '),
        };

        console.log(envioDatos);

        GeneralFn.HttpRequest(
            clase + "CargarReporteNps",
            envioDatos
        ).then(function (data) {
            if (data === null)
                return false;
            LLenarReporte(data.d.dataReporte);
        });
    }

    function LLenarReporte(data) {
        var html = "";
        for (var i = 0; i < Object.keys(data).length; i++) {
            html += "<tr><td> " + data[i].Fecha_Entrevista + "</td>" +
                "<td> " + data[i].Nps_Estatus + "</td>" +
                "<td> " + data[i].Nps_Descripcion + " </td>" +
                "<td> " + data[i].Nps_Tema + "</td>" +
                "<td> " + data[i].Cte_Nomcomercial + "</td>" +
                "<td> " + data[i].Cd_Nombre + "</td>";
        }
        $("#tblNpsReporte tbody").html(html);

    }

    return {
        ConsultarCatalogos: function () {
            ConsultarCatalogos();
        },
        BuscarNps: function () {
            BuscarNps();
        },
        LimpiarFiltros: function () {
            LimpiarFiltros();
        },
        DescargarNps: function () {
            DescargarNps();
        },
        LlenarDxCmbUEN: function () {
            LlenarDxCmbUEN();
        },
        DescargarIndicadorNps: function () {
            DescargarTablaExcel('tblGeneral', 'Indicador_NPS_General');
        },
        DescargarTrazabilidadCliente: function () {
            DescargarTablaExcel('tblTrazabilidadCliente', 'Trazabilidad_Cliente');
        },
        DescargarTrazabilidadGlobal: function () {
            DescargarTablaExcel('tblTrazabilidad', 'Trazabilidad_Global');
        },
        DescargarConversion: function () {
            DescargarTablaExcel('tblConversion', 'Conversion');
        }
    }
})();


function ConsultaIndicadorGeneral() {

    GeneralFn.BarraProgreso_Abrir();

    var lstUEN = dxLstBoxUENGeneral.GetSelectedValues();
    var strRik = dxCmbRikIndicadorGeneral.GetValue();

    if (lstUEN.length == 0) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione al menos UEN.");
        return false;
    }

    var jsDate = AnioIniIndicadorGeneral.GetDate();

    if (jsDate === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha inicial");
        return false;
    }

    var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
    var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month < 10) {
        var myDate = `${day}/0${month}/${year}`
    } else {
        var myDate = `${day}/${month}/${year}`
    }
    var jsDate2 = AnioFinIndicadorGeneral.GetDate();

    if (jsDate2 === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha final");
        return false;
    }

    var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
    var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month2 < 10) {
        var myDateFinal = `${day2}/0${month2}/${year2}`
    } else {
        var myDateFinal = `${day2}/${month2}/${year2}`
    }

    if (jsDate.getTime() > jsDate2.getTime()) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("La fecha inicial es mayor a la fecha final");
        return false;
    }

    var dataValue = {
        "mesAnioInicial": myDate,
        "mesAniofinal": myDateFinal,
        "strUEN": lstUEN.join(', '),
        "strRik": strRik
    };


    GeneralFn.HttpRequest(
        "CapNps_Reportes.aspx/IndicadorGeneralNps",
        dataValue
    ).then(function (response) {
        if (response === null)
            return false;

        var data = response.d.objResultado;

        if (response.d.boolResult == false) {
            GeneralFn.BarraProgreso_Cerrar();
            GeneralFn.MensajeAlerta("La fecha inicial es mayor a la fecha final de la sección de observar totales.");

            return false;
        }

        $('#ChartIndicadorNps').show();
        $('#tblGeneral').show();
        $('#btnExportarNps').show();


        // LLenar tabla
        $("#spanNpsTotal").html(data.granTotal);
        $("#spanNpsPorcentaje").html("100%");
        var promotorVal = 0;
        var detractorVal = 0;
        var promotorPorciento = 0;
        var detractorPorciento = 0;
        
        for (var i = 0; i < data.dataResult.length; i++) {
            switch (data.dataResult[i].Id_Nps_Tipo) {
                case 1:
                    if (data.dataResult[i].Total == 0) {
                        $("#spanValorPromotor").html("");
                        $("#spanPorcentajePromotor").html("");
                    } else {
                        $("#spanValorPromotor").html(data.dataResult[i].Total);
                        $("#spanPorcentajePromotor").html(data.dataResult[i].Porcentaje + '%');
                        promotorVal = data.dataResult[i].Total;
                        promotorPorciento = data.dataResult[i].Porcentaje;
                    }
                    break;
                case 2:
                    if (data.dataResult[i].Total == 0) {
                        $("#spanValorPasivo").html("");
                        $("#spanPorcentajePasivo").html("");
                    } else {
                        $("#spanValorPasivo").html(data.dataResult[i].Total);
                        $("#spanPorcentajePasivo").html(data.dataResult[i].Porcentaje + '%');
                    }

                    break;
                case 3:
                    if (data.dataResult[i].Total == 0) {
                        $("#spanValorRetractor").html("");
                        $("#spanPorcentajeRetractor").html("");
                    } else {
                        $("#spanValorRetractor").html(data.dataResult[i].Total);
                        $("#spanPorcentajeRetractor").html(data.dataResult[i].Porcentaje + '%');
                        detractorVal = data.dataResult[i].Total;
                        detractorPorciento = data.dataResult[i].Porcentaje;
                    }

                    break;
            }
        }
        var rstNpsValor = 0;
        var rstNpsPorciento = 0;

        if (promotorVal > 0) {
            rstNpsValor = promotorVal - detractorVal;
            rstNpsPorciento= promotorPorciento - detractorPorciento;
        }

        $("#spanPorcentajeNps").html(rstNpsPorciento);

        var tituloArr = data.dataLabels;
        var datosArr = data.dataTotals;

        var ctx = document.getElementById('ChartIndicadorNps');
        if (window.myChart1 != undefined) {
            window.myChart1.destroy();
        }
        window.myChart1 = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: tituloArr,
                datasets: [{
                    backgroundColor: ['rgba(135,193,232,0.96)'
                        , 'rgba(5,140,62,1)'
                        , 'rgba(210,0,0,0.97)'
                        , 'rgba(11,216,219,0.95)'
                        , 'rgba(54,146,207,1)'
                        , 'rgba(237,26,51,0.95)'
                        , 'rgba(127,235,85,0.7)'
                        , 'rgba(97,18,97,0.95)'
                        , 'rgba(230,208,43,0.86)'
                        , 'rgba(219, 91, 17, 0.78)',],
                    data: datosArr
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
                    text: 'Indicador NPS General',
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
                            return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString();
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
                                return '' + args.value.toString();
                            },
                            arc: false,
                            fontColor: '#000',
                            position: 'outside',
                        }
                    ],
                },
            },
        });
    });
}

function ConsultaTrazabilidad() {

    GeneralFn.BarraProgreso_Abrir();

    var lstUEN = dxLstBoxUENTrazabilidad.GetSelectedValues();
    var lstEstatus = dxLstBoxEstatusTrazabilidad.GetSelectedValues();
    var strRik = dxCmbRikTrazabilidad.GetValue();

    if (lstEstatus.length == 0) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione al menos estatus.");
        return false;
    }

    if (lstUEN.length == 0) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione al menos UEN.");
        return false;
    }

    var jsDate = fechaIniTrazabilidad.GetDate();

    if (jsDate === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha inicial");
        return false;
    }

    var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
    var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month < 10) {
        var myDate = `${day}/0${month}/${year}`
    } else {
        var myDate = `${day}/${month}/${year}`
    }
    var jsDate2 = fechaFinTrazabilidad.GetDate();

    if (jsDate2 === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha final");
        return false;
    }

    var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
    var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month2 < 10) {
        var myDateFinal = `${day2}/0${month2}/${year2}`
    } else {
        var myDateFinal = `${day2}/${month2}/${year2}`
    }

    if (jsDate.getTime() > jsDate2.getTime()) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("La fecha inicial es mayor a la fecha final");
        return false;
    }

    var dataValue = {
        "mesAnioInicial": myDate,
        "mesAniofinal": myDateFinal,
        "strEstatus": lstEstatus.join(', '),
        "strUEN": lstUEN.join(', '),
        "strRik": strRik
    };


    GeneralFn.HttpRequest(
        "CapNps_Reportes.aspx/IndicadorTrazabilidad",
        dataValue
    ).then(function (response) {
        if (response === null)
            return false;
        if (response != null && response.d != null) {
            var data = response.d.objResultado;

            if (response.d.boolResult == false) {
                GeneralFn.BarraProgreso_Cerrar();
                GeneralFn.MensajeAlerta(response.d.strMensaje);
                return false;
            }

            $('#chartTrazabilidad').show();
            $('#tblTrazabilidad').show();
            $('#iconTrazabilidad').show();
            $('#btnExportarTrazabilidadGlobal').show();

            // LLenar tabla
            for (var i = 0; i < data.dataResult.length; i++) {

                switch (data.dataResult[i].Id_Nps_Estatus) {
                    case 1:
                        if (data.dataResult[i].Total == 0) {
                            $("#spanStatus1Valor").html("");
                            $("#spanStatus1Porcentaje").html("");
                        } else {
                            $("#spanStatus1Valor").html(data.dataResult[i].Total);
                            $("#spanStatus1Porcentaje").html(data.dataResult[i].Porcentaje + '%');
                        }
                        break;
                    case 2:
                        if (data.dataResult[i].Total == 0) {
                            $("#spanStatus2Valor").html("");
                            $("#spanStatus2Porcentaje").html("");
                        } else {
                            $("#spanStatus2Valor").html(data.dataResult[i].Total);
                            $("#spanStatus2Porcentaje").html(data.dataResult[i].Porcentaje + '%');
                        }
                        break;
                    case 3:
                        if (data.dataResult[i].Total == 0) {
                            $("#spanStatus3Valor").html("");
                            $("#spanStatus3Porcentaje").html("");
                        } else {
                            $("#spanStatus3Valor").html(data.dataResult[i].Total);
                            $("#spanStatus3Porcentaje").html(data.dataResult[i].Porcentaje + '%');
                        }
                        break;
                    case 4:
                        if (data.dataResult[i].Total == 0) {
                            $("#spanStatus4Valor").html("");
                            $("#spanStatus4Porcentaje").html("");
                        } else {
                            $("#spanStatus4Valor").html(data.dataResult[i].Total);
                            $("#spanStatus4Porcentaje").html(data.dataResult[i].Porcentaje + '%');
                        }
                    case 5:
                        if (data.dataResult[i].Total == 0) {
                            $("#spanStatus5Valor").html("");
                            $("#spanStatus5Porcentaje").html("");
                        } else {
                            $("#spanStatus5Valor").html(data.dataResult[i].Total);
                            $("#spanStatus5Porcentaje").html(data.dataResult[i].Porcentaje + '%');
                        }
                        break;

                }
            }

            var htmltbody = "";
            var valor1, valor2, valor3, valor4, valor5;
            var Porcentaje1, Porcentaje2, Porcentaje3, Porcentaje4, Porcentaje5;

            for (var i = 0; i < data.dataDtlClientes.length; i++) {
                if (data.dataDtlClientes[i].Total_Estatus1 == 0) {
                    valor1 = "";
                    Porcentaje1 = "";
                } else {
                    valor1 = data.dataDtlClientes[i].Total_Estatus1;
                    Porcentaje1 = data.dataDtlClientes[i].Porcentaje_Estatus1 + " %";
                }

                if (data.dataDtlClientes[i].Total_Estatus2 == 0) {
                    valor2 = "";
                    Porcentaje2 = "";
                } else {
                    valor2 = data.dataDtlClientes[i].Total_Estatus2;
                    Porcentaje2 = data.dataDtlClientes[i].Porcentaje_Estatus2 + " %";
                }

                if (data.dataDtlClientes[i].Total_Estatus3 == 0) {
                    valor3 = "";
                    Porcentaje3 = "";
                } else {
                    valor3 = data.dataDtlClientes[i].Total_Estatus3;
                    Porcentaje3 = data.dataDtlClientes[i].Porcentaje_Estatus3 + " %";
                }

                if (data.dataDtlClientes[i].Total_Estatus4 == 0) {
                    valor4 = "";
                    Porcentaje4 = "";
                } else {
                    valor4 = data.dataDtlClientes[i].Total_Estatus4;
                    Porcentaje4 = data.dataDtlClientes[i].Porcentaje_Estatus4 + " %";
                }

                if (data.dataDtlClientes[i].Total_Estatus5 == 0) {
                    valor5 = "";
                    Porcentaje5 = "";
                } else {
                    valor5 = data.dataDtlClientes[i].Total_Estatus5;
                    Porcentaje5 = data.dataDtlClientes[i].Porcentaje_Estatus5 + " %";
                }

                htmltbody += "<tr>";
                htmltbody += "<td class='text-left'>" + data.dataDtlClientes[i].Cte_NomComercial + "</td>";
                htmltbody += "<td>" + valor1 + "</td>";
                htmltbody += "<td>" + Porcentaje1 + "</td>";
                htmltbody += "<td>" + valor2 + "</td>";
                htmltbody += "<td>" + Porcentaje2 + "</td>";
                htmltbody += "<td>" + valor3 + "</td>";
                htmltbody += "<td>" + Porcentaje3 + "</td>";
                htmltbody += "<td>" + valor5 + "</td>";
                htmltbody += "<td>" + Porcentaje5 + "</td>";
                htmltbody += "<td>" + valor4 + "</td>";
                htmltbody += "<td>" + Porcentaje4 + "</td>";
                htmltbody += "</tr>";
            }

            $("#tblTrazabilidadCliente tbody").html(htmltbody);

            if (htmltbody == "") {
                $('#btnExportarTrazabilidadCliente').hide();
            } else {
                $('#btnExportarTrazabilidadCliente').show();
            }

            var tituloArr = data.dataLabels;
            var datosArr = data.dataTotals;

            var ctx = document.getElementById('chartTrazabilidad');
            if (window.myChart2 != undefined) {
                window.myChart2.destroy();
            }
            window.myChart2 = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: tituloArr,
                    datasets: [{
                        backgroundColor: ['rgba(135,193,232,0.96)'
                            , 'rgba(5,140,62,1)'
                            , 'rgba(248,143,255,0.97)'
                            , 'rgba(11,216,219,0.95)'
                            , 'rgba(127,235,85,0.7)'
                            , 'rgba(97,18,97,0.95)'
                            , 'rgba(230,208,43,0.86)'
                            , 'rgba(219, 91, 17, 0.78)',],
                        data: datosArr
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
                        text: 'Indicador Trazabilidad',
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
                                return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString();
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
                                    return '' + args.value.toString();
                                },
                                arc: false,
                                fontColor: '#000',
                                position: 'outside',
                            }
                        ],
                    },
                },
            });

        }

    });


}

function ShowTrazabilidadCliente() {

    if ($('#iconTrazabilidadCliente').hasClass('fa-plus-square')) {
        $("#contenedorTblTrazabilidadCliente").show();
        $('#iconTrazabilidad span').html(' Ocultar detalle por cliente');
        $('#iconTrazabilidadCliente').removeClass('fa-plus-square');
        $('#iconTrazabilidadCliente').addClass('fa-minus-square');
    } else {
        $("#contenedorTblTrazabilidadCliente").hide();
        $('#iconTrazabilidad span').html(' Ver detalle por cliente');
        $('#iconTrazabilidadCliente').removeClass('fa-minus-square');
        $('#iconTrazabilidadCliente').addClass('fa-plus-square');
    }
}

function ConsultaConversion() {
    GeneralFn.BarraProgreso_Abrir();

    var lstUEN = dxLstBoxUENConversion.GetSelectedValues();
    var strRik = dxCmbRikConversion.GetValue();

    if (lstUEN.length == 0) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione al menos UEN.");
        return false;
    }

    var jsDate = FechaIniConversion.GetDate();

    if (jsDate === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha inicial");
        return false;
    }

    var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
    var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month < 10) {
        var myDate = `${day}/0${month}/${year}`
    } else {
        var myDate = `${day}/${month}/${year}`
    }
    var jsDate2 = FechaFinConversion.GetDate();

    if (jsDate2 === null) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("Seleccione la fecha final");
        return false;
    }

    var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
    var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
    var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
    if (month2 < 10) {
        var myDateFinal = `${day2}/0${month2}/${year2}`
    } else {
        var myDateFinal = `${day2}/${month2}/${year2}`
    }

    if (jsDate.getTime() > jsDate2.getTime()) {
        GeneralFn.BarraProgreso_Cerrar();
        GeneralFn.MensajeAlerta("La fecha inicial es mayor a la fecha final");
        return false;
    }

    var dataValue = {
        "mesAnioInicial": myDate,
        "mesAniofinal": myDateFinal,
        "strUEN": lstUEN.join(', '),
        "strRik": strRik
    };

    GeneralFn.HttpRequest(
        "CapNps_Reportes.aspx/IndicadorConversion",
        dataValue
    ).then(function (response) {
        if (response === null) {
            return false;
        }
        data = response.d.objResultado;
        $('#tblConversion').show();
        $('#btnExportarConversion').show();


        // LLenar tabla
        var htmltbody = "";
        var htmlIcon = "";
        for (var i = 0; i < data.length; i++) {
            if (data[i].Valor_Conversion > 0) {
                htmlIcon = "<i class='fa fa-arrow-up text-success'></i> ";
            } else if (data[i].Valor_Conversion < 0) {
                htmlIcon = "<i class='fa fa-arrow-down text-danger'></i> ";
            } else {
                htmlIcon = " ";
            }
            htmltbody += "<tr>";
            htmltbody += "<td class='text-left'>" + data[i].Cte_NomComercial + "</td>";
            htmltbody += "<td>" + data[i].Valor_Inicial + "</td>";
            htmltbody += "<td>" + data[i].Tipo_Inicial + "</td>";
            htmltbody += "<td>" + data[i].Valor_Final + "</td>";
            htmltbody += "<td>" + data[i].Tipo_Final + "</td>";
            htmltbody += "<td><h4>" + htmlIcon + data[i].Valor_Conversion + "</h4></td>";
            htmltbody += "<td><h4>" + data[i].Porcentaje_Conversion + " %</h4></td>";
            htmltbody += "</tr>";
        }
        $("#tblConversion tbody").html(htmltbody);


    });
}

function OnChangedRikFiltro(sender, eventArgs) {

}

function SoloNumericoYDiagonal(sender, eventArgs) {
    var c = eventArgs.get_keyCode();

    if (c && c == 13)
        eventArgs.set_cancel(true);
    if ((c < 48 || c > 57))//si no es numero
        if (c != 47) //si no es punto
            eventArgs.set_cancel(true);
}