
var aCRM_Prospecto_Tour = {
  data : [
    { element: '#btnNuevoProspecto', 'tooltip' : 'Este es el botón Nuevo prospecto, aqui iniciar la captura de un prospecto.' },
    { element: '#btnAyudaTour', 'tooltip' : 'Este es el botón Ayuda de prospecto, aquí inciar el toru de la aplicación.' },
    { element: '#lstProspectos', 'tooltip': 'Este es el listado o embudo de prospectos.' },
    { element: '#tblProspectos_filter', 'tooltip': 'Filtro de prospectos, Aquí teclea el nombre y automaticamente se aplica el filtro a el listado de prospectos.' },
    { element: '#dvGeneral tab-pane', 'tooltip': 'El tab despliega la información basica del cliente, esta informació es de solo lextrua.' },
    { element: '#dvNotas', 'tooltip': 'El tab despliega la información de notas, son todas las notas realizadas por el RIK de este prospecto.' },
    { element: '#btnCrearNota', 'tooltip': 'Puede utilizar el botón "Agregar Nota" para crear una nota nueva.' },
    { element: '#dvSeccionTerritorios', 'tooltip': 'Despliega la información de los territorios ligados al prospecto.' }
  ],  
  welcomeMessage: 'Bienvenidos al tour de la aplicacion CRM Prospectos.',
  controlsPosition : 'TR',
    buttons: {
                    next  : { text : 'Siguente &rarr;', class : 'btn btn-default'},
                    prev  : { text : '&larr; Anterior', class: 'btn btn-default' },
                    start : { text : 'Iniciar', class: 'btn btn-primary' },
                    end   : { text : 'Fin', class: 'btn btn-primary' }
    },
    controlsCss: {
                    background: 'rgba(124, 124, 124, 0.9)',
                    color: '#fff',
                    width: '400px',
                    'border-radius': 0
    }
}

//$.aSimpleTour(aCRM_Prospecto_Tour);

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// ready
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

        
    $('#btnAyudaToru').click(function () {
        $.aSimpleTour(aCRM_Prospecto_Tour);
    });

});
