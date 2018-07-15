using Microsoft.AspNetCore.Mvc;
using HiShop.Models;
using HiShop.Herramientas;
using HiShop.Entity;
using HiShop.Enum;
using HiShop.Entity.Data;
using HiShop.Dao;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using HiShop.Models.Data;
using HiShop.Models.Filtros;
using HiShop.Models.Negocio;
using HiShop.Views.Negocio;
using HiShop.Models.Mail;
using HiShop.Models.Inicio;
using HiShop.Models.Base;

namespace HiShop.Controllers
{
    public class NegocioController : BaseCoreController
    {

        private IHostingEnvironment _env;
        public NegocioController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

        MailService mailService = new MailService();

        public IActionResult MisNegocios(FiltroNegocio filtro)
        {
            Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
            List<Entity.Negocio> lista = NegocioDao.getListado(_context, filtro, usuarioEnSession);
            NegocioGeneralModelAndView model = new NegocioGeneralModelAndView(lista, _context, HttpContext);
            model.filtro.llenar(filtro.nombreFiltro, filtro.total, filtro.paginaActual);
            Negocio negocioActivo = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            if (usuarioEnSession.negocioActivo != 0)
            {
                negocioActivo = NegocioDao.get(_context, usuarioEnSession.negocioActivo);
            }
            if (negocioActivo != null)
            {
                model.negocioActivo = negocioActivo;
                model.idNegocioActivo = negocioActivo.ID;
            }
            return View("MisNegocios", model);
        }

        public IActionResult RegistrarNegocio()
        {
            NegocioGeneralModelAndView model = new NegocioGeneralModelAndView(HttpContext, _context);
            try
            {
                model.llenarListados(_context);
            }
            catch (Exception e)
            {
                Console.Write("Ocurio un error al traer el listado de categorias");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarNegocio(NegocioModelAndView model)
        {
            if (!ModelState.IsValid)
            {
                NegocioGeneralModelAndView model2 = new NegocioGeneralModelAndView(HttpContext, _context);
                model2.llenarEnBaseANegocioModel(model, _context);
                model2.urlForm = "/Negocio/RegistrarNegocio";
                return View(model2);
            }
            else
            {
                Negocio negocio = new Negocio
                {
                    Nombre = model.Nombre,
                    //UrlImagenNegocio = model.UrlImagenNegocio,
                    Categoria = CategoriaDao.getCategoria(_context, model.CategoriaFK).Result,
                    Localidad = LocalidadDao.get(_context, model.LocalidadFK),
                    Calle = model.Calle,
                    Numero = model.Numero,
                    Telefono = model.Telefono,
                    Email = model.Email,
                    Descripcion = model.Descripcion,
                    Usuario = UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID),
                    Estado = EstadoNegocio.INHABILITADO, //se crea con estado inhabilitado por el momento.
                    UrlImagenNegocio = "",
                    //Showroom = ShowroomDao.get(_context, model.ShowroomFk)
                };
                negocio.UrlImagenNegocio = NegocioDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, negocio);
                NegocioDao.grabar(_context, negocio);
                MailModel _objModelMail = new MailModel();
                mailService.aprobarNegocio(_objModelMail, negocio);
                TempData["RegistroCorrecto"] = "¡El negocio se ha registrado correctamente!";
                return RedirectToAction("MisNegocios", "Negocio");
                //Una vez que se guarda el Negocio, se lo redirige a la vista del showroom (que hay que diseñarla) 
            }
        }


        public IActionResult Packs()
        {
            ModelBase model = new ModelBase();
            model.llenarDatosGenerales(HttpContext, _context);
            return View();
        }



        //CONFIRMAR POR MAIL 
        public IActionResult AprobarNegocio(int id)
        {
            Negocio negocio = NegocioDao.get(_context, id);
            negocio.Nombre = negocio.Nombre;
            negocio.CategoriaID = negocio.CategoriaID;
            negocio.LocalidadID = negocio.LocalidadID;
            negocio.Calle = negocio.Calle;
            negocio.Numero = negocio.Numero;
            negocio.Telefono = negocio.Telefono;
            negocio.Email = negocio.Email;
            negocio.Descripcion = negocio.Descripcion;
            negocio.UsuarioID = negocio.UsuarioID;
            negocio.Estado = EstadoNegocio.APROBADO;
            negocio.UrlImagenNegocio = negocio.UrlImagenNegocio;
            NegocioDao.editar(_context, negocio);
            agregarMensajePrincipal("Negocio aprobado", TipoMensaje.EXITO);
            TempData["Mensajes"] = mensajes;
            return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
        }

        public IActionResult PefilNegocio()
        {
            try
            {
                PerfilNegocioModelAndView model = new PerfilNegocioModelAndView(HttpContext, _context);
                if (model.negocioModelBase != null)
                {
                    return View(model);
                }
                else
                {
                    agregarMensajePrincipal("Error al seleccionar traer el perfil del negocio, verifique que tenga seleccionado uno .", TipoMensaje.ERROR);
                    return RedirectToAction("InicioLogueado", "Inicio");
                }

            }
            catch (Exception e)
            {
                agregarMensajePrincipal("Error al seleccionar traer el perfil del negocio.", TipoMensaje.ERROR);
                return RedirectToAction("InicioLogueado", "Inicio");
            }
        }

        //MODIFICACIÓN
        [ActionName("EditarNegocio")]
        public IActionResult EditarNegocio(int? id)
        {
            int idNegocio = Convert.ToInt32(id);
            Negocio negocio = NegocioDao.get(_context, idNegocio);
            NegocioGeneralModelAndView model = new NegocioGeneralModelAndView(HttpContext, _context);
            model.llenarEnBaseAUnNegocio(negocio, _context);
            negocio.UrlImagenNegocio = negocio.UrlImagenNegocio;
            model.urlForm = "/Negocio/EditarNegocio";
            return View("RegistrarNegocio", model);
        }

        [HttpPost]
        public IActionResult EditarNegocio(EditarNegocioModelAndView model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    NegocioGeneralModelAndView model2 = new NegocioGeneralModelAndView(HttpContext, _context);
                    model2.llenarEnBaseAEditarNegocioModel(model, _context);
                    model2.urlForm = "/Negocio/EditarNegocio";
                    return View("RegistrarNegocio", model2);
                }
                else
                {
                    Negocio negocio = NegocioDao.get(_context, model.id);
                    negocio.Nombre = model.Nombre;
                    negocio.Categoria = CategoriaDao.getCategoria(_context, model.CategoriaFK).Result;
                    negocio.Localidad = LocalidadDao.get(_context, model.LocalidadFK);
                    negocio.Calle = model.Calle;
                    negocio.Numero = model.Numero;
                    negocio.Telefono = model.Telefono;
                    negocio.Email = model.Email;
                    negocio.Descripcion = model.Descripcion;
                    //negocio.Estado = EstadoNegocio.APROBADO;
                    negocio.Estado = model.estado;
                    //negocio.Showroom = ShowroomDao.get(_context, model.ShowroomFk);
                    if (model.file != null)
                    {
                        negocio.UrlImagenNegocio = NegocioDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, negocio);
                    }
                    NegocioDao.editar(_context, negocio);
                }
            }
            catch
            {
                Console.Write("Error al editar negocio");
            }
            return RedirectToAction("MisNegocios", "Negocio");
        }

        [HttpPost]
        public void CambiarEstado(int id, string estado)
        {
            Negocio negocio = NegocioDao.get(_context, id);
            MensajeModel mensaje = new MensajeModel();
            if (estado.ToLower().Equals("aprobado"))
            {
                negocio.Estado = EstadoNegocio.APROBADO;
            }

            if (estado.ToLower().Equals("inhabilitado"))
            {
                negocio.Estado = EstadoNegocio.INHABILITADO;
            }
            NegocioDao.editar(_context, negocio);
        }

        [HttpPost]
        public DataModel SeleccionarNegocio(int id)
        {
            DataModel respuesta = new DataModel();
            MensajeModel mensaje = new MensajeModel();
            Negocio negocio = NegocioDao.get(_context, id);
            if (negocio.Estado == EstadoNegocio.APROBADO)
            {
                HttpContext.Session.Remove("negocioEnSession");
                HttpContext.Session.SetObjectAsJson("negocioEnSession", negocio);

                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.ID = usuario.ID;
                nuevoUsuario.Nombre = usuario.Nombre;
                nuevoUsuario.Mail = usuario.Mail;
                nuevoUsuario.negocioActivo = usuario.negocioActivo;
                nuevoUsuario.Negocios = new List<Negocio>();
                nuevoUsuario.UrlImagen = usuario.UrlImagen;
                nuevoUsuario.OrdenPedidos = usuario.OrdenPedidos;
                nuevoUsuario.Estado = usuario.Estado;
                nuevoUsuario.Contraseña = usuario.Contraseña;
                nuevoUsuario.Apellido = usuario.Apellido;
                nuevoUsuario.negocioActivo = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession").ID;
                UsuarioDao.editarUsuario(_context, nuevoUsuario);
                HttpContext.Session.SetObjectAsJson2("usuarioEnSession", nuevoUsuario);
            }
            else
            {
                mensaje.texto = "No se puede seleccionar un negocio inhabilitado para operar";
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            respuesta.data.Add("mensaje", mensaje);
            return respuesta;
        }
    }
}