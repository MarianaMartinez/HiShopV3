using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models
{
    public class UsuarioCambioDEContraeñaModelAndView
    {
        public int id { get; set; }

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
        public string contraseña3{ get; set; }

    }
}
