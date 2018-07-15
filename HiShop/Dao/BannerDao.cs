using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class BannerDao
    {
        /// <summary>
        /// Trae un banner por id
        /// Axel Molaro
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Banner get(HiShopContext context, int id)
        {
            Banner banner = null;
            try
            {
                banner = context.Banners.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al buscar el banner, verifique los datos .");
            }
            return banner;
        }


        public static void grabarActualizar(HiShopContext context, Banner banner)
        {
            if (banner != null)
            {
                if (  banner.ID != 0)
                {
                    context.Banners.Update(banner);
                }
                else
                {

                    context.Banners.Add(banner);
                }

                context.SaveChanges();
            }
        }
    }
}
