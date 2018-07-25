using System;
using System.Collections.Generic;
using System.Linq;
using HiShop.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using HiShop.Entity;
using HiShop.Dao;
using HiShop.Models.Showroom;
using HiShop.Herramientas;
using HiShop.Models.Data;

namespace HiShop.Controllers
{
    public class ShowroomController : BaseCoreController
    {
        public ShowroomController(HiShopContext context) : base(context)
        {

        }

        public IActionResult Ver(int id)
        {
            ShowroomVerModelAndView model = new ShowroomVerModelAndView();
            Negocio negocio = NegocioDao.get(_context, id);
            Showroom showroom = new Showroom();
            Banner banner = new Banner();
            Cuerpo cuerpo = new Cuerpo();
            model.llenarDatosGenerales(HttpContext, _context);
            if (negocio.Showroom != null || negocio.ShowroomID != null)
            {
                if (negocio.ShowroomID != null)
                {
                    showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID));
                }
                else
                {
                    showroom = negocio.Showroom;
                }
            }
            if (showroom.Banner != null || showroom.BannerID != null)
            {
                if (showroom.BannerID != null)
                {
                    banner = BannerDao.get(_context, Convert.ToInt32(showroom.BannerID));
                }
                else
                {
                    banner = showroom.Banner;
                }
            }
            if (showroom.Cuerpo != null || showroom.CuerpoID != null)
            {
                if (showroom.CuerpoID != null)
                {
                    cuerpo = CuerpoDao.get(_context, Convert.ToInt32(showroom.CuerpoID));
                }
                else
                {
                    cuerpo = showroom.Cuerpo;
                }
            }
            model.Showroom = showroom;
            model.Banner = banner;
            model.Cuerpo = cuerpo;

            //model.llenarElmentos(_context, negocio);
            return View(model);
        }

        public IActionResult EditarShowroom(/*int? id*/)
        {
            ShowroomDADGeneralModelAndView model = new ShowroomDADGeneralModelAndView(HttpContext, _context);
            Showroom showroom = null;
            Negocio negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");

            if (negocio.tieneShowroom())
            {
                showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID));
            }
            else
            {
                if (negocio.ShowroomID == null)
                {
                    Entity.Showroom showroomBDD = new Entity.Showroom();
                    ShowroomDao.grabar(_context, showroomBDD);
                    Negocio n = NegocioDao.get(_context, model.negocioModelBase.ID);
                    n.Showroom = showroomBDD;
                    NegocioDao.editar(_context, n);
                    HttpContext.Session.Remove("negocioEnSession");
                    Negocio negocioModel = n;
                    n.Showroom = null;
                    n.Productos = null;
                    HttpContext.Session.SetObjectAsJson("negocioEnSession", n);
                    model.negocioModelBase = negocioModel;
                }
                else
                {
                    showroom = ShowroomDao.get(_context, Convert.ToInt32(model.negocioModelBase.ShowroomID));
                }
            }

            if (showroom == null)
            {
                model.llenarElmentos(_context, model.negocioModelBase);
            }
            model.showroom = showroom;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditarShowroom(string html)
        {
            Negocio negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            //Showroom showroom;

            //showroom = new Entity.Data.Showroom();
            // showroom.Html = html;

            ShowroomDADGeneralModelAndView model = new ShowroomDADGeneralModelAndView();
            // model.llenarElmentos(_context);
            //model.html = html;
            return View(model);
        }

        [HttpPost]
        public DataModel GuardarShowroom(int idBanner, string colorBanner, string estaVacioSeccionBanner, bool esNuevoBanner, string textoPrincipalBanner,
            string colorFondoPrincipal,
            int idCuerpo, bool esNuevoCuerpo, string tipoDeLetraCuerpo, string estaVacioSeccionCuerpo)
        {
            Negocio negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            Showroom showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID));
            if (estaVacioSeccionBanner != "si")
                showroom = guardarSeccionBanner(showroom, idBanner, colorBanner, esNuevoBanner, textoPrincipalBanner);
            if (!String.IsNullOrEmpty("colorFondoPrincipal"))
                showroom = guardarSeccionGeneral(showroom, colorFondoPrincipal);
            if (estaVacioSeccionCuerpo != "si")
                showroom = guardarSeccionCuerpo(showroom, idCuerpo, esNuevoCuerpo, tipoDeLetraCuerpo);
            _context.Showrooms.Update(showroom);
            _context.SaveChanges();
            Negocio negocioEnSession = HttpContext.Session.GetObjectFromJson<HiShop.Entity.Negocio>("negocioEnSession");
            negocioEnSession.Showroom = showroom;
            DataModel respuesta = new DataModel();
            respuesta.data.Add("idBannerActual", showroom.BannerID);
            respuesta.data.Add("mensaje", "seGuardo");
            return respuesta;
        }
        /// <summary>
        /// Maneja las operaciones de la seccion de banner de un showroom al guardar, si usa en GuardarShowrrom
        /// </summary>
        /// <param name="idBanner"></param>
        /// <param name="colorBanner"></param>
        private Showroom guardarSeccionBanner(Showroom showroom, int idBanner, string colorBanner, bool esNuevoBanner, string textoPrincipal)
        {
            if (idBanner != 0)
            {
                if (showroom.tieneBanner() && !esNuevoBanner)
                {
                    if (showroom.Banner == null)
                        showroom.Banner = BannerDao.get(_context, Convert.ToInt32(showroom.BannerID));
                    showroom.Banner.llenarParaEdicion(null, null, textoPrincipal, colorBanner, null);
                }
                else
                {
                    showroom.BannerID = idBanner;
                    ElementoMenuDAD elemento = _context.ElementosMenuDADs.Single(m => m.ID == idBanner);
                    Banner banner = new Banner { Showroom = showroom, ColorBanner = colorBanner, TipoLetra = "", Html = elemento.Html, Titulo = textoPrincipal, UrlLogo = "" };
                    showroom.Banner = banner;
                    showroom.BannerID = idBanner;
                }
            }
            BannerDao.grabarActualizar(_context, showroom.Banner);
            //ShowroomDao.grabar(_context, showroom);
            return showroom;
        }

        private Showroom guardarSeccionGeneral(Showroom showroom, string colorFondoPrincipal)
        {
            showroom.ColorDeFondo = colorFondoPrincipal;
            return showroom;
        }

        private Showroom guardarSeccionCuerpo(Showroom showroom, int IdCuerpo, bool esNuevoCuerpo, string tipoDeLetraCuerpo)
        {
            if (IdCuerpo != 0)
            {
                if (showroom.tieneBanner() && !esNuevoCuerpo)
                {
                    if (showroom.Cuerpo == null)
                        showroom.Cuerpo = CuerpoDao.get(_context, Convert.ToInt32(showroom.CuerpoID));
                    showroom.Cuerpo.llenarParaEdicion(null, null, null, tipoDeLetraCuerpo);
                }
                else
                {
                    showroom.CuerpoID = IdCuerpo;
                    ElementoMenuDAD elemento = _context.ElementosMenuDADs.Single(m => m.ID == IdCuerpo);
                    Cuerpo cuerpo = new Cuerpo { Showroom = showroom, TipoDeLetra = "", Html = elemento.Html, UrlLogo = "" };
                    showroom.Cuerpo = cuerpo;
                    showroom.CuerpoID = IdCuerpo;
                }
            }
            CuerpoDao.grabarActualizar(_context, showroom.Cuerpo);
            //ShowroomDao.grabar(_context, showroom);
            return showroom;
        }

        /// <summary>
        /// Optiene los productos de un showrrom
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public DataModel OptenerProductos()
        {
            Negocio negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");
            List<Producto> productos = ProductoDAo.getListado(_context, negocio);
            DataModel respuesta = new DataModel();
            respuesta.data.Add("productos", productos.ToArray());
            return respuesta;
        }

        [HttpPost]
        public DataModel OptenerHtmlBanner(int idBanner)
        {
            Banner banner = null;
            if (idBanner != 0)
                banner = BannerDao.get(_context, idBanner);
            DataModel respuesta = new DataModel();

            respuesta.data.Add("html", banner != null ? banner.Html : "");
            return respuesta;
        }

        [HttpPost]
        public DataModel OptenerHtmlCuerpo(int idCuerpo)
        {
            Cuerpo cuerpo = null;
            if (idCuerpo != 0)
                cuerpo = CuerpoDao.get(_context, idCuerpo);
            DataModel respuesta = new DataModel();

            respuesta.data.Add("html", cuerpo != null ? cuerpo.Html : "");
            return respuesta;
        }

        public IActionResult DosColumnas()
        {
            return View();
        }

        public IActionResult TresColumnas()
        {
            return View();
        }

        public IActionResult CuatroColumnas()
        {
            return View();
        }

        public IActionResult Boxed2()
        {
            return View();
        }

        public IActionResult Boxed3()
        {
            return View();
        }

        public IActionResult Full2()
        {
            return View();
        }

        public IActionResult Full3()
        {
            return View();
        }

        public IActionResult FullDivision2()
        {
            return View();
        }

        public IActionResult FullDivision3()
        {
            return View();
        } 

        /// <summary>
        /// Una versión diferente para crear/editar el showroom 
        /// Mariana.
        /// </summary>
        /// <returns></returns>
        public IActionResult EditarShowroom2()
        {
            ShowroomDADGeneralModelAndView model = new ShowroomDADGeneralModelAndView(HttpContext, _context);
            Showroom showroom = null;
            Negocio negocio = HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession");

            if (negocio.tieneShowroom())
            {
                showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID)); 
            }
            else
            {
                if (negocio.ShowroomID == null)
                {
                    Showroom showroomBDD = new Showroom();
                    ShowroomDao.grabar(_context, showroomBDD);
                    Negocio n = NegocioDao.get(_context, model.negocioModelBase.ID);
                    n.Showroom = showroomBDD;
                    NegocioDao.editar(_context, n);
                    HttpContext.Session.Remove("negocioEnSession");
                    Negocio negocioModel = n;
                    n.Showroom = null;
                    n.Productos = null;
                    HttpContext.Session.SetObjectAsJson("negocioEnSession", n);
                    model.negocioModelBase = negocioModel; 
                }
                else
                {
                    showroom = ShowroomDao.get(_context, Convert.ToInt32(model.negocioModelBase.ShowroomID));
                }
            }

            if (showroom == null)
            {
                model.llenarElmentos(_context, model.negocioModelBase);
            }
            model.showroom = showroom;
            return View(model);
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Ver2(int id)
        {
            ShowroomVerModelAndView model = new ShowroomVerModelAndView();
            Negocio negocio = NegocioDao.get(_context, HttpContext.Session.GetObjectFromJson<Negocio>("negocioEnSession").ID);
            Showroom showroom = new Showroom();
            Banner banner = new Banner();
            Cuerpo cuerpo = new Cuerpo();
            model.llenarDatosGenerales(HttpContext, _context);
            if (negocio.Showroom != null || negocio.ShowroomID != null)
            {
                if (negocio.ShowroomID != null)
                {
                    showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID));
                }
                else
                {
                    showroom = negocio.Showroom;
                }
            }
            if (showroom.Banner != null || showroom.BannerID != null)
            {
                if (showroom.BannerID != null)
                {
                    banner = BannerDao.get(_context, Convert.ToInt32(showroom.BannerID));
                }
                else
                {
                    banner = showroom.Banner;
                }
            }
            if (showroom.Cuerpo != null || showroom.CuerpoID != null)
            {
                if (showroom.CuerpoID != null)
                {
                    cuerpo = CuerpoDao.get(_context, Convert.ToInt32(showroom.CuerpoID));
                }
                else
                {
                    cuerpo = showroom.Cuerpo;
                }
            }
            model.Showroom = showroom;
            model.Banner = banner;
            model.Cuerpo = cuerpo;

            return View(model);
        }
    }
}