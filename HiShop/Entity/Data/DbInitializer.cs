using System.Collections.Generic;
using System.Linq;
using HiShop.Enum;

namespace HiShop.Entity.Data
{
    /// <summary>
    /// Inicializa datos predeterminados
    /// Si se deja reinicia bdd en true va a borrar todos los datos y los va a iniciar cada vez que se reinicie el server
    /// AxelMolaro
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(HiShopContext context)
        {
            context.Database.EnsureCreated();
            bool reiniciaBdd = false;//Dejar en false si no queiro que recree la base de datos
            //Borra Datos
            List<Usuario> ListaDeUsuarios = context.Usuarios.ToList();
            if (reiniciaBdd)
            {
                List<Categoria> ListaDeCategorias = context.Categorias.ToList();
                List<Entity.Negocio> ListaDeNegocios = context.Negocios.ToList();
                List<Localidad> ListaDeLocalidades = context.Localidades.ToList();
                List<Provincia> ListaDeProvincias = context.Provincias.ToList();
                List<Showroom> ListaDeShowrooms = context.Showrooms.ToList();
                //Borra Usuarios
                foreach (Usuario u in ListaDeUsuarios)
                {
                    context.Remove(u);
                    context.SaveChanges();

                }
                //Borra Categorias
                foreach (Categoria c in ListaDeCategorias)
                {
                    context.Remove(c);
                    context.SaveChanges();

                }

                //Borra Negocios
                foreach (Entity.Negocio n in ListaDeNegocios)
                {
                    context.Remove(n);
                    context.SaveChanges();

                }

                //Borra Localidades
                foreach (Localidad l in ListaDeLocalidades)
                {
                    context.Remove(l);
                    context.SaveChanges();

                }

                //Borra Provincias
                foreach (Provincia p in ListaDeProvincias)
                {
                    context.Remove(p);
                    context.SaveChanges();

                }

                //Borra Showrooms
                foreach (Showroom s in ListaDeShowrooms)
                {
                    context.Remove(s);
                    context.SaveChanges();

                }




                //Usuarios
                var usuario = new Usuario { Nombre = "Axel", Apellido = "Molaro", Mail = "axelmolaro3@gmail.com", Contraseña = "1234", Estado = Enum.Estado.APROBADO, UrlImagen = "HiShop\\wwwroot\\ImagenesServidor/ImagenesUsuarios/Inicializar/usuarioLogo1.jpg" };
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                var usuario5 = new Usuario { Nombre = "Pedro", Apellido = "Cora", Mail = "coraPedro@gmail.com", Contraseña = "1234", Estado = Enum.Estado.APROBADO };
                context.Usuarios.Add(usuario5);
                context.SaveChanges();

                var usuario2 = new Usuario { Nombre = "Mariana", Apellido = "Martinez", Mail = "marianamartinez@gmail.com", Contraseña = "1234", Estado = Enum.Estado.APROBADO };
                context.Usuarios.Add(usuario2);
                context.SaveChanges();

                var usuario3 = new Usuario { Nombre = "Mariana", Apellido = "Martinez", Mail = "marianapatriciamartinez@hotmail.com.ar", Contraseña = "1234", Estado = Enum.Estado.APROBADO };
                context.Usuarios.Add(usuario3);
                context.SaveChanges();

                //Categorías (Rubros de Negocio): 
                var rubroPadre = new Categoria { Nombre = "Negocio" };
                context.Categorias.Add(rubroPadre);
                context.SaveChanges();

                var rubro1 = new Categoria { Nombre = "Reposteria", Padre = rubroPadre };
                context.Categorias.Add(rubro1);
                context.SaveChanges();

                var rubro2 = new Categoria { Nombre = "Indumentaria", Padre = rubroPadre };
                context.Categorias.Add(rubro2);
                context.SaveChanges();

                var rubro3 = new Categoria { Nombre = "Servicios", Padre = rubroPadre };
                context.Categorias.Add(rubro3);
                context.SaveChanges();

                var rubro4 = new Categoria { Nombre = "Farmacias", Padre = rubroPadre };
                context.Categorias.Add(rubro4);
                context.SaveChanges();

                //Categorias - Productos
                var padre = new Categoria { Nombre = "Articulos" };
                context.Categorias.Add(padre);
                context.SaveChanges();

                var indumenariaPadre = new Categoria { Nombre = "Indumentaria", Padre = padre };
                context.Categorias.Add(indumenariaPadre);
                context.SaveChanges();

                var caetegoria2 = new Categoria { Nombre = "Calzados", Padre = indumenariaPadre };
                context.Categorias.Add(caetegoria2);
                context.SaveChanges();

                var caetegoria3 = new Categoria { Nombre = "Camisas", Padre = indumenariaPadre };
                context.Categorias.Add(caetegoria3);
                context.SaveChanges();

                var caetegoria5 = new Categoria { Nombre = "Zapatillas Deportivas", Padre = caetegoria2 };
                context.Categorias.Add(caetegoria5);
                context.SaveChanges();

                var caetegoria7 = new Categoria { Nombre = "Zapatos", Padre = caetegoria2 };
                context.Categorias.Add(caetegoria7);
                context.SaveChanges();

                var caetegoria6 = new Categoria { Nombre = "Femeninas", Padre = caetegoria5 };
                context.Categorias.Add(caetegoria6);
                context.SaveChanges();

                var serviciosPadre = new Categoria { Nombre = "Servicios" };
                context.Categorias.Add(serviciosPadre);
                context.SaveChanges();

                var categoria9 = new Categoria { Nombre = "Plomeria", Padre = serviciosPadre };
                context.Categorias.Add(categoria9);
                context.SaveChanges();











                //PROVINCIAS: 
                var provincia1 = new Provincia { Nombre = "Buenos Aires" };
                context.Provincias.Add(provincia1);
                context.SaveChanges();

                var provincia2 = new Provincia { Nombre = "Buenos Aires - GBA" };
                context.Provincias.Add(provincia2);
                context.SaveChanges();

                var provincia3 = new Provincia { Nombre = "Capital Federal" };
                context.Provincias.Add(provincia3);
                context.SaveChanges();

                //LOCALIDADES: 
                var localidad1 = new Localidad { Nombre = "25 de Mayo", Provincia = provincia1 };
                context.Localidades.Add(localidad1);
                context.SaveChanges();

                var localidad2 = new Localidad { Nombre = "3 de Febrero", Provincia = provincia1 };
                context.Localidades.Add(localidad2);
                context.SaveChanges();

                var localidad3 = new Localidad { Nombre = "A. Alsina", Provincia = provincia1 };
                context.Localidades.Add(localidad3);
                context.SaveChanges();

                var localidad4 = new Localidad { Nombre = "San Justo", Provincia = provincia1 };
                context.Localidades.Add(localidad4);
                context.SaveChanges();

                //Showroom
                Showroom showroom1 = new Showroom();
                context.Showrooms.Add(showroom1);
                context.SaveChanges();

                //Negocios
                /* var negocio1 = new Negocio
                {
                    Nombre = "Fashion",
                    Email = "Fashion@gmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = rubro2,
                    Localidad = localidad4,
                    Calle = "Av. Rivadavia",
                    Numero = "1234",
                    Telefono = "1234567891",
                    Descripcion = "Viste elegante, siéntete bien",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/fashionLogo.jpg",
                    Usuario = usuario,
                    Showroom = showroom1

                };
                context.Negocios.Add(negocio1);
                context.SaveChanges();

                var negocio2 = new Negocio
                {
                    Nombre = "Tortitas",
                    Email = "RicasTortasn@gmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = rubro1,
                    Localidad = localidad4,
                    Calle = "Peru",
                    Numero = "1234",
                    Telefono = "1234567891",
                    Descripcion = "Tortas de cumpleaños para todos",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/totasLogo.jpg",
                    Usuario = usuario,

                };
                context.Negocios.Add(negocio2);
                context.SaveChanges();

                var negocio3 = new Negocio
                {
                    Nombre = "Plomeria Miguel",
                    Email = "MiguelEcheren@gmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = rubro3,
                    Localidad = localidad4,
                    Calle = "Florio",
                    Numero = "1234",
                    Telefono = "1234567891",
                    Descripcion = "El mejor servicio de plomería garantizado",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/plomeriaLogo.jpg",
                    Usuario = usuario,

                };
                context.Negocios.Add(negocio3);
                context.SaveChanges();

                var negocio4 = new Negocio
                {
                    Nombre = "Farmacity",
                    Email = "Farmacity@gmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = rubro4,
                    Localidad = localidad4,
                    Calle = "Pichincha",
                    Numero = "1234",
                    Telefono = "1234567891",
                    Descripcion = "Viste elegante, siéntete bien",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/farmacityLogo.jpg",
                    Usuario = usuario,

                };
                context.Negocios.Add(negocio4);
                context.SaveChanges(); */

                var negocio5 = new Negocio
                {
                    Nombre = "Mil Colores",
                    Email = "marianapatriciamartinez@hotmail.com.ar",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = caetegoria2,
                    Localidad = localidad4,
                    Calle = "Arieta",
                    Numero = "1234",
                    Telefono = "1234567891",
                    Descripcion = "Calzado femenino",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/ArcoIris.jpg",
                    Usuario = usuario2,

                };
                context.Negocios.Add(negocio5);
                context.SaveChanges();

                var negocio6 = new Negocio
                {
                    Nombre = "Pinturerias Oeste",
                    Email = "pintureriasdeloeste@hotmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = caetegoria2,
                    Localidad = localidad4,
                    Calle = "F. Varela",
                    Numero = "1900",
                    Telefono = "1134567890",
                    Descripcion = "Todo para el pintor. Pinturas para exterior e interior, accesorios, y mucho mas...",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/pintureria-logo.png",
                    Usuario = usuario, 
                };
                context.Negocios.Add(negocio6);
                context.SaveChanges();

                var negocio7 = new Negocio
                {
                    Nombre = "Blue Jeans",
                    Email = "sjbluejeans@hotmail.com",
                    Estado = EstadoNegocio.APROBADO,
                    Categoria = caetegoria2,
                    Localidad = localidad4,
                    Calle = "Jose Marmol",
                    Numero = "1900",
                    Telefono = "1134567890",
                    Descripcion = "Jeans! Jeans! Jeans! Y mas jeans! En todos los talles, en todos los colores. La mas amplia variedad.",
                    UrlImagenNegocio = "/ImagenesServidor/ImagenesNegocios/Inicializar/images/jeans-logo.png",
                    Usuario = usuario2,
                };
                context.Negocios.Add(negocio7);
                context.SaveChanges();

                //Servicios
                /* var servicio1 = new Servicio { Descripcion = "Servicio de modista", Nombre = "Modista", Precio = 600, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesServicios/Inicializar/fashionServicio1.jpg" };
                context.Servicios.Add(servicio1);
                context.SaveChanges();

                var servicio2 = new Servicio { Descripcion = "Pedi Tu torta", Nombre = "Torta de Cumpleaños5", Precio = 600, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesServicios/Inicializar/totasServicio1.jpg" };
                context.Servicios.Add(servicio2);
                context.SaveChanges();

                var servicio3 = new Servicio { Descripcion = "Arreglo lo que sea", Nombre = "Plomero", Precio = 600, Negocio = negocio3, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/plomeriaServicio1.jpg" };
                context.Servicios.Add(servicio3);
                context.SaveChanges();

                //Articulos
                var articulo1 = new Articulo { Cantidad = 20, Descripcion = "Zapato elegante para fiestas", Nombre = "Zapato", Precio = 1200, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/fashionProducto1.jpg" };
                context.Productos.Add(articulo1);
                context.SaveChanges();
                var articulo2 = new Articulo { Cantidad = 6, Descripcion = "Zapato elegante para fiestas", Nombre = "Zapato2", Precio = 1000, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/fashionProducto2.jpg" };
                context.Productos.Add(articulo2);
                context.SaveChanges();

                var articulo3 = new Articulo { Cantidad = 2, Descripcion = "Zapato elegante para fiestas", Nombre = "Zapato3", Precio = 20000, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/fashionProducto3.jpg" };
                context.Productos.Add(articulo3);
                context.SaveChanges();
                var articulo4 = new Articulo { Cantidad = 76, Descripcion = "Zapato elegante para fiestas", Nombre = "Zapato4", Precio = 5600, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/fashionProducto4.jpg" };
                context.Productos.Add(articulo4);
                context.SaveChanges();

                var articulo5 = new Articulo { Cantidad = 20, Descripcion = "Zapato elegante para fiestas", Nombre = "Zapato5", Precio = 600, Negocio = negocio1, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/fashionProducto5.jpg" };
                context.Productos.Add(articulo5);
                context.SaveChanges();

                var articulo6 = new Articulo { Cantidad = 20, Descripcion = "Torta ideal para fiestas de cumpleaños", Nombre = "Torta de Cumpleaños1", Precio = 1200, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/totasProducto1.jpg" };
                context.Productos.Add(articulo6);
                context.SaveChanges();
                var articulo7 = new Articulo { Cantidad = 1, Descripcion = "Torta ideal para fiestas de cumpleaños", Nombre = "Torta de Cumpleaños2", Precio = 1000, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/totasProducto2.jpg" };
                context.Productos.Add(articulo7);
                context.SaveChanges();

                var articulo8 = new Articulo { Cantidad = 20, Descripcion = "Torta ideal para fiestas de cumpleaños", Nombre = "Torta de Cumpleaño3", Precio = 20000, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/totasProducto3.jpg" };
                context.Productos.Add(articulo8);
                context.SaveChanges();
                var articulo9 = new Articulo { Cantidad = 89, Descripcion = "Torta ideal para fiestas de cumpleaños", Nombre = "Torta de Cumpleaños4", Precio = 5600, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/totasProducto4.jpg" };
                context.Productos.Add(articulo9);
                context.SaveChanges();

                var articulo10 = new Articulo { Cantidad = 20, Descripcion = "Torta ideal para fiestas de cumpleaños", Nombre = "Torta de Cumpleaños5", Precio = 600, Negocio = negocio2, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/totasProducto5.jpg" };
                context.Productos.Add(articulo10);
                context.SaveChanges(); */


                /* var articulo11 = new Articulo { Cantidad = 20, Descripcion = "Remera casual", Nombre = "Remera", Precio = 300, Negocio = negocio5, UrlImagen = "/ImagenesServidor/ImagenesArticulos/Inicializar/remera.jpg" };
                context.Productos.Add(articulo11);
                context.SaveChanges(); */

                //Artículos Mariana: 
                var articulo12 = new Articulo { Cantidad = 20, Descripcion = "Ultimos pares!", Nombre = "Calzado", Precio = 750, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/e2KsE8/M1.jpg" };
                context.Productos.Add(articulo12);
                context.SaveChanges();
                var articulo13 = new Articulo { Cantidad = 20, Descripcion = "Color: Rosa. Ultimos talles!!! 36 y 38", Nombre = "Borcegos Mujer", Precio = 1800, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/fnSO48/M2.jpg" };
                context.Productos.Add(articulo13);
                context.SaveChanges();
                var articulo14 = new Articulo { Cantidad = 20, Descripcion = "Del 35 al 40. Material: Gamuza. Color: Negro.", Nombre = "Botas", Precio = 1500, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/mBXUu8/M3.jpg" };
                context.Productos.Add(articulo14);
                context.SaveChanges();
                var articulo15 = new Articulo { Cantidad = 20, Descripcion = "3 cuotas SIN INTERES de $500", Nombre = "Botas grises", Precio = 1500, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/mrtDMo/M4.jpg" };
                context.Productos.Add(articulo15);
                context.SaveChanges();
                var articulo16 = new Articulo { Cantidad = 20, Descripcion = "Talles del 35 al 39", Nombre = "Borcegos Blancos", Precio = 1400, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/kLBdnT/M5.jpg" };
                context.Productos.Add(articulo16);
                context.SaveChanges();
                var articulo17 = new Articulo { Cantidad = 1, Descripcion = "Solo talle 36", Nombre = "Borcegos azul marino. OFERTA!", Precio = 600, Negocio = negocio5, UrlImagen = "https://preview.ibb.co/hR24u8/M6.jpg" };
                context.Productos.Add(articulo17);
                context.SaveChanges();

                var articulo18 = new Articulo { Cantidad = 150, Descripcion = "Color: Blanco - Tamaño: 20Lts.", Nombre = "Membrana liquida para techos", Precio = 1600, Negocio = negocio6, UrlImagen = "https://image.ibb.co/itDeMy/Img1.jpg" };
                context.Productos.Add(articulo18);
                context.SaveChanges();
                var articulo19 = new Articulo { Cantidad = 300, Descripcion = "Decoralba Profesional - 20Lts.", Nombre = "Latex interior ALBA", Precio = 1250, Negocio = negocio6, UrlImagen = "https://image.ibb.co/jZr5uJ/Img2.jpg" };
                context.Productos.Add(articulo19);
                context.SaveChanges();
                var articulo20 = new Articulo { Cantidad = 1500, Descripcion = "Oferta! -10% de descuento en compra online", Nombre = "Cinta para pintor - Oferta!", Precio = 55, Negocio = negocio6, UrlImagen = "https://image.ibb.co/giX61y/Img3.jpg" };
                context.Productos.Add(articulo20);
                context.SaveChanges();
                var articulo21 = new Articulo { Cantidad = 250, Descripcion = "3 tamaños diferentes - Perfectos para el hogar", Nombre = "Pinceles x 3", Precio = 125, Negocio = negocio6, UrlImagen = "https://image.ibb.co/iSMoEJ/Img4.jpg" };
                context.Productos.Add(articulo21);
                context.SaveChanges();
                var articulo22 = new Articulo { Cantidad = 75, Descripcion = "Sherwin Williams Satinadoado", Nombre = "Latex interior 20 lts.", Precio = 3600, Negocio = negocio6, UrlImagen = "https://image.ibb.co/j30gZJ/Img5.jpg" };
                context.Productos.Add(articulo22);
                context.SaveChanges();
                var articulo23 = new Articulo { Cantidad = 20, Descripcion = "Blanco 20Lts", Nombre = "Frentes Recuplast", Precio = 2600, Negocio = negocio6, UrlImagen = "https://image.ibb.co/dgUVTd/Img6.jpg" };
                context.Productos.Add(articulo23);
                context.SaveChanges();

                var articulo24 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456781", Nombre = "Jean", Precio = 600, Negocio = negocio7, UrlImagen = "https://image.ibb.co/kQFyWy/1.jpg" };
                context.Productos.Add(articulo24);
                context.SaveChanges();
                var articulo25 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456782", Nombre = "Jean", Precio = 200, Negocio = negocio7, UrlImagen = "https://image.ibb.co/cYdcry/2.jpg" };
                context.Productos.Add(articulo25);
                context.SaveChanges();
                var articulo26 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456783", Nombre = "Jean", Precio = 300, Negocio = negocio7, UrlImagen = "https://image.ibb.co/j3kU4J/3.jpg" };
                context.Productos.Add(articulo26);
                context.SaveChanges();
                var articulo27 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456784", Nombre = "Jean", Precio = 350, Negocio = negocio7, UrlImagen = "https://image.ibb.co/dDgrJd/4.jpg" };
                context.Productos.Add(articulo27);
                context.SaveChanges();
                var articulo28 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456785", Nombre = "Jean", Precio = 400, Negocio = negocio7, UrlImagen = "https://image.ibb.co/dmWQdd/5.jpg" };
                context.Productos.Add(articulo28);
                context.SaveChanges();
                var articulo29 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456786", Nombre = "Jean", Precio = 500, Negocio = negocio7, UrlImagen = "https://image.ibb.co/jmioyd/6.jpg" };
                context.Productos.Add(articulo29);
                context.SaveChanges();
                var articulo30 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456787", Nombre = "Jean", Precio = 650, Negocio = negocio7, UrlImagen = "https://image.ibb.co/c212PJ/7.jpg" };
                context.Productos.Add(articulo30);
                context.SaveChanges();
                var articulo31 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456788", Nombre = "Jean", Precio = 270, Negocio = negocio7, UrlImagen = "https://image.ibb.co/nKg2PJ/8.jpg" };
                context.Productos.Add(articulo31);
                context.SaveChanges();
                var articulo32 = new Articulo { Cantidad = 100, Descripcion = "Codigo: 123456789", Nombre = "Jean", Precio = 380, Negocio = negocio7, UrlImagen = "https://image.ibb.co/iGhLBy/9.jpg" };
                context.Productos.Add(articulo32);
                context.SaveChanges(); 

                //ORDEN DE PEDIDO 
                /* var orden = new OrdenPedido { NegocioID = negocio5.ID, Pago = Pago.EFECTIVO, Envio = Envio.ACUERDO, Producto = articulo10, Total = 600, Usuario = usuario5, EstadoPedido = EstadoPedido.CONCRETADO };
                context.OrdenPedidos.Add(orden);
                context.SaveChanges();
                var orden2 = new OrdenPedido { NegocioID = negocio5.ID, Pago = Pago.EFECTIVO, Envio = Envio.ACUERDO, Producto = articulo10, Total = 600, Usuario = usuario5, EstadoPedido = EstadoPedido.CONCRETADO };
                context.OrdenPedidos.Add(orden2);
                context.SaveChanges();
                var orden3 = new OrdenPedido { NegocioID = negocio5.ID, Pago = Pago.EFECTIVO, Envio = Envio.ACUERDO, Producto = articulo10, Total = 600, Usuario = usuario5, EstadoPedido = EstadoPedido.PENDIENTE };
                context.OrdenPedidos.Add(orden3);
                context.SaveChanges(); */
            }



        }
    }
}
