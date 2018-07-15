using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class Showroom
    {
        public int  ID { get; set; }
        //public string Nombre { get; set; }
        // public string NombreVista { get; set; }
        
        //public string Html { get; set; }
        //public int NegocioID { get; set; }
        public virtual Negocio Negocio { get; set; }

       
        public int? BannerID { set; get; }
        public virtual Banner Banner { set; get; }

        public int? CuerpoID { set; get; }
        public virtual Cuerpo Cuerpo { set; get; }

        public string ColorDeFondo { get; set; }

        /// <summary>
        /// Si tiene baner devuelve true si no false
        /// AxelMolaro
        /// </summary>
        /// <returns></returns>
        public Boolean tieneBanner()
        {
            Boolean tiene = false;
            if (BannerID != null || Banner != null)
                tiene = true;

            return tiene;
        }

        public Boolean tieneCuerpo()
        {
            Boolean tiene = false;
            if (CuerpoID != null || Cuerpo != null)
                tiene = true;

            return tiene;
        }
    }

}
