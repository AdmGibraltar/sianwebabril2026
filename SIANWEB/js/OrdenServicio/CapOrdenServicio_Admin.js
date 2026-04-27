// Reemplazo para alertify.error
var originalError = alertify.error;
alertify.error = function (message, timeout) {
    if (timeout === undefined) timeout = 10000;

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

// Inyectar estilos para que alertify se muestre sobre los modales Bootstrap
(function ensureAlertifyZIndex(){
    if (document.getElementById('alertify-zindex-fix')) return;
    var style = document.createElement('style');
    style.id = 'alertify-zindex-fix';
    style.type = 'text/css';
    style.textContent = ''+
        '.alertify, .alertify-logs { z-index: 20000 !important; }' +
        '.alertify-logs > div { z-index: 20001 !important; }' +
        /* Ocultar valores en bootstrap-select dropdowns */
        '.bootstrap-select .dropdown-menu li small { display: none !important; }' +
        '.bootstrap-select .dropdown-menu li .text { width: 100% !important; }';
    document.head.appendChild(style);
})();

// Definir función global para selección de cliente
window.SeleccionClienteLocal = function (idCliente) {
    const cliente = window.DataLocalClientes.find(c => c.intIdCliente === idCliente);
    const dataTerritorio = (window.DataLocalTerritorio || []).filter(t => t.ValorInt === idCliente);
    //const dataUsuario = (window.DataLocalUsuario || []).filter(t => t.ValorInt === idCliente);


  
    //llenarCombo2($('#ddlUsuario'), dataUsuario, -1);
    if (cliente) {
        $('#txtIdCliente').val(cliente.intIdCliente || '');
        $('#txtNombreClienteModal').val(cliente.strNombreComercial || '');
        $('#lblSegmento').text(cliente.strSegmento || '');
        $('#lblCalle').text(cliente.strCalle || '');
        $('#lblNumero').text(cliente.strNumCalle || '');
        $('#lblColonia').text(cliente.strColonia || '');
        $('#lblMunicipio').text(cliente.strMunicipio || '');
        $('#lblEstado').text(cliente.strEstado || '');
        $('#lblRfc').text(cliente.strRFC || '');
        $('#lblTelefono').text(cliente.strTelefono || '');
    }

    OrdenServicioAdmin.llenarCombo2($('#ddlTerritorio'), dataTerritorio, -1);
    const currentTer = $('#ddlTerritorio').val();
    if (currentTer != -1) {
        $('#txtTerritorio').val(currentTer);
        $('#ddlTerritorio').trigger("change");
    }

    $('#modalClientes').modal('hide');
};



// Inicialización y lógica de administración de Orden de Servicio
var OrdenServicioAdmin = new (function(){
    'use strict';

    // Variables filtros
    let strNombreCliente = '', intIdCteInicial = '', intIdCteFinal = '', intIdEstatus = '', dateFechaInicial = '', dateFechaFinal = '', intIdOrdenServicioInicial = '', intIdOrdenServicioFinal = '';
    const $tbl = $('#tblOrdenesServicio tbody');
    // Hacer DataLocalClientes global
    window.DataLocalClientes = [];
    window.DataLocalTerritorio = [];
    window.DataLocalUsuario = [];
    // Variable global para lista de servicios
    let lstServicio = [];
    let lstUsuarios = [];
    // Variable global para catálogo de estatus
    let catalogoEstatus = [];
    // Variables globales para gestión de roles y usuarios
    let lstRoles = [];
    window.arrRolUsuario = [];
    window.arrIdRolUsuarioEliminar = [];
    // Cache de productos encontrados (Id -> {Id, Descripcion, Costo, TipoProducto})
    let cacheProductosEncontrados = [];
    
    function init(){
        // Prevenir submit del form (postback)
        $('#frmAdminOrdenServicio').on('submit', function(e){ e.preventDefault(); return false; });
        // Prevenir Enter en filtros y modal
        $(document).on('keydown', '#frmAdminOrdenServicio input, #frmAdminOrdenServicio select', function(e){
            if(e.key === 'Enter') { e.preventDefault(); return false; }
        });

        setHoy($('#dtFechaInicial'));
        setHoy($('#dtFechaFinal'));
        cargarCatalogoEstatus();
        configurarEventos();
        
        // Inicializar selectpicker para ddlServicio
        $('#ddlServicio').selectpicker({
            liveSearch: true,
            showSubtext: false,
            title: 'Seleccione',
            width: '400px',  // Forzar ancho específico
            style: 'btn-default'
        });

        onBuscar();
    }

    function setHoy($ctrl){
        const hoy = new Date();
        $ctrl.val(hoy.toISOString().substring(0,10));
    }

    function onTerritorioChanged() {
        const selectedValue = $('#ddlTerritorio').val();
        $('#txtTerritorio').val(selectedValue);

        // Obtener ID del cliente
        const intIdCte = parseInt($('#txtIdCliente').val()) || 0;
        const intIdTer = parseInt(selectedValue) || 0;
        $('#hdnIdRepServicio').val("-1");
        $('#hdnIdRepServicioTecnico').val("-1");
        $('#lblRepresentantes').text("");
        // Si hay cliente y territorio válidos, consultar representantes
        if (intIdCte > 0 && intIdTer > 0) {
            ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarRepresentantesServicio?intIdCte=' + intIdCte + '&intIdTer=' + intIdTer)
                .then(function(data){
                    let terRepresentantes = '';
                    
                    if (data && data.length > 0) {
                        data.forEach(function (item) {
                            if (item.ValorInt === 1) {
                                // Representante Servicio
                                $('#hdnIdRepServicio').val(item.Id);                               
                                terRepresentantes += 'Representante Servicio: ' + item.Id + ' - ' + item.Descripcion;
                            } else if (item.ValorInt === 2) {
                                // Representante Servicio Técnico
                                $('#hdnIdRepServicioTecnico').val(item.Id);
                                terRepresentantes += 'Representante Servicio Técnico: ' + item.Id + ' - ' + item.Descripcion;
                            }
                            terRepresentantes += '<br/>';
                        });
                        
                        $('#lblRepresentantes').html(terRepresentantes);
                        
                    }
                })
                .catch(function(err){ 
                    console.error('Error al consultar representantes:', err);
                });
        }

        return false;
    }

    function onRolChanged() {
        const selectedValue = $('#ddlRol').val();
        
        // Filtrar usuarios por el rol seleccionado
        const usuariosFiltrados = (lstUsuarios || []).filter(u => u.ValorInt == selectedValue);

        if (usuariosFiltrados.length === 0) {
            alertify.error('No hay usuarios asociados a este rol');
        } 

        let opcDefault = -1;
        if (selectedValue == "7") {
            opcDefault = parseInt($('#hdnIdRepServicioTecnico').val());
            // Llenar el dropdown de usuarios con los datos filtrados
            llenarCombo2($('#ddlUsuario'), usuariosFiltrados, opcDefault); 
            $('#ddlUsuario').trigger("change");
        } else {
            // Llenar el dropdown de usuarios con los datos filtrados
            llenarCombo2($('#ddlUsuario'), usuariosFiltrados, opcDefault);     
        }

       
       
       
        
        return false;
    }

    function onUsuarioChanged() {
        const idRol = $('#ddlRol').val();
        const rol = $('#ddlRol option:selected').text();
        const idRep = $('#ddlUsuario').val();
        const representante = $('#ddlUsuario option:selected').text();
        
        // Validar que se haya seleccionado un rol
        if (!idRol || idRol === '-1' || rol === 'Seleccione') {
            alertify.error('Debe seleccionar un rol primero');
            $('#ddlUsuario').val('-1');
            return false;
        }
        
        // Validar que se haya seleccionado un usuario
        if (!idRep || idRep === '-1' || representante === 'Seleccione') {
            return false;
        }
        
        // Validar que no se haya agregado ya la misma combinación
        const existe = window.arrRolUsuario.find(ru => ru.intIdRol == idRol && ru.intIdRep == idRep);
        if (existe) {
            alertify.error('Esta combinación de rol y usuario ya fue agregada');
            $('#ddlUsuario').val('-1');
            return false;
        }
        
        // Agregar a la tabla tblUsuario
        const $tblUsuario = $('#tblUsuario tbody');
        const tr = $('<tr/>');
        tr.append(`<td>${idRol}</td>`);
        tr.append(`<td>${html(rol)}</td>`);
        tr.append(`<td>${idRep}</td>`);
        tr.append(`<td>${html(representante)}</td>`);
        tr.append(`<td style="text-align:center;"><button type='button' class='btn btn-danger btn-sm btnQuitarUsuario'><i class='fa fa-trash'></i></button></td>`);
        
        // Guardar datos como data attribute (intIdRolUsuario = 0 para nuevos)
        tr.data('idRolUsuario', 0);
        tr.data('idRol', idRol);
        tr.data('rol', rol);
        tr.data('idRep', idRep);
        tr.data('representante', representante);
        
        $tblUsuario.append(tr);
        
        // Agregar al array global
        window.arrRolUsuario.push({
            intIdRolUsuario: 0,
            intIdRol: parseInt(idRol),
            strRol: rol,
            intIdRep: parseInt(idRep),
            strRepresentante: representante
        });
        
        // Evento para quitar fila
        tr.find('.btnQuitarUsuario').on('click', function(){
            const $fila = $(this).closest('tr');
            const idRolUsuario = $fila.data('idRolUsuario') || 0;
            const idRolFila = $fila.data('idRol');
            const idRepFila = $fila.data('idRep');
            
            // Si es un registro existente (idRolUsuario > 0), agregar al array de eliminación
            if (idRolUsuario > 0) {
                window.arrIdRolUsuarioEliminar.push(idRolUsuario);
            }
            
            // Remover del array global
            window.arrRolUsuario = window.arrRolUsuario.filter(ru => 
                !(ru.intIdRol == idRolFila && ru.intIdRep == idRepFila)
            );
            
            // Remover visualmente la fila
            $fila.remove();
        });
        
        // Limpiar selección de usuario
        $('#ddlUsuario').val('-1');
        
        return false;
    }

    function onServicioChanged() {
        const servicioId = $('#ddlServicio').val();
        const servicioText = $('#ddlServicio option:selected').text();
        
        // SOLUCIÓN: Obtener data-costo del option original, no del select
        const costo = $('#ddlServicio option:selected').data('costo') || 0;
        
        if (servicioId && servicioText !== 'Seleccione') {
            // Buscar la primera fila de la tabla de detalle
            const $primeraFila = $('#tblDetalleServicios tbody tr:first');
            if ($primeraFila.length > 0) {
                $primeraFila.find('.txtIdPrd').val(servicioId);
                $primeraFila.find('.txtServicioDet').val(servicioText);
                $primeraFila.find('.txtCosto').val(costo);
                $primeraFila.find('.txtTipoProducto').val(1); // Asignar TipoProducto = 1 (Servicio)
                // Recalcular total si la fila tiene cantidad
                recalcularTotal($primeraFila);
            }
        }
        
        return false;
    }

    function LlenarDescripcion($input) {
        const valorCapturado = $input.val().trim();
        if (!valorCapturado) return;

        // Validar si el valor ya existe en otras filas
        const $filaActual = $input.closest('tr');
        let isValeusado = false;

        $('#tblDetalleServicios tbody tr').each(function() {
            const $fila = $(this);
            if ($fila[0] !== $filaActual[0]) { // No comparar con la fila actual
                const valorEnFila = $fila.find('.txtIdPrd').val().trim();
                if (valorEnFila === valorCapturado) {
                    isValeusado = true;
                    return false; // break del each
                }
            }
        });

        if (isValeusado) {
            alertify.error('Este servicio ya fue capturado en otra fila');
            $input.val('');
            $filaActual.find('.txtServicioDet').val('');
            return;
        }

        // Buscar en la lista de servicios
        const servicio = lstServicio.find(s => s.Id_Prd == valorCapturado || s.Id == valorCapturado);
        if (servicio) {
            $filaActual.find('.txtServicioDet').val(servicio.Descripcion || '');
            $filaActual.find('.txtCosto').val(servicio.Costo || 0);
            $filaActual.find('.txtTipoProducto').val(servicio.TipoProducto || 0);
            // Recalcular total
            recalcularTotal($filaActual);
        } else {
            // Si no se encuentra en la lista local, buscar en la base de datos vía API
            ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarIdProducto?intIdProducto=' + valorCapturado)
                .then(function (data) {
                    if (data && data.Id) {
                        // Producto encontrado en la BD
                        $filaActual.find('.txtServicioDet').val(data.Descripcion || '');
                        $filaActual.find('.txtCosto').val(data.Costo || 0);
                        $filaActual.find('.txtTipoProducto').val(data.TipoProducto || 0);

                        // Agregar al cache de productos encontrados
                        const productoExisteEnCache = cacheProductosEncontrados.find(p => p.Id == valorCapturado);
                        if (!productoExisteEnCache) {
                            cacheProductosEncontrados.push({
                                Id: data.Id,
                                Descripcion: data.Descripcion || '',
                                Costo: data.Costo || 0,
                                TipoProducto: data.TipoProducto || 0
                            });
                        }

                        // Recalcular total
                        recalcularTotal($filaActual);
                        //alertify.success('Producto encontrado');
                    } else {
                        // Producto no encontrado
                        let strMensaje='';
                        if (data.mensaje)
                            strMensaje = data.mensaje;
                        alertify.error('Servicio/Producto no encontrado. ' + strMensaje) ;
                        $input.val('');
                        $filaActual.find('.txtServicioDet').val('');
                        $filaActual.find('.txtCosto').val('');
                        $filaActual.find('.txtTotal').val('');
                        $filaActual.find('.txtTipoProducto').val(0);

                    }
                })
                .catch(function (err) {
                    // Error en la consulta o producto no encontrado
                    console.error('Error al consultar producto:', err);
                    alertify.error('Servicio/Producto no encontrado');
                    $input.val('');
                    $filaActual.find('.txtServicioDet').val('');
                    $filaActual.find('.txtCosto').val('');
                    $filaActual.find('.txtTotal').val('');
                    $filaActual.find('.txtTipoProducto').val(0);

                });

        }
    }

    function LlenarIdFromDescripcion($input) {
        const descripcionCapturada = $input.val().trim();
        if (!descripcionCapturada) return;

        const $filaActual = $input.closest('tr');
        const idProductoCapturado = $filaActual.find('.txtIdPrd').val().trim();

        // PASO 1: Validar primero si la descripción corresponde al producto con el ID ya capturado en el cache
        if (idProductoCapturado) {
            const productoCacheado = cacheProductosEncontrados.find(p => 
                p.Id == idProductoCapturado && 
                p.Descripcion && 
                p.Descripcion.toLowerCase() === descripcionCapturada.toLowerCase()
            );

            if (productoCacheado) {
                // La descripción coincide con el producto del ID capturado, no hacer nada más
                return;
            }
        }

        // PASO 2: Si no coincide con el cache, buscar en la lista de servicios por descripción
        const servicio = lstServicio.find(s => s.Descripcion && s.Descripcion.toLowerCase() === descripcionCapturada.toLowerCase());
        if (servicio) {
            // Validar si el ID ya existe en otras filas
            let Existe = false;
            $('#tblDetalleServicios tbody tr').each(function () {
                const $fila = $(this);
                if ($fila[0] !== $filaActual[0]) { // No comparar con la fila actual
                    const valorEnFila = $fila.find('.txtIdPrd').val().trim();
                    const idServicio = servicio.Id_Prd || servicio.Id;
                    if (valorEnFila === idServicio.toString()) {
                        Existe = true;
                        return false; // break del each
                    }
                }
            });

            if (Existe) {
                alertify.error('Este servicio ya fue capturado en otra fila');
                $input.val('');
                $filaActual.find('.txtIdPrd').val('');
                $filaActual.find('.txtCosto').val('');
                $filaActual.find('.txtTotal').val('');
                $filaActual.find('.txtTipoProducto').val(0);
                return;
            }

            $filaActual.find('.txtIdPrd').val(servicio.Id_Prd || servicio.Id || '');
            $filaActual.find('.txtCosto').val(servicio.Costo || 0);
            $filaActual.find('.txtTipoProducto').val(servicio.TipoProducto || 1); // Asignar TipoProducto del servicio
            // Recalcular total
            recalcularTotal($filaActual);
        } else {
            alertify.error('Servicio/Producto no encontrado');
            $input.val('');
            $filaActual.find('.txtServicioDet').val('');
            $filaActual.find('.txtCosto').val('');
            $filaActual.find('.txtTotal').val('');
            $filaActual.find('.txtTipoProducto').val(0);
        }
    }

    function recalcularTotal($fila) {
        const cantidad = parseFloat($fila.find('.txtCantidad').val()) || 0;
        const costo = parseFloat($fila.find('.txtCosto').val()) || 0;
        const total = cantidad * costo;
        $fila.find('.txtTotal').val(total.toFixed(2));
    }

    function inicializarAutocomplete() {
        // Inicializar autocomplete para elementos .txtServicioDet existentes y futuros
        $(document).off('focus.autocomplete', '.txtServicioDet').on('focus.autocomplete', '.txtServicioDet', function() {
            const $this = $(this);
            if (!$this.hasClass('ui-autocomplete-input')) {
                $this.autocomplete({
                    source: function(request, response) {
                        const term = request.term.toLowerCase();
                        const matches = lstServicio.filter(function(item) {
                            return item.Descripcion && item.Descripcion.toLowerCase().indexOf(term) !== -1;
                        }).map(function(item) {
                            return {
                                label: item.Descripcion,
                                value: item.Descripcion,
                                id: item.Id
                            };
                        });
                        response(matches);
                    },
                    minLength: 2,
                    select: function(event, ui) {
                        const $filaActual = $(this).closest('tr');
                        $filaActual.find('.txtIdPrd').val(ui.item.id || '');
                        $filaActual.find('.txtCosto').val(ui.item.costo || 0);
                        return true;
                    }
                });
            }
        });
    }

    function configurarEventos(){
        $('#dtFechaInicial').on('change', function (e) { validarFechaInicial(); e.preventDefault(); return false; });
        $('#dtFechaFinal').on('change', function (e) { validarFechaFinal(); e.preventDefault(); return false; });
        // Nuevo: validar fecha compromiso > hoy
        $('#dtFechaCompromiso').on('change', function(e){ validarFechaCompromiso(); e.preventDefault(); return false; });
        // Nuevo: evento change de ddlServicio
        $('#ddlServicio').on('changed.bs.select', function(e){ onServicioChanged(); e.preventDefault(); return false; });
        // Evento blur para .txtIdPrd (usar delegación de eventos)
        $(document).on('blur', '.txtIdPrd', function(e){ LlenarDescripcion($(this)); e.preventDefault(); return false; });
        // Evento blur para .txtServicioDet (autocomplete)
        $(document).on('blur', '.txtServicioDet', function(e){ LlenarIdFromDescripcion($(this)); e.preventDefault(); return false; });
        $('#btnBuscar').on('click', function(e){ e.preventDefault(); onBuscar(); return false; });
        $('#btnExportar').on('click', function(e){ e.preventDefault(); onExportar(); return false; });
        $('#btnNuevo').on('click', function(e){ e.preventDefault(); onNuevo(); return false; });
        $('#btnAgregarFilaDet').on('click', function(e){ e.preventDefault(); agregarFilaDetalle(); return false; });
        $('#btnGuardarOrdenServicio').on('click', function(e){ e.preventDefault(); onGuardar(); return false; });
        $('#btnBuscarCliente').on('click', function(e){ e.preventDefault(); onBuscarCliente(); return false; });
        // Nuevo: onchange de txtIdCliente
        $('#txtIdCliente').on('change', function(e){ e.preventDefault(); SeleccionarCliente(); return false; });
        $('#ddlTerritorio').on('change', function (e) { e.preventDefault(); onTerritorioChanged(); return false; });
        $('#ddlRol').on('change', function (e) { e.preventDefault(); onRolChanged(); return false; });
        // Nuevo: evento change de ddlUsuario para agregar a tabla
        $('#ddlUsuario').on('change', function (e) { e.preventDefault(); onUsuarioChanged(); return false; });
        
        // Eventos del modal de confirmación
        $('input[name="rdConfirmacion"]').on('change', function(e){ onConfirmacionChanged(); e.preventDefault(); return false; });
        $('#btnGuardaConfirmacion').on('click', function(e){ e.preventDefault(); onGuardarConfirmacion(); return false; });
        
        // Eventos del modal de eliminación
        $('#btnConfirmarEliminacion').on('click', function(e){ e.preventDefault(); onConfirmarEliminacion(); return false; });
        
        // Validaciones de rangos de clientes - funciones individuales
        $('#txtClienteInicial').on('blur', function(e){ validarClienteInicial(); e.preventDefault(); return false; });
        $('#txtClienteFinal').on('blur', function(e){ validarClienteFinal(); e.preventDefault(); return false; });
        
        // Validaciones de rangos de orden de servicio - funciones individuales
        $('#txtOrdenServicioInicial').on('blur', function(e){ validarOrdenServicioInicial(); e.preventDefault(); return false; });
        $('#txtOrdenServicioFinal').on('blur', function(e){ validarOrdenServicioFinal(); e.preventDefault(); return false; });
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
    function validarRangoFechas(){
        const fi = $('#dtFechaInicial').val();
        const ff = $('#dtFechaFinal').val();
        if(fi && ff && fi>ff){
            alertify.error('La fecha inicial debe ser menor o igual a la fecha final');
            return false;
        }
        return true;
    }

    // Validar Cliente Inicial
    function validarClienteInicial(){
        const inicial = $('#txtClienteInicial').val();
        const final = $('#txtClienteFinal').val();
        
        // Solo validar si ambos campos tienen valor
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('El cliente inicial no puede ser mayor que el cliente final');
                    // Devolver focus al campo que activó la validación
                    setTimeout(function() {
                        $('#txtClienteInicial').focus().select();
                    }, 100);
                    return false;
                }
            }
        }
        return true;
    }
    
    // Validar Cliente Final
    function validarClienteFinal(){
        const inicial = $('#txtClienteInicial').val();
        const final = $('#txtClienteFinal').val();
        
        // Solo validar si ambos campos tienen valor
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('El cliente final no puede ser menor que el cliente inicial');
                    // Devolver focus al campo que activó la validación
                    setTimeout(function() {
                        $('#txtClienteFinal').focus().select();
                    }, 100);
                    return false;
                }
            }
        }
        return true;
    }
    
    // Validar Orden Servicio Inicial
    function validarOrdenServicioInicial(){
        const inicial = $('#txtOrdenServicioInicial').val();
        const final = $('#txtOrdenServicioFinal').val();
        
        // Solo validar si ambos campos tienen valor
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('La orden de servicio inicial no puede ser mayor que la orden de servicio final');
                    // Devolver focus al campo que activó la validación
                    setTimeout(function() {
                        $('#txtOrdenServicioInicial').focus().select();
                    }, 100);
                    return false;
                }
            }
        }
        return true;
    }
    
    // Validar Orden Servicio Final
    function validarOrdenServicioFinal(){
        const inicial = $('#txtOrdenServicioInicial').val();
        const final = $('#txtOrdenServicioFinal').val();
        
        // Solo validar si ambos campos tienen valor
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('La orden de servicio final no puede ser menor que la orden de servicio inicial');
                    // Devolver focus al campo que activó la validación
                    setTimeout(function() {
                        $('#txtOrdenServicioFinal').focus().select();
                    }, 100);
                    return false;
                }
            }
        }
        return true;
    }
    
    // Validar rango de clientes (para onBuscar)
    function validarRangoClientes(){
        const inicial = $('#txtCliente Inicial').val();
        const final = $('#txtClienteFinal').val();
        
        // Si ambos campos tienen valor, validar
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('El cliente inicial no puede ser mayor que el cliente final');
                    $('#txtClienteInicial').focus();
                    return false;
                }
            }
        }
        return true;
    }
    
    // Validar rango de órdenes de servicio (para onBuscar)
    function validarRangoOrdenServicio(){
        const inicial = $('#txtOrdenServicioInicial').val();
        const final = $('#txtOrdenServicioFinal').val();
        
        // Si ambos campos tienen valor, validar
        if(inicial && final){
            const numInicial = parseInt(inicial);
            const numFinal = parseInt(final);
            
            if(!isNaN(numInicial) && !isNaN(numFinal)){
                if(numInicial > numFinal){
                    alertify.success('La orden de servicio inicial no puede ser mayor que la orden de servicio final');
                    $('#txtOrdenServicioInicial').focus();
                    return false;
                }
            }
        }
        return true;
    }

    // Nuevo: validar que la fecha de compromiso sea mayor a la fecha actual (hoy)
    function validarFechaCompromiso(){
        const val = $('#dtFechaCompromiso').val();
        if(!val) return true; // nada que validar
        
        // Crear fecha de hoy sin horas
        const hoy = new Date();
        hoy.setHours(0,0,0,0);
        
        // Convertir la fecha del input correctamente (formato YYYY-MM-DD del input date)
        const sel = new Date(val + 'T00:00:00');
        if(isNaN(sel.getTime())) return true;

        // Debe ser estrictamente mayor o igual a hoy
        if(sel < hoy){
            alertify.error('La fecha compromiso debe ser igual o mayor a la fecha actual');
            $('#dtFechaCompromiso').val('');
            $('#divCambioCompromiso').hide();
            return false;
        }
        
        // Si es edición (hdnIdOrdenServicio > 0), verificar si cambió la fecha
        const idOrdenServicio = parseInt($('#hdnIdOrdenServicio').val()) || 0;
        if (idOrdenServicio > 0) {
            const fechaOriginal = $('#hdnFechaCompromisoOriginal').val();
            if (fechaOriginal && val !== fechaOriginal) {
                // La fecha cambió, mostrar div para seleccionar motivo
                $('#divCambioCompromiso').show();
            } else {
                // La fecha no cambió, ocultar div
                $('#divCambioCompromiso').hide();
                $('#ddlCambioCompromiso').val('-1');
            }
        }
        
        return true;
    }

    function cargarCatalogoEstatus() {
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/CatalogoEstatus?intRef1=1&intRef2=2&intRef3=3')
            .then(function(data){
                // Almacenar catálogo en variable global
                catalogoEstatus = data || [];
                
                const ddl = $('#ddlEstatus');
                ddl.empty().append('<option value="">Todos</option>');
                catalogoEstatus.forEach(function(x){ ddl.append('<option value="'+x.Id+'">'+x.Descripcion+'</option>'); });
            })
            .catch(function(err){ 
                console.error('Error catálogo estatus:', err);
            });
    }
    
    // Función helper para obtener descripción del estatus
    function obtenerDescripcionEstatus(idEstatus) {
        const estatus = catalogoEstatus.find(e => e.Id == idEstatus);
        return estatus ? estatus.Descripcion : `Estatus ${idEstatus}`;
    }

    function leerFiltros(){
        strNombreCliente = $('#txtNombreCliente').val();
        intIdCteInicial = $('#txtClienteInicial').val();
        intIdCteFinal = $('#txtClienteFinal').val();
        intIdEstatus  = $('#ddlEstatus').val();
        dateFechaInicial = $('#dtFechaInicial').val();
        dateFechaFinal = $('#dtFechaFinal').val();
        intIdOrdenServicioInicial = $('#txtOrdenServicioInicial').val();
        intIdOrdenServicioFinal = $('#txtOrdenServicioFinal').val();
    }

    function onBuscar(){
        if(!validarRangoFechas()) return false;
        if(!validarRangoClientes()) return false;
        if(!validarRangoOrdenServicio()) return false;
        
        leerFiltros();
        const qs = new URLSearchParams({
            strNombreCliente,
            intIdCteInicial:intIdCteInicial||0,
            intIdCteFinal:intIdCteFinal||0,
            intIdEstatus: intIdEstatus ||0,
            dateFechaInicial,
            dateFechaFinal,
            intIdOrdenServicioInicial:intIdOrdenServicioInicial||0,
            intIdOrdenServicioFinal:intIdOrdenServicioFinal||0
        });
        
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/BuscarOrdenesServicios?'+qs.toString())
            .then(function(data){ 
                llenarTabla(data);
            })
            .catch(function(err){ 
                console.error('Error búsqueda:', err);
            });
        return false;
    }

    function llenarTabla(lst) {
        $tbl.empty();
        
        // Validar si la lista está vacía
        if(!lst || lst.length === 0){
            alertify.success('No se encontraron órdenes de servicio con los criterios especificados');
            const tr = $('<tr/>');
            tr.append(`<td colspan="9" style="text-align:center; padding:20px; color:#999;">No hay datos para mostrar</td>`);
            $tbl.append(tr);
            return;
        }

        (lst||[]).forEach(o=>{
            const tr = $('<tr/>');
            tr.append(`<td>${html(o.strCveServicio)}</td>`);
            tr.append(`<td>${o.intIdOrdenServicio}</td>`);
            tr.append(`<td>${o.strEstatus}</td>`);
            tr.append(`<td>${o.dateFecha? formatDate(o.dateFecha):''}</td>`);
            tr.append(`<td>${o.intIdCte}</td>`);
            tr.append(`<td>${html(o.strNombreComercial)}</td>`);
            tr.append(`<td>${formatMoney(o.dcmSubtotal)}</td>`);
            
            // Condicionar botón de confirmación según Id_Estatus
            if(o.Id_Estatus === 2){
                // Si Id_Estatus = 2, mostrar botón activo con evento
                tr.append(`<td><button type='button' class='btn btn-success btn-sm btnConfirmar' data-id='${o.intIdOrdenServicio}' title='Confirmar'><i class='fa fa-check'></i></button></td>`);
            } else {
                // Si Id_Estatus != 2, mostrar botón deshabilitado en gris con evento informativo
                tr.append(`<td><button type='button' class='btn btn-default btn-sm btnConfirmarDisabled' data-estatus='${o.Id_Estatus}' title='No disponible' style='background-color:#e0e0e0; cursor:not-allowed;'><i class='fa fa-check'></i></button></td>`);
            }
            
            // Botón Editar
            if (o.Id_Estatus === 1 || o.Id_Estatus === 2){
                tr.append(`<td><button type='button' class='btn btn-primary btn-sm btnEditar' data-id='${o.intIdOrdenServicio}' title='Editar'><i class='fa fa-pencil'></i></button></td>`);
            } else {
                // Botón deshabilitado en gris con evento informativo
                tr.append(`<td><button type='button' class='btn btn-default btn-sm btnEditarDisabled' data-estatus='${o.Id_Estatus}' title='No disponible' style='background-color:#e0e0e0; cursor:not-allowed;'><i class='fa fa-pencil'></i></button></td>`);
            }
            
            // Botón Eliminar: Solo habilitado si Id_Estatus es 1 o 2 Y el usuario que insertó es el mismo que está en sesión
            const puedeEliminar = (o.Id_Estatus === 1 || o.Id_Estatus === 2) && (o.intIdUsuarioInserta === o.intUsuarioSesion);
            if(puedeEliminar){
                tr.append(`<td><button type='button' class='btn btn-danger btn-sm btnEliminar' data-id='${o.intIdOrdenServicio}' title='Eliminar'><i class='fa fa-trash'></i></button></td>`);
            } else {
                // Determinar razón de deshabilitación
                let razon = '';
                if(o.Id_Estatus !== 1 && o.Id_Estatus !== 2){
                    razon = `estatus${o.Id_Estatus}`;
                } else if(o.intIdUsuarioInserta !== o.intUsuarioSesion){
                    razon = 'usuario';
                }
                // Botón deshabilitado en gris con evento informativo
                tr.append(`<td><button type='button' class='btn btn-default btn-sm btnEliminarDisabled' data-razon='${razon}' data-estatus='${o.Id_Estatus}' title='No disponible' style='background-color:#e0e0e0; cursor:not-allowed;'><i class='fa fa-trash'></i></button></td>`);
            }
            
            $tbl.append(tr);
        });
        
        // Asignar eventos a botones habilitados
        $tbl.find('.btnEditar').on('click', function(e){ e.preventDefault(); onEditar.call(this,e); return false; });
        $tbl.find('.btnConfirmar').on('click', function(e){ e.preventDefault(); onConfirmar.call(this,e); return false; });
        $tbl.find('.btnEliminar').on('click', function(e){ e.preventDefault(); onEliminar.call(this,e); return false; });
        
        // Asignar eventos informativos a botones deshabilitados
        $tbl.find('.btnConfirmarDisabled').on('click', function(e){ 
            e.preventDefault(); 
            const estatus = $(this).data('estatus');
            const descripcionEstatus = obtenerDescripcionEstatus(estatus);
            alertify.success(`No se puede confirmar. La orden debe tener estatus "Capturada" o "Impreso" (Estatus actual: "${descripcionEstatus}")`);
            return false; 
        });
        
        $tbl.find('.btnEditarDisabled').on('click', function(e){ 
            e.preventDefault(); 
            const estatus = $(this).data('estatus');
            const descripcionEstatus = obtenerDescripcionEstatus(estatus);
            alertify.success(`No se puede editar. La orden debe tener estatus  "Asignada" o "Capturada" (Estatus actual: "${descripcionEstatus}")`);
            return false; 
        });
        
        $tbl.find('.btnEliminarDisabled').on('click', function(e){ 
            e.preventDefault(); 
            const razon = $(this).data('razon');
            const estatus = $(this).data('estatus');
            const descripcionEstatus = obtenerDescripcionEstatus(estatus);
            
            if(razon === 'usuario'){
                alertify.success('No se puede eliminar. Solo el usuario que creó la orden puede eliminarla');
            } else if(razon.startsWith('estatus')){
                alertify.success(`No se puede eliminar. La orden debe tener estatus "Asignada" o "Capturada" (Estatus actual: "${descripcionEstatus}")`);
            } else {
                alertify.success('No tiene permisos para eliminar esta orden de servicio');
            }
            return false; 
        });
    }

    function onNuevo(){
        limpiarModal();
        $('#lblTituloModal').text('Alta Orden de Servicio');
        // Ocultar estatus para alta
        $('#divEstatus').hide();  // ✅ ESTA LÍNEA OCULTA EL ESTATUS
        
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/CatalogosAltaOrdenServicio?intRef1=1&intRef2=2&intRef3=3&intRef4=4')
            .then(function(data){
                llenarCombo2($('#ddlRol'), data.lstRol, -1);
                lstRoles = data.lstRol || [];
                llenarComboServicio($('#ddlServicio'), data.lstServicio);
                lstUsuarios = data.lstUsuarios;
                // Asignar lista de servicios a variable global
                lstServicio = data.lstServicio || [];
                
                $('#modalOrdenServicio').appendTo('body').modal({
                    backdrop: 'static',
                    keyboard: false,
                    show: true
                });

                agregarFilaDetalle();
                // Inicializar autocomplete después de cargar los datos
                inicializarAutocomplete();

                // Activar el primer tab (Formulario)
                $('#tabFormulario').tab('show');
            })
            .catch(function(err){ 
                console.error('Error catálogos:', err);
            });
        return false;
    }

    function onEditar(){
        const id = $(this).data('id');
        limpiarModal();
        $('#lblTituloModal').text('Editar Orden de Servicio');
        // Mostrar estatus para edición
        $('#divEstatus').show();
        
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarOrdenServicio?intIdOrdenServicio='+id)
            .then(function(data){
                const det = data.entOrdenServicioDetalle || {};
                const dir = data.entOrdenServicioClienteDireccion || {};
                $('#txtNumeroFolio').val(det.strSerie + "-" + det.intIdOrdenServicio);
                $('#hdnIdOrdenServicio').val(det.intIdOrdenServicio); 
                $('#chkExtemporaneo').prop('checked', det.isExtemporaneo||false);
                let mostrarInfoAdicional= false;
                if (det.dateEstimada) {
                    $('#lblFechaEstimada').html("Fecha estimada: " + formatDate(det.dateEstimada));
                    $('#dvFechaEstimada').show();
                    mostrarInfoAdicional = true;
                }

                if (det.strMatriz) {
                    $('#lblMatriz').html("Matriz Cta. Nacional: " + det.strMatriz);
                    $('#dvMatriz').show();
                    mostrarInfoAdicional = true;
                }

                if (det.strIndicaciones) {
                    $('#lblIndicaciones').text("Indicaciones del creador: " + det.strIndicaciones);
                    $('#dvIndicaciones').show();
                    mostrarInfoAdicional = true;
                }           

                if (mostrarInfoAdicional) {
                    $('#dvInformativo').show();
                }                

                if(det.dateCompromiso) {
                    const fechaCompromiso = formatDateInput(det.dateCompromiso);
                    $('#dtFechaCompromiso').val(fechaCompromiso);
                    // Guardar fecha original para comparación
                    $('#hdnFechaCompromisoOriginal').val(fechaCompromiso);
                }
                
                // Ocultar inicialmente el div de cambio de compromiso
                $('#divCambioCompromiso').hide();
                
                // Llenar combo de motivo cambio compromiso
                if (data.lstCambioCompromiso && data.lstCambioCompromiso.length > 0) {
                    llenarCombo2($('#ddlCambioCompromiso'), data.lstCambioCompromiso, -1);
                }
                
                // Mostrar estatus
                if(det.Id_Estatus) {
                    const descripcionEstatus = obtenerDescripcionEstatus(det.Id_Estatus);
                    $('#lblEstatus').text(descripcionEstatus);
                    // Agregar clase CSS según el estatus
                    $('#lblEstatus').removeClass('estatus-asignada estatus-capturada estatus-impreso estatus-confirmada estatus-incompleta estatus-eliminada');
                    const claseEstatus = 'estatus-' + descripcionEstatus.toLowerCase().replace(/\s+/g, '');
                    $('#lblEstatus').addClass(claseEstatus);
                } else {
                    $('#lblEstatus').text('');
                }
                
                llenarCombo($('#ddlRol'), data.lstRol, det.intIdRol);
                lstRoles = data.lstRol || [];
                
                // Asignar lista de servicios y usuarios a variables globales
                lstServicio = data.lstServicio || [];
                lstUsuarios = data.lstUsuario || [];
                
                $('#txtIdCliente').val(det.intIdCliente);
                $('#txtNombreClienteModal').val(dir.strNombreComercial || '');
                $('#txtTerritorio').val(det.intIdTer||'');
                llenarCombo2($('#ddlTerritorio'), data.lstTerritorio, det.intIdTer);
                if (det.intIdTer) {
                    $('#ddlTerritorio').trigger("change");
                }                
                // llenarCombo($('#ddlUsuario'), data.lstUsuario, det.intIdUsuario); // se llenas con el evento al selecciona el rol               
                $('#lblSegmento').text(dir.strSegmento ||'');
                $('#lblCalle').text(dir.strCalle || '');
                $('#lblNumero').text(dir.strNumCalle||'');
                $('#lblColonia').text(dir.strColonia||'');
                $('#lblMunicipio').text(dir.strMunicipio||'');
                $('#lblEstado').text(dir.strEstado||'');
                $('#lblRfc').text(dir.strRFC||'');
                $('#lblTelefono').text(dir.strTelefono || '');
                
                // Llenar tabla de roles y usuarios si existen en data.lstRolUsuario
                const $tblUsuario = $('#tblUsuario tbody');
                $tblUsuario.empty();
                window.arrRolUsuario = [];
                if (data.lstRolUsuario && data.lstRolUsuario.length > 0) {
                    (data.lstRolUsuario || []).forEach(function (ru) {
                        const tr = $('<tr/>');
                        tr.append('<td>' + ru.intIdRol + '</td>');
                        tr.append('<td>' + html(ru.strRol) + '</td>');
                        tr.append('<td>' + ru.intIdRep + '</td>');
                        tr.append('<td>' + html(ru.strRepresentante) + '</td>');
                        tr.append('<td style="text-align:center;"><button type="button" class="btn btn-danger btn-sm btnQuitarUsuario"><i class="fa fa-trash"></i></button></td>');

                        // Guardar datos como data attribute
                        tr.data('idRolUsuario', ru.intIdRolUsuario || 0);
                        tr.data('idRol', ru.intIdRol);
                        tr.data('rol', ru.strRol);
                        tr.data('idRep', ru.intIdRep);
                        tr.data('representante', ru.strRepresentante);

                        $tblUsuario.append(tr);

                        // Agregar al array global
                        window.arrRolUsuario.push({
                            intIdRolUsuario: ru.intIdRolUsuario || 0,
                            intIdRol: ru.intIdRol,
                            strRol: ru.strRol,
                            intIdRep: ru.intIdRep,
                            strRepresentante: ru.strRepresentante
                        });

                        // Evento para quitar fila
                        tr.find('.btnQuitarUsuario').on('click', function () {
                            const $fila = $(this).closest('tr');
                            const idRolUsuario = $fila.data('idRolUsuario') || 0;
                            const idRolFila = $fila.data('idRol');
                            const idRepFila = $fila.data('idRep');

                            // Si es un registro existente (idRolUsuario > 0), agregar al array de eliminación
                            if (idRolUsuario > 0) {
                                window.arrIdRolUsuarioEliminar.push(idRolUsuario);
                            }

                            // Remover del array global
                            window.arrRolUsuario = window.arrRolUsuario.filter(function (ru) {
                                return !(ru.intIdRol == idRolFila && ru.intIdRep == idRepFila);
                            });

                            // Remover visualmente la fila
                            $fila.remove();
                        });
                    });
                }
                
                // Inicializar autocomplete después de cargar los datos
                inicializarAutocomplete();
                
                // Activar el primer tab (Formulario)
                $('#tabFormulario').tab('show');
                
                $('#modalOrdenServicio').appendTo('body').modal({
                    backdrop: 'static',
                    keyboard: false,
                    show: true
                });

                llenarComboServicio($('#ddlServicio'), data.lstServicio, det.intIdServicio);
                // Llenar tabla de detalle de servicios con los datos existentes en data.lstProductos...
                const $tblDet = $('#tblDetalleServicios tbody');
                $tblDet.empty();
                (data.lstProductos || []).forEach(function(p){
                    const tr = $('<tr/>');
                    tr.append('<td><input type="text" class="form-control txtIdPrd" placeholder="#" value="'+(p.Id_Prd || '')+'"/></td>');
                    tr.append('<td><input type="text" class="form-control txtServicioDet" placeholder="Descripcion" value="'+html(p.Descripcion || '')+'"/></td>');
                    tr.append('<td><input type="number" class="form-control txtCantidad" placeholder="Cantidad" value="'+(p.Cantidad || 1)+'" min="1"/></td>');
                    tr.append('<td><input type="text" class="form-control txtCosto" placeholder="Costo" value="'+(p.Costo || 0)+'" readonly/></td>');
                    tr.append('<td><input type="text" class="form-control txtTotal" placeholder="Total" value="'+(p.Total || 0)+'" readonly/></td>');
                    tr.append('<td style="text-align:center;"><button type="button" class="btn btn-danger btn-sm btnEliminarFila"><i class="fa fa-trash"></i></button></td>');
                    tr.append('<td style="display:none;"><input type="hidden" class="txtTipoProducto" value="' + (p.TipoProducto || 1) + '"/></td>');
                    // Almacenar Id_SrvDet como data attribute para productos existentes
                    tr.data('idSrvDet', p.Id_SrvDet || 0);
                    
                    $tblDet.append(tr);
                    
                    // Agregar evento para recalcular total cuando cambie cantidad
                    tr.find('.txtCantidad').on('change', function(){
                        recalcularTotal($(this).closest('tr'));
                    });
                    
                    // Agregar evento para eliminar fila
                    tr.find('.btnEliminarFila').on('click', function(){
                        const $fila = $(this).closest('tr');
                        const idSrvDet = $fila.data('idSrvDet') || 0;
                        
                        if(idSrvDet > 0) {
                            // Es un producto existente, marcar para eliminación lógica
                            if(!window.productosAEliminar) window.productosAEliminar = [];
                            window.productosAEliminar.push(idSrvDet);
                        }
                        
                        // Remover visualmente la fila
                        $fila.remove();
                    });
                });

                if (($tblDet.find('tr').length || 0) === 0) {
                    agregarFilaDetalle();
                }
            })
            .catch(function(err){ 
                console.error('Error consultar:', err);
            });
        return false;
    }

    function AbreModalClientes(lstDireccion) {
        
        window.DataLocalClientes = lstDireccion;
        // llena la tabla de clientes tblClientes y abre el modal modalClientes
        const $tblClientes = $('#tblClientes tbody');
        $tblClientes.empty();
        (lstDireccion || []).forEach((c, index) => {
            const tr = $('<tr/>');
            tr.append(`<td>${c.intIdCliente || ''}</td>`);
            tr.append(`<td>${html(c.strNombreComercial) || ''}</td>`);
            // CAMBIO AQUÍ: usar event handler en lugar de onClick inline
            tr.append(`<td><button type='button' class='btn btn-primary btn-sm btnSeleccionar' data-cliente-id='${c.intIdCliente}'><i class='fa fa-check'></i></button></td>`);
            $tblClientes.append(tr);
        });

        // Agregar event handler para los botones de selección
        $tblClientes.find('.btnSeleccionar').on('click', function (e) {
            e.preventDefault();
            const clienteId = parseInt($(this).data('cliente-id'));
            window.SeleccionClienteLocal(clienteId);
            return false;
        });
        $('#modalClientes').appendTo('body').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }

    //function SeleccionClienteLocal(idCliente) {
    //    const cliente = DataLocalClientes.find(c => c.intIdCliente === idCliente);
    //    if (cliente) {
    //        $('#txtIdCliente').val(cliente.intIdCliente || '');
    //        $('#txtNombreClienteModal').val(cliente.strNombreComercial || '');
    //        $('#lblSegmento').text(cliente.strSegmento || '');
    //        $('#lblCalle').text(cliente.strCalle || '');
    //        $('#lblNumero').text(cliente.strNumCalle || '');
    //        $('#lblColonia').text(cliente.strColonia || '');
    //        $('#lblMunicipio').text(cliente.strMunicipio || '');
    //        $('#lblEstado').text(cliente.strEstado || '');
    //        $('#lblRfc').text(cliente.strRFC || '');
    //        $('#lblTelefono').text(cliente.strTelefono || '');
    //    }
    //    $('#modalClientes').modal('hide');
    //}


    function onBuscarCliente(){      
        const nombre = $('#txtNombreClienteModal').val();
        if(!nombre){ alertify.success('Capture el nombre del cliente'); return false; }
        const qs = new URLSearchParams({ strNombreCliente: nombre || '', intIdCliente:0 });
        LlenarCliente(qs)
        return false;
    }

    // Nueva función SeleccionarCliente (onchange txtIdCliente)
    function SeleccionarCliente() {
        const nombre = '';
        const id = $('#txtIdCliente').val();
        if (!id) { return false; }
        const qs = new URLSearchParams({ strNombreCliente: nombre, intIdCliente: id });
        LlenarCliente(qs);
        return false;
    }

    function LlenarCliente(qs) {
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarCliente?' + qs.toString())
            .then(function(data){
                if (data.strMensaje) {
                    alertify.error(data.strMensaje);
                    return;
                }
                // Si solo hay un cliente, llenar los datos directamente, si hay varios llmar a la funcion 
                if (data.lstDireccion.length === 1) {
                    const cliente = data.lstDireccion[0];
                    $('#txtIdCliente').val(cliente.intIdCliente || '');
                    $('#txtNombreClienteModal').val(cliente.strNombreComercial || '');
                    $('#lblSegmento').text(cliente.strSegmento || '');
                    $('#lblCalle').text(cliente.strCalle || '');
                    $('#lblNumero').text(cliente.strNumCalle||'');
                    $('#lblColonia').text(cliente.strColonia||'');
                    $('#lblMunicipio').text(cliente.strMunicipio||'');
                    $('#lblEstado').text(cliente.strEstado||'');
                    $('#lblRfc').text(cliente.strRFC||'');
                    $('#lblTelefono').text(cliente.strTelefono || '');
                    llenarCombo2($('#ddlTerritorio'), data.lstTerritorio, data.intIdTerSel);
                    //llenarCombo($('#ddlUsuario'), data.lstUsuario, data.intIdUsuarioSel);
                    const currentTer = $('#ddlTerritorio').val();
                    if (currentTer != data.intIdTerSel) {
                        $('#txtTerritorio').val(currentTer);
                    }
                    if (data.intIdTerSel) {
                        $('#ddlTerritorio').trigger("change");
                    }
                    
                } else {
                    // Si hay varios clientes,
                    window.DataLocalTerritorio = data.lstTerritorio;
                    window.DataLocalUsuario = data.lstUsuario;
                    AbreModalClientes(data.lstDireccion);
                }
            })
            .catch(function(err){ 
                console.error('Error cliente:', err);
            });
    }

    function onGuardar(){
        // Validaciones de datos antes de enviar
        
        // 1. Validar fecha compromiso es requerida
        const fechaCompromiso = $('#dtFechaCompromiso').val();
        if (!fechaCompromiso) {
            alertify.error('La fecha compromiso es requerida');
            $('#dtFechaCompromiso').focus();
            return false;
        }
        
        // 2. Validar que la fecha compromiso sea mayor a la fecha actual
        if(!validarFechaCompromiso()) return false;
        
        // NUEVA VALIDACIÓN: Si es edición y la fecha cambió, validar que se haya seleccionado un motivo
        const idOrdenServicio = parseInt($('#hdnIdOrdenServicio').val()) || 0;
        const fechaOriginal = $('#hdnFechaCompromisoOriginal').val();
        let intMotivoCambioFecha = 0;
        
        if (idOrdenServicio > 0 && fechaOriginal && fechaCompromiso !== fechaOriginal) {
            // La fecha cambió, validar que se haya seleccionado un motivo
            intMotivoCambioFecha = parseInt($('#ddlCambioCompromiso').val()) || -1;
            if (intMotivoCambioFecha === -1) {
                alertify.error('Debe seleccionar un motivo de cambio de fecha compromiso');
                $('#ddlCambioCompromiso').focus();
                return false;
            }
        }
        
        // 3. Validar selección de servicio
        const servicio = $('#ddlServicio').val();
        if (!servicio || servicio === '' || servicio === '-1') {
            alertify.error('Debe seleccionar un servicio');
            $('#ddlServicio').focus();
            return false;
        }
        
        // 4. NUEVA VALIDACIÓN: Validar que exista al menos un usuario en tblUsuario
        const filasUsuario = $('#tblUsuario tbody tr').length;
        if (filasUsuario === 0) {
            alertify.error('Debe agregar al menos un rol y usuario');
            $('#ddlRol').focus();
            return false;
        }
        
        // 5. Validar ID cliente
        const idCliente = $('#txtIdCliente').val().trim();
        if (!idCliente) {
            alertify.error('El ID del cliente es requerido');
            $('#txtIdCliente').focus();
            return false;
        }
        
        // 6. Validar territorio
        const territory = $('#txtTerritorio').val().trim();
        if (!territory || territory === '-1') {
            alertify.error('El territorio es requerido');
            $('#ddlTerritorio').focus();
            return false;
        }
        
        // 7. Validar que haya al menos una fila de servicio en la tabla
        const filasServicio = $('#tblDetalleServicios tbody tr').length;
        if (filasServicio === 0) {
            alertify.error('Debe agregar al menos un servicio al detalle');
            $('#btnAgregarFilaDet').focus();
            return false;
        }
        
        // 8. Validar que todas las filas de servicio tengan datos y construir array de productos
        let filasSinDatos = false;
        const productos = [];
        let orden = 1;
        
        $('#tblDetalleServicios tbody tr').each(function() {
            const idPrd = $(this).find('.txtIdPrd').val().trim();
            const descripcion = $(this).find('.txtServicioDet').val().trim();
            const cantidad = parseInt($(this).find('.txtCantidad').val()) || 0;
            const costo = parseFloat($(this).find('.txtCosto').val()) || 0;
            const total = parseFloat($(this).find('.txtTotal').val()) || 0;
            const tipoProducto = parseInt($(this).find('.txtTipoProducto').val()) || 1;
            const idSrvDet = $(this).data('idSrvDet') || 0;
            
            if (!idPrd || !descripcion) {
                filasSinDatos = true;
                return false; // break del each
            }
            
            // Validar que la cantidad sea mayor a 0
            if (cantidad <= 0) {
                alertify.error('La cantidad debe ser mayor a 0 en todas las filas');
                filasSinDatos = true;
                return false; // break del each
            }
            
            productos.push({
                Id_SrvDet: idSrvDet,
                Orden: orden++,
                Id_Prd: parseInt(idPrd) || 0,
                Descripcion: descripcion,
                Cantidad: cantidad,
                Costo: costo,
                Total: total,
                TipoProducto: tipoProducto
            });
        });
        
        if (filasSinDatos) {
            alertify.error('Todas las filas de servicio deben tener ID, descripción y cantidad mayor a 0');
            return false;
        }
        
        // 9. Verificar que el array de productos tenga al menos un elemento
        if (productos.length === 0) {
            alertify.error('Debe agregar al menos un producto con datos completos al detalle de la orden');
            $('#tblDetalleServicios tbody tr:first .txtIdPrd').focus();
            return false;
        }
        
        // Serializar el array de productos a JSON string
        const strProductos = JSON.stringify(productos);
        
        // Serializar array de productos a eliminar
        const productosAEliminar = window.productosAEliminar || [];
        const strProductosAEliminar = JSON.stringify(productosAEliminar);
        
        // Serializar array de roles y usuarios
        const strRolUsuario = JSON.stringify(window.arrRolUsuario || []);
        
        // Serializar array de roles y usuarios a eliminar
        const strIdRolUsuarioEliminar = JSON.stringify(window.arrIdRolUsuarioEliminar || []);
        
        // Si todas las validaciones pasan, proceder a guardar
        const payload = {
            intIdOrdenServicio: parseInt($('#hdnIdOrdenServicio').val())||0,
            isExtemporaneo: $('#chkExtemporaneo').is(':checked'),
            dateCompromiso: $('#dtFechaCompromiso').val(),
            intMotivoCambioFecha: intMotivoCambioFecha,  // NUEVO: Agregar motivo de cambio de fecha
            intIdServicio: parseInt($('#ddlServicio').val()) || 0,
            strDescripcionServicio: $('#ddlServicio option:selected').text(),
            intIdCliente: parseInt($('#txtIdCliente').val())||0,
            intIdTer: parseInt($('#txtTerritorio').val())||0,
            strProductos: strProductos,
            strProductosAEliminar: strProductosAEliminar,
            strRolUsuario: strRolUsuario,
            strIdRolUsuarioEliminar: strIdRolUsuarioEliminar
        };
        
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/GuardarOrdenServicio?'+toQuery(payload))
            .then(function(data){
                alertify.success(data.mensaje || 'Guardado correctamente');
                $('#modalOrdenServicio').modal('hide');
                // Limpiar arrays después de guardar exitosamente
                window.productosAEliminar = [];
                window.arrRolUsuario = [];
                window.arrIdRolUsuarioEliminar = [];
                onBuscar();
            })
            .catch(function(err){ 
                console.error('Error al guardar:', err);
            });
        return false;
    }

    function onExportar(){
        leerFiltros();
        const qs = new URLSearchParams({
            strNombreCliente,
            intIdCteInicial:intIdCteInicial||0,
            intIdCteFinal: intIdCteFinal || 0,
            intIdEstatus: intIdEstatus ||0,
            strEstatus: $('#ddlEstatus option:selected').text(),
            dateFechaInicial,
            dateFechaFinal,
            intIdOrdenServicioInicial:intIdOrdenServicioInicial||0,
            intIdOrdenServicioFinal:intIdOrdenServicioFinal||0
        });
        
        // Usar ApiHelper para descargar el archivo Excel
        ApiHelper.fetchBlob(
            ApplicationUrl + '/api/CapOrdenServicio_Admin/DescargarExcelOrdenesServicio?'+qs.toString(),
            'reporte_ordenes_servicio.xlsx'
        )
        .then(function() {
            // El mensaje de éxito ya lo muestra ApiHelper
            console.log('Archivo descargado correctamente');
        })
        .catch(function(error) {
            console.error('Error al descargar:', error);
        });
        
        return false;
    }

    function onConfirmar(){
        const id = $(this).data('id');
        limpiarModalConfirmar();
        $('#hdnIdOrdenServicioConfirmar').val(id);
        
        // Consultar catálogo de motivos de incompleto
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarCatMotivoIncompleto?intIdOrdenServicio='+id)
            .then(function(data){
                const ddl = $('#ddlMotivoIncompleto');
                ddl.empty().append('<option value="-1">-- Seleccione un motivo --</option>');
                (data || []).forEach(function(x){ ddl.append('<option value="'+x.Id+'">'+x.Descripcion+'</option>'); });
                
                // Abrir modal
                $('#mdConfirmar').appendTo('body').modal({
                    backdrop: 'static',
                    keyboard: false,
                    show: true
                });
            })
            .catch(function(err){ 
                console.error('Error al cargar motivos:', err);
            });
        return false;
    }

    function limpiarModalConfirmar(){
        $('#hdnIdOrdenServicioConfirmar').val('0');
        $('#rdCompleto').prop('checked', true);
        $('#rdIncompleto').prop('checked', false);
        $('#ddlMotivoIncompleto').val('-1');
        $('#divMotivoIncompleto').hide();
    }

    function onConfirmacionChanged(){
        const isIncompleto = $('#rdIncompleto').is(':checked');
        if(isIncompleto){
            $('#divMotivoIncompleto').show();
        } else {
            $('#divMotivoIncompleto').hide();
            $('#ddlMotivoIncompleto').val('-1');
        }
        return false;
    }

    function onGuardarConfirmacion(){
        const intIdOrdenServicio = parseInt($('#hdnIdOrdenServicioConfirmar').val()) || 0;
        const isCompleto = $('#rdCompleto').is(':checked');
        const isIncompleto = $('#rdIncompleto').is(':checked');
        const intMotivoIncompleto = parseInt($('#ddlMotivoIncompleto').val()) || -1;

        // Validaciones
        if(intIdOrdenServicio === 0){
            alertify.error('No se ha seleccionado una orden de servicio');
            return false;
        }

        if(isIncompleto && intMotivoIncompleto === -1){
            alertify.error('Debe seleccionar un motivo de incompleto');
            $('#ddlMotivoIncompleto').focus();
            return false;
        }

        // Preparar datos para enviar
        const payload = {
            intIdOrdenServicio: intIdOrdenServicio,
            isCompleto: isCompleto,
            intMotivoIncompleto: isCompleto ? 0 : intMotivoIncompleto
        };

        // Enviar al API
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/GuardarConfirmacion?'+toQuery(payload))
            .then(function(data){
                // validar si data tiene una propiedad 'mensaje'
                if (data.mensaje) {
                    alertify.success(data.mensaje);
                }
                
                $('#mdConfirmar').modal('hide');
                onBuscar(); // Recargar tabla
            })
            .catch(function(err){ 
                console.error('Error al guardar confirmación:', err);
            });
        return false;
    }

    // Evento para confirmar eliminación
    function onConfirmarEliminacion(){
        const intIdOrdenServicio = parseInt($('#hdnIdOrdenServicioEliminar').val()) || 0;
        const intMotivoEliminacion = parseInt($('#ddlMotivoEliminacion').val()) || -1;

        // Validaciones
        if(intIdOrdenServicio === 0){
            alertify.error('No se ha seleccionado una orden de servicio');
            return false;
        }

        if(intMotivoEliminacion === -1){
            alertify.error('Debe seleccionar un motivo de eliminación');
            $('#ddlMotivoEliminacion').focus();
            return false;
        }

        // Preparar datos para enviar
        const payload = {
            intIdOrdenServicio: intIdOrdenServicio,
            intMotivoEliminacion: intMotivoEliminacion
        };

        // Enviar al API
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/EliminarOrdenServicio?'+toQuery(payload))
            .then(function(data){
                alertify.success(data.mensaje || 'Orden de servicio eliminada correctamente');
                $('#mdConfirmarEliminacion').modal('hide');
                onBuscar(); // Recargar tabla
            })
            .catch(function(err){ 
                console.error('Error al eliminar orden de servicio:', err);
            });
        return false;
    }
    
    function onEliminar(){
        const id = $(this).data('id');
        limpiarModalEliminar();
        $('#hdnIdOrdenServicioEliminar').val(id);
        
        // Consultar catálogo de motivos de eliminación
        ApiHelper.fetchJson(ApplicationUrl + '/api/CapOrdenServicio_Admin/ConsultarCatMotivoEliminacion?intIdOrdenServicio='+id)
            .then(function(data){
                const ddl = $('#ddlMotivoEliminacion');
                ddl.empty().append('<option value="-1">-- Seleccione un motivo --</option>');
                (data || []).forEach(function(x){ ddl.append('<option value="'+x.Id+'">'+x.Descripcion+'</option>'); });
                
                // Abrir modal
                $('#mdConfirmarEliminacion').appendTo('body').modal({
                    backdrop: 'static',
                    keyboard: false,
                    show: true
                });
            })
            .catch(function(err){ 
                console.error('Error al cargar motivos de eliminación:', err);
            });
        return false;
    }

    function limpiarModalEliminar(){
        $('#hdnIdOrdenServicioEliminar').val('0');
        $('#ddlMotivoEliminacion').val('-1');
    }
    
    function agregarFilaDetalle(){
        const idx = $('#tblDetalleServicios tbody tr').length + 1;
        const tr = $('<tr/>');
        tr.append(`<td><input type='text' class='form-control txtIdPrd' placeholder='#'/></td>`);
        tr.append(`<td><input type='text' class='form-control txtServicioDet' placeholder='Descripcion'/></td>`);
        tr.append(`<td><input type='number' class='form-control txtCantidad' placeholder='Cantidad' value='1' min='1'/></td>`);
        tr.append(`<td><input type='text' class='form-control txtCosto' placeholder='Costo' readonly/></td>`);
        tr.append(`<td><input type='text' class='form-control txtTotal' placeholder='Total' readonly/></td>`);
        tr.append(`<td><button type='button' class='btn btn-danger btn-sm btnEliminarFila'><i class='fa fa-trash'></i></button></td>`);
        tr.append(`<td style="display:none;"><input type="hidden" class='txtTipoProducto' /></td>`);
        
        // Marcar como producto nuevo (Id_SrvDet = 0)
        tr.data('idSrvDet', 0);
        
        $('#tblDetalleServicios tbody').append(tr);
        
        // Cambiar automáticamente al tab de servicios
        $('#tabServicios').tab('show');
        
        // Agregar evento para recalcular total cuando cambie cantidad
        tr.find('.txtCantidad').on('change', function(){
            recalcularTotal($(this).closest('tr'));
        });
        
        // Agregar evento para eliminar fila
        tr.find('.btnEliminarFila').on('click', function(){
            const $fila = $(this).closest('tr');
            const idSrvDet = $fila.data('idSrvDet') || 0;
            
            if(idSrvDet > 0) {
                // Es un producto existente, marcar para eliminación lógica
                if(!window.productosAEliminar) window.productosAEliminar = [];
                window.productosAEliminar.push(idSrvDet);
            }
            
            // Remover visualmente la fila
            $fila.remove();
        });
        
        return false;
    }

    function limpiarModal(){
        $('#modalOrdenServicio input[type=text], #modalOrdenServicio input[type=number], #modalOrdenServicio input[type=date]').val('');

        $('#lblFechaEstimada').html("");
        $('#dvFechaEstimada').hide();
        $('#lblIndicaciones').html("");
        $('#dvIndicaciones').hide();
        $('#dvMatriz').hide();
        $('#lblMatriz').html("");
        $('#hdnIdRepServicio').val("-1");
        $('#hdnIdRepServicioTecnico').val("-1");
        $('#lblRepresentantes').text("");

        $('#chkExtemporaneo').prop('checked', false);
        $('#tblDetalleServicios tbody').empty();
        $('#tblUsuario tbody').empty();
        $('#lblSegmento,#lblCalle,#lblNumero,#lblColonia,#lblMunicipio,#lblEstado,#lblRfc,#lblTelefono').text('');
        $('#lblEstatus').text('').removeClass('estatus-asignada estatus-capturada estatus-impreso estatus-confirmada estatus-incompleta estatus-eliminada');
        $('#hdnIdOrdenServicio').val('0');
        llenarCombo2($('#ddlUsuario'), [], -1);     
        llenarCombo2($('#ddlTerritorio'), [], -1);

        // Limpiar arrays globales
        window.productosAEliminar = [];
        window.arrRolUsuario = [];
        window.arrIdRolUsuarioEliminar = [];

        // Limpiar cache de productos encontrados
        cacheProductosEncontrados = [];
    }

    // Utilidades
    function llenarCombo2($ddl, lst, sel) {
        $ddl.empty();
        // si la lista tine mas de un item agregar el option value="-1" texto= "Seleccione"...
        if ((lst || []).length > 1) $ddl.append("<option value='-1'>Seleccione</option>");
        else if ((lst || []).length === 1) sel = lst[0].Id; // si solo hay un item, selecionarlo        

        (lst || []).forEach(x => $ddl.append(`<option value='${x.Id}'>${x.Descripcion}</option>`));
        if (sel !== undefined) $ddl.val(sel);
    }
    function llenarCombo($ddl, lst, sel){
        $ddl.empty();
        (lst||[]).forEach(x=>$ddl.append(`<option value='${x.Id}'>${x.Descripcion}</option>`));
        if(sel!==undefined) $ddl.val(sel);
        
        // Refrescar selectpicker si es ddlServicio
        if ($ddl.attr('id') === 'ddlServicio') {
            
            $ddl.selectpicker('refresh');
            if (sel !== undefined) {
                setTimeout(function () {
                    $ddl.selectpicker('val', sel.toString());
                }, 10);
            }
            
        }
    }
    function llenarComboServicio($ddl, lst, sel) {
        $ddl.empty();
        (lst || []).forEach(x => $ddl.append(`<option value='${x.Id}' data-costo='${x.Costo}'>${x.Descripcion}</option>`));
        if (sel !== undefined) $ddl.val(sel);

        // Refrescar selectpicker si es ddlServicio
        if ($ddl.attr('id') === 'ddlServicio') {
            
            $ddl.selectpicker('refresh');
            $ddl.selectpicker('setStyle', 'btn-default', 'add');
            
            // Forzar ancho después de refresh
            setTimeout(function() {
                $ddl.selectpicker('setStyle', '', 'remove');
                $('.bootstrap-select button[data-id="ddlServicio"]').css('width', '400px');
                $('.bootstrap-select[class*="ddlServicio"]').css('width', '400px');
            }, 50);
            
            if (sel !== undefined) {
                setTimeout(function () {
                    $ddl.selectpicker('val', sel.toString());
                }, 10);
            }
            
        }
    }
    
    function html(s){ return (s||'').toString().replace(/[&<>]/g,c=>({'&':'&amp;','<':'&lt;','>':'&gt;'}[c])); }
    function formatMoney(v){ return (v==null?0:v).toLocaleString('es-MX',{minimumFractionDigits:2,maximumFractionDigits:2}); }
    function formatDate(d){ if(!d) return ''; const dt = new Date(d); if(isNaN(dt)) return d; return dt.toISOString().substring(0,10); }
    function formatDateInput(d){ return formatDate(d); }
    function toQuery(o){ return Object.keys(o).map(k=>encodeURIComponent(k)+'='+encodeURIComponent(o[k]==null?'':o[k])).join('&'); }

    $(init);
    return {
        llenarCombo2: function ($ddl, lst, sel) {
            llenarCombo2($ddl, lst, sel);
        }
    }
})();
