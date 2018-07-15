using System.Collections;
using System.Collections.Generic;

namespace HiShop.Entity
{
    public class Localidad
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int ProvinciaID { get; set; }
        public Provincia Provincia { get; set; }
        public List<Negocio> Negocios { get; set; }
    }
}
