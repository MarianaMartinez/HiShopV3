using HiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Http;

namespace HiShop.Models.Catalogo
{
    public class VentasModelAndView : ModelBase
    {
        public VentasModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }

        public List<Entity.OrdenPedido> ordenes { get; set; }
    }
}
