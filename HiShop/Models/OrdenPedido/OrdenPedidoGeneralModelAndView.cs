using HiShop.Entity;
using HiShop.Enum;
using HiShop.Models.ShowroomsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.OrdenPedido
{
    public class OrdenPedidoGeneralModelAndView :ShowroomGeneralModelAndView
    {
        public OrdenPedidoGeneralModelAndView(Entity.Negocio negocio) : base(negocio)
        {

        }

        public int id { get; set; }

        public string UsuarioFK { get; set; }

        public string NegocioFK { get; set; }

        public int  cantidad { get; set; }
        public int precio { get; set; }
        public int Total { get; set; }

        public Producto Producto { get; set; }
        public int productoId { get; set; }
        public Estado estadopedido { get; set; }
        public Pago pago { get; set; }
        public Envio envio { get; set; }

        public List<Estado> estados { get; set; }
        public List<Pago> pagos { get; set; }
        public List<Envio> envios { get; set; }

    }
}
