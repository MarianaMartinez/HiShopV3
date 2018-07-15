using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public abstract class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; }

        public int NegocioID { get; set; }
        public Negocio Negocio { get; set; }

    }
}
