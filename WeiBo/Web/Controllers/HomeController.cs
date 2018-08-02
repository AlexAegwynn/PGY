using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int catid = 0)
        {
            List<Model.MContent> list = Logic.LContent.GetContents();

            ViewBag.CatID = catid;

            return View();
        }
    }
}