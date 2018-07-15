using HiShop.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HiShop.Models
{
    public class NegocioModelAndView 
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = "Sólo es permitido el formato .jpg .jpeg, y .png")]
        public IFormFile file { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        public int CategoriaFK{ get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        public int ProvinciaFK { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        public int LocalidadFK{ get; set; }
        public int PackActivo { get; set; }
        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        [StringLength(30, ErrorMessage = "No se permite un nombre tan largo")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Sólo se permiten letras")]
        public string Calle { get; set; }


        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        [StringLength(5, ErrorMessage = "Ingrese hasta 5 digitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se permiten números.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Número de teléfono no válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        [EmailAddress(ErrorMessage = "El mail ingresado no es válido.")]
        public string Email { get; set; }

        public string Descripcion { get; set; }

        public string UsuarioFKModel { get; set; } 

        public EstadoNegocio estado { get; set; }

        [Required(ErrorMessage = "El campo es un campo obligatorio")]
        public int ShowroomFk { get; set; }

        public NegocioModelAndView()
        {
            
        }

    }
}
