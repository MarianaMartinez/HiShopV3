using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Entity
{
    public class Cuerpo : Elemento
    {
        public String  TipoDeLetra { get; set; }
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
        public void llenarParaEdicion(String html, String urlLogo, String titulo, String tipoLetra)
        {
            if (!String.IsNullOrEmpty(html))
                this.Html = html;

            if (!String.IsNullOrEmpty(urlLogo))
                this.UrlLogo = urlLogo;


            if (!String.IsNullOrEmpty(titulo))
                this.Titulo = titulo;


            if (!String.IsNullOrEmpty(tipoLetra))
                this.TipoDeLetra = tipoLetra;


        }
    }

}


