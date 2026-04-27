var CapUsuariosFacturaCancelada = (function () {
    // Variables privadas
    var tblUsuarios = null;

    // Función para mostrar el indicador de carga
    function mostrarLoading() {
        document.getElementById('loading').style.display = 'flex';
    }

    // Función para ocultar el indicador de carga
    function ocultarLoading() {
        document.getElementById('loading').style.display = 'none';
    }

    // Función para cargar usuarios por sucursal
    function cargarUsuarios() {
        mostrarLoading();

        fetch(ApplicationUrl + '/api/CapUsuariosFacturaCancelada/CatalogoUsuarios', {
            mode: "cors",
            cache: "no-cache",
            credentials: "same-origin",
            redirect: "follow",
            referrer: "no-referrer"
        })
        .then(function (response) {
            if (!response.ok) {
                throw new Error('Error en la consulta: ' + response.statusText);
            }
            return response.json();
        })
        .then(function (data) {
            // llenar txtCorreo
            document.getElementById('txtCorreo').value = data.entCorreos.strCorreo;
        })
        .catch(function (error) {
            alertify.error("Error: " + error.message);
            console.error(error);
        })
        .finally(function () {
            ocultarLoading();
        });
    }

    // Función para agregar un usuario
    function GuardarCorreo() {
        //  Obtener el valor del input de correo
        var correo = document.getElementById('txtCorreo').value.trim();
        // Validar que el correo no esté vacío
        if (!correo) {
            alertify.error("Ingrese un correo electr&oacute;nico");
            return;
        }           

        // validar coreo o lista de correos separados por ;
        var correos = correo.split(';').map(function (c) { return c.trim(); });
        // Validar que todos los correos sean válidos
        var regexCorreo = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/; // Expresión regular simple para validar correos
        for (var i = 0; i < correos.length; i++) {
            if (!regexCorreo.test(correos[i])) {
                alertify.error("Ingrese un correo electr&oacute;nico v&aacute;lido: " + correos[i]);
                return;
            }
        }

        correo = correos.join(';');
        // Validar que el correo no tenga más de 500 caracteres
        if (correo.length > 500) {
            alertify.error("El correo electr&oacute;nico no puede tener m&aacute;s de 500 caracteres");
            return;
        }     
        
        mostrarLoading();

        fetch(ApplicationUrl + '/api/CapUsuariosFacturaCancelada/AgregarUsuario?strCorreo=' + correo, {
            mode: "cors",
            cache: "no-cache",
            credentials: "same-origin",
            redirect: "follow",
            referrer: "no-referrer"
        })
        .then(function (response) {
            if (!response.ok) {
                throw new Error('Error en la operaci&oacute;n: ' + response.statusText);
            }
            return response.json();
        })
        .then(function (data) {
            if (data && data.success) {                
                alertify.success("Correo agregado correctamente");
            } else {
                alertify.error("No se pudo agregar el Correo");
            }
        })
        .catch(function (error) {
            alertify.error("Error: " + error.message);
            console.error(error);
        })
        .finally(function () {
            ocultarLoading();
        });
    }

    // Función para inicializar eventos
    function inicializarEventos() {
        $('#btnGuardar').on('click', function (e) {
            e.preventDefault(); // Evitar postback
            GuardarCorreo();
            return false;
        });
    }

    // Función de inicialización pública
    function init() {
        inicializarEventos();
        cargarUsuarios();
    }

    // Interfaz pública
    return {
        init: init
    };
})();

// Inicializar el módulo cuando el documento esté listo
$(document).ready(function() {
    CapUsuariosFacturaCancelada.init();
    // Evitar postback en todo el formulario si existe
    $('form').on('submit', function (e) {
        e.preventDefault();
        return false;
    });
});