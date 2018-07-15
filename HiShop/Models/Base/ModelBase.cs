using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using HiShop.Models;
using HiShop.Enum;
using HiShop.Entity;
using HiShop.Herramientas;
using Microsoft.AspNetCore.Http;
using HiShop.Dao;
using HiShop.Entity.Data;
using HiShop.Models.Filtros;

namespace HiShop.Models.Base
{

    public class ModelBase 
    {
        public Usuario usuarioModelBase { get; set; }
        public HiShop.Entity.Negocio negocioModelBase { get; set; }
        public List<HiShop.Entity.Negocio> negociosModelBase { get; set; }

        public List<MensajeModel> mensajesPrincipales { get; set; }
        public bool estaLogueadoConFacebook { get; set; }

        public ModelBase()
        {
            this.mensajesPrincipales = new List<MensajeModel>();
            
        }

        public ModelBase(HttpContext httpContext, HiShopContext _context)
        {
            this.mensajesPrincipales = new List<MensajeModel>();
            llenarDatosGenerales(httpContext, _context);
            

        }

        /// <summary>
        /// Carga los mensajes que se muestran en la cabecera cuando carga la pagina
        /// Se debe pasar un tempdata
        /// AxelMolaro
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tipo"></param>
        public void agregarMensajePrincipal(String texto, TipoMensaje tipo)
        {
            MensajeModel mensaje = new MensajeModel(texto, tipo);
            this.mensajesPrincipales.Add(mensaje);
        }

        public void llenarDatosGenerales(HttpContext httpContext, HiShopContext _context)
        {
            usuarioModelBase = httpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession");
            negocioModelBase = httpContext.Session.GetObjectFromJson<HiShop.Entity.Negocio>("negocioEnSession");
            if (usuarioModelBase != null)
            {
                negociosModelBase = NegocioDao.getListado(_context, new FiltroNegocio(), usuarioModelBase);
            }
            var accesToken = httpContext.Session.GetObjectFromJson<string>("accessToken");
            if (String.IsNullOrEmpty(accesToken))
            {
                estaLogueadoConFacebook = false;
            }
            else {
                estaLogueadoConFacebook = true;
            }


        }
    }


   
}