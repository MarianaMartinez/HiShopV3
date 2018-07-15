using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class ShowroomDao
    {

        /// <summary>
        /// Trae un listado de showeooms
        /// AXEL Molaro
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static List<Entity.Showroom> getListado(HiShopContext _context)
        {
            return _context.Showrooms.ToList();

        }

        /// <summary>
        /// Trae un showroom por id
        /// Axel Molaro
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Showroom get(HiShopContext context, int id)
        {
            Showroom showroom = null;
            try
            {
                showroom = context.Showrooms.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al buscar el showroom, verifique los datos .");
            }
            return showroom;
        }

        public static void grabar(HiShopContext context, Showroom showroom)
        {
            if (showroom.ID != 0)
            {
                context.Showrooms.Update(showroom);
            }
            else {

                context.Showrooms.Add(showroom);
            }
                context.SaveChanges();
        }



       
    }
}
