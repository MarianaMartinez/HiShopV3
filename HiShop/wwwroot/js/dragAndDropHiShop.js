 //GuardarShowrrom
    //AxelMolaro
function guardarShowroom() {

        var idBanner = $("#idBannerActivo").val();
        var colorBanner = $("#colorBannerActivo").val();
        var estaVacioSeccionBanner = $("#estaVacioSeccionBanner").val();
        var esNuevoBanner = $("#esNuevoBanner").val();
        var textoPrincipalBanner = $("#textoPrincipalBannerActivo").val();

        var colorFondoPrincipal = $("#colorDeFondoGenetal").val();

        var idCuerpo = $("#idCuerpoActivo").val();
        var esNuevoCuerpo = $("#esNuevoCuerpo").val();
        $.ajax({
            url: '/Showroom/GuardarShowroom',
            data: {
                idBanner, colorBanner, estaVacioSeccionBanner, esNuevoBanner, textoPrincipalBanner,
                colorFondoPrincipal,
                idCuerpo, esNuevoCuerpo
            },
            type: 'POST',
            success: (function (respuesta) {
                alert(respuesta.data.mensaje);
                $("#esNuevoBanner").val(false);
                $("#idBannerActivo").val(respuesta.data.idBannerActual);
                $("#esNuevoCuerpo").val(false);
            }),
            error: (function (result) {
                alert("Error al guardar el showroom");
            }),
        })
}



    //Llena el catalogo, le paso el contenedor anterior y el elemento del item producto
function llenarCatalogoAjax(contenedor, elementoItemHTML) {
    elementoItemHTML.hide();
        $.ajax({
            url: '/Showroom/OptenerProductos',
            type: 'POST',

            success: (function (respuesta) {
                var productos = respuesta.data.productos;
                //debugger;
                for (var i = 0; i < respuesta.data.productos.length; i++)
                {
                    //debugger;
                    var elemento = elementoItemHTML.clone();
                    elemento.removeClass("itemProductoBase");
                    var elementoTitulo = elemento.find("[pk='tituloCuerpo-0']");
                    var elementoDescripcion = elemento.find("[pk='descripcionCuerpo-0']");
                    var elementoPrecio = elemento.find("[pk='precioCuerpo-0']");
                    var elementoImagen = elemento.find("[pk='imagenCuerpo-0']");
                    var nombre = productos[i].nombre;
                    var descripcion = productos[i].descripcion;
                    var precio = productos[i].precio;
                    var urlImagen = productos[i].urlImagen;

                    elementoTitulo.text(nombre);
                    elementoDescripcion.text(descripcion);
                    elementoPrecio.text("$ "+precio);
                    elementoImagen.attr("src", urlImagen);
                    elemento.show();
                    contenedor.append(elemento);
                }
                
            }),
            error: (function (result) {
                alert("Error al traer productos.");
            }),
        })
        
}

//Llena el catálogo con los lins de los a, se usa para la parte de ver catálogo
//AxelMolaro
function llenarCatalogoAjaxConLinks(contenedor, elementoItemHTML) {
    elementoItemHTML.hide();
    $.ajax({
        url: '/Showroom/OptenerProductos',
        type: 'POST',

        success: (function (respuesta) {
            var productos = respuesta.data.productos;
            //debugger;
            for (var i = 0; i < respuesta.data.productos.length; i++) {
                //debugger;
                var elemento = elementoItemHTML.clone();
                elemento.removeClass("itemProductoBase");
                var elementoTitulo = elemento.find("[pk='tituloCuerpo-0']");
                var elementoDescripcion = elemento.find("[pk='descripcionCuerpo-0']");
                var elementoPrecio = elemento.find("[pk='precioCuerpo-0']");
                var elementoImagen = elemento.find("[pk='imagenCuerpo-0']");
                var elementoLink = elemento.find("[pk='linkCuerpo-0']");
                var nombre = productos[i].nombre;
                var descripcion = productos[i].descripcion;
                var precio = productos[i].precio;
                var urlImagen = productos[i].urlImagen;
                var linkProducto = "/Catalogo/DetallesDeProducto/" + productos[i].id;
                elementoTitulo.text(nombre);
                elementoDescripcion.text(descripcion);
                elementoPrecio.text("$ " + precio);
                elementoImagen.attr("src", urlImagen);
                elemento.attr("href", linkProducto);
                elemento.show();
                contenedor.append(elemento);
            }

        }),
        error: (function (result) {
            alert("Error al traer productos.");
        }),
    })

}

    //Optiene el html del banner
    function agregarBanner(id) {
        var idBanner = id;
        var html = "";
        $.ajax({
            async: false,
            url: '/Showroom/OptenerHtmlBanner',
            type: 'POST',
            data: {
                idBanner
            },
            success: (function (respuesta) {
                html = respuesta.data.html;
                $("#contenedorBanner").ready(function () {
                    //debugger;
                    var contenedorBanner = $("#contenedorBanner");
                    contenedorBanner.append(formatearHtml(html));
                });
            }),
            error: (function (result) {
                alert("Error al traer banner.");
            }),
            
        })
    }

    //Optiene el html del cierpo
    function agregarCuerpo(id) {
        var idCuerpo = id;
        $.ajax({
            async: false,
            url: '/Showroom/OptenerHtmlCuerpo',
            type: 'POST',
            data: {
                idCuerpo
            },
            success: (function (respuesta) {
                var html = respuesta.data.html;
                $("#contenedorCuerpo").ready(function () {
                    var contenedorCuerpo = $("#contenedorCuerpo");
                    contenedorCuerpo.append(formatearHtml(html));
                });

            }),
            error: (function (result) {
                alert("Error al traer cuerpo.");
            }),
        })
    }

    var contenedorPrincipal;
window.addEventListener('load', init);
function init() {
    
    /*ocultar elementos*/
    $(".pie").hide();
    $("#botonMenuDAD").hide();
    /*variables*/

    /*funciones*/
    $("#botonMinimizarMenuDAD").click(function () {
        ocultarMenuDAD();
    });
    $("#botonMenuDAD").click(function () {
        mostrarMenuDAD();
    });
    aplicarFuncionesElementosFondo();
    aplicarFuncionesElementosMenu();
    aplicarFuncionesContenedorPrincipal();
    aplicarFuncionesElementosBanner();
    aplicarFuncionesDeBotonesDeSeleccionarTipoDeElementoMenuDAD();

    /*Banner*/
    $("#colorPickerBannerInput").ready(function () {
        $("#colorPickerBannerInput").change(function () {
            var color = $("#colorPickerBannerInput").val();
            llenarDatosFormularioGeneralBanners(false, null, color);
            cambiarColorPrincipalDelBanner(color);
        });
    });

    $("#textoPrincipalBannerInput").ready(function () {
        $("#textoPrincipalBannerInput").on("keyup",function () {
            var texto = $("#textoPrincipalBannerInput").val();
            cambiarElTextoDeUnElemento(texto, $(".textoPrincipal"));
            cambiarElValueDeUnElemento(texto, $("#textoPrincipalBannerActivo"));
        });
    });


   
}

/************************************Elementos General***************************/

/*Agrega las funcioenes dad a los elementos del menu
AxelMolaro
*/
function aplicarFuncionesElementosMenu() {
    var bannerElementos = $(".elementoMenu");
    for (var i = 0; i < bannerElementos.length; i++) {
        var elemento = bannerElementos[i];
        elemento.addEventListener('dragstart', dragIniciadoElementoMenu, false);
    }
}
/**
 * Funcion cuando se agarra un elemento del menu
    Pasa por data transfer el html
    AxelMolaro
 * @param {any} e
 */
function dragIniciadoElementoMenu(e) {
    var tipoElemento = this.getAttribute("tipo");
    var htmlElemento = this.getAttribute("htmlIncluido");
    var idElemento = this.getAttribute("idBdd");
    var param = { id : idElemento,tipo: tipoElemento, html: htmlElemento };
    var data = JSON.stringify(param);
    e.dataTransfer.setData("param", data);
}
/**********************************Banners************************************/

/*
Funciones de inicializacion de los banner
AxelMolaro
*/
function aplicarFuncionesElementosBanner() {
    $("#contenedorBanner").ready(function () {
        var color = $("#colorPickerBannerInput").val();
        var titulo = $("#textoPrincipalBannerActivo").val();
        if (color != "" && color != null && color != "undefined")
            $("#colorPickerBannerInput").val(color);
        cambiarColorPrincipalDelBanner(color);
        cambiarElTextoDeUnElemento(titulo, $(".textoPrincipal"));
        aplicarSeleccionable($("#contenedorBanner"));

        $("#contenedorBanner").click(function () {
            $(".tablaEditable").hide();
            ocultarElemento("ElementosSeleccionablesBanner");
            ocultarElemento("ElementosSeleccionablesCuerpoInicio");
            mostrarElemento("tablaBannerEditable");
        });
    });
}
/**
/*Maneja el drop cuando los elementos son banners*/
function aplicarDropBanner(data) {
    var contenedorBanner = $("#contenedorBanner");
    contenedorBanner.empty();
    var html = data.html;
    html = formatearHtml(html);
    var id = data.id;
   $("#esNuevoBanner").val(true);
    contenedorBanner.append(html);
    llenarDatosFormularioGeneralBanners(true, id, null);
    $("#colorPickerBannerInput").val("#ffa500");
    $("#colorBannerActivo").val("#ffa500");
    $(".minicolors-swatch-color").css("backgroundColor", "orange");
    cambiarElValueDeUnElemento("Banner", $("#textoPrincipalBannerInput"));
    cambiarElValueDeUnElemento("Banner", $("#textoPrincipalBannerActivo"));
    
    activarColorPicker();
}

function llenarDatosFormularioGeneralBanners(limpiaFormulario,idBanner,colorBanner)
{
    if (limpiaFormulario == true)
        limpiarFormularioBanners();

    if (idBanner != null && idBanner != "undefined")
        $("#idBannerActivo").val(idBanner);

    if (colorBanner != null && colorBanner != "undefined")
        $("#colorBannerActivo").val(colorBanner);
}



//Elmina el banner del showroom del contenedor principal
//AxelMolaro
function eliminarBanner()
{
    var contenedorBanner = $("#contenedorBanner");
    contenedorBanner.empty();
    limpiarFormularioBanners();
}
/**
 * Limpia todos los datos del formulario genral correspondiente a banners
 */
function limpiarFormularioBanners()
{
    var inputs = $(".inputBannerFormGeneral");
    inputs.val("");

}

//Cambia el color del banner en el front 
//AxelMolaro
function cambiarColorPrincipalDelBanner(color)
{
    
    var elementosPrincipales = $(".colorPrincipalBanner");
    elementosPrincipales.css("background", color);
}

/**
 * Ve si esta vacio la seccion de banners en el formulario genral y setea el input
 */
function estaVacioLaSeccionBannerGeneral()
{
    var inputs = $(".inputBannerFormGeneral");
    var inputSeccion = $("#estaVacioSeccionBanner");
    if (inputs.val() != "") {
        inputSeccion = "No";
    } else
    {
        inputSeccion = "Si";
    }
}
/**********************************Cuerpo Inicio******************************************/
function aplicarDropCuerpoHome(data) { //todo seguir con esta funcion
    var contenedorCuerpo = $("#contenedorCuerpo");
    contenedorCuerpo.empty();
    var html = data.html;
    html = formatearHtml(html);
    var id = data.id;
    $("#esNuevoCuerpo").val(true); 
    contenedorCuerpo.append(html);
    llenarDatosFormularioGeneralCuerpo(true, id, null);
   
    var contenedorProductos = $("#contenedorCuerpo .container");
    var itemProducto = $(".itemProducto");
    llenarCatalogoAjax(contenedorProductos, itemProducto);
    //$("#colorPickerBannerInput").val("#ffa500");
    //$("#colorBannerActivo").val("#ffa500");
    //$(".minicolors-swatch-color").css("backgroundColor", "orange");
   // cambiarElValueDeUnElemento("Banner", $("#textoPrincipalBannerInput"));
    //cambiarElValueDeUnElemento("Banner", $("#textoPrincipalBannerActivo"));

    //activarColorPicker();
}

//Llena la pantalla del showroom
function llenarPantalla() {
    $("#idBannerActivo").ready(function () {

        var idBanner = $("#idBannerActivo").val();
        if (idBanner != 0 && idBanner != "")
            agregarBanner(idBanner);

    });
    $("#contenedorCuerpo").ready(function () {
        $("idCuerpoActivo").ready(function () {
            var idCuerpo = $("#idCuerpoActivo").val();
            if (idCuerpo != 0 && idCuerpo != "")
                agregarCuerpo(idCuerpo);
            $("#contenedorCuerpo .container").ready(function () {

                $(".itemProducto").ready(function () {

                    var contenedorProductos = $("#contenedorCuerpo .container");
                    var itemProducto = $(".itemProducto");
                    //debugger;
                    llenarCatalogoAjax(contenedorProductos, itemProducto);
                });

            });

        });

    });
    llenarEstaVacioBannerFormularioGeneral();
}





function llenarDatosFormularioGeneralCuerpo(limpiaFormulario, idCuerpo,tipoDeLetra) {
    if (limpiaFormulario == true)
        limpiarFormularioCuerpos();

    if (idCuerpo != null && idCuerpo != "undefined")
        $("#idCuerpoActivo").val(idCuerpo);
}

function limpiarFormularioCuerpos() {
    var inputs = $(".inputCuerpoFormGeneral");
    inputs.val("");

}



/****************************Fondo general************************************************/

//Cambia el color del banner en el front 
//AxelMolaro
function cambiarColorPrincipalDelFondo(color) {

    var body = $("body");
    body.css("background", color);
}

function aplicarFuncionesElementosFondo() {
    $("body").ready(function () {
        $("#colorPickerFondoInput").ready(function () {
            var color = $("#colorPickerFondoInput").val();
            cambiarColorPrincipalDelFondo(color);
            $("#colorDeFondoGenetal").ready(function () {
                $("#colorDeFondoGenetal").val(color);
            });
        });
    });
    $("#colorPickerFondoInput").change(function () {
        var color = $("#colorPickerFondoInput").val();
        llenarDatosFormularioGeneralPrincipal(false, color);
        cambiarColorPrincipalDelFondo(color);
    });
}
//Llena la seccion de datos generales del formulario del showroom
function llenarDatosFormularioGeneralPrincipal(limpiaFormulario, colorDeFondo) {
    if (limpiaFormulario == true)
        limpiarFormularioPrincipal();
    if (colorDeFondo != null && colorDeFondo != "undefined")
        $("#colorDeFondoGenetal").val(colorDeFondo);
}

 //Limpia la seccion de datos generales del showroom
function limpiarFormularioPrincipal() {
    var inputs = $(".inputFormularioPrincipal");
    inputs.val("");

}

/**
/**********************************Contenedor principal**********************************/

/*Agrega las funcioenes dad a los elementos del menu
AxelMolaro
*/
function aplicarFuncionesContenedorPrincipal() {
    contenedorPrincipal = document.querySelector("#contenedorPrincipal");
    contenedorPrincipal.addEventListener('dragover', contenedorOver, false);
    contenedorPrincipal.addEventListener('drop', manejarDrop, false);

}

function contenedorOver(e) {
    e.preventDefault();

}

/**
 * Recibe un json data, que le pasa el dragIniciadoElementoMenu que contiene
    el html y el tipo y lo trata en el drop
 * @param {any} e
 */
function manejarDrop(e) {
    e.preventDefault();
    var data = JSON.parse(e.dataTransfer.getData("param"));
    var tipo = data.tipo;
    var html = data.html;
    if (tipo == "Banner") {
        aplicarDropBanner(data);
    } else if (tipo == "CurepoHome"){
        aplicarDropCuerpoHome(data);
    } 
    else {

        this.innerHTML += datos; //this seria el body que disparo el elemento
    }
}

/***************************Menu************************************/

//Muesta el menu con los los elementos de banner
function mostrarMenuDAD()
{
    var menu = $("#MenuElementosDAD");
    var botonMenu = $("#botonMenuDAD");
    botonMenu.hide();
    menu.show();
}

//Oculta el menu dad de los elementos del menu
function ocultarMenuDAD() {
    var menu = $("#MenuElementosDAD");
    var botonMenu = $("#botonMenuDAD");
    menu.hide();
    botonMenu.show();
}

//Oculta todas las tablas seleccionables de lementos dad
function ocultarTablasElementosDAD()
{
    $(".elementosSeleccionablesMenu").hide();
}

function aplicarFuncionesDeBotonesDeSeleccionarTipoDeElementoMenuDAD() {
    $("#btnMenuBanners").click(function () {
        ocultarTablasElementosDAD();
        mostrarElemento("ElementosSeleccionablesBanner");
    });
    $("#btnMenuCuerpoInicio").click(function () {
        ocultarTablasElementosDAD();
        mostrarElemento("ElementosSeleccionablesCuerpoInicio");

    });
    $("#btnMenuFondo").click(function () {
        ocultarTablasElementosDAD();
        mostrarElemento("ElementosSeleccionablesFondo");

    });
}



/****************************Formulario General ****************************/
//Llena algunos datos del formulario general de banner
function llenarEstaVacioBannerFormularioGeneral() {
    if ($("#idBannerActivo").val() != "") {
        $("#estaVacioSeccionBanner").val("No");
    } else {
        $("#estaVacioSeccionBanner").val("So");
    }

}
/****************Generales********************/
//Formate un string que recibe de base de daos para que el bloque de codigo sea correcto y leible por el ordenador
//AxelMolaro
function formatearHtml(texto)
{
    var textoFormateado = texto;
    for (var i = 0; i < 100; i++) {
        textoFormateado = textoFormateado
            .replace("&lt;", "<")
            .replace("&gt;", ">")
            .replace("&#x2f;", "/")
            .replace("&#47;", "/")
            .replace("&apos;", "'")
            .replace("&#39;", "'")
            .replace("&#x27;", "'");
    }
    for (var i = 0; i < 100; i++) {
        textoFormateado = textoFormateado
            .replace(";", "'");
    }
    return textoFormateado;
}

//cambiar el texto de un elemento
//axel molaro
function cambiarElTextoDeUnElemento(texto, elemento)
{
    elemento.text(texto);
}

//cambiar el value de un elemento
//axel molaro
function cambiarElValueDeUnElemento(val, elemento) {
    elemento.val(val);
}

function mostrarSeleccionable(elemento) {
    elemento.addClass("sombra");
}

function ocultarSeleccionable(elemento) {
    elemento.removeClass("sombra");
}
//Muestra una animacion que es seleccionable el elemento
//AxelMolaro
function aplicarSeleccionable(elemento) {
    elemento.mouseover(function () {
        mostrarSeleccionable(elemento)
    });
    elemento.mouseleave(function () {
        ocultarSeleccionable(elemento)
    });
}


function mostrarElemento(id)
{
    $("#" + id).fadeIn(1);
}

function ocultarElemento(id) {
    $("#" + id).fadeOut(1);
}


function volverAElementos() {
    $(".tablaEditable").hide();
    mostrarElemento("ElementosSeleccionablesBanner");
}