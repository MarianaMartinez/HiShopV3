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
    /// Esta clase contiene todas las operaciones relacionadas con Usuario
    /// AxelMolaro
    /// </summary>
    public  class UsuarioDao 
    {
        /// <summary>
        /// Lista todos los usuarios
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static  List<Usuario> getListado(HiShopContext _context)
        {
            return  _context.Usuarios.ToList();
        }

        /// <summary>
        /// Trae un usuario por id
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static  Usuario getUsuario(HiShopContext _context,int idUsuario)
        {
            var usuario =  _context.Usuarios.Single(o => o.ID == idUsuario);
            return usuario;
        }
        /// <summary>
        /// Optiene un usuario po uid de facebook
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="idUsuarioFacebook"></param>
        /// <returns></returns>
        public static Usuario getUsuarioPorIdFacebook(HiShopContext _context, string idUsuarioFacebook)
        {
            var usuario = _context.Usuarios.SingleOrDefault(o => o.IdentificacionFacebook == idUsuarioFacebook);
            return usuario;
        }

        public static Usuario getUsuarioPorNickName(HiShopContext _context, string nickName)
        {
            var usuario = _context.Usuarios.SingleOrDefault(o => o.Nombre == nickName);
            return usuario;
        }

        public static int getCantidadConNombre(HiShopContext _context, string nickName)
        {
            var usuario = _context.Usuarios.Where(o => o.Nombre == nickName);
            return usuario.Count() ;
        }

        /// <summary>
        /// Tree un usuario por id
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static Usuario getUsuarioPorMail(HiShopContext _context, string mailUsuario)
        {
            var usuario =  _context.Usuarios.SingleOrDefault(o => o.Mail == mailUsuario);

            return usuario;
        }


        /// <summary>
        /// Guarda un usuario en la base de datos
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="usuario"></param>
        public static  void grabarUsuario(HiShopContext _context, Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
             _context.SaveChanges();
        }

        /// <summary>
        /// Guarda un usuario en la base de datos
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="usuario"></param>
        public static  void editarUsuario(HiShopContext _context, Usuario usuario)
        {
            _context.Update(usuario);
             _context.SaveChanges();
        }

        /// <summary>
        /// Busca un usuario por mail y si coincide con la contraseña pasada devuelve true, de lo contrario null
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="mail"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public static bool coincideMailYContraseña(HiShopContext _context, string mail,string contraseña)
        {
            var usuario = new Usuario();
            try
            {
                usuario =  getUsuarioPorMail(_context, mail);
                Boolean esValido = false;
                if(usuario != null)
                {
                    if (usuario.Mail != null || usuario.Mail != "")
                    {
                        if (usuario.Contraseña.Equals(contraseña))
                        {
                            esValido = true;
                        }
                    }

                }
               
                return esValido;
            }
            catch
            {
                throw new Exception("Ocurrio un error al verifiar usuario en la base de datos");
            }

           
        }

        /// <summary>
        /// Guarda una imagen en el servidor en la carpeta Imagenes Usuarios
        /// y La url en base de datos
        /// Tambien crea una carpeta con la id del usuario y guarda la imagen ahi
        /// Si la imnagen existe la borra y crea otra, siempre va a tener una sola imagen esa carpeta
        /// AxelMolaro
        /// </summary>
        /// <param name="urlCarpeta"></param>
        /// <param name="imagenPelicula"></param>
        /// <returns></returns>
        public static string guardarUnaImagenEnUnCarpetaDelServidor(IFormFile file, IHostingEnvironment _env,Usuario usuario)
        {
            if (file == null)
            {
                return usuario.UrlImagen;
            }
            String ruta = _env.WebRootPath + "\\ImagenesServidor\\ImagenesUsuarios";
            if (!System.IO.Directory.Exists(ruta + "\\" + usuario.ID.ToString()))
                {
                    System.IO.Directory.CreateDirectory(ruta + "\\" + usuario.ID.ToString());
                }

            ruta = ruta + "\\" + usuario.ID.ToString();
            
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
                    if (usuario.UrlImagen != null)
                    {
                        System.IO.File.Delete(usuario.UrlImagen);
                    }
                }
                return filename;
            }
            

        }
        /// <summary>
        /// Borra una imagen de usuario, y borra su ruta de la base de datos
        /// AxelMolaro
        /// </summary>
        /// <param name="usuario"></param>
        public static  void BorrarImagen( HiShopContext _context, Usuario usuario)
        {
            if (!String.IsNullOrEmpty(usuario.UrlImagen))
            {
                System.IO.File.Delete(usuario.UrlImagen);
            }
            usuario.UrlImagen = null;
            editarUsuario(_context , usuario);
        }


    }
}
