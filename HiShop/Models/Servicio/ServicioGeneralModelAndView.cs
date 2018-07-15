using HiShop.Models.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Servicio
{
    public class ServicioGeneralModelAndView
    {
        public FiltrosServicio filtro { get; set; }
        public List<Entity.Servicio> listaDeServicios { get; set; }
        public ServicioGeneralModelAndView()
        {
            filtro = new FiltrosServicio();
            listaDeServicios = new List<Entity.Servicio>();
        }
    }
}
