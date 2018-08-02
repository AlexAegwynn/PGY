using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int catid = 0)
        {
            List<Model.MContent> list = Logic.LContent.GetContents();
            List<VMContent> vList = new List<VMContent>();

            foreach (var item in list)
            {
                VMContent vModel = new VMContent
                {
                    ArticleID = item.ArticleID,
                    DomainID = item.DomainID,
                    Conten = item.Conten,
                    ImgUrl = item.ImgUrl
                };
                vList.Add(vModel);
            }

            ViewBag.CatID = catid;

            return View(vList);
        }
    }
}