using System;
using Microsoft.AspNetCore.Mvc;
using HiShop.Controllers;
using HiShop.Entity.Data;
using HiShop.Models;
using HiShop.Enum;
using HiShop.Entity;
using HiShop.Dao;
using Microsoft.AspNetCore.Http;
using HiShop.Herramientas;
using HiShop.Models.Inicio;
using HiShop.Models.Data;
using HiShop.Models.Mail;

/// <summary>
/// Maneja las vistas basica del home sin estar registrado
/// </summary>
public class RegistroController : BaseCoreController
{
    public RegistroController(HiShopContext context) : base(context)
    {
    }

    MailService mailService = new MailService();

    /// <summary>
    /// Este metodo verifica si el mail y la contraseña ingresadas en el formulario de modal coinciden o no 
    /// y dependiendo de ese vuelve al inicio o el home de usuario logueado
    /// Si logue correctamente guarda el usuario en session
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// 
    [HttpPost]
    public DataModel LoginPost(RegistroModalAndView model)
    {
        DataModel dataModel = new DataModel();
        MensajeModel mensaje = new MensajeModel();
        try
        {
            model.limpiarDatosDeRegistro();

            if (UsuarioDao.coincideMailYContraseña(_context, model.mailLogin, model.contraseñaLogin))
            {
                Usuario usaurioParaSession = UsuarioDao.getUsuarioPorMail(_context, model.mailLogin);
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usaurioParaSession);
                dataModel.Redireccion = "/Inicio/InicioLogueado";
                return dataModel;
            }
            else
            {
                mensaje.texto = "Acceso denegado, datos ingresados incorrectos .";
                mensaje.tipo = TipoMensaje.ERROR.ToString();
                dataModel.data.Add("mensaje", mensaje);
                return dataModel;
            }

        }
        catch (Exception)
        {
            mensaje.texto = "Ah ocurrido un error tratar de ingresar al sistema .";
            mensaje.tipo = TipoMensaje.ERROR.ToString();
            ModelState.Clear();
            dataModel.data.Add("mensaje", mensaje);
            return dataModel;
        }
    }

    [HttpPost]
    public IActionResult LoginPostMobile(RegistroModalAndView model)
    {
        try
        {
            InicioModelAndView modelInicio = new InicioModelAndView();
            model.limpiarDatosDeRegistro();

            if (UsuarioDao.coincideMailYContraseña(_context, model.mailLogin, model.contraseñaLogin))
            {
                Usuario usaurioParaSession = UsuarioDao.getUsuarioPorMail(_context, model.mailLogin);
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usaurioParaSession);
                return RedirectToAction("InicioLogueado", "Inicio");
            }
            else
            {
                agregarMensajePrincipal("Acceso denegado, datos ingresados incorrectos .", TipoMensaje.ERROR);
                modelInicio.llenarModeloEnBaseAUnRegistroModelAndView(model);
                TempData["Mensajes"] = mensajes;
                ModelState.Clear();
                return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
            }

        }
        catch (Exception)
        {
            agregarMensajePrincipal("Ah ocurrido un error tratar de ingresar al sistema .", TipoMensaje.ERROR);
            TempData["Mensajes"] = mensajes;
            ModelState.Clear();
            return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
        }
    }

    /// <summary>
    /// Maneja el formulario de registro, sie el modelo viene correcto lo crea el usuario y redirige al inicio,
    /// si no no lo crea al usuario y muestra el mensaje de error
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// 
    [HttpPost]
    public IActionResult RegistroPostAsync(RegistroModalAndView model)
    {
        try
        {
            InicioModelAndView modelInicio = new InicioModelAndView();
            model.limpiarDatosDeLogin();
            if (!ModelState.IsValid)
            {
                agregarMensajePrincipal("Datos incorrectos al registrar .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                modelInicio.llenarModeloEnBaseAUnRegistroModelAndView(model);
                return View("~/Views/Inicio/Inicio.cshtml", modelInicio);
            }
            if (UsuarioDao.getUsuarioPorMail(_context, model.mail) != null)
            {
                agregarMensajePrincipal("El mail que quieres ingresar ya esta en uso, por favor utilice otro .", TipoMensaje.ERROR);
                TempData["Mensajes"] = mensajes;
                modelInicio.llenarModeloEnBaseAUnRegistroModelAndView(model);
                return View("~/Views/Inicio/Inicio.cshtml", modelInicio);
            }
            Usuario usuarioNuevo = new Usuario
            {
                Nombre = model.nombre,
                Apellido = model.apellido,
                Contraseña = model.contraseña,
                Mail = model.mail,
                Estado = Estado.PENDIENDTE //TODOAM: CAMBIAR A PENDIENTE CUANDO HAYA CONFIRMAION X MAIL (LISTO) 
            };
            UsuarioDao.grabarUsuario(_context, usuarioNuevo);
            MailModel _objModelMail = new MailModel();
            mailService.aprobarUsuario(_objModelMail, usuarioNuevo);
            agregarMensajePrincipal("Gracias por registrarte. A la brevedad aprobaremos tu solicitud.", TipoMensaje.EXITO);
            TempData["Mensajes"] = mensajes;
            return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
        }
        catch (Exception e)
        {
            InicioModelAndView modelInicio2 = new InicioModelAndView();
            agregarMensajePrincipal("Ah ocurrido un error al registrar el usuario .", TipoMensaje.ERROR);
            modelInicio2.llenarModeloEnBaseAUnRegistroModelAndView(model);
            return View("~/Views/Inicio/Inicio.cshtml", modelInicio2);
        }
    }

    public IActionResult AprobarUsuario(int id)
    {
        Usuario usuarioNuevo = UsuarioDao.getUsuario(_context, id);
        usuarioNuevo.Nombre = usuarioNuevo.Nombre;
        usuarioNuevo.Apellido = usuarioNuevo.Apellido;
        usuarioNuevo.Contraseña = usuarioNuevo.Contraseña;
        usuarioNuevo.Mail = usuarioNuevo.Mail;
        usuarioNuevo.Estado = Estado.APROBADO;
        UsuarioDao.editarUsuario(_context, usuarioNuevo);
        agregarMensajePrincipal("Usuario aprobado", TipoMensaje.EXITO);
        TempData["Mensajes"] = mensajes;
        MailModel _objModelMail = new MailModel();
        mailService.avisoDeAprobacionAUsuario(_objModelMail, usuarioNuevo);
        return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
    }
}