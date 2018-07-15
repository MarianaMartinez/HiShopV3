using HiShop.Entity;
using HiShop.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Filtros
{
    /// <summary>
    /// Clase que se usa en el filtro de negocios
    /// AxelMolaro
    /// </summary>
    public class FiltroNegocio : Paginacion<Entity.Negocio>
    {
        public string nombreFiltro { get; set; }
        public int idCategoriaFiltro { get; set; }
        public int estadoFiltro { get; set; }
        public int paginaActualFiltro { get; set; }


        public void llenar(string nombre, int total, int paginaActual)
        {
            this.nombreFiltro = nombre;
            llenarPaginar(total, paginaActual);
        }
    }
}
