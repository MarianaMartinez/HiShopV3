using HiShop.Entity;
using HiShop.Models.Base;
using HiShop.Models.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Http;

namespace HiShop.Models.Articulo
{

    public class ArticuloGeneralModelAndView : ModelBase
    {
        public FiltroArticulo filtro { get; set; }
        public List<Entity.Articulo> listaDeArticulos { get; set; }
        public ArticuloGeneralModelAndView()
        {
            filtro = new FiltroArticulo();
            listaDeArticulos = new List<Entity.Articulo>();
        }

        public ArticuloGeneralModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }
    }
}
