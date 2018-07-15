using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity.Data;
using HiShop.Models;
using HiShop.Enum;
using HiShop.Entity;
using Microsoft.AspNetCore.Http;
using HiShop.Herramientas;

namespace HiShop.Controllers
{
    /// <summary>
    /// Esta es el controles base que va a tener el contexto para realizar operaciones de base de dartos
    /// y tambien tine los mensajes que son los proncipales que se muestan arriba, se intancia unas lista de mensajes
    /// por constructor
    /// AxelMolaro
    /// </summary>
    public abstract class BaseCoreController : Controller
    {
        protected readonly HiShopContext _context;
        public static List<MensajeModel> mensajes;

        /// <summary>
        /// Constructor de BaseCoreController
        /// AxelMolaro
        /// </summary>
        /// <param name="context"></param>
        protected BaseCoreController(HiShopContext context)
        {
            _context = context;
            mensajes = new List<MensajeModel>();
        }

        /// <summary>
        /// Agrega un mensaje a la lisata mensajes, se le pasa el texto y el tipo 
        /// AxelMolaro
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tipo"></param>
        public static void agregarMensajePrincipal(String texto, TipoMensaje tipo)
        {
            MensajeModel mensaje = new MensajeModel(texto, tipo);
            mensajes.Add(mensaje);
        }

        /// <summary>
        /// Vacio la lista de mensajes principalaes
        /// AxelMolaro
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tipo"></param>
        public static void limpiarMensajePrincipal()
        {
            mensajes.Clear();
        }

        
    }
}