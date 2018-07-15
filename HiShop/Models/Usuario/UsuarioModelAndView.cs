using HiShop.Entity;
using HiShop.Enum;
using HiShop.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models
{
    public class UsuarioModelAndView : ModelBase
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio .")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido es un campo obligatorio .")]
        public string apellido { get; set; }

        public Estado estado { get; set; }

        public string urlImagen { get; set; }

        [Required(ErrorMessage = "El mail es un campo obligatorio .")]
        [EmailAddress(ErrorMessage = "El mail ingresado no es válido .")]
        public string mail { get; set; }


        [Required(ErrorMessage = "El mail es un campo obligatorio .")]
        [EmailAddress(ErrorMessage = "El mail ingresado no es válido .")]
        [Compare(nameof(mail), ErrorMessage = "Los mails ingresados no coinciden.")]
        public string mail2 { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña actual es un campo obligatorio .")]
        public string contraseña { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña nueva es un campo obligatorio .")]
        [Compare(nameof(contraseña3), ErrorMessage = "Las nuevas contraseñas deben ser iguales .")]
        public string contraseña2 { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Repita la contraseña nueva .")]
        [Compare(nameof(contraseña2), ErrorMessage = "Las nuevas contraseñas deben ser iguales .")]
        public string contraseña3 { get; set; }


        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = " Solo es permitido el formato.jpg ,jpeg, y.png")]
        public IFormFile file { get; set; }


        //Se usa para mostrar el formulario general de perfil para edicion, o para no edicion
        //Segun corresponda
        //AxelMoalro
        public String losDatosSonValidos { get; set; }

        /// <summary>
        /// Esta variable la usio para mostrar el formualrio correspondiente cuendo recargo la pagina
        /// AxelMolaro
        /// </summary>
        public String formularioActivo { get; set; }

        public void llenarValoresEnBaseAUnUsuario(Usuario usuario)
        {
            this.id = usuario.ID;
            this.nombre = usuario.Nombre;
            this.apellido = usuario.Apellido;
            this.mail = usuario.Mail;
            this.estado = usuario.Estado;
            if (string.IsNullOrEmpty(usuario.UrlImagen))
            {
                string ruta = "/images/sinFotoDePerfil.jpg";
               urlImagen = ruta;
            }
            else
            {
                urlImagen = usuario.UrlImagen.Replace("HiShop\\wwwroot\\", "+");
                urlImagen = "\\"+urlImagen.Split("+")[1] ;
                urlImagen = urlImagen.Replace("\\", "/");
               

            }
        }

    }
}
