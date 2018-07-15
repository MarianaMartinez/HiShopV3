using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Enum
{
    public enum BannerEnum
    {
       BASICO
    }
    public class BannerEnumClass
    {
        public string getString(BannerEnum bannerEnum)  
        {
            if (bannerEnum.Equals(BannerEnum.BASICO))
            {
                return "<div class='banner bannerElementoColocado container-fluid' style='background-color:orange;'> Banner </div >";
            }
            else
            {
                throw new Exception("Error al seleccionar el banner");
            }
        }
    }

}
