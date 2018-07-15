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
    /// Esta clase contiene todas las operaciones relacionadas con Servicio
    /// PedroCora
    /// </summary>
    public class ServicioDao
    {

        /// <summary>
        /// Guarda un servicio en la base de datos , creacion
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="servicio"></param>
        public static  void grabarServicio(HiShopContext _context, Servicio servicio)
        {
            _context.Entry(servicio).State = EntityState.Added;
            _context.Add(servicio);
            _context.Update(servicio.Negocio);
            _context.SaveChanges();

        }


        /// <summary>
        /// Tomar un servicio de la base de datos por id
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="id"></param>
        public Servicio obtenerServicioPorID(HiShopContext _context, int? id)
        {
            if (id == null)
            {
                throw new Exception();
            }

            Servicio servicio = _context.Servicios.Single(m => m.ID == id);
            if (servicio == null)
            {
                throw new Exception();
            }
            return servicio;
        }

        /// <summary>
        /// Edicion de servicio en la base de datos 
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="servicio"></param>
        public void editarServicio(HiShopContext _context, Servicio servicio)
        {
            Servicio serv = obtenerServicioPorID(_context, servicio.ID);
            if (serv == null)
            {
                throw new Exception();
            }

            serv.ID = servicio.ID;
            serv.Nombre = servicio.Nombre;
            serv.Descripcion = servicio.Descripcion;
            serv.UrlImagen= servicio.UrlImagen;
            _context.SaveChanges();
        }

        /// <summary>
        /// Listado de servicios  
        /// PedroCora
        /// </summary>
        /// <param name="_context"></param>
        public List<Servicio> ListadoDeServicios(HiShopContext _context)
        {
            return _context.Servicios.ToList();
        }


        public List<Servicio> ListadoDeServicios(HiShopContext _context, FiltrosServicio filtro, Negocio negocio)
        {
            var lista = _context.Servicios.ToList();
            if (!String.IsNullOrEmpty(filtro.nombreFiltro))
            {
                lista = lista.ToList().Where(o => o.Nombre.ToLower().Contains(filtro.nombreFiltro.ToLower())).ToList();

            }

            lista = lista.Where(o => o.NegocioID == negocio.ID).ToList();
            List<Servicio> servicios = lista.ToList();
            return filtro.paginar(servicios, filtro.paginaActual);


        }

        public void borrarServicio(HiShopContext _context, Servicio servicio)
        {
            Servicio serv = obtenerServicioPorID(_context, servicio.ID);
            if (serv == null)
            {
                throw new Exception();
            }

            _context.Entry(serv).State = EntityState.Deleted;

            _context.SaveChanges();
        }



        /* IMAGEN */

        public static string guardarImagenServicio(IFormFile file, IHostingEnvironment _env, Servicio servicio)
        {
            if (file == null)
            {
                return servicio.UrlImagen;
            }
            String ruta = _env.WebRootPath + "\\ImagenesServidor\\ImagenesServicios";
            if (!System.IO.Directory.Exists(ruta + "\\" + servicio.ID.ToString()))
            {
                System.IO.Directory.CreateDirectory(ruta + "\\" + servicio.ID.ToString());
            }

            ruta = ruta + "\\" + servicio.ID.ToString();

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
                    if (servicio.UrlImagen != null)
                    {
                        System.IO.File.Delete(_env.WebRootPath  + servicio.UrlImagen);
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
