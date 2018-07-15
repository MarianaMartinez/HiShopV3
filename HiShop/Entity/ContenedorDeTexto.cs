using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class ContenedorDeTexto : Elemento
    {
        public  string Texto{ get; set; }
        public  string Alto { get; set; }
        public  string Ancho { get; set; }
        public  string X { get; set; }
        public  string Y { get; set; }
    }
}
