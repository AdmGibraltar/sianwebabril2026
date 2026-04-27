jQuery(document).ready(function () {
    init()

    window.addEventListener('storage', (e) => {
        if (e.key === 'sentinelPicking') {
            localStorage.setItem('sentinelPicking_newPage', Math.random())
        }

        if (e.key === 'sentinelPicking_newPage') {
            window.alert('No se pueden tener dos ventanas del picking')
            if (document.referrer) {
                const lastIndex = document.referrer.lastIndexOf('/')
                const path = document.referrer.slice(0, lastIndex + 1) + 'inicio.aspx'
                window.location.href = path
            } else window.history.back()
        }

    }, false)

    localStorage.setItem('sentinelPicking', Math.random())
})

function init() {
    jQuery('* .form-control').removeClass('rfdDecorated')

    jQuery('#myTab a').click(function (e) {
        e.preventDefault();
        jQuery(this).tab('show');

        jQuery('#por-producto-tab').show();
        jQuery('#por-pedido-tab').show();
        jQuery('#por-sin-identificar').show();
    })
}

/*
 * muestra una ventana con las cantidades
 * restantes en cantidades por producto
 */
function VerMasCantidadesPorProducto(Id_Prd, Prd_Descripcion, Ped_Cantidad, Ped_Facturado, Ped_Remisionado, Ped_Pendiente, Ped_Asignado, Prd_Disponible, Ped_Picking, Ped_CantidadDisponible) {
    jQuery('#tcantidadesPorProducto').empty()
    jQuery('#tcantidadesPorProducto').append(
        '<tr>\
            <td>' + Ped_Cantidad + '</td> \
            <td>' + Ped_Facturado + '</td> \
            <td>' + Ped_Remisionado + '</td> \
            <td>' + Ped_Pendiente + '</td> \
            <td>' + Ped_Asignado + '</td> \
            <td>' + Prd_Disponible + '</td> \
            <td style="color:red">' + Ped_Picking + '</td> \
            <td>' + Ped_CantidadDisponible + '</td> \
        </tr>'
    );
    jQuery('#cantidadesPorProductoTitle').text(Id_Prd + ' - ' + Prd_Descripcion)

    jQuery('#cantidadesPorProducto').modal('show')
}

/*
 * muestra una ventana con las cantidades
 * restantes en cantidades por pedido
 */
function VerMasCantidadesPorPedido(Id_Cte, Cte_NomComercial, Ped_Cantidad, Ped_Facturado, Ped_Remisionado, Ped_Pendiente, Ped_Asignado, Ped_Picking, Ped_CantidadDisponible) {
    jQuery('#tcantidadesPorPedido').empty()
    jQuery('#tcantidadesPorPedido').append(
        '<tr>\
            <td>' + Ped_Cantidad + '</td> \
            <td>' + Ped_Facturado + '</td> \
            <td>' + Ped_Remisionado + '</td> \
            <td>' + Ped_Pendiente + '</td> \
            <td>' + Ped_Asignado + '</td> \
            <td>' + Ped_Picking + '</td> \
            <td>' + Ped_CantidadDisponible + '</td> \
        </tr>'
    );
    jQuery('#cantidadesPorPedidoTitle').text(Id_Cte + ' - ' + Cte_NomComercial)

    jQuery('#cantidadesPorPedido').modal('show')
}

function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
}

var confirmModal = false
function endRequestHandler() {
    init()
    hideLoader()
    if (confirmModal) sweetAlertSuccess('Las cantidades fueron actualizadas correctamente')
}

/*
 * (+)Pick - por producto
 */
function guardar(id_Prd, ruta, credito, parcialidades, tipoPedido, agrupado) {
    var url = 'ProAuxiliarV2.aspx/ConfirmarPedido'
    pick_guardar_cancelar(url, id_Prd, ruta, credito, parcialidades, tipoPedido, agrupado)
}

function cancelar(id_Prd, ruta, credito, parcialidades, tipoPedido, agrupado) {
    var url = 'ProAuxiliarV2.aspx/CancelarPedido'
    pick_guardar_cancelar(url, id_Prd, ruta, credito, parcialidades, tipoPedido, agrupado)
}

function pick_guardar_cancelar(url, id_Prd, ruta, credito, parcialidades, tipoPedido, agrupado) {
    showLoader()

    jQuery.ajax({
        type: 'POST',
        data: JSON.stringify({
            id_Prd: id_Prd,
            ruta: ruta,
            credito: credito,
            parcialidades: parcialidades,
            tipoPedido: tipoPedido,
            chAgrupar: agrupado
        }),
        url: url,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            if (data.d.Rebind) refreshTable()
            else hideLoader()

            if (data.d.Status) confirmModal = true
            else
            {
                if (data.d.Message == 'connection close') {}
            }
        },
        error: function () {
            hideLoader()
        }
    })
}

/*
 * muestra la imagen de cargar
 */
function showLoader() {
    jQuery('#spinner').addClass('show')
}

function hideLoader() {
    jQuery('#spinner').removeClass('show')
}

function sweetAlertSuccess(message) {
    swal.fire({
        icon: 'success',
        position: 'top-end',
        title: message,
        showConfirmButton: false,
        timer: 1500
    })
}
function sweetAlertMessageRegresa(message) {
    swal.fire({
        title: 'Picking',
        text: message,
        showConfirmButton: false,
        timer: 5000
    })

    setTimeout(() => {
        window.location.href = 'inicio.aspx'
    }, 3000)
}


window.reload = false
function clearIframe() {
    if (window.reload) {
        showLoader()
        refreshTable()
    }
    document.getElementById('iframeContent').src = 'about:blank'
}

function setIframe(src, height, parameters) {
    window.reload = false
    var iframe = document.getElementById('iframeContent')
    iframe.style.height = height
    iframe.src = src + parameters;

    jQuery('#iframeModal').modal('show')
}