using HiShop.Models;
using HiShop.Models.Facebook;
using HiShop.Controllers;
using HiShop.Entity.Data;
using HiShop.Herramientas;
using Microsoft.AspNetCore.Mvc;
using HiShop.Models.Data;

namespace FacebookLoginButton.Controllers
{
    public class AccountController : BaseCoreController
    {
        public AccountController(HiShopContext context) : base(context)
        {
        }

        public ActionResult Facebook()
        {
            return View();
        }

        [HttpPost]
        public DataModel FacebookLogin(FacebookLoginModel model)
        {
            HttpContext.Session.SetObjectAsJson("uid", model.uid);
            HttpContext.Session.SetObjectAsJson("accessToken" , model.accessToken);
            DataModel respuesta = new DataModel();
            respuesta.data.Add("uidData", model.uid);
            respuesta.data.Add("accessToken", model.accessToken);
            return respuesta;
        }

    }
}