using HiShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.ShowroomsModel
{
    public class ShowroomGeneralModelAndView
    {
        public Entity.Negocio Negocio { get; set; }
        public List<Producto> listaDeProductos { get; set; }

        public ShowroomGeneralModelAndView(Entity.Negocio negocio)
        {
            this.Negocio = negocio;
        }

    }
}
