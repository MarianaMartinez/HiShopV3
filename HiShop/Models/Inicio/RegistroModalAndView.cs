using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HiShop.Models.Base;
using HiShop.Models.Inicio;

namespace HiShop.Models
{
    public class RegistroModalAndView : InicioModelAndView
    {
       
        [Required(ErrorMessage = "El nombre es un campo obligatorio .")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido es un campo obligatorio .")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "El mail es un campo obligatorio .")]
        [ EmailAddress(ErrorMessage = "El mail ingresado no es válido .")]
        public string mail { get; set; }

        [Required(ErrorMessage = "El mail es un campo obligatorio .")]
        [EmailAddress(ErrorMessage = "El mail ingresado no es válido .")]
        [Compare(nameof(mail),ErrorMessage = "Los mails ingresados no coinciden.")]
        public string mail2 { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es un campo obligatorio .")]
        public string contraseña { get; set; }

        [Required(ErrorMessage = "La segunda contraseña un campo obligatorio .")]
        [Compare(nameof(contraseña), ErrorMessage = "Los contraseñas ingresadas no coinciden.")]
        public string contraseña2 { get; set; }



        public RegistroModalAndView()
        {

        }

        /// <summary>
        /// Constructor con attrs
        /// AxelMolaro
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="mail"></param>
        /// <param name="mail2"></param>
        /// <param name="contraseña"></param>
        /// <param name="contraseña2"></param>
        /// <param name="mailLogin"></param>
        /// <param name="contraseñaLogin"></param>
        public RegistroModalAndView(string nombre, string apellido, string mail, string mail2, string contraseña, string contraseña2, string mailLogin,string contraseñaLogin)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.mail2 = mail2;
            this.contraseña = contraseña;
            this.contraseña2 = contraseña2;
            this.mailLogin = mailLogin;
            this.contraseñaLogin = contraseñaLogin;
        }


        public void limpiarDatosDeRegistro()
        {
            this.nombre = null;
            this.apellido = null;
            this.mail = null;
            this.mail2 = null;
            this.contraseña = null;
            this.contraseña = null;
        }
        public void limpiarDatosDeLogin()
        {
            this.mailLogin = null;
            this.contraseñaLogin = null;
        }
    }
}