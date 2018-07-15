using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop.Models.Base;

namespace HiShop.Controllers.Error
{
    public class ErrorController : Controller
    {
        public IActionResult ErroresEnProcesoDeSolucion()
        {
            ModelBase model = new ModelBase();
            return View(model);
        }
    }
}