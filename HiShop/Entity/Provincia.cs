using System.Collections;
using System.Collections.Generic;

namespace HiShop.Entity
{
    public class Provincia
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public List<Localidad> Localidades { get; set; } 
    }
}
