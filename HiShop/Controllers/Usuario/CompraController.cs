using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity.Data;
using HiShop.Dao;
using HiShop.Entity;
using HiShop.Herramientas;
using HiShop.Models.OrdenPedido;

namespace HiShop.Controllers
{
    public class CompraController : BaseCoreController
    {
        public CompraController(HiShopContext context) : base(context)
        {

        }


        //Esta vista contiene los detalles del, por el momento, Artículo, que se eligió. 
        public IActionResult DetallesDeProducto(int id)
        {
            Entity.Producto producto = ProductoDAo.get(_context, id);
            Entity.Negocio negocio = NegocioDao.get(_context, producto.NegocioID);
            OrdenPedidoGeneralModelAndView model = new OrdenPedidoGeneralModelAndView(negocio);
            model.Producto = producto;
            return View(model);
        }


        public IActionResult EfectuarCompra(int id)
        {

            Entity.Producto producto = ProductoDAo.get(_context, id);
            Entity.Negocio negocio = NegocioDao.get(_context, producto.NegocioID);
            OrdenPedidoGeneralModelAndView model = new OrdenPedidoGeneralModelAndView(negocio);
            model.Producto = producto;
            return View(model);
        }

        [HttpPost]
        public IActionResult EfectuarCompra(OrdenPedidoGeneralModelAndView model)
        {
            if (ModelState.IsValid)
            {
                Producto producto = ProductoDAo.get(_context, model.productoId);
                OrdenPedido ordenPedido = new OrdenPedido
                {
                    EstadoPedido = Enum.EstadoPedido.PENDIENTE,
                    Total = model.cantidad * producto.Precio,
                    Negocio = NegocioDao.get(_context, producto.NegocioID),
                    Usuario = UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID),
                    Producto = producto
                };
                OrdenPedidoDao.grabar(_context, ordenPedido);
                return RedirectToAction("Perfil","Usuario");
            }
            return View();
        }


        public IActionResult ComprasListado()
        {

            Usuario usuario = UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID);
            List<OrdenPedido> lista = OrdenPedidoDao.getListado(_context, usuario);
            return View(lista);
        }

    }
}