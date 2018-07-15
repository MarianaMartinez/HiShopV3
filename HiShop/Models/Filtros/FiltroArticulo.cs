using HiShop.Entity;
using HiShop.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Filtros
{
    public class FiltroArticulo : Paginacion<Entity.Articulo>
    {
        public string nombreFiltro { get; set; }
        public void llenar(string nombre, int total, int paginaActual)
        {
            this.nombreFiltro = nombre;
            llenarPaginar(total, paginaActual);
        }
    }
}
