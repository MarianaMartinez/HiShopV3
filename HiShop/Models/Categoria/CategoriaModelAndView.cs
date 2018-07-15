using HiShop.Dao;
using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Models.Base;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HiShop.Models
{
    public class CategoriaModelAndView : ModelBase
    {

        public List<Categoria> categorias { get; set; }

        public CategoriaModelAndView(List<Categoria> categoriasLista)
        {
            this.categorias = categoriasLista;
        }

        public CategoriaModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {

        }
    }
}
