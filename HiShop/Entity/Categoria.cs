using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiShop.Entity
{
    /// <summary>
    /// Clase entity cateforia, ID indica que es la primari key, si una variable tiene el nombre y seguido un signo de pregutna, significa que s nulleable
    /// o sea que puede ser null, ñas relaciones se hacen con listas
    /// PedroCora
    /// </summary>
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Padre")]
        public int? PadreId { get; set; }
        public virtual Categoria Padre { get; set; }


    }
}
