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
        private static List<Model.MContent> articleList = new List<Model.MContent>();

        public ActionResult Index(string search = "", int catid = 0)
        {
            articleList = Logic.LContent.GetArticles();

            //var a = articleList.Where(m => m.ArticleID == 5822665).ToList();
            //var text = GetSrc(a[0].Conten);

            var list = (from l in articleList where l.Conten.Contains("<img src=") orderby Guid.NewGuid() select l).Take(6).ToList();
            List<ViewModels.VMArticle> vList = GetVmList(list);

            ViewData["ShowList"] = vList;  //首页图片轮播

            var ywList = (from l in articleList where l.Conten.Contains("<img src=") orderby l.ReleaseTime descending select l).Take(6).ToList();
            List<ViewModels.VMArticle> vywList = GetVmList(ywList);

            ViewData["NewList"] = vywList;  //最新要闻

            if (!string.IsNullOrEmpty(search))
            {
                articleList = articleList.Where(m => m.Title.Contains(search)).ToList();
            }

            if (catid != 0)
            {
                articleList = articleList.Where(m => m.DomainID == catid).ToList();
            }

            decimal total = articleList.Count;
            int pcount = Convert.ToInt32(Math.Ceiling(total / 10));  //页数

            ViewBag.PageCount = pcount;

            ViewBag.Search = search;
            ViewBag.CatID = catid;

            ViewModels.VMUser vUser = Session["LoginUser"] as ViewModels.VMUser;  //获取session

            ViewBag.IsLogin = vUser == null;
            ViewBag.Name = vUser == null ? "" : vUser.UserName;

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

            List<ViewModels.VMArticle> vList = GetVmList(list);

            return PartialView(vList);
        }

        public ActionResult ArticleInfo(long inArticleID)
        {
            ViewModels.VMArticle vModel = new ViewModels.VMArticle();

            var list = Logic.LContent.GetArticles();

            var vList = (from l in list where l.ArticleID == inArticleID select l).ToList();
            if (vList.Count > 0)
            {
                vModel.ArticleID = vList[0].ArticleID;
                vModel.Title = vList[0].Title;
                vModel.DomainID = vList[0].DomainID;
                vModel.ReleaseTime = ConvertLongToDateTime(vList[0].ReleaseTime).ToShortDateString();
                vModel.Conten = vList[0].Conten;

                ViewModels.VMUser vUser = Session["LoginUser"] as ViewModels.VMUser;  //获取session
                if (vModel != null)
                {
                    Model.MFootmarks footmarks = new Model.MFootmarks
                    {
                        UID = vUser.UID,
                        ArticleID = vModel.ArticleID,
                        FmTitle = vModel.Title
                    };

                    int result = Logic.LFootmarks.CreateFootmark(footmarks);
                }
            }

            return View(vModel);
        }

        public JsonResult DeleteFootmark(int fmid)
        {
            JsonResult json = new JsonResult();

            bool result = Logic.LFootmarks.DeleteFootmark(fmid) > 0;

            return json;
        }

        /// <summary>
        /// DateTime To long
        /// </summary>
        private static long ConvertDataTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }

        /// <summary>
        /// long To DateTime
        /// </summary>
        private static DateTime ConvertLongToDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }

        /// <summary>
        /// 获取文章ViewModel列表
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        private static List<ViewModels.VMArticle> GetVmList(List<Model.MContent> inList)
        {
            List<ViewModels.VMArticle> vList = new List<ViewModels.VMArticle>();

            foreach (var item in inList)
            {
                ViewModels.VMArticle vModel = new ViewModels.VMArticle
                {
                    ArticleID = item.ArticleID,
                    DomainID = item.DomainID,
                    Title = item.Title,
                    ReleaseTime = ConvertLongToDateTime(item.ReleaseTime).ToShortDateString(),
                    Conten = item.Conten,
                    ImgSrc = GetSrc(item.Conten)
                };

                vList.Add(vModel);
            }

            return vList;
        }

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string GetSrc(string str)
        {
            string src = string.Empty;

            int fstr = str.IndexOf("<img src=");
            if (fstr >= 0)
            {
                string content = str.Substring(fstr + 10);
                int lstr = content.IndexOf('"');
                if (lstr >= 0)
                {
                    src = content.Substring(0, lstr);
                }
            }

            return src;
        }
    }
}