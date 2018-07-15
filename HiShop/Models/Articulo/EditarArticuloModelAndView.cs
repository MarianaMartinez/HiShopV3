using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HiShop.Entity;
using Microsoft.AspNetCore.Http;
namespace HiShop.Models.Articulo
{
    public class EditarArticuloModelAndView
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio .")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La descripion es obligatoria.")]
        public string Descripcion { get; set; }

        [Range(0, 5000)]
        [Required(ErrorMessage = "El Precio es un campo obligatorio .")]
        public decimal Precio { get; set; }

        [Range(0, 500)]
        [Required(ErrorMessage = "La Cantidad es un campo obligatorio .")]
        public decimal Cantidad { get; set; }

        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = " Solo es permitido el formato.jpg ,jpeg, y.png")]
        public IFormFile file { get; set; }

        public EditarArticuloModelAndView()
        {

        }
    }
}
