using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class Elemento
    {
        public int ID { get; set; }
        public string Html { get; set; }
        public string UrlLogo { get; set; }
        public string Titulo { get; set; }

        //  public int ShowroomID { get; set; }
        //  public Showroom Showroom { get; set; }
    }
}
