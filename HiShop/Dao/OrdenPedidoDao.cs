using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class OrdenPedidoDao
    {
        //Guarda un Negocio en la Base de Datos: 
        public static void grabar(HiShopContext context, OrdenPedido orden)
        {
            context.OrdenPedidos.Add(orden);
            context.SaveChanges();
        }



        public static List<OrdenPedido> getListado(HiShopContext _context,  Usuario usuario)
        {
            var lista = _context.OrdenPedidos.ToList();
            List<OrdenPedido> negocios = lista.ToList();
            return lista;

        }

        public static List<OrdenPedido> getListadoPorUsuario(HiShopContext _context, Usuario usuario)
        {
            var lista = _context.OrdenPedidos.ToList();
            var listaNueva = new List<OrdenPedido>(); 
            foreach(var item in lista)
            {
                if(item.Usuario != usuario)
                {
                    listaNueva.Add(item);
                }
            }

            return listaNueva;

        }

        public static List<OrdenPedido> getListadoPorUsuarioCompras(HiShopContext _context, Usuario usuario)
        {
            var lista = _context.OrdenPedidos.ToList();
            var listaNueva = new List<OrdenPedido>();
            foreach (var item in lista)
            {
                if (item.Usuario == usuario)
                {
                    listaNueva.Add(item);
                }
            }

            return listaNueva;

        }

        public static List<OrdenPedido> getListadoCompras(HiShopContext _context, Usuario usuario)
        {
            var lista = _context.OrdenPedidos.Where(O => O.Usuario.ID == usuario.ID).ToList();
            List<OrdenPedido> negocios = lista.ToList();
            return lista;

        }

        public static OrdenPedido GetOrdenPedido(HiShopContext _context, int id)
        {
            OrdenPedido ordenPedido = null;
            try
            {
                ordenPedido = _context.OrdenPedidos.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("ERROR.");
            }
            return ordenPedido;
        } 

        public static void Editar(HiShopContext context, OrdenPedido ordenPedido)
        {
            try
            {
                context.OrdenPedidos.Update(ordenPedido);
                context.SaveChanges();
            }
            catch
            {
                throw new InvalidDataException("ERROR");
            }
        }
    }
}
