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
            return View();
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