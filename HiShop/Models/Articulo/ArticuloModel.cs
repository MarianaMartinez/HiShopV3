using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HiShop.Entity;
using Microsoft.AspNetCore.Http;
using HiShop.Models.Base;
using HiShop.Entity.Data;

namespace HiShop.Models
{
    public class ArticuloModel : ModelBase
    {

        public int Id { get; set; }

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

        public string UrlImagen { get; set; }

        [Herramientas.FileExtensions("jpg,jpeg,png", ErrorMessage = " Solo es permitido el formato.jpg ,jpeg, y.png")]
        public IFormFile file { get; set; }

        public ArticuloModel()
        {
            
        }

        public ArticuloModel(HttpContext httpContext, HiShopContext _context,Entity.Articulo articulo) : base(httpContext, _context)
        {
            this.Id = articulo.ID;
            this.Nombre = articulo.Nombre;
            this.UrlImagen = articulo.UrlImagen;
            this.Descripcion = articulo.Descripcion;
            this.Precio = articulo.Precio;
            this.Cantidad = articulo.Cantidad;
        }

        public ArticuloModel(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
        }
    }
}