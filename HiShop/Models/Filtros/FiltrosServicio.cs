using HiShop.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Filtros
{
    public class FiltrosServicio :Paginacion<Entity.Servicio>
    {
        public string nombreFiltro { get; set; }
        public void llenar(string nombre, int total, int paginaActual)
        {
            this.nombreFiltro = nombre;
            llenarPaginar(total, paginaActual);
        }
    }
}
