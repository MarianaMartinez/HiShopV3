using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HiShop.Herramientas;
/// <summary>
/// Model utilizado para recibir los datos del formulario de edicion de datos generales
/// en la secion de perfil
/// AxelMolaro
/// </summary>
namespace HiShop.Models
{

    public class UsuarioDatosGeneralesModelAndView
    {
       
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio .")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido es un campo obligatorio .")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "El mail es un campo obligatorio .")]
        [EmailAddress(ErrorMessage = "El mail ingresado no es válido .")]
        public string mail { get; set; }

        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = " Solo es permitido el formato.jpg ,jpeg, y.png")]
        public IFormFile file { get; set; }

    }
}
