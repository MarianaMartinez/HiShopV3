using HiShop.Entity;
using HiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Http;

namespace HiShop.Models.Catalogo
{
    public class CatalogoGeneral : ModelBase
    {
        public CatalogoGeneral(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }
        public List<Producto> productos { get; set; }
    }
}
