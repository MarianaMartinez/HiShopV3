using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Mail
{
    public class MailModel
    {
        //From y To ya están definidos. 
        public string From { get; set; } 
        public string To { get; set; }

        public string Subject { get; set; } //Opcional. 

        [Required(ErrorMessage="Escribí un mensaje")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Ingres{a tu nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingresá tu Email")] 
        //validar mail valido
        public string Email { get; set; }
    }
}
