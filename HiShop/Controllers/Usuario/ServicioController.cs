using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop.Controllers;
using HiShop.Entity.Data;
using HiShop.Models;
using HiShop.Enum;
using Microsoft.EntityFrameworkCore;
using HiShop.Entity;
using HiShop.Dao;
using Microsoft.AspNetCore.Http;
using HiShop.Herramientas;
using Microsoft.AspNetCore.Hosting;
using HiShop.Models.Servicio;
using HiShop.Models.Filtros;

namespace HiShop.Controllers
{
    /// <summary>
    /// Controlador que maneja lo relacionado a servicios
    /// </summary>

    public class ServicioController : BaseCoreController
    {
        private IHostingEnvironment _env;
        public ServicioController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

        ServicioDao servicioDao = new ServicioDao();


        /*MOSTRAR POR ID*/

        /// <summary>
        /// Maneja la vista que maneja  Lista los servicios de un negocio de un usuario registrado
        /// PedroCora
        /// </summary>
        /// <returns></returns>

        public IActionResult ListarServicio(FiltrosServicio filtro)
        {
            Negocio negocioActivo = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            ServicioGeneralModelAndView model = new ServicioGeneralModelAndView();
            model.filtro.llenar(filtro.nombreFiltro, filtro.total, filtro.paginaActual);
            List<Servicio> ListarServicio = servicioDao.ListadoDeServicios(_context, filtro, negocioActivo);
            model.listaDeServicios = ListarServicio;
            model.filtro = filtro;
            return View("ListarServicio", model);
        }


        /*CREAR*/

        public IActionResult CrearServicio()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CrearServicio(ServicioModel model)
        {
            if (!ModelState.IsValid)
            {

                agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;

                ViewData["Nombre"] = model.Nombre;
                ViewData["Descripcion"] = model.Descripcion;
                return View(model);

            }

            else
            {

                Servicio servicioNuevo = new Servicio
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    Negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession"),
                    NegocioID = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession").ID

                };

                string urlImagen = "/images/sinFotoDePerfil.jpg";
                if (model.file != null)
                {
                     urlImagen = ServicioDao.guardarImagenServicio(model.file, _env, servicioNuevo);
                }
                servicioNuevo.UrlImagen = urlImagen;
                ServicioDao.grabarServicio(_context, servicioNuevo);
                return RedirectToAction("ListarServicio", "Servicio", servicioNuevo);
            }

        }


        /*EDITAR*/

        [ActionName("EditarServicio")]
        public IActionResult EditarServicio(int? id)
        {
            Servicio servicio = servicioDao.obtenerServicioPorID(_context, id);
            return View(servicio);
        }

        [HttpPost]
        public IActionResult EditarServicio(EditarServicioModelAndView model)
        {

            Servicio serv = servicioDao.obtenerServicioPorID(_context, model.ID);
            serv.Nombre = model.Nombre;
            serv.Descripcion = model.Descripcion;
            if (model.file != null)
            {
                serv.UrlImagen = ServicioDao.guardarImagenServicio(model.file, _env, serv);
            }
            servicioDao.editarServicio(_context, serv);

            return RedirectToAction("ListarServicio", "Servicio");
        }


        /*  ELIMINAR Articulo */





        [HttpPost]
        public void BorrarServicio(int id)
        {
            int idAServicio = Convert.ToInt32(id);
            Servicio serv = servicioDao.obtenerServicioPorID(_context, idAServicio);
            servicioDao.borrarServicio(_context, serv);
        }

    }
}
