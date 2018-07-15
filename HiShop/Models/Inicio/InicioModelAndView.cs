using HiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Inicio
{
    public class InicioModelAndView : ModelBase
    {

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string mail { get; set; }

        public string mail2 { get; set; }

        public string contraseña { get; set; }

        public string contraseña2 { get; set; }


        public String mailLogin { get; set; }

        public String contraseñaLogin { get; set; }


        /// <summary>
        /// Crea y llena su registroModelAndView
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
        public void llenarModeloEnBaseAUnRegistroModelAndView(RegistroModalAndView model){
            this.nombre = model.nombre;
            this.apellido = model.apellido;
            this.mail = model.mail;
            this.mail2 = model.mail2;
            this.contraseña = model.contraseña;
            this.contraseña2 = model.mailLogin;
            this.mailLogin = model.nombre;
            this.contraseñaLogin = model.contraseñaLogin;


        }

        
    }
}
