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
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (LoginUser.IsAdmin)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                return RedirectToAction("CommodityList");
            }
        }

        public ActionResult UserList()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Model.UserList> list = Logic.UserList.GetUsers();

            ViewData["LoginUser"] = LoginUser;
            ViewBag.Module = "comoruser";

            return View();
        }

        public ActionResult UserInfo()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();
            vModel.User = Logic.UserList.GetUser(LoginUser.UserID);

            ViewBag.Module = "info";

            return View(vModel);
        }

        public ActionResult CommodityList()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;           

            ViewBag.Module = "comoruser";

            return View();
        }

        public ActionResult AllCommodityList()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;
            ViewBag.Module = "allcomm";

            return View();
        }

        public ActionResult CommodityInfo(string inCommodityID = "")
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;
            ViewBag.Module = "comoruser";

            return View();
        }

        [HttpPost]
        public JsonResult AddCommodity()
        {
            JsonResult json = new JsonResult();

            return json;
        }

        [HttpPost]
        public JsonResult UpdateCommodity()
        {
            JsonResult json = new JsonResult();

            return json;
        }
    }
}