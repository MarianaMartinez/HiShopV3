using HiShop.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiShop.Models
{
    public class ShowroomModelAndView
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string NombreVista { get; set; }
        public int NegocioFK { get; set; }


        public ShowroomModelAndView()
        {

        }

    }
}
