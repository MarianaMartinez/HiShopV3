using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity.Data;
using HiShop.Models;
using HiShop.Entity;
using HiShop.Dao;
using Microsoft.AspNetCore.Http;
using HiShop.Herramientas;
using Microsoft.AspNetCore.Hosting;
using HiShop.Enum;
using HiShop.Models.Articulo;
using HiShop.Models.Filtros;

namespace HiShop.Controllers
{
    /// <summary>
    /// Controlador que maneja lo relacionado a Articulos
    ///  </summary>
    public class ArticuloController : BaseCoreController
   {
        private IHostingEnvironment _env;
        public ArticuloController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

    ArticuloDao ArticuloDao = new ArticuloDao();


    /*MOSTRAR POR ID*/

    /// <summary>
    /// Maneja la vista que maneja  Lista los artiuctos de un negocio de un usuario registrado
    /// PedroCora
    /// </summary>
    /// <returns></returns>

    public IActionResult ListarArticulo(FiltroArticulo filtro)
    {
            Negocio negocioActivo = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            ArticuloGeneralModelAndView model = new ArticuloGeneralModelAndView();
            model.llenarDatosGenerales(HttpContext, _context);
            model.filtro.llenar(filtro.nombreFiltro, filtro.total, filtro.paginaActual);
            List<Articulo> ListarArticulo = ArticuloDao.ListadoDeArticulos(_context,filtro,negocioActivo);
            model.listaDeArticulos = ListarArticulo;
            model.filtro = filtro;
            return View("ListarArticulo", model);
    }


    /*CREAR*/

    public IActionResult CrearArticulo()
    {
        ArticuloModel model = new ArticuloModel();
        model.llenarDatosGenerales(HttpContext, _context);
        return View(model);
    }


    [HttpPost]
    public IActionResult CrearArticulo(ArticuloModel model)
    {

        if (!ModelState.IsValid)
        {
            agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
            TempData["Mensajes"] = mensajes;
            ViewData["Nombre"] = model.Nombre;
            ViewData["Descripcion"] = model.Descripcion;
            ViewData["Precio"] = model.Precio;
            ViewData["Cantidad"] = model.Cantidad;
            return View(model);

        }
        else
        {
                Articulo ArticuloNuevo = new Articulo
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    Precio = Convert.ToInt32(model.Precio),
                    Cantidad = Convert.ToInt32(model.Cantidad),
                    Negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession"),
                    NegocioID = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession").ID
                };
                string urlImagen = "/images/sinFotoDePerfil.jpg";
                if (model.file != null)
                {
                    string name = model.file.FileName;
                    if (name == "pintureria1.jpg")
                    {
                        urlImagen = "https://image.ibb.co/geFBdo/pintureria1.jpg";
                    }
                    else if (name == "pintureria2.jpg") {
                        urlImagen = "https://image.ibb.co/hyxPyo/pitureria2.jpg";
                    }else if (name == "pintureria3.jpg")
                    {
                        urlImagen = "https://image.ibb.co/njDKW8/pitureria3.jpg";
                    }
                    else if (name == "pintureria4.jpg")
                    {
                        urlImagen = "https://image.ibb.co/bxrjyo/pitureria4.jpg";
                    }
                    else if (name == "pintureria5.jpg")
                    {
                        urlImagen = "https://image.ibb.co/dtMTPT/pitureria5.jpg";
                    }
                    else if (name == "pintureria6.jpg")
                    {
                        urlImagen = "https://image.ibb.co/j4VzW8/pitureria6.jpg";
                    }
                    if (name == "zapato1.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/nfRTPT/zapato1.jpg";
                    }
                    else if (name == "zapato2.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/hwi8PT/zapato2.jpg";
                    }
                    else if (name == "zapato3.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/g2UhjT/zapato3.jpg";
                    }
                    else if (name == "zapato4.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/c5pRB8/zapato4.jpg";
                    }
                    else if (name == "zapato5.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/itp4yo/zapato5.jpg";
                    }
                    else if (name == "zapato6.jpg")
                    {
                        urlImagen = "https://preview.ibb.co/nGw6B8/zapato6.jpg";
                    }
                    if (name == "cafe1.jpg")
                    {
                        urlImagen = "https://image.ibb.co/h1pCJo/cafe1.jpg";
                    }
                    else if (name == "cafe2.jpg")
                    {
                        urlImagen = "https://image.ibb.co/gESor8/cafe2.jpg";
                    }
                    else if (name == "cafe3.jpg")
                    {
                        urlImagen = "https://image.ibb.co/dGCMB8/cafe3.jpg";
                    }
                    else if (name == "cafe4.jpg")
                    {
                        urlImagen = "https://image.ibb.co/dBtuW8/cafe4.jpg";
                    }
                    else if (name == "cafe5.jpg")
                    {
                        urlImagen = "https://image.ibb.co/h386do/cafe5.jpg";
                    }
                    else if (name == "cafe7.jpg")
                    {
                        urlImagen = "https://image.ibb.co/iyv8r8/cafe7.jpg";
                    }
                    else {
                         urlImagen = ArticuloDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, ArticuloNuevo);
                    }
                }
                ArticuloNuevo.UrlImagen = urlImagen;
            ArticuloDao.grabarArticulo(_context, ArticuloNuevo);
            return RedirectToAction("ListarArticulo", "Articulo", ArticuloNuevo);
        }

    }


    /*EDITAR*/

    [ActionName("EditarArticulo")]
    public IActionResult EditarArticulo(int? id)
    {
       
        int idProducto = Convert.ToInt32(id);
        ArticuloDao articuloDao = new ArticuloDao();
        Articulo Articulo = ArticuloDao.get(_context, idProducto);
        ArticuloModel model = new ArticuloModel(HttpContext,_context, Articulo);
        model.llenarDatosGenerales(HttpContext, _context);
        return View(model);
    }

    [HttpPost]
    public IActionResult EditarArticulo(EditarArticuloModelAndView model)
    {
        Articulo articuloEditar = ArticuloDao.get(_context, model.id);
        articuloEditar.Nombre = model.Nombre;
        articuloEditar.Descripcion = model.Descripcion;
        articuloEditar.Precio = model.Precio;
        articuloEditar.Cantidad = model.Cantidad;
        if (model.file != null){
                articuloEditar.UrlImagen = ArticuloDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, articuloEditar);
        }
        ArticuloDao.editarArticulo(_context, articuloEditar);
        return RedirectToAction("ListarArticulo", "Articulo");
    }




    [HttpPost]
    public void BorrarArticulo(int id)
    {
        int idArticulo = Convert.ToInt32(id);
        Articulo prod = ArticuloDao.get(_context, idArticulo);
        ArticuloDao.borrarArticulo(_context, prod);
    }



}
}
