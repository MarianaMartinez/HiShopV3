using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public static class ProvinciaDao
    {
        public static async Task<Provincia> get(HiShopContext _context, int id)
        {
            Provincia provincia;
            try
            {
                 provincia = await _context.Provincias.SingleOrDefaultAsync(o => o.ID == id);
            }
            catch {
                throw new Exception("Ocurrio un error al buscar la provincia por id");
            }
            return provincia;
        }

        public static  List<Provincia> getListado(HiShopContext _context)
        {
            return  _context.Provincias.ToList();
        }
    }
}
