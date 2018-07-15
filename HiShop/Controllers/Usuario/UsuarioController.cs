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

namespace HiShop.Controllers
{
    public class UsuarioController : BaseCoreController
    {
        private IHostingEnvironment _env;
        public UsuarioController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

        /// <summary>
        /// Te dirije al la vista de perfil de usuario
        /// AxelMolaro
        /// </summary>
        /// <returns></returns>
        public IActionResult Perfil()
        {
            UsuarioModelAndView model = new UsuarioModelAndView();
            Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
            model.llenarValoresEnBaseAUnUsuario(usuario);
            model.llenarDatosGenerales(HttpContext, _context);


            return View("Perfil", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditarDatosGenerales(UsuarioDatosGeneralesModelAndView model)
        {
            try
            {
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                if (!ModelState.IsValid)
                {
                    agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
                    TempData["Mensajes"] = mensajes;
                    UsuarioModelAndView model2 = new UsuarioModelAndView();
                    model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                    model2.nombre = model.nombre;
                    model2.apellido = model.apellido;
                    model2.mail = model.mail;
                 
                    model2.losDatosSonValidos = "No";
                    return View("Perfil", model2); 
                }
                
                if (!model.mail.Equals(usuarioEnSession.Mail))
                {
                    Usuario otro = UsuarioDao.getUsuarioPorMail(_context, model.mail);
                    if (otro != null)
                    {
                        agregarMensajePrincipal("Mail en uso .", TipoMensaje.ERROR);
                        TempData["Mensajes"] = mensajes;
                        UsuarioModelAndView model2 = new UsuarioModelAndView();
                        model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                        model2.nombre = model.nombre;
                        model2.apellido = model.apellido;
                        model2.mail = model.mail;

                        model2.losDatosSonValidos = "No";
                        return View("Perfil", model2);
                    }
                }

                Usuario usuarioAEditar = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                usuarioAEditar.Nombre = model.nombre;
                usuarioAEditar.Apellido = model.apellido;
                usuarioAEditar.Mail = model.mail;
                /*Imagen*/
                //ruta de la carpeta

                string urlImagen = UsuarioDao.guardarUnaImagenEnUnCarpetaDelServidor(model.file, _env, usuarioAEditar);
                usuarioAEditar.UrlImagen = urlImagen;
                UsuarioDao.editarUsuario(_context, usuarioAEditar);

                agregarMensajePrincipal("Los datos de han modificado correctamente .", TipoMensaje.EXITO);
                TempData["Mensajes"] = mensajes;

                UsuarioModelAndView model3 = new UsuarioModelAndView();
                HttpContext.Session.Remove("usuarioEnSession");
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usuarioAEditar);
                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                model3.llenarValoresEnBaseAUnUsuario(usuario);

                return View("Perfil", model3);

            }
            catch(Exception e)
            {


                agregarMensajePrincipal("Ocurrio un error al procesar su solicitud  .", TipoMensaje.ERROR);
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                TempData["Mensajes"] = mensajes;
                UsuarioModelAndView model3 = new UsuarioModelAndView();
                model3.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                model3.nombre = model.nombre;
                model3.apellido = model.apellido;
                model3.mail = model.mail;
                return View("Perfil", model3);
            }
        }

        /// <summary>
        /// IAction result que maneja las peticiones de cambio de contraseña
        /// AxelMolaro
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CambioDeContraseña(UsuarioCambioDEContraeñaModelAndView model)
        {
            try
            {
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                if (!ModelState.IsValid)
                {
                    agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
                    TempData["Mensajes"] = mensajes;
                    UsuarioModelAndView model2 = new UsuarioModelAndView();
                    model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                    model2.formularioActivo = "formularioCambioDeContraseña";
                    model2.contraseña = model.contraseña;
                    model2.contraseña2 = model.contraseña2;
                    model2.contraseña3 = model.contraseña3;

                    return View("Perfil", model2);
                }
                if (usuarioEnSession.Contraseña != model.contraseña)
                {
                    agregarMensajePrincipal("La contraseña actual es incorrecta .", TipoMensaje.ERROR);
                    TempData["Mensajes"] = mensajes;
                    UsuarioModelAndView model2 = new UsuarioModelAndView();
                    model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                    model2.formularioActivo = "formularioCambioDeContraseña";
                    model2.contraseña = model.contraseña;
                    model2.contraseña2 = model.contraseña2;
                    model2.contraseña3 = model.contraseña3;
                    return View("Perfil", model2);
                }
                Usuario usuarioAEditar = usuarioEnSession;
                usuarioAEditar.Contraseña = model.contraseña2;
                UsuarioDao.editarUsuario(_context, usuarioAEditar);

                agregarMensajePrincipal("La contraseña se a modificado correctamente .", TipoMensaje.EXITO);
                TempData["Mensajes"] = mensajes;

                UsuarioModelAndView model3 = new UsuarioModelAndView();
                HttpContext.Session.Remove("usuarioEnSession");
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usuarioAEditar);
                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                model3.llenarValoresEnBaseAUnUsuario(usuario);
                return View("Perfil", model3);
            }
            catch
            {

                agregarMensajePrincipal("Ocurrio un error al procesar su solicitud  .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                return RedirectToAction("Perfil");
            }
        }


        public IActionResult CerrarSesion(UsuarioCambioDEContraeñaModelAndView model)
        {
            HttpContext.Session.Remove("usuarioEnSession");
            HttpContext.Session.Remove("accessToken");
            return Redirect("/Inicio/Inicio");
        }

        public IActionResult CerrarCuenta(UsuarioCerrarCuentaModelAndView model)
        {
            try
            {
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                if (!ModelState.IsValid)
                {
                    agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
                    TempData["Mensajes"] = mensajes;
                    UsuarioModelAndView model2 = new UsuarioModelAndView();
                    model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                    model2.formularioActivo = "formularioCerrarCuenta";
                    model2.contraseña = model.contraseña;
                    return View("Perfil", model2);
                }
                Usuario usuarioAEditar = usuarioEnSession;
                usuarioAEditar.Estado = Estado.INHABILITADO;
                UsuarioDao.editarUsuario(_context, usuarioAEditar);

                agregarMensajePrincipal("La cuenta ah sido cerrada Correctamente.", TipoMensaje.EXITO);
                TempData["Mensajes"] = mensajes;

                UsuarioModelAndView model3 = new UsuarioModelAndView();
                HttpContext.Session.Remove("usuarioEnSession");
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usuarioAEditar);
                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                model3.llenarValoresEnBaseAUnUsuario(usuario);
                return View("Perfil", model3);
            }
            catch {
                agregarMensajePrincipal("Ocurrio un error al procesar su solicitud  .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                return RedirectToAction("Perfil");
            }
        }

        public IActionResult HabilitarCuenta(UsuarioCerrarCuentaModelAndView model)
        {
            try
            {
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                if (!ModelState.IsValid)
                {
                    agregarMensajePrincipal("Verifique los datos ingresados .", TipoMensaje.ERROR);
                    TempData["Mensajes"] = mensajes;
                    UsuarioModelAndView model2 = new UsuarioModelAndView();
                    model2.llenarValoresEnBaseAUnUsuario(usuarioEnSession);
                    model2.formularioActivo = "formularioHabilitarCuenta";
                    model2.contraseña = model.contraseña;
                    return View("Perfil", model2);
                }
                Usuario usuarioAEditar = usuarioEnSession;
                usuarioAEditar.Estado = Estado.APROBADO;
                UsuarioDao.editarUsuario(_context, usuarioAEditar);

                agregarMensajePrincipal("La cuenta ah sido habilitada Correctamente.", TipoMensaje.EXITO);
                TempData["Mensajes"] = mensajes;

                UsuarioModelAndView model3 = new UsuarioModelAndView();
                HttpContext.Session.Remove("usuarioEnSession");
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usuarioAEditar);
                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                model3.llenarValoresEnBaseAUnUsuario(usuario);
                return View("Perfil", model3);
            }
            catch
            {
                agregarMensajePrincipal("Ocurrio un error al procesar su solicitud  .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                return RedirectToAction("Perfil");
            }

        }

        [HttpPost]
        public IActionResult BorrarImagenDePerfil(UsuarioModelAndView model)
        {
            try
            {
                Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                UsuarioDao.BorrarImagen(_context, usuarioEnSession);
                agregarMensajePrincipal("La foto ah sido borrada correctamente .", TipoMensaje.EXITO);
                TempData["Mensajes"] = mensajes;
                HttpContext.Session.Remove("usuarioEnSession");
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usuarioEnSession);
                Usuario usuario = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
                model.urlImagen = "/images/sinFotoDePerfil.jpg";
                model.formularioActivo = "formularioDatosGenerales";
                return View("Perfil", model);
            }
            catch
            {
                agregarMensajePrincipal("Ocurrio un error al borrar la imagen  .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                return RedirectToAction("Perfil");
            }
          
        }
    }
}