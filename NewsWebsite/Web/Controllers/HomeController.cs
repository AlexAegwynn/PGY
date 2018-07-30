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

        public ActionResult Index(string search = "")
        {
            articleList = Logic.LContent.GetArticles();

            if (!string.IsNullOrEmpty(search))
            {
                articleList = articleList.Where(m => m.Title.Contains(search)).ToList();
            }

            decimal total = articleList.Count;
            int pcount = Convert.ToInt32(Math.Ceiling(total / 10));

            ViewBag.PageCount = pcount;
            ViewBag.Search = search;

            return View();
        }

        public PartialViewResult ArticleList(int page = 0)
        {
            if (articleList == null)
            {
                articleList = Logic.LContent.GetArticles();
            }

            List<Model.MContent> list = articleList;

            list = list.Skip(10 * page).Take(10).ToList();

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

        public ActionResult ArticleInfo(long inArticleID)
        {
            ViewModels.VMArticle vModel = new ViewModels.VMArticle();

            if (articleList.Count > 0)
            {
                var vList = (from l in articleList where l.ArticleID == inArticleID select l).ToList();
                if (vList.Count > 0)
                {
                    vModel.ArticleID = vList[0].ArticleID;
                    vModel.Title = vList[0].Title;
                    vModel.DomainID = vList[0].DomainID;
                    vModel.ReleaseTime = ConvertLongToDateTime(vList[0].ReleaseTime).ToShortDateString();
                    vModel.Conten = vList[0].Conten;
                }
            }

            return View(vModel);
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