using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HiShop.Entity;
using Microsoft.AspNetCore.Http;


namespace HiShop.Models.Servicio
{
    public class EditarServicioModelAndView
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio .")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es un campo obligatorio .")]
        public string Descripcion { get; set; }


        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = " Solo es permitido el formato.jpg ,jpeg, y.png")]
        public IFormFile file { get; set; }


        public String Mensaje { get; set; }

        public EditarServicioModelAndView()
        {



        }

    }
}
