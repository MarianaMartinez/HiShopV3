using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class CuerpoDao
    {
        /// <summary>
        /// Trae un cuerpo por id
        /// Axel Molaro
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Cuerpo get(HiShopContext context, int id)
        {
            Cuerpo cuerpo = null;
            try
            {
                cuerpo = context.Cuerpos.Single(m => m.ID == id);
            }
            catch
            {
                throw new InvalidDataException("Ocurrio un error al buscar el cuerpo, verifique los datos .");
            }
            return cuerpo;
        }



        public static void grabarActualizar(HiShopContext context, Cuerpo cuerpo)
        {
          
            if (cuerpo.ID != 0)
            {
                context.Cuerpos.Update(cuerpo);
            }
            else
            {
                context.Cuerpos.Add(cuerpo);
            }

            context.SaveChanges();
        }
    }
}
