using HiShop.Entity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class Banner : Elemento
    {
        public string ColorBanner { get; set; }
        public string TipoLetra { get; set; }

        //public int ShowroomID { get; set; }
        public virtual Showroom Showroom { get; set; }


        /// <summary>
        /// Edita algunos datos del banner pasador por parametro
        /// AxelMolaro
        /// </summary>
        /// <param name="html"></param>
        /// <param name="urlLogo"></param>
        /// <param name="titulo"></param>
        /// <param name="colorBaner"></param>
        /// <param name="tipoLetra"></param>
        public void llenarParaEdicion(String html, String urlLogo, String titulo, String colorBaner, String tipoLetra)
        {
            if (!String.IsNullOrEmpty(html))
                this.Html = html;


            if (!String.IsNullOrEmpty(urlLogo))
                this.UrlLogo = urlLogo;


            if (!String.IsNullOrEmpty(titulo))
                this.Titulo = titulo;


            if (!String.IsNullOrEmpty(colorBaner))
                this.ColorBanner = colorBaner;


            if (!String.IsNullOrEmpty(tipoLetra))
                this.TipoLetra = tipoLetra;


        }
    }

}
