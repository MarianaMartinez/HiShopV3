/*
    Esta clase la uso para las funciones de mostrar mensajes , la clasie
    AxelMolaro
*/


function crearMensaje(idElemento,mensaje,tipo)
{
    var clase;
    debugger;
    if (tipo == "EXITO")
    {
        clase = "alert-success" 
    }
    if (tipo == "ADVERTENCIA") {
        clase = "alert-warning"
    }
    if (tipo == "ERROR") {
        clase = "alert-danger"
    }

    var elementoContenedor = $("#" + idElemento);

    debugger;
    var cantidadDeMensajes = $(".mensajeAutoCreado").length;
    $("#mensajeAutoCreado-" + cantidadDeMensajes).fadeOut(2);
    var valor = cantidadDeMensajes + 1;
    var id = "mensajeAutoCreado-" + valor;
    elementoContenedor.append("<div id='" + id + "' class=' mensajeAutoCreado alert " + clase + "'>" + mensaje + "</div>");
   
    
}