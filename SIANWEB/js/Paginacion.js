
function Paginacion_Repaginar(obj) {
    var p = $(obj).data('pagina');
    ACyS_PaginaActual = p;
    Cargar_Indice(p, ACyS_RenglonesXPagina);
    Paginacion_Inicializar(ACyS_PaginaActual, ACyS_RegistroEncontrados);
    console.log('pagina:' + p);
}

function Paginacion_Inicializar(PaginaActual, TotalRegistros) {
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

    var li = $('<li>').append('<a data-pagina="' + PaginaActual + '" onclick="Paginacion_Repaginar(this);">Inicio</a>');
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
                    $('<li class="active">').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            } else {
                $('#PaginacionPie').append(
                    $('<li>').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            }
        }
        //PaginaActual = PaginaActual + 1;
    }

    $('#PaginacionPie').append(
        $('<li>').append('<a data-paginaactual=' + PAG + ' data-pagina="' + (PaginaActual + 1) + '" onclick="Paginacion_Repaginar(this);">Siguiente</a>')
    );

}

function GC_Paginacion_Repaginar(obj) {
    var p = $(obj).data('pagina');
    //GC_PaginaActual = p;
    GC_PageNumber = p;

    GC_ASC.Cargar_Indice();
    GC_Paginacion_Inicializar(GC_PaginaActual, ACyS_RegistroEncontrados);
}

function GC_Paginacion_Inicializar(PaginaActual, TotalRegistros) {
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

    var li = $('<li>').append('<a data-pagina="' + PaginaActual + '" onclick="GC_Paginacion_Repaginar(this);">Inicio</a>');
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
                    $('<li class="active">').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="GC_Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            } else {
                $('#PaginacionPie').append(
                    $('<li>').append('<a data-paginaactual=' + PAG + ' data-pagina="' + PAG + '" onclick="GC_Paginacion_Repaginar(this);">' + PAG + '</a>')
                );
            }
        }
    }

    $('#PaginacionPie').append(
        $('<li>').append('<a data-paginaactual=' + PAG + ' data-pagina="' + (PaginaActual + 1) + '" onclick="GC_Paginacion_Repaginar(this);">Siguiente</a>')
    );

}
