
function paginarSiguiente() {
    if ($("#inputTotalDePaginas").val() > $("#inputPaginaActual").val()) {
        var paginaActual = $("#inputPaginaActual").val();
        $("#inputPaginaActual").val(parseInt(paginaActual) + 1);
        $("#formFiltro").submit();
    }
}

function paginarAnterior() {
        if ($("#inputPaginaActual").val() > 1) {

            var paginaActual = $("#inputPaginaActual").val();
            $("#inputPaginaActual").val(paginaActual - 1);
            $("#formFiltro").submit();
        }
    }


