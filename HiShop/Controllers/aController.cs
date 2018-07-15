using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop2.Manager;
using Microsoft.EntityFrameworkCore;
using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Dao;
using HiShop.Enum;
using HiShop.Models.Base;

namespace HiShop.Controllers
{
    public class AController : BaseCoreController
    {
        public AController(HiShopContext context) : base(context)
        {

        }
        public IActionResult prueba()
        {

            ModelBase model = new ModelBase();
            model.llenarDatosGenerales(HttpContext, _context);
            return View();
        }

    }
}