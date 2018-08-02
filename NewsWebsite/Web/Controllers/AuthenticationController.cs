using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public JsonResult Login(ViewModels.VMUser inModel)
        {
            JsonResult json = new JsonResult();

            Model.MUsers model = Logic.LUsers.GetUsers(inModel.UserName, inModel.Password);

            

            json.Data = true;

            return json;
        }
    }
}