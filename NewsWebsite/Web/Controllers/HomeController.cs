using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 页面文章列表
        /// </summary>
        //private static List<ViewModels.VMArticle> articleList = new List<ViewModels.VMArticle>();

        private static List<Model.MContent> articleList = new List<Model.MContent>();

        public ActionResult Index()
        {
            //List<Model.MContent> list = Logic.LContent.GetArticles();

            articleList = Logic.LContent.GetArticles();

            decimal total = articleList.Count;
            int page = Convert.ToInt32(Math.Ceiling(total / 10));

            //foreach (var item in list)
            //{
            //    ViewModels.VMArticle vModel = new ViewModels.VMArticle
            //    {
            //        ArticleID = item.ArticleID,
            //        Title = item.Title,
            //        DomainID = item.DomainID,
            //        ReleaseTime = item.ReleaseTime,
            //        Conten = item.Conten
            //    };
            //    articleList.Add(vModel);
            //}

            ViewBag.PageCount = page;

            return View();
        }

        public PartialViewResult ArticleList(int page = 0)
        {
            List<Model.MContent> list = articleList.Skip(10 * page).Take(10).ToList();
            List<ViewModels.VMArticle> vList = new List<ViewModels.VMArticle>();

            foreach (var item in list)
            {
                ViewModels.VMArticle vModel = new ViewModels.VMArticle
                {
                    ArticleID = item.ArticleID,
                    Title = item.Title,
                    DomainID = item.DomainID,
                    ReleaseTime = ConvertLongToDateTime(item.ReleaseTime).ToShortDateString(),
                    Conten = item.Conten
                };

                vList.Add(vModel);
            }

            return PartialView(vList);
        }


        // DateTime --> long
        private static long ConvertDataTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }


        // long --> DateTime
        private static DateTime ConvertLongToDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
    }
}