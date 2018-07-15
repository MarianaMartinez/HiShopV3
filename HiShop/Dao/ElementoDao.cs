using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public static class ElementoDao
    {


       
            ////Guarda un Elemento en la Base de Datos: 
            //public static void grabar(HiShopContext context, Elemento elemento)
            //{
            //    context.Elementos.Add(elemento);
            //    context.SaveChanges();
            //}

            ///// <summary>
            ///// Trae un elemento de la base de datos
            ///// </summary>
            ///// <param name="context"></param>
            ///// <param name="id"></param>
            ///// <returns></returns>
            //public static Elemento get(HiShopContext context, int id)
            //{
            //    Elemento elemento = null;
            //    try
            //    {
            //        elemento = context.Elementos.Single(m => m.ID == id);
            //    }
            //    catch
            //    {
            //        throw new InvalidDataException("Ocurrio un error al buscar el elemento, intentelo nuevamente.");
            //    }
            //    return elemento;
            //}

            ///// <summary>
            ///// Edita un elemento
            ///// </summary>
            ///// <param name="context"></param>
            ///// <param name="elemento"></param>
            //public static void editar(HiShopContext context, Elemento elemento)
            //{
            //    try
            //    {
            //        context.Elementos.Update(elemento);
            //        context.SaveChanges();
            //    }
            //    catch
            //    {
            //        throw new InvalidDataException("Ocurrio un error al editar el elemento .");
            //    }
            //}


            //public static List<Elemento> getListado(HiShopContext _context)
            //{ 
            //    return _context.Elementos.ToList();

            //}

            //public static List<Elemento> getListado(HiShopContext _context, Negocio negocio)
            //{
            //    var lista = _context.Elementos.ToList();
            //    lista = lista.Where(o => o.ID == negocio.ShowroomID).ToList();
            //    List<Elemento> elementos = lista.ToList();
            //    return elementos;

            //}

            ///// <summary>
            ///// Borra una imagen de Negocio, y borra su ruta de la base de datos
            ///// AxelMolaro
            ///// </summary>
            ///// <param name="usuario"></param>
            //public static  void BorrarElemento(HiShopContext _context, int id)
            //{
            //    Elemento elemento = get(_context, id);
            //    _context.Elementos.Remove(elemento);
            //   _context.SaveChanges();
            //}


    }
}
