
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
function PaginacionTerritorios_Inicializar(paginaActual, totalRegistros) {

    var content = $('#PaginacionPie')
    content.empty()

    var totalPages = Math.ceil(totalRegistros / ACyS_RenglonesXPagina)

    if (totalPages > 0) {

        var initialPage = 1
        if (totalPages > ACyS_Paginacion_MaxPages) {
            if (paginaActual == totalPages) initialPage += paginaActual - ACyS_Paginacion_MaxPages
            else initialPage += paginaActual >= ACyS_Paginacion_MaxPages
                ? ((paginaActual - ACyS_Paginacion_MaxPages) + 1)
                : 0
        }

        var backButton = $('<li style="cursor:pointer">')
        if (paginaActual == initialPage)
            backButton.append('<a>‹</a>')
        else backButton.append('<a onclick="Paginacion_Repaginar(this)" data-pagina=' + (paginaActual - 1) + '>‹</a>')

        content.append(backButton)

        var condition = totalPages
        if (totalPages > ACyS_Paginacion_MaxPages) {
            if (totalPages < ACyS_Paginacion_MaxPages) condition = totalPages
            else condition = paginaActual >= ACyS_Paginacion_MaxPages
                ? ((ACyS_Paginacion_MaxPages + initialPage) - 1)
                : ACyS_Paginacion_MaxPages
        }

        for (var i = initialPage; i <= condition; i++) {

            var active = i == paginaActual
            var pageButton =
                $('<li class="' + (active ? 'active' : '') + '" style="cursor:pointer;">')
                    .append('<a onclick="Paginacion_Repaginar(this)" data-pagina=' + i + '>' + i + '</a>')

            content.append(pageButton)
        }

        var nextButton = $('<li style="cursor:pointer">')
        if (paginaActual == totalPages)
            nextButton.append('<a>›</a>')
        else nextButton.append('<a onclick="Paginacion_Repaginar(this)" data-pagina=' + (paginaActual + 1) + '>›</a>')
        content.append(nextButton)
    }
}


