using HiShop.Dao;
using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Enum;
using HiShop.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Showroom
{
    public class ShowroomDADGeneralModelAndView : ModelBase
    {
        public Entity.Showroom showroom  { get; set; }
        public List<ElementoMenuDAD> bannersMenu { get; set; }
        public List<ElementoMenuDAD> cuerpoInicioMenu { get; set; }
        public List<Producto> productos { get; set; }


        public ShowroomDADGeneralModelAndView()
        {

        }

        /// <summary>
        /// Constructor que llena los datos del menu
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="_context"></param>
        public ShowroomDADGeneralModelAndView(HttpContext httpContext, HiShopContext _context) : base(httpContext, _context)
        {
            llenarDatosGenerales(httpContext, _context);
            this.bannersMenu = Dao.ElementoMenuDADDao.getListado(_context, TipoElementoMenuDAD.Banner);
            this.cuerpoInicioMenu = Dao.ElementoMenuDADDao.getListado(_context, TipoElementoMenuDAD.CurepoHome);
            this.productos = Dao.ProductoDAo.getListado(_context, negocioModelBase);
        }

        /// <summary>
        /// Llenar los elementos del modelo
        /// </summary>
        /// <param name="_context"></param>
        public void llenarElmentos(HiShopContext _context,Entity.Negocio negocio)
        {
            this.showroom = ShowroomDao.get(_context, Convert.ToInt32(negocio.ShowroomID.ToString()));
            if (this.showroom.tieneBanner())
            {
                this.showroom.Banner = BannerDao.get(_context, Convert.ToInt32(this.showroom.BannerID.ToString()));
            }
            else {
                this.showroom.Banner = new Banner();

            }
            if (this.showroom.tieneCuerpo())
            {
                this.showroom.Cuerpo = CuerpoDao.get(_context, Convert.ToInt32(this.showroom.CuerpoID.ToString()));
            }
            else
            {
                this.showroom.Cuerpo = new Cuerpo();

            }
        }
      


    }
}
