using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class PedidoDao
    {
        //Crea un orden en la Base de Datos
        public void GenerarPedido(HiShopContext context, OrdenPedido pedido)
        {
            context.OrdenPedidos.Add(pedido);
            context.SaveChanges();
        }


        //Buscar un orden en la Base de Datos
        public static OrdenPedido ObtenerOrdenPorID(HiShopContext context, int? id)
        {
            if (id == null)
            {
                throw new Exception();
            }

            OrdenPedido pedido = context.OrdenPedidos.Single(m => m.ID == id);
            if (pedido == null)
            {
                throw new Exception();
            }
            return pedido;
        }

        ////Listar  las ordenes pendientes en la Base de Datos
        //public List<OrdenPedido> ListarPedidosActivos(HiShopContext context)
        //{
        //    List<OrdenPedido> PedidosActivos = new List<OrdenPedido>();
        //    foreach (var n in context.OrdenPedidos.ToList())
        //    {
        //        if (n.EstadoPedido == EstadoPedido.PENDIENTE)
        //        {
        //            OrdenPedido.Add(n);
        //        }
        //    }
        //    return PedidosActivos;
        //}

        ////Listar  las ordenes concretadas en la Base de Datos
        //public List<OrdenPedido> ListarPedidosFinalizadas(HiShopContext context)
        //{
        //    List<OrdenPedido> PedidosFin = new List<OrdenPedido>();
        //    foreach (var n in context.OrdenPedidos.ToList())
        //    {
        //        if (n.EstadoPedido == EstadoPedido.CONCRETADO)
        //        {
        //            OrdenPedido.Add(n);
        //        }
        //    }
        //    return PedidosFin;
        //}

        //Lista toas las ordenes en la Base de Datos
        public static List<OrdenPedido> ListarTodos(HiShopContext context)
        {
            return context.OrdenPedidos.ToList();
        }
        

    }
}