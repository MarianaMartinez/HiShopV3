using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Models.Filtros;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HiShop.Herramientas;

namespace HiShop.Dao
{
    public static class NegocioDao
    {
        //Guarda un Negocio en la Base de Datos: 
        public static  void grabar(HiShopContext context, Negocio negocio)
        {
             context.Negocios.Add(negocio);
            context.SaveChanges();
        }

        public static  Negocio get(HiShopContext context, int id)
        {
            Negocio negocio = null;
            try
            {
                negocio =  context.Negocios.SingleOrDefault(m => m.ID == id);
            }
            catch {
                throw new InvalidDataException("Ocurrio un error al buscar el negocio, verifique los datos .");
            }
            return negocio;
        }

        public static void editar(HiShopContext context, Negocio negocio)
        {
            try {
                context.Negocios.Update(negocio);
                context.SaveChanges();
            }catch
            {
                throw new InvalidDataException("Ocurrio un error al editar el negocio .");
            }
        }


        public static void darDeBajaLogica(HiShopContext context, Negocio negocio)
        {
            try
            {
                negocio.Estado = Enum.EstadoNegocio.INHABILITADO;
                editar(context, negocio);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al dar de baja el negocio .");
            }
           
        }


        public static void darDeAltaLogica(HiShopContext context, Negocio negocio)
        {
            try
            {
                negocio.Estado = Enum.EstadoNegocio.INHABILITADO;
                editar(context, negocio);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al dar de alta el negocio .");
            }

        }


        public static  List<Negocio> getListado (HiShopContext _context)
        {
            return  _context.Negocios.ToList();

        }

        public static List<Negocio> getListado(HiShopContext _context,FiltroNegocio filtro,Usuario usuario)
        {
            var lista = _context.Negocios.ToList();
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nombreFiltro))
                {
                    lista = lista.ToList().Where(o => o.Nombre.ToLower().Contains(filtro.nombreFiltro.ToLower())).ToList();

                }
                if (filtro.idCategoriaFiltro != 0)
                {
                    lista = lista.Where(o => o.CategoriaID == filtro.idCategoriaFiltro).ToList();
                }
            }
           
            lista = lista.Where(o => o.UsuarioID == usuario.ID).ToList();
            List<Negocio> negocios = lista.ToList();
            return filtro.paginar(negocios,filtro.paginaActual);

        }

        public static List<Negocio> getListadoPorUsuario(HiShopContext _context, Usuario usuario)
        {
            var lista = _context.Negocios.ToList();
            lista = lista.Where(o => o.UsuarioID == usuario.ID).ToList();
            return lista;
        }



        /// <summary>
        /// Guarda una imagen en el servidor en la carpeta Imagenes Negocios
        /// y La url en base de datos
        /// Tambien crea una carpeta con la id del Negocio y guarda la imagen ahi
        /// Si la imnagen existe la borra y crea otra, siempre va a tener una sola imagen esa carpeta
        /// AxelMolaro
        /// </summary>
        /// <param name="urlCarpeta"></param>
        /// <param name="imagenPelicula"></param>
        /// <returns></returns>
        public static string guardarUnaImagenEnUnCarpetaDelServidor(IFormFile file, IHostingEnvironment _env, Negocio negocio)
        {
            if (file == null)
            {
                return negocio.UrlImagenNegocio;
            }
            String ruta = _env.WebRootPath + "\\ImagenesServidor\\ImagenesNegocios";
            if (!System.IO.Directory.Exists(ruta + "\\" + negocio.ID.ToString()))
            {
                System.IO.Directory.CreateDirectory(ruta + "\\" + negocio.ID.ToString());
            }

            ruta = ruta + "\\" + negocio.ID.ToString();

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
                    if (!String.IsNullOrEmpty(negocio.UrlImagenNegocio) && negocio.UrlImagenNegocio != "/images/sinFotoDePerfil.jpg")
                    {
                        System.IO.File.Delete(negocio.UrlImagenNegocio);
                    }
                }

                filename = filename.Replace("HiShop\\wwwroot\\", "+");
                filename = "\\" + filename.Split("+")[1];
                filename = filename.Replace("\\", "/");

                return filename;
            }


        }
        /// <summary>
        /// Borra una imagen de Negocio, y borra su ruta de la base de datos
        /// AxelMolaro
        /// </summary>
        /// <param name="usuario"></param>
        public static async void BorrarImagen(HiShopContext _context, Negocio negocio)
        {
            if (!String.IsNullOrEmpty(negocio.UrlImagenNegocio))
            {
                System.IO.File.Delete(negocio.UrlImagenNegocio );
            }
            negocio.UrlImagenNegocio = null;
            editar(_context, negocio);
        }


    }

    /*
    public List<Negocio> getListado(HiShopContext context, )
    {
        var consulta = from n in context.Negocios select n;
        if (!String.IsNullOrEmpty(filtro.nombre))
        {
            consulta.ToList().Where(o => o.Nombre == filtro.nombre);
        }
        if (filtro.idCategoria != 0)
        {
            consulta.Where(o => o.CategoriaFK == filtro.idCategoria);
        }
        if (filtro.estado == 2)
        {
            consulta.Where(o => o.Eliminado == false);
        }
        if (filtro.estado == 1)
        {
            consulta.Where(o => o.Eliminado == true);
        }
        var lista = consulta.ToList();
        return lista;
    }
    */


}
