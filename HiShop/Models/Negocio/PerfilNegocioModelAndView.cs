using HiShop.Entity.Data;
using HiShop.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Herramientas;

namespace HiShop.Models.Negocio
{
    public class PerfilNegocioModelAndView : ModelBase
    {


        public PerfilNegocioModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }

        
    }
}
