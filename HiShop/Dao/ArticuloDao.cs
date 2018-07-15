using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Models.Filtros;
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
    /// Esta clase contiene todas las operaciones relacionadas con Articulo
    /// PedroCora
    /// </summary>
    public class ArticuloDao
    {

        /// <summary>
        /// Guarda un Articulo en la base de datos , creacion
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="Articulo"></param>
        public static void grabarArticulo(HiShopContext _context, Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Added;
            _context.Add(articulo);
            _context.Update(articulo.Negocio);
            _context.SaveChanges();
        }


        /// <summary>
        /// Tomar un Articulo de la base de datos por id
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="id"></param>
        public static Articulo get(HiShopContext context, int id)
        {
            Articulo artiiculo = null;
            try
            {
                artiiculo = context.Articulos.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al buscar el articulo .");
            }
            return artiiculo;
        }


        /// <summary>
        /// Edicion de Articulo en la base de datos 
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="Articulo"></param>
        public void editarArticulo(HiShopContext _context, Articulo Articulo)
        {
            Articulo arti = get(_context, Articulo.ID);
            if (arti == null)
            {
                throw new Exception();
            }

            _context.Articulos.Update(Articulo);
            _context.SaveChanges();
        }

        /// <summary>
        /// Listado de Articulo  
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        public List<Articulo> ListadoDeArticulos(HiShopContext _context)
        {
            return _context.Articulos.ToList();
        }

        public List<Articulo> ListadoDeArticulos(HiShopContext _context,FiltroArticulo filtro , Negocio negocio)
        {
            var lista = _context.Articulos.ToList();
            if (!String.IsNullOrEmpty(filtro.nombreFiltro))
            {
                lista = lista.ToList().Where(o => o.Nombre.ToLower().Contains(filtro.nombreFiltro.ToLower())).ToList();

            }

            lista = lista.Where(o => o.NegocioID == negocio.ID).ToList();
            List<Articulo> articulos = lista.ToList();
            return filtro.paginar(articulos, filtro.paginaActual);


        }


        public void borrarArticulo(HiShopContext _context, Articulo Articulo)
        {
            Articulo arti = get(_context, Articulo.ID);
            if (arti == null)
            {
                throw new Exception();
            }
            _context.Entry(arti).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        /* IMAGEN */

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static string guardarUnaImagenEnUnCarpetaDelServidor(IFormFile file, IHostingEnvironment _env, Articulo articulo)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (file == null)
            {
                return articulo.UrlImagen;
            }
            String ruta = _env.WebRootPath + "\\ImagenesServidor\\ImagenesArticulos";
            if (!System.IO.Directory.Exists(ruta + "\\" + articulo.ID.ToString()))
            {
                System.IO.Directory.CreateDirectory(ruta + "\\" + articulo.ID.ToString());
            }

            ruta = ruta + "\\" + articulo.ID.ToString();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var filename = ContentDispositionHeaderValue
                       .Parse(file.ContentDisposition)
                       .FileName
                       .Trim('"');
                filename = ruta + $@"\{filename}";
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    if (articulo.UrlImagen != null)
                    {
                        System.IO.File.Delete(_env.WebRootPath +articulo.UrlImagen);//Esta parte hay qye verla pq borra la imagen anterior, por el tema de la galeria
                    }
                }

                filename = filename.Replace("HiShop\\wwwroot\\", "+");
                filename = "\\" + filename.Split("+")[1];
                filename = filename.Replace("\\", "/");

                return filename;
            }


        }





    }
}
