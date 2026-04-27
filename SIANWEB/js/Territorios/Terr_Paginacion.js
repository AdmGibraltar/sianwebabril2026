
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Paginacion_Repaginar(obj) {
    $('#spinner_Indice').css('display', 'block');
    var p = $(obj).data('pagina');
    Paginacion_Actual = p;

    var Ter_Activo = $('#ddlTer_Activo').val();

    //Cargar_Indice(p, ACyS_RenglonesXPagina);    
    //Territorios_CargarIndice()

    Territorios_CargarIndice(
        Paginacion_Actual,
        Paginacion_PageSize,
        $('#tbParametroBusqueda').val(),
        $('#ddlTipoRepresentante').val(),
        Ter_Activo,
        function (RES) {
            Desplegar_Indice(RES)
            PaginacionTerritorios_Inicializar(Paginacion_Actual, ACyS_RegistroEncontrados);
            console.log('pagina:' + p);

            setTimeout(function () {
                $('#spinner_Indice').css('display', 'none');
            }, 1000);

        },
        function () {
            setTimeout(function () {
                $('#spinner_Indice').css('display', 'none');
            }, 1000);
        }
    );
    //console.log('TotalRegistros(1):' + ACyS_RegistroEncontrados);
    //PaginacionTerritorios_Inicializar(Paginacion_Actual, ACyS_RegistroEncontrados);
    //console.log('pagina:' + p);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function PaginacionTerritorios_Inicializar(PaginaActual, TotalRegistros) {
    //console.log('TotalRegistros(2):' + TotalRegistros);
    var MaxPaginas = TotalRegistros / ACyS_RenglonesXPagina;

    MaxPaginas = parseFloat(MaxPaginas);
    //console.log('MaxPaginas(1):' + MaxPaginas);
    var iMaxPaginas = parseInt(MaxPaginas);
    if ((MaxPaginas - iMaxPaginas) > 0) {
        MaxPaginas = iMaxPaginas + 1;
    }
    //console.log('MaxPaginas(2):' + MaxPaginas);

    $('#PaginacionPie').empty();
    var li = $('<li>').append('<a data-pagina="' + PaginaActual + '" onclick="Paginacion_Repaginar(this);" style="cursor:pointer;">Inicio</a>');
    $('#PaginacionPie').append(li);

    for (var i = 1; i < ACyS_Paginacion_MaxPages; i++) {

        if (PaginaActual <= ACyS_Paginacion_MaxPages) {
            var PAG = i;
        } else {
            var PAG = PaginaActual + i;
        }

        if ((PAG) > MaxPaginas) {
        } else {
            if (PAG == PaginaActual) {
                $('#PaginacionPie').append(
                    $('<li class="active" style="cursor:pointer;">').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            } else {
                $('#PaginacionPie').append(
                    $('<li style="cursor:pointer;">').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            }
        }
        //PaginaActual = PaginaActual + 1;
    }

    $('#PaginacionPie').append(
        $('<li style="cursor:pointer;">').append('<a data-paginaactual=' + PAG + ' data-pagina="' + (PaginaActual + 1) + '" onclick="Paginacion_Repaginar(this);">Siguiente</a>')
    );

}


