using HiShop.Entity.Data;
using HiShop.Enum;
using System.Collections.Generic;

namespace HiShop.Entity
{
    public class Negocio
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
       
        public string UrlImagenNegocio { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Descripcion { get; set; }
        public EstadoNegocio Estado { get; set; }

        public int CategoriaID { get; set; }
        public int LocalidadID { get; set; }
        public int UsuarioID { get; set; }

        public Categoria Categoria { get; set; }
        public Localidad Localidad { get; set; }
        public Usuario Usuario { get; set; }

        public int? ShowroomID { set; get; }
        public virtual Showroom Showroom { set; get; }

        public List<Producto> Productos { get; set; }
        public List<OrdenPedido> OrdenesDePedido { get; set; }


        public bool tieneShowroom()
        {
            var tiene = false;
            if (Showroom != null || ShowroomID != null)
                tiene =  true;
            return tiene;
        }
    }
}
