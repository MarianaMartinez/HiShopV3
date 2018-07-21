using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity.Data;
using HiShop.Entity;
using HiShop.Dao;
using Microsoft.AspNetCore.Http;
using HiShop.Herramientas;
using Microsoft.AspNetCore.Hosting;
using HiShop.Enum;
using System.Linq;
using HiShop.Models.Mail;
using HiShop.Models.Inicio;
using HiShop.Models.Base;
using HiShop.Models.Catalogo;

namespace HiShop.Controllers
{
    /// <summary>
    /// Controlador que maneja lo relacionado al Ecommerce propio de la plataforma. 
    /// Mariana Martínez 
    /// </summary>
    public class CatalogoController : BaseCoreController
    {
        private IHostingEnvironment _env;
        public CatalogoController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

        ArticuloDao ArticuloDao = new ArticuloDao();
        ServicioDao servicioDao = new ServicioDao();
        MailService mailService = new MailService();
        
        /// <summary>
        /// Trae todos los Productos (artículos y servicios) 
        /// </summary>
        /// <returns></returns>
        public IActionResult Ecommerce()
        {
            CatalogoGeneral model = new CatalogoGeneral(HttpContext, _context);
            List<Producto> productos = ProductoDAo.ListadoDeProductos(_context);
            model.productos = productos;
            return View(model);
        }

        /// <summary>
        /// Buscador básico
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Ecommerce(string query)
        {
            CatalogoGeneral model = new CatalogoGeneral(HttpContext, _context);

            List<Producto> productos = ProductoDAo.ListadoDeProductos(_context);

            TempData["PalabraClave"] = query;

            List<Producto> nuevaLista = new List<Producto>();

            foreach (var q in productos)
            {
                if (query != null)
                {
                    if ((q.Nombre.ToLower()).Contains(query.ToLower()))
                    {
                        nuevaLista.Add(q);
                    }
                }
                else {
                    nuevaLista = productos;
                }
            }

            if (nuevaLista.Count() == 0)
            {
                TempData["Mensaje"] = "No se encontraron resultados";
            }
            model.productos = nuevaLista;

            return View(model);
        }

        //[HttpPost]
        public IActionResult DetallesDeProducto(int id)
        {
            Producto producto = ProductoDAo.get(_context, id);

            List<Articulo> articulos = ArticuloDao.ListadoDeArticulos(_context);

            foreach (var a in articulos)
            {
                if (producto.ID == a.ID)
                {
                    DetalleArticuloModelAndView model = new DetalleArticuloModelAndView(HttpContext, _context);
                    model.Articulo = a;
                    return View("DetallesDeArticulo", model);
                }
            }
            DetalleServicioModelAndView model2 = new DetalleServicioModelAndView(HttpContext, _context);
            model2.Servicio = producto;
            return View("DetallesDeServicio", model2);
        }

        public IActionResult DetallesDeArticulo(int id)
        {
            DetalleArticuloModelAndView model = new DetalleArticuloModelAndView(HttpContext, _context);
            Articulo articulo = ArticuloDao.get(_context, id);
            model.Articulo = articulo;
            return View(model);
        }

        /// <summary>
        /// Envía consultas al mail sobre el producto: 
        /// </summary>
        /// <param name="_objModelMail"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Consultar(MailModel _objModelMail, int id)
        {
            Producto producto = ProductoDAo.get(_context, id);
            mailService.consultarPorUnProducto(_context, _objModelMail, producto);
            agregarMensajePrincipal("Consulta Enviada", TipoMensaje.EXITO);
            TempData["Mensajes"] = mensajes;
            return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
        }

        public IActionResult Comprar(int id, int cantidad)
        {
            Producto producto = ProductoDAo.get(_context, id);
            HiShop.Models.Catalogo.DetalleServicioModelAndView model = new DetalleServicioModelAndView(HttpContext, _context);
            model.Servicio = producto;
            if (cantidad == 0)
            {
                TempData["total"] = producto.Precio;
            }
            else
            {
                decimal calculoTotal = producto.Precio * cantidad;
                TempData["total"] = calculoTotal;
                TempData["GuardarCantidad"] = cantidad;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ComprarPost(int id, decimal total, int p, OrdenPedido orden)
        {
            Producto producto = ProductoDAo.get(_context, id);

            TempData["total"] = total;

            //Axel: con esto estuve probando, pero todo lo q tenga q ver con enum, no sirve
            p = Convert.ToInt32(TempData["GuardarPago"]);

            OrdenPedido ordenPedido = new OrdenPedido
            {
                EstadoPedido = EstadoPedido.PENDIENTE,
                Pago = (Pago)p,
                Envio = orden.Envio,
                Total = Convert.ToDecimal(TempData["total"]),
                NegocioID = producto.NegocioID,
                Producto = ProductoDAo.get(_context, id),
                Usuario = UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID)
            };



            OrdenPedidoDao.grabar(_context, ordenPedido);
            //MailModel _objModelMail = new MailModel();
            //mailService.ConfirmarPedido(_context, _objModelMail, ordenPedido); Tengo q terminar enviar mail cuando se realiza un pedido! Mariana. 
            agregarMensajePrincipal("Se ha realizado una orden de pedido correctamente.", TipoMensaje.EXITO);
            TempData["Mensajes"] = mensajes;
            ModelBase model = new ModelBase();
            model.llenarDatosGenerales(HttpContext, _context);
            return View("~/Views/Inicio/InicioLogueado.cshtml",model);
        }

        //[HttpPost] 
        /*public IActionResult ConcretarCompra(OrdenPedido orden)
        {
            MailModel _objModelMail = new MailModel();
            mailService.ConcretarCompra(_context, _objModelMail, orden);
            agregarMensajePrincipal("Se confirmó la compra.", TipoMensaje.EXITO);
            TempData["Mensajes"] = mensajes;
            return View("~/Views/Inicio/Inicio.cshtml", new InicioModelAndView());
        }*/

        /// <summary>
        /// Filtro de Órdenes de Pedido Pendientes y Concretadas. 
        /// </summary>
        /// <returns></returns>
        public IActionResult OrdenesDePedido()
        {
            HiShop.Models.Catalogo.VentasModelAndView model = new VentasModelAndView(HttpContext, _context);
            List<OrdenPedido> ordenes = OrdenPedidoDao.getListado(_context, UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID));
            model.ordenes = ordenes;

            return View(model);
        }

        /// <summary>
        /// Permite cambiar el estado de las Órdenes Pendientes. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult OrdenesDePedido(int id)
        {
            HiShop.Models.Catalogo.VentasModelAndView model = new VentasModelAndView(HttpContext, _context);
            OrdenPedido orden = OrdenPedidoDao.GetOrdenPedido(_context, id);
            orden.Envio = orden.Envio;
            orden.NegocioID = orden.NegocioID;
            orden.Pago = orden.Pago;
            orden.Producto = orden.Producto;
            orden.Total = orden.Total;
            orden.Usuario = orden.Usuario;
            orden.EstadoPedido = EstadoPedido.CONCRETADO;
            OrdenPedidoDao.Editar(_context, orden);
            List<OrdenPedido> ordenes = OrdenPedidoDao.getListado(_context, UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID));
            model.ordenes = ordenes;

            return View(model);
        }

        public IActionResult MisCompras()
        {
            List<OrdenPedido> ordenes = OrdenPedidoDao.getListadoCompras(_context, UsuarioDao.getUsuario(_context, HttpContext.Session.GetObjectFromJson<Usuario>("usuarioEnSession").ID));
            return View(ordenes);
        }
    }
}
