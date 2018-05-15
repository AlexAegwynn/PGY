using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Model.UserList model = LoginUser;
            if (model == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel
            {
                Name = model.Name,
                Email = model.Email,
                IsAdmin = Convert.ToBoolean(model.IsAdmin)
            };

            ViewData["LoginUser"] = vModel;

            return View();
        }

        public PartialViewResult UserListPartial()
        {
            List<Model.UserList> list = Logic.UserList.GetUsers();

            return PartialView();
        }

        public PartialViewResult UserPartial()
        {


            return PartialView();
        }

        public PartialViewResult CommodityPartial(string isAll = "")
        {
            ViewBag.IsAll = isAll == "All" ? true : false;

            return PartialView();
        }
    }
}