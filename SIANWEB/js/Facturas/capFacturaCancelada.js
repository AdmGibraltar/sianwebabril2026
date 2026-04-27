var CapFacturaCancelada = (function () {
    var tblFacturas;

    function init() {
        configurarTabla();
        configurarEventos();
        ConsultarFacturasCanceladas();
    }

    function formatearFechaDDMMYYYY(fecha) {
        if (!fecha) return " ";
        var d = new Date(fecha);
        if (isNaN(d.getTime())) return " ";
        var dia = ("0" + d.getDate()).slice(-2);
        var mes = ("0" + (d.getMonth() + 1)).slice(-2);
        var anio = d.getFullYear();
        return `${dia}/${mes}/${anio}`;
    }

    function configurarTabla() {
        tblFacturas = $('#tblFacturas').DataTable({
            //scrollX: true,         // Permite scroll horizontal, evita que la tabla se ajuste al contenedor
            // autoajuste responsivo..
            //responsive: true,
            columns: [
                { data: 'strRfcReceptor' },
                { data: 'strRazonSocial' },
                { data: 'strSerie' },
                {
                    data: 'intFolio',
                    render: function (data) {
                        return data > 0 ? data : "";
                    }
                },
                { data: 'strFolioFiscal' },
                {
                    data: 'dtFechaEmision',
                    render: function (data) {
                        return formatearFechaDDMMYYYY(data);
                    }
                },
                {
                    data: 'dtFechaSolCanc',
                    render: function (data) {
                        return formatearFechaDDMMYYYY(data);
                    }
                },
                { data: 'strTipoDocumento' },
                { data: 'strEstadoSAT' },
                {
                    data: 'decSubtotal',
                    render: $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    data: 'decIVA',
                    render: $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    data: 'decTotal',
                    render: $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    data: 'intFolioRelacionado',
                    render: function (data) {
                        return data > 0 ? data : "";
                    }
                },
                { data: 'strSerieRelacionada' },
                { data: 'strFolioFiscalRelacionado' },
                { data: 'strTipoDocumentoRelacionado' },
                { data: 'boolEsTotal' }

            ],
            ordering: false, 
            columnDefs: [
                { targets: 16, visible: false } 
            ],
            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
            pageLength: 25,
            createdRow: function (row, data, dataIndex) {
                if (data.boolEsTotal) {
                    $(row).css('background-color', '#f2f2f2'); // gris claro
                    $(row).css('font-weight', 'bold'); // texto en negritas
                }
            }
        });
    }

    function configurarEventos() {
        $('#btnDescargarExcel').on('click', function (e) {
            e.preventDefault(); // Prevenir el comportamiento predeterminado
            DescargarReporte();
            return false; // Asegurar que no se produzca el postback
        });
    }
    

    function ConsultarFacturasCanceladas() {
        $('.loading-overlay').show();
        fetch(ApplicationUrl + '/api/FacturaCancelada/ConsultarFacturasCanceladas?str1=hola', {
            method: "GET",
            mode: "cors",
            cache: "no-cache",
            credentials: "same-origin",
            redirect: "follow",
            referrer: "no-referrer",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => {
                tblFacturas.clear();
                // ordenar el objeto lstTotalFacturaCancelada por total descendente
                var lstTotalFacturaCancelada = data.lstTotalFacturaCancelada.sort((a, b) => b.decTotal - a.decTotal);

                data.lstTotalFacturaCancelada.forEach(item => {
                    tblFacturas.row.add(item);
                    // en el segundo foreach usa la lista lstFacturaCancelada con la condición de seleccionar las facturas que tengan el mismo strRFCReceptor del ítem de lstTotalFacturaCancelada los seleccionados llenan las siguientes líneas...
                    var lstFacturaCancelada = data.lstFacturaCancelada.filter(factura => factura.strRfcReceptor === item.strRfcReceptor);
                    lstFacturaCancelada.forEach(factura => {
                        tblFacturas.row.add({
                            strRfcReceptor: factura.strRfcReceptor,
                            strRazonSocial: factura.strRazonSocial,
                            strSerie: factura.strSerie,
                            intFolio: factura.intFolio,
                            strFolioFiscal: factura.strFolioFiscal,
                            dtFechaEmision: factura.dtFechaEmision,
                            dtFechaSolCanc: factura.dtFechaSolCanc,
                            strTipoDocumento: factura.strTipoDocumento,
                            strEstadoSAT: factura.strEstadoSAT,
                            decSubtotal: factura.decSubtotal,
                            decIVA: factura.decIVA,
                            decTotal: factura.decTotal,
                            intFolioRelacionado: factura.intFolioRelacionado,
                            strSerieRelacionada: factura.strSerieRelacionada,
                            strFolioFiscalRelacionado: factura.strFolioFiscalRelacionado,
                            strTipoDocumentoRelacionado: factura.strTipoDocumentoRelacionado,
                             boolEsTotal: false
                        });
                    });

                });
                tblFacturas.draw();
                $('.loading-overlay').hide();
            })
            .catch(error => {
                alertify.error('Error al consultar las facturas canceladas.');
                $('.loading-overlay').hide();
            });
    }

    function DescargarReporte() {
        $('.loading-overlay').show();
        fetch(ApplicationUrl + '/api/FacturaCancelada/DescargarReporte?str1=1&str2=2')
            .then(response => {
                if (response.ok) {
                    return response.blob();
                }
                throw new Error('Error al descargar el reporte');
            })
            .then(blob => {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = 'reporte_facturas_canceladas.xlsx';
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
                // Liberar la URL
                window.URL.revokeObjectURL(url);
            })
            .catch(error => {
                alertify.error('Error al descargar el reporte.');
               
            }).finally(function () {
                $('.loading-overlay').hide();
            });
    }

    return {
        init: init
    };
})();

$(document).ready(function () {
    CapFacturaCancelada.init();
});
