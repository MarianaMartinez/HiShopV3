using HiShop.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiShop.Models
{
    /// <summary>
    /// Esta clase es utuilizada para manejar los mensaje principales que se muestran arriba
    /// AxelMolaro
    /// </summary>
    public class MensajeModel
    {
        private string v1;
        private string v2;

        public String texto { get; set; }
        public String tipo { get; set; }

        /// <summary>
        /// Constructor vacio de mensajeModel
        /// AxelMolaro
        /// </summary>
        public MensajeModel()
        {
           
        }

        /// <summary>
        /// Crea un mensaje a partir de que se le pasa el texto y el tipo
        /// AxelMolaro
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tipo"></param>
        public MensajeModel(String texto, TipoMensaje tipo)
        {
            this.texto = texto;
            this.tipo = llenarTipoMensaje(tipo);
        }

        public MensajeModel(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }


        /// <summary>
        /// Este metodo le agregar el tipo a un mensaje, lo que hacer es agregar una al tipo la clse de bootstrap alert correspondiente
        /// AxelMolaro
        /// </summary>
        /// <param name="tipo"></param>
        public  String llenarTipoMensaje(TipoMensaje tipo)
        {
            if (tipo.Equals(TipoMensaje.EXITO))
                {
                  return  "alert-success";
                }
            if (tipo.Equals(TipoMensaje.ERROR))
            {
                return "alert-danger";
            }
            if (tipo.Equals(TipoMensaje.ADVERTENCIA))
            {
                return "alert-warning";
            }
            return null;
        }
    }
}