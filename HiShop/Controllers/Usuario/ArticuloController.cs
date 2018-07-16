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
                     urlImagen = ArticuloDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, ArticuloNuevo);
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
