using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Dao
{
    public class ElementoMenuDADDao
    {
        /// <summary>
        /// Trae todos los elementos dle menu DAD
        /// AxelMolaro
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static List<ElementoMenuDAD> getListado(HiShopContext _context)
        {
            return _context.ElementosMenuDADs.ToList();

        }


        /// <summary>
        /// Trae todos los elementos dle menu dad que sean de un determinado Tipo
        /// AXEL MOLARO
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static List<ElementoMenuDAD> getListado(HiShopContext _context,TipoElementoMenuDAD tipoElemnto)
        {
            return _context.ElementosMenuDADs.ToList().Where(o => o.tipo == tipoElemnto).ToList(); ;

        }
    }
}
