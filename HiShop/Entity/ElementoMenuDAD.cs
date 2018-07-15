using HiShop.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class ElementoMenuDAD
    {
        public int ID { get; set; }
        public string Html { get; set; }
        public string Titulo { get; set; }
        public string UrlImagen { get; set; }
        public TipoElementoMenuDAD tipo { get; set; }
    }
}
