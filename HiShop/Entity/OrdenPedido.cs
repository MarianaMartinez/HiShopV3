using HiShop.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class OrdenPedido
    {
        public int ID { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public Pago Pago { get; set; }
        public Envio Envio { get; set; }
        public decimal Total { get; set; }
        public int NegocioID { get; set; }

        public Negocio Negocio { get; set; }
        public Usuario Usuario { get; set; }

        public Producto Producto { get; set; } 

        public int ProductoID { get; set; }
    }
}
