// CapOrdenServicio_Monitor.js
// Inicialización y comportamiento de la página de Monitoreo de Orden de Servicio
// Estándares: camelCase para variables JS, uso de const/let, comentarios claros.

// Reemplazo para alertify.error
var originalError = alertify.error;
alertify.error = function (message, timeout) {
    if (timeout === undefined) timeout = 5000;

    // Fix para problemas de visualización
    var result = originalError(message);

    // Intentar forzar el ancho directamente en el elemento creado
    setTimeout(function () {
        var errorElements = document.querySelectorAll('.alertify-logs > .error');
        if (errorElements.length > 0) {
            var lastError = errorElements[errorElements.length - 1];
            lastError.style.width = '360px';
            lastError.style.maxWidth = '360px';
            lastError.style.whiteSpace = 'normal';
        }
    }, 10);

    return result;
};

(function(){
    'use strict';

    // Variables filtros
    let dtFechaInicial = null;
    let dtFechaFinal = null;
    let intOrdenServicio = 0;
    let intEstatus = '';

    const $tbl = $('#tblResultados');
    let dataTable = null;
    let lstOrdenProductos = [];
    let lstOrdenCliente = [];

    function init(){
        setHoy($('#dtFechaInicial'));
        setHoy($('#dtFechaFinal'));
        cargarCatalogoEstatus();
        initTabla();
        configurarEventos();
        onBuscar();
    }

    function setHoy($ctrl){
        const hoy = new Date();
        $ctrl.val(hoy.toISOString().substring(0,10));
    }

    function configurarEventos(){
        $('#dtFechaInicial').on('change', function (e) { validarFechaInicial(); e.preventDefault(); return false; });
        $('#dtFechaFinal').on('change', function (e) { validarFechaFinal(); e.preventDefault(); return false; });
        $('#btnBuscar').on('click', function(e){ e.preventDefault(); onBuscar(); return false; });
        $('#btnExportar').on('click', function(e){ e.preventDefault(); onExportar(); return false; });

        // Botones toggle columnas usando atributo data-col
        $('#btnAddColEstatusTicket').data('col', 'strEstatus').on('click', onToggleColumn);
        $('#btnAddColTipoConfirmacion').data('col', 'strTipoConfirmacion').on('click', onToggleColumn);
        
        $('#btnAddColTicketPedido').data('col','ticketConPedido').on('click', onToggleColumn);
        $('#btnAddColFactura').data('col','factura').on('click', onToggleColumn);
        $('#btnAddColCodigoProducto').data('col','intIdPrd').on('click', onToggleColumn);
        $('#btnAddColProducto').data('col','strPrdDescripcion').on('click', onToggleColumn);
        $('#btnAddColMatrizCN').data('col','strMatriz').on('click', onToggleColumn);
        $('#btnAddColUsuarioCreador').data('col','strUsuarioCreador').on('click', onToggleColumn);
        $('#btnAddColMotivoIncompleto').data('col','strMotivoIncompleto').on('click', onToggleColumn);
        $('#btnAddColMotivoCambioFecha').data('col','strMotivoCambioFecha').on('click', onToggleColumn);
        $('#btnAddColUsuarioAsignado').data('col','strUsuarioAsignado').on('click', onToggleColumn);
    }

    function validarFechaFinal() {
        const fi = $('#dtFechaInicial').val();
        const ff = $('#dtFechaFinal').val();
        if (fi && ff && ff < fi) {
            alertify.success('La fecha final debe ser mayor a la fecha inicial');
            return false;
        }
        return true;
    }

    function validarFechaInicial() {
        const fi = $('#dtFechaInicial').val();
        const ff = $('#dtFechaFinal').val();
        if (fi && ff && fi > ff) {
            alertify.success('La fecha inicial debe ser menor a la fecha final');
            return false;
        }
        return true;
    }
    function cargarCatalogoEstatus(){
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/CatalogoEstatus?intRef1=1&intRef2=2&intRef3=3')
            .then(function(data){
                const ddl = $('#ddlEstatus');
                ddl.empty().append('<option value="">Todos</option>');
                (data||[]).forEach(x=>ddl.append(`<option value="${x.Id}">${x.Descripcion}</option>`));
            })
            .catch(function(err){ 
                console.error('Error catálogo estatus:', err);
            });
    }

    function leerFiltros(){
        dtFechaInicial = $('#dtFechaInicial').val();
        dtFechaFinal = $('#dtFechaFinal').val();
        intOrdenServicio = parseInt($('#txtOrdenServicio').val()||'0');
        intEstatus = $('#ddlEstatus').val();
    }

    function validarRangoFechas(){
        const fi = $('#dtFechaInicial').val();
        const ff = $('#dtFechaFinal').val();
        if(fi && ff && fi>ff){
            alertify.error('La fecha inicial debe ser menor o igual a la fecha final');
            return false;
        }
        return true;
    }

    function initTabla(){
        dataTable = $tbl.DataTable({
            paging: true,
            searching: false, // quitar campo Search
            info: true,
            pageLength: 50, // mostrar 50 por defecto
            lengthMenu: [[10,25,50,100],[10,25,50,100]],
            // Colocar selector de entradas (l) en el pie junto con paginación (p) e info (i)
            dom: 'rt<"row"<"col-sm-4"l><"col-sm-4"p><"col-sm-4"i>>',
            columns: [
                { data:'intIdCte' },
                { data:'strCliente' },
                { data:'intIdSrv' },
                { data:'strEstatus', visible: false },
                { data: 'strTipoConfirmacion', visible: false },
                { data: 'strMatriz', visible: false },
                { data: 'strUsuarioCreador', visible: false },
                { data: 'strUsuarioAsignado', visible: false },
                { data: 'strMotivoIncompleto', visible: false },
                { data: 'strMotivoCambioFecha', visible: false },
                { data:'dateCaptura' },
                { data:'dateCompromiso' },
                { data:'dateConfirmacion' },
                { data: 'intTiempoConfirmacion' },
                { data: 'intIdPrd', visible: false, orderable: false },
                { data: 'strPrdDescripcion', visible: false, orderable: false },                
                { data: 'unidades', orderable: false },
                { data: 'monto', orderable: false }
                
            ],                  
            data: []          
        });
        

    }

    // Evento toggle de columna
    function onToggleColumn(){
        const $btn = $(this);
        const dataSrc = $btn.data('col');
        toggleColumnaDataSrc(dataSrc, $btn);
        actualizarTabla();
    }

    function toggleColumnaDataSrc(dataSrc, $btn){
        let targetIdx = null;
        dataTable.columns().every(function(idx){
            if(this.dataSrc() === dataSrc){ targetIdx = idx; return false; }
        });
        if(targetIdx === null) return;
        const col = dataTable.column(targetIdx);
        const visibleAhora = col.visible();
        col.visible(!visibleAhora);
        actualizarBotonColumna($btn, !visibleAhora);
    }

    function actualizarBotonColumna($btn, activo){
        const $icon = $btn.find('i.fa');
        if($icon.length){ $icon.removeClass('fa-plus fa-minus'); }
        if(activo){
            $btn.removeClass('btn-info').addClass('btn-default');
            if($icon.length){ $icon.addClass('fa-minus'); } else { $btn.prepend('<i class="fa fa-minus"></i> '); }
        }else{
            $btn.removeClass('btn-default').addClass('btn-info');
            if($icon.length){ $icon.addClass('fa-plus'); } else { $btn.prepend('<i class="fa fa-plus"></i> '); }
        }
    }

    function onBuscar(){
        if(!validarRangoFechas()) return false;
        leerFiltros();
        const qs = new URLSearchParams({
            dateFechaInicial: dtFechaInicial,
            dateFechaFinal: dtFechaFinal,
            intIdOrdenServicioInicial: intOrdenServicio||0,
            intIdOrdenServicioFinal: intOrdenServicio||0,
            intIdEstatus: intEstatus||0
        });
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/BuscarOrdenServicioMonitor?'+qs.toString())
            .then(function(data) {
                lstOrdenProductos = data.lstOrdenProductos;
                lstOrdenCliente = data.lstOrdenCliente;
                // funcion actualizar tabla para resiclar el codigo
                actualizarTabla();
                actualizarResumen(data.entIndicadores);
            })
            .catch(function(err){ 
                console.error('Error búsqueda:', err);
            });
    }

    function actualizarTabla(){
        // Determinar si las columnas de producto están ocultas
        let colIntIdPrdVisible = false;
        let colStrPrdDescripcionVisible = false;

        dataTable.columns().every(function (idx) {
            const dataSrc = this.dataSrc();
            if (dataSrc === 'intIdPrd') {
                colIntIdPrdVisible = this.visible();
            }
            if (dataSrc === 'strPrdDescripcion') {
                colStrPrdDescripcionVisible = this.visible();
            }
        });

        // Si ambas columnas están ocultas, adaptamos para cliente
        let isAdaptadoCliente = !colIntIdPrdVisible && !colStrPrdDescripcionVisible;

        // Adaptar datos para la tabla clientes
        let adaptado;
        if (isAdaptadoCliente) {
            adaptado = (lstOrdenCliente || []).map(x => ({
                intIdCte: x.intIdCte,
                strCliente: x.strCliente,
                intIdSrv: x.intIdSrv,
                strEstatus: x.strEstatus,
                strTipoConfirmacion: x.strTipoConfirmacion, 
                strMatriz: x.strMatriz,
                strUsuarioCreador: x.strUsuarioCreador || '',
                strUsuarioAsignado: x.strUsuarioAsignado || '',
                strMotivoIncompleto: x.strMotivoIncompleto || '',
                strMotivoCambioFecha: x.strMotivoCambioFecha || '',
                dateCaptura: x.dateCaptura ? formatDate(x.dateCaptura) : '',
                dateCompromiso: x.dateCompromiso ? formatDate(x.dateCompromiso) : '',
                dateConfirmacion: x.dateConfirmacion ? formatDate(x.dateConfirmacion) : '',
                intTiempoConfirmacion: x.intTiempoConfirmacion,
                intIdPrd: 0,
                strPrdDescripcion: '',
                unidades: x.intUnidades_Cliente || 0,
                monto: x.dcmTotal_Cliente || 0
                               
            }));
        } else {
            // Adaptar datos para la tabla productos
            adaptado = (lstOrdenProductos || []).map(x => ({
                intIdCte: x.intIdCte,
                strCliente: x.strCliente,
                intIdSrv: x.intIdSrv,
                strEstatus: x.strEstatus,
                strTipoConfirmacion: x.strTipoConfirmacion, 
                strMatriz: x.strMatriz,
                strUsuarioCreador: x.strUsuarioCreador || '',
                strUsuarioAsignado: x.strUsuarioAsignado || '',
                strMotivoIncompleto: x.strMotivoIncompleto || '',
                strMotivoCambioFecha: x.strMotivoCambioFecha || '',
                dateCaptura: x.dateCaptura ? formatDate(x.dateCaptura) : '',
                dateCompromiso: x.dateCompromiso ? formatDate(x.dateCompromiso) : '',
                dateConfirmacion: x.dateConfirmacion ? formatDate(x.dateConfirmacion) : '',
                intTiempoConfirmacion: x.intTiempoConfirmacion,
                intIdPrd: x.intIdPrd,
                strPrdDescripcion: x.strPrdDescripcion,
                unidades: x.intUnidades_Producto || 0,
                monto: x.dcmTotal_Producto || 0
                
            }));
        }
        dataTable.clear().rows.add(adaptado).draw();
        if (!isAdaptadoCliente) {
            aplicarRowspan();
        }
    }
    function aplicarRowspan() {
        // Columnas que deben tener rowspan (índices basados en la definición de columns)
        let columnasRowspan = [0, 1, 2]; // intIdCte, strCliente, intIdSrv, strEstatus, strMatriz, dateCaptura, dateCompromiso, dateConfirmacion
        let numCol = columnasRowspan.length;

        dataTable.columns().every(function (idx) {
            const dataSrc = this.dataSrc();
            if (dataSrc === 'strEstatus') {
                if (this.visible()) {
                    columnasRowspan.push(numCol);
                    numCol++;
                }
            }
            if (dataSrc === 'strTipoConfirmacion') {
                if (this.visible()) {
                    columnasRowspan.push(numCol);
                    numCol++;
                }
            }            
            if (dataSrc === 'strMatriz') {
                if (this.visible()) {
                    columnasRowspan.push(numCol);
                    numCol++
                }
            }
        });

       
        columnasRowspan.push(numCol);
        numCol++;
        columnasRowspan.push(numCol);
        numCol++;
        columnasRowspan.push(numCol);
        // Obtener todas las filas visibles
        const rows = $tbl.find('tbody tr');

        // Objeto para rastrear el último intIdSrv procesado
        let grupoActual = {
            intIdSrv: null,
            primeraFila: null,
            contador: 0
        };

        rows.each(function (index) {
            const $row = $(this);
            const rowData = dataTable.row(this).data();

            if (!rowData) return;

            const intIdSrv = rowData.intIdSrv;

            // Si es el mismo grupo
            if (intIdSrv === grupoActual.intIdSrv) {
                grupoActual.contador++;
                // Ocultar las celdas de las columnas agrupadas
                columnasRowspan.forEach(colIdx => {
                    $row.find('td').eq(colIdx).hide();
                });
            } else {
                // Aplicar rowspan al grupo anterior si existe
                if (grupoActual.primeraFila && grupoActual.contador > 1) {
                    columnasRowspan.forEach(colIdx => {
                        grupoActual.primeraFila.find('td').eq(colIdx).attr('rowspan', grupoActual.contador);
                    });
                }

                // Iniciar nuevo grupo
                grupoActual = {
                    intIdSrv: intIdSrv,
                    primeraFila: $row,
                    contador: 1
                };
            }
        });

        // Aplicar rowspan al último grupo
        if (grupoActual.primeraFila && grupoActual.contador > 1) {
            columnasRowspan.forEach(colIdx => {
                grupoActual.primeraFila.find('td').eq(colIdx).attr('rowspan', grupoActual.contador);
            });
        }
    }
    function actualizarResumen(data){
        $('#resCompPedidos').text(data.intCompleto);
        $('#resCompMontos').text(formatMoney(data.dcmCompletoMonto));
        $('#resCompPorc').text((data.dcmCompletoPorcentaje * 100).toFixed(2) + '%');
        $('#resIncompPedidos').text(data.intIncompleto);
        $('#resIncompMontos').text(formatMoney(data.dcmIncompletoMonto));
        $('#resIncompPorc').text((data.dcmIncompletoPorcentaje * 100).toFixed(2) + '%');       
    }

    function onExportar(){
        if (!validarRangoFechas()) return false;
        leerFiltros();
        const qs = new URLSearchParams({
            dateFechaInicial: dtFechaInicial,
            dateFechaFinal: dtFechaFinal,
            intIdOrdenServicioInicial: intOrdenServicio || 0,
            intIdOrdenServicioFinal: intOrdenServicio || 0,
            intEstatus: intEstatus || 0,
            strEstatus: $('#ddlEstatus option:selected').text()
        });

        // Usar ApiHelper para descargar el archivo Excel
        ApiHelper.fetchBlob(
            ApplicationUrl + '/api/CapOrdenServicio_Admin/ExportarOrdenServicioMonitor?' + qs.toString(),
            'monitoreo_ordenes_servicio.xlsx')
            .then(function () {
                // El mensaje de éxito ya lo muestra ApiHelper
                console.log('Archivo descargado correctamente');
            })
            .catch(function (error) {
                console.error('Error al descargar:', error);
            });

        return false;
    }

    function formatDate(d){ if(!d) return ''; const dt = new Date(d); if(isNaN(dt)) return d; return dt.toISOString().substring(0,10); }
    function formatMoney(v){ return (v==null?0:v).toLocaleString('es-MX',{minimumFractionDigits:2,maximumFractionDigits:2}); }

    $(init);
})();
