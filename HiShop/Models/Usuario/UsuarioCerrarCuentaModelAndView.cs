using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models
{
    public class UsuarioCerrarCuentaModelAndView
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña  es un campo obligatorio .")]
        public string contraseña { get; set; }
    }
}
