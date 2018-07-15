using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facebook;
using Microsoft.AspNetCore.Authentication.Twitter;
using System.Web.Http;
using HiShop.Entity;
using HiShop.Dao;
using HiShop.Entity.Data;
using HiShop.Herramientas;

namespace HiShop.Controllers
{
    public class FaceControllerController : BaseCoreController
    {
        public FaceControllerController(HiShopContext context) : base(context)
        {
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public String loguearConFacebook(string idUsuario, string nombreUsuario,string urlActual)
        {
            Usuario usuario = UsuarioDao.getUsuarioPorIdFacebook(_context, idUsuario);
            if (urlActual == "http://localhost:49836" || urlActual.ToLower() == "http://localhost:49836/inicio/inicio")
            {
                string nick = nombreUsuario.Trim().ToLower();
                if (idUsuario != null)
                {
                    if (usuario == null)
                    {
                        if (UsuarioDao.getUsuarioPorNickName(_context, idUsuario) != null)
                        {
                            string cantidad = UsuarioDao.getCantidadConNombre(_context, nick).ToString();
                            nick = nick + cantidad;
                        }
                        usuario = new Usuario { IdentificacionFacebook = idUsuario, Nombre = nick };
                        UsuarioDao.grabarUsuario(_context, usuario);
                    }

                }

                Usuario usaurioParaSession = UsuarioDao.getUsuarioPorNickName(_context, nick);
                HttpContext.Session.SetObjectAsJson("usuarioEnSession", usaurioParaSession);
                return "/Usuario/Perfil";
            }
            else
            {
                return "/Inicio/Inicio";
            }
        }


    }
}