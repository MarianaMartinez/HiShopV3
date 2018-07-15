using HiShop.Entity;
using HiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Http;

namespace HiShop.Models.Showroom
{
    public class ShowroomVerModelAndView : ModelBase
    {
        public ShowroomVerModelAndView()
        {
        }

        public ShowroomVerModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }

        public Entity.Showroom Showroom { get; set; }
        public Banner Banner { get; set; }
        public Cuerpo Cuerpo { get; set; }

    }
}
