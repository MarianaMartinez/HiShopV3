using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HiShop.Controllers
{
    /// <summary>
    /// Controlador que maneja las vistas basicas de usuarios que se registraron
    /// </summary>
    public class InicioLogueadoController : Controller
    {
        /// <summary>
        /// Es la vista posterior a registrarse
        /// </summary>
        /// <returns></returns>
        public IActionResult Inicio()
        {
            return View();
        }
    }
}