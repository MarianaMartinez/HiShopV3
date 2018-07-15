using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HiShop2.Manager
{
    public class RepositorioManager
    {

        public HiShop.Entity.Usuario usuarioEntity { get; set; }
        public HiShop.Entity.Data.HiShopContext ctx { get; set; }
        public HiShop.Entity.Articulo articuloEntity { get; set; }
        public HiShop.Entity.Servicio servicioEntity { get; set; }

        public RepositorioManager()
        {

            //es el nombre que le di a la base de datos
            usuarioEntity = new HiShop.Entity.Usuario();
            servicioEntity = new HiShop.Entity.Servicio();
            articuloEntity = new HiShop.Entity.Articulo();

        }


    }

}