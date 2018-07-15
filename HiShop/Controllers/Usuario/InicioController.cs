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
using HiShop.Herramientas;
using HiShop.Models.Inicio;
using HiShop.Models.Base;
/// <summary>
/// Maneja las vistas basica del home sin estar registrado
/// </summary>
public class InicioController : BaseCoreController
{
    public InicioController(HiShopContext context) : base(context)
    {
    }


    /// <summary>
    /// Maneja las vista de /Inicio/inicio
    ///Que seria la home principal del sitemada
    /// Axel-Mariana
    /// </summary>
    /// <returns></returns>
    public IActionResult Inicio()
    {
        InicioModelAndView model = new InicioModelAndView();
        HttpContext.Session.Remove("accessToken");
       var accesToken = HttpContext.Session.GetObjectFromJson<string>("accessToken");
        if (String.IsNullOrEmpty(accesToken))
        {
            model.estaLogueadoConFacebook = false;
        }
        else
        {
            model.estaLogueadoConFacebook = true;
        }
        return View(model);
    }

    /// <summary>
    /// Maneja las operaciones con la vista que contiene la informacion básica del sistema
    /// Pedro
    /// </summary>
    /// <returns></returns>
    public IActionResult Nosotros()
    {
        return View(new RegistroModalAndView());
    }

    /// <summary>
    /// Maneja la vista de consultas del usuarios no registrados
    /// Pedro
    /// </summary>
    /// <returns></returns>
    public IActionResult Contacto()
    {
        return View(new RegistroModalAndView());
    }
    /// <summary>
    /// Maneja la vista de consultas del usuarios no registrados
    /// Pedro
    /// </summary>
    /// <returns></returns>
    public IActionResult Packs()
    {
        return View(new RegistroModalAndView());
    }

    /// <summary>
    /// Maneja el formulario de registro, sie el modelo viene correcto lo crea el usuario y redirige al inicio,
    /// si no no lo crea al usuario y muestra el mensaje de error
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Registro(RegistroModalAndView model)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                model.agregarMensajePrincipal("Datos incorrectos al registrar .", TipoMensaje.ERROR);
                ViewBag.Mensajes = mensajes;
                return View("Inicio", model);
            }
          
            Usuario usuarioNuevo = new Usuario();
            usuarioNuevo.Nombre = model.nombre;
            usuarioNuevo.Apellido = model.apellido;
            usuarioNuevo.Contraseña = model.contraseña;
            usuarioNuevo.Mail = model.contraseña;
            UsuarioDao.grabarUsuario(_context,usuarioNuevo);
            
            agregarMensajePrincipal("El usaurio se ah registrado correctamente .", TipoMensaje.EXITO);
            ViewBag.Mensajes = mensajes;
            return View("Inicio", new RegistroModalAndView());
        }
        catch (Exception e)
        {
            agregarMensajePrincipal("Ah ocurrido un error al registrar el usuario .", TipoMensaje.ERROR);
            ViewBag.Mensajes = mensajes;
            return View("Inicio", model);
        }


    }

    /// <summary>
    /// Este metodo verifica si el mail y la contraseña ingresadas en el formulario de modal coinciden o no 
    /// y dependiendo de ese vuelve al inicio o el home de usuario logueado
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(RegistroModalAndView model)
    {
        try
        {
             if (UsuarioDao.coincideMailYContraseña(_context,model.mail, model.contraseña))
             {
                var negocioActivoId = UsuarioDao.getUsuarioPorMail(_context, model.mail).negocioActivo;
                if (negocioActivoId != 0 )
                {
                    var negocio = NegocioDao.get(_context, negocioActivoId);
                    HttpContext.Session.SetObjectAsJson("negocioEnSession", negocio);
                }
                 return Redirect("/InicioLogueado/Inicio");
             }
             else
             {
                agregarMensajePrincipal("Acceso denegado .", TipoMensaje.ERROR);
                ViewBag.Mensajes = mensajes;
                return RedirectToAction("Inicio", model);
             }

        }
        catch (Exception e)
        {
            agregarMensajePrincipal("Ah ocurrido un error tratar de ingresar al sistema .", TipoMensaje.ERROR);
            ViewBag.Mensajes = mensajes;
            ViewBag.Mensaje = "";
            return RedirectToAction("Inicio", model);
        }


    }


    public IActionResult InicioLogueado()
    {
        ModelBase model = new ModelBase(HttpContext,_context);
        Usuario usuarioEnSession = HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
        if (model.usuarioModelBase.negocioActivo != 0)
        {
            model.negocioModelBase = NegocioDao.get(_context, usuarioEnSession.negocioActivo);
            HttpContext.Session.SetObjectAsJson("negocioEnSession", model.negocioModelBase);
        }
        else {

            List<Negocio> lista = NegocioDao.getListadoPorUsuario(_context,usuarioEnSession);
            if (lista.Count != 0) {
                 model.negocioModelBase = lista.First();
                HttpContext.Session.SetObjectAsJson("negocioEnSession", lista.First());
            }
        }
        return View("~/Views/Inicio/InicioLogueado.cshtml",model);
    }


}