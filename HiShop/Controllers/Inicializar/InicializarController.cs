using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity;
using HiShop.Entity.Data;

namespace HiShop.Controllers.Inicializar
{
    public class InicializarController : BaseCoreController
    {
        public InicializarController(HiShopContext context) : base(context)
        {
        }

        /// <summary>
        /// Inicializa los datos para test
        /// </summary>
        /// <returns></returns>
        public IActionResult InicializarDatosGeneralesDeTest()
        {
            return View();
        }

        /// <summary>
        /// Inicializa los elementos del menu de drag and drop
        /// </summary>
        /// <returns></returns>
        public IActionResult InicializarMenuDragAndDrop()
        {
            inicializarBanners();
            inicializarCuerpoHome();
            return View();
        }

        public void inicializarBanners()
        {

            ElementoMenuDAD bannerClasico = new ElementoMenuDAD();
            bannerClasico.tipo = Enum.TipoElementoMenuDAD.Banner;
            bannerClasico.Titulo = "Banner Clasico";
            bannerClasico.UrlImagen = "";
            bannerClasico.Html = "<div class='banner bannerElementoColocado colorPrincipalBanner ' style='background-color:orange;'> <span class='textoPrincipal'> Banner <span> </div >";
            _context.ElementosMenuDADs.Add(bannerClasico);
            _context.SaveChanges();

            ElementoMenuDAD bannerClasicoConMenu = new ElementoMenuDAD();
            bannerClasicoConMenu.tipo = Enum.TipoElementoMenuDAD.Banner;
            bannerClasicoConMenu.Titulo = "Banner Clasico Con Menú";
            bannerClasicoConMenu.UrlImagen = "";
            bannerClasicoConMenu.Html = "<div class='bannerElementoColocado colorPrincipalBanner ' '><nav class=' colorPrincipalBanner navbar navbar-default' style='background-color:orange;>" +
                                    "<div class='container-fluid'>"+
                                         " <div class='navbar-header'>"+
                                                    " <a class='navbar-brand textoPrincipal' href='#'>Banner</a>" +
                                        "</div>"+
                                        "<ul class='nav navbar-nav'>"+
                                        "<li><a href ='HomeBannerLink'> Inicio </a></li>"+
                                        "<li><a href='ProductosBannerLink'>Productos</a></li>"+
                                        "</ul>"+
                                        "</div>"+
                                      "</nav>"+
                                "</div>";
            _context.ElementosMenuDADs.Add(bannerClasicoConMenu);
            _context.SaveChanges();

        }

        public void inicializarCuerpoHome()
        {
            ElementoMenuDAD curpoProductoClasico = new ElementoMenuDAD();
            curpoProductoClasico.tipo = Enum.TipoElementoMenuDAD.CurepoHome;
            curpoProductoClasico.Titulo = "Cuerpo de productos disponibles clásico";
            curpoProductoClasico.UrlImagen = "";
            String html = "<div class='container' style='margin-top:4px;'>" +
          "<a class='linkProducto itemProducto itemProductoBase' href='' pk='linkCuerpo-0'>" +
              "<div class=' productoSeleccionable  col-md-4'>" +
                "<img class='img-thumbnail imagenProducto' pk='imagenCuerpo-0' height='375px' width='375px' src='http://mxcdn.ar-cdn.com/recipes/originals/nophoto.svg' />" +
                 "<p class='tituloPodructo' pk='tituloCuerpo-0' style='text-align:center'>Titulo Producto</p>" +
                 "<p class='descripcionPodructo' pk='descripcionCuerpo-0' style='text-align:left'>Descripcion Producto</p>" +
                 "<p class='precioPodructo' pk='precioCuerpo-0' style='text-align:right'>Precio Producto</p>" +
              "</div>" +
          "</a>" +
         "</div>";
            curpoProductoClasico.Html = html;
            _context.ElementosMenuDADs.Add(curpoProductoClasico);
            _context.SaveChanges();

        

        }
    }
}