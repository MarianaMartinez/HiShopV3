using HiShop.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HiShop.Entity
{
    /// <summary>
    /// Clase entity usuario, ID indica ques es la primari ket, si una variable tiene el nombre y seguido un signo de pregutna, significa que s nulleable
    /// o sea que puede ser null, ñas relaciones se hacen con listas
    /// leer https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro
    /// AxelMolaro
    /// </summary>
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }
        public string UrlImagen { get; set; }
        public string IdentificacionFacebook { get; set; }
        //public Gradec Grade { get; set; } es nuleable , usuarioid o id es la clabe, y las listas corres ponden a otros
        public Estado Estado { get; set; }

        public int negocioActivo { get; set; }

        [JsonIgnore] 
       [IgnoreDataMember]
        public List<Negocio> Negocios { get; set; }

        public  List<OrdenPedido> OrdenPedidos { get; set; }
       

    }
}
