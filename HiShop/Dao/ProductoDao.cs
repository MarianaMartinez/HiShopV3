using HiShop.Entity;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    /// <summary>
    /// Esta clase contiene todas las operaciones relacionadas con Producto
    /// PedroCora
    /// </summary>
    public class ProductoDAo
    {

        /// <summary>
        /// Listado de Productos  
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        public static List<Producto> ListadoDeProductos(HiShopContext _context)
        {
            return _context.Productos.ToList();
        }

        public static List<Producto> ListadoDeProductosFiltro(HiShopContext _context, int id)
        {
            var listaDeProductos = _context.Productos.ToList();
            var nuevaLista = new List<Producto>();
            foreach (var item in listaDeProductos)
            {
                if (item.Negocio.UsuarioID != id)
                {
                    nuevaLista.Add(item);
                }
            }

            return nuevaLista;
        }

        public static List<Producto> getListado(HiShopContext _context, Negocio negocio)
        {
            var lista = _context.Productos.ToList();
            lista = lista.Where(o => o.NegocioID == negocio.ID).ToList();
            List<Producto> productos = lista.ToList();
            // return filtro.paginar(articulos, filtro.paginaActual);
            return productos;

        }

        public static Producto get(HiShopContext context, int id)
        {
            Producto prodcuto = null;
            try
            {
                prodcuto = context.Productos.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al buscar el articulo .");
            }
            return prodcuto;
        }

        public static bool EsArticulo(HiShopContext context, int id)
        {
            Producto producto = get(context, id);

            ArticuloDao articuloDao = new ArticuloDao();

            List<Articulo> articulos = articuloDao.ListadoDeArticulos(context);

            foreach (var a in articulos)
            {
                if (producto.ID == a.ID)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
