/**
 * CapOrdenServicio_ApiHelper.js
 * Funciones generales para realizar peticiones al API con fetch
 * Incluye manejo de errores, loading y validación de sesión
 * Versión: 1.2
 * Fecha: 2025-01-15
 */

// Función Loading con efecto fade-out centrado y delay mínimo
var Loading = (function () {
    var timeoutId = null;
    var showTime = 0;
    var minDisplayTime = 500; // Tiempo mínimo en ms para mostrar el loading
    
    return {
        Mostrar: function () {
            // Cancelar cualquier timeout pendiente de ocultado
            if (timeoutId) {
                clearTimeout(timeoutId);
                timeoutId = null;
            }
            
            // Registrar el momento en que se muestra
            showTime = Date.now();
            
            var loadingElement = document.getElementById('loading');
            
            // CAMBIADO: Ya no se modifica display, solo se agrega la clase show
            // El elemento siempre está con display: flex para mantener el centrado
            loadingElement.classList.add('show');
        },
        
        Ocultar: function () {
            var loadingElement = document.getElementById('loading');
            
            // Calcular cuánto tiempo ha estado visible
            var elapsedTime = Date.now() - showTime;
            var remainingTime = Math.max(0, minDisplayTime - elapsedTime);
            
            // Si no ha pasado el tiempo mínimo, esperar
            setTimeout(function() {
                // Remover clase para iniciar fade-out
                loadingElement.classList.remove('show');
                
                // Ya no es necesario cambiar display, visibility se maneja por CSS
                timeoutId = null;
            }, remainingTime);
        }
    };
})();

var ApiHelper = (function () {
    'use strict';

    /**
     * Realiza una petición fetch al API que regresa datos en formato JSON
     * @param {string} url - URL del endpoint del API
     * @param {object} options - Opciones adicionales para fetch (opcional)
     * @returns {Promise} Promise con los datos JSON
     */
    function fetchJson(url, options) {
        // Mostrar loading
        Loading.Mostrar();

        // Configuración por defecto
        const defaultOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        };

        // Combinar opciones
        const fetchOptions = Object.assign({}, defaultOptions, options || {});

        return fetch(url, fetchOptions)
            .then(function (response) {
                // Verificar si la respuesta es exitosa
                if (!response.ok) {
                    // Manejar errores HTTP
                    if (response.status === 500) {
                        throw new Error('Error');
                    } else if (response.status === 401 || response.status === 403) {
                        // Error de sesión
                        throw new Error('SESION_TERMINADA');
                    } else {
                        throw new Error('Error HTTP ' + response.status + ': ' + response.statusText);
                    }
                }
                return response.json();
            })
            .then(function (data) {
                // Ocultar loading
                Loading.Ocultar();

                // Verificar si el API regresa mensaje de sesión terminada
                if (data && (data.mensaje === 'Sesión no encontrada' || data.mensaje === 'Sesión terminada')) {
                    throw new Error('SESION_TERMINADA');
                }

                return data;
            })
            .catch(function (error) {
                // Ocultar loading en caso de error
                Loading.Ocultar();

                // Manejar errores específicos
                if (error.message === 'SESION_TERMINADA') {
                    alertify.error('Su sesión ha terminado. Será redirigido al login.');
                    setTimeout(function () {
                        window.location.href = ApplicationUrl + '/login.aspx';
                    }, 2000);
                    throw error;
                } else if (error.message.indexOf('Error del servidor (500)') !== -1) {
                    alertify.error('Error en el servidor. Por favor, contacte al administrador.');
                    console.error('Error 500:', error.message);
                    throw error;
                } else {
                    alertify.error('Error en la petición: ' + error.message);
                    throw error;
                }
            });
    }

    /**
     * Realiza una petición fetch al API que regresa un archivo (blob)
     * @param {string} url - URL del endpoint del API
     * @param {string} filename - Nombre del archivo a descargar
     * @param {object} options - Opciones adicionales para fetch (opcional)
     * @returns {Promise} Promise que descarga el archivo
     */
    function fetchBlob(url, filename, options) {
        // Mostrar loading
        Loading.Mostrar();

        // Configuración por defecto
        const defaultOptions = {
            method: 'GET',
            headers: {}
        };

        // Combinar opciones
        const fetchOptions = Object.assign({}, defaultOptions, options || {});

        return fetch(url, fetchOptions)
            .then(function (response) {
                // Verificar si la respuesta es exitosa
                if (!response.ok) {
                    // Manejar errores HTTP
                    if (response.status === 500) {
                        return response.text().then(function (errorText) {
                            throw new Error('Error del servidor (500): ' + errorText);
                        });
                    } else if (response.status === 401 || response.status === 403) {
                        // Error de sesión
                        throw new Error('SESION_TERMINADA');
                    } else {
                        throw new Error('Error HTTP ' + response.status + ': ' + response.statusText);
                    }
                }
                return response.blob();
            })
            .then(function (blob) {
                // Ocultar loading
                Loading.Ocultar();

                // Crear URL temporal para el blob
                const blobUrl = window.URL.createObjectURL(blob);

                // Crear elemento <a> temporal para descargar
                const link = document.createElement('a');
                link.href = blobUrl;
                link.download = filename || 'archivo_descargado';
                document.body.appendChild(link);
                link.click();

                // Limpiar
                document.body.removeChild(link);
                window.URL.revokeObjectURL(blobUrl);

                alertify.success('Archivo descargado exitosamente');
                return true;
            })
            .catch(function (error) {
                // Ocultar loading en caso de error
                Loading.Ocultar();

                // Manejar errores específicos
                if (error.message === 'SESION_TERMINADA') {
                    alertify.error('Su sesión ha terminado. Será redirigido al login.');
                    setTimeout(function () {
                        window.location.href = ApplicationUrl + '/login.aspx';
                    }, 2000);
                    throw error;
                } else if (error.message.indexOf('Error del servidor (500)') !== -1) {
                    alertify.error('Error en el servidor. Por favor, contacte al administrador.');
                    console.error('Error 500:', error.message);
                    throw error;
                } else {
                    alertify.error('Error al descargar archivo: ' + error.message);
                    throw error;
                }
            });
    }

    /**
     * Realiza una petición POST al API con datos JSON
     * @param {string} url - URL del endpoint del API
     * @param {object} data - Datos a enviar en el body
     * @param {object} options - Opciones adicionales para fetch (opcional)
     * @returns {Promise} Promise con los datos JSON de respuesta
     */
    function postJson(url, data, options) {
        const defaultOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        const fetchOptions = Object.assign({}, defaultOptions, options || {});
        return fetchJson(url, fetchOptions);
    }

    /**
     * Realiza una petición PUT al API con datos JSON
     * @param {string} url - URL del endpoint del API
     * @param {object} data - Datos a enviar en el body
     * @param {object} options - Opciones adicionales para fetch (opcional)
     * @returns {Promise} Promise con los datos JSON de respuesta
     */
    function putJson(url, data, options) {
        const defaultOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        const fetchOptions = Object.assign({}, defaultOptions, options || {});
        return fetchJson(url, fetchOptions);
    }

    /**
     * Realiza una petición DELETE al API
     * @param {string} url - URL del endpoint del API
     * @param {object} options - Opciones adicionales para fetch (opcional)
     * @returns {Promise} Promise con los datos JSON de respuesta
     */
    function deleteJson(url, options) {
        const defaultOptions = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const fetchOptions = Object.assign({}, defaultOptions, options || {});
        return fetchJson(url, fetchOptions);
    }

    // API pública
    return {
        fetchJson: fetchJson,
        fetchBlob: fetchBlob,
        postJson: postJson,
        putJson: putJson,
        deleteJson: deleteJson
    };
})();
