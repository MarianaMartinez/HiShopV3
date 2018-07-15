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
    /// Trae el listado entero de categorias
    /// AxelMolaro
    /// </summary>
    public  class CategoriaDao 
    {
        public static async Task<List<Categoria>> getListado(HiShopContext _context)
        {
            return await _context.Categorias.ToListAsync();
        }

        public static async Task<List<Categoria>> getCategoriasPorPadre(HiShopContext _context, Categoria categoriaPadre)
        {
            var query = (from cat in _context.Categorias
                         where cat.Padre == null
                         select cat);
            if (categoriaPadre != null)
            {
                query = (from cat in _context.Categorias
                         where cat.Padre.ID == categoriaPadre.ID
                         select cat);

            }
            List<Categoria> lista;
            lista = await query.ToListAsync();
             return lista;
        }


        public static async Task<Categoria> getCategoriaPorPadreYNombre(HiShopContext _context, Categoria categoriaPadre,string nombre)
        {
            var query = (from cat in _context.Categorias
                     where cat.Nombre == nombre
                     select cat).FirstAsync();

            if (categoriaPadre != null && !String.IsNullOrEmpty(nombre))
            {
                query = (from cat in _context.Categorias
                         where cat.Padre.ID == categoriaPadre.ID && cat.Nombre == nombre
                         select cat).FirstAsync();

            }

            if (categoriaPadre != null && String.IsNullOrEmpty(nombre))
            {
                query = (from cat in _context.Categorias
                         where cat.Padre.ID == categoriaPadre.ID 
                         select cat).FirstAsync();
            }

           Categoria categoria = await query;
            return categoria;
        }

        /// <summary>
        /// Optiene la ultima categoria
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static  Categoria getUltimaCategoriaAgregada(HiShopContext _context)
        {
            var categoria = _context.Categorias.LastAsync().Result;
            return categoria;
        }

        public static  async Task grabarCategoria(HiShopContext _context, Categoria categoria)
        {
                if(String.IsNullOrEmpty(categoria.Nombre))
                
                {
                    throw new InvalidDataException("No se puede guardar una categoria sin nombre .");
                }
                else if(existeCategoriaConEsePadre(_context, categoria)) {
                    throw new InvalidDataException("Ya existe una categoria con ese nombre y padre .");

                }
                else
                {
                    _context.Categorias.Add(categoria);
                    await _context.SaveChangesAsync();
                }
           
        }

        /// <summary>
        /// verifica si existe una categoria con un padre pasado , se le pasa la categoria a tratar
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public static  bool existeCategoriaConEsePadre (HiShopContext _context, Categoria categoria)
        {
            List<Categoria> lista = getCategoriasPorPadre(_context, categoria.Padre).Result;
            foreach(var c in lista)
            {
                if (c.Nombre == categoria.Nombre)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// /// verifica si existe mas de una  categoria con un padre pasado , se le pasa la categoria a tratar
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public static bool existeCategoriaConEsePadreMasDeUnaVez(HiShopContext _context, Categoria categoria)
        {
            List<Categoria> lista = getCategoriasPorPadre(_context, categoria.Padre).Result;
            var i = 0;
            foreach (var c in lista)
            {

                if (c.Nombre == categoria.Nombre)
                    i++;
            }
            if(i > 1)
            {
                return true;
            }
            return false;


        }

        /// <summary>
        ///Optiene una categoria por id
        ///AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Categoria> getCategoria(HiShopContext _context, int id)
        {
            var categoria = await _context.Categorias.SingleOrDefaultAsync(o => o.ID == id);

            return categoria;
        }

      
        /// <summary>
        /// Edita una categoria
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public static async Task editarCategoria(HiShopContext _context, Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            
        }

     /// <summary>
     /// Elimina una categoria de la base de datos
     /// AxelMolaro
     /// </summary>
     /// <param name="_context"></param>
     /// <param name="categoria"></param>
     /// <returns></returns>
        public static async Task eliminarCategoria (HiShopContext _context, Categoria categoria) 
        {
            List<Categoria> lista = getCategoriasPorPadre(_context, categoria).Result;
             if (lista.Count > 0)
            {
                throw new InvalidDataException("No se puede eliminar una categoria que tiene hijas .");
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
