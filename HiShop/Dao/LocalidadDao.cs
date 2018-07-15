using HiShop.Entity;
using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class LocalidadDao
    {
        public static  Localidad get(HiShopContext _context, int id)
        {
            Localidad localidad;
            try
            {
                localidad =  _context.Localidades.Single(o => o.ID == id);
            }
            catch
            {
                throw new Exception("Ocurrio un error al buscar la localidad por id");
            }
            return localidad;
        }

        public static List<Localidad> getListado(HiShopContext _context)
        {
             List<Localidad> lista ;
            try {
                lista = _context.Localidades.ToList();
            }
            catch
            {
                throw new Exception("Ocurrio un error al traer el listado de localidades");
            }
            return lista;
        }

        public static List<Localidad> getLocalidadesPorProvincia(HiShopContext context, int idProvincia)
        {
                //Este metodo es medio lento pq trae todo verlo despue sAxelMolaro
             List<Localidad> Localidades = LocalidadDao.getListado(context);
             List<Localidad> LocalidadesPorProvincia = new List<Localidad>();
             foreach (var l in Localidades)
              {
                  if (l.Provincia.ID == idProvincia)
                  {
                        LocalidadesPorProvincia.Add(l);
                  }
              }
              return LocalidadesPorProvincia;
        }
    }
}
