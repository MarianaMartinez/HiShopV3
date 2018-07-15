using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Data
{
    /// <summary>
    /// Esta clase la uso para mandar mas de un dato por ajax a la vista, solo tengo que irle setendo al data un string - object , tipo un Map 
    /// AxelMolaro
    /// </summary>
    public class DataModel
    {
        public Dictionary<string, object> data { get; set; }
        public string Redireccion { get; set; }
        public DataModel()
        {
            data = new Dictionary<string, object>();
        }


    }
}
